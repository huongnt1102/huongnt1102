namespace DIPCRM.PriceAlert
{
    partial class frmGiaThue
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
            this.gcGiaThue = new DevExpress.XtraGrid.GridControl();
            this.gvLoaiGiaThue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.coltenloaigia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldongia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinDonGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colloaitien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiTien = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkDVTGiaThue = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcGiaThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiGiaThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkDVTGiaThue)).BeginInit();
            this.SuspendLayout();
            // 
            // gcGiaThue
            // 
            this.gcGiaThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGiaThue.Location = new System.Drawing.Point(0, 0);
            this.gcGiaThue.MainView = this.gvLoaiGiaThue;
            this.gcGiaThue.Name = "gcGiaThue";
            this.gcGiaThue.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkLoaiTien,
            this.spinDonGia,
            this.lkDVTGiaThue});
            this.gcGiaThue.Size = new System.Drawing.Size(657, 273);
            this.gcGiaThue.TabIndex = 3;
            this.gcGiaThue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoaiGiaThue});
            // 
            // gvLoaiGiaThue
            // 
            this.gvLoaiGiaThue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.coltenloaigia,
            this.coldongia,
            this.colloaitien,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn22,
            this.gridColumn1});
            this.gvLoaiGiaThue.GridControl = this.gcGiaThue;
            this.gvLoaiGiaThue.Name = "gvLoaiGiaThue";
            this.gvLoaiGiaThue.OptionsBehavior.Editable = false;
            this.gvLoaiGiaThue.OptionsDetail.EnableMasterViewMode = false;
            this.gvLoaiGiaThue.OptionsSelection.MultiSelect = true;
            this.gvLoaiGiaThue.OptionsView.ColumnAutoWidth = false;
            this.gvLoaiGiaThue.OptionsView.ShowGroupPanel = false;
            this.gvLoaiGiaThue.DoubleClick += new System.EventHandler(this.gvLoaiGiaThue_DoubleClick);
            // 
            // coltenloaigia
            // 
            this.coltenloaigia.Caption = "Tên Loại Giá";
            this.coltenloaigia.FieldName = "TenLG";
            this.coltenloaigia.Name = "coltenloaigia";
            this.coltenloaigia.Width = 118;
            // 
            // coldongia
            // 
            this.coldongia.Caption = "Đơn Giá";
            this.coldongia.ColumnEdit = this.spinDonGia;
            this.coldongia.DisplayFormat.FormatString = "{0:#,0.####}";
            this.coldongia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.coldongia.FieldName = "DonGia";
            this.coldongia.Name = "coldongia";
            this.coldongia.Visible = true;
            this.coldongia.VisibleIndex = 1;
            this.coldongia.Width = 102;
            // 
            // spinDonGia
            // 
            this.spinDonGia.AutoHeight = false;
            this.spinDonGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia.Name = "spinDonGia";
            // 
            // colloaitien
            // 
            this.colloaitien.Caption = "Loại Tiền";
            this.colloaitien.FieldName = "TenLT";
            this.colloaitien.Name = "colloaitien";
            this.colloaitien.Visible = true;
            this.colloaitien.VisibleIndex = 2;
            this.colloaitien.Width = 61;
            // 
            // lkLoaiTien
            // 
            this.lkLoaiTien.AutoHeight = false;
            this.lkLoaiTien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiTien.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KyHieuLT", "Name7")});
            this.lkLoaiTien.DisplayMember = "KyHieuLT";
            this.lkLoaiTien.Name = "lkLoaiTien";
            this.lkLoaiTien.NullText = "";
            this.lkLoaiTien.ShowHeader = false;
            this.lkLoaiTien.ValueMember = "ID";
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Diễn Giải";
            this.gridColumn18.FieldName = "DienGiai";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Width = 331;
            // 
            // gridColumn19
            // 
            this.gridColumn19.FieldName = "MaTN";
            this.gridColumn19.Name = "gridColumn19";
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "Đơn vị tính";
            this.gridColumn22.FieldName = "TenDVT";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 3;
            this.gridColumn22.Width = 72;
            // 
            // lkDVTGiaThue
            // 
            this.lkDVTGiaThue.AutoHeight = false;
            this.lkDVTGiaThue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkDVTGiaThue.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDVT", "Đơn vị tính")});
            this.lkDVTGiaThue.DisplayMember = "TenDVT";
            this.lkDVTGiaThue.Name = "lkDVTGiaThue";
            this.lkDVTGiaThue.NullText = "";
            this.lkDVTGiaThue.ValueMember = "ID";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên chính sách";
            this.gridColumn1.FieldName = "TenChinhSach";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 398;
            // 
            // frmGiaThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 273);
            this.Controls.Add(this.gcGiaThue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGiaThue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chính sách giá";
            ((System.ComponentModel.ISupportInitialize)(this.gcGiaThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiGiaThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkDVTGiaThue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcGiaThue;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoaiGiaThue;
        private DevExpress.XtraGrid.Columns.GridColumn coltenloaigia;
        private DevExpress.XtraGrid.Columns.GridColumn coldongia;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colloaitien;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiTien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkDVTGiaThue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;

    }
}