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

        private void roundTextBox1__TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
} 
