namespace KyThuat.NhiemVu
{
    partial class frmUpdateEquipmentcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateEquipmentcs));
            this.gcThietBi = new DevExpress.XtraGrid.GridControl();
            this.grvThietBi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaTB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinSoLuong = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinDonGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDMCV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteRow = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.gcThietBi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThietBi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcThietBi
            // 
            this.gcThietBi.Location = new System.Drawing.Point(7, 10);
            this.gcThietBi.MainView = this.grvThietBi;
            this.gcThietBi.Name = "gcThietBi";
            this.gcThietBi.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinDonGia,
            this.spinSoLuong});
            this.gcThietBi.Size = new System.Drawing.Size(746, 286);
            this.gcThietBi.TabIndex = 9;
            this.gcThietBi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvThietBi});
            // 
            // grvThietBi
            // 
            this.grvThietBi.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaTB,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn9,
            this.gridColumn10,
            this.colDMCV});
            this.grvThietBi.GridControl = this.gcThietBi;
            this.grvThietBi.Name = "grvThietBi";
            this.grvThietBi.OptionsCustomization.AllowGroup = false;
            this.grvThietBi.OptionsSelection.MultiSelect = true;
            this.grvThietBi.OptionsView.ColumnAutoWidth = false;
            this.grvThietBi.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvThietBi.OptionsView.ShowGroupPanel = false;
            this.grvThietBi.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvThietBi_InitNewRow);
            this.grvThietBi.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvThietBi_CellValueChanged);
            this.grvThietBi.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvThietBi_CellValueChanging);
            // 
            // colMaTB
            // 
            this.colMaTB.Caption = "Thiết bị";
            this.colMaTB.FieldName = "MaLTS";
            this.colMaTB.Name = "colMaTB";
            this.colMaTB.Visible = true;
            this.colMaTB.VisibleIndex = 0;
            this.colMaTB.Width = 130;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Số lượng";
            this.gridColumn6.ColumnEdit = this.spinSoLuong;
            this.gridColumn6.FieldName = "SoLuong";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 57;
            // 
            // spinSoLuong
            // 
            this.spinSoLuong.AutoHeight = false;
            this.spinSoLuong.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoLuong.IsFloatValue = false;
            this.spinSoLuong.Mask.EditMask = "N00";
            this.spinSoLuong.Name = "spinSoLuong";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Đơn giá";
            this.gridColumn7.ColumnEdit = this.spinDonGia;
            this.gridColumn7.DisplayFormat.FormatString = "c0";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "DonGia";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 103;
            // 
            // spinDonGia
            // 
            this.spinDonGia.AutoHeight = false;
            this.spinDonGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia.Mask.EditMask = "c0";
            this.spinDonGia.Name = "spinDonGia";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Thành tiền";
            this.gridColumn9.DisplayFormat.FormatString = "c0";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "ThanhTien";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 111;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Diễn giải";
            this.gridColumn10.FieldName = "DienGiai";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 305;
            // 
            // colDMCV
            // 
            this.colDMCV.Caption = "Mã công việc bảo trì";
            this.colDMCV.FieldName = "MaCVBT";
            this.colDMCV.Name = "colDMCV";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageIndex = 1;
            this.btnSave.ImageOptions.ImageList = this.imageCollection1;
            this.btnSave.Location = new System.Drawing.Point(597, 302);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu nhận";
            this.btnSave.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.ImageIndex = 2;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(678, 302);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.ImageOptions.ImageIndex = 0;
            this.btnDeleteRow.ImageOptions.ImageList = this.imageCollection1;
            this.btnDeleteRow.Location = new System.Drawing.Point(516, 302);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRow.TabIndex = 12;
            this.btnDeleteRow.Text = "Xóa dòng";
            this.btnDeleteRow.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(1, "Save1.png");
            this.imageCollection1.Images.SetKeyName(2, "Cancel1.png");
            // 
            // frmUpdateEquipmentcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 330);
            this.Controls.Add(this.btnDeleteRow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gcThietBi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmUpdateEquipmentcs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thiết bị sử dụng";
            this.Load += new System.EventHandler(this.frmUpdateEquipmentcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcThietBi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvThietBi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcThietBi;
        private DevExpress.XtraGrid.Views.Grid.GridView grvThietBi;
        private DevExpress.XtraGrid.Columns.GridColumn colMaTB;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnDeleteRow;
        private DevExpress.XtraGrid.Columns.GridColumn colDMCV;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}