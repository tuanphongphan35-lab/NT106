namespace Login
{
    partial class VideoCall
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
            pictureBox2 = new PictureBox();
            roundPanel1 = new RoundPanel();
            roundButton1 = new RoundButton();
            roundButton2 = new RoundButton();
            roundButton3 = new RoundButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            roundPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(913, 565);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(730, 368);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(161, 172);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // roundPanel1
            // 
            roundPanel1.BackColor = Color.FromArgb(40, 43, 51);
            roundPanel1.BorderColor = Color.Gray;
            roundPanel1.BorderRadius = 10;
            roundPanel1.BorderThickness = 1F;
            roundPanel1.Controls.Add(roundButton3);
            roundPanel1.Controls.Add(roundButton2);
            roundPanel1.Controls.Add(roundButton1);
            roundPanel1.Location = new Point(44, 588);
            roundPanel1.Name = "roundPanel1";
            roundPanel1.Size = new Size(832, 59);
            roundPanel1.TabIndex = 2;
            // 
            // roundButton1
            // 
            roundButton1.BackColor = Color.CornflowerBlue;
            roundButton1.BorderColor = Color.Transparent;
            roundButton1.BorderRadius = 20;
            roundButton1.BorderThickness = 0F;
            roundButton1.FlatAppearance.BorderSize = 0;
            roundButton1.FlatStyle = FlatStyle.Flat;
            roundButton1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton1.ForeColor = Color.White;
            roundButton1.Location = new Point(32, 6);
            roundButton1.Name = "roundButton1";
            roundButton1.Size = new Size(188, 50);
            roundButton1.TabIndex = 0;
            roundButton1.Text = "Bật/Tắt Mic";
            roundButton1.UseVisualStyleBackColor = false;
            // 
            // roundButton2
            // 
            roundButton2.BackColor = Color.CornflowerBlue;
            roundButton2.BorderColor = Color.Transparent;
            roundButton2.BorderRadius = 20;
            roundButton2.BorderThickness = 0F;
            roundButton2.FlatAppearance.BorderSize = 0;
            roundButton2.FlatStyle = FlatStyle.Flat;
            roundButton2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton2.ForeColor = Color.White;
            roundButton2.Location = new Point(309, 6);
            roundButton2.Name = "roundButton2";
            roundButton2.Size = new Size(188, 50);
            roundButton2.TabIndex = 1;
            roundButton2.Text = "Bật/Tắt Video";
            roundButton2.UseVisualStyleBackColor = false;
            // 
            // roundButton3
            // 
            roundButton3.BackColor = Color.Red;
            roundButton3.BorderColor = Color.Transparent;
            roundButton3.BorderRadius = 20;
            roundButton3.BorderThickness = 0F;
            roundButton3.FlatAppearance.BorderSize = 0;
            roundButton3.FlatStyle = FlatStyle.Flat;
            roundButton3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundButton3.ForeColor = Color.White;
            roundButton3.Location = new Point(593, 6);
            roundButton3.Name = "roundButton3";
            roundButton3.Size = new Size(188, 50);
            roundButton3.TabIndex = 2;
            roundButton3.Text = "Thoát";
            roundButton3.UseVisualStyleBackColor = false;
            // 
            // VideoCall
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(62, 66, 75);
            ClientSize = new Size(913, 681);
            Controls.Add(roundPanel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "VideoCall";
            Text = "VideoCall";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            roundPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private RoundPanel roundPanel1;
        private RoundButton roundButton3;
        private RoundButton roundButton2;
        private RoundButton roundButton1;
    }
}