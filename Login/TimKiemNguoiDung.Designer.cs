namespace Login
{
    partial class TimKiemNguoiDung
    {
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
            txtTimKiem = new RoundTextBox();
            roundButton1 = new RoundButton();
            btnXem = new RoundButton();
            btnSua = new RoundButton();
            btnXoa = new RoundButton();
            label1 = new Label();
            flpDanhSach = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // txtTimKiem
            // 
            txtTimKiem.BackColor = Color.FromArgb(40, 43, 51);
            txtTimKiem.BorderColor = Color.MediumSlateBlue;
            txtTimKiem.BorderFocusColor = Color.HotPink;
            txtTimKiem.BorderRadius = 15;
            txtTimKiem.BorderSize = 0;
            txtTimKiem.ForeColor = SystemColors.ButtonHighlight;
            txtTimKiem.Location = new Point(29, 86);
            txtTimKiem.Margin = new Padding(4);
            txtTimKiem.Multiline = false;
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Padding = new Padding(12, 9, 12, 9);
            txtTimKiem.PasswordChar = false;
            txtTimKiem.Size = new Size(408, 44);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.Texts = "";
            txtTimKiem.UnderlinedStyle = false;
            // 
            // roundButton1
            // 
            roundButton1.BackColor = Color.MediumSlateBlue;
            roundButton1.BackgroundImage = Properties.Resources.Colored_Sky;
            roundButton1.BorderColor = Color.Transparent;
            roundButton1.BorderRadius = 20;
            roundButton1.BorderThickness = 0F;
            roundButton1.FlatAppearance.BorderSize = 0;
            roundButton1.FlatStyle = FlatStyle.Flat;
            roundButton1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton1.ForeColor = Color.White;
            roundButton1.Location = new Point(444, 86);
            roundButton1.Margin = new Padding(4);
            roundButton1.Name = "roundButton1";
            roundButton1.Size = new Size(149, 44);
            roundButton1.TabIndex = 2;
            roundButton1.Text = "Tìm kiếm";
            roundButton1.UseVisualStyleBackColor = false;
            roundButton1.Click += roundButton1_Click;
            // 
            // btnXem
            // 
            btnXem.BackColor = Color.MediumSlateBlue;
            btnXem.BackgroundImage = Properties.Resources.Colored_Sky;
            btnXem.BorderColor = Color.Transparent;
            btnXem.BorderRadius = 20;
            btnXem.BorderThickness = 0F;
            btnXem.FlatAppearance.BorderSize = 0;
            btnXem.FlatStyle = FlatStyle.Flat;
            btnXem.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXem.ForeColor = Color.White;
            btnXem.Location = new Point(29, 686);
            btnXem.Margin = new Padding(4);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(139, 54);
            btnXem.TabIndex = 0;
            btnXem.Text = "Xem";
            btnXem.UseVisualStyleBackColor = false;
            btnXem.Click += btnXem_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.MediumSlateBlue;
            btnSua.BackgroundImage = Properties.Resources.Colored_Sky;
            btnSua.BorderColor = Color.Transparent;
            btnSua.BorderRadius = 20;
            btnSua.BorderThickness = 0F;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSua.ForeColor = Color.White;
            btnSua.Location = new Point(216, 686);
            btnSua.Margin = new Padding(4);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(175, 54);
            btnSua.TabIndex = 1;
            btnSua.Text = "Thêm bạn bè";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.BorderColor = Color.Transparent;
            btnXoa.BorderRadius = 20;
            btnXoa.BorderThickness = 0F;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXoa.ForeColor = Color.White;
            btnXoa.Location = new Point(444, 686);
            btnXoa.Margin = new Padding(4);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(149, 54);
            btnXoa.TabIndex = 2;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Silver;
            label1.Location = new Point(158, 29);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(303, 38);
            label1.TabIndex = 3;
            label1.Text = "Tìm kiếm người dùng";
            // 
            // flpDanhSach
            // 
            flpDanhSach.BackColor = Color.FromArgb(40, 43, 51);
            flpDanhSach.Location = new Point(29, 155);
            flpDanhSach.Margin = new Padding(4);
            flpDanhSach.Name = "flpDanhSach";
            flpDanhSach.Size = new Size(564, 499);
            flpDanhSach.TabIndex = 5;
            flpDanhSach.Paint += flpDanhSach_Paint;
            // 
            // TimKiemNguoiDung
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(62, 66, 75);
            ClientSize = new Size(620, 788);
            Controls.Add(flpDanhSach);
            Controls.Add(label1);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnXem);
            Controls.Add(roundButton1);
            Controls.Add(txtTimKiem);
            Margin = new Padding(2, 4, 2, 4);
            Name = "TimKiemNguoiDung";
            Text = "Tìm kiếm người dùng";
            ResumeLayout(false);
            PerformLayout();
        }
        private RoundButton btnSua;
        private RoundButton btnXoa;
        private RoundTextBox txtTimKiem;
        private RoundButton roundButton1;
        private RoundButton btnXem;
        private Label label1;
        private FlowLayoutPanel flpDanhSach;
    }
}
