namespace Login
{
    partial class FormChonThanhVien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnDanhSach = new RoundFlowLayoutPanel();
            btnXacNhan = new RoundButton();
            btnHuy = new RoundButton();
            roundLabel1 = new RoundLabel();
            SuspendLayout();
            // 
            // pnDanhSach
            // 
            pnDanhSach.AutoScroll = true;
            pnDanhSach.BackColor = Color.FromArgb(64, 68, 75);
            pnDanhSach.BorderColor = Color.Transparent;
            pnDanhSach.BorderRadius = 20;
            pnDanhSach.BorderThickness = 0F;
            pnDanhSach.FlowDirection = FlowDirection.TopDown;
            pnDanhSach.Location = new Point(37, 98);
            pnDanhSach.Name = "pnDanhSach";
            pnDanhSach.Size = new Size(727, 255);
            pnDanhSach.TabIndex = 6;
            pnDanhSach.WrapContents = false;
            pnDanhSach.Click += btnXacNhan_Click;
            // 
            // btnXacNhan
            // 
            btnXacNhan.BackColor = Color.MediumSlateBlue;
            btnXacNhan.BorderColor = Color.Transparent;
            btnXacNhan.BorderRadius = 20;
            btnXacNhan.BorderThickness = 0F;
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(649, 375);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(115, 50);
            btnXacNhan.TabIndex = 7;
            btnXacNhan.Text = "Xác nhận";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.MediumSlateBlue;
            btnHuy.BorderColor = Color.Transparent;
            btnHuy.BorderRadius = 20;
            btnHuy.BorderThickness = 0F;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.White;
            btnHuy.Location = new Point(514, 375);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(86, 50);
            btnHuy.TabIndex = 8;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // roundLabel1
            // 
            roundLabel1.BackColor = Color.Transparent;
            roundLabel1.BorderColor = Color.Transparent;
            roundLabel1.BorderRadius = 10;
            roundLabel1.BorderThickness = 0F;
            roundLabel1.Font = new Font("Segoe UI", 13F);
            roundLabel1.ForeColor = Color.White;
            roundLabel1.Location = new Point(216, 29);
            roundLabel1.Name = "roundLabel1";
            roundLabel1.Size = new Size(338, 38);
            roundLabel1.TabIndex = 9;
            roundLabel1.Text = "Chọn thành viên thêm vào nhóm";
            roundLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormChonThanhVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 43, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(roundLabel1);
            Controls.Add(btnHuy);
            Controls.Add(btnXacNhan);
            Controls.Add(pnDanhSach);
            Name = "FormChonThanhVien";
            Text = "FormChonThanhVien";
            Load += FormChonThanhVien_Load;
            ResumeLayout(false);
        }

        #endregion

        private RoundTextBox txtTenNhom;
        private RoundFlowLayoutPanel pnDanhSach;
        private RoundButton btnXacNhan;
        private RoundButton btnHuy;
        private RoundLabel roundLabel1;
    }
}