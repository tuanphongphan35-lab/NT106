namespace Login
{
    partial class DangKy
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
            button1 = new RoundButton();
            button3 = new RoundButton();
            textBox5 = new RoundTextBox();
            textBox4 = new RoundTextBox();
            textBox3 = new RoundTextBox();
            textBoxOTP = new RoundTextBox();
            textBox2 = new RoundTextBox();
            textBox1 = new RoundTextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(62, 66, 75);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(textBox5);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBoxOTP);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(50, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(399, 518);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSlateBlue;
            button1.BackgroundImage = Properties.Resources.CyanToViolet;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.BorderColor = Color.Transparent;
            button1.BorderRadius = 20;
            button1.BorderThickness = 0F;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(82, 420);
            button1.Name = "button1";
            button1.Size = new Size(239, 38);
            button1.TabIndex = 7;
            button1.Text = "Đăng kí";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // button3
            // 
            button3.BackColor = Color.MediumSlateBlue;
            button3.BackgroundImage = Properties.Resources.CyanToViolet;
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.BorderColor = Color.Transparent;
            button3.BorderRadius = 20;
            button3.BorderThickness = 0F;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Location = new Point(261, 119);
            button3.Name = "button3";
            button3.Size = new Size(107, 35);
            button3.TabIndex = 6;
            button3.Text = "Gửi OTP";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // textBox5
            // 
            textBox5.BackColor = Color.FromArgb(40, 43, 51);
            textBox5.BorderColor = Color.MediumSlateBlue;
            textBox5.BorderFocusColor = Color.HotPink;
            textBox5.BorderRadius = 15;
            textBox5.BorderSize = 0;
            textBox5.ForeColor = Color.Silver;
            textBox5.Location = new Point(31, 363);
            textBox5.Multiline = false;
            textBox5.Name = "textBox5";
            textBox5.Padding = new Padding(10, 7, 10, 7);
            textBox5.PasswordChar = false;
            textBox5.Size = new Size(337, 35);
            textBox5.TabIndex = 5;
            textBox5.Texts = "Verify Password";
            textBox5.UnderlinedStyle = false;
            textBox5.Enter += textBox5_Enter;
            textBox5.Leave += textBox5_Leave;
            // 
            // textBox4
            // 
            textBox4.BackColor = Color.FromArgb(40, 43, 51);
            textBox4.BorderColor = Color.MediumSlateBlue;
            textBox4.BorderFocusColor = Color.HotPink;
            textBox4.BorderRadius = 15;
            textBox4.BorderSize = 0;
            textBox4.ForeColor = Color.Silver;
            textBox4.Location = new Point(31, 302);
            textBox4.Multiline = false;
            textBox4.Name = "textBox4";
            textBox4.Padding = new Padding(10, 7, 10, 7);
            textBox4.PasswordChar = false;
            textBox4.Size = new Size(337, 35);
            textBox4.TabIndex = 4;
            textBox4.Texts = "Password";
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
            textBox3.Location = new Point(31, 239);
            textBox3.Multiline = false;
            textBox3.Name = "textBox3";
            textBox3.Padding = new Padding(10, 7, 10, 7);
            textBox3.PasswordChar = false;
            textBox3.Size = new Size(337, 35);
            textBox3.TabIndex = 3;
            textBox3.Texts = "Username";
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
            textBoxOTP.Location = new Point(31, 180);
            textBoxOTP.Multiline = false;
            textBoxOTP.Name = "textBoxOTP";
            textBoxOTP.Padding = new Padding(10, 7, 10, 7);
            textBoxOTP.PasswordChar = false;
            textBoxOTP.Size = new Size(337, 35);
            textBoxOTP.TabIndex = 2;
            textBoxOTP.Texts = "Verify OTP";
            textBoxOTP.UnderlinedStyle = false;
            textBoxOTP._TextChanged += roundTextBox2__TextChanged;
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
            textBox2.Location = new Point(31, 119);
            textBox2.Multiline = false;
            textBox2.Name = "textBox2";
            textBox2.Padding = new Padding(10, 7, 10, 7);
            textBox2.PasswordChar = false;
            textBox2.Size = new Size(224, 35);
            textBox2.TabIndex = 1;
            textBox2.Texts = "Email";
            textBox2.UnderlinedStyle = false;
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(40, 43, 51);
            textBox1.BorderColor = Color.MediumSlateBlue;
            textBox1.BorderFocusColor = Color.HotPink;
            textBox1.BorderRadius = 15;
            textBox1.BorderSize = 0;
            textBox1.ForeColor = Color.Silver;
            textBox1.Location = new Point(31, 58);
            textBox1.Multiline = false;
            textBox1.Name = "textBox1";
            textBox1.Padding = new Padding(10, 7, 10, 7);
            textBox1.PasswordChar = false;
            textBox1.Size = new Size(337, 35);
            textBox1.TabIndex = 0;
            textBox1.Texts = "ID tự tạo";
            textBox1.UnderlinedStyle = false;
            // 
            // DangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.GreenLake;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(495, 618);
            Controls.Add(panel1);
            DoubleBuffered = true;
            Margin = new Padding(2);
            Name = "DangKy";
            Text = "DangKy";
            Load += DangKy_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private RoundTextBox textBox1;
        private RoundTextBox textBox5;
        private RoundTextBox textBox4;
        private RoundTextBox textBox3;
        private RoundTextBox textBoxOTP;
        private RoundTextBox textBox2;
        private RoundButton button1;
        private RoundButton button3;
    }
}