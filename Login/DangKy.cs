using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Login
{
    public partial class DangKy : Form
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ChatApp;Integrated Security=True;";
        private string? verificationCode;
        private string? UsersID;
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private byte[]? fileAnh = null;
        public DangKy()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox1.Text = "ID tự động";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();
            string maOTP = textBoxOTP.Text.Trim();
            string tenDangNhap = textBox3.Text.Trim();
            string matKhau = textBox4.Text.Trim();
            string xacNhanMatKhau = textBox5.Text.Trim();

            // 2. Kiểm tra dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(maOTP) ||
                 maOTP != this.verificationCode ||
                string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(xacNhanMatKhau))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string matKhauDaBam = BCrypt.Net.BCrypt.HashPassword(matKhau);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (Username, Email, Password, Avatar) VALUES(@user, @email, @hash, @avatar)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", tenDangNhap);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@hash", matKhauDaBam);

                        if (this.fileAnh != null)
                        {
                            cmd.Parameters.AddWithValue("@avatar", this.fileAnh);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@avatar", DBNull.Value);

                        }
                        await cmd.ExecuteNonQueryAsync();
                        MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Lỗi 2627 hoặc 2601 là lỗi trùng lặp (UNIQUE constraint)
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    MessageBox.Show("Tên đăng nhập hoặc Email này đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Các lỗi SQL khác (mất kết nối, v.v.)
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string imagePath = openFileDialog.FileName;

                    // Hiển thị ảnh lên PictureBox
                    pictureBox1.Image = new Bitmap(imagePath);

                    // Đọc file ảnh thành mảng byte[] để chuẩn bị lưu vào CSDL
                    this.fileAnh = File.ReadAllBytes(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đọc file ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.fileAnh = null;
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email để gửi mã xác nhận.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                bool emailExists = false;
                using SqlConnection conn = new SqlConnection(connectionString);
                {
                    await conn.OpenAsync();
                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    using SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    if (count > 0)
                    {
                        emailExists = true;
                    }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string GeneraiVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public void GuiEmailXacThuc(string email, string verificationCode)
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
    }
}
