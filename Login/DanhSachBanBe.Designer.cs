namespace Login
{
    partial class DanhSachBanBe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private Login.RoundTextBox txtSearch;
        private System.Windows.Forms.ListView listViewFriends;
        private System.Windows.Forms.ImageList imageListAvatars;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            txtSearch = new RoundTextBox();
            listViewFriends = new ListView();
            imageListAvatars = new ImageList(components);
            panelTop = new Panel();
            panelContent = new Panel();
            panelTop.SuspendLayout();
            panelContent.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(12, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(168, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Danh sách bạn bè";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSearch.BackColor = Color.White;
            txtSearch.BorderColor = Color.White;
            txtSearch.BorderFocusColor = Color.Cyan;
            txtSearch.BorderRadius = 15;
            txtSearch.BorderSize = 0;
            txtSearch.Location = new Point(370, 10);
            txtSearch.Multiline = false;
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(10, 7, 10, 7);
            txtSearch.PasswordChar = false;
            txtSearch.Size = new Size(230, 30);
            txtSearch.TabIndex = 1;
            txtSearch.Texts = "";
            txtSearch.UnderlinedStyle = false;
            txtSearch._TextChanged += txtSearch_TextChanged;
            // 
            // listViewFriends
            // 
            listViewFriends.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewFriends.BackColor = Color.Gray;
            listViewFriends.Font = new Font("Segoe UI", 11F);
            listViewFriends.Location = new Point(12, 12);
            listViewFriends.Name = "listViewFriends";
            listViewFriends.Size = new Size(998, 693);
            listViewFriends.SmallImageList = imageListAvatars;
            listViewFriends.TabIndex = 0;
            listViewFriends.UseCompatibleStateImageBehavior = false;
            listViewFriends.View = View.SmallIcon;
            listViewFriends.DoubleClick += listViewFriends_DoubleClick;
            // 
            // imageListAvatars
            // 
            imageListAvatars.ColorDepth = ColorDepth.Depth32Bit;
            imageListAvatars.ImageSize = new Size(50, 50);
            imageListAvatars.TransparentColor = Color.Transparent;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(15, 15, 20);
            panelTop.BackgroundImage = Properties.Resources.nền_đăng_nhập;
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(txtSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(610, 55);
            panelTop.TabIndex = 1;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(25, 25, 30);
            panelContent.BackgroundImage = Properties.Resources.nền_đăng_nhập;
            panelContent.Controls.Add(listViewFriends);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 55);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(610, 405);
            panelContent.TabIndex = 0;
            // 
            // DanhSachBanBe
            // 
            ClientSize = new Size(610, 460);
            Controls.Add(panelContent);
            Controls.Add(panelTop);
            Name = "DanhSachBanBe";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bạn bè";
            Load += DanhSachBanBe_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelContent.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
