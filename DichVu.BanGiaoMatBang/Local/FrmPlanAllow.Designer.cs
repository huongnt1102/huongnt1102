namespace DichVu.BanGiaoMatBang.Local
{
    partial class FrmPlanAllow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlanAllow));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemAllow = new DevExpress.XtraBars.BarButtonItem();
            this.itemNotAllow = new DevExpress.XtraBars.BarButtonItem();
            this.itemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gcSchedule = new DevExpress.XtraGrid.GridControl();
            this.gvSchedule = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcScheduleApartment = new DevExpress.XtraGrid.GridControl();
            this.gvScheduleApartment = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.memo = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemMemo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSplit = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcScheduleApartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvScheduleApartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemAllow,
            this.itemCancel,
            this.itemNotAllow});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemAllow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNotAllow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemCancel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemAllow
            // 
            this.itemAllow.Caption = "Đồng ý";
            this.itemAllow.Id = 0;
            this.itemAllow.ImageOptions.ImageIndex = 0;
            this.itemAllow.Name = "itemAllow";
            this.itemAllow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemAllow_ItemClick);
            // 
            // itemNotAllow
            // 
            this.itemNotAllow.Caption = "Không đồng ý";
            this.itemNotAllow.Id = 2;
            this.itemNotAllow.ImageOptions.ImageIndex = 1;
            this.itemNotAllow.Name = "itemNotAllow";
            this.itemNotAllow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemNotAllow_ItemClick);
            // 
            // itemCancel
            // 
            this.itemCancel.Caption = "Thoát";
            this.itemCancel.Id = 1;
            this.itemCancel.ImageOptions.ImageIndex = 2;
            this.itemCancel.Name = "itemCancel";
            this.itemCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemCancel_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(851, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 586);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(851, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 555);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(851, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 555);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "DuyetYes.png");
            this.imageCollection1.Images.SetKeyName(1, "DuyetNo.png");
            this.imageCollection1.Images.SetKeyName(2, "Cancel1.png");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.splitContainer1);
            this.layoutControl1.Controls.Add(this.memo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(851, 555);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gcSchedule);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gcScheduleApartment);
            this.splitContainer1.Size = new System.Drawing.Size(827, 496);
            this.splitContainer1.SplitterDistance = 275;
            this.splitContainer1.TabIndex = 5;
            // 
            // gcSchedule
            // 
            this.gcSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSchedule.Location = new System.Drawing.Point(0, 0);
            this.gcSchedule.MainView = this.gvSchedule;
            this.gcSchedule.MenuManager = this.barManager1;
            this.gcSchedule.Name = "gcSchedule";
            this.gcSchedule.ShowOnlyPredefinedDetails = true;
            this.gcSchedule.Size = new System.Drawing.Size(275, 496);
            this.gcSchedule.TabIndex = 0;
            this.gcSchedule.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSchedule});
            // 
            // gvSchedule
            // 
            this.gvSchedule.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gvSchedule.GridControl = this.gcSchedule;
            this.gvSchedule.GroupCount = 1;
            this.gvSchedule.Name = "gvSchedule";
            this.gvSchedule.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvSchedule.OptionsBehavior.Editable = false;
            this.gvSchedule.OptionsBehavior.ReadOnly = true;
            this.gvSchedule.OptionsView.ColumnAutoWidth = false;
            this.gvSchedule.OptionsView.ShowAutoFilterRow = true;
            this.gvSchedule.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvSchedule.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GvSchedule_RowClick);
            this.gvSchedule.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GvSchedule_CustomDrawRowIndicator);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nhóm lịch";
            this.gridColumn1.FieldName = "ScheduleGroupName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 188;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Lịch";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 239;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Từ ngày";
            this.gridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "DateHandoverFrom";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 160;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Đến ngày";
            this.gridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "DateHandoverTo";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 171;
            // 
            // gcScheduleApartment
            // 
            this.gcScheduleApartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcScheduleApartment.Location = new System.Drawing.Point(0, 0);
            this.gcScheduleApartment.MainView = this.gvScheduleApartment;
            this.gcScheduleApartment.MenuManager = this.barManager1;
            this.gcScheduleApartment.Name = "gcScheduleApartment";
            this.gcScheduleApartment.ShowOnlyPredefinedDetails = true;
            this.gcScheduleApartment.Size = new System.Drawing.Size(548, 496);
            this.gcScheduleApartment.TabIndex = 0;
            this.gcScheduleApartment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvScheduleApartment});
            // 
            // gvScheduleApartment
            // 
            this.gvScheduleApartment.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gvScheduleApartment.GridControl = this.gcScheduleApartment;
            this.gvScheduleApartment.GroupCount = 1;
            this.gvScheduleApartment.Name = "gvScheduleApartment";
            this.gvScheduleApartment.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvScheduleApartment.OptionsBehavior.Editable = false;
            this.gvScheduleApartment.OptionsBehavior.ReadOnly = true;
            this.gvScheduleApartment.OptionsView.ColumnAutoWidth = false;
            this.gvScheduleApartment.OptionsView.ShowAutoFilterRow = true;
            this.gvScheduleApartment.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn10, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn12, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvScheduleApartment.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.GvScheduleApartment_CustomDrawRowIndicator);
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Ngày bàn giao";
            this.gridColumn10.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn10.FieldName = "DateHandoverFrom";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 137;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Kết thúc";
            this.gridColumn11.DisplayFormat.FormatString = "HH:mm | dd/MM/yyyy";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn11.FieldName = "DateHandoverTo";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Width = 123;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Ca bàn giao";
            this.gridColumn12.FieldName = "DutyName";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            this.gridColumn12.Width = 110;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mặt bằng";
            this.gridColumn5.FieldName = "ApartmentName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 202;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Khách hàng";
            this.gridColumn6.FieldName = "CustomerName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Width = 207;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Mẫu checklist";
            this.gridColumn7.FieldName = "BuildingChecklistName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 193;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Nhân viên";
            this.gridColumn8.FieldName = "UserName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 166;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Thời gian thông báo";
            this.gridColumn9.DisplayFormat.FormatString = "{0:#,0.##} Ngày";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "DateNumberNotification";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 108;
            // 
            // memo
            // 
            this.memo.Location = new System.Drawing.Point(99, 12);
            this.memo.MenuManager = this.barManager1;
            this.memo.Name = "memo";
            this.memo.Size = new System.Drawing.Size(740, 31);
            this.memo.StyleController = this.layoutControl1;
            this.memo.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemMemo,
            this.layoutControlItemSplit});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(851, 555);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Control = this.memo;
            this.layoutControlItemMemo.CustomizationFormText = "Đề xuất, ghi chú:";
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemMemo.Name = "layoutControlItemMemo";
            this.layoutControlItemMemo.Size = new System.Drawing.Size(831, 35);
            this.layoutControlItemMemo.Text = "Đề xuất, ghi chú:";
            this.layoutControlItemMemo.TextSize = new System.Drawing.Size(84, 13);
            // 
            // layoutControlItemSplit
            // 
            this.layoutControlItemSplit.Control = this.splitContainer1;
            this.layoutControlItemSplit.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItemSplit.Name = "layoutControlItemSplit";
            this.layoutControlItemSplit.Size = new System.Drawing.Size(831, 500);
            this.layoutControlItemSplit.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSplit.TextVisible = false;
            // 
            // FrmPlanAllow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 586);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmPlanAllow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xác nhận kế hoạch bàn giao";
            this.Load += new System.EventHandler(this.FrmPlanAllow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcScheduleApartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvScheduleApartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSplit)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem itemAllow;
        private DevExpress.XtraBars.BarButtonItem itemCancel;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gcSchedule;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSchedule;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.GridControl gcScheduleApartment;
        private DevExpress.XtraGrid.Views.Grid.GridView gvScheduleApartment;
        private DevExpress.XtraEditors.MemoEdit memo;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMemo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSplit;
        private DevExpress.XtraBars.BarButtonItem itemNotAllow;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
    }
}