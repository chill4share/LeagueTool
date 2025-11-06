using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueTool.Tabs
{
    public partial class ResetTab : UserControl
    {
        private readonly LeagueConnection _lc;
        private readonly HttpClient _http = new HttpClient();

        public ResetTab(LeagueConnection lc)
        {
            InitializeComponent();
            _lc = lc;

            // Nếu đã kết nối sẵn → nạp ngay
            if (_lc?.IsConnected == true)
                _ = LoadEquippedAsync();
        }

        public void UpdateConnectionState(bool connected)
        {
            if (connected)
                _ = LoadEquippedAsync();
            else
                ClearAllPreviews();
        }

        // =========================
        // == TẢI DỮ LIỆU HIỂN THỊ ==
        // =========================
        private async Task LoadEquippedAsync()
        {
            try
            {
                statusLabel.Text = "Đang tải trang trí…";
                progressBar.Visible = true;

                // 1) Preferences người chơi (3 thử thách, title, crestBorder, bannerAccent)
                dynamic pref = await _lc.Get("/lol-challenges/v1/player-client-preferences");
                if (pref == null)
                {
                    // một số phiên bản dùng endpoint khác
                    pref = await _lc.Get("/lol-challenges/v1/player-preferences");
                }

                // Reset trước
                ClearAllPreviews();

                // 2) Challenges đang đeo — challengeIds: [id1, id2, id3] hoặc rỗng
                var ids = ReadIdArraySafe(pref, "challengeIds");
                long? id1 = ids.Count > 0 && ids[0] >= 0 ? ids[0] : (long?)null;
                long? id2 = ids.Count > 1 && ids[1] >= 0 ? ids[1] : (long?)null;
                long? id3 = ids.Count > 2 && ids[2] >= 0 ? ids[2] : (long?)null;

                await LoadChallengeBadgeAsync(id1, picChal1, lblChal1);
                await LoadChallengeBadgeAsync(id2, picChal2, lblChal2);
                await LoadChallengeBadgeAsync(id3, picChal3, lblChal3);

                // 3) Title (danh hiệu dạng chữ)
                string titleText = ReadStringSafe(pref, "title");
                if (string.IsNullOrWhiteSpace(titleText))
                {
                    // Có client trả titleId thay vì text — thử đọc "titleId"
                    long titleId = ReadLongSafe(pref, "titleId", -1);
                    if (titleId > 0)
                    {
                        // Thử tra tên từ /titles (nếu API có)
                        titleText = await TryResolveTitleFromListAsync(titleId);
                    }
                }
                lblTitleValue.Text = !string.IsNullOrWhiteSpace(titleText) ? titleText : "—";

                // 4) Crest Border (khung) và Banner Accent
                string crestPath = ReadStringSafe(pref, "crestBorder");
                string bannerPath = ReadStringSafe(pref, "bannerAccent");

                await LoadOneAssetPreviewAsync(crestPath, picCrestBorder);
                await LoadOneAssetPreviewAsync(bannerPath, picBanner);

                statusLabel.Text = "✅ Đã tải xong.";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "❌ Lỗi tải dữ liệu: " + ex.Message;
                ClearAllPreviews();
            }
            finally
            {
                progressBar.Visible = false;
            }
        }

        // Tải 1 badge theo challengeId
        private async Task LoadChallengeBadgeAsync(long? challengeId, PictureBox pic, Label lbl)
        {
            if (challengeId == null)
            {
                SetBadge(pic, lbl, null, "");
                return;
            }

            // /lol-challenges/v1/challenges/{id}/config → lấy icon + tên
            dynamic cfg = await _lc.Get($"/lol-challenges/v1/challenges/{challengeId}/config");
            if (cfg == null)
            {
                SetBadge(pic, lbl, null, "");
                return;
            }

            string name =
                ReadStringSafe(cfg, "name") ??
                ReadStringLocale(cfg, "localizedNames", "vi_VN", "name") ??
                ReadStringLocale(cfg, "localizedNames", "en_US", "name") ??
                $"Challenge {challengeId}";

            string assetPath =
                ReadStringSafe(cfg, "iconPath") ??
                ReadStringDeepSafe(cfg, "assets", "iconPath") ??
                ReadStringSafe(cfg, "clientIcon") ??
                ReadStringSafe(cfg, "crestIcon");

            Image img = await LoadCDragonImageAsync(assetPath);
            SetBadge(pic, lbl, img, name);
        }

        // Tải preview 1 asset (crest/border/banner…)
        private async Task LoadOneAssetPreviewAsync(string assetPath, PictureBox pic)
        {
            Image img = await LoadCDragonImageAsync(assetPath);
            pic.Image = img;
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.BackColor = Color.White;
        }

        private async Task<Image> LoadCDragonImageAsync(string assetPath)
        {
            if (string.IsNullOrWhiteSpace(assetPath))
                return null;

            string url = MapToCDragon(assetPath);
            if (string.IsNullOrWhiteSpace(url))
                return null;

            try
            {
                var bytes = await _http.GetByteArrayAsync(url);
                using (var ms = new System.IO.MemoryStream(bytes))
                    return Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }

        private static string MapToCDragon(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return null;

            if (path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return path; // đã là URL đầy đủ

            const string prefix = "/lol-game-data/assets/";
            if (path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                var lower = path.Substring(prefix.Length).TrimStart('/').ToLowerInvariant();
                return $"https://raw.communitydragon.org/latest/plugins/rcp-be-lol-game-data/global/default/{lower}";
            }
            return null;
        }

        // =========================
        // == UI TRỢ GIÚP          ==
        // =========================
        private void ClearAllPreviews()
        {
            SetBadge(picChal1, lblChal1, null, "");
            SetBadge(picChal2, lblChal2, null, "");
            SetBadge(picChal3, lblChal3, null, "");

            picCrestBorder.Image = null;
            picBanner.Image = null;
            lblTitleValue.Text = "—";
        }

        private void SetBadge(PictureBox pic, Label lbl, Image img, string text)
        {
            pic.Image = img;
            MakeCircle(pic);
            lbl.Text = text ?? "";
        }

        private void MakeCircle(PictureBox pic)
        {
            // bo tròn hình — hiển thị dạng O (vòng tròn)
            int d = Math.Min(pic.Width, pic.Height);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle((pic.Width - d) / 2, (pic.Height - d) / 2, d, d));
                pic.Region = new Region(gp);
            }
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.BackColor = Color.White;
        }

        // =========================
        // == ĐẶT LẠI & REFRESH    ==
        // =========================
        private async void resetButton_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar.Visible = true;
                statusLabel.Text = "Đang đặt lại thử thách…";

                // Bỏ đeo thử thách, không đụng title/crest/banner (tuỳ nhu cầu)
                // Nếu muốn bỏ luôn title/crest/banner, set "" tương ứng.
                string bodyJson = "{\"challengeIds\":[-1,-1,-1]}";

                await _lc.Post("/lol-challenges/v1/update-player-preferences", bodyJson);

                statusLabel.Text = "Đã đặt lại. Đang cập nhật hiển thị…";
                await LoadEquippedAsync();

                statusLabel.Text = "✅ Hoàn tất.";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "❌ Lỗi: " + ex.Message;
            }
            finally
            {
                progressBar.Visible = false;
            }
        }

        // =========================
        // == HÀM ĐỌC DYNAMIC SAFE ==
        // =========================
        private static List<long> ReadIdArraySafe(dynamic obj, string key)
        {
            var result = new List<long>();
            if (obj == null) return result;

            try
            {
                var arr = obj[key];
                if (arr == null) return result;

                foreach (var x in arr)
                {
                    try
                    {
                        long v = Convert.ToInt64(x);
                        result.Add(v);
                    }
                    catch { /* ignore */ }
                }
            }
            catch { /* ignore */ }

            return result;
        }

        private static string ReadStringSafe(dynamic obj, string key)
        {
            try
            {
                var v = obj[key];
                if (v == null) return null;
                string s = Convert.ToString(v);
                return string.IsNullOrWhiteSpace(s) ? null : s;
            }
            catch { return null; }
        }

        private static long ReadLongSafe(dynamic obj, string key, long def = 0)
        {
            try
            {
                var v = obj[key];
                if (v == null) return def;
                return Convert.ToInt64(v);
            }
            catch { return def; }
        }

        private static string ReadStringDeepSafe(dynamic obj, string parent, string child)
        {
            try
            {
                var p = obj[parent];
                if (p == null) return null;
                var v = p[child];
                if (v == null) return null;
                string s = Convert.ToString(v);
                return string.IsNullOrWhiteSpace(s) ? null : s;
            }
            catch { return null; }
        }

        private static string ReadStringLocale(dynamic obj, string parent, string locale, string child)
        {
            try
            {
                var p = obj[parent];
                if (p == null) return null;
                var loc = p[locale];
                if (loc == null) return null;
                var v = loc[child];
                if (v == null) return null;
                string s = Convert.ToString(v);
                return string.IsNullOrWhiteSpace(s) ? null : s;
            }
            catch { return null; }
        }

        private async Task<string> TryResolveTitleFromListAsync(long titleId)
        {
            try
            {
                // Nhiều bản client cung cấp endpoint liệt kê title; nếu không có sẽ catch
                dynamic titles = await _lc.Get("/lol-challenges/v1/titles");
                if (titles != null)
                {
                    foreach (var t in titles)
                    {
                        long id = ReadLongSafe(t, "id", -1);
                        if (id == titleId)
                        {
                            // Ưu tiên tiếng Việt nếu có
                            string vi = ReadStringLocale(t, "localizedNames", "vi_VN", "name");
                            if (!string.IsNullOrWhiteSpace(vi)) return vi;
                            string en = ReadStringLocale(t, "localizedNames", "en_US", "name");
                            if (!string.IsNullOrWhiteSpace(en)) return en;
                            string name = ReadStringSafe(t, "name");
                            if (!string.IsNullOrWhiteSpace(name)) return name;
                        }
                    }
                }
            }
            catch { /* ignore */ }
            return null;
        }
    }
}
