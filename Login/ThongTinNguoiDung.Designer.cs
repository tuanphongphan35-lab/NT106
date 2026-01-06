namespace Login
{
    partial class ThongTinNguoiDung
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
            pictureBox1 = new PictureBox();
            circularPictureBox1 = new CircularPictureBox();
            panel1 = new Panel();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox3 = new RoundTextBox();
            textBox2 = new RoundTextBox();
            textBox4 = new RoundTextBox();
            textBox1 = new RoundTextBox();
            button1 = new RoundButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Logo_xám_cyan;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(-1, -1);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(102, 105);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.BackColor = Color.FromArgb(62, 66, 75);
            circularPictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            circularPictureBox1.Location = new Point(185, 68);
            circularPictureBox1.Margin = new Padding(4);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(126, 124);
            circularPictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            circularPictureBox1.TabIndex = 1;
            circularPictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(62, 66, 75);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(40, 151);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(430, 474);
            panel1.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(62, 66, 75);
            label5.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(118, 62);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(189, 30);
            label5.TabIndex = 8;
            label5.Text = "User Information";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(62, 66, 75);
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(42, 355);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(42, 25);
            label4.TabIndex = 7;
            label4.Text = "Sex";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(62, 66, 75);
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(42, 218);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(58, 25);
            label3.TabIndex = 6;
            label3.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(62, 66, 75);
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(42, 286);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(55, 25);
            label2.TabIndex = 5;
            label2.Text = "Birth";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(62, 66, 75);
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(24, 144);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 4;
            label1.Text = "Username";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(40, 43, 51);
            textBox3.BorderColor = Color.MediumSlateBlue;
            textBox3.BorderFocusColor = Color.HotPink;
            textBox3.BorderRadius = 15;
            textBox3.BorderSize = 0;
            textBox3.ForeColor = Color.Silver;
            textBox3.Location = new Point(131, 336);
            textBox3.Margin = new Padding(4);
            textBox3.Multiline = false;
            textBox3.Name = "textBox3";
            textBox3.Padding = new Padding(12, 9, 12, 9);
            textBox3.PasswordChar = false;
            textBox3.Size = new Size(261, 44);
            textBox3.TabIndex = 3;
            textBox3.Texts = "";
            textBox3.UnderlinedStyle = false;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(40, 43, 51);
            textBox2.BorderColor = Color.MediumSlateBlue;
            textBox2.BorderFocusColor = Color.HotPink;
            textBox2.BorderRadius = 15;
            textBox2.BorderSize = 0;
            textBox2.ForeColor = Color.Silver;
            textBox2.Location = new Point(131, 268);
            textBox2.Margin = new Padding(4);
            textBox2.Multiline = false;
            textBox2.Name = "textBox2";
            textBox2.Padding = new Padding(12, 9, 12, 9);
            textBox2.PasswordChar = false;
            textBox2.Size = new Size(261, 44);
            textBox2.TabIndex = 2;
            textBox2.Texts = "";
            textBox2.UnderlinedStyle = false;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(40, 43, 51);
            textBox4.BorderColor = Color.MediumSlateBlue;
            textBox4.BorderFocusColor = Color.HotPink;
            textBox4.BorderRadius = 15;
            textBox4.BorderSize = 0;
            textBox4.ForeColor = Color.Silver;
            textBox4.Location = new Point(131, 199);
            textBox4.Margin = new Padding(4);
            textBox4.Multiline = false;
            textBox4.Name = "textBox4";
            textBox4.Padding = new Padding(12, 9, 12, 9);
            textBox4.PasswordChar = false;
            textBox4.Size = new Size(261, 44);
            textBox4.TabIndex = 1;
            textBox4.Texts = "";
            textBox4.UnderlinedStyle = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(40, 43, 51);
            textBox1.BorderColor = Color.MediumSlateBlue;
            textBox1.BorderFocusColor = Color.HotPink;
            textBox1.BorderRadius = 15;
            textBox1.BorderSize = 0;
            textBox1.ForeColor = Color.Silver;
            textBox1.Location = new Point(131, 125);
            textBox1.Margin = new Padding(4);
            textBox1.Multiline = false;
            textBox1.Name = "textBox1";
            textBox1.Padding = new Padding(12, 9, 12, 9);
            textBox1.PasswordChar = false;
            textBox1.Size = new Size(261, 44);
            textBox1.TabIndex = 0;
            textBox1.Texts = "";
            textBox1.UnderlinedStyle = false;
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSlateBlue;
            button1.BackgroundImage = Properties.Resources.Colored_Sky;
            button1.BorderColor = Color.Transparent;
            button1.BorderRadius = 20;
            button1.BorderThickness = 0F;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(138, 601);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(235, 62);
            button1.TabIndex = 8;
            button1.Text = "Chỉnh sửa thông tin";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // ThongTinNguoiDung
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 43, 51);
            ClientSize = new Size(509, 719);
            Controls.Add(button1);
            Controls.Add(circularPictureBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "ThongTinNguoiDung";
            Text = "ThongTinNguoiDung";
            Load += ThongTinNguoiDung_Load;
            MouseDown += ThongTinNguoiDungForm_MouseDown;
            MouseMove += ThongTinNguoiDungForm_MouseMove;
            MouseUp += ThongTinNguoiDungForm_MouseUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private CircularPictureBox circularPictureBox1;
        private Panel panel1;
        private Label label1;
        private RoundTextBox textBox3;
        private RoundTextBox textBox2;
        private RoundTextBox textBox4;
        private RoundTextBox textBox1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private RoundButton button1;
    }
}