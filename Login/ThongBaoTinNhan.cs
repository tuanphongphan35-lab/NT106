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
    public partial class ThongBaoTinNhan : Form
    {
        public ThongBaoTinNhan()
        {
            InitializeComponent();
        }

        private void ThongBaoTinNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Nếu người dùng bấm nút X (UserClosing)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Hủy lệnh hủy diệt Form
                this.Hide();     // Chỉ ẩn nó đi thôi
            }
        }

        // 2. Hàm public để bên ngoài (ChatForm) nhét thông báo vào
        public void ThemThongBaoMoi(UserControl item)
        {
            flowLayoutPanel1.Controls.Add(item);
            flowLayoutPanel1.Controls.SetChildIndex(item, 0); // Đưa cái mới nhất lên đầu
        }
        private void ThongBaoTinNhan_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
