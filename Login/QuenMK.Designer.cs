namespace Login
{
    partial class QuenMK
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
            panel1 = new Panel();
            label1 = new Label();
            buttonOTP = new RoundButton();
            button1 = new RoundButton();
            textBox4 = new RoundTextBox();
            textBox3 = new RoundTextBox();
            textBoxOTP = new RoundTextBox();
            textBox2 = new RoundTextBox();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(62, 66, 75);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(buttonOTP);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBoxOTP);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(27, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(404, 513);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(122, 88);
            label1.Name = "label1";
            label1.Size = new Size(147, 25);
            label1.TabIndex = 7;
            label1.Text = "Reset Mật Khẩu";
            // 
            // buttonOTP
            // 
            buttonOTP.BackColor = Color.MediumSlateBlue;
            buttonOTP.BackgroundImage = Properties.Resources.Colored_Sky;
            buttonOTP.BorderColor = Color.Transparent;
            buttonOTP.BorderRadius = 20;
            buttonOTP.BorderThickness = 0F;
            buttonOTP.FlatAppearance.BorderSize = 0;
            buttonOTP.FlatStyle = FlatStyle.Flat;
            buttonOTP.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonOTP.ForeColor = Color.White;
            buttonOTP.Location = new Point(269, 151);
            buttonOTP.Name = "buttonOTP";
            buttonOTP.Size = new Size(89, 35);
            buttonOTP.TabIndex = 6;
            buttonOTP.Text = "Gửi OTP";
            buttonOTP.UseVisualStyleBackColor = false;
            buttonOTP.Click += buttonOTP_Click_1;
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
            button1.Location = new Point(91, 374);
            button1.Name = "button1";
            button1.Size = new Size(226, 40);
            button1.TabIndex = 5;
            button1.Text = "Reset";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(40, 43, 51);
            textBox4.BorderColor = Color.MediumSlateBlue;
            textBox4.BorderFocusColor = Color.HotPink;
            textBox4.BorderRadius = 15;
            textBox4.BorderSize = 0;
            textBox4.ForeColor = Color.Silver;
            textBox4.Location = new Point(46, 306);
            textBox4.Multiline = false;
            textBox4.Name = "textBox4";
            textBox4.Padding = new Padding(10, 7, 10, 7);
            textBox4.PasswordChar = false;
            textBox4.Size = new Size(312, 35);
            textBox4.TabIndex = 4;
            textBox4.Texts = "Verify Password";
            textBox4.UnderlinedStyle = false;
            textBox4.Enter += textBox4_Enter;
            textBox4.Leave += textBox4_Leave;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(40, 43, 51);
            textBox3.BorderColor = Color.MediumSlateBlue;
            textBox3.BorderFocusColor = Color.HotPink;
            textBox3.BorderRadius = 15;
            textBox3.BorderSize = 0;
            textBox3.ForeColor = Color.Silver;
            textBox3.Location = new Point(46, 255);
            textBox3.Multiline = false;
            textBox3.Name = "textBox3";
            textBox3.Padding = new Padding(10, 7, 10, 7);
            textBox3.PasswordChar = false;
            textBox3.Size = new Size(312, 35);
            textBox3.TabIndex = 3;
            textBox3.Texts = "Password";
            textBox3.UnderlinedStyle = false;
            textBox3.Enter += textBox3_Enter;
            textBox3.Leave += textBox3_Leave;
            // 
            // textBoxOTP
            // 
            textBoxOTP.BackColor = Color.FromArgb(40, 43, 51);
            textBoxOTP.BorderColor = Color.MediumSlateBlue;
            textBoxOTP.BorderFocusColor = Color.HotPink;
            textBoxOTP.BorderRadius = 15;
            textBoxOTP.BorderSize = 0;
            textBoxOTP.ForeColor = Color.Silver;
            textBoxOTP.Location = new Point(46, 203);
            textBoxOTP.Multiline = false;
            textBoxOTP.Name = "textBoxOTP";
            textBoxOTP.Padding = new Padding(10, 7, 10, 7);
            textBoxOTP.PasswordChar = false;
            textBoxOTP.Size = new Size(312, 35);
            textBoxOTP.TabIndex = 2;
            textBoxOTP.Texts = "Verify OTP";
            textBoxOTP.UnderlinedStyle = false;
            textBoxOTP.Enter += textBoxOTP_Enter;
            textBoxOTP.Leave += textBoxOTP_Leave;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(40, 43, 51);
            textBox2.BorderColor = Color.MediumSlateBlue;
            textBox2.BorderFocusColor = Color.HotPink;
            textBox2.BorderRadius = 15;
            textBox2.BorderSize = 0;
            textBox2.ForeColor = Color.Silver;
            textBox2.Location = new Point(46, 151);
            textBox2.Multiline = false;
            textBox2.Name = "textBox2";
            textBox2.Padding = new Padding(10, 7, 10, 7);
            textBox2.PasswordChar = false;
            textBox2.Size = new Size(217, 35);
            textBox2.TabIndex = 1;
            textBox2.Texts = "Email";
            textBox2.UnderlinedStyle = false;
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Logo_xám_cyan;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(18, 20);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(69, 70);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // QuenMK
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Colored_Sky;
            ClientSize = new Size(456, 585);
            Controls.Add(panel1);
            Margin = new Padding(2);
            Name = "QuenMK";
            Text = "Quên Mật Khẩu";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private RoundButton buttonOTP;
        private RoundButton button1;
        private RoundTextBox textBox4;
        private RoundTextBox textBox3;
        private RoundTextBox textBoxOTP;
        private RoundTextBox textBox2;
        private PictureBox pictureBox1;
        private Label label1;
    }
}