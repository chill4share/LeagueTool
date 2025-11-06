using System;
using System.Drawing;
using System.Windows.Forms;

namespace LeagueTool.Tabs
{
    public partial class HomeTab : UserControl
    {
        private readonly LeagueConnection _lc;

        public HomeTab(LeagueConnection lc)
        {
            InitializeComponent();
            _lc = lc;

            // Thông điệp khởi động (sẽ bị thay khi bấm "Tìm client")
            findStatusLabel.Text = "🔍 Vào tab Home và nhấn 'Tìm client' để dò LCU";
            findStatusLabel.ForeColor = Color.Black;

            // Trạng thái ban đầu của nút/label theo kết nối hiện có
            UpdateConnectionState(_lc?.IsConnected == true);
        }

        // Nhận dữ liệu Summoner từ MainForm.Observe
        public void UpdateSummonerInfo(dynamic summonerData)
        {
            if (summonerData == null) return;

            try
            {
                string name = summonerData["displayName"];
                string tag = summonerData["tagLine"];
                string level = summonerData["summonerLevel"].ToString();

                Action ui = () =>
                {
                    // Header hồ sơ
                    summonerNameLabel.Text = $"{name} #{tag}";
                    levelLabel.Text = $"Cấp: {level}";

                    // Rank: để trống nếu bạn chưa fetch API rank riêng
                    if (string.IsNullOrEmpty(rankLabel.Text))
                        rankLabel.Text = "Hạng: —";
                };

                if (InvokeRequired) Invoke(ui); else ui();
            }
            catch
            {
                Action ui = () =>
                {
                    summonerNameLabel.Text = "Lỗi tải thông tin";
                    levelLabel.Text = "";
                    rankLabel.Text = "";
                };
                if (InvokeRequired) Invoke(ui); else ui();
            }
        }

        // MainForm gọi khi trạng thái kết nối thay đổi
        public void UpdateConnectionState(bool isConnected)
        {
            Action ui = () =>
            {
                btnFindClient.Enabled = !isConnected;

                if (isConnected)
                {
                    findStatusLabel.Text = "✅ Đã kết nối LCU";
                    findStatusLabel.ForeColor = Color.ForestGreen;
                }
                else
                {
                    // Khi mất kết nối: hiển thị hướng dẫn lại
                    findStatusLabel.Text = "🔍 Vào tab Home và nhấn 'Tìm client' để dò LCU";
                    findStatusLabel.ForeColor = Color.Black;

                    // Reset header
                    summonerNameLabel.Text = "Đang chờ...";
                    levelLabel.Text = "";
                    rankLabel.Text = "";
                }
            };

            if (InvokeRequired) Invoke(ui); else ui();
        }

        // Nút Tìm client (góc dưới-phải)
        private async void btnFindClient_Click(object sender, EventArgs e)
        {
            if (_lc == null) return;

            btnFindClient.Enabled = false;
            findStatusLabel.Text = "🔎 Đang tìm client...";
            findStatusLabel.ForeColor = Color.Black;

            try
            {
                var ok = await _lc.ManualFindAndConnectAsync();
                if (!ok)
                {
                    findStatusLabel.Text = "❌ Không tìm thấy client. Mở game hoặc thử lại.";
                    findStatusLabel.ForeColor = Color.IndianRed;
                    btnFindClient.Enabled = true;
                }
                else
                {
                    findStatusLabel.Text = "✅ Đã kết nối LCU";
                    findStatusLabel.ForeColor = Color.ForestGreen;
                }
            }
            catch (Exception ex)
            {
                findStatusLabel.Text = "❌ Lỗi khi dò: " + ex.Message;
                findStatusLabel.ForeColor = Color.IndianRed;
                btnFindClient.Enabled = true;
            }
        }
    }
}
