namespace DichVu.PhiVeSinh
{
    partial class frmPaidMulti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaidMulti));
            this.gcCongNo = new DevExpress.XtraGrid.GridControl();
            this.gvCongNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.gcCongNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCongNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCongNo
            // 
            this.gcCongNo.Location = new System.Drawing.Point(12, 12);
            this.gcCongNo.MainView = this.gvCongNo;
            this.gcCongNo.Name = "gcCongNo";
            this.gcCongNo.Size = new System.Drawing.Size(712, 394);
            this.gcCongNo.TabIndex = 0;
            this.gcCongNo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCongNo});
            // 
            // gvCongNo
            // 
            this.gvCongNo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gvCongNo.GridControl = this.gcCongNo;
            this.gvCongNo.Name = "gvCongNo";
            this.gvCongNo.OptionsSelection.MultiSelect = true;
            this.gvCongNo.OptionsView.ColumnAutoWidth = false;
            this.gvCongNo.OptionsView.ShowAutoFilterRow = true;
            this.gvCongNo.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mặt bằng";
            this.gridColumn1.FieldName = "MaSoMB";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 103;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày thu";
            this.gridColumn2.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "NgayThu";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Đợt thanh toán";
            this.gridColumn3.DisplayFormat.FormatString = "{0:MM/yyyy}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "DotThu";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 89;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Số tiền";
            this.gridColumn4.DisplayFormat.FormatString = "{0:#,0.#}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "SoTien";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 104;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Người nộp";
            this.gridColumn5.FieldName = "NguoiNop";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 135;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Địa chỉ";
            this.gridColumn6.FieldName = "DiaChi";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 158;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Diễn giải";
            this.gridColumn7.FieldName = "DienGiai";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 164;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Chuyển khoản";
            this.gridColumn8.FieldName = "ChuyenKhoan";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 81;
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 1;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(636, 412);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 26);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.ImageOptions.ImageIndex = 0;
            this.btnChapNhan.ImageOptions.ImageList = this.imageCollection1;
            this.btnChapNhan.Location = new System.Drawing.Point(534, 412);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(96, 26);
            this.btnChapNhan.TabIndex = 4;
            this.btnChapNhan.Text = "&Chấp nhận";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_save.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_cancel1.png");  
            // 
            // frmPaidMulti
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(736, 446);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.gcCongNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaidMulti";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thanh toán tổng hợp";
            this.Load += new System.EventHandler(this.frmPaidMulti_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcCongNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCongNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCongNo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCongNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}