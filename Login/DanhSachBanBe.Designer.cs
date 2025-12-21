namespace Login
{
    partial class DanhSachBanBe
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
            flpDanhSach = new RoundFlowLayoutPanel();
            txtTimKiem = new RoundTextBox();
            btnTim = new RoundButton();
            roundButton2 = new RoundButton();
            label1 = new Label();
            SuspendLayout();
            // 
            // flpDanhSach
            // 
            flpDanhSach.BackColor = Color.FromArgb(40, 43, 51);
            flpDanhSach.BorderColor = Color.Transparent;
            flpDanhSach.BorderRadius = 20;
            flpDanhSach.BorderThickness = 0F;
            flpDanhSach.Location = new Point(32, 142);
            flpDanhSach.Name = "flpDanhSach";
            flpDanhSach.Size = new Size(443, 421);
            flpDanhSach.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            txtTimKiem.BackColor = Color.FromArgb(40, 43, 51);
            txtTimKiem.BorderColor = Color.MediumSlateBlue;
            txtTimKiem.BorderFocusColor = Color.HotPink;
            txtTimKiem.BorderRadius = 15;
            txtTimKiem.BorderSize = 0;
            txtTimKiem.Location = new Point(32, 84);
            txtTimKiem.Multiline = false;
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Padding = new Padding(10, 7, 10, 7);
            txtTimKiem.PasswordChar = false;
            txtTimKiem.Size = new Size(325, 35);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.Texts = "";
            txtTimKiem.UnderlinedStyle = false;
            // 
            // btnTim
            // 
            btnTim.BackColor = Color.MediumSlateBlue;
            btnTim.BorderColor = Color.Transparent;
            btnTim.BorderRadius = 20;
            btnTim.BorderThickness = 0F;
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.FlatStyle = FlatStyle.Flat;
            btnTim.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTim.ForeColor = Color.White;
            btnTim.Location = new Point(363, 86);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(112, 33);
            btnTim.TabIndex = 2;
            btnTim.Text = "Tìm";
            btnTim.UseVisualStyleBackColor = false;
            // 
            // roundButton2
            // 
            roundButton2.BackColor = Color.MediumSlateBlue;
            roundButton2.BorderColor = Color.Transparent;
            roundButton2.BorderRadius = 20;
            roundButton2.BorderThickness = 0F;
            roundButton2.FlatAppearance.BorderSize = 0;
            roundButton2.FlatStyle = FlatStyle.Flat;
            roundButton2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton2.ForeColor = Color.White;
            roundButton2.Location = new Point(156, 545);
            roundButton2.Name = "roundButton2";
            roundButton2.Size = new Size(188, 50);
            roundButton2.TabIndex = 0;
            roundButton2.Text = "Quay lại";
            roundButton2.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(64, 64, 64);
            label1.Location = new Point(163, 30);
            label1.Name = "label1";
            label1.Size = new Size(181, 28);
            label1.TabIndex = 3;
            label1.Text = "Danh sách Bạn bè";
            // 
            // DanhSachBanBe
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            BackgroundImage = Properties.Resources.Colored_Sky;
            ClientSize = new Size(506, 620);
            Controls.Add(label1);
            Controls.Add(roundButton2);
            Controls.Add(btnTim);
            Controls.Add(txtTimKiem);
            Controls.Add(flpDanhSach);
            Margin = new Padding(5, 4, 5, 4);
            Name = "DanhSachBanBe";
            Text = "Danh Sách Bạn Bè";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private RoundFlowLayoutPanel flpDanhSach;
        private RoundTextBox txtTimKiem;
        private RoundButton btnTim;
        private RoundButton roundButton2;
        private Label label1;
    }
}
