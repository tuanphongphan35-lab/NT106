using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class DangNhap : Form
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ChatApp;Integrated Security=True;";

        public DangNhap()
        {
            InitializeComponent();
            // Kết nối với cơ sở dữ liệu SQL 
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tenDangNhap = textBox1.Text; // Textbox tên đăng nhập
            string matKhau = textBox2.Text;     // Textbox mật khẩu

            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Lấy chuỗi băm (Password) từ database 
                    string query = "SELECT Password FROM Users WHERE Username = @user";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", tenDangNhap);

                        object result = cmd.ExecuteScalar();

                        if (result != null) // Nếu tìm thấy user
                        {
                            string matKhauDaLuu = result.ToString();

                            // 2. Dùng BCrypt.Verify để so sánh
                            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(matKhau, matKhauDaLuu);
                            if (isPasswordValid)
                            {
                                PhienDangNhap.TaiKhoanHienTai = tenDangNhap;
                                PhienDangNhap.IDNguoiDungHienTai = Server.Database.LayIDNguoiDung(tenDangNhap);
                                MessageBox.Show("Đăng nhập thành công!");
                                // Mở form chính...
                                ThongTinNguoiDung mainForm = new ThongTinNguoiDung();
                                mainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                            }
                        }
                        else
                        {
                            // Không tìm thấy user
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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