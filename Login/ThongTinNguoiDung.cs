using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http; // Cần thiết để tải ảnh từ URL
using System.Threading.Tasks;
using System.Windows.Forms;
using Server;
using static Server.Database; // Lớp chứa FirestoreDatabase
// Loại bỏ: using Microsoft.Data.SqlClient;
// Loại bỏ: using Microsoft.VisualBasic.ApplicationServices;

namespace Login
{
    public partial class ThongTinNguoiDung : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public ThongTinNguoiDung()
        {
            InitializeComponent();
        }

        private async void ThongTinNguoiDung_Load(object sender, EventArgs e)
        {
            // --- GIỮ NGUYÊN LOGIC BO GÓC FORM ---
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 20; // Độ bo góc
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(this.Width - (radius * 2), 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(this.Width - (radius * 2), this.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
            path.AddArc(0, this.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
            path.CloseFigure();

            // Áp dụng vùng bo tròn cho Form
            this.Region = new System.Drawing.Region(path);

            // --- BẮT ĐẦU LOGIC TẢI DỮ LIỆU FIRESTORE ---
            try
            {
                // 1. Lấy username và ID (là STRING trong Firestore)
                string username = PhienDangNhap.TaiKhoanHienTai;
                // IDNguoiDungHienTai giờ là string ID Document của Firestore
                string userId = PhienDangNhap.IDNguoiDungHienTai;

                // 2. Tải Avatar qua URL
                if (!string.IsNullOrEmpty(username))
                {
                    // THAY ĐỔI: Gọi lớp FirestoreDatabase và hàm LayAvatarUrl (trả về string URL)
                    string avatarUrl = await Server.Database.LayAvatarUrl(username);

                    if (!string.IsNullOrEmpty(avatarUrl))
                    {
                        // Hàm tải ảnh từ URL và chuyển thành Image
                        await LoadImageFromUrlAsync(avatarUrl);
                        }
                    }
                    else
                    {
                        // Hình mặc định
                        circularPictureBox1.Image = Properties.Resources.user_default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải avatar: " + ex.Message);
            }
            // Lấy từ database TaiKhoan_Server và hiển thị các thông tin người dùng khác tương tự

                // 3. Tải Thông tin Người dùng
                // THAY ĐỔI: Gọi lớp FirestoreDatabase và dùng STRING ID
                UserInfo info = await Server.Database.LayThongTinNguoiDung(userId);

                // 4. Hiển thị thông tin lên Form
            if (info != null)
            {
                    // LƯU Ý: UserInfo.Avatar giờ là null (nếu bạn giữ lại byte[] cũ)

                textBox1.Text = info.TenNguoiDung;
                textBox4.Text = info.Email;

                    if (info.NgaySinh.Year > 1900)
                {
                    // Định dạng ngày/tháng/năm
                    textBox2.Text = info.NgaySinh.ToString("dd/MM/yyyy");

                    // Bạn có thể dùng định dạng tiếng Việt: "dd tháng MM năm yyyy"
                    // txtNgaySinh.Text = user.NgaySinh.ToString("dd \\t\\h\\á\\n\\g MM yyyy");
                }
                else
                {
                    textBox2.Text = "Chưa cập nhật";
                }

                if (!string.IsNullOrEmpty(info.GioiTinh))
                {
                    textBox3.Text = info.GioiTinh;
                }
                else
                {
                    textBox3.Text = "Chưa cập nhật";
                }
            }
            else
            {
                    MessageBox.Show("Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu người dùng: " + ex.Message, "Lỗi Firebase/Network");
            }
        }

        // --- HÀM MỚI: TẢI ẢNH TỪ URL ---
        private async Task LoadImageFromUrlAsync(string url)
        {
            try
            {
                // Tải dữ liệu ảnh dưới dạng byte array từ URL
                byte[] imageBytes = await client.GetByteArrayAsync(url);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    circularPictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tải ảnh từ URL: " + ex.Message);
                circularPictureBox1.Image = Properties.Resources.user_default;
            }
        }

        // --- GIỮ NGUYÊN CÁC HÀM XỬ LÝ SỰ KIỆN KHÁC ---
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        // ... (Các hàm MouseDown, MouseMove, MouseUp, Click) ...

        private void ThongTinNguoiDungForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void ThongTinNguoiDungForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void ThongTinNguoiDungForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void circularPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void roundPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Mở form chỉnh sửa thông tin (ChinhSuaTTND)
            ChinhSuaTTND chinhSuaTTND = new ChinhSuaTTND();
            chinhSuaTTND.Show();
            this.Hide();
        }

        // ... (Các hàm không sử dụng) ...

        private void circularPictureBox1_Click(object sender, EventArgs e) { }
        private void roundPanel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
