namespace DichVu.YeuCau
{
    partial class FrmAppGroupProcessEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAppGroupProcessEdit));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.itemClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.spinTimeFinish = new DevExpress.XtraEditors.SpinEdit();
            this.spinTimeReply = new DevExpress.XtraEditors.SpinEdit();
            this.spinTimeRecive = new DevExpress.XtraEditors.SpinEdit();
            this.chkIsDefault = new DevExpress.XtraEditors.CheckEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinTimeFinish.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTimeReply.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTimeRecive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
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
            this.barStaticItem1,
            this.itemSave,
            this.itemClose});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "GROUP PROCESS";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Save";
            this.itemSave.Id = 1;
            this.itemSave.ImageOptions.ImageIndex = 0;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // itemClose
            // 
            this.itemClose.Caption = "Close";
            this.itemClose.Id = 2;
            this.itemClose.ImageOptions.ImageIndex = 1;
            this.itemClose.Name = "itemClose";
            this.itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(635, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 169);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(635, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 138);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(635, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 138);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.spinTimeFinish);
            this.layoutControl1.Controls.Add(this.spinTimeReply);
            this.layoutControl1.Controls.Add(this.spinTimeRecive);
            this.layoutControl1.Controls.Add(this.chkIsDefault);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(635, 138);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // spinTimeFinish
            // 
            this.spinTimeFinish.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinTimeFinish.Location = new System.Drawing.Point(75, 84);
            this.spinTimeFinish.MenuManager = this.barManager1;
            this.spinTimeFinish.Name = "spinTimeFinish";
            this.spinTimeFinish.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTimeFinish.Properties.DisplayFormat.FormatString = "n0";
            this.spinTimeFinish.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTimeFinish.Properties.EditFormat.FormatString = "n0";
            this.spinTimeFinish.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTimeFinish.Properties.Mask.EditMask = "n0";
            this.spinTimeFinish.Properties.MaxValue = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.spinTimeFinish.Size = new System.Drawing.Size(531, 20);
            this.spinTimeFinish.StyleController = this.layoutControl1;
            this.spinTimeFinish.TabIndex = 9;
            // 
            // spinTimeReply
            // 
            this.spinTimeReply.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinTimeReply.Location = new System.Drawing.Point(75, 60);
            this.spinTimeReply.MenuManager = this.barManager1;
            this.spinTimeReply.Name = "spinTimeReply";
            this.spinTimeReply.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTimeReply.Properties.DisplayFormat.FormatString = "n0";
            this.spinTimeReply.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTimeReply.Properties.EditFormat.FormatString = "n0";
            this.spinTimeReply.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTimeReply.Properties.Mask.EditMask = "n0";
            this.spinTimeReply.Properties.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinTimeReply.Size = new System.Drawing.Size(531, 20);
            this.spinTimeReply.StyleController = this.layoutControl1;
            this.spinTimeReply.TabIndex = 8;
            // 
            // spinTimeRecive
            // 
            this.spinTimeRecive.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinTimeRecive.Location = new System.Drawing.Point(75, 36);
            this.spinTimeRecive.MenuManager = this.barManager1;
            this.spinTimeRecive.Name = "spinTimeRecive";
            this.spinTimeRecive.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinTimeRecive.Properties.DisplayFormat.FormatString = "n0";
            this.spinTimeRecive.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTimeRecive.Properties.EditFormat.FormatString = "n0";
            this.spinTimeRecive.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTimeRecive.Properties.Mask.EditMask = "n0";
            this.spinTimeRecive.Properties.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spinTimeRecive.Size = new System.Drawing.Size(531, 20);
            this.spinTimeRecive.StyleController = this.layoutControl1;
            this.spinTimeRecive.TabIndex = 7;
            // 
            // chkIsDefault
            // 
            this.chkIsDefault.Location = new System.Drawing.Point(12, 108);
            this.chkIsDefault.MenuManager = this.barManager1;
            this.chkIsDefault.Name = "chkIsDefault";
            this.chkIsDefault.Properties.Caption = "IsDefault";
            this.chkIsDefault.Size = new System.Drawing.Size(594, 19);
            this.chkIsDefault.StyleController = this.layoutControl1;
            this.chkIsDefault.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(75, 12);
            this.txtName.MenuManager = this.barManager1;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(531, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(618, 139);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtName;
            this.layoutControlItem1.CustomizationFormText = "Name";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(598, 24);
            this.layoutControlItem1.Text = "Name";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkIsDefault;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(598, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spinTimeRecive;
            this.layoutControlItem2.CustomizationFormText = "TimeReceive";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(598, 24);
            this.layoutControlItem2.Text = "TimeReceive";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spinTimeReply;
            this.layoutControlItem4.CustomizationFormText = "TimeReply";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(598, 24);
            this.layoutControlItem4.Text = "TimeReply";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.spinTimeFinish;
            this.layoutControlItem5.CustomizationFormText = "TimeFinish";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(598, 24);
            this.layoutControlItem5.Text = "TimeFinish";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 13);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // FrmAppGroupProcessEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 169);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmAppGroupProcessEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GROUP PROCESS EDIT";
            this.Load += new System.EventHandler(this.frmAppGroupProcessEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinTimeFinish.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTimeReply.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTimeRecive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit chkIsDefault;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem itemSave;
        private DevExpress.XtraBars.BarButtonItem itemClose;
        private DevExpress.XtraEditors.SpinEdit spinTimeFinish;
        private DevExpress.XtraEditors.SpinEdit spinTimeReply;
        private DevExpress.XtraEditors.SpinEdit spinTimeRecive;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}