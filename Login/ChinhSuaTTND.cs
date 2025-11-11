using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.IO; // Cần cho File.ReadAllBytes
using System.Windows.Forms;
using Server; // Cần để gọi lớp FirestoreDatabase

namespace Login
{
    public partial class ChinhSuaTTND : Form
    {
        private byte[]? fileAnh = null;

        public ChinhSuaTTND()
        {
            InitializeComponent();
        }

        // ... (Các hàm không liên quan đến database) ...

        private async void button1_Click(object sender, EventArgs e)
        {
            // --- THAY ĐỔI QUAN TRỌNG: ID người dùng là STRING ---
            // 1. Lấy ID Document (string) từ PhienDangNhap
            // Bạn phải đảm bảo PhienDangNhap.IDNguoiDungHienTai đã được đổi kiểu sang string.
            string id = PhienDangNhap.IDNguoiDungHienTai;

            string tenNguoiDung = textBox1.Text.Trim();
            DateTime ngaySinh = dateTimePicker1.Value;
            string gioiTinh = comboBox1.SelectedItem?.ToString() ?? "";

            if (string.IsNullOrEmpty(tenNguoiDung) || string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Gọi hàm Lưu thông tin đã được chuyển đổi (sẽ Upload ảnh lên Storage nếu có)
            // LƯU Ý: Đổi tên lớp gọi từ Server.Database sang Server.FirestoreDatabase
            bool luuThanhCong = await Server.Database.LuuThongTinNguoiDung(id, tenNguoiDung, ngaySinh, gioiTinh, this.fileAnh);

            if (luuThanhCong)
            {
                MessageBox.Show("Lưu thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi lưu thành công
            }
            else
            {
                MessageBox.Show("Lưu thông tin thất bại. Vui lòng kiểm tra cấu hình Firebase.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // Hiển thị ảnh lên PictureBox (circularPictureBox1)
                    circularPictureBox1.Image = new Bitmap(imagePath);

                    // Đọc file ảnh thành mảng byte[] để chuẩn bị cho việc Upload lên Firebase Storage
                    this.fileAnh = File.ReadAllBytes(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đọc file ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.fileAnh = null;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void roundPanel1_Paint(object sender, PaintEventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }
    }
}