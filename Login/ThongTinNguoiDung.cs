using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Server.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Login
{
    public partial class ThongTinNguoiDung : Form
    {
        public ThongTinNguoiDung()
        {
            InitializeComponent();
        }

        private async void ThongTinNguoiDung_Load(object sender, EventArgs e)
        {
            // Đoạn code này sẽ tạo một "vùng" (Region) bo tròn và áp dụng nó cho Form
            // Bạn có thể thay đổi số 20 để tăng/giảm độ bo góc
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 20; // Độ bo góc

            // Vẽ hình chữ nhật bo góc
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90); // Góc trên bên trái
            path.AddArc(this.Width - (radius * 2), 0, radius * 2, radius * 2, 270, 90); // Góc trên bên phải
            path.AddArc(this.Width - (radius * 2), this.Height - (radius * 2), radius * 2, radius * 2, 0, 90); // Góc dưới bên phải
            path.AddArc(0, this.Height - (radius * 2), radius * 2, radius * 2, 90, 90); // Góc dưới bên trái
            path.CloseFigure();

            // Áp dụng vùng bo tròn cho Form
            this.Region = new System.Drawing.Region(path);
            try
            {
                // load avatar từ database 
                // xác định đường dẫn avatar từ database theo tài khoản đăng nhập hiện tại
                string username = PhienDangNhap.TaiKhoanHienTai;
                if (!string.IsNullOrEmpty(username))
                {
                    byte[]? avatarData = Server.Database.LayAvatar(username);
                    if (avatarData != null && avatarData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(avatarData))
                        {
                            circularPictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // Nếu không có avatar, bạn có thể đặt một hình mặc định
                        // Giả sử bạn có một hình mặc định trong resources
                        circularPictureBox1.Image = Properties.Resources.user_default; // Thay đổi theo tên hình mặc định của bạn
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải avatar: " + ex.Message);
            }
            // Lấy từ database TaiKhoan_Server và hiển thị các thông tin người dùng khác tương tự

            // 1. Lấy userid của người đang đăng nhập
            int id = PhienDangNhap.IDNguoiDungHienTai;
            // 2. Gọi hàm để lấy thông tin
            UserInfo info = await Server.Database.LayThongTinNguoiDung(id);

            // 3. Hiển thị thông tin lên Form
            if (info != null)
            {
                // Gán giá trị vào các controls
                textBox1.Text = info.TenNguoiDung;
                textBox4.Text = info.Email;

                // Kiểm tra NgaySinh hợp lệ trước khi gán
                if (info.NgaySinh.Year > 1900) // Kiểm tra nếu không phải giá trị NULL mặc định
                {
                    // Định dạng ngày/tháng/năm
                    textBox2.Text = info.NgaySinh.ToString("dd/MM/yyyy");

                    // Bạn có thể dùng định dạng tiếng Việt: "dd tháng MM năm yyyy"
                    // txtNgaySinh.Text = user.NgaySinh.ToString("dd \\t\\h\\á\\n\\g MM yyyy");
                }
                else
                {
                    textBox2.Text = "Chưa cập nhật";
                }

                if (!string.IsNullOrEmpty(info.GioiTinh))
                {
                    textBox3.Text = info.GioiTinh;
                }
                else
                {
                    textBox3.Text = "Chưa cập nhật";
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng.");
            }
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void ThongTinNguoiDungForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void ThongTinNguoiDungForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void ThongTinNguoiDungForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void circularPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void roundPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChinhSuaTTND chinhSuaTTND = new ChinhSuaTTND();
            chinhSuaTTND.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // Lớp để chứa thông tin lấy từ cả 2 bảng

    }
}
