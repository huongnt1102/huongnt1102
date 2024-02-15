namespace DIP.SwitchBoard
{
    partial class frmHistoryControl
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
            this.historyControl1 = new DIP.SwitchBoard.HistoryControl();
            this.SuspendLayout();
            // 
            // historyControl1
            // 
            this.historyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyControl1.Location = new System.Drawing.Point(0, 0);
            this.historyControl1.Name = "historyControl1";
            this.historyControl1.Size = new System.Drawing.Size(966, 463);
            this.historyControl1.TabIndex = 0;
            this.historyControl1.Type = 0;
            // 
            // frmHistoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 463);
            this.Controls.Add(this.historyControl1);
            this.Name = "frmHistoryControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhật ký cuộc gọi";
            this.ResumeLayout(false);

        }

        #endregion

        private HistoryControl historyControl1;
    }
}