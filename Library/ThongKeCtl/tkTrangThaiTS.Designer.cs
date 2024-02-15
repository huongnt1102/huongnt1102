namespace Library.ThongKeCtl
{
    partial class tkTrangThaiTS
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            this.ChartTrangThaiTS = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTrangThaiTS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartTrangThaiTS
            // 
            this.ChartTrangThaiTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartTrangThaiTS.Location = new System.Drawing.Point(0, 0);
            this.ChartTrangThaiTS.Name = "ChartTrangThaiTS";
            this.ChartTrangThaiTS.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel1.LineVisible = true;
            this.ChartTrangThaiTS.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
            this.ChartTrangThaiTS.Size = new System.Drawing.Size(405, 291);
            this.ChartTrangThaiTS.TabIndex = 0;
            // 
            // tkTrangThaiTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChartTrangThaiTS);
            this.Name = "tkTrangThaiTS";
            this.Size = new System.Drawing.Size(405, 291);
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTrangThaiTS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl ChartTrangThaiTS;
    }
}
