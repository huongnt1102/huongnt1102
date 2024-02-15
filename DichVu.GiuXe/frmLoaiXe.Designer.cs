namespace DichVu.GiuXe
{
    partial class frmLoaiXe
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoaiXe));
            this.gcLoaiXe = new DevExpress.XtraGrid.GridControl();
            this.grvLoaiXe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkNhomXe = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkLoaiXe_CPK = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinNumber = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemToaNha = new DevExpress.XtraBars.BarEditItem();
            this.lkToaNha = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemNap = new DevExpress.XtraBars.BarButtonItem();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiXe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLoaiXe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhomXe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiXe_CPK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLoaiXe
            // 
            this.gcLoaiXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLoaiXe.Location = new System.Drawing.Point(0, 31);
            this.gcLoaiXe.MainView = this.grvLoaiXe;
            this.gcLoaiXe.Name = "gcLoaiXe";
            this.gcLoaiXe.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinNumber,
            this.lkNhomXe,
            this.lkLoaiXe_CPK});
            this.gcLoaiXe.ShowOnlyPredefinedDetails = true;
            this.gcLoaiXe.Size = new System.Drawing.Size(882, 436);
            this.gcLoaiXe.TabIndex = 0;
            this.gcLoaiXe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvLoaiXe});
            // 
            // grvLoaiXe
            // 
            this.grvLoaiXe.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn11,
            this.gridColumn4,
            this.gridColumn5});
            this.grvLoaiXe.GridControl = this.gcLoaiXe;
            this.grvLoaiXe.Name = "grvLoaiXe";
            this.grvLoaiXe.OptionsSelection.MultiSelect = true;
            this.grvLoaiXe.OptionsView.ColumnAutoWidth = false;
            this.grvLoaiXe.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvLoaiXe.OptionsView.ShowGroupPanel = false;
            this.grvLoaiXe.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvLoaiXe_InitNewRow);
            this.grvLoaiXe.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvLoaiXe_KeyUp);
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "MaTN";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "STT";
            this.gridColumn3.FieldName = "STT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 43;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên loại xe";
            this.gridColumn1.FieldName = "TenLX";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 203;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Nhóm xe";
            this.gridColumn11.ColumnEdit = this.lkNhomXe;
            this.gridColumn11.FieldName = "MaNX";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Width = 105;
            // 
            // lkNhomXe
            // 
            this.lkNhomXe.AutoHeight = false;
            this.lkNhomXe.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkNhomXe.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNX", "Name1")});
            this.lkNhomXe.DisplayMember = "TenNX";
            this.lkNhomXe.Name = "lkNhomXe";
            this.lkNhomXe.NullText = "";
            this.lkNhomXe.ShowHeader = false;
            this.lkNhomXe.ValueMember = "ID";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Loại xe CPK";
            this.gridColumn4.ColumnEdit = this.lkLoaiXe_CPK;
            this.gridColumn4.FieldName = "TenLoaiXe_CPK";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 100;
            // 
            // lkLoaiXe_CPK
            // 
            this.lkLoaiXe_CPK.AutoHeight = false;
            this.lkLoaiXe_CPK.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiXe_CPK.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Loại xe CPK")});
            this.lkLoaiXe_CPK.DisplayMember = "Name";
            this.lkLoaiXe_CPK.Name = "lkLoaiXe_CPK";
            this.lkLoaiXe_CPK.NullText = "";
            this.lkLoaiXe_CPK.ValueMember = "Name";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mã dịch vụ";
            this.gridColumn5.FieldName = "MaDichVu";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 163;
            // 
            // spinNumber
            // 
            this.spinNumber.AutoHeight = false;
            this.spinNumber.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNumber.DisplayFormat.FormatString = "c0";
            this.spinNumber.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNumber.Mask.EditMask = "c0";
            this.spinNumber.Name = "spinNumber";
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
            this.itemLuu,
            this.itemNap,
            this.itemToaNha,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 9;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkToaNha});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemToaNha, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemNap, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemToaNha
            // 
            this.itemToaNha.Edit = this.lkToaNha;
            this.itemToaNha.EditWidth = 156;
            this.itemToaNha.Id = 6;
            this.itemToaNha.Name = "itemToaNha";
            this.itemToaNha.EditValueChanged += new System.EventHandler(this.itemToaNha_EditValueChanged);
            // 
            // lkToaNha
            // 
            this.lkToaNha.AutoHeight = false;
            this.lkToaNha.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkToaNha.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lkToaNha.DisplayMember = "TenTN";
            this.lkToaNha.Name = "lkToaNha";
            this.lkToaNha.NullText = "[Chọn Dự án]";
            this.lkToaNha.ShowHeader = false;
            this.lkToaNha.ValueMember = "MaTN";
            // 
            // itemNap
            // 
            this.itemNap.Caption = "Nạp";
            this.itemNap.Id = 3;
            this.itemNap.ImageOptions.ImageIndex = 0;
            this.itemNap.Name = "itemNap";
            this.itemNap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemNap_ItemClick);
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 2;
            this.itemLuu.ImageOptions.ImageIndex = 1;
            this.itemLuu.Name = "itemLuu";
            this.itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLuu_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(882, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 467);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(882, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 436);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(882, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 436);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Refresh1.png");
            this.imageCollection1.Images.SetKeyName(1, "Save1.png");
            this.imageCollection1.Images.SetKeyName(2, "Import1.png");
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Import";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.ImageOptions.ImageIndex = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // frmLoaiXe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 467);
            this.Controls.Add(this.gcLoaiXe);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmLoaiXe";
            this.Text = "Loại xe";
            this.Load += new System.EventHandler(this.frmLoaiMatBang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcLoaiXe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLoaiXe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhomXe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiXe_CPK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkToaNha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcLoaiXe;
        private DevExpress.XtraGrid.Views.Grid.GridView grvLoaiXe;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemLuu;
        private DevExpress.XtraBars.BarButtonItem itemNap;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinNumber;
        private DevExpress.XtraBars.BarEditItem itemToaNha;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkToaNha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkNhomXe;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkLoaiXe_CPK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}