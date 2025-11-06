using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace LeagueTool
{
    public class LeagueConnection
    {
        private static HttpClient HTTP_CLIENT;

        private WebSocket socketConnection;
        private Tuple<Process, string, string> processInfo;
        private bool connected;

        // Auto-reconnect control
        private CancellationTokenSource _retryCts;
        private bool _autoReconnectEnabled;
        private int _retryAttempt;
        private readonly Random _rng = new Random();

        public event Action OnConnected;
        public event Action OnDisconnected;
        public event Action<OnWebsocketEventArgs> OnWebsocketEvent;

        public bool IsConnected => connected;

        /// <summary>
        /// Khởi tạo kết nối LCU.
        /// autoReconnect: bật vòng auto-retry với exponential backoff (mặc định false).
        /// </summary>
        public LeagueConnection(bool autoReconnect = false)
        {
            try
            {
                HTTP_CLIENT = new HttpClient(new HttpClientHandler()
                {
                    SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
                    ServerCertificateCustomValidationCallback = (a, b, c, d) => true
                });
            }
            catch
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HTTP_CLIENT = new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (a, b, c, d) => true
                });
            }

            if (autoReconnect) StartAutoReconnect();
        }

        public void ClearAllListeners()
        {
            OnWebsocketEvent = null;
        }

        /// <summary>
        /// Thử dò và kết nối LCU 1 lần (không loop).
        /// </summary>
        public async Task<bool> TryConnectAsync()
        {
            try
            {
                if (connected) return true;

                var status = LeagueUtils.GetLeagueStatus();
                if (status == null)
                {
                    Debug.WriteLine("[LeagueConnection] ⏳ Chưa thấy client...");
                    return false;
                }

                // Tuple(Process, port, password)
                var port = status.Item2;
                var password = status.Item3;

                Debug.WriteLine($"[LeagueConnection] ✅ Tìm thấy client, port={port}, token={password}");

                var byteArray = Encoding.ASCII.GetBytes("riot:" + password);
                HTTP_CLIENT.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                string wsUrl = $"wss://127.0.0.1:{port}/";
                socketConnection = new WebSocket(wsUrl, "wamp");

                socketConnection.SetCredentials("riot", password, true);
                socketConnection.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
                socketConnection.SslConfiguration.ServerCertificateValidationCallback = (a, b, c, d) => true;

                socketConnection.OnMessage += HandleMessage;
                socketConnection.OnClose += HandleDisconnect;

                socketConnection.Connect();
                socketConnection.Send("[5,\"OnJsonApiEvent\"]");

                processInfo = status;
                connected = true;
                _retryAttempt = 0; // reset bộ đếm retry

                Debug.WriteLine("[LeagueConnection] 🔗 Kết nối thành công với LCU WebSocket.");
                OnConnected?.Invoke();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"[LeagueConnection] ❌ Lỗi khi kết nối: {e.Message}");
                processInfo = null;
                connected = false;
                return false;
            }
        }

        /// <summary>
        /// Bật vòng auto-retry với exponential backoff (2s → 4s → … → tối đa 30s + jitter).
        /// </summary>
        public void StartAutoReconnect()
        {
            if (_autoReconnectEnabled) return;
            _autoReconnectEnabled = true;
            _retryCts = new CancellationTokenSource();
            _ = AutoReconnectLoopAsync(_retryCts.Token);
        }

        /// <summary>
        /// Tắt auto-retry.
        /// </summary>
        public void StopAutoReconnect()
        {
            _autoReconnectEnabled = false;
            _retryCts?.Cancel();
            _retryCts = null;
        }

        private async Task AutoReconnectLoopAsync(CancellationToken ct)
        {
            _retryAttempt = 0;
            const int baseDelayMs = 2000;   // 2s
            const int maxDelayMs = 30000;   // 30s

            while (!ct.IsCancellationRequested)
            {
                if (connected)
                {
                    try { await Task.Delay(1000, ct); } catch { }
                    continue;
                }

                var ok = await TryConnectAsync();
                if (ok)
                {
                    try { await Task.Delay(1000, ct); } catch { }
                    continue;
                }

                _retryAttempt = Math.Min(_retryAttempt + 1, 10);
                var exp = Math.Min(maxDelayMs, (int)(baseDelayMs * Math.Pow(2, _retryAttempt - 1)));
                var jitter = _rng.Next(250, 1000);
                var delay = Math.Min(maxDelayMs, exp + jitter);

                Debug.WriteLine($"[LeagueConnection] ⏳ Thử lại sau {delay}ms (attempt={_retryAttempt})");
                try { await Task.Delay(delay, ct); } catch { }
            }
        }

        /// <summary>
        /// Dò thủ công khi người dùng nhấn nút.
        /// </summary>
        public async Task<bool> ManualFindAndConnectAsync()
        {
            StopAutoReconnect(); // tránh cạnh tranh với auto
            return await TryConnectAsync();
        }

        private void HandleDisconnect(object sender, CloseEventArgs args)
        {
            Debug.WriteLine("[LeagueConnection] ⚠️ Mất kết nối với LCU.");

            processInfo = null;
            connected = false;
            socketConnection = null;

            OnDisconnected?.Invoke();
            // Nếu auto đang bật → vòng backoff sẽ tự chạy; nếu tắt → chờ người dùng nhấn nút.
        }

        private void HandleMessage(object sender, MessageEventArgs args)
        {
            try
            {
                if (!args.IsText || string.IsNullOrWhiteSpace(args.Data))
                    return;

                // Bỏ qua nếu không phải chuỗi JSON mảng
                if (!args.Data.TrimStart().StartsWith("["))
                    return;

                var payload = SimpleJson.DeserializeObject<JsonArray>(args.Data);
                if (payload == null || payload.Count < 3)
                    return;

                if ((long)payload[0] != 8 || !((string)payload[1]).Equals("OnJsonApiEvent"))
                    return;

                dynamic ev = payload[2];
                if (ev == null) return;

                OnWebsocketEvent?.Invoke(new OnWebsocketEventArgs()
                {
                    Path = ev["uri"],
                    Type = ev["eventType"],
                    Data = ev["eventType"] == "Delete" ? null : ev["data"]
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[LeagueConnection] ⚠️ Lỗi xử lý WebSocket message: {ex.Message}");
            }
        }

        public async Task<dynamic> Get(string url)
        {
            if (!connected)
                throw new InvalidOperationException("Not connected to LCU");

            var res = await HTTP_CLIENT.GetAsync("https://127.0.0.1:" + processInfo.Item2 + url);
            var stringContent = await res.Content.ReadAsStringAsync();

            if (res.StatusCode == HttpStatusCode.NotFound)
                return null;

            return SimpleJson.DeserializeObject(stringContent);
        }

        public async Task Post(string url, string body)
        {
            if (!connected)
                throw new InvalidOperationException("Not connected to LCU");

            await HTTP_CLIENT.PostAsync(
                "https://127.0.0.1:" + processInfo.Item2 + url,
                new StringContent(body, Encoding.UTF8, "application/json")
            );
        }

        public async Task<dynamic> Put(string url, string body)
        {
            if (!connected)
                throw new InvalidOperationException("Not connected to LCU");

            var res = await HTTP_CLIENT.PutAsync(
                "https://127.0.0.1:" + processInfo.Item2 + url,
                new StringContent(body, Encoding.UTF8, "application/json")
            );

            var stringContent = await res.Content.ReadAsStringAsync();

            if (res.StatusCode == HttpStatusCode.NotFound)
                return null;

            return SimpleJson.DeserializeObject(stringContent);
        }

        public async void Observe(string url, Action<dynamic> handler)
        {
            OnWebsocketEvent += data =>
            {
                if (data.Path == url) handler(data.Data);
            };

            if (connected)
            {
                handler(await Get(url));
            }
            else
            {
                Action connectHandler = null;
                connectHandler = async () =>
                {
                    OnConnected -= connectHandler;
                    handler(await Get(url));
                };

                OnConnected += connectHandler;
            }
        }
    }

    public class OnWebsocketEventArgs : EventArgs
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public dynamic Data { get; set; }
    }
}
