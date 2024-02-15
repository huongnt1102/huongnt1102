
namespace DichVu.KhoaSo.ClosingEntry
{
    partial class frmAddAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddAll));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemLuu = new DevExpress.XtraBars.BarButtonItem();
            this.itemHuy = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkSerive = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.dateDateTo = new DevExpress.XtraEditors.DateEdit();
            this.dateDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutDateFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSerice = new DevExpress.XtraLayout.LayoutControlItem();
            this.lkTower = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutTower = new DevExpress.XtraLayout.LayoutControlItem();
            this.cbxPeriod = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutPeriod = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSerive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSerice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTower.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPeriod)).BeginInit();
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
            this.itemLuu,
            this.itemHuy});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemLuu, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemHuy, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemLuu
            // 
            this.itemLuu.Caption = "Lưu";
            this.itemLuu.Id = 0;
            this.itemLuu.ImageOptions.ImageIndex = 0;
            this.itemLuu.Name = "itemLuu";
            this.itemLuu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemLuu_ItemClick);
            // 
            // itemHuy
            // 
            this.itemHuy.Caption = "Hủy";
            this.itemHuy.Id = 1;
            this.itemHuy.ImageOptions.ImageIndex = 1;
            this.itemHuy.Name = "itemHuy";
            this.itemHuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemHuy_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(523, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 182);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(523, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 151);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(523, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 151);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cbxPeriod);
            this.layoutControl1.Controls.Add(this.lkTower);
            this.layoutControl1.Controls.Add(this.chkSerive);
            this.layoutControl1.Controls.Add(this.dateDateTo);
            this.layoutControl1.Controls.Add(this.dateDateFrom);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(523, 151);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkSerive
            // 
            this.chkSerive.Location = new System.Drawing.Point(68, 108);
            this.chkSerive.MenuManager = this.barManager1;
            this.chkSerive.Name = "chkSerive";
            this.chkSerive.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkSerive.Properties.DisplayMember = "TableName";
            this.chkSerive.Properties.ValueMember = "Id";
            this.chkSerive.Size = new System.Drawing.Size(443, 20);
            this.chkSerive.StyleController = this.layoutControl1;
            this.chkSerive.TabIndex = 11;
            // 
            // dateDateTo
            // 
            this.dateDateTo.EditValue = null;
            this.dateDateTo.Location = new System.Drawing.Point(68, 84);
            this.dateDateTo.MenuManager = this.barManager1;
            this.dateDateTo.Name = "dateDateTo";
            this.dateDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateTo.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateDateTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDateTo.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateDateTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDateTo.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateDateTo.Size = new System.Drawing.Size(443, 20);
            this.dateDateTo.StyleController = this.layoutControl1;
            this.dateDateTo.TabIndex = 9;
            // 
            // dateDateFrom
            // 
            this.dateDateFrom.EditValue = null;
            this.dateDateFrom.Location = new System.Drawing.Point(68, 60);
            this.dateDateFrom.MenuManager = this.barManager1;
            this.dateDateFrom.Name = "dateDateFrom";
            this.dateDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDateFrom.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dateDateFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDateFrom.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dateDateFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDateFrom.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateDateFrom.Size = new System.Drawing.Size(443, 20);
            this.dateDateFrom.StyleController = this.layoutControl1;
            this.dateDateFrom.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutDateFrom,
            this.layoutDateTo,
            this.layoutSerice,
            this.layoutTower,
            this.layoutPeriod});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(523, 151);
            this.Root.TextVisible = false;
            // 
            // layoutDateFrom
            // 
            this.layoutDateFrom.Control = this.dateDateFrom;
            this.layoutDateFrom.Location = new System.Drawing.Point(0, 48);
            this.layoutDateFrom.Name = "layoutDateFrom";
            this.layoutDateFrom.Size = new System.Drawing.Size(503, 24);
            this.layoutDateFrom.Text = "Từ ngày";
            this.layoutDateFrom.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutDateTo
            // 
            this.layoutDateTo.Control = this.dateDateTo;
            this.layoutDateTo.Location = new System.Drawing.Point(0, 72);
            this.layoutDateTo.Name = "layoutDateTo";
            this.layoutDateTo.Size = new System.Drawing.Size(503, 24);
            this.layoutDateTo.Text = "Đến ngày";
            this.layoutDateTo.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutSerice
            // 
            this.layoutSerice.Control = this.chkSerive;
            this.layoutSerice.Location = new System.Drawing.Point(0, 96);
            this.layoutSerice.Name = "layoutSerice";
            this.layoutSerice.Size = new System.Drawing.Size(503, 35);
            this.layoutSerice.Text = "Dịch vụ";
            this.layoutSerice.TextSize = new System.Drawing.Size(53, 13);
            // 
            // lkTower
            // 
            this.lkTower.Location = new System.Drawing.Point(68, 12);
            this.lkTower.MenuManager = this.barManager1;
            this.lkTower.Name = "lkTower";
            this.lkTower.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkTower.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Dự án")});
            this.lkTower.Properties.DisplayMember = "TenTN";
            this.lkTower.Properties.NullText = "";
            this.lkTower.Properties.ValueMember = "MaTN";
            this.lkTower.Size = new System.Drawing.Size(443, 20);
            this.lkTower.StyleController = this.layoutControl1;
            this.lkTower.TabIndex = 13;
            // 
            // layoutTower
            // 
            this.layoutTower.Control = this.lkTower;
            this.layoutTower.Location = new System.Drawing.Point(0, 0);
            this.layoutTower.Name = "layoutTower";
            this.layoutTower.Size = new System.Drawing.Size(503, 24);
            this.layoutTower.Text = "Dự án";
            this.layoutTower.TextSize = new System.Drawing.Size(53, 13);
            // 
            // cbxPeriod
            // 
            this.cbxPeriod.Location = new System.Drawing.Point(68, 36);
            this.cbxPeriod.MenuManager = this.barManager1;
            this.cbxPeriod.Name = "cbxPeriod";
            this.cbxPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxPeriod.Size = new System.Drawing.Size(443, 20);
            this.cbxPeriod.StyleController = this.layoutControl1;
            this.cbxPeriod.TabIndex = 14;
            this.cbxPeriod.EditValueChanged += new System.EventHandler(this.cbxPeriod_EditValueChanged);
            // 
            // layoutPeriod
            // 
            this.layoutPeriod.Control = this.cbxPeriod;
            this.layoutPeriod.Location = new System.Drawing.Point(0, 24);
            this.layoutPeriod.Name = "layoutPeriod";
            this.layoutPeriod.Size = new System.Drawing.Size(503, 24);
            this.layoutPeriod.Text = "Kỳ báo cáo";
            this.layoutPeriod.TextSize = new System.Drawing.Size(53, 13);
            // 
            // frmAddAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 182);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmAddAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkSerive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSerice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTower.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem itemLuu;
        private DevExpress.XtraBars.BarButtonItem itemHuy;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.DateEdit dateDateTo;
        private DevExpress.XtraEditors.DateEdit dateDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutDateFrom;
        private DevExpress.XtraLayout.LayoutControlItem layoutDateTo;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkSerive;
        private DevExpress.XtraLayout.LayoutControlItem layoutSerice;
        private DevExpress.XtraEditors.LookUpEdit lkTower;
        private DevExpress.XtraLayout.LayoutControlItem layoutTower;
        private DevExpress.XtraEditors.ComboBoxEdit cbxPeriod;
        private DevExpress.XtraLayout.LayoutControlItem layoutPeriod;
    }
}