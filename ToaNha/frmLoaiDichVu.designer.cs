namespace ToaNha
{
    partial class frmLoaiDichVu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoaiDichVu));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.itemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.itemAddChild = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.itemView = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn13 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn14 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn15 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn16 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCTDG = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.TableName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn11 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn12 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeField = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.HideBarsWhenMerging = false;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemAdd,
            this.itemAddChild,
            this.itemRefresh,
            this.itemSave,
            this.itemClose,
            this.itemDelete,
            this.itemView});
            this.barManager1.MaxItemId = 7;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAddChild, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemView, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemRefresh
            // 
            this.itemRefresh.Caption = "Nạp";
            this.itemRefresh.Id = 2;
            this.itemRefresh.ImageOptions.ImageIndex = 0;
            this.itemRefresh.Name = "itemRefresh";
            // 
            // itemAdd
            // 
            this.itemAdd.Caption = "Thêm";
            this.itemAdd.Id = 0;
            this.itemAdd.ImageOptions.ImageIndex = 1;
            this.itemAdd.Name = "itemAdd";
            // 
            // itemAddChild
            // 
            this.itemAddChild.Caption = "Thêm cấp";
            this.itemAddChild.Id = 1;
            this.itemAddChild.ImageOptions.ImageIndex = 1;
            this.itemAddChild.Name = "itemAddChild";
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 5;
            this.itemDelete.ImageOptions.ImageIndex = 2;
            this.itemDelete.Name = "itemDelete";
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 3;
            this.itemSave.ImageOptions.ImageIndex = 3;
            this.itemSave.Name = "itemSave";
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Đóng";
            this.itemClose.Id = 4;
            this.itemClose.ImageOptions.ImageIndex = 4;
            this.itemClose.Name = "itemClose";
            // 
            // itemView
            // 
            this.itemView.Caption = "Xem";
            this.itemView.Id = 6;
            this.itemView.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("itemView.ImageOptions.Image")));
            this.itemView.Name = "itemView";
            this.itemView.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.itemView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemView_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(724, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 489);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(724, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 458);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(724, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 458);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Add1.png");
            this.imageCollection1.Images.SetKeyName(2, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(3, "Save1.png");
            this.imageCollection1.Images.SetKeyName(4, "Cancel1.png");
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(724, 458);
            this.splitContainerControl1.SplitterPosition = 1556;
            this.splitContainerControl1.TabIndex = 14;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn13,
            this.treeListColumn14,
            this.treeListColumn15,
            this.treeListColumn16,
            this.treeListColumn4,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn6,
            this.ID,
            this.treeListColumn7,
            this.colCTDG,
            this.TableName,
            this.ParentID,
            this.treeListColumn5,
            this.treeListColumn8,
            this.treeListColumn11,
            this.treeListColumn12});
            this.treeList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.treeList1.LookAndFeel.UseWindowsXPTheme = true;
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.PopulateServiceColumns = true;
            this.treeList1.OptionsView.AutoWidth = false;
            this.treeList1.Size = new System.Drawing.Size(719, 458);
            this.treeList1.TabIndex = 10;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Tên loại dịch vụ";
            this.treeListColumn1.FieldName = "TenLDV";
            this.treeListColumn1.MinWidth = 52;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 198;
            // 
            // treeListColumn13
            // 
            this.treeListColumn13.Caption = "Mã";
            this.treeListColumn13.FieldName = "TenVT";
            this.treeListColumn13.Name = "treeListColumn13";
            this.treeListColumn13.Visible = true;
            this.treeListColumn13.VisibleIndex = 1;
            // 
            // treeListColumn14
            // 
            this.treeListColumn14.Caption = "Loại phiếu";
            this.treeListColumn14.FieldName = "MaLoaiPhieu";
            this.treeListColumn14.Name = "treeListColumn14";
            this.treeListColumn14.Visible = true;
            this.treeListColumn14.VisibleIndex = 2;
            // 
            // treeListColumn15
            // 
            this.treeListColumn15.Caption = "Mã cư dân";
            this.treeListColumn15.FieldName = "MaCuDan";
            this.treeListColumn15.Name = "treeListColumn15";
            this.treeListColumn15.Visible = true;
            this.treeListColumn15.VisibleIndex = 3;
            // 
            // treeListColumn16
            // 
            this.treeListColumn16.Caption = "Mã cho thuê";
            this.treeListColumn16.FieldName = "MaKhachThue";
            this.treeListColumn16.Name = "treeListColumn16";
            this.treeListColumn16.Visible = true;
            this.treeListColumn16.VisibleIndex = 4;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "STT";
            this.treeListColumn4.FieldName = "STT";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 5;
            this.treeListColumn4.Width = 58;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Tên hiển thị";
            this.treeListColumn2.FieldName = "TenHienThi";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 6;
            this.treeListColumn2.Width = 233;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Tên tiếng anh";
            this.treeListColumn3.FieldName = "TenTA";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 7;
            this.treeListColumn3.Width = 239;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Cơ bản";
            this.treeListColumn6.FieldName = "IsCoBan";
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 9;
            this.treeListColumn6.Width = 61;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Hạn thanh toán";
            this.treeListColumn7.FieldName = "NgayHanTT";
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 8;
            this.treeListColumn7.Width = 113;
            // 
            // colCTDG
            // 
            this.colCTDG.Caption = "Cấu hình diễn giải";
            this.colCTDG.FieldName = "CauTrucDienGiai";
            this.colCTDG.Name = "colCTDG";
            this.colCTDG.Visible = true;
            this.colCTDG.VisibleIndex = 10;
            this.colCTDG.Width = 409;
            // 
            // TableName
            // 
            this.TableName.Caption = "TableName";
            this.TableName.FieldName = "TableName";
            this.TableName.Name = "TableName";
            // 
            // ParentID
            // 
            this.ParentID.Caption = "treeListColumn5";
            this.ParentID.FieldName = "ParentID";
            this.ParentID.Name = "ParentID";
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Tính lãi";
            this.treeListColumn5.FieldName = "IS_TIEN_LAI";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 11;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.Caption = "Cost Center";
            this.treeListColumn8.FieldName = "CostCenter";
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 12;
            // 
            // treeListColumn11
            // 
            this.treeListColumn11.Caption = "Profit Center";
            this.treeListColumn11.FieldName = "ProfitCenter";
            this.treeListColumn11.Name = "treeListColumn11";
            this.treeListColumn11.Visible = true;
            this.treeListColumn11.VisibleIndex = 13;
            // 
            // treeListColumn12
            // 
            this.treeListColumn12.Caption = "Cài đặt dịch vụ trên app";
            this.treeListColumn12.FieldName = "IsConfigDateApp";
            this.treeListColumn12.Name = "treeListColumn12";
            this.treeListColumn12.Visible = true;
            this.treeListColumn12.VisibleIndex = 14;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.treeField);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(0, 0);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Danh sách các trường trộn";
            // 
            // treeField
            // 
            this.treeField.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn9,
            this.treeListColumn10});
            this.treeField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeField.Location = new System.Drawing.Point(0, 19);
            this.treeField.Name = "treeField";
            this.treeField.OptionsView.AutoWidth = false;
            this.treeField.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.treeField.Size = new System.Drawing.Size(0, 0);
            this.treeField.TabIndex = 0;
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn9.AppearanceCell.Options.UseFont = true;
            this.treeListColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.treeListColumn9.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn9.Caption = "Ký hiệu";
            this.treeListColumn9.ColumnEdit = this.repositoryItemButtonEdit1;
            this.treeListColumn9.FieldName = "FieldName";
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.OptionsColumn.ReadOnly = true;
            this.treeListColumn9.Visible = true;
            this.treeListColumn9.VisibleIndex = 0;
            this.treeListColumn9.Width = 135;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.Caption = "Diễn giải";
            this.treeListColumn10.FieldName = "Value";
            this.treeListColumn10.Name = "treeListColumn10";
            this.treeListColumn10.OptionsColumn.AllowEdit = false;
            this.treeListColumn10.OptionsColumn.ReadOnly = true;
            this.treeListColumn10.Visible = true;
            this.treeListColumn10.VisibleIndex = 1;
            this.treeListColumn10.Width = 431;
            // 
            // frmLoaiDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 489);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoaiDichVu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loại dịch vụ";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemRefresh;
        private DevExpress.XtraBars.BarButtonItem itemAdd;
        private DevExpress.XtraBars.BarButtonItem itemAddChild;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCTDG;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTreeList.TreeList treeField;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private DevExpress.XtraBars.BarButtonItem itemView;
        private DevExpress.XtraTreeList.Columns.TreeListColumn TableName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn11;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn12;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn13;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn14;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn15;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn16;
    }
}