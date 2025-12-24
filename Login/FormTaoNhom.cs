using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Login // Đổi namespace theo project của bạn (VD: Login hoặc ChatApp)
{
    public partial class FormTaoNhom : Form
    {
        private NetworkStream _serverStream;
        private List<string> _danhSachBanBe;
        // Constructor nhận vào Stream (để gửi lệnh) và Danh sách bạn bè hiện có
        public FormTaoNhom(NetworkStream stream, List<string> listBanBe)
        {
            InitializeComponent();
            this._serverStream = stream;
            this._danhSachBanBe = listBanBe;
        }

        private void FormTaoNhom_Load(object sender, EventArgs e)
        {
            clbDanhSachBanBe.Controls.Clear();
            if (_danhSachBanBe != null)
            {
                foreach (string ban in _danhSachBanBe)
                {
                    CheckBox chk = new CheckBox();
                    chk.Text = "  " + ban; // Thêm khoảng trắng cho đẹp
                    chk.ForeColor = Color.White;
                    chk.Font = new Font("Segoe UI", 11);
                    chk.AutoSize = false;
                    chk.Size = new Size(clbDanhSachBanBe.Width - 25, 40); // Chiều rộng trừ thanh cuộn, cao 40px
                    chk.Padding = new Padding(10, 0, 0, 0); // Cách lề trái
                    chk.Cursor = Cursors.Hand;

                    // Hiệu ứng khi di chuột vào (Hover)
                    chk.MouseEnter += (s, ev) => chk.BackColor = Color.FromArgb(60, 60, 70);
                    chk.MouseLeave += (s, ev) => chk.BackColor = Color.Transparent;

                    clbDanhSachBanBe.Controls.Add(chk);
                }
            }
        }

        private void btnTaoNhom_Click(object sender, EventArgs e)
        {
            string tenNhom = txtTenNhom.Texts.Trim();

            // 1. Kiểm tra tên nhóm
            if (string.IsNullOrEmpty(tenNhom))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm!");
                return;
            }

            // 2. Kiểm tra số lượng thành viên
            // Yêu cầu: Nhóm >= 3 người (Bao gồm người tạo)
            // => Phải chọn ít nhất 2 người bạn
            List<string> selectedMembers = new List<string>();

            // Duyệt qua panel để tìm CheckBox được tick
            foreach (Control c in clbDanhSachBanBe.Controls)
            {
                if (c is CheckBox chk && chk.Checked)
                {
                    selectedMembers.Add(chk.Text.Trim());
                }
            }

            if (selectedMembers.Count < 2)
            {
                MessageBox.Show("Cần chọn ít nhất 2 người bạn!");
                return;
            }

            // Tạo chuỗi: User1;User2;User3
            string memberString = string.Join(";", selectedMembers);

            // 4. Gửi lệnh lên Server
            // Format: TAO_NHOM | Tên Nhóm | Danh sách thành viên
            try
            {
                string cmd = $"TAO_NHOM|{tenNhom}|{memberString}\n";
                byte[] buffer = Encoding.UTF8.GetBytes(cmd);
                _serverStream.Write(buffer, 0, buffer.Length);
                _serverStream.Flush();

                // Đóng form sau khi gửi lệnh, đợi Server phản hồi ở ChatForm
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi yêu cầu: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTenNhom_Enter(object sender, EventArgs e)
        {
            // Nếu nội dung đang là chữ gợi ý -> Xóa đi để người dùng nhập
            if (txtTenNhom.Texts == "Nhập tên nhóm...")
            {
                txtTenNhom.Texts = "";
                txtTenNhom.ForeColor = Color.White; // Đổi màu chữ sang Trắng (hoặc màu sáng) để nhập
            }
        }

        private void txtTenNhom_Leave(object sender, EventArgs e)
        {
            // Nếu người dùng chưa nhập gì (hoặc chỉ nhập khoảng trắng) -> Hiện lại gợi ý
            if (string.IsNullOrWhiteSpace(txtTenNhom.Texts))
            {
                txtTenNhom.Texts = "Nhập tên nhóm...";
                txtTenNhom.ForeColor = Color.Gray; // Đổi màu chữ sang Xám cho giống gợi ý
            }
        }
    }
}