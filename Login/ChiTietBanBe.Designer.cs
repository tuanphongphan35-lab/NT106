namespace Login
{
    partial class ChiTietBanBe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelMain = new Panel();
            btnClose = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lblNgaySinh = new Label();
            lblGioiTinh = new Label();
            lblEmail = new Label();
            lblUsername = new Label();
            lblTen = new Label();
            picAvatar = new PictureBox();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.Transparent;
            panelMain.BackgroundImage = Properties.Resources.nền_đăng_nhập;
            panelMain.Controls.Add(btnClose);
            panelMain.Controls.Add(label4);
            panelMain.Controls.Add(label3);
            panelMain.Controls.Add(label2);
            panelMain.Controls.Add(label1);
            panelMain.Controls.Add(lblNgaySinh);
            panelMain.Controls.Add(lblGioiTinh);
            panelMain.Controls.Add(lblEmail);
            panelMain.Controls.Add(lblUsername);
            panelMain.Controls.Add(lblTen);
            panelMain.Controls.Add(picAvatar);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(4, 3, 4, 3);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(490, 600);
            panelMain.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Transparent;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(432, 12);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(47, 46);
            btnClose.TabIndex = 10;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label4.ForeColor = Color.LightGray;
            label4.Location = new Point(70, 473);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 9;
            label4.Text = "Ngày sinh:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label3.ForeColor = Color.LightGray;
            label3.Location = new Point(70, 415);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 8;
            label3.Text = "Giới tính:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label2.ForeColor = Color.LightGray;
            label2.Location = new Point(70, 358);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 7;
            label2.Text = "Email:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label1.ForeColor = Color.LightGray;
            label1.Location = new Point(70, 300);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 6;
            label1.Text = "Tên hiển thị:";
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Font = new Font("Segoe UI", 11F);
            lblNgaySinh.ForeColor = Color.White;
            lblNgaySinh.Location = new Point(210, 473);
            lblNgaySinh.Margin = new Padding(4, 0, 4, 0);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(0, 20);
            lblNgaySinh.TabIndex = 5;
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Font = new Font("Segoe UI", 11F);
            lblGioiTinh.ForeColor = Color.White;
            lblGioiTinh.Location = new Point(210, 415);
            lblGioiTinh.Margin = new Padding(4, 0, 4, 0);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(0, 20);
            lblGioiTinh.TabIndex = 4;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 11F);
            lblEmail.ForeColor = Color.White;
            lblEmail.Location = new Point(210, 358);
            lblEmail.Margin = new Padding(4, 0, 4, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(0, 20);
            lblEmail.TabIndex = 3;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 11F);
            lblUsername.ForeColor = Color.White;
            lblUsername.Location = new Point(210, 300);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(0, 20);
            lblUsername.TabIndex = 2;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTen.ForeColor = Color.Cyan;
            lblTen.Location = new Point(117, 242);
            lblTen.Margin = new Padding(4, 0, 4, 0);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(196, 32);
            lblTen.TabIndex = 1;
            lblTen.Text = "Tên người dùng";
            lblTen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = Color.Transparent;
            picAvatar.Location = new Point(152, 46);
            picAvatar.Margin = new Padding(4, 3, 4, 3);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(187, 185);
            picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            // 
            // ChiTietBanBe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 25, 40);
            ClientSize = new Size(490, 600);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "ChiTietBanBe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thông tin bạn bè";
            Load += ChiTietBanBe_Load;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
        }

        private Panel panelMain;
        PictureBox picAvatar;
        Label lblTen;
        Label lblUsername;
        Label lblEmail;
        Label lblGioiTinh;
        Label lblNgaySinh;
        Label label1;
        Label label2;
        Label label3;
        Label label4;
        Button btnClose;
    }
}