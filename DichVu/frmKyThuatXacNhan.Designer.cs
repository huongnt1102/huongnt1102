﻿namespace DichVu
{
    partial class frmKyThuatXacNhan
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinTongTien = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinTienCK = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinTyLeCK = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinTienVAT = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinTyLeVAT = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinThanhTien = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinDonGia = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinSL = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvChiTiet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcChiTiet = new DevExpress.XtraGrid.GridControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.txtGhiChu = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTongTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienCK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyLeCK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyLeVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinThanhTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "ID";
            this.gridColumn12.FieldName = "ID";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            // 
            // spinTongTien
            // 
            this.spinTongTien.AutoHeight = false;
            this.spinTongTien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTongTien.DisplayFormat.FormatString = "{0:n0}";
            this.spinTongTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTongTien.EditFormat.FormatString = "{0:n0}";
            this.spinTongTien.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTongTien.Name = "spinTongTien";
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Tổng tiền";
            this.gridColumn10.ColumnEdit = this.spinTongTien;
            this.gridColumn10.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "TongTien";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            this.gridColumn10.Width = 128;
            // 
            // spinTienCK
            // 
            this.spinTienCK.AutoHeight = false;
            this.spinTienCK.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTienCK.DisplayFormat.FormatString = "{0:n0}";
            this.spinTienCK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienCK.EditFormat.FormatString = "{0:n0}";
            this.spinTienCK.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienCK.Name = "spinTienCK";
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "Tiền CK";
            this.gridColumn9.ColumnEdit = this.spinTienCK;
            this.gridColumn9.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "TienCK";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 100;
            // 
            // spinTyLeCK
            // 
            this.spinTyLeCK.AutoHeight = false;
            this.spinTyLeCK.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTyLeCK.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinTyLeCK.Name = "spinTyLeCK";
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Tỷ lệ CK";
            this.gridColumn8.ColumnEdit = this.spinTyLeCK;
            this.gridColumn8.DisplayFormat.FormatString = "{p}";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "TyLeCK";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 56;
            // 
            // spinTienVAT
            // 
            this.spinTienVAT.AutoHeight = false;
            this.spinTienVAT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTienVAT.DisplayFormat.FormatString = "{0:n0}";
            this.spinTienVAT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienVAT.EditFormat.FormatString = "{0:n0}";
            this.spinTienVAT.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienVAT.Name = "spinTienVAT";
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Tiền VAT";
            this.gridColumn7.ColumnEdit = this.spinTienVAT;
            this.gridColumn7.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "TienVAT";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 103;
            // 
            // spinTyLeVAT
            // 
            this.spinTyLeVAT.AutoHeight = false;
            this.spinTyLeVAT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTyLeVAT.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinTyLeVAT.Name = "spinTyLeVAT";
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Tỷ lệ VAT";
            this.gridColumn6.ColumnEdit = this.spinTyLeVAT;
            this.gridColumn6.DisplayFormat.FormatString = "{p}";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "TyleVAT";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 63;
            // 
            // spinThanhTien
            // 
            this.spinThanhTien.AutoHeight = false;
            this.spinThanhTien.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinThanhTien.DisplayFormat.FormatString = "{0:n0}";
            this.spinThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThanhTien.EditFormat.FormatString = "{0:n0}";
            this.spinThanhTien.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThanhTien.Name = "spinThanhTien";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Thành tiền";
            this.gridColumn5.ColumnEdit = this.spinThanhTien;
            this.gridColumn5.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "ThanhTien";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 123;
            // 
            // spinDonGia
            // 
            this.spinDonGia.AutoHeight = false;
            this.spinDonGia.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinDonGia.DisplayFormat.FormatString = "{0:n0}";
            this.spinDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia.EditFormat.FormatString = "{0:n0}";
            this.spinDonGia.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinDonGia.Name = "spinDonGia";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Đơn giá";
            this.gridColumn4.ColumnEdit = this.spinDonGia;
            this.gridColumn4.DisplayFormat.FormatString = "{0:n0}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "DonGia";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 106;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Số lượng";
            this.gridColumn3.ColumnEdit = this.spinSL;
            this.gridColumn3.FieldName = "SoLuong";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 67;
            // 
            // spinSL
            // 
            this.spinSL.AutoHeight = false;
            this.spinSL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSL.Name = "spinSL";
            this.spinSL.EditValueChanged += new System.EventHandler(this.spinSL_EditValueChanged);
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Vật phẩm";
            this.gridColumn2.FieldName = "VatPham";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 158;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "STT";
            this.gridColumn1.FieldName = "STT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 58;
            // 
            // gvChiTiet
            // 
            this.gvChiTiet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvChiTiet.GridControl = this.gcChiTiet;
            this.gvChiTiet.Name = "gvChiTiet";
            this.gvChiTiet.OptionsView.ColumnAutoWidth = false;
            this.gvChiTiet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvChiTiet.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "Ghi chú";
            this.gridColumn11.ColumnEdit = this.txtGhiChu;
            this.gridColumn11.FieldName = "KTGhiChu";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            this.gridColumn11.Width = 128;
            // 
            // gcChiTiet
            // 
            this.gcChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.gcChiTiet.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcChiTiet.Location = new System.Drawing.Point(0, 24);
            this.gcChiTiet.MainView = this.gvChiTiet;
            this.gcChiTiet.Name = "gcChiTiet";
            this.gcChiTiet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinDonGia,
            this.spinTyLeVAT,
            this.spinTienVAT,
            this.spinTyLeCK,
            this.spinTienCK,
            this.spinThanhTien,
            this.spinTongTien,
            this.spinSL,
            this.txtGhiChu});
            this.gcChiTiet.ShowOnlyPredefinedDetails = true;
            this.gcChiTiet.Size = new System.Drawing.Size(1111, 261);
            this.gcChiTiet.TabIndex = 22;
            this.gcChiTiet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChiTiet});
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 261);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSave});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Lưu và gửi khách hàng";
            this.btnSave.Id = 0;
            this.btnSave.ImageOptions.SvgImage = global::DichVu.Properties.Resources.checkbox;
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1111, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 285);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1111, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1111, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 261);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.AutoHeight = false;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.EditValueChanged += new System.EventHandler(this.txtGhiChu_EditValueChanged);
            // 
            // frmKyThuatXacNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 285);
            this.Controls.Add(this.gcChiTiet);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmKyThuatXacNhan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KỸ THUẬT XÁC NHẬN HOÀN THÀNH";
            this.Load += new System.EventHandler(this.frmKyThuatXacNhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinTongTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienCK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyLeCK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTyLeVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinThanhTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTongTien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTienCK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTyLeCK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTienVAT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinTyLeVAT;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChiTiet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.GridControl gcChiTiet;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinSL;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtGhiChu;
    }
}