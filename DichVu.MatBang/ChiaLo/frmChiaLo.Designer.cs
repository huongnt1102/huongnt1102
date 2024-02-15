namespace MatBang.ChiaLo
{
    partial class frmChiaLo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChiaLo));
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollectionAddInsertUpdate = new DevExpress.Utils.ImageCollection();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaMB = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDienTich = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spinSoLo = new DevExpress.XtraEditors.SpinEdit();
            this.gcChiaLo = new DevExpress.XtraGrid.GridControl();
            this.grvChiaLo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenLo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienTich = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinGiaChoThue = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookTrangThai = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookKhachHang = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colGiaThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaMB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spinDienTichConLai = new DevExpress.XtraEditors.SpinEdit();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionAddInsertUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienTich.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiaLo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiaLo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinGiaChoThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDienTichConLai.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.ImageOptions.ImageIndex = 1;
            this.btnThoat.ImageOptions.ImageList = this.imageCollectionAddInsertUpdate;
            this.btnThoat.Location = new System.Drawing.Point(499, 303);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(90, 30);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // imageCollectionAddInsertUpdate
            // 
            this.imageCollectionAddInsertUpdate.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionAddInsertUpdate.ImageStream")));
            this.imageCollectionAddInsertUpdate.Images.SetKeyName(0, "Save1.png");
            this.imageCollectionAddInsertUpdate.Images.SetKeyName(1, "Cancel1.png");
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(41, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Mặt bằng:";
            // 
            // txtMaMB
            // 
            this.txtMaMB.Location = new System.Drawing.Point(96, 9);
            this.txtMaMB.Name = "txtMaMB";
            this.txtMaMB.Properties.ReadOnly = true;
            this.txtMaMB.Size = new System.Drawing.Size(493, 20);
            this.txtMaMB.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(45, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Diện tích:";
            // 
            // txtDienTich
            // 
            this.txtDienTich.Location = new System.Drawing.Point(96, 35);
            this.txtDienTich.Name = "txtDienTich";
            this.txtDienTich.Properties.ReadOnly = true;
            this.txtDienTich.Size = new System.Drawing.Size(493, 20);
            this.txtDienTich.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 65);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Số lô muốn chia:";
            // 
            // spinSoLo
            // 
            this.spinSoLo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoLo.Location = new System.Drawing.Point(96, 57);
            this.spinSoLo.Name = "spinSoLo";
            this.spinSoLo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoLo.Size = new System.Drawing.Size(493, 20);
            this.spinSoLo.TabIndex = 0;
            this.spinSoLo.EditValueChanged += new System.EventHandler(this.spinSoLo_EditValueChanged);
            // 
            // gcChiaLo
            // 
            this.gcChiaLo.Location = new System.Drawing.Point(12, 97);
            this.gcChiaLo.MainView = this.grvChiaLo;
            this.gcChiaLo.Name = "gcChiaLo";
            this.gcChiaLo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookTrangThai,
            this.lookKhachHang,
            this.spinGiaChoThue});
            this.gcChiaLo.Size = new System.Drawing.Size(577, 200);
            this.gcChiaLo.TabIndex = 1;
            this.gcChiaLo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvChiaLo});
            // 
            // grvChiaLo
            // 
            this.grvChiaLo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenLo,
            this.colDienTich,
            this.colTrangThai,
            this.colKhachHang,
            this.colGiaThue,
            this.colDienGiai,
            this.colMaMB});
            this.grvChiaLo.GridControl = this.gcChiaLo;
            this.grvChiaLo.Name = "grvChiaLo";
            this.grvChiaLo.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvChiaLo.OptionsCustomization.AllowGroup = false;
            this.grvChiaLo.OptionsSelection.MultiSelect = true;
            this.grvChiaLo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvChiaLo.OptionsView.ShowGroupPanel = false;
            this.grvChiaLo.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.grvChiaLo_CellValueChanged);
            // 
            // colTenLo
            // 
            this.colTenLo.Caption = "Tên lô";
            this.colTenLo.FieldName = "TenLo";
            this.colTenLo.Name = "colTenLo";
            this.colTenLo.Visible = true;
            this.colTenLo.VisibleIndex = 0;
            // 
            // colDienTich
            // 
            this.colDienTich.Caption = "Diện tích";
            this.colDienTich.ColumnEdit = this.spinGiaChoThue;
            this.colDienTich.FieldName = "DienTich";
            this.colDienTich.Name = "colDienTich";
            this.colDienTich.Visible = true;
            this.colDienTich.VisibleIndex = 1;
            // 
            // spinGiaChoThue
            // 
            this.spinGiaChoThue.AutoHeight = false;
            this.spinGiaChoThue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinGiaChoThue.DisplayFormat.FormatString = "{0:#,0.#}";
            this.spinGiaChoThue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinGiaChoThue.EditFormat.FormatString = "{0:#,0.#}";
            this.spinGiaChoThue.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinGiaChoThue.Name = "spinGiaChoThue";
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "Trạng thái";
            this.colTrangThai.ColumnEdit = this.lookTrangThai;
            this.colTrangThai.FieldName = "MaTT";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 2;
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.AutoHeight = false;
            this.lookTrangThai.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "Trạn thái", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookTrangThai.DisplayMember = "TenTT";
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.NullText = "";
            this.lookTrangThai.ValueMember = "MaTT";
            // 
            // colKhachHang
            // 
            this.colKhachHang.Caption = "Khách hàng";
            this.colKhachHang.ColumnEdit = this.lookKhachHang;
            this.colKhachHang.FieldName = "MaKH";
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.Visible = true;
            this.colKhachHang.VisibleIndex = 3;
            // 
            // lookKhachHang
            // 
            this.lookKhachHang.AutoHeight = false;
            this.lookKhachHang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKhachHang.DisplayMember = "KhachHang";
            this.lookKhachHang.Name = "lookKhachHang";
            this.lookKhachHang.NullText = "";
            this.lookKhachHang.ValueMember = "MaKH";
            // 
            // colGiaThue
            // 
            this.colGiaThue.Caption = "Giá cho thuê";
            this.colGiaThue.ColumnEdit = this.spinGiaChoThue;
            this.colGiaThue.FieldName = "GiaThue";
            this.colGiaThue.Name = "colGiaThue";
            this.colGiaThue.Visible = true;
            this.colGiaThue.VisibleIndex = 4;
            // 
            // colDienGiai
            // 
            this.colDienGiai.Caption = "Diễn giải";
            this.colDienGiai.FieldName = "DienGiai";
            this.colDienGiai.Name = "colDienGiai";
            this.colDienGiai.Visible = true;
            this.colDienGiai.VisibleIndex = 5;
            // 
            // colMaMB
            // 
            this.colMaMB.Caption = "colMaMB";
            this.colMaMB.FieldName = "MaMB";
            this.colMaMB.Name = "colMaMB";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 306);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(78, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Diện tích còn lại:";
            // 
            // spinDienTichConLai
            // 
            this.spinDienTichConLai.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinDienTichConLai.Location = new System.Drawing.Point(96, 303);
            this.spinDienTichConLai.Name = "spinDienTichConLai";
            this.spinDienTichConLai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDienTichConLai.Properties.DisplayFormat.FormatString = "{0:#,0.#} m2";
            this.spinDienTichConLai.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDienTichConLai.Properties.EditFormat.FormatString = "{0:#,0.#} m2";
            this.spinDienTichConLai.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDienTichConLai.Properties.ReadOnly = true;
            this.spinDienTichConLai.Size = new System.Drawing.Size(83, 20);
            this.spinDienTichConLai.TabIndex = 2;
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.ImageOptions.ImageIndex = 0;
            this.btnChapNhan.ImageOptions.ImageList = this.imageCollectionAddInsertUpdate;
            this.btnChapNhan.Location = new System.Drawing.Point(403, 303);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(90, 30);
            this.btnChapNhan.TabIndex = 4;
            this.btnChapNhan.Text = "Chấp nhận";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // frmChiaLo
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(601, 340);
            this.Controls.Add(this.gcChiaLo);
            this.Controls.Add(this.spinDienTichConLai);
            this.Controls.Add(this.spinSoLo);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtDienTich);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtMaMB);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.btnThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChiaLo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chia lô";
            this.Load += new System.EventHandler(this.frmChiaLo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionAddInsertUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienTich.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiaLo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvChiaLo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinGiaChoThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDienTichConLai.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMaMB;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDienTich;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit spinSoLo;
        private DevExpress.XtraGrid.GridControl gcChiaLo;
        private DevExpress.XtraGrid.Views.Grid.GridView grvChiaLo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spinDienTichConLai;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private DevExpress.XtraGrid.Columns.GridColumn colTenLo;
        private DevExpress.XtraGrid.Columns.GridColumn colDienTich;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookTrangThai;
        private DevExpress.XtraGrid.Columns.GridColumn colKhachHang;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookKhachHang;
        private DevExpress.XtraGrid.Columns.GridColumn colGiaThue;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinGiaChoThue;
        private DevExpress.XtraGrid.Columns.GridColumn colDienGiai;
        private DevExpress.XtraGrid.Columns.GridColumn colMaMB;
        private DevExpress.Utils.ImageCollection imageCollectionAddInsertUpdate;
    }
}