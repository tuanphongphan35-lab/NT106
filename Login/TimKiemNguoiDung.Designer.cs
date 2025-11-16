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
            txtTimKiem.Location = new Point(177, 33);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(244, 31);
            txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = SystemColors.Highlight;
            btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnTimKiem.ForeColor = SystemColors.HighlightText;
            btnTimKiem.Location = new Point(444, 32);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(111, 37);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvNguoiDung
            // 
            dgvNguoiDung.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNguoiDung.Location = new Point(33, 100);
            dgvNguoiDung.Name = "dgvNguoiDung";
            dgvNguoiDung.RowHeadersWidth = 62;
            dgvNguoiDung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNguoiDung.Size = new Size(667, 313);
            dgvNguoiDung.TabIndex = 3;
            // 
            // btnXem
            // 
            btnXem.BackColor = SystemColors.Highlight;
            btnXem.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnXem.ForeColor = SystemColors.ButtonHighlight;
            btnXem.Location = new Point(33, 437);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(100, 43);
            btnXem.TabIndex = 4;
            btnXem.Text = "Xem";
            btnXem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = SystemColors.Highlight;
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSua.ForeColor = SystemColors.ButtonHighlight;
            btnSua.Location = new Point(167, 437);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(254, 43);
            btnSua.TabIndex = 5;
            btnSua.Text = "Thêm Bạn Bè";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = SystemColors.Highlight;
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnXoa.ForeColor = SystemColors.HighlightText;
            btnXoa.Location = new Point(480, 437);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 43);
            btnXoa.TabIndex = 6;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // lblTenNguoiDung
            // 
            lblTenNguoiDung.AutoSize = true;
            lblTenNguoiDung.BackColor = Color.Transparent;
            lblTenNguoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTenNguoiDung.ForeColor = SystemColors.ButtonHighlight;
            lblTenNguoiDung.Location = new Point(33, 37);
            lblTenNguoiDung.Name = "lblTenNguoiDung";
            lblTenNguoiDung.Size = new Size(152, 25);
            lblTenNguoiDung.TabIndex = 0;
            lblTenNguoiDung.Text = "Tên người dùng:";
            // 
            // TimKiemNguoiDung
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.nền_đăng_nhập;
            ClientSize = new Size(777, 525);
            Controls.Add(lblTenNguoiDung);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(dgvNguoiDung);
            Controls.Add(btnXem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Name = "TimKiemNguoiDung";
            Text = "Tìm kiếm người dùng";
            ((System.ComponentModel.ISupportInitialize)dgvNguoiDung).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
