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
            btnAccept = new Button();
            btnReject = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // btnAccept
            // 
            btnAccept.Location = new Point(107, 436);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(112, 34);
            btnAccept.TabIndex = 0;
            btnAccept.Text = "accpet";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // btnReject
            // 
            btnReject.Location = new Point(319, 436);
            btnReject.Name = "btnReject";
            btnReject.Size = new Size(112, 34);
            btnReject.TabIndex = 1;
            btnReject.Text = "reject";
            btnReject.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(195, 198);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(59, 25);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "label1";
            // 
            // PhoneCall
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 670);
            Controls.Add(lblStatus);
            Controls.Add(btnReject);
            Controls.Add(btnAccept);
            Name = "PhoneCall";
            Text = "PhoneCall";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAccept;
        private Button btnReject;
        private Label lblStatus;
    }
}