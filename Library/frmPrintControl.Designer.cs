namespace Library
{
    partial class frmPrintControl
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
            this.printControl1 = new Building.PrintControls.PrintControl();
            this.SuspendLayout();
            // 
            // printControl1
            // 
            //this.printControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printControl1.FilterForm = null;
            this.printControl1.IDPhieuthu = 0;
            //this.printControl1.Location = new System.Drawing.Point(0, 0);
            this.printControl1.MaBC = 0;
            this.printControl1.MaTN = ((byte)(0));
            //this.printControl1.Name = "printControl1";
            this.printControl1.ReportID = null;
            //this.printControl1.Size = new System.Drawing.Size(1109, 446);
            //this.printControl1.TabIndex = 0;
            //this.printControl1.Tag = "Nhu cầu theo loại";
            // 
            // frmPrintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 417);
            this.Name = "frmPrintControl";
            this.Text = "frmPrintControl";
            this.ResumeLayout(false);

        }

        #endregion

        private Building.PrintControls.PrintControl printControl1;
    }
}