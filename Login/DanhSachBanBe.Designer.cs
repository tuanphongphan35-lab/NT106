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
            roundFlowLayoutPanel1 = new RoundFlowLayoutPanel();
            roundTextBox1 = new RoundTextBox();
            roundButton1 = new RoundButton();
            roundButton2 = new RoundButton();
            label1 = new Label();
            SuspendLayout();
            // 
            // roundFlowLayoutPanel1
            // 
            roundFlowLayoutPanel1.BackColor = Color.FromArgb(40, 43, 51);
            roundFlowLayoutPanel1.BorderColor = Color.Transparent;
            roundFlowLayoutPanel1.BorderRadius = 20;
            roundFlowLayoutPanel1.BorderThickness = 0F;
            roundFlowLayoutPanel1.Location = new Point(32, 142);
            roundFlowLayoutPanel1.Name = "roundFlowLayoutPanel1";
            roundFlowLayoutPanel1.Size = new Size(443, 421);
            roundFlowLayoutPanel1.TabIndex = 0;
            // 
            // roundTextBox1
            // 
            roundTextBox1.BackColor = Color.FromArgb(40, 43, 51);
            roundTextBox1.BorderColor = Color.MediumSlateBlue;
            roundTextBox1.BorderFocusColor = Color.HotPink;
            roundTextBox1.BorderRadius = 15;
            roundTextBox1.BorderSize = 0;
            roundTextBox1.Location = new Point(32, 84);
            roundTextBox1.Multiline = false;
            roundTextBox1.Name = "roundTextBox1";
            roundTextBox1.Padding = new Padding(10, 7, 10, 7);
            roundTextBox1.PasswordChar = false;
            roundTextBox1.Size = new Size(325, 35);
            roundTextBox1.TabIndex = 1;
            roundTextBox1.Texts = "";
            roundTextBox1.UnderlinedStyle = false;
            // 
            // roundButton1
            // 
            roundButton1.BackColor = Color.MediumSlateBlue;
            roundButton1.BorderColor = Color.Transparent;
            roundButton1.BorderRadius = 20;
            roundButton1.BorderThickness = 0F;
            roundButton1.FlatAppearance.BorderSize = 0;
            roundButton1.FlatStyle = FlatStyle.Flat;
            roundButton1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton1.ForeColor = Color.White;
            roundButton1.Location = new Point(363, 86);
            roundButton1.Name = "roundButton1";
            roundButton1.Size = new Size(112, 33);
            roundButton1.TabIndex = 2;
            roundButton1.Text = "Tìm";
            roundButton1.UseVisualStyleBackColor = false;
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
            Controls.Add(roundButton1);
            Controls.Add(roundTextBox1);
            Controls.Add(roundFlowLayoutPanel1);
            Margin = new Padding(5, 4, 5, 4);
            Name = "DanhSachBanBe";
            Text = "Danh Sách Bạn Bè";
            Load += DanhSachBanBe_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private RoundFlowLayoutPanel roundFlowLayoutPanel1;
        private RoundTextBox roundTextBox1;
        private RoundButton roundButton1;
        private RoundButton roundButton2;
        private Label label1;
    }
}
