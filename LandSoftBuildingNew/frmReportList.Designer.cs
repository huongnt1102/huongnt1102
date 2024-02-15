namespace LandSoftBuildingMain
{
    partial class frmReportList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportList));
            this.reportList1 = new Building.PrintControls.ReportList();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // reportList1
            // 
            this.reportList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportList1.Location = new System.Drawing.Point(0, 0);
            this.reportList1.MaTN = ((byte)(0));
            this.reportList1.Name = "reportList1";
            this.reportList1.Size = new System.Drawing.Size(677, 440);
            this.reportList1.TabIndex = 1;
            this.reportList1.ToaNhaEditValueChanged += new Building.PrintControls.ToaNhaEditValueChangedEventHandler(this.reportList1_ToaNhaEditValueChanged);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Edit3.png");  
            this.imageCollection1.Images.SetKeyName(1, "Print1.png");  
            // 
            // frmReportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 440);
            this.Controls.Add(this.reportList1);
            this.Name = "frmReportList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo";
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Building.PrintControls.ReportList reportList1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}