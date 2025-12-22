namespace Login
{
    partial class ThongBaoVideoCall
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
            circularPictureBox1 = new CircularPictureBox();
            roundPanel1 = new RoundPanel();
            label1 = new Label();
            label2 = new Label();
            roundButton1 = new RoundButton();
            roundButton2 = new RoundButton();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            roundPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.Location = new Point(176, 28);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(125, 119);
            circularPictureBox1.TabIndex = 0;
            circularPictureBox1.TabStop = false;
            // 
            // roundPanel1
            // 
            roundPanel1.BackColor = Color.FromArgb(40, 43, 51);
            roundPanel1.BorderColor = Color.Gray;
            roundPanel1.BorderRadius = 10;
            roundPanel1.BorderThickness = 1F;
            roundPanel1.Controls.Add(roundButton2);
            roundPanel1.Controls.Add(roundButton1);
            roundPanel1.Location = new Point(23, 534);
            roundPanel1.Name = "roundPanel1";
            roundPanel1.Size = new Size(459, 57);
            roundPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(180, 160);
            label1.Name = "label1";
            label1.Size = new Size(121, 31);
            label1.TabIndex = 2;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Silver;
            label2.Location = new Point(180, 208);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 3;
            label2.Text = "Cuộc gọi đến...";
            label2.Click += label2_Click;
            // 
            // roundButton1
            // 
            roundButton1.BackColor = Color.MediumSlateBlue;
            roundButton1.BackgroundImage = Properties.Resources.RejectCall;
            roundButton1.BackgroundImageLayout = ImageLayout.Zoom;
            roundButton1.BorderColor = Color.Transparent;
            roundButton1.BorderRadius = 20;
            roundButton1.BorderThickness = 0F;
            roundButton1.FlatAppearance.BorderSize = 0;
            roundButton1.FlatStyle = FlatStyle.Flat;
            roundButton1.ForeColor = Color.White;
            roundButton1.Location = new Point(62, 3);
            roundButton1.Name = "roundButton1";
            roundButton1.Size = new Size(91, 50);
            roundButton1.TabIndex = 0;
            roundButton1.UseVisualStyleBackColor = false;
            // 
            // roundButton2
            // 
            roundButton2.BackColor = Color.MediumSlateBlue;
            roundButton2.BackgroundImage = Properties.Resources.AcceptCall;
            roundButton2.BackgroundImageLayout = ImageLayout.Zoom;
            roundButton2.BorderColor = Color.Transparent;
            roundButton2.BorderRadius = 20;
            roundButton2.BorderThickness = 0F;
            roundButton2.FlatAppearance.BorderSize = 0;
            roundButton2.FlatStyle = FlatStyle.Flat;
            roundButton2.ForeColor = Color.White;
            roundButton2.Location = new Point(300, 3);
            roundButton2.Name = "roundButton2";
            roundButton2.Size = new Size(89, 50);
            roundButton2.TabIndex = 1;
            roundButton2.UseVisualStyleBackColor = false;
            // 
            // ThongBaoVideoCall
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(62, 66, 75);
            ClientSize = new Size(500, 632);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(roundPanel1);
            Controls.Add(circularPictureBox1);
            Name = "ThongBaoVideoCall";
            Text = "ThongBaoVideoCall";
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            roundPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CircularPictureBox circularPictureBox1;
        private RoundPanel roundPanel1;
        private Label label1;
        private Label label2;
        private RoundButton roundButton2;
        private RoundButton roundButton1;
    }
}