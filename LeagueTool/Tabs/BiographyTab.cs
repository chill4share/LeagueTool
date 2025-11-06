using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueTool.Tabs
{
    public partial class BiographyTab : UserControl
    {
        private LeagueConnection _lc;
        private const string API_ENDPOINT = "/lol-summoner/v1/current-summoner/summoner-profile";

        public BiographyTab(LeagueConnection leagueConnection)
        {
            InitializeComponent();
            _lc = leagueConnection;

            // Cập nhật trạng thái ban đầu
            UpdateConnectionState(_lc.IsConnected);
        }

        // Cập nhật trạng thái (được gọi từ MainForm)
        public void UpdateConnectionState(bool isConnected)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    this.Enabled = isConnected;
                    if (isConnected)
                    {
                        // Tự động tải tiểu sử khi kết nối
                        loadButton_Click(null, null);
                    }
                }));
            }
            else
            {
                this.Enabled = isConnected;
                if (isConnected)
                {
                    loadButton_Click(null, null);
                }
            }
        }

        // Tải tiểu sử hiện tại
        private async void loadButton_Click(object sender, EventArgs e)
        {
            if (!_lc.IsConnected) return;

            try
            {
                SetLoading(true, "Đang tải tiểu sử...");
                dynamic profile = await _lc.Get(API_ENDPOINT);
                if (profile != null && profile.ContainsKey("summary"))
                {
                    bioTextBox.Text = profile["summary"];
                    SetLoading(false, "Tải thành công.");
                }
            }
            catch (Exception ex)
            {
                SetLoading(false, $"Lỗi: {ex.Message}");
            }
        }

        // Áp dụng tiểu sử mới
        private async void applyButton_Click(object sender, EventArgs e)
        {
            if (!_lc.IsConnected) return;

            try
            {
                SetLoading(true, "Đang áp dụng...");
                string summary = bioTextBox.Text;

                // Tạo payload
                var payload = new JsonObject { { "summary", summary } };
                string jsonBody = SimpleJson.SerializeObject(payload);

                // Gửi POST
                await _lc.Post(API_ENDPOINT, jsonBody);

                SetLoading(false, "Áp dụng thành công!");
            }
            catch (Exception ex)
            {
                SetLoading(false, $"Lỗi: {ex.Message}");
            }
        }

        // Xóa tiểu sử
        private async void clearButton_Click(object sender, EventArgs e)
        {
            bioTextBox.Text = "";
            await Task.Delay(100);
            applyButton_Click(sender, e);
        }

        // Hàm helper
        private void SetLoading(bool isLoading, string message)
        {
            this.Enabled = !isLoading;
            statusLabel.Text = message;
            statusLabel.ForeColor = message.StartsWith("Lỗi") ? Color.Red : Color.WhiteSmoke;
        }
    }
}