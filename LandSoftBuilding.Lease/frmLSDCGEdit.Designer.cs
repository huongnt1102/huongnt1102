namespace LandSoftBuilding.Lease
{
    partial class frmLSDCGEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLSDCGEdit));
            this.itemCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lkPhanLoai = new DevExpress.XtraEditors.LookUpEdit();
            this.spGiaTriMoi = new DevExpress.XtraEditors.SpinEdit();
            this.itemSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.dateNgayDC = new DevExpress.XtraEditors.DateEdit();
            this.spTyLeDC = new DevExpress.XtraEditors.SpinEdit();
            this.spGiaTriCu = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkPhanLoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spGiaTriMoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDC.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTyLeDC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spGiaTriCu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // itemCancel
            // 
            this.itemCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.itemCancel.Appearance.Options.UseFont = true;
            this.itemCancel.ImageOptions.ImageIndex = 1;
            this.itemCancel.ImageOptions.ImageList = this.imageCollection1;
            this.itemCancel.Location = new System.Drawing.Point(386, 205);
            this.itemCancel.Name = "itemCancel";
            this.itemCancel.Size = new System.Drawing.Size(76, 26);
            this.itemCancel.StyleController = this.layoutControl1;
            this.itemCancel.TabIndex = 13;
            this.itemCancel.Text = "Hủy bỏ";
            this.itemCancel.Click += new System.EventHandler(this.itemCancel_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lkPhanLoai);
            this.layoutControl1.Controls.Add(this.spGiaTriMoi);
            this.layoutControl1.Controls.Add(this.itemCancel);
            this.layoutControl1.Controls.Add(this.itemSubmit);
            this.layoutControl1.Controls.Add(this.txtDienGiai);
            this.layoutControl1.Controls.Add(this.dateNgayDC);
            this.layoutControl1.Controls.Add(this.spTyLeDC);
            this.layoutControl1.Controls.Add(this.spGiaTriCu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(474, 243);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lkPhanLoai
            // 
            this.lkPhanLoai.Location = new System.Drawing.Point(91, 36);
            this.lkPhanLoai.Name = "lkPhanLoai";
            this.lkPhanLoai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkPhanLoai.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenPL", "Name1")});
            this.lkPhanLoai.Properties.DisplayMember = "TenPL";
            this.lkPhanLoai.Properties.NullText = "";
            this.lkPhanLoai.Properties.ShowHeader = false;
            this.lkPhanLoai.Properties.ValueMember = "ID";
            this.lkPhanLoai.Size = new System.Drawing.Size(371, 20);
            this.lkPhanLoai.StyleController = this.layoutControl1;
            this.lkPhanLoai.TabIndex = 22;
            this.lkPhanLoai.EditValueChanged += new System.EventHandler(this.lkPhanLoai_EditValueChanged);
            // 
            // spGiaTriMoi
            // 
            this.spGiaTriMoi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spGiaTriMoi.Location = new System.Drawing.Point(91, 108);
            this.spGiaTriMoi.Name = "spGiaTriMoi";
            this.spGiaTriMoi.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spGiaTriMoi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spGiaTriMoi.Size = new System.Drawing.Size(371, 20);
            this.spGiaTriMoi.StyleController = this.layoutControl1;
            this.spGiaTriMoi.TabIndex = 14;
            // 
            // itemSubmit
            // 
            this.itemSubmit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.itemSubmit.Appearance.Options.UseFont = true;
            this.itemSubmit.ImageOptions.ImageIndex = 0;
            this.itemSubmit.ImageOptions.ImageList = this.imageCollection1;
            this.itemSubmit.Location = new System.Drawing.Point(286, 205);
            this.itemSubmit.Name = "itemSubmit";
            this.itemSubmit.Size = new System.Drawing.Size(96, 26);
            this.itemSubmit.StyleController = this.layoutControl1;
            this.itemSubmit.TabIndex = 12;
            this.itemSubmit.Text = "Thực hiện";
            this.itemSubmit.Click += new System.EventHandler(this.itemSubmit_Click);
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(91, 132);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(371, 69);
            this.txtDienGiai.StyleController = this.layoutControl1;
            this.txtDienGiai.TabIndex = 11;
            // 
            // dateNgayDC
            // 
            this.dateNgayDC.EditValue = null;
            this.dateNgayDC.Location = new System.Drawing.Point(91, 12);
            this.dateNgayDC.Name = "dateNgayDC";
            this.dateNgayDC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayDC.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayDC.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgayDC.Size = new System.Drawing.Size(371, 20);
            this.dateNgayDC.StyleController = this.layoutControl1;
            this.dateNgayDC.TabIndex = 6;
            // 
            // spTyLeDC
            // 
            this.spTyLeDC.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spTyLeDC.Location = new System.Drawing.Point(91, 84);
            this.spTyLeDC.Name = "spTyLeDC";
            this.spTyLeDC.Properties.DisplayFormat.FormatString = "p2";
            this.spTyLeDC.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spTyLeDC.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spTyLeDC.Properties.Mask.EditMask = "p2";
            this.spTyLeDC.Size = new System.Drawing.Size(371, 20);
            this.spTyLeDC.StyleController = this.layoutControl1;
            this.spTyLeDC.TabIndex = 8;
            this.spTyLeDC.EditValueChanged += new System.EventHandler(this.spTyLeDC_EditValueChanged);
            // 
            // spGiaTriCu
            // 
            this.spGiaTriCu.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spGiaTriCu.Location = new System.Drawing.Point(91, 60);
            this.spGiaTriCu.Name = "spGiaTriCu";
            this.spGiaTriCu.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spGiaTriCu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spGiaTriCu.Properties.ReadOnly = true;
            this.spGiaTriCu.Size = new System.Drawing.Size(371, 20);
            this.spGiaTriCu.StyleController = this.layoutControl1;
            this.spGiaTriCu.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem6,
            this.layoutControlItem17,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem9});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(474, 243);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateNgayDC;
            this.layoutControlItem1.CustomizationFormText = "Ngày điều chỉnh";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(454, 24);
            this.layoutControlItem1.Text = "Ngày điều chỉnh";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 193);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(274, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.itemSubmit;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(274, 193);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(100, 30);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(100, 30);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(100, 30);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.itemCancel;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(374, 193);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(80, 30);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(80, 30);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(80, 30);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtDienGiai;
            this.layoutControlItem6.CustomizationFormText = "Diễn giải";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(454, 73);
            this.layoutControlItem6.Text = "Diễn giải";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(76, 13);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.lkPhanLoai;
            this.layoutControlItem17.CustomizationFormText = "Phân loại";
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(454, 24);
            this.layoutControlItem17.Text = "Phân loại";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(76, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spGiaTriCu;
            this.layoutControlItem2.CustomizationFormText = "Đơn giá cũ";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(454, 24);
            this.layoutControlItem2.Text = "Giá trị cũ";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 13);
            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.spTyLeDC;
            this.layoutControlItem3.CustomizationFormText = "Tỷ lệ điều chỉnh";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(454, 24);
            this.layoutControlItem3.Text = "Tỷ lệ điều chỉnh";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(76, 13);
            this.layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.spGiaTriMoi;
            this.layoutControlItem9.CustomizationFormText = "Đơn giá mới";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(454, 24);
            this.layoutControlItem9.Text = "Giá trị mới";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(76, 13);
            // 
            // frmLSDCGEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 243);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLSDCGEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Điểu chỉnh giá cho thuê";
            this.Load += new System.EventHandler(this.frmLSDCGEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkPhanLoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spGiaTriMoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDC.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTyLeDC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spGiaTriCu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.SpinEdit spTyLeDC;
        private DevExpress.XtraEditors.SpinEdit spGiaTriCu;
        private DevExpress.XtraEditors.DateEdit dateNgayDC;
        private DevExpress.XtraEditors.SimpleButton itemCancel;
        private DevExpress.XtraEditors.SimpleButton itemSubmit;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SpinEdit spGiaTriMoi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LookUpEdit lkPhanLoai;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;

    }
}