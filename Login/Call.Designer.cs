namespace Login
{
    partial class Call
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
            circularPictureBox1 = new PictureBox();
            circularPictureBox2 = new PictureBox();
            panel1 = new Panel();
            button2 = new RoundButton();
            button1 = new RoundButton();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.Dock = DockStyle.Top;
            circularPictureBox1.Location = new Point(0, 0);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(913, 565);
            circularPictureBox1.TabIndex = 0;
            circularPictureBox1.TabStop = false;
            // 
            // circularPictureBox2
            // 
            circularPictureBox2.Location = new Point(730, 368);
            circularPictureBox2.Name = "circularPictureBox2";
            circularPictureBox2.Size = new Size(161, 172);
            circularPictureBox2.TabIndex = 1;
            circularPictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(40, 43, 51);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(44, 588);
            panel1.Name = "panel1";
            panel1.Size = new Size(832, 59);
            panel1.TabIndex = 2;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.BorderColor = Color.Transparent;
            button2.BorderRadius = 20;
            button2.BorderThickness = 0F;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.White;
            button2.Location = new Point(540, 6);
            button2.Name = "button2";
            button2.Size = new Size(188, 50);
            button2.TabIndex = 1;
            button2.Text = "Thoát";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.CornflowerBlue;
            button1.BorderColor = Color.Transparent;
            button1.BorderRadius = 20;
            button1.BorderThickness = 0F;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(109, 6);
            button1.Name = "button1";
            button1.Size = new Size(188, 50);
            button1.TabIndex = 0;
            button1.Text = "Mic";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Call
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(62, 66, 75);
            ClientSize = new Size(913, 681);
            Controls.Add(panel1);
            Controls.Add(circularPictureBox2);
            Controls.Add(circularPictureBox1);
            Margin = new Padding(2);
            Name = "Call";
            Text = "Call";
            Load += Call_Load;
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox circularPictureBox1;
        private PictureBox circularPictureBox2;
        private Panel panel1;
        private RoundButton button2;
        private RoundButton button1;
    }
}