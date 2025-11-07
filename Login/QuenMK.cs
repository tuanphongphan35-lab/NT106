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
                // Sử dụng phương thức từ lớp Database để kiểm tra tồn tại email
                bool emailExists = Server.Database.KiemTraTonTaiEmail(email);
                if (!emailExists)
                {
                    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Nếu tồn tại thì gửi mã xác thực
                this.verificationCode = DangKy.GeneraiVerificationCode();
                DangKy.GuiEmailXacThuc(email, this.verificationCode);
                MessageBox.Show("Mã xác nhận đã được gửi đến email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            // Mã hóa mật khẩu và cập nhật vào cơ sở dữ liệu
            string matKhauDaBam = BCrypt.Net.BCrypt.HashPassword(matKhau);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Users SET Password = @pass WHERE Email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@pass", matKhauDaBam);
                        command.Parameters.AddWithValue("@Email", email);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Mật khẩu đã được đặt lại thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Đóng form sau khi đổi mật khẩu thành công
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy email trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
