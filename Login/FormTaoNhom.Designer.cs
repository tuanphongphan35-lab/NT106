namespace Login
{
    partial class FormTaoNhom
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
            txtTenNhom = new RoundTextBox();
            btnTaoNhom = new RoundButton();
            btnHuy = new RoundButton();
            clbDanhSachBanBe = new RoundFlowLayoutPanel();
            SuspendLayout();
            // 
            // txtTenNhom
            // 
            txtTenNhom.BackColor = Color.FromArgb(64, 68, 75);
            txtTenNhom.BorderColor = Color.MediumSlateBlue;
            txtTenNhom.BorderFocusColor = Color.HotPink;
            txtTenNhom.BorderRadius = 15;
            txtTenNhom.BorderSize = 0;
            txtTenNhom.ForeColor = Color.Gray;
            txtTenNhom.Location = new Point(38, 24);
            txtTenNhom.Multiline = false;
            txtTenNhom.Name = "txtTenNhom";
            txtTenNhom.Padding = new Padding(10, 7, 10, 7);
            txtTenNhom.PasswordChar = false;
            txtTenNhom.Size = new Size(217, 35);
            txtTenNhom.TabIndex = 0;
            txtTenNhom.Texts = "Nhập tên nhóm...";
            txtTenNhom.UnderlinedStyle = false;
            txtTenNhom.Enter += txtTenNhom_Enter;
            txtTenNhom.Leave += txtTenNhom_Leave;
            // 
            // btnTaoNhom
            // 
            btnTaoNhom.BackColor = Color.MediumSlateBlue;
            btnTaoNhom.BorderColor = Color.Transparent;
            btnTaoNhom.BorderRadius = 20;
            btnTaoNhom.BorderThickness = 0F;
            btnTaoNhom.FlatAppearance.BorderSize = 0;
            btnTaoNhom.FlatStyle = FlatStyle.Flat;
            btnTaoNhom.ForeColor = Color.White;
            btnTaoNhom.Location = new Point(650, 376);
            btnTaoNhom.Name = "btnTaoNhom";
            btnTaoNhom.Size = new Size(115, 50);
            btnTaoNhom.TabIndex = 2;
            btnTaoNhom.Text = "Xác nhận";
            btnTaoNhom.UseVisualStyleBackColor = false;
            btnTaoNhom.Click += btnTaoNhom_Click;
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
            btnHuy.Location = new Point(522, 376);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(86, 50);
            btnHuy.TabIndex = 3;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // clbDanhSachBanBe
            // 
            clbDanhSachBanBe.AutoScroll = true;
            clbDanhSachBanBe.BackColor = Color.FromArgb(64, 68, 75);
            clbDanhSachBanBe.BorderColor = Color.Transparent;
            clbDanhSachBanBe.BorderRadius = 20;
            clbDanhSachBanBe.BorderThickness = 0F;
            clbDanhSachBanBe.FlowDirection = FlowDirection.TopDown;
            clbDanhSachBanBe.Location = new Point(38, 91);
            clbDanhSachBanBe.Name = "clbDanhSachBanBe";
            clbDanhSachBanBe.Size = new Size(727, 255);
            clbDanhSachBanBe.TabIndex = 5;
            clbDanhSachBanBe.WrapContents = false;
            // 
            // FormTaoNhom
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 43, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(clbDanhSachBanBe);
            Controls.Add(btnHuy);
            Controls.Add(btnTaoNhom);
            Controls.Add(txtTenNhom);
            Name = "FormTaoNhom";
            Text = "FormTaoNhom";
            Load += FormTaoNhom_Load;
            ResumeLayout(false);
        }

        #endregion

        private RoundTextBox txtTenNhom;
        private RoundButton btnTaoNhom;
        private RoundButton btnHuy;
        private RoundTextBox roundTextBox1;
        private RoundFlowLayoutPanel clbDanhSachBanBe;
    }
}