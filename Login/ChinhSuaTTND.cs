using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class ChinhSuaTTND : Form
    {
        private byte[]? fileAnh = null;
        public ChinhSuaTTND()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void roundPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Lưu thông tin người dùng sau khi chỉnh sửa
            int  id = PhienDangNhap.IDNguoiDungHienTai; // Change type to int
            string tenNguoiDung = textBox1.Text.Trim();
            DateTime ngaySinh = dateTimePicker1.Value;
            string gioiTinh = comboBox1.SelectedItem?.ToString() ?? "";
            // Thực hiện lưu thông tin vào cơ sở dữ liệu hoặc xử lý theo yêu cầu
            if (string.IsNullOrEmpty(tenNguoiDung) || string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool luuThanhCong = await Server.Database.LuuThongTinNguoiDung(id, tenNguoiDung, ngaySinh, gioiTinh, this.fileAnh);
            if (luuThanhCong)
            {
                MessageBox.Show("Lưu thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi lưu thành công
            }
            else
            {
                MessageBox.Show("Lưu thông tin thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    circularPictureBox1.Image = new Bitmap(imagePath);

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý khi chọn giới tính

        }
    }
}
