namespace TaiSan.MuaHang
{
    partial class frmTraHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTraHang));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcTraHang = new DevExpress.XtraGrid.GridControl();
            this.grvTraHang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTaiSanTra = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtSLTra = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn36 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rdateNgayNK = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.txtNCC = new DevExpress.XtraEditors.TextEdit();
            this.txtPMH = new DevExpress.XtraEditors.TextEdit();
            this.dateNgayMua = new DevExpress.XtraEditors.DateEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.txtNguoiNhanHang = new DevExpress.XtraEditors.TextEdit();
            this.dateXuatTra = new DevExpress.XtraEditors.DateEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.txtNguoiTra = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTraHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTraHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTaiSanTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSLTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdateNgayNK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdateNgayNK.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNCC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPMH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayMua.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayMua.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNhanHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateXuatTra.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateXuatTra.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiTra.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "add_outline.png");
            this.imageCollection1.Images.SetKeyName(1, "edit-delete.png");
            this.imageCollection1.Images.SetKeyName(2, "edit.png");
            this.imageCollection1.Images.SetKeyName(3, "108.gif");
            this.imageCollection1.Images.SetKeyName(4, "print.png");
            this.imageCollection1.Images.SetKeyName(5, "1337494333_document.png");
            this.imageCollection1.Images.SetKeyName(6, "Tick-02.png");
            this.imageCollection1.InsertImage(global::TaiSan.Properties.Resources.Luu, "Luu", typeof(global::TaiSan.Properties.Resources), 7);
            this.imageCollection1.Images.SetKeyName(7, "Luu");
            this.imageCollection1.InsertImage(global::TaiSan.Properties.Resources.cancel, "cancel", typeof(global::TaiSan.Properties.Resources), 8);
            this.imageCollection1.Images.SetKeyName(8, "cancel");
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSave,
            this.btnCancel});
            this.barManager1.MaxItemId = 3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancel)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Lưu && Đóng";
            this.btnSave.Id = 1;
            this.btnSave.ImageIndex = 7;
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Caption = "Thoát";
            this.btnCancel.Id = 2;
            this.btnCancel.ImageIndex = 8;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancel_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(837, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 402);
            this.barDockControlBottom.Size = new System.Drawing.Size(837, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 371);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(837, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 371);
            // 
            // gcTraHang
            // 
            this.gcTraHang.Location = new System.Drawing.Point(3, 129);
            this.gcTraHang.MainView = this.grvTraHang;
            this.gcTraHang.MenuManager = this.barManager1;
            this.gcTraHang.Name = "gcTraHang";
            this.gcTraHang.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtSLTra,
            this.lookTaiSanTra,
            this.rdateNgayNK});
            this.gcTraHang.Size = new System.Drawing.Size(834, 268);
            this.gcTraHang.TabIndex = 19;
            this.gcTraHang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTraHang});
            // 
            // grvTraHang
            // 
            this.grvTraHang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn35,
            this.gridColumn36,
            this.gridColumn37,
            this.gridColumn1,
            this.gridColumn2});
            this.grvTraHang.GridControl = this.gcTraHang;
            this.grvTraHang.Name = "grvTraHang";
            this.grvTraHang.OptionsView.ColumnAutoWidth = false;
            this.grvTraHang.OptionsView.ShowGroupPanel = false;
            this.grvTraHang.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvTraHang_CellValueChanged);
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Tài Sản";
            this.gridColumn13.ColumnEdit = this.lookTaiSanTra;
            this.gridColumn13.FieldName = "ID";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 0;
            this.gridColumn13.Width = 198;
            // 
            // lookTaiSanTra
            // 
            this.lookTaiSanTra.AutoHeight = false;
            this.lookTaiSanTra.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTaiSanTra.DisplayMember = "TenLTS";
            this.lookTaiSanTra.Name = "lookTaiSanTra";
            this.lookTaiSanTra.NullText = "";
            this.lookTaiSanTra.ValueMember = "ID";
            this.lookTaiSanTra.EditValueChanged += new System.EventHandler(this.lookTaiSanTra_EditValueChanged);
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Đơn Vị Tính";
            this.gridColumn10.FieldName = "TenDVT";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 83;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Số Lượng Trả";
            this.gridColumn11.ColumnEdit = this.txtSLTra;
            this.gridColumn11.FieldName = "SoLuongTra";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 6;
            this.gridColumn11.Width = 82;
            // 
            // txtSLTra
            // 
            this.txtSLTra.AutoHeight = false;
            this.txtSLTra.DisplayFormat.FormatString = "##,##.##";
            this.txtSLTra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSLTra.Name = "txtSLTra";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Lý Do";
            this.gridColumn12.FieldName = "LyDo";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 7;
            this.gridColumn12.Width = 435;
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "Hạn Sử Dụng";
            this.gridColumn35.DisplayFormat.FormatString = "{##,##} Tháng";
            this.gridColumn35.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn35.FieldName = "HanSuDung";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.OptionsColumn.AllowEdit = false;
            this.gridColumn35.OptionsColumn.ReadOnly = true;
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 4;
            this.gridColumn35.Width = 94;
            // 
            // gridColumn36
            // 
            this.gridColumn36.Caption = "Số Lượng Mua";
            this.gridColumn36.DisplayFormat.FormatString = "##,##.##";
            this.gridColumn36.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn36.FieldName = "SoLuong";
            this.gridColumn36.Name = "gridColumn36";
            this.gridColumn36.OptionsColumn.AllowEdit = false;
            this.gridColumn36.OptionsColumn.ReadOnly = true;
            this.gridColumn36.Visible = true;
            this.gridColumn36.VisibleIndex = 5;
            this.gridColumn36.Width = 85;
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "Đơn Giá";
            this.gridColumn37.DisplayFormat.FormatString = "{##,##} VNĐ";
            this.gridColumn37.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn37.FieldName = "DonGia";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.OptionsColumn.AllowEdit = false;
            this.gridColumn37.OptionsColumn.ReadOnly = true;
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 3;
            this.gridColumn37.Width = 117;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã tài sản mua hàng";
            this.gridColumn1.FieldName = "IDmsTaiSan";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ngày Nhập Kho";
            this.gridColumn2.ColumnEdit = this.rdateNgayNK;
            this.gridColumn2.FieldName = "NgayNK";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 82;
            // 
            // rdateNgayNK
            // 
            this.rdateNgayNK.AutoHeight = false;
            this.rdateNgayNK.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rdateNgayNK.Name = "rdateNgayNK";
            this.rdateNgayNK.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.txtNguoiTra);
            this.groupControl4.Controls.Add(this.labelControl1);
            this.groupControl4.Controls.Add(this.txtNCC);
            this.groupControl4.Controls.Add(this.txtPMH);
            this.groupControl4.Controls.Add(this.dateNgayMua);
            this.groupControl4.Controls.Add(this.labelControl20);
            this.groupControl4.Controls.Add(this.labelControl14);
            this.groupControl4.Controls.Add(this.txtNguoiNhanHang);
            this.groupControl4.Controls.Add(this.dateXuatTra);
            this.groupControl4.Controls.Add(this.labelControl19);
            this.groupControl4.Controls.Add(this.labelControl21);
            this.groupControl4.Controls.Add(this.labelControl23);
            this.groupControl4.Location = new System.Drawing.Point(0, 28);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.ShowCaption = false;
            this.groupControl4.Size = new System.Drawing.Size(834, 95);
            this.groupControl4.TabIndex = 18;
            this.groupControl4.Text = "Thông tin xuất kho:";
            // 
            // txtNCC
            // 
            this.txtNCC.Location = new System.Drawing.Point(510, 62);
            this.txtNCC.MenuManager = this.barManager1;
            this.txtNCC.Name = "txtNCC";
            this.txtNCC.Properties.ReadOnly = true;
            this.txtNCC.Size = new System.Drawing.Size(315, 20);
            this.txtNCC.TabIndex = 44;
            // 
            // txtPMH
            // 
            this.txtPMH.Location = new System.Drawing.Point(510, 9);
            this.txtPMH.MenuManager = this.barManager1;
            this.txtPMH.Name = "txtPMH";
            this.txtPMH.Properties.ReadOnly = true;
            this.txtPMH.Size = new System.Drawing.Size(315, 20);
            this.txtPMH.TabIndex = 43;
            this.txtPMH.EditValueChanged += new System.EventHandler(this.txtPMH_EditValueChanged);
            // 
            // dateNgayMua
            // 
            this.dateNgayMua.EditValue = null;
            this.dateNgayMua.Location = new System.Drawing.Point(510, 36);
            this.dateNgayMua.MenuManager = this.barManager1;
            this.dateNgayMua.Name = "dateNgayMua";
            this.dateNgayMua.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.dateNgayMua.Properties.ReadOnly = true;
            this.dateNgayMua.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayMua.Size = new System.Drawing.Size(315, 20);
            this.dateNgayMua.TabIndex = 42;
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(428, 39);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(52, 13);
            this.labelControl20.TabIndex = 41;
            this.labelControl20.Text = "Ngày mua:";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(428, 66);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(69, 13);
            this.labelControl14.TabIndex = 37;
            this.labelControl14.Text = "Nhà cung cấp:";
            // 
            // txtNguoiNhanHang
            // 
            this.txtNguoiNhanHang.Location = new System.Drawing.Point(99, 62);
            this.txtNguoiNhanHang.MenuManager = this.barManager1;
            this.txtNguoiNhanHang.Name = "txtNguoiNhanHang";
            this.txtNguoiNhanHang.Size = new System.Drawing.Size(315, 20);
            this.txtNguoiNhanHang.TabIndex = 36;
            // 
            // dateXuatTra
            // 
            this.dateXuatTra.EditValue = null;
            this.dateXuatTra.Location = new System.Drawing.Point(99, 36);
            this.dateXuatTra.MenuManager = this.barManager1;
            this.dateXuatTra.Name = "dateXuatTra";
            this.dateXuatTra.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateXuatTra.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateXuatTra.Size = new System.Drawing.Size(315, 20);
            this.dateXuatTra.TabIndex = 34;
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(13, 39);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(46, 13);
            this.labelControl19.TabIndex = 35;
            this.labelControl19.Text = "Ngày trả:";
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(428, 12);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(80, 13);
            this.labelControl21.TabIndex = 24;
            this.labelControl21.Text = "Phiếu mua hàng:";
            // 
            // labelControl23
            // 
            this.labelControl23.Location = new System.Drawing.Point(13, 65);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(59, 13);
            this.labelControl23.TabIndex = 22;
            this.labelControl23.Text = "Người nhận:";
            // 
            // txtNguoiTra
            // 
            this.txtNguoiTra.Location = new System.Drawing.Point(99, 9);
            this.txtNguoiTra.MenuManager = this.barManager1;
            this.txtNguoiTra.Name = "txtNguoiTra";
            this.txtNguoiTra.Properties.ReadOnly = true;
            this.txtNguoiTra.Size = new System.Drawing.Size(315, 20);
            this.txtNguoiTra.TabIndex = 46;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 45;
            this.labelControl1.Text = "Người trả:";
            // 
            // frmTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 402);
            this.Controls.Add(this.gcTraHang);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTraHang";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trả Hàng Đã Nhập Kho";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTraHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTraHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTaiSanTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSLTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdateNgayNK.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdateNgayNK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNCC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPMH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayMua.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayMua.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNhanHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateXuatTra.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateXuatTra.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiTra.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnCancel;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.DateEdit dateNgayMua;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.TextEdit txtNguoiNhanHang;
        private DevExpress.XtraEditors.DateEdit dateXuatTra;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraGrid.GridControl gcTraHang;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTraHang;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn36;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtSLTra;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTaiSanTra;
        private DevExpress.XtraEditors.TextEdit txtNCC;
        private DevExpress.XtraEditors.TextEdit txtPMH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rdateNgayNK;
        private DevExpress.XtraEditors.TextEdit txtNguoiTra;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}