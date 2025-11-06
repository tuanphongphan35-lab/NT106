using System;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using BCrypt.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace Login
{
    public partial class QuenMK : Form
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ChatApp;Integrated Security=True;";
        private string? verificationCode;
        public QuenMK()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void buttonOTP_Click(object sender, EventArgs e) // Gửi OTP xác thực
        {
            string email = textBox2.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Kiem tra email có tồn tại trong hệ thống không
            try
            {
                bool emailExists = false;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            emailExists = true;
                        }
                    }
                    if (!emailExists)
                    {
                        MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                // Nếu tồn tại thì gửi mã xác thực
                this.verificationCode = GeneraiVerificationCode();
                GuiEmailXacThuc(email, this.verificationCode);
                MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuiEmailXacThuc(string email, string? verificationCode)
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
                string subject = "Mã xác nhận đăng ký tài khoản";
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

        private string? GeneraiVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 999999); // Tạo mã gồm 6 chữ số
            return code.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Xử lý đổi mật khẩu
            string email = textBox2.Text.Trim();
            string toAdress = email;
            string maOTP = textBoxOTP.Text.Trim();
            string matKhau = textBox3.Text.Trim();
            string xacNhanMatKhau = textBox4.Text.Trim();
            // 2. Kiểm tra dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(maOTP) ||
                 maOTP != this.verificationCode ||
                 matKhau != xacNhanMatKhau ||
                string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mã hóa mật khẩu
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(matKhau);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Users SET MatKhau = @MatKhau WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
                        cmd.Parameters.AddWithValue("@Email", email);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đặt lại mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Đóng form sau khi đổi mật khẩu thành công
                        }
                        else
                        {
                            MessageBox.Show("Đặt lại mật khẩu thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
