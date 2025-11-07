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
                // Kiểm tra đăng nhập trong cơ sở dữ liệu
                bool ketQuaDangNhap = Server.Database.KiemTraDangNhap(tenDangNhap, matKhau);
                if (ketQuaDangNhap)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                    // Mở form chính sau khi đăng nhập thành công
                    //Form1 mainForm = new Form1(tenDangNhap);
                    //mainForm.Show();
                    this.Hide(); // Ẩn form đăng nhập
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi");
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