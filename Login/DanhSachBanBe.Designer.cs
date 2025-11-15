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
            lblTieuDe = new Label();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            lvDanhSach = new ListView();
            btnQuayLai = new Button();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.BackColor = Color.Transparent;
            lblTieuDe.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblTieuDe.ForeColor = SystemColors.ButtonHighlight;
            lblTieuDe.Location = new Point(140, 23);
            lblTieuDe.Margin = new Padding(4, 0, 4, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(172, 19);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "DANH SÁCH BẠN BÈ";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(47, 69);
            txtTimKiem.Margin = new Padding(4, 3, 4, 3);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(233, 23);
            txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.LightSkyBlue;
            btnTimKiem.FlatStyle = FlatStyle.Popup;
            btnTimKiem.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnTimKiem.ForeColor = SystemColors.ButtonHighlight;
            btnTimKiem.Location = new Point(292, 67);
            btnTimKiem.Margin = new Padding(4, 3, 4, 3);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(105, 29);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // lvDanhSach
            // 
            lvDanhSach.BackColor = Color.Gray;
            lvDanhSach.Location = new Point(47, 115);
            lvDanhSach.Margin = new Padding(4, 3, 4, 3);
            lvDanhSach.Name = "lvDanhSach";
            lvDanhSach.Size = new Size(349, 207);
            lvDanhSach.TabIndex = 3;
            lvDanhSach.UseCompatibleStateImageBehavior = false;
            lvDanhSach.View = View.Details;
            // 
            // btnQuayLai
            // 
            btnQuayLai.BackColor = Color.LightGreen;
            btnQuayLai.FlatStyle = FlatStyle.Popup;
            btnQuayLai.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnQuayLai.Location = new Point(152, 346);
            btnQuayLai.Margin = new Padding(4, 3, 4, 3);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(140, 35);
            btnQuayLai.TabIndex = 4;
            btnQuayLai.Text = "Quay Lại";
            btnQuayLai.UseVisualStyleBackColor = false;
            btnQuayLai.Click += btnQuayLai_Click;
            // 
            // DanhSachBanBe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            BackgroundImage = Properties.Resources.nền_đăng_nhập;
            ClientSize = new Size(443, 415);
            Controls.Add(btnQuayLai);
            Controls.Add(lvDanhSach);
            Controls.Add(btnTimKiem);
            Controls.Add(txtTimKiem);
            Controls.Add(lblTieuDe);
            Margin = new Padding(4, 3, 4, 3);
            Name = "DanhSachBanBe";
            Text = "Danh Sách Bạn Bè";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ListView lvDanhSach;
        private System.Windows.Forms.Button btnQuayLai;
    }
}
