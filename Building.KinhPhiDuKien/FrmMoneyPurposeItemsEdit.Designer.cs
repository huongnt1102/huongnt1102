namespace Building.KinhPhiDuKien
{
    partial class FrmMoneyPurposeItemsEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMoneyPurposeItemsEdit));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.txtMoneyPurpose = new DevExpress.XtraEditors.TextEdit();
            this.spinMoneyPurposeItems = new DevExpress.XtraEditors.SpinEdit();
            this.memoName = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemHuongDan = new DevExpress.XtraBars.BarButtonItem();
            this.itemClearText = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneyPurpose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMoneyPurposeItems.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
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
            this.itemSave,
            this.itemCancel,
            this.itemHuongDan,
            this.itemClearText});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemCancel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemHuongDan, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClearText, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 0;
            this.itemSave.ImageOptions.ImageIndex = 0;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemSave_ItemClick);
            // 
            // itemCancel
            // 
            this.itemCancel.Caption = "Hủy";
            this.itemCancel.Id = 1;
            this.itemCancel.ImageOptions.ImageIndex = 1;
            this.itemCancel.Name = "itemCancel";
            this.itemCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemCancel_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(544, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 265);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(544, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 234);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(544, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 234);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            this.imageCollection1.Images.SetKeyName(2, "icons8_Agreement_Delete_50px.png");
            this.imageCollection1.Images.SetKeyName(3, "Open2.png");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtNo);
            this.layoutControl1.Controls.Add(this.txtMoneyPurpose);
            this.layoutControl1.Controls.Add(this.spinMoneyPurposeItems);
            this.layoutControl1.Controls.Add(this.memoName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(544, 234);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtNo
            // 
            this.txtNo.EditValue = "HM01";
            this.txtNo.Location = new System.Drawing.Point(89, 36);
            this.txtNo.MenuManager = this.barManager1;
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(443, 20);
            this.txtNo.StyleController = this.layoutControl1;
            this.txtNo.TabIndex = 1;
            this.txtNo.Tag = "Nhập mã hạng mục";
            // 
            // txtMoneyPurpose
            // 
            this.txtMoneyPurpose.Location = new System.Drawing.Point(89, 12);
            this.txtMoneyPurpose.MenuManager = this.barManager1;
            this.txtMoneyPurpose.Name = "txtMoneyPurpose";
            this.txtMoneyPurpose.Properties.ReadOnly = true;
            this.txtMoneyPurpose.Size = new System.Drawing.Size(443, 20);
            this.txtMoneyPurpose.StyleController = this.layoutControl1;
            this.txtMoneyPurpose.TabIndex = 0;
            this.txtMoneyPurpose.Tag = "Kiểm tra số tiền đang có";
            // 
            // spinMoneyPurposeItems
            // 
            this.spinMoneyPurposeItems.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinMoneyPurposeItems.Location = new System.Drawing.Point(89, 202);
            this.spinMoneyPurposeItems.MenuManager = this.barManager1;
            this.spinMoneyPurposeItems.Name = "spinMoneyPurposeItems";
            this.spinMoneyPurposeItems.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinMoneyPurposeItems.Properties.DisplayFormat.FormatString = "n0";
            this.spinMoneyPurposeItems.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMoneyPurposeItems.Properties.EditFormat.FormatString = "n0";
            this.spinMoneyPurposeItems.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMoneyPurposeItems.Size = new System.Drawing.Size(443, 20);
            this.spinMoneyPurposeItems.StyleController = this.layoutControl1;
            this.spinMoneyPurposeItems.TabIndex = 3;
            this.spinMoneyPurposeItems.Tag = "Nhập số tiền";
            // 
            // memoName
            // 
            this.memoName.EditValue = "Hạng mục 1";
            this.memoName.Location = new System.Drawing.Point(89, 60);
            this.memoName.MenuManager = this.barManager1;
            this.memoName.Name = "memoName";
            this.memoName.Size = new System.Drawing.Size(443, 138);
            this.memoName.StyleController = this.layoutControl1;
            this.memoName.TabIndex = 2;
            this.memoName.Tag = "Nhập tên hạng mục";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(544, 234);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.memoName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(524, 142);
            this.layoutControlItem1.Text = "Hạng mục";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(74, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinMoneyPurposeItems;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(524, 24);
            this.layoutControlItem2.Text = "Số tiền";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(74, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtMoneyPurpose;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(524, 24);
            this.layoutControlItem4.Text = "Số tiền đang có";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(74, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtNo;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(524, 24);
            this.layoutControlItem3.Text = "Mã hạng mục";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(74, 13);
            // 
            // itemHuongDan
            // 
            this.itemHuongDan.Caption = "Hướng dẫn";
            this.itemHuongDan.Id = 2;
            this.itemHuongDan.ImageOptions.ImageIndex = 3;
            this.itemHuongDan.Name = "itemHuongDan";
            // 
            // itemClearText
            // 
            this.itemClearText.Caption = "Clear text";
            this.itemClearText.Id = 3;
            this.itemClearText.ImageOptions.ImageIndex = 2;
            this.itemClearText.Name = "itemClearText";
            // 
            // FrmMoneyPurposeItemsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 265);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmMoneyPurposeItemsEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm kinh phí hạng mục";
            this.Load += new System.EventHandler(this.FrmMoneyPurposeItemsEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMoneyPurpose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMoneyPurposeItems.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemCancel;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtMoneyPurpose;
        private DevExpress.XtraEditors.SpinEdit spinMoneyPurposeItems;
        private DevExpress.XtraEditors.MemoEdit memoName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarButtonItem itemHuongDan;
        private DevExpress.XtraBars.BarButtonItem itemClearText;
    }
}