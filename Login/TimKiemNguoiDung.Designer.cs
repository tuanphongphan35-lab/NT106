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
            txtTimKiem.Location = new Point(178, 34);
            txtTimKiem.Margin = new Padding(2, 4, 2, 4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(244, 31);
            txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = SystemColors.Highlight;
            btnTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnTimKiem.ForeColor = SystemColors.HighlightText;
            btnTimKiem.Location = new Point(444, 31);
            btnTimKiem.Margin = new Padding(2, 4, 2, 4);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(111, 36);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvNguoiDung
            // 
            dgvNguoiDung.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNguoiDung.Location = new Point(32, 100);
            dgvNguoiDung.Margin = new Padding(2, 4, 2, 4);
            dgvNguoiDung.Name = "dgvNguoiDung";
            dgvNguoiDung.RowHeadersWidth = 51;
            dgvNguoiDung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNguoiDung.Size = new Size(668, 314);
            dgvNguoiDung.TabIndex = 3;
            // 
            // btnXem
            // 
            btnXem.BackColor = SystemColors.Highlight;
            btnXem.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnXem.ForeColor = SystemColors.ButtonHighlight;
            btnXem.Location = new Point(32, 436);
            btnXem.Margin = new Padding(2, 4, 2, 4);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(100, 44);
            btnXem.TabIndex = 4;
            btnXem.Text = "Xem";
            btnXem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = SystemColors.Highlight;
            btnSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnSua.ForeColor = SystemColors.ButtonHighlight;
            btnSua.Location = new Point(249, 436);
            btnSua.Margin = new Padding(2, 4, 2, 4);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(218, 44);
            btnSua.TabIndex = 5;
            btnSua.Text = "Thêm bạn bè ";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = SystemColors.Highlight;
            btnXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnXoa.ForeColor = SystemColors.HighlightText;
            btnXoa.Location = new Point(600, 436);
            btnXoa.Margin = new Padding(2, 4, 2, 4);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 44);
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
            lblTenNguoiDung.Location = new Point(16, 36);
            lblTenNguoiDung.Margin = new Padding(2, 0, 2, 0);
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
            ClientSize = new Size(778, 525);
            Controls.Add(lblTenNguoiDung);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(dgvNguoiDung);
            Controls.Add(btnXem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Margin = new Padding(2, 4, 2, 4);
            Name = "TimKiemNguoiDung";
            Text = "Tìm kiếm người dùng";
            Load += TimKiemNguoiDung_Load;
            ((System.ComponentModel.ISupportInitialize)dgvNguoiDung).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
