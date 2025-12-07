using System;
// Loại bỏ: using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using BCrypt.Net;
using System.Windows.Forms;
using System.Threading.Tasks; // Cần thiết cho các thao tác Firebase
using System.Collections.Generic;
using Server; // Cần để gọi lớp FirestoreDatabase và các hàm Helper

namespace Login
{
    public partial class QuenMK : Form
    {
        private string? verificationCode;

        public QuenMK()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = panel1; // Đặt focus vào panel1 khi form load
        }

        private async void buttonOTP_Click_1(object sender, EventArgs e)
        {
            string email = textBox2.Texts.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool emailExists = await Server.Database.KiemTraTonTaiEmail(email);

                if (!emailExists)
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.verificationCode = DangKy.GeneraiVerificationCode();
                DangKy.GuiEmailXacThuc(email, this.verificationCode);
                MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false; // Vô hiệu hóa nút để tránh nhấn nhiều lần
            string email = textBox2.Texts.Trim();
            string maOTP = textBoxOTP.Texts.Trim();
            string matKhau = textBox3.Texts.Trim();
            string xacNhanMatKhau = textBox4.Texts.Trim();

            // 1. Kiểm tra dữ liệu đầu vào và OTP
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(xacNhanMatKhau) ||
                string.IsNullOrWhiteSpace(maOTP))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (maOTP != this.verificationCode)
            {
                MessageBox.Show("Mã OTP xác nhận không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Mã hóa mật khẩu mới
            string matKhauDaBam = BCrypt.Net.BCrypt.HashPassword(matKhau);

            try
            {
                string? username = await Server.Database.LayUsernameTuEmail(email);

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Không tìm thấy email trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Bước B: Lấy ID Document (string) từ Username
                string? userID = await Server.Database.LayIDNguoiDung(username);

                if (string.IsNullOrEmpty(userID))
                {
                    MessageBox.Show("Lỗi nội bộ: Không thể xác định ID người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool updateSuccess = await Server.Database.UpdatePasswordAsync(userID, matKhauDaBam);

                if (updateSuccess)
                {
                    MessageBox.Show("Mật khẩu đã được đặt lại thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form sau khi đổi mật khẩu thành công
                }
                else
                {
                    MessageBox.Show("Cập nhật mật khẩu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Texts == "Email")
            {
                textBox2.Texts = "";
                textBox2.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Texts))
            {
                textBox2.Texts = "Email";
                textBox2.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void textBoxOTP_Enter(object sender, EventArgs e)
        {
            if (textBoxOTP.Texts == "Verify OTP")
            {
                textBoxOTP.Texts = "";
                textBoxOTP.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void textBoxOTP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTP.Texts))
            {
                textBoxOTP.Texts = "Verify OTP";
                textBoxOTP.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Texts == "Password")
            {
                textBox3.Texts = "";
                textBox3.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Texts))
            {
                textBox3.Texts = "Password";
                textBox3.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Texts == "Verify Password")
            {
                textBox4.Texts = "";
                textBox4.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Texts))
            {
                textBox4.Texts = "Verify Password";
                textBox4.ForeColor = System.Drawing.Color.Silver;
            }
        }
    }
}