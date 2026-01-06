using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server;
using static Server.Database;

namespace Login
{
    public partial class ThongTinNguoiDung : Form
    {
        private string _targetID; // ID người cần xem (Có thể là mình hoặc người khác)
        private static readonly HttpClient client = new HttpClient();

        // --- CONSTRUCTOR DUY NHẤT ---
        // Nhận vào ID người cần xem.
        // - Nếu xem chính mình: Truyền PhienDangNhap.IDNguoiDungHienTai
        // - Nếu xem người khác: Truyền ID của người đó
        public ThongTinNguoiDung(string userIdCanXem)
        {
            InitializeComponent();
            _targetID = userIdCanXem;
        }

        private async void ThongTinNguoiDung_Load(object sender, EventArgs e)
        {
            BoTronGoc();

            if (_targetID == PhienDangNhap.IDNguoiDungHienTai)
            {
                // TRƯỜNG HỢP 1: XEM CHÍNH MÌNH
                if (button1 != null) button1.Visible = true; // Hiện nút sửa
            }
            else
            {
                // TRƯỜNG HỢP 2: XEM NGƯỜI KHÁC
                if (button1 != null) button1.Visible = false; // Ẩn nút sửa
            }

            // --- 2. TẢI DỮ LIỆU ---
            await TaiDuLieuTuServer();
        }

        private async Task TaiDuLieuTuServer()
        {
            try
            {
                UserInfo info = await Server.Database.LayThongTinNguoiDung(_targetID);

                if (info != null)
                {
                    textBox1.Texts = info.TenNguoiDung;
                    textBox4.Texts = info.Email;

                    if (info.NgaySinh.Year > 1900)
                        textBox2.Texts = info.NgaySinh.ToString("dd/MM/yyyy");
                    else
                        textBox2.Texts = "Chưa cập nhật";

                    textBox3.Texts = !string.IsNullOrEmpty(info.GioiTinh) ? info.GioiTinh : "Chưa cập nhật";

                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BoTronGoc()
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 20;
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(this.Width - (radius * 2), 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(this.Width - (radius * 2), this.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
            path.AddArc(0, this.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            this.Region = new System.Drawing.Region(path);
        }

        private async Task LoadImageFromUrlAsync(string url)
        {
            try
            {
                byte[] imageBytes = await client.GetByteArrayAsync(url);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    circularPictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch
            {
                circularPictureBox1.Image = Properties.Resources.user_default;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Nút sửa chỉ hoạt động khi xem chính mình
            if (_targetID == PhienDangNhap.IDNguoiDungHienTai)
            {
                ChinhSuaTTND chinhSuaTTND = new ChinhSuaTTND();
                chinhSuaTTND.Show();
                this.Hide();
            }
        }

        // Logic kéo thả form
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void ThongTinNguoiDungForm_MouseDown(object sender, MouseEventArgs e) { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; }
        private void ThongTinNguoiDungForm_MouseMove(object sender, MouseEventArgs e) { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(dif)); } }
        private void ThongTinNguoiDungForm_MouseUp(object sender, MouseEventArgs e) { dragging = false; }
    }
}