using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueTool.Tabs
    {
    public partial class StatusTab : UserControl
    {
        private LeagueConnection _lc;
        private dynamic currentUserData; // Dùng để lưu status hiện tại
        private List<string> favoriteStatuses = new List<string>();
        private const string FAVORITES_FILE = "status_favorites.json";
        private const string ENDPOINT = "/lol-chat/v1/me";
        private readonly Color _placeholderColor = Color.Gray; // Màu cho chữ mờ
        private string _currentPlaceholder = "(Trạng thái trống)"; // Chữ mờ

        public StatusTab(LeagueConnection leagueConnection)
        {
            InitializeComponent();
            _lc = leagueConnection;

            // Cập nhật trạng thái ban đầu (vô hiệu hóa nếu chưa kết nối)
            UpdateConnectionState(_lc.IsConnected);
        }

        private async void StatusTab_Load(object sender, EventArgs e)
        {
            // 1. Tải danh sách trạng thái vào ComboBox
            var availabilityItems = new[]
            {
                new { Name = "Online", Value = "chat" },
                new { Name = "Away", Value = "away" },
                new { Name = "Playing (DND)", Value = "dnd" },
                new { Name = "Mobile", Value = "mobile" },
                new { Name = "Offline", Value = "offline" }
            };

            availabilityComboBox.DataSource = availabilityItems;
            availabilityComboBox.DisplayMember = "Name";
            availabilityComboBox.ValueMember = "Value";

            // 2. Tải favorites (nếu có)
            LoadFavoritesFromFile();

            // 3. Tải thông tin người dùng hiện tại
            if (_lc.IsConnected)
            {
                await LoadCurrentUserData();
            }
        }

        // Cập nhật trạng thái (được gọi từ MainForm)
        public void UpdateConnectionState(bool isConnected)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(async () => {
                    this.Enabled = isConnected;
                    if (isConnected) await LoadCurrentUserData();
                }));
            }
            else
            {
                this.Enabled = isConnected;
            }
        }

        // Tải thông tin status/availability hiện tại từ LCU
        private async Task LoadCurrentUserData()
        {
            try
            {
                currentUserData = await _lc.Get(ENDPOINT);
                if (currentUserData == null) return;

                string currentStatus = currentUserData.statusMessage;
                string currentAvailability = currentUserData.availability;

                // CẬP NHẬT Ở ĐÂY
                _currentPlaceholder = string.IsNullOrEmpty(currentStatus) ? "(Trạng thái trống)" : currentStatus;
                availabilityComboBox.SelectedValue = currentAvailability;

                // Xóa text và gọi sự kiện Leave để mô phỏng placeholder
                statusTextBox.Text = "";
                statusTextBox_Leave(null, null); // Giả lập sự kiện Leave
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý nút "Áp dụng"
        private async void applyButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            bool statusUpdated = false;
            bool availUpdated = false;

            // 1. Cập nhật Status Message (nếu có thay đổi)
            string newStatus = statusTextBox.Text;
            if (!string.IsNullOrEmpty(newStatus) && newStatus != currentUserData.statusMessage)
            {
                var body = new JsonObject { { "statusMessage", newStatus } };
                await _lc.Put(ENDPOINT, SimpleJson.SerializeObject(body));
                statusTextBox.Text = ""; // Xóa text sau khi áp dụng
                statusUpdated = true;
            }

            // 2. Cập nhật Availability (nếu có thay đổi)
            string newAvailability = availabilityComboBox.SelectedValue.ToString();
            if (newAvailability != currentUserData.availability)
            {
                var body = new JsonObject { { "availability", newAvailability } };
                await _lc.Put(ENDPOINT, SimpleJson.SerializeObject(body));
                availUpdated = true;
            }

            // Tải lại dữ liệu và kích hoạt lại UI
            if (statusUpdated || availUpdated)
            {
                await LoadCurrentUserData(); // Tải lại để cập nhật placeholder
            }
            this.Enabled = true;
        }

        // Xử lý nút "Xóa Status"
        private async void clearButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            var body = new JsonObject { { "statusMessage", "" } };
            await _lc.Put(ENDPOINT, SimpleJson.SerializeObject(body));
            await LoadCurrentUserData();
            this.Enabled = true;
        }

        #region Favorites Logic

        // Tải favorites từ file
        private void LoadFavoritesFromFile()
        {
            if (File.Exists(FAVORITES_FILE))
            {
                try
                {
                    string json = File.ReadAllText(FAVORITES_FILE);
                    var data = SimpleJson.DeserializeObject<JsonObject>(json);
                    if (data.ContainsKey("statuses"))
                    {
                        favoriteStatuses = ((JsonArray)data["statuses"]).Select(o => o.ToString()).ToList();
                    }
                }
                catch { /* Bỏ qua nếu file hỏng */ }
            }
            UpdateFavoritesComboBox();
        }

        // Lưu favorites vào file
        private void SaveFavoritesToFile()
        {
            try
            {
                // Tạo một JsonArray rỗng
                var jsonArray = new JsonArray();
                // Thêm các mục từ danh sách của bạn vào
                jsonArray.AddRange(favoriteStatuses.Cast<object>());

                // Gán jsonArray đã có dữ liệu
                var data = new JsonObject { { "statuses", jsonArray } };
                File.WriteAllText(FAVORITES_FILE, SimpleJson.SerializeObject(data));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu favorites: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cập nhật ComboBox
        private void UpdateFavoritesComboBox()
        {
            favoritesComboBox.DataSource = null;
            favoritesComboBox.DataSource = favoriteStatuses;
            favoritesComboBox.SelectedItem = null;
        }

        // Nút "Lưu mới" (Lưu nội dung từ textbox)
        private void saveButton_Click(object sender, EventArgs e)
        {
            string statusToSave = statusTextBox.Text;
            if (string.IsNullOrWhiteSpace(statusToSave))
            {
                MessageBox.Show("Vui lòng nhập thông điệp status để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (favoriteStatuses.Contains(statusToSave))
            {
                MessageBox.Show("Status này đã tồn tại trong yêu thích.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            favoriteStatuses.Add(statusToSave);
            UpdateFavoritesComboBox();
            SaveFavoritesToFile();
            favoritesComboBox.SelectedItem = statusToSave;
        }

        // Nút "Chèn"
        private void insertButton_Click(object sender, EventArgs e)
        {
            if (favoritesComboBox.SelectedItem != null)
            {
                statusTextBox.Text = favoritesComboBox.SelectedItem.ToString();
            }
        }

        // Nút "Xóa"
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (favoritesComboBox.SelectedItem != null)
            {
                string selected = favoritesComboBox.SelectedItem.ToString();
                favoriteStatuses.Remove(selected);
                UpdateFavoritesComboBox();
                SaveFavoritesToFile();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một status yêu thích để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Khi click vào TextBox
        private void statusTextBox_Enter(object sender, EventArgs e)
        {
            if (statusTextBox.ForeColor == _placeholderColor)
            {
                statusTextBox.Text = "";
                statusTextBox.ForeColor = Color.Black; // Hoặc màu chữ gõ bình thường
            }
        }

        // Khi click ra ngoài TextBox
        private void statusTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(statusTextBox.Text))
            {
                statusTextBox.ForeColor = _placeholderColor;
                statusTextBox.Text = _currentPlaceholder;
            }
        }

        #endregion
    }
}