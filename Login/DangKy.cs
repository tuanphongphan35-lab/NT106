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
        private string verificationCode;
        private string UsersID;
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private byte[] fileAnh = null;
        public DangKy()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox1.Text = "ID tự động";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();
            string tenDangNhap = textBox3.Text.Trim();
            string matKhau = textBox4.Text.Trim();
            string xacNhanMatKhau = textBox5.Text.Trim();

            // 2. Kiểm tra dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(email) ||
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

                        // Xóa dữ liệu trong các TextBox sau khi đăng ký thành công
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        pictureBox1.Image = null;   

                        this.fileAnh = null; // Reset ảnh sau khi đăng ký thành công
                    }
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
    }
}
