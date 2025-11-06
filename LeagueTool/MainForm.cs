using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using LeagueTool.Tabs;

namespace LeagueTool
{
    public partial class MainForm : Form
    {
        LeagueConnection lc;

        // Khai báo các UserControl (tab)
        HomeTab homeTabControl;
        ResetTab resetTabControl;
        StatusTab statusTabControl;
        BiographyTab biographyTabControl;

        // Biến lưu tab hiện tại
        Button currentActiveButton;

        public MainForm()
        {
            InitializeComponent();
            statusStrip.ForeColor = Color.Black; // giao diện sáng
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // KHỞI TẠO: tắt auto reconnect, dùng nút trong HomeTab để dò thủ công
            lc = new LeagueConnection(autoReconnect: false);

            // Không hiển thị hướng dẫn ở statusStrip nữa (tránh trùng với HomeTab)
            connectionStatusLabel.Text = "";

            lc.OnConnected += Lc_OnConnected;
            lc.OnDisconnected += Lc_OnDisconnected;

            // Khởi tạo các tab (HomeTab nhận lc)
            homeTabControl = new HomeTab(lc);
            resetTabControl = new ResetTab(lc);
            statusTabControl = new StatusTab(lc);
            biographyTabControl = new BiographyTab(lc);

            lc.Observe("/lol-summoner/v1/current-summoner", (data) => {
                homeTabControl.UpdateSummonerInfo(data);
            });

            // Style & bo góc cho các nút nav
            StyleNavButton(btnHome);
            StyleNavButton(btnReset);
            StyleNavButton(btnStatus);
            StyleNavButton(btnBiography);

            // Hiển thị tab Home làm tab mặc định
            ShowTab(homeTabControl, btnHome);
        }

        // Bo góc control bằng Region
        private void ApplyRounded(Control c, int radius = 8)
        {
            c.Resize += (s, e) =>
            {
                using (var path = new GraphicsPath())
                {
                    int w = c.Width, h = c.Height, d = radius * 2;
                    path.AddArc(0, 0, d, d, 180, 90);
                    path.AddArc(w - d, 0, d, d, 270, 90);
                    path.AddArc(w - d, h - d, d, d, 0, 90);
                    path.AddArc(0, h - d, d, d, 90, 90);
                    path.CloseAllFigures();
                    c.Region = new Region(path);
                }
            };
            c.PerformLayout();
        }

        // Style chuẩn cho nút nav (sáng, hover/active)
        private void StyleNavButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.Cursor = Cursors.Hand;
            b.BackColor = Color.Transparent;
            b.ForeColor = Color.Black;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 210, 210);
            ApplyRounded(b, 8);
        }

        // Active/Inactive màu nền
        private void SetActiveNav(Button active)
        {
            foreach (Control c in flowNav.Controls)
            {
                if (c is Button btn)
                {
                    bool isActive = ReferenceEquals(btn, active);
                    btn.BackColor = isActive ? Color.FromArgb(220, 220, 220) : Color.Transparent;
                    btn.ForeColor = Color.Black;
                }
            }
        }

        // Hàm chung để chuyển tab
        private void ShowTab(UserControl controlToShow, Button clickedButton)
        {
            contentPanel.Controls.Clear();
            controlToShow.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(controlToShow);

            SetActiveNav(clickedButton);
            currentActiveButton = clickedButton;
        }

        // Sự kiện kết nối LCU
        private void Lc_OnConnected()
        {
            if (InvokeRequired)
                Invoke(new Action(OnConnectedUI));
            else
                OnConnectedUI();
        }

        private void OnConnectedUI()
        {
            connectionStatusLabel.Text = ""; // để HomeTab hiển thị trạng thái
            homeTabControl?.UpdateConnectionState(true);
            resetTabControl?.UpdateConnectionState(true);
            statusTabControl?.UpdateConnectionState(true);
            biographyTabControl?.UpdateConnectionState(true);
        }

        // Sự kiện mất kết nối LCU
        private void Lc_OnDisconnected()
        {
            if (InvokeRequired)
                Invoke(new Action(OnDisconnectedUI));
            else
                OnDisconnectedUI();
        }

        private void OnDisconnectedUI()
        {
            connectionStatusLabel.Text = ""; // để HomeTab hiển thị trạng thái
            homeTabControl?.UpdateConnectionState(false);
            resetTabControl?.UpdateConnectionState(false);
            statusTabControl?.UpdateConnectionState(false);
            biographyTabControl?.UpdateConnectionState(false);
        }

        // Click handlers
        private void btnHome_Click(object sender, EventArgs e) => ShowTab(homeTabControl, btnHome);
        private void btnReset_Click(object sender, EventArgs e) => ShowTab(resetTabControl, btnReset);
        private void btnStatus_Click(object sender, EventArgs e) => ShowTab(statusTabControl, btnStatus);
        private void btnBiography_Click(object sender, EventArgs e) => ShowTab(biographyTabControl, btnBiography);

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
    }
}
