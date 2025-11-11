using BCrypt.Net;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks; // Cần cho async/await
using System.Windows.Forms;
using Server; // Cần để gọi lớp FirestoreDatabase

namespace Login
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
            // Kết nối Firebase được xử lý trong FirestoreHelper
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        // Chuyển đổi hàm xử lý sự kiện sang async
        private async void button1_Click(object sender, EventArgs e)
        {
            string tenDangNhap = textBox1.Text.Trim(); // Textbox tên đăng nhập
            string matKhau = textBox2.Text.Trim();      // Textbox mật khẩu

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi");
                return;
            }

            // --- THAY THẾ LOGIC SQL BẰNG LOGIC FIRESTORE ---

            try
            {
                // Gọi hàm KiemTraDangNhap (Hàm này đã được sửa trong FirestoreDatabase để dùng WhereEqualTo và BCrypt.Verify)
                bool isPasswordValid = await Server.Database.KiemTraDangNhap(tenDangNhap, matKhau);

                if (isPasswordValid)
                {
                    // 1. Đăng nhập thành công, Lấy ID Document (string)
                    // Hàm LayIDNguoiDung phải là async và trả về string
                    string currentUserId = await Server.Database.LayIDNguoiDung(tenDangNhap);

                    if (string.IsNullOrEmpty(currentUserId))
                    {
                        MessageBox.Show("Lỗi nội bộ: Không thể lấy ID người dùng.", "Lỗi");
                        return;
                    }

                    // 2. Lưu thông tin phiên đăng nhập
                    PhienDangNhap.TaiKhoanHienTai = tenDangNhap;
                    // LƯU Ý: PhienDangNhap.IDNguoiDungHienTai giờ là STRING
                    PhienDangNhap.IDNguoiDungHienTai = currentUserId;

                    MessageBox.Show("Đăng nhập thành công!");

                    // 3. Mở form chính
                    ThongTinNguoiDung mainForm = new ThongTinNguoiDung();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // Tên đăng nhập được tìm thấy nhưng mật khẩu sai HOẶC Tên đăng nhập không tồn tại
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi kết nối Firebase, lỗi cấu hình, v.v.
                MessageBox.Show("Lỗi kết nối hoặc xử lý Firebase: " + ex.Message, "Lỗi");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Mở form đăng ký
            DangKy dangKyForm = new DangKy();
            dangKyForm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMK quenMKForm = new QuenMK();
            quenMKForm.Show();
        }
    }
}