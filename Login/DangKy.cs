using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Threading.Tasks;
using System.Text;
using Server;
using System.Windows.Forms;

namespace Login
{
    public partial class DangKy : Form
    {
        private string? verificationCode;
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        // TẠO ID TỰ ĐỘNG
        private string? UsersID;
        private byte[]? fileAnh = null;
        public DangKy()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox1.Text = "ID tự động";
        }


        private async void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            string email = textBox2.Texts.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email để gửi mã xác nhận.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                // Sử dụng phương thức từ lớp Database để kiểm tra tồn tại email
                bool emailExists = await Server.Database.KiemTraTonTaiEmail(email);
                if (emailExists)
                {
                    MessageBox.Show("Email này đã được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Nếu OK thì gửi mã xác thực
                this.verificationCode = GeneraiVerificationCode();
                GuiEmailXacThuc(email, this.verificationCode);
                MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static string GeneraiVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public static void GuiEmailXacThuc(string email, string verificationCode)
        {
            try
            {
                string? fromAddress = ConfigurationManager.AppSettings["GmailAddress"];
                string? fromPassword = ConfigurationManager.AppSettings["GmailAppPassword"];
                if (string.IsNullOrEmpty(fromAddress) || string.IsNullOrEmpty(fromPassword))
                {
                    MessageBox.Show("Chưa cấu hình địa chỉ email hoặc mật khẩu ứng dụng trong file cấu hình.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string toAdress = email;
                string subject = "Mã xác nhận tài khoản";
                string body = $"Mã xác nhận của bạn là: {verificationCode}";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(toAdress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            this.ActiveControl = panel1;
        }

        private void roundTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Texts == "Email")
            {
                textBox2.Texts = "";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Texts))
            {
                textBox2.Texts = "Email";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBoxOTP_Enter(object sender, EventArgs e)
        {
            if (textBoxOTP.Texts == "Verify OTP")
            {
                textBoxOTP.Texts = "";
                textBoxOTP.ForeColor = Color.Silver;
            }
        }

        private void textBoxOTP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOTP.Texts))
            {
                textBoxOTP.Texts = "Verify OTP";
                textBoxOTP.ForeColor = Color.Silver;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Texts == "Username")
            {
                textBox3.Texts = "";
                textBox3.ForeColor = Color.Silver;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Texts))
            {
                textBox3.Texts = "Username";
                textBox3.ForeColor = Color.Silver;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Texts == "Password")
            {
                textBox4.Texts = "";
                textBox4.ForeColor = Color.Silver;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Texts))
            {
                textBox4.Texts = "Password";
                textBox4.ForeColor = Color.Silver;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Texts == "Verify Password")
            {
                textBox5.Texts = "";
                textBox5.ForeColor = Color.Silver;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Texts))
            {
                textBox5.Texts = "Verify Password";
                textBox5.ForeColor = Color.Silver;
            }
        }

        private async void roundButton1_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            string email = textBox2.Texts.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email để gửi mã xác nhận.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                // Sử dụng phương thức từ lớp Database để kiểm tra tồn tại email
                bool emailExists = await Server.Database.KiemTraTonTaiEmail(email);
                if (emailExists)
                {
                    MessageBox.Show("Email này đã được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Nếu OK thì gửi mã xác thực
                this.verificationCode = GeneraiVerificationCode();
                GuiEmailXacThuc(email, this.verificationCode);
                MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            string email = textBox2.Texts.Trim();
            string maOTP = textBoxOTP.Texts.Trim();
            string tenDangNhap = textBox3.Texts.Trim();
            string matKhau = textBox4.Texts.Trim();
            string xacNhanMatKhau = textBox5.Texts.Trim();

            // 2. Kiểm tra dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(maOTP) ||
                string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(xacNhanMatKhau))

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
            // Kiểm tra có bị trùng tên đăng nhập hoặc email không
            bool emailExist = await Server.Database.KiemTraTonTaiEmail(email);
            if (emailExist)
            {
                MessageBox.Show("Email này đã được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool taiKhoanExist = await Server.Database.KiemTraTonTaiTaiKhoan(tenDangNhap);
            if (taiKhoanExist)
            {
                MessageBox.Show("Tên đăng nhập này đã được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                bool themThanhCong = await Server.Database.ThemTaiKhoan(tenDangNhap, matKhau, email, this.fileAnh);
                if (themThanhCong)
                {
                    MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
