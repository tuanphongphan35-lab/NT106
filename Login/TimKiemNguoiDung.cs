using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Agora.Rtc;

namespace Login
{
    public partial class TimKiemNguoiDung : Form
    {
        private NetworkStream _serverStream;
        private string _myUserName;

        // 1. Biến lưu danh sách bạn bè hiện tại (Đã thêm mới)
        private List<string> _danhSachBanBeHienTai;
        private ThongTinNguoiDung _frmInfo;
        private string _selectedUserId = "";
        private string _selectedUserName = "";
        private Button _currentSelectedButton = null;

        // 2. CONSTRUCTOR ĐÃ SỬA: Nhận thêm List<string> currentFriends
        public TimKiemNguoiDung(NetworkStream stream, string myName, List<string> currentFriends)
        {
            InitializeComponent();
            _serverStream = stream;
            _myUserName = myName;


            // Lưu danh sách bạn bè vào biến. Nếu null thì tạo list rỗng.
            _danhSachBanBeHienTai = currentFriends ?? new List<string>();

            // Cấu hình panel danh sách (nếu chưa chỉnh trong Design)
            flpDanhSach.AutoScroll = true;
            flpDanhSach.WrapContents = false;
            flpDanhSach.FlowDirection = FlowDirection.TopDown;
        }

        // --- NÚT TÌM KIẾM (Giữ nguyên) ---
        private void roundButton1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Texts.Trim();
            if (string.IsNullOrEmpty(keyword)) return;

            try
            {
                // Reset lựa chọn cũ
                _selectedUserId = "";
                _selectedUserName = "";
                _currentSelectedButton = null;

                // Gửi lệnh tìm kiếm
                string cmd = $"TIM_KIEM|{keyword}\n";
                byte[] buffer = Encoding.UTF8.GetBytes(cmd);
                _serverStream.Write(buffer, 0, buffer.Length);
                _serverStream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi tin: " + ex.Message);
            }
        }

        // --- NÚT THÊM BẠN BÈ (Đã cập nhật logic chặn trùng) ---
        // Lưu ý: Hãy đảm bảo bên Design bạn đã gán sự kiện Click của nút "Thêm bạn bè" vào hàm này
        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đã chọn người chưa
            if (string.IsNullOrEmpty(_selectedUserName))
            {
                MessageBox.Show("Vui lòng chọn người cần kết bạn!", "Thông báo");
                return;
            }

            // 2. Kiểm tra có phải chính mình không
            if (_selectedUserName == _myUserName)
            {
                MessageBox.Show("Không thể kết bạn với chính mình!", "Lỗi");
                return;
            }

            // 3. [QUAN TRỌNG] Kiểm tra xem đã là bạn bè chưa
            bool daLaBanBe = _danhSachBanBeHienTai.Any(ban =>
                ban.Equals(_selectedUserName, StringComparison.OrdinalIgnoreCase));

            if (daLaBanBe)
            {
                MessageBox.Show($"Bạn và {_selectedUserName} đã là bạn bè rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Dừng lại tại đây
            }

            // 4. Gửi lời mời nếu hợp lệ
            try
            {
                string cmd = $"KET_BAN|{_selectedUserName}\n";
                byte[] buffer = Encoding.UTF8.GetBytes(cmd);
                _serverStream.Write(buffer, 0, buffer.Length);
                _serverStream.Flush();
                MessageBox.Show($"Đã gửi lời mời tới {_selectedUserName}!", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        // --- XỬ LÝ KẾT QUẢ TỪ SERVER (Giữ nguyên để hiển thị danh sách) ---
        public void XuLyKetQuaTuServer(string messageFromServer)
        {
            string[] parts = messageFromServer.Split('|');
            if (parts.Length < 2) return;

            string dataPart = parts[1];
            string[] listUsers = dataPart.Split(';');

            // Dùng Invoke để cập nhật giao diện từ luồng khác
            flpDanhSach.Invoke(new Action(() =>
            {
                flpDanhSach.Controls.Clear();

                foreach (string item in listUsers)
                {
                    if (string.IsNullOrWhiteSpace(item)) continue;

                    // Server có thể trả về "TenUser" hoặc "TenUser:ID"
                    string[] info = item.Split(':');
                    string tenUser = info[0];
                    string userId = (info.Length > 1) ? info[1] : "";

                    TaoNutNguoiDung(tenUser, userId);
                }

                if (flpDanhSach.Controls.Count == 0)
                {
                    Label lbl = new Label { Text = "Không tìm thấy!", ForeColor = Color.White, AutoSize = true };
                    flpDanhSach.Controls.Add(lbl);
                }
            }));
        }

        // --- HÀM TẠO NÚT HIỂN THỊ NGƯỜI DÙNG ---
        private void TaoNutNguoiDung(string ten, string id)
        {
            Button btn = new Button();

            bool isFriend = _danhSachBanBeHienTai.Any(ban =>
            ban.Trim().Equals(ten.Trim(), StringComparison.OrdinalIgnoreCase) 
            || ban.Contains(ten) 
            );
            if (isFriend)
            {
                btn.Text = "✓ " + ten + " (Bạn bè)";
                btn.ForeColor = Color.LimeGreen;
            }
            else
            {
                btn.Text = "  " + ten;
                btn.ForeColor = Color.FromArgb(220, 221, 222);
            }

            btn.Width = flpDanhSach.Width - 25;
            btn.Height = 60;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Tag = id;
            btn.BackColor = Color.FromArgb(54, 57, 63);

            // Sự kiện khi bấm vào tên người dùng
            btn.Click += (s, e) =>
            {
                // Reset màu nút cũ
                if (_currentSelectedButton != null) _currentSelectedButton.BackColor = Color.FromArgb(54, 57, 63);

                // Highlight nút mới
                btn.BackColor = Color.FromArgb(88, 101, 242);
                _currentSelectedButton = btn;

                // Lưu lại người đang chọn để dùng cho nút "Thêm bạn bè"
                _selectedUserId = id;
                _selectedUserName = ten;
            };

            flpDanhSach.Controls.Add(btn);
        }

        // Nút Đóng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Nút Xem thông tin (Tùy chọn)
        private void btnXem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUserId))
            {
                MessageBox.Show("Vui lòng chọn người cần xem!", "Thông báo");
                return;
            }

            if (_frmInfo != null && !_frmInfo.IsDisposed)
            {
                _frmInfo.Close();
                return; 
            }

            _frmInfo = new ThongTinNguoiDung(_selectedUserId);

            _frmInfo.StartPosition = FormStartPosition.CenterScreen;
            _frmInfo.Show();
        }

    }
}