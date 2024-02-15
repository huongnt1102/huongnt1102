namespace Library.ThongKeCtl
{
    partial class ctlTKMatBangTheoLoaiMB
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
            this.chartMatBangTheoLoai = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chartMatBangTheoLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMatBangTheoLoai
            // 
            this.chartMatBangTheoLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMatBangTheoLoai.Location = new System.Drawing.Point(0, 0);
            this.chartMatBangTheoLoai.Name = "chartMatBangTheoLoai";
            this.chartMatBangTheoLoai.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            sideBySideBarSeriesLabel1.LineVisible = true;
            this.chartMatBangTheoLoai.SeriesTemplate.Label = sideBySideBarSeriesLabel1;
            this.chartMatBangTheoLoai.Size = new System.Drawing.Size(562, 317);
            this.chartMatBangTheoLoai.TabIndex = 0;
            // 
            // ctlTKMatBangTheoLoaiMB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartMatBangTheoLoai);
            this.Name = "ctlTKMatBangTheoLoaiMB";
            this.Size = new System.Drawing.Size(562, 317);
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMatBangTheoLoai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartMatBangTheoLoai;
    }
}
