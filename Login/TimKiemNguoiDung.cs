using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Login
{
    public partial class TimKiemNguoiDung : Form
    {
        private NetworkStream _serverStream; // Biến giữ kết nối
        private string _myUserName;          // Tên của chính mình

        // --- MỚI: Biến để lưu người dùng đang được chọn trong danh sách ---
        private string _selectedUserId = "";
        private string _selectedUserName = "";
        private Button _currentSelectedButton = null; // Để xử lý hiệu ứng đổi màu nút đang chọn

        public TimKiemNguoiDung(NetworkStream stream, string myName)
        {
            InitializeComponent();
            _serverStream = stream;
            _myUserName = myName;

            // Cấu hình thanh cuộn cho danh sách
            flpDanhSach.AutoScroll = true;
            flpDanhSach.WrapContents = false; // Xếp theo cột dọc
            flpDanhSach.FlowDirection = FlowDirection.TopDown;
        }

        // --- 1. Nút TÌM KIẾM ---
        private void roundButton1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Texts.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập tên cần tìm!");
                return;
            }

            if (_serverStream == null || !_serverStream.CanWrite)
            {
                MessageBox.Show("Lỗi kết nối Server!");
                return;
            }

            try
            {
                // Reset lại lựa chọn cũ
                _selectedUserId = "";
                _currentSelectedButton = null;

                byte[] buffer = Encoding.UTF8.GetBytes($"TIM_KIEM|{keyword}");
                _serverStream.Write(buffer, 0, buffer.Length);
                _serverStream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi tin: " + ex.Message);
            }
        }

        // --- 2. Nút THÊM BẠN BÈ (Lấy từ người đang chọn trong flpDanhSach) ---
        // Nút thêm bạn bè 
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn ai chưa
            if (string.IsNullOrEmpty(_selectedUserId))
            {
                MessageBox.Show("Vui lòng bấm chọn một người trong danh sách bên dưới trước!", "Chưa chọn người dùng");
                return;
            }

            if (_selectedUserName == _myUserName)
            {
                MessageBox.Show("Không thể kết bạn với chính mình!");
                return;
            }
            try
            {
                // Gửi lệnh kết bạn kèm theo Tên người đó (hoặc ID tùy server bạn xử lý)
                // Ở đây mình gửi cả ID để chắc chắn: KET_BAN|ID_Nguoi_Muon_Ket
                byte[] buffer = Encoding.UTF8.GetBytes($"KET_BAN|{_selectedUserName}");
                _serverStream.Write(buffer, 0, buffer.Length);
                _serverStream.Flush();

                MessageBox.Show($"Đã gửi lời mời kết bạn tới {_selectedUserName}!");
                // làm sao phải thông báo cho người nhận biết được có lời mời kết bạn
                // phần này để server xử lý gửi tin nhắn thông báo
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi lời mời: " + ex.Message);
            }
        }

        // --- 3. HÀM XỬ LÝ KẾT QUẢ TỪ SERVER (Public để Form chính gọi vào) ---
        public void XuLyKetQuaTuServer(string messageFromServer)
        {
            // Format tin nhắn nhận được: "TIM_THAY|Tuan:001;Lan:002;Hung:003"

            string[] parts = messageFromServer.Split('|');
            if (parts.Length < 2) return;

            string dataPart = parts[1]; // Lấy phần dữ liệu phía sau
            string[] listUsers = dataPart.Split(';');

            // Dùng Invoke để vẽ giao diện an toàn từ luồng khác
            flpDanhSach.Invoke(new Action(() =>
            {
                flpDanhSach.Controls.Clear(); // Xóa danh sách cũ

                foreach (string item in listUsers)
                {
                    if (string.IsNullOrWhiteSpace(item)) continue;

                    // Tách Tên và ID (Format: Tên:ID)
                    string[] info = item.Split(':');
                    string tenUser = info[0];
                    string userId = (info.Length > 1) ? info[1] : ""; // Nếu không có ID thì để rỗng

                    TaoNutNguoiDung(tenUser, userId);
                }

                // Thông báo nếu danh sách rỗng
                if (flpDanhSach.Controls.Count == 0)
                {
                    Label lbl = new Label
                    {
                        Text = "Không tìm thấy người dùng nào!",
                        ForeColor = Color.White,
                        AutoSize = true,
                        Padding = new Padding(10)
                    };
                    flpDanhSach.Controls.Add(lbl);
                }
            }));
        }

        // --- 4. HÀM VẼ NÚT (Custom Giao Diện & Logic Chọn) ---
        private void TaoNutNguoiDung(string ten, string id)
        {
            Button btn = new Button();

            // Thiết kế giao diện nút
            btn.Text = "  " + ten;
            btn.Width = flpDanhSach.Width - 25; // Trừ hao thanh cuộn
            btn.Height = 60;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Tag = id; // Lưu ID ẩn trong nút

            // Màu mặc định (Xám tối)
            btn.BackColor = Color.FromArgb(54, 57, 63);
            btn.ForeColor = Color.FromArgb(220, 221, 222);

            // --- SỰ KIỆN CLICK (LOGIC CHỌN NGƯỜI DÙNG) ---
            btn.Click += (s, e) =>
            {
                // 1. Trả màu nút cũ về bình thường (nếu có)
                if (_currentSelectedButton != null)
                {
                    _currentSelectedButton.BackColor = Color.FromArgb(54, 57, 63);
                }

                // 2. Đổi màu nút vừa bấm (Xám sáng hơn để biết đang chọn)
                btn.BackColor = Color.FromArgb(88, 101, 242); // Màu xanh tím kiểu Discord Brand
                _currentSelectedButton = btn;

                // 3. Lưu thông tin vào biến toàn cục để nút "Thêm bạn bè" sử dụng
                _selectedUserId = id;
                _selectedUserName = ten;

                // (Tùy chọn) Hiện thông báo nhỏ hoặc Log
                // MessageBox.Show($"Đã chọn: {ten}");
            };

            flpDanhSach.Controls.Add(btn);
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            // Nút Xem Thông Tin của người dùng đang chọn
            if (string.IsNullOrEmpty(_selectedUserId))
            {
                MessageBox.Show("Vui lòng bấm chọn một người trong danh sách bên dưới trước!", "Chưa chọn người dùng");
                return;
            }
            try
            {
                // Gửi lệnh xem thông tin kèm theo Tên người đó (hoặc ID tùy server bạn xử lý)
                byte[] buffer = Encoding.UTF8.GetBytes($"XEM_THONG_TIN|{_selectedUserName}");
                _serverStream.Write(buffer, 0, buffer.Length);
                _serverStream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi yêu cầu xem thông tin: " + ex.Message);
            }
            // hiện vào trang thông tin người dùng

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flpDanhSach_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}