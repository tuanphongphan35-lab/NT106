namespace Login
{
    partial class TimKiemNguoiDung
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvNguoiDung;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Label lblTenNguoiDung;

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
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            dgvNguoiDung = new DataGridView();
            btnXem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            lblTenNguoiDung = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvNguoiDung).BeginInit();
            SuspendLayout();
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(124, 20);
            txtTimKiem.Margin = new Padding(2);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(172, 23);
            txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = SystemColors.Highlight;
            btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnTimKiem.ForeColor = SystemColors.HighlightText;
            btnTimKiem.Location = new Point(311, 19);
            btnTimKiem.Margin = new Padding(2);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(78, 22);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvNguoiDung
            // 
            dgvNguoiDung.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNguoiDung.Location = new Point(23, 60);
            dgvNguoiDung.Margin = new Padding(2);
            dgvNguoiDung.Name = "dgvNguoiDung";
            dgvNguoiDung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNguoiDung.Size = new Size(467, 188);
            dgvNguoiDung.TabIndex = 3;
            // 
            // btnXem
            // 
            btnXem.BackColor = SystemColors.Highlight;
            btnXem.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnXem.ForeColor = SystemColors.ButtonHighlight;
            btnXem.Location = new Point(23, 262);
            btnXem.Margin = new Padding(2);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(70, 26);
            btnXem.TabIndex = 4;
            btnXem.Text = "Xem";
            btnXem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = SystemColors.Highlight;
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSua.ForeColor = SystemColors.ButtonHighlight;
            btnSua.Location = new Point(101, 262);
            btnSua.Margin = new Padding(2);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(70, 26);
            btnSua.TabIndex = 5;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = SystemColors.Highlight;
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnXoa.ForeColor = SystemColors.HighlightText;
            btnXoa.Location = new Point(179, 262);
            btnXoa.Margin = new Padding(2);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(70, 26);
            btnXoa.TabIndex = 6;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // lblTenNguoiDung
            // 
            lblTenNguoiDung.AutoSize = true;
            lblTenNguoiDung.BackColor = Color.Transparent;
            lblTenNguoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTenNguoiDung.ForeColor = SystemColors.ButtonHighlight;
            lblTenNguoiDung.Location = new Point(23, 22);
            lblTenNguoiDung.Margin = new Padding(2, 0, 2, 0);
            lblTenNguoiDung.Name = "lblTenNguoiDung";
            lblTenNguoiDung.Size = new Size(97, 15);
            lblTenNguoiDung.TabIndex = 0;
            lblTenNguoiDung.Text = "Tên người dùng:";
            // 
            // TimKiemNguoiDung
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.nền_đăng_nhập;
            ClientSize = new Size(544, 315);
            Controls.Add(lblTenNguoiDung);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(dgvNguoiDung);
            Controls.Add(btnXem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Margin = new Padding(2);
            Name = "TimKiemNguoiDung";
            Text = "Tìm kiếm người dùng";
            ((System.ComponentModel.ISupportInitialize)dgvNguoiDung).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
