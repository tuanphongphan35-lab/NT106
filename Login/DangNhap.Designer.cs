namespace Login
{
    partial class DangNhap
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
            panel2 = new Panel();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            button1 = new RoundButton();
            textBox2 = new RoundTextBox();
            textBox1 = new RoundTextBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(62, 66, 75);
            panel2.Controls.Add(linkLabel2);
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(43, 43);
            panel2.Name = "panel2";
            panel2.Size = new Size(402, 505);
            panel2.TabIndex = 0;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.LinkColor = Color.Silver;
            linkLabel2.Location = new Point(231, 433);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(128, 20);
            linkLabel2.TabIndex = 5;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Create an account";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkColor = Color.Silver;
            linkLabel1.Location = new Point(47, 433);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(125, 20);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Forgot Password?";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
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
            button1.Location = new Point(109, 334);
            button1.Name = "button1";
            button1.Size = new Size(205, 50);
            button1.TabIndex = 3;
            button1.Text = "Đăng nhập";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(40, 43, 51);
            textBox2.BorderColor = Color.MediumSlateBlue;
            textBox2.BorderFocusColor = Color.HotPink;
            textBox2.BorderRadius = 15;
            textBox2.BorderSize = 0;
            textBox2.ForeColor = Color.Silver;
            textBox2.Location = new Point(47, 259);
            textBox2.Multiline = false;
            textBox2.Name = "textBox2";
            textBox2.Padding = new Padding(10, 7, 10, 7);
            textBox2.PasswordChar = false;
            textBox2.Size = new Size(312, 35);
            textBox2.TabIndex = 2;
            textBox2.Texts = "Password";
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
            textBox1.Location = new Point(47, 200);
            textBox1.Multiline = false;
            textBox1.Name = "textBox1";
            textBox1.Padding = new Padding(10, 7, 10, 7);
            textBox1.PasswordChar = false;
            textBox1.Size = new Size(312, 35);
            textBox1.TabIndex = 1;
            textBox1.Texts = "Username";
            textBox1.UnderlinedStyle = false;
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.Logo_xám_cyan;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(146, 48);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(110, 111);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.GreenLake;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(495, 592);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // DangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.nền_đăng_nhập;
            ClientSize = new Size(495, 592);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlText;
            Margin = new Padding(2);
            Name = "DangNhap";
            Text = "DangNhap";
            Load += DangNhap_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private RoundButton button1;
        private RoundTextBox textBox2;
        private RoundTextBox textBox1;
        private PictureBox pictureBox1;
        private Panel panel1;
    }
}