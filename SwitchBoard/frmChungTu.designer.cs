namespace DIP.SwitchBoard
{
    partial class frmChungTu
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.itemChungTu = new DevExpress.XtraBars.BarEditItem();
            this.lookLoaiChungTu = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemKyBaoCao = new DevExpress.XtraBars.BarEditItem();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.itemTuNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateTuNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemDenNgay = new DevExpress.XtraBars.BarEditItem();
            this.dateDenNgay = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.itemGetDaTa = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ItemLuu = new DevExpress.XtraEditors.SimpleButton();
            this.itemHuy = new DevExpress.XtraEditors.SimpleButton();
            this.gcChungTu = new DevExpress.XtraGrid.GridControl();
            this.gvChungTu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkCustomerEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.btnCoHoiAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnHopDongAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiChungTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChungTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChungTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkCustomerEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
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
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.itemChungTu,
            this.itemKyBaoCao,
            this.itemTuNgay,
            this.itemDenNgay,
            this.itemGetDaTa});
            this.barManager1.MaxItemId = 5;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookLoaiChungTu,
            this.cmbKyBaoCao,
            this.dateTuNgay,
            this.dateDenNgay});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.itemChungTu, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemKyBaoCao),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemTuNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemDenNgay),
            new DevExpress.XtraBars.LinkPersistInfo(this.itemGetDaTa)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // itemChungTu
            // 
            this.itemChungTu.Caption = "Loại chứng từ";
            this.itemChungTu.Edit = this.lookLoaiChungTu;
            this.itemChungTu.Id = 0;
            this.itemChungTu.Name = "itemChungTu";
            this.itemChungTu.Width = 131;
            // 
            // lookLoaiChungTu
            // 
            this.lookLoaiChungTu.AutoHeight = false;
            this.lookLoaiChungTu.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoaiChungTu.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLCT", "Tên chứng từ")});
            this.lookLoaiChungTu.DisplayMember = "TenLCT";
            this.lookLoaiChungTu.Name = "lookLoaiChungTu";
            this.lookLoaiChungTu.NullText = "";
            this.lookLoaiChungTu.ValueMember = "ID";
            // 
            // itemKyBaoCao
            // 
            this.itemKyBaoCao.Caption = "barEditItem2";
            this.itemKyBaoCao.Edit = this.cmbKyBaoCao;
            this.itemKyBaoCao.Id = 1;
            this.itemKyBaoCao.Name = "itemKyBaoCao";
            this.itemKyBaoCao.Width = 108;
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.AutoHeight = false;
            this.cmbKyBaoCao.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.NullText = "Kỳ báo cáo";
            // 
            // itemTuNgay
            // 
            this.itemTuNgay.Caption = "barEditItem3";
            this.itemTuNgay.Edit = this.dateTuNgay;
            this.itemTuNgay.Id = 2;
            this.itemTuNgay.Name = "itemTuNgay";
            this.itemTuNgay.Width = 80;
            this.itemTuNgay.EditValueChanged += new System.EventHandler(this.itemTuNgay_EditValueChanged);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.AutoHeight = false;
            this.dateTuNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.NullText = "Từ ngày";
            this.dateTuNgay.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // itemDenNgay
            // 
            this.itemDenNgay.Caption = "barEditItem4";
            this.itemDenNgay.Edit = this.dateDenNgay;
            this.itemDenNgay.Id = 3;
            this.itemDenNgay.Name = "itemDenNgay";
            this.itemDenNgay.Width = 80;
            this.itemDenNgay.EditValueChanged += new System.EventHandler(this.itemDenNgay_EditValueChanged);
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.AutoHeight = false;
            this.dateDenNgay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.NullText = "Đến ngày";
            this.dateDenNgay.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // itemGetDaTa
            // 
            this.itemGetDaTa.Caption = "Lấy dữ liệu";
            this.itemGetDaTa.Id = 4;
            this.itemGetDaTa.Name = "itemGetDaTa";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(731, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 362);
            this.barDockControlBottom.Size = new System.Drawing.Size(731, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 333);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(731, 29);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 333);
            // 
            // ItemLuu
            // 
            this.ItemLuu.Location = new System.Drawing.Point(565, 327);
            this.ItemLuu.Name = "ItemLuu";
            this.ItemLuu.Size = new System.Drawing.Size(73, 23);
            this.ItemLuu.TabIndex = 9;
            this.ItemLuu.Text = "Chọn";
            this.ItemLuu.Click += new System.EventHandler(this.ItemLuu_Click);
            // 
            // itemHuy
            // 
            this.itemHuy.Location = new System.Drawing.Point(646, 327);
            this.itemHuy.Name = "itemHuy";
            this.itemHuy.Size = new System.Drawing.Size(73, 23);
            this.itemHuy.TabIndex = 9;
            this.itemHuy.Text = "Hủy";
            this.itemHuy.Click += new System.EventHandler(this.itemHuy_Click);
            // 
            // gcChungTu
            // 
            this.gcChungTu.Location = new System.Drawing.Point(12, 35);
            this.gcChungTu.MainView = this.gvChungTu;
            this.gcChungTu.Name = "gcChungTu";
            this.gcChungTu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkCustomerEdit});
            this.gcChungTu.Size = new System.Drawing.Size(707, 286);
            this.gcChungTu.TabIndex = 14;
            this.gcChungTu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChungTu});
            // 
            // gvChungTu
            // 
            this.gvChungTu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn3});
            this.gvChungTu.GridControl = this.gcChungTu;
            this.gvChungTu.Name = "gvChungTu";
            this.gvChungTu.OptionsSelection.MultiSelect = true;
            this.gvChungTu.OptionsView.ShowAutoFilterRow = true;
            this.gvChungTu.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ngày chứng từ";
            this.gridColumn1.FieldName = "NgayCT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 83;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Số chứng từ";
            this.gridColumn2.FieldName = "SoCT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 116;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Nhân viên";
            this.gridColumn5.FieldName = "NhanVien";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 153;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Diễn giải";
            this.gridColumn3.FieldName = "DienGiai";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 337;
            // 
            // lkCustomerEdit
            // 
            this.lkCustomerEdit.AutoHeight = false;
            this.lkCustomerEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkCustomerEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenCongTy", "Name1")});
            this.lkCustomerEdit.DisplayMember = "TenCongTy";
            this.lkCustomerEdit.Name = "lkCustomerEdit";
            this.lkCustomerEdit.NullText = "";
            this.lkCustomerEdit.ShowHeader = false;
            this.lkCustomerEdit.ShowLines = false;
            this.lkCustomerEdit.ValueMember = "MaKH";
            // 
            // barManager2
            // 
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.MaxItemId = 0;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(731, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 362);
            this.barDockControl2.Size = new System.Drawing.Size(731, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Size = new System.Drawing.Size(0, 362);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(731, 0);
            this.barDockControl4.Size = new System.Drawing.Size(0, 362);
            // 
            // btnCoHoiAdd
            // 
            this.btnCoHoiAdd.Location = new System.Drawing.Point(486, 327);
            this.btnCoHoiAdd.Name = "btnCoHoiAdd";
            this.btnCoHoiAdd.Size = new System.Drawing.Size(73, 23);
            this.btnCoHoiAdd.TabIndex = 9;
            this.btnCoHoiAdd.Text = "Thêm cơ hội";
            this.btnCoHoiAdd.Click += new System.EventHandler(this.btnCoHoiAdd_Click);
            // 
            // btnHopDongAdd
            // 
            this.btnHopDongAdd.Location = new System.Drawing.Point(392, 327);
            this.btnHopDongAdd.Name = "btnHopDongAdd";
            this.btnHopDongAdd.Size = new System.Drawing.Size(88, 23);
            this.btnHopDongAdd.TabIndex = 9;
            this.btnHopDongAdd.Text = "Thêm hợp đồng";
            this.btnHopDongAdd.Click += new System.EventHandler(this.btnHopDongAdd_Click);
            // 
            // frmChungTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 362);
            this.Controls.Add(this.gcChungTu);
            this.Controls.Add(this.itemHuy);
            this.Controls.Add(this.btnHopDongAdd);
            this.Controls.Add(this.btnCoHoiAdd);
            this.Controls.Add(this.ItemLuu);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChungTu";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chứng từ tham chiếu";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiChungTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChungTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvChungTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkCustomerEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem itemChungTu;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookLoaiChungTu;
        private DevExpress.XtraBars.BarEditItem itemKyBaoCao;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbKyBaoCao;
        private DevExpress.XtraBars.BarEditItem itemTuNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateTuNgay;
        private DevExpress.XtraBars.BarEditItem itemDenNgay;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateDenNgay;
        private DevExpress.XtraBars.BarButtonItem itemGetDaTa;
        private DevExpress.XtraEditors.SimpleButton itemHuy;
        private DevExpress.XtraEditors.SimpleButton ItemLuu;
        private DevExpress.XtraGrid.GridControl gcChungTu;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChungTu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkCustomerEdit;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraEditors.SimpleButton btnCoHoiAdd;
        private DevExpress.XtraEditors.SimpleButton btnHopDongAdd;
    }
}