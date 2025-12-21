using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text;

namespace Login
{
    public partial class DanhSachBanBe : Form
    {
        private NetworkStream _serverStream;
        private string _myUserName;

        // Danh sách gốc chứa tất cả bạn bè (để khi xóa từ khóa tìm kiếm thì hiện lại hết)
        private List<string> _fullListBanBe;
        private Action<string> _callbackXoa;
        // Constructor nhận thêm List<string> listBanBe
        public DanhSachBanBe(NetworkStream stream, string myName, List<string> listBanBe, Action<string> onXoa)
        {
            InitializeComponent();
            _serverStream = stream;
            _myUserName = myName;

            // Lưu danh sách được truyền sang
            _fullListBanBe = listBanBe ?? new List<string>();
            _callbackXoa = onXoa;
            // Cấu hình khung chứa danh sách
            // Giả sử khung đen bên dưới bạn đặt tên là flpDanhSach (FlowLayoutPanel)
            // Nếu chưa có, hãy vào Designer thêm FlowLayoutPanel và đặt tên là flpDanhSach
            if (flpDanhSach != null)
            {
                flpDanhSach.AutoScroll = true;
                flpDanhSach.FlowDirection = FlowDirection.TopDown;
                flpDanhSach.WrapContents = false;
            }

            // Hiển thị toàn bộ danh sách lúc đầu
            HienThiDanhSach(_fullListBanBe);
        }

        // --- HÀM 1: NÚT TÌM KIẾM ---
        private void btnTim_Click(object sender, EventArgs e)
        {
            // Lấy từ khóa và chuyển về chữ thường
            string keyword = txtTimKiem.Texts.Trim().ToLower();

            // [DEBUG] Kiểm tra xem bạn gõ gì và danh sách gốc có bao nhiêu người
            // MessageBox.Show($"Tìm: '{keyword}' trong số {_fullListBanBe.Count} người.");

            if (string.IsNullOrEmpty(keyword))
            {
                HienThiDanhSach(_fullListBanBe);
            }
            else
            {
                // Lọc danh sách (so sánh chữ thường)
                List<string> ketQua = _fullListBanBe
                    .Where(ten => ten.ToLower().Contains(keyword))
                    .ToList();

                HienThiDanhSach(ketQua);
            }
        }

        // --- HÀM 2: HIỂN THỊ DANH SÁCH RA GIAO DIỆN ---
        private void HienThiDanhSach(List<string> data)
        {
            flpDanhSach.Controls.Clear(); // Xóa cũ

            if (data.Count == 0)
            {
                Label lbl = new Label
                {
                    Text = "Không tìm thấy ai!",
                    ForeColor = Color.White,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10, FontStyle.Italic)
                };
                flpDanhSach.Controls.Add(lbl);
                return;
            }

            foreach (string tenBan in data)
            {
                TaoNutBanBe(tenBan);
            }
        }

        // --- HÀM 3: TẠO GIAO DIỆN TỪNG DÒNG BẠN BÈ ---
        // Thay thế toàn bộ hàm TaoNutBanBe cũ bằng hàm này trong file DanhSachBanBe.cs
        private void TaoNutBanBe(string tenBan)
        {
            // 1. Tạo Panel chứa dòng bạn bè
            Panel pnlItem = new Panel();
            pnlItem.Size = new Size(flpDanhSach.Width - 25, 60);
            pnlItem.BackColor = Color.FromArgb(54, 57, 63);
            pnlItem.Margin = new Padding(0, 0, 0, 5);

            // 2. Label hiển thị Tên
            Label lblTen = new Label();
            lblTen.Text = tenBan;
            lblTen.ForeColor = Color.White;
            lblTen.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTen.Location = new Point(15, 18);
            lblTen.AutoSize = true;

            // 3. Nút XÓA BẠN BÈ
            Button btnXoa = new Button();
            btnXoa.Text = "Xóa bạn";
            btnXoa.BackColor = Color.IndianRed;
            btnXoa.ForeColor = Color.White;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Size = new Size(80, 30);
            btnXoa.Location = new Point(pnlItem.Width - 95, 15); // Căn phải
            btnXoa.Cursor = Cursors.Hand;

            // --- SỰ KIỆN CLICK NÚT XÓA (LOGIC CHÍNH) ---
            btnXoa.Click += (s, e) =>
            {
                DialogResult hoi = MessageBox.Show(
                    $"Bạn có chắc chắn muốn hủy kết bạn với {tenBan}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (hoi == DialogResult.Yes)
                {
                    try
                    {
                        // [GỬI LỆNH LÊN SERVER]
                        // Cấu trúc: XOA_BAN | Tên người cần xóa | \n
                        if (_serverStream != null && _serverStream.CanWrite)
                        {
                            string cmd = $"XOA_BAN|{tenBan}\n"; // Nhớ phải có \n
                            byte[] buffer = Encoding.UTF8.GetBytes(cmd);
                            _serverStream.Write(buffer, 0, buffer.Length);
                            _serverStream.Flush();
                        }

                        // [CẬP NHẬT GIAO DIỆN NGAY LẬP TỨC]
                        _fullListBanBe.Remove(tenBan); // Xóa khỏi danh sách lưu trong RAM
                        flpDanhSach.Controls.Remove(pnlItem); // Xóa dòng đó khỏi màn hình
                        pnlItem.Dispose(); // Giải phóng bộ nhớ
                        if (_callbackXoa != null)
                        {
                            _callbackXoa(tenBan);
                        }
                        MessageBox.Show($"Đã xóa {tenBan} khỏi danh sách bạn bè.");

                        // Nếu xóa hết danh sách thì hiện thông báo "Không tìm thấy ai"
                        if (_fullListBanBe.Count == 0) HienThiDanhSach(_fullListBanBe);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi kết nối Server: " + ex.Message);
                    }
                }
            };

            // 4. Thêm control vào Panel
            pnlItem.Controls.Add(lblTen);
            pnlItem.Controls.Add(btnXoa);

            // 5. Thêm Panel vào danh sách
            flpDanhSach.Controls.Add(pnlItem);
        }
    }
}