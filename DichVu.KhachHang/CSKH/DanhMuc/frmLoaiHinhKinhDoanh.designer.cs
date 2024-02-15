namespace DichVu.KhachHang.CSKH
{
    partial class frmLoaiHinhKinhDoanh
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
            this.gcLoaiHinhKD = new DevExpress.XtraGrid.GridControl();
            this.gvLoaiHinhKD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiHinhKD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiHinhKD)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLoaiHinhKD
            // 
            this.gcLoaiHinhKD.Location = new System.Drawing.Point(12, 12);
            this.gcLoaiHinhKD.MainView = this.gvLoaiHinhKD;
            this.gcLoaiHinhKD.Name = "gcLoaiHinhKD";
            this.gcLoaiHinhKD.Size = new System.Drawing.Size(616, 285);
            this.gcLoaiHinhKD.TabIndex = 0;
            this.gcLoaiHinhKD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoaiHinhKD});
            // 
            // gvLoaiHinhKD
            // 
            this.gvLoaiHinhKD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvLoaiHinhKD.GridControl = this.gcLoaiHinhKD;
            this.gvLoaiHinhKD.Name = "gvLoaiHinhKD";
            this.gvLoaiHinhKD.OptionsCustomization.AllowGroup = false;
            this.gvLoaiHinhKD.OptionsSelection.MultiSelect = true;
            this.gvLoaiHinhKD.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvLoaiHinhKD.OptionsView.ShowGroupPanel = false;
            this.gvLoaiHinhKD.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gvLoaiHinhKD_KeyUp);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "STT";
            this.gridColumn1.FieldName = "STT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 95;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên Loại Hình Kinh Doanh";
            this.gridColumn2.FieldName = "TenLoaiHinhKD";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 1066;
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(454, 303);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(93, 23);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu && Đóng";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(553, 303);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmLoaiHinhKinhDoanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 338);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.gcLoaiHinhKD);
            this.Name = "frmLoaiHinhKinhDoanh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loại Hình Kinh Doanh";
            this.Load += new System.EventHandler(this.frmLoaiHinhKinhDoanh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiHinhKD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiHinhKD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcLoaiHinhKD;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoaiHinhKD;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}