using DevExpress.XtraBars;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
namespace DichVu.YeuCau
{
    partial class frmGiaoViec
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGiaoViec));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiHuy = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lkLoaiThongBao = new DevExpress.XtraEditors.LookUpEdit();
            this.glkNhanVien = new DevExpress.XtraEditors.GridLookUpEdit();
            this.glkNhanVienView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkPhongBan = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkNhomCV = new DevExpress.XtraEditors.LookUpEdit();
            this.memoNoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiThongBao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkNhanVienView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkPhongBan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhomCV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
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
            this.barManager1.HideBarsWhenMerging = false;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemSave,
            this.bbiHuy});
            this.barManager1.MaxItemId = 5;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiHuy, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemSave
            // 
            this.itemSave.Caption = "Lưu";
            this.itemSave.Id = 1;
            this.itemSave.ImageOptions.ImageIndex = 0;
            this.itemSave.Name = "itemSave";
            this.itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.itemSave_ItemClick);
            // 
            // bbiHuy
            // 
            this.bbiHuy.Caption = "Hủy";
            this.bbiHuy.Id = 4;
            this.bbiHuy.ImageOptions.ImageIndex = 1;
            this.bbiHuy.Name = "bbiHuy";
            this.bbiHuy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiHuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiHuy_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(3, 3);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(373, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(3, 167);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(373, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(3, 34);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 133);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(376, 34);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 133);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lkLoaiThongBao);
            this.layoutControl1.Controls.Add(this.glkNhanVien);
            this.layoutControl1.Controls.Add(this.glkPhongBan);
            this.layoutControl1.Controls.Add(this.lkNhomCV);
            this.layoutControl1.Controls.Add(this.memoNoiDung);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(3, 34);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(668, 153, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(373, 133);
            this.layoutControl1.TabIndex = 9;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lkLoaiThongBao
            // 
            this.lkLoaiThongBao.Location = new System.Drawing.Point(80, 74);
            this.lkLoaiThongBao.MenuManager = this.barManager1;
            this.lkLoaiThongBao.Name = "lkLoaiThongBao";
            this.lkLoaiThongBao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkLoaiThongBao.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLoai", "Loại gửi")});
            this.lkLoaiThongBao.Properties.DisplayMember = "TenLoai";
            this.lkLoaiThongBao.Properties.NullText = "[Chọn loại gửi sms]";
            this.lkLoaiThongBao.Properties.ValueMember = "MaLoai";
            this.lkLoaiThongBao.Size = new System.Drawing.Size(291, 20);
            this.lkLoaiThongBao.StyleController = this.layoutControl1;
            this.lkLoaiThongBao.TabIndex = 15;
            // 
            // glkNhanVien
            // 
            this.glkNhanVien.Location = new System.Drawing.Point(80, 26);
            this.glkNhanVien.MenuManager = this.barManager1;
            this.glkNhanVien.Name = "glkNhanVien";
            this.glkNhanVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkNhanVien.Properties.DisplayMember = "HoTenNV";
            this.glkNhanVien.Properties.NullText = "[Chọn nhân viên]";
            this.glkNhanVien.Properties.PopupFormSize = new System.Drawing.Size(500, 300);
            this.glkNhanVien.Properties.PopupView = this.glkNhanVienView;
            this.glkNhanVien.Properties.ValueMember = "MaNV";
            this.glkNhanVien.Properties.ViewType = DevExpress.XtraEditors.Repository.GridLookUpViewType.GridView;
            this.glkNhanVien.Size = new System.Drawing.Size(291, 20);
            this.glkNhanVien.StyleController = this.layoutControl1;
            this.glkNhanVien.TabIndex = 14;
            this.glkNhanVien.EditValueChanged += new System.EventHandler(this.glkNhanVien_EditValueChanged);
            // 
            // glkNhanVienView
            // 
            this.glkNhanVienView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.glkNhanVienView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.glkNhanVienView.Name = "glkNhanVienView";
            this.glkNhanVienView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.glkNhanVienView.OptionsView.ShowAutoFilterRow = true;
            this.glkNhanVienView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mã số";
            this.gridColumn2.FieldName = "MaSoNV";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Họ tên nhân viên";
            this.gridColumn3.FieldName = "HoTenNV";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Điện thoại";
            this.gridColumn4.FieldName = "DienThoai";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Email";
            this.gridColumn5.FieldName = "Email";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // glkPhongBan
            // 
            this.glkPhongBan.Location = new System.Drawing.Point(80, 2);
            this.glkPhongBan.MenuManager = this.barManager1;
            this.glkPhongBan.Name = "glkPhongBan";
            this.glkPhongBan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkPhongBan.Properties.DisplayMember = "TenPB";
            this.glkPhongBan.Properties.NullText = "[Chọn phòng ban]";
            this.glkPhongBan.Properties.PopupView = this.gridLookUpEdit1View;
            this.glkPhongBan.Properties.ValueMember = "MaPB";
            this.glkPhongBan.Size = new System.Drawing.Size(291, 20);
            this.glkPhongBan.StyleController = this.layoutControl1;
            this.glkPhongBan.TabIndex = 13;
            this.glkPhongBan.EditValueChanged += new System.EventHandler(this.glkPhongBan_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Phòng ban";
            this.gridColumn1.FieldName = "TenPB";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // lkNhomCV
            // 
            this.lkNhomCV.Location = new System.Drawing.Point(80, 50);
            this.lkNhomCV.MenuManager = this.barManager1;
            this.lkNhomCV.Name = "lkNhomCV";
            this.lkNhomCV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkNhomCV.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.lkNhomCV.Properties.DisplayMember = "Name";
            this.lkNhomCV.Properties.NullText = "[Vui lòng chọn nhóm công việc]";
            this.lkNhomCV.Properties.ValueMember = "Id";
            this.lkNhomCV.Size = new System.Drawing.Size(291, 20);
            this.lkNhomCV.StyleController = this.layoutControl1;
            this.lkNhomCV.TabIndex = 12;
            // 
            // memoNoiDung
            // 
            this.memoNoiDung.Location = new System.Drawing.Point(80, 98);
            this.memoNoiDung.MenuManager = this.barManager1;
            this.memoNoiDung.Name = "memoNoiDung";
            this.memoNoiDung.Size = new System.Drawing.Size(291, 33);
            this.memoNoiDung.StyleController = this.layoutControl1;
            this.memoNoiDung.TabIndex = 11;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.ContentImageOptions.Alignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(373, 133);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.memoNoiDung;
            this.layoutControlItem2.CustomizationFormText = "Nội Dung";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(373, 37);
            this.layoutControlItem2.Text = "Nội Dung";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(75, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lkNhomCV;
            this.layoutControlItem3.CustomizationFormText = "Nhóm công việc";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(373, 24);
            this.layoutControlItem3.Text = "Nhóm công việc";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(75, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.glkPhongBan;
            this.layoutControlItem4.CustomizationFormText = "Phòng ban";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(373, 24);
            this.layoutControlItem4.Text = "Phòng ban";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(75, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.glkNhanVien;
            this.layoutControlItem5.CustomizationFormText = "Nhân viên";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(373, 24);
            this.layoutControlItem5.Text = "Nhân viên";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(75, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lkLoaiThongBao;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(373, 24);
            this.layoutControlItem1.Text = "Gửi thông báo";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(75, 13);
            // 
            // frmGiaoViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 170);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmGiaoViec";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giao việc";
            this.Load += new System.EventHandler(this.frmGiaoViec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkLoaiThongBao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkNhanVienView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkPhongBan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkNhomCV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoNoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BarManager barManager1;

        private Bar bar1;

        private BarDockControl barDockControlTop;

        private BarDockControl barDockControlBottom;

        private BarDockControl barDockControlLeft;

        private BarDockControl barDockControlRight;

        private BarButtonItem itemSave;

        private BarButtonItem bbiHuy;

        private LayoutControl layoutControl1;

        private LayoutControlGroup layoutControlGroup1;

        private MemoEdit memoNoiDung;

        private LayoutControlItem layoutControlItem2;

        private LookUpEdit lkNhomCV;

        private LayoutControlItem layoutControlItem3;
        private ImageCollection imageCollection1;
        private GridLookUpEdit glkNhanVien;
        private DevExpress.XtraGrid.Views.Grid.GridView glkNhanVienView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private GridLookUpEdit glkPhongBan;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlItem layoutControlItem5;
        private LookUpEdit lkLoaiThongBao;
        private LayoutControlItem layoutControlItem1;
    }
}
