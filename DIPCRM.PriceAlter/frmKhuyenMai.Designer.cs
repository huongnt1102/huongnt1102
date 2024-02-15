namespace DIPCRM.PriceAlert
{
    partial class frmKhuyenMai
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
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.tpNoiDung = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcCongThuc = new DevExpress.XtraGrid.GridControl();
            this.gvCongThuc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkHinhThuc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcCongThucChiTiet = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkDVTChiTiet = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkHinhThucChiTiet = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.tpNoiDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCongThuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCongThuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkHinhThuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCongThucChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkDVTChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkHinhThucChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.tpNoiDung;
            this.xtraTabControl2.Size = new System.Drawing.Size(1002, 497);
            this.xtraTabControl2.TabIndex = 1;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpNoiDung});
            // 
            // tpNoiDung
            // 
            this.tpNoiDung.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tpNoiDung.Appearance.Header.Options.UseFont = true;
            this.tpNoiDung.Controls.Add(this.splitContainerControl3);
            this.tpNoiDung.Name = "tpNoiDung";
            this.tpNoiDung.Size = new System.Drawing.Size(996, 469);
            this.tpNoiDung.Text = "NỘI DUNG ÁP DỤNG";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.gcCongThuc);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(996, 469);
            this.splitContainerControl3.SplitterPosition = 653;
            this.splitContainerControl3.TabIndex = 3;
            this.splitContainerControl3.Text = "splitContainerControl3";
            // 
            // gcCongThuc
            // 
            this.gcCongThuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCongThuc.Location = new System.Drawing.Point(0, 0);
            this.gcCongThuc.MainView = this.gvCongThuc;
            this.gcCongThuc.Name = "gcCongThuc";
            this.gcCongThuc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit2,
            this.repositoryItemLookUpEdit1,
            this.lkHinhThuc});
            this.gcCongThuc.Size = new System.Drawing.Size(653, 469);
            this.gcCongThuc.TabIndex = 2;
            this.gcCongThuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCongThuc});
            // 
            // gvCongThuc
            // 
            this.gvCongThuc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18});
            this.gvCongThuc.GridControl = this.gcCongThuc;
            this.gvCongThuc.Name = "gvCongThuc";
            this.gvCongThuc.OptionsBehavior.Editable = false;
            this.gvCongThuc.OptionsDetail.EnableMasterViewMode = false;
            this.gvCongThuc.OptionsDetail.ShowDetailTabs = false;
            this.gvCongThuc.OptionsDetail.SmartDetailExpand = false;
            this.gvCongThuc.OptionsView.ColumnAutoWidth = false;
            this.gvCongThuc.OptionsView.ShowGroupPanel = false;
            this.gvCongThuc.DoubleClick += new System.EventHandler(this.gvLoaiGiaThue_DoubleClick);
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Số tháng";
            this.gridColumn11.ColumnEdit = this.repositoryItemSpinEdit2;
            this.gridColumn11.DisplayFormat.FormatString = "{0:n0} tháng";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "SoLuong";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            this.gridColumn11.Width = 93;
            // 
            // repositoryItemSpinEdit2
            // 
            this.repositoryItemSpinEdit2.AutoHeight = false;
            this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit2.DisplayFormat.FormatString = "{0:#,0.##} tháng";
            this.repositoryItemSpinEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit2.EditFormat.FormatString = "{0:#,0.##}";
            this.repositoryItemSpinEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "ĐVT";
            this.gridColumn13.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gridColumn13.FieldName = "MaDVT_From";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Width = 78;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDVT", "Đơn vị tính")});
            this.repositoryItemLookUpEdit1.DisplayMember = "TenDVT";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ValueMember = "ID";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Giá trị KM";
            this.gridColumn14.ColumnEdit = this.repositoryItemSpinEdit2;
            this.gridColumn14.FieldName = "SoLuong_To";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Width = 71;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "ĐVT KM";
            this.gridColumn15.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gridColumn15.FieldName = "MaDVT_To";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Width = 64;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Hình thức";
            this.gridColumn16.ColumnEdit = this.lkHinhThuc;
            this.gridColumn16.FieldName = "MaHinhThuc";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 1;
            this.gridColumn16.Width = 97;
            // 
            // lkHinhThuc
            // 
            this.lkHinhThuc.AutoHeight = false;
            this.lkHinhThuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkHinhThuc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHinhThuc", "Hình thức")});
            this.lkHinhThuc.DisplayMember = "TenHinhThuc";
            this.lkHinhThuc.Name = "lkHinhThuc";
            this.lkHinhThuc.NullText = "";
            this.lkHinhThuc.ValueMember = "ID";
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Hình thức KM";
            this.gridColumn17.ColumnEdit = this.lkHinhThuc;
            this.gridColumn17.FieldName = "MaHinhThuc_To";
            this.gridColumn17.Name = "gridColumn17";
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Chính sách";
            this.gridColumn18.FieldName = "TenChinhSach";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.OptionsColumn.ReadOnly = true;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 0;
            this.gridColumn18.Width = 372;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.gcCongThucChiTiet);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(338, 469);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "CHI TIẾT KHUYẾN MÃI";
            // 
            // gcCongThucChiTiet
            // 
            this.gcCongThucChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCongThucChiTiet.Location = new System.Drawing.Point(2, 21);
            this.gcCongThucChiTiet.MainView = this.gridView2;
            this.gcCongThucChiTiet.Name = "gcCongThucChiTiet";
            this.gcCongThucChiTiet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit3,
            this.lkDVTChiTiet,
            this.lkHinhThucChiTiet});
            this.gcCongThucChiTiet.Size = new System.Drawing.Size(334, 446);
            this.gcCongThucChiTiet.TabIndex = 3;
            this.gcCongThucChiTiet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn24});
            this.gridView2.GridControl = this.gcCongThucChiTiet;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView2.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Giá trị";
            this.gridColumn19.ColumnEdit = this.repositoryItemSpinEdit3;
            this.gridColumn19.DisplayFormat.FormatString = "{0:n0} tháng";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn19.FieldName = "SoLuong_From";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Width = 78;
            // 
            // repositoryItemSpinEdit3
            // 
            this.repositoryItemSpinEdit3.AutoHeight = false;
            this.repositoryItemSpinEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit3.DisplayFormat.FormatString = "{0:#,0.##}";
            this.repositoryItemSpinEdit3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit3.EditFormat.FormatString = "{0:#,0.##}";
            this.repositoryItemSpinEdit3.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit3.Name = "repositoryItemSpinEdit3";
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "ĐVT";
            this.gridColumn20.ColumnEdit = this.lkDVTChiTiet;
            this.gridColumn20.FieldName = "MaDVT_From";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Width = 78;
            // 
            // lkDVTChiTiet
            // 
            this.lkDVTChiTiet.AutoHeight = false;
            this.lkDVTChiTiet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkDVTChiTiet.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDVT", "Đơn vị tính")});
            this.lkDVTChiTiet.DisplayMember = "TenDVT";
            this.lkDVTChiTiet.Name = "lkDVTChiTiet";
            this.lkDVTChiTiet.NullText = "";
            this.lkDVTChiTiet.ValueMember = "ID";
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Giá trị KM";
            this.gridColumn21.ColumnEdit = this.repositoryItemSpinEdit3;
            this.gridColumn21.FieldName = "SoLuong";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 0;
            this.gridColumn21.Width = 81;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "ĐVT KM";
            this.gridColumn22.ColumnEdit = this.lkDVTChiTiet;
            this.gridColumn22.FieldName = "MaDVT";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 1;
            this.gridColumn22.Width = 203;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "Hình thức";
            this.gridColumn23.ColumnEdit = this.lkHinhThucChiTiet;
            this.gridColumn23.FieldName = "MaHinhThuc_From";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Width = 97;
            // 
            // lkHinhThucChiTiet
            // 
            this.lkHinhThucChiTiet.AutoHeight = false;
            this.lkHinhThucChiTiet.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkHinhThucChiTiet.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenHinhThuc", "Hình thức")});
            this.lkHinhThucChiTiet.DisplayMember = "TenHinhThuc";
            this.lkHinhThucChiTiet.Name = "lkHinhThucChiTiet";
            this.lkHinhThucChiTiet.NullText = "";
            this.lkHinhThucChiTiet.ValueMember = "ID";
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "Hình thức KM";
            this.gridColumn24.ColumnEdit = this.lkHinhThucChiTiet;
            this.gridColumn24.FieldName = "MaHinhThuc_To";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Width = 74;
            // 
            // frmKhuyenMai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 497);
            this.Controls.Add(this.xtraTabControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmKhuyenMai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chính sách giá";
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.tpNoiDung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCongThuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCongThuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkHinhThuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCongThucChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkDVTChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkHinhThucChiTiet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage tpNoiDung;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraGrid.GridControl gcCongThuc;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCongThuc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkHinhThuc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.GridControl gcCongThucChiTiet;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkDVTChiTiet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkHinhThucChiTiet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraEditors.GroupControl groupControl1;


    }
}