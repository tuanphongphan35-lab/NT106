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
    public partial class ChatForm : Form
    {
        private readonly string _loggedInUserID;

        // 2. Chỉnh sửa Constructor để nhận ID người dùng
        public ChatForm(string currentUserID)
        {
            InitializeComponent();

            // Gán ID được truyền vào cho biến nội bộ
            _loggedInUserID = currentUserID;
        }
        public ChatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            DangNhap loginform = new DangNhap();
            loginform.Show();
        }

        private void circularPictureBox1_Click(object sender, EventArgs e)
        {

            string idToDisplay = _loggedInUserID;

            // Kiểm tra an toàn (chỉ nên xảy ra nếu có lỗi hệ thống)
            if (idToDisplay == null)
            {
                MessageBox.Show("Lỗi Session. Vui lòng đăng nhập lại.");
                return;
            }

            // Truyền ID này vào Form Profile
            ThongTinNguoiDung profileForm = new ThongTinNguoiDung(idToDisplay);
            profileForm.ShowDialog();
        }

        private void roundButton4_Click(object sender, EventArgs e)
        {
            roundButton4.Enabled = false;
            ThongBaoTinNhan thongBaoTinNhan = new ThongBaoTinNhan();
            thongBaoTinNhan.Show();
            this.Hide();
        }

        private void roundButton5_Click(object sender, EventArgs e)
        {
            roundButton5.Enabled = false;
        }

        private void roundButton7_Click(object sender, EventArgs e)
        {
            roundButton7.Enabled = false;
        }

        private void roundButton6_Click(object sender, EventArgs e)
        {
            roundButton6.Enabled = false;
            TimKiemNguoiDung timKiemNguoiDung = new TimKiemNguoiDung();
            timKiemNguoiDung.Show();
        }
    }
}
