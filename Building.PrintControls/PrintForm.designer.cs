namespace Building.PrintControls
{
    partial class PrintForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintForm));
            this.PrintControl = new Building.PrintControls.PrintControl();
            this.SuspendLayout();
            // 
            // PrintControl
            // 
            this.PrintControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintControl.FilterForm = null;
            this.PrintControl.Location = new System.Drawing.Point(0, 0);
            this.PrintControl.Name = "PrintControl";
            this.PrintControl.ReportID = null;
            this.PrintControl.Size = new System.Drawing.Size(940, 439);
            this.PrintControl.TabIndex = 0;
            this.PrintControl.Tag = "";
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 439);
            this.Controls.Add(this.PrintControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrintForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public PrintControl PrintControl;

    }
}