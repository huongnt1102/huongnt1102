namespace DichVu.NhanKhau
{
    partial class ctlCuDan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlCuDan));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.itemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.itemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.gcNhanKhau = new DevExpress.XtraGrid.GridControl();
            this.grvNhanKhau = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn47 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn52 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNhanKhau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemEdit,
            this.itemDelete});
            this.barManager1.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(868, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 261);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(868, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 261);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(868, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 261);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add.png");
            this.imageCollection1.Images.SetKeyName(1, "delete.png");
            this.imageCollection1.Images.SetKeyName(2, "edit.png");
            // 
            // itemEdit
            // 
            this.itemEdit.Caption = "Sửa";
            this.itemEdit.Id = 1;
            this.itemEdit.ImageOptions.ImageIndex = 2;
            this.itemEdit.Name = "itemEdit";
            // 
            // itemDelete
            // 
            this.itemDelete.Caption = "Xóa";
            this.itemDelete.Id = 2;
            this.itemDelete.ImageOptions.ImageIndex = 1;
            this.itemDelete.Name = "itemDelete";
            // 
            // gcNhanKhau
            // 
            this.gcNhanKhau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNhanKhau.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcNhanKhau.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcNhanKhau.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcNhanKhau.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcNhanKhau.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcNhanKhau.EmbeddedNavigator.TextStringFormat = "{0}/{1}";
            this.gcNhanKhau.Location = new System.Drawing.Point(0, 0);
            this.gcNhanKhau.MainView = this.grvNhanKhau;
            this.gcNhanKhau.MenuManager = this.barManager1;
            this.gcNhanKhau.Name = "gcNhanKhau";
            this.gcNhanKhau.ShowOnlyPredefinedDetails = true;
            this.gcNhanKhau.Size = new System.Drawing.Size(868, 261);
            this.gcNhanKhau.TabIndex = 4;
            this.gcNhanKhau.UseEmbeddedNavigator = true;
            this.gcNhanKhau.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNhanKhau});
            // 
            // grvNhanKhau
            // 
            this.grvNhanKhau.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn44,
            this.gridColumn1,
            this.gridColumn45,
            this.gridColumn46,
            this.gridColumn47,
            this.gridColumn48,
            this.gridColumn49,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn52,
            this.gridColumn53,
            this.gridColumn2,
            this.gridColumn54,
            this.gridColumn3});
            this.grvNhanKhau.GridControl = this.gcNhanKhau;
            this.grvNhanKhau.Name = "grvNhanKhau";
            this.grvNhanKhau.OptionsCustomization.AllowGroup = false;
            this.grvNhanKhau.OptionsSelection.MultiSelect = true;
            this.grvNhanKhau.OptionsView.ColumnAutoWidth = false;
            this.grvNhanKhau.OptionsView.ShowAutoFilterRow = true;
            this.grvNhanKhau.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "Họ và tên";
            this.gridColumn44.FieldName = "HoTenNK";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.OptionsColumn.AllowEdit = false;
            this.gridColumn44.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 0;
            this.gridColumn44.Width = 130;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Quan hệ";
            this.gridColumn1.FieldName = "TenQuanHe";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 66;
            // 
            // gridColumn45
            // 
            this.gridColumn45.Caption = "Giới tính";
            this.gridColumn45.FieldName = "GioiTinh";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.OptionsColumn.AllowEdit = false;
            this.gridColumn45.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn45.Visible = true;
            this.gridColumn45.VisibleIndex = 2;
            this.gridColumn45.Width = 60;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "Ngày sinh";
            this.gridColumn46.FieldName = "NgaySinh";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.OptionsColumn.AllowEdit = false;
            this.gridColumn46.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn46.Visible = true;
            this.gridColumn46.VisibleIndex = 3;
            // 
            // gridColumn47
            // 
            this.gridColumn47.Caption = "Số CMND";
            this.gridColumn47.FieldName = "CMND";
            this.gridColumn47.Name = "gridColumn47";
            this.gridColumn47.OptionsColumn.AllowEdit = false;
            this.gridColumn47.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn47.Visible = true;
            this.gridColumn47.VisibleIndex = 4;
            this.gridColumn47.Width = 85;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "Ngày cấp";
            this.gridColumn48.FieldName = "NgayCap";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.OptionsColumn.AllowEdit = false;
            this.gridColumn48.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 5;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "Nơi cấp";
            this.gridColumn49.FieldName = "NopCap";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.OptionsColumn.AllowEdit = false;
            this.gridColumn49.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 6;
            this.gridColumn49.Width = 100;
            // 
            // gridColumn50
            // 
            this.gridColumn50.Caption = "Địa chỉ";
            this.gridColumn50.FieldName = "DCTT";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.OptionsColumn.AllowEdit = false;
            this.gridColumn50.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 7;
            this.gridColumn50.Width = 150;
            // 
            // gridColumn51
            // 
            this.gridColumn51.Caption = "Điện thoại";
            this.gridColumn51.FieldName = "DienThoai";
            this.gridColumn51.Name = "gridColumn51";
            this.gridColumn51.OptionsColumn.AllowEdit = false;
            this.gridColumn51.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn51.Visible = true;
            this.gridColumn51.VisibleIndex = 8;
            this.gridColumn51.Width = 85;
            // 
            // gridColumn52
            // 
            this.gridColumn52.Caption = "Email";
            this.gridColumn52.FieldName = "Email";
            this.gridColumn52.Name = "gridColumn52";
            this.gridColumn52.OptionsColumn.AllowEdit = false;
            this.gridColumn52.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn52.Visible = true;
            this.gridColumn52.VisibleIndex = 9;
            this.gridColumn52.Width = 100;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "Đăng kí tạm trú";
            this.gridColumn53.FieldName = "DaDKTT";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.OptionsColumn.AllowEdit = false;
            this.gridColumn53.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 10;
            this.gridColumn53.Width = 92;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tình trạng";
            this.gridColumn2.FieldName = "TenTrangThai";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 11;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn54
            // 
            this.gridColumn54.Caption = "Người nhập";
            this.gridColumn54.FieldName = "HoTenNV";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.OptionsColumn.AllowEdit = false;
            this.gridColumn54.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn54.Visible = true;
            this.gridColumn54.VisibleIndex = 12;
            this.gridColumn54.Width = 115;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Ngày đăng ký";
            this.gridColumn3.FieldName = "NgayDK";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 13;
            this.gridColumn3.Width = 80;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.itemEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDelete)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.MenuCaption = "Tùy chọn";
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.ShowCaption = true;
            // 
            // ctlCuDan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcNhanKhau);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ctlCuDan";
            this.Size = new System.Drawing.Size(868, 261);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNhanKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNhanKhau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem itemEdit;
        private DevExpress.XtraBars.BarButtonItem itemDelete;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.GridControl gcNhanKhau;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNhanKhau;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn47;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn52;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}
