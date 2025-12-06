using Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class DanhSachBanBe : Form
    {
        private List<Database.UserInfo> allFriends = new List<Database.UserInfo>();

        public DanhSachBanBe()
        {
            InitializeComponent();
        }

        private void DanhSachBanBe_Load(object sender, EventArgs e)
        {
            txtSearch.Texts = "Tìm bạn...";
            txtSearch.ForeColor = Color.Gray;

            txtSearch.Enter += (s, ev) =>
            {
                if (txtSearch.Texts == "Tìm bạn...")
                {
                    txtSearch.Texts = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (s, ev) =>
            {
                if (txtSearch.Texts.Trim() == "")
                {
                    txtSearch.Texts = "Tìm bạn...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            _ = LoadFriends();
        }


        private async Task LoadFriends()
        {
            listViewFriends.Items.Clear();
            imageListAvatars.Images.Clear();
            allFriends.Clear();

            try
            {
                var db = FirestoreHelper.GetDatabase();
                var userRef = db.Collection("Users").Document(PhienDangNhap.IDNguoiDungHienTai);
                var snapshot = await userRef.Collection("friends").GetSnapshotAsync();

                int index = 0;
                foreach (var doc in snapshot.Documents)
                {
                    string friendId = doc.Id;
                    var info = await Database.LayThongTinNguoiDung(friendId);

                    if (info != null)
                    {
                        allFriends.Add(info);

                        // Avatar
                        Image avatar = Properties.Resources.user_default;

                        if (info.Avatar != null && info.Avatar.Length > 0)
                            using (var ms = new MemoryStream(info.Avatar))
                                avatar = Image.FromStream(ms);

                        // Tạo avatar tròn
                        Image rounded = MakeRoundAvatar(avatar);
                        imageListAvatars.Images.Add(rounded);

                        var item = new ListViewItem(info.TenNguoiDung ?? "Người dùng");
                        item.Tag = friendId;
                        item.ImageIndex = index++;
                        listViewFriends.Items.Add(item);
                    }
                }

                if (listViewFriends.Items.Count == 0)
                    listViewFriends.Items.Add("(Chưa có bạn bè)").ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private Image MakeRoundAvatar(Image avatar)
        {
            int size = 50;
            Bitmap bmp = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath p = new GraphicsPath())
                {
                    p.AddEllipse(0, 0, size, size);
                    g.Clip = new Region(p);
                    g.DrawImage(avatar, 0, 0, size, size);
                }

                // Viền online
                g.DrawEllipse(new Pen(Color.LimeGreen, 3), 2, 2, size - 4, size - 4);
            }

            return bmp;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Texts.Trim().ToLower();

            listViewFriends.Items.Clear();
            imageListAvatars.Images.Clear();

            int index = 0;
            foreach (var f in allFriends)
            {
                if (string.IsNullOrEmpty(keyword) || (f.TenNguoiDung ?? "").ToLower().Contains(keyword))
                {
                    Image avatar = Properties.Resources.user_default;

                    if (f.Avatar != null && f.Avatar.Length > 0)
                        using (var ms = new MemoryStream(f.Avatar))
                            avatar = Image.FromStream(ms);

                    Image rounded = MakeRoundAvatar(avatar);

                    imageListAvatars.Images.Add(rounded);

                    var item = new ListViewItem(f.TenNguoiDung ?? "Người dùng");
                    item.Tag = f.Id;
                    item.ImageIndex = index++;
                    listViewFriends.Items.Add(item);
                }
            }
        }

        private void listViewFriends_DoubleClick(object sender, EventArgs e)
        {
            if (listViewFriends.SelectedItems.Count > 0 && listViewFriends.SelectedItems[0].Tag != null)
            {
                string friendId = listViewFriends.SelectedItems[0].Tag.ToString();
                ChiTietBanBe profile = new ChiTietBanBe(friendId);
                profile.ShowDialog();
            }
        }
    }
}
