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

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false; // Vô hiệu hóa nút để tránh nhấn nhiều lần
            string tenDangNhap = textBox1.Text.Trim(); // Textbox tên đăng nhập
            string matKhau = textBox2.Text.Trim();      // Textbox mật khẩu

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi");
                return;
            }


            try
            {
                bool isPasswordValid = await Server.Database.KiemTraDangNhap(tenDangNhap, matKhau);

                if (isPasswordValid)
                {
                    string currentUserId = await Server.Database.LayIDNguoiDung(tenDangNhap);

                    if (string.IsNullOrEmpty(currentUserId))
                    {
                        MessageBox.Show("Lỗi nội bộ: Không thể lấy ID người dùng.", "Lỗi");
                        return;
                    }

                    // 2. Lưu thông tin phiên đăng nhập
                    PhienDangNhap.TaiKhoanHienTai = tenDangNhap;
                    PhienDangNhap.IDNguoiDungHienTai = currentUserId;

                    MessageBox.Show("Đăng nhập thành công!");

                    // 3. Mở form chính
                    ChatForm mainForm = new ChatForm(currentUserId, tenDangNhap, matKhau);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // Tên đăng nhập được tìm thấy nhưng mật khẩu sai HOẶC Tên đăng nhập không tồn tại
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                    // Kích hoạt lại nút đăng nhập
                    button1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi kết nối Firebase, lỗi cấu hình, v.v.
                MessageBox.Show("Lỗi kết nối hoặc xử lý Firebase: " + ex.Message, "Lỗi");
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            this.ActiveControl = panel2; // Đặt focus vào panel2 khi form load
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false; // Vô hiệu hóa nút để tránh nhấn nhiều lần
            string tenDangNhap = textBox1.Texts.Trim(); // Textbox tên đăng nhập
            string matKhau = textBox2.Texts.Trim();      // Textbox mật khẩu

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi");
                return;
            }


            try
            {
                bool isPasswordValid = await Server.Database.KiemTraDangNhap(tenDangNhap, matKhau);

                if (isPasswordValid)
                {
                    string currentUserId = await Server.Database.LayIDNguoiDung(tenDangNhap);

                    if (string.IsNullOrEmpty(currentUserId))
                    {
                        MessageBox.Show("Lỗi nội bộ: Không thể lấy ID người dùng.", "Lỗi");
                        return;
                    }

                    // 2. Lưu thông tin phiên đăng nhập
                    PhienDangNhap.TaiKhoanHienTai = tenDangNhap;
                    PhienDangNhap.IDNguoiDungHienTai = currentUserId;

                    MessageBox.Show("Đăng nhập thành công!");

                    // 3. Mở form chính
                    ChatForm mainForm = new ChatForm(currentUserId, tenDangNhap, matKhau);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // Tên đăng nhập được tìm thấy nhưng mật khẩu sai HOẶC Tên đăng nhập không tồn tại
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                    // Kích hoạt lại nút đăng nhập
                    button1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi kết nối Firebase, lỗi cấu hình, v.v.
                MessageBox.Show("Lỗi kết nối hoặc xử lý Firebase: " + ex.Message, "Lỗi");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMK quenMKForm = new QuenMK();
            quenMKForm.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mở form đăng ký
            DangKy dangKyForm = new DangKy();
            dangKyForm.Show();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Texts == "Username")
            {
                textBox1.Texts = "";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Texts))
            {
                textBox1.Texts = "Username";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Texts == "Password")
            {
                textBox2.Texts = "";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Texts))
            {
                textBox2.Texts = "Password";
                textBox2.ForeColor = Color.Silver;
            }
        }
    }
}