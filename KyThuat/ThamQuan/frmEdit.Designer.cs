namespace KyThuat.ThamQuan
{
    partial class frmEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEdit));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaSoTQ = new DevExpress.XtraEditors.TextEdit();
            this.dateNgayTQ = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookNhanVien = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.gcTaiSan = new DevExpress.XtraGrid.GridControl();
            this.grvTaiSan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.slookTaiSan = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTaiSan = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lookMatBang = new DevExpress.XtraEditors.LookUpEdit();
            this.lookTangLau = new DevExpress.XtraEditors.LookUpEdit();
            this.lookKhoiNha = new DevExpress.XtraEditors.LookUpEdit();
            this.lookTN = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSoTQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTQ.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slookTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTaiSan)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTangLau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhoiNha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã số:";
            // 
            // txtMaSoTQ
            // 
            this.txtMaSoTQ.Location = new System.Drawing.Point(94, 20);
            this.txtMaSoTQ.Name = "txtMaSoTQ";
            this.txtMaSoTQ.Size = new System.Drawing.Size(149, 20);
            this.txtMaSoTQ.TabIndex = 1;
            // 
            // dateNgayTQ
            // 
            this.dateNgayTQ.EditValue = null;
            this.dateNgayTQ.Location = new System.Drawing.Point(94, 46);
            this.dateNgayTQ.Name = "dateNgayTQ";
            this.dateNgayTQ.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayTQ.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayTQ.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy | hh:mm tt}";
            this.dateNgayTQ.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayTQ.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy | hh:mm tt}";
            this.dateNgayTQ.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayTQ.Properties.Mask.EditMask = "dd/MM/yyyy | hh:mm tt";
            this.dateNgayTQ.Size = new System.Drawing.Size(149, 20);
            this.dateNgayTQ.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 49);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(77, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Ngày thực hiện:";
            // 
            // lookNhanVien
            // 
            this.lookNhanVien.Location = new System.Drawing.Point(326, 20);
            this.lookNhanVien.Name = "lookNhanVien";
            this.lookNhanVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhanVien.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNV", "Nhân viên", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookNhanVien.Properties.DisplayMember = "HoTenNV";
            this.lookNhanVien.Properties.NullText = "";
            this.lookNhanVien.Properties.ShowHeader = false;
            this.lookNhanVien.Properties.ShowLines = false;
            this.lookNhanVien.Properties.ValueMember = "MaNV";
            this.lookNhanVien.Size = new System.Drawing.Size(167, 20);
            this.lookNhanVien.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(268, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Nhân viên:";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(94, 72);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(399, 43);
            this.txtDienGiai.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 75);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Diễn giải:";
            // 
            // gcTaiSan
            // 
            this.gcTaiSan.Location = new System.Drawing.Point(11, 136);
            this.gcTaiSan.MainView = this.grvTaiSan;
            this.gcTaiSan.Name = "gcTaiSan";
            this.gcTaiSan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookTrangThai,
            this.lookTaiSan,
            this.slookTaiSan});
            this.gcTaiSan.Size = new System.Drawing.Size(758, 200);
            this.gcTaiSan.TabIndex = 5;
            this.gcTaiSan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvTaiSan});
            // 
            // grvTaiSan
            // 
            this.grvTaiSan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.grvTaiSan.GridControl = this.gcTaiSan;
            this.grvTaiSan.Name = "grvTaiSan";
            this.grvTaiSan.OptionsCustomization.AllowGroup = false;
            this.grvTaiSan.OptionsSelection.MultiSelect = true;
            this.grvTaiSan.OptionsView.ColumnAutoWidth = false;
            this.grvTaiSan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvTaiSan.OptionsView.ShowGroupPanel = false;
            this.grvTaiSan.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grvTaiSan.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvTaiSan_InitNewRow);
            this.grvTaiSan.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvTaiSan_CellValueChanging);
            this.grvTaiSan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvTaiSan_KeyUp);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tài sản";
            this.gridColumn1.ColumnEdit = this.slookTaiSan;
            this.gridColumn1.FieldName = "MaTS";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 112;
            // 
            // slookTaiSan
            // 
            this.slookTaiSan.AutoHeight = false;
            this.slookTaiSan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slookTaiSan.DisplayMember = "TenTS";
            this.slookTaiSan.Name = "slookTaiSan";
            this.slookTaiSan.NullText = "";
            this.slookTaiSan.PopupFormSize = new System.Drawing.Size(600, 300);
            this.slookTaiSan.PopupView = this.repositoryItemSearchLookUpEdit1View;
            this.slookTaiSan.ValueMember = "ID";
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ColumnAutoWidth = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Tài sản";
            this.gridColumn4.FieldName = "TenTS";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            this.gridColumn4.Width = 123;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Loại tài sản";
            this.gridColumn5.FieldName = "TenLTS";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 89;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Đơn vị SD";
            this.gridColumn6.FieldName = "TenDVSD";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 126;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Mặt bằng";
            this.gridColumn7.FieldName = "MaSoMB";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 107;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Nhà sản xuất";
            this.gridColumn8.FieldName = "NhaSanXuat";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 175;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Trạng thái";
            this.gridColumn9.FieldName = "TenTT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            this.gridColumn9.Width = 98;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Trạng thái";
            this.gridColumn2.ColumnEdit = this.lookTrangThai;
            this.gridColumn2.FieldName = "MaTT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 125;
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.AutoHeight = false;
            this.lookTrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "Trạng thái")});
            this.lookTrangThai.DisplayMember = "TenTT";
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.NullText = "";
            this.lookTrangThai.ShowHeader = false;
            this.lookTrangThai.ValueMember = "MaTT";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Diễn giải";
            this.gridColumn3.FieldName = "DienGiai";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 483;
            // 
            // lookTaiSan
            // 
            this.lookTaiSan.AutoHeight = false;
            this.lookTaiSan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTaiSan.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaTS", "Mã tài sản", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTS", "Tài sản", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookTaiSan.DisplayMember = "TenTS";
            this.lookTaiSan.Name = "lookTaiSan";
            this.lookTaiSan.NullText = "";
            this.lookTaiSan.ShowHeader = false;
            this.lookTaiSan.ShowLines = false;
            this.lookTaiSan.ValueMember = "ID";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(694, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageIndex = 0;
            this.btnSave.ImageOptions.ImageList = this.imageCollection1;
            this.btnSave.Location = new System.Drawing.Point(596, 342);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaSoTQ);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.dateNgayTQ);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.txtDienGiai);
            this.groupBox1.Controls.Add(this.lookNhanVien);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(11, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 125);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin chung .";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl8);
            this.groupBox2.Controls.Add(this.lookMatBang);
            this.groupBox2.Controls.Add(this.lookTangLau);
            this.groupBox2.Controls.Add(this.lookKhoiNha);
            this.groupBox2.Controls.Add(this.lookTN);
            this.groupBox2.Controls.Add(this.labelControl7);
            this.groupBox2.Controls.Add(this.labelControl6);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(517, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 125);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vị trí .";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(10, 98);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(49, 13);
            this.labelControl8.TabIndex = 11;
            this.labelControl8.Text = "Mặt bằng:";
            // 
            // lookMatBang
            // 
            this.lookMatBang.Location = new System.Drawing.Point(74, 95);
            this.lookMatBang.Name = "lookMatBang";
            this.lookMatBang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookMatBang.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoMB", "Mặt bằng", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookMatBang.Properties.DisplayMember = "MaSoMB";
            this.lookMatBang.Properties.NullText = "";
            this.lookMatBang.Properties.ShowHeader = false;
            this.lookMatBang.Properties.ShowLines = false;
            this.lookMatBang.Properties.ValueMember = "MaMB";
            this.lookMatBang.Size = new System.Drawing.Size(173, 20);
            this.lookMatBang.TabIndex = 10;
            this.lookMatBang.EditValueChanged += new System.EventHandler(this.lookMatBang_EditValueChanged);
            // 
            // lookTangLau
            // 
            this.lookTangLau.Location = new System.Drawing.Point(74, 69);
            this.lookTangLau.Name = "lookTangLau";
            this.lookTangLau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTangLau.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTL", "Tầng lầu", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookTangLau.Properties.DisplayMember = "TenTL";
            this.lookTangLau.Properties.NullText = "";
            this.lookTangLau.Properties.ShowHeader = false;
            this.lookTangLau.Properties.ShowLines = false;
            this.lookTangLau.Properties.ValueMember = "MaTL";
            this.lookTangLau.Size = new System.Drawing.Size(173, 20);
            this.lookTangLau.TabIndex = 9;
            this.lookTangLau.EditValueChanged += new System.EventHandler(this.lookTangLau_EditValueChanged);
            // 
            // lookKhoiNha
            // 
            this.lookKhoiNha.Location = new System.Drawing.Point(74, 43);
            this.lookKhoiNha.Name = "lookKhoiNha";
            this.lookKhoiNha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKhoiNha.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKN", "Khối nhà", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookKhoiNha.Properties.DisplayMember = "TenKN";
            this.lookKhoiNha.Properties.NullText = "";
            this.lookKhoiNha.Properties.ShowHeader = false;
            this.lookKhoiNha.Properties.ShowLines = false;
            this.lookKhoiNha.Properties.ValueMember = "MaKN";
            this.lookKhoiNha.Size = new System.Drawing.Size(173, 20);
            this.lookKhoiNha.TabIndex = 8;
            this.lookKhoiNha.EditValueChanged += new System.EventHandler(this.lookKhoiNha_EditValueChanged);
            // 
            // lookTN
            // 
            this.lookTN.Location = new System.Drawing.Point(74, 17);
            this.lookTN.Name = "lookTN";
            this.lookTN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTN.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Dự án")});
            this.lookTN.Properties.DisplayMember = "TenTN";
            this.lookTN.Properties.NullText = "";
            this.lookTN.Properties.ShowHeader = false;
            this.lookTN.Properties.ShowLines = false;
            this.lookTN.Properties.ValueMember = "MaTN";
            this.lookTN.Size = new System.Drawing.Size(173, 20);
            this.lookTN.TabIndex = 5;
            this.lookTN.EditValueChanged += new System.EventHandler(this.lookTN_EditValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(10, 72);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(41, 13);
            this.labelControl7.TabIndex = 7;
            this.labelControl7.Text = "Tầng lầu";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(6, 46);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(45, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Khối nhà:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 20);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(29, 13);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "Dự án";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // frmEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(777, 372);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gcTaiSan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tham quan Dự án";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSoTQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTQ.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slookTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTaiSan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTangLau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhoiNha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMaSoTQ;
        private DevExpress.XtraEditors.DateEdit dateNgayTQ;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookNhanVien;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraGrid.GridControl gcTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView grvTaiSan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTrangThai;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTaiSan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lookMatBang;
        private DevExpress.XtraEditors.LookUpEdit lookTangLau;
        private DevExpress.XtraEditors.LookUpEdit lookKhoiNha;
        private DevExpress.XtraEditors.LookUpEdit lookTN;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit slookTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}