namespace LandSoftBuilding.Lease
{
    partial class frmChuyenTienCocVeThuTruoc
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkCompanyCode = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.spGiaTriChuyenVeThuTruoc = new DevExpress.XtraEditors.SpinEdit();
            this.spTongTienCoc = new DevExpress.XtraEditors.SpinEdit();
            this.deNgayPhieu = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemGiaTriChuyen = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkCompanyCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spGiaTriChuyenVeThuTruoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTongTienCoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayPhieu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayPhieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGiaTriChuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkCompanyCode);
            this.layoutControl1.Controls.Add(this.btnDong);
            this.layoutControl1.Controls.Add(this.btnLuu);
            this.layoutControl1.Controls.Add(this.spGiaTriChuyenVeThuTruoc);
            this.layoutControl1.Controls.Add(this.spTongTienCoc);
            this.layoutControl1.Controls.Add(this.deNgayPhieu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1045, 207, 812, 500);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(451, 183);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // glkCompanyCode
            // 
            this.glkCompanyCode.Location = new System.Drawing.Point(181, 90);
            this.glkCompanyCode.Name = "glkCompanyCode";
            this.glkCompanyCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkCompanyCode.Properties.DisplayMember = "KyHieu";
            this.glkCompanyCode.Properties.NullText = "";
            this.glkCompanyCode.Properties.PopupView = this.gridLookUpEdit1View;
            this.glkCompanyCode.Properties.ValueMember = "ID";
            this.glkCompanyCode.Size = new System.Drawing.Size(258, 22);
            this.glkCompanyCode.StyleController = this.layoutControl1;
            this.glkCompanyCode.TabIndex = 9;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(353, 116);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(86, 27);
            this.btnDong.StyleController = this.layoutControl1;
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(272, 116);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(77, 27);
            this.btnLuu.StyleController = this.layoutControl1;
            this.btnLuu.TabIndex = 7;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // spGiaTriChuyenVeThuTruoc
            // 
            this.spGiaTriChuyenVeThuTruoc.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spGiaTriChuyenVeThuTruoc.Location = new System.Drawing.Point(181, 64);
            this.spGiaTriChuyenVeThuTruoc.Name = "spGiaTriChuyenVeThuTruoc";
            this.spGiaTriChuyenVeThuTruoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spGiaTriChuyenVeThuTruoc.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spGiaTriChuyenVeThuTruoc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spGiaTriChuyenVeThuTruoc.Size = new System.Drawing.Size(258, 22);
            this.spGiaTriChuyenVeThuTruoc.StyleController = this.layoutControl1;
            this.spGiaTriChuyenVeThuTruoc.TabIndex = 6;
            // 
            // spTongTienCoc
            // 
            this.spTongTienCoc.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spTongTienCoc.Location = new System.Drawing.Point(181, 38);
            this.spTongTienCoc.Name = "spTongTienCoc";
            this.spTongTienCoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spTongTienCoc.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spTongTienCoc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spTongTienCoc.Properties.ReadOnly = true;
            this.spTongTienCoc.Size = new System.Drawing.Size(258, 22);
            this.spTongTienCoc.StyleController = this.layoutControl1;
            this.spTongTienCoc.TabIndex = 5;
            // 
            // deNgayPhieu
            // 
            this.deNgayPhieu.EditValue = null;
            this.deNgayPhieu.Location = new System.Drawing.Point(181, 12);
            this.deNgayPhieu.Name = "deNgayPhieu";
            this.deNgayPhieu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayPhieu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deNgayPhieu.Size = new System.Drawing.Size(258, 22);
            this.deNgayPhieu.StyleController = this.layoutControl1;
            this.deNgayPhieu.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.itemGiaTriChuyen,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(451, 183);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.deNgayPhieu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem1.Text = "Ngày phiếu";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(166, 17);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.spTongTienCoc;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem2.Text = "Tổng tiền cọc";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(166, 17);
            // 
            // itemGiaTriChuyen
            // 
            this.itemGiaTriChuyen.Control = this.spGiaTriChuyenVeThuTruoc;
            this.itemGiaTriChuyen.Location = new System.Drawing.Point(0, 52);
            this.itemGiaTriChuyen.Name = "itemGiaTriChuyen";
            this.itemGiaTriChuyen.Size = new System.Drawing.Size(431, 26);
            this.itemGiaTriChuyen.Text = "Giá trị chuyển về thu trước";
            this.itemGiaTriChuyen.TextSize = new System.Drawing.Size(166, 17);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnLuu;
            this.layoutControlItem4.Location = new System.Drawing.Point(260, 104);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(81, 59);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 104);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(260, 59);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnDong;
            this.layoutControlItem5.Location = new System.Drawing.Point(341, 104);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(90, 59);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.glkCompanyCode;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(431, 26);
            this.layoutControlItem3.Text = "Company Code";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(166, 16);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã";
            this.gridColumn1.FieldName = "KyHieu";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên ngắn";
            this.gridColumn2.FieldName = "TenVT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tên";
            this.gridColumn3.FieldName = "Ten";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // frmChuyenTienCocVeThuTruoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 183);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmChuyenTienCocVeThuTruoc";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển tiền cọc về thu trước";
            this.Load += new System.EventHandler(this.frmChuyenTienCocVeThuTruoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkCompanyCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spGiaTriChuyenVeThuTruoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spTongTienCoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayPhieu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deNgayPhieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemGiaTriChuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SpinEdit spGiaTriChuyenVeThuTruoc;
        private DevExpress.XtraEditors.SpinEdit spTongTienCoc;
        private DevExpress.XtraEditors.DateEdit deNgayPhieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem itemGiaTriChuyen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.GridLookUpEdit glkCompanyCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}