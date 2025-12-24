using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Login
{
    public partial class FormChonThanhVien : Form
    {
        // Form này sẽ trả về danh sách người được chọn
        public List<string> SelectedUsers { get; private set; } = new List<string>();

        public FormChonThanhVien(List<string> danhSachUngVien)
        {
            InitializeComponent();
            LoadDanhSach(danhSachUngVien);
        }

        private void LoadDanhSach(List<string> list)
        {
            pnDanhSach.Controls.Clear();
            pnDanhSach.AutoScroll = true;

            if (list.Count == 0)
            {
                Label lbl = new Label { Text = "Không còn ai để mời!", AutoSize = true, ForeColor = Color.White, Padding = new Padding(10) };
                pnDanhSach.Controls.Add(lbl);
                btnXacNhan.Enabled = false;
                return;
            }

            foreach (string user in list)
            {
                CheckBox chk = new CheckBox();
                chk.Text = "  " + user;
                chk.ForeColor = Color.White;
                chk.Font = new Font("Segoe UI", 11);
                chk.AutoSize = false;
                chk.Size = new Size(pnDanhSach.Width - 25, 40);
                chk.Cursor = Cursors.Hand;
                // Tô màu khi hover
                chk.MouseEnter += (s, e) => chk.BackColor = Color.FromArgb(60, 60, 70);
                chk.MouseLeave += (s, e) => chk.BackColor = Color.Transparent;

                pnDanhSach.Controls.Add(chk);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            foreach (Control c in pnDanhSach.Controls)
            {
                if (c is CheckBox chk && chk.Checked)
                {
                    SelectedUsers.Add(chk.Text.Trim());
                }
            }

            if (SelectedUsers.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Chưa chọn ai cả!");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Setup màu nền tối cho Form khi Load
        private void FormChonThanhVien_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(32, 32, 32);
            pnDanhSach.BackColor = Color.FromArgb(40, 40, 40);
        }
    }
}