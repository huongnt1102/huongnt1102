namespace DIPCRM.Need
{
    partial class frmDetail
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
            this.ctlManager1 = new DIPCRM.NhuCau.ctlManager();
            this.SuspendLayout();
            // 
            // ctlManager1
            // 
            this.ctlManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlManager1.Location = new System.Drawing.Point(0, 0);
            this.ctlManager1.Name = "ctlManager1";
            this.ctlManager1.Size = new System.Drawing.Size(1100, 450);
            this.ctlManager1.TabIndex = 0;
            this.ctlManager1.Tag = "Nhu cầu";
            // 
            // frmDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 450);
            this.Controls.Add(this.ctlManager1);
            this.Name = "frmDetail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin chi tiết cơ hội kinh doanh";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private NhuCau.ctlManager ctlManager1;
    }
}