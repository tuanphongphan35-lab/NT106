namespace Login
{
    partial class PhoneCall
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
            label1 = new Label();
            label2 = new Label();
            btnAccept = new RoundButton();
            btnReject = new RoundButton();
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // circularPictureBox1
            // 
            circularPictureBox1.Location = new Point(158, 72);
            circularPictureBox1.Name = "circularPictureBox1";
            circularPictureBox1.Size = new Size(125, 118);
            circularPictureBox1.TabIndex = 0;
            circularPictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(167, 193);
            label1.Name = "label1";
            label1.Size = new Size(106, 28);
            label1.TabIndex = 1;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Silver;
            label2.Location = new Point(167, 221);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 2;
            label2.Text = "Cuộc gọi đến...";
            // 
            // btnAccept
            // 
            btnAccept.BackColor = Color.MediumSlateBlue;
            btnAccept.BackgroundImage = Properties.Resources.AcceptCall;
            btnAccept.BackgroundImageLayout = ImageLayout.Stretch;
            btnAccept.BorderColor = Color.Transparent;
            btnAccept.BorderRadius = 20;
            btnAccept.BorderThickness = 0F;
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.ForeColor = Color.White;
            btnAccept.Location = new Point(80, 495);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(102, 59);
            btnAccept.TabIndex = 3;
            btnAccept.UseVisualStyleBackColor = false;
            btnAccept.Click += btnAccept_Click;
            // 
            // btnReject
            // 
            btnReject.BackColor = Color.MediumSlateBlue;
            btnReject.BackgroundImage = Properties.Resources.RejectCall;
            btnReject.BackgroundImageLayout = ImageLayout.Stretch;
            btnReject.BorderColor = Color.Transparent;
            btnReject.BorderRadius = 20;
            btnReject.BorderThickness = 0F;
            btnReject.FlatAppearance.BorderSize = 0;
            btnReject.FlatStyle = FlatStyle.Flat;
            btnReject.ForeColor = Color.White;
            btnReject.Location = new Point(286, 495);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(102, 59);
            btnReject.TabIndex = 4;
            btnReject.UseVisualStyleBackColor = false;
            btnReject.Click += btnReject_Click;
            // 
            // PhoneCall
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(62, 66, 75);
            ClientSize = new Size(467, 642);
            Controls.Add(btnReject);
            Controls.Add(btnAccept);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(circularPictureBox1);
            Margin = new Padding(2);
            Name = "PhoneCall";
            Text = "PhoneCall";
            ((System.ComponentModel.ISupportInitialize)circularPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CircularPictureBox circularPictureBox1;
        private Label label1;
        private Label label2;
        private RoundButton btnAccept;
        private RoundButton btnReject;
    }
}