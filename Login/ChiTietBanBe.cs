using Server;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class ChiTietBanBe : Form
    {
        private string FriendId;

        public ChiTietBanBe(string friendId)
        {
            InitializeComponent();
            this.FriendId = friendId;
        }

        private async void ChiTietBanBe_Load(object sender, EventArgs e)
        {
            await LoadFriendInfo();
        }

        private async Task LoadFriendInfo()
        {
            try
            {
                var info = await Database.LayThongTinNguoiDung(FriendId);
                if (info == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng.");
                    this.Close();
                    return;
                }

                // Tên hiển thị
                lblTen.Text = info.TenNguoiDung ?? "Chưa đặt tên";
                lblTen.Location = new Point((panelMain.Width - lblTen.Width) / 2, 220);

                // Thông tin chi tiết
                lblUsername.Text = info.TenNguoiDung ?? "Không có";
                lblEmail.Text = info.Email ?? "Ẩn email";
                lblGioiTinh.Text = info.GioiTinh ?? "Chưa cập nhật";
                lblNgaySinh.Text = info.NgaySinh != default(DateTime)
                    ? info.NgaySinh.ToString("dd/MM/yyyy")
                    : "Chưa cập nhật";

                // Ảnh đại diện
                Image avatar = Properties.Resources.user_default;
                if (info.Avatar != null && info.Avatar.Length > 0)
                {
                    using (var ms = new MemoryStream(info.Avatar))
                    {
                        avatar = Image.FromStream(ms);
                    }
                }

                // Làm tròn ảnh
                Bitmap rounded = new Bitmap(160, 160);
                using (Graphics g = Graphics.FromImage(rounded))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddEllipse(0, 0, 160, 160);
                        g.Clip = new Region(path);
                        g.DrawImage(avatar, 0, 0, 160, 160);
                    }
                    // Viền online
                    using (Pen pen = new Pen(Color.LimeGreen, 5))
                        g.DrawEllipse(pen, 2, 2, 156, 156);
                }

                picAvatar.Image = rounded;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}