namespace DIPCRM.NhuCau
{
    partial class frmAddProduct
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateNgayDH = new DevExpress.XtraEditors.DateEdit();
            this.btnProduct = new DevExpress.XtraEditors.ButtonEdit();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.spinSoLuong = new DevExpress.XtraEditors.SpinEdit();
            this.spinChietKhau = new DevExpress.XtraEditors.SpinEdit();
            this.spinThanhTien = new DevExpress.XtraEditors.SpinEdit();
            this.spinDonGia = new DevExpress.XtraEditors.SpinEdit();
            this.txtTenSP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDH.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinChietKhau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinThanhTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenSP.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateNgayDH);
            this.panelControl1.Controls.Add(this.btnProduct);
            this.panelControl1.Controls.Add(this.txtDienGiai);
            this.panelControl1.Controls.Add(this.spinSoLuong);
            this.panelControl1.Controls.Add(this.spinChietKhau);
            this.panelControl1.Controls.Add(this.spinThanhTien);
            this.panelControl1.Controls.Add(this.spinDonGia);
            this.panelControl1.Controls.Add(this.txtTenSP);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(511, 303);
            this.panelControl1.TabIndex = 0;
            // 
            // dateNgayDH
            // 
            this.dateNgayDH.EditValue = null;
            this.dateNgayDH.Location = new System.Drawing.Point(17, 115);
            this.dateNgayDH.Name = "dateNgayDH";
            this.dateNgayDH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayDH.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayDH.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayDH.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayDH.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayDH.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateNgayDH.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayDH.Size = new System.Drawing.Size(134, 20);
            this.dateNgayDH.TabIndex = 5;
            // 
            // btnProduct
            // 
            this.btnProduct.Location = new System.Drawing.Point(17, 25);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Chọn", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btnProduct.Properties.ReadOnly = true;
            this.btnProduct.Size = new System.Drawing.Size(134, 20);
            this.btnProduct.TabIndex = 0;
            this.btnProduct.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnProduct_ButtonClick);
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(17, 160);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(476, 127);
            this.txtDienGiai.TabIndex = 6;
            // 
            // spinSoLuong
            // 
            this.spinSoLuong.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoLuong.Location = new System.Drawing.Point(17, 70);
            this.spinSoLuong.Name = "spinSoLuong";
            this.spinSoLuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoLuong.Properties.DisplayFormat.FormatString = "n0";
            this.spinSoLuong.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoLuong.Properties.EditFormat.FormatString = "n0";
            this.spinSoLuong.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoLuong.Properties.MaxValue = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.spinSoLuong.Size = new System.Drawing.Size(134, 20);
            this.spinSoLuong.TabIndex = 1;
            this.spinSoLuong.EditValueChanged += new System.EventHandler(this.spinSoLuong_EditValueChanged);
            // 
            // spinChietKhau
            // 
            this.spinChietKhau.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinChietKhau.Location = new System.Drawing.Point(384, 70);
            this.spinChietKhau.Name = "spinChietKhau";
            this.spinChietKhau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinChietKhau.Properties.DisplayFormat.FormatString = "n0";
            this.spinChietKhau.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinChietKhau.Properties.EditFormat.FormatString = "n0";
            this.spinChietKhau.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinChietKhau.Properties.MaxValue = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.spinChietKhau.Size = new System.Drawing.Size(109, 20);
            this.spinChietKhau.TabIndex = 4;
            // 
            // spinThanhTien
            // 
            this.spinThanhTien.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinThanhTien.Location = new System.Drawing.Point(258, 70);
            this.spinThanhTien.Name = "spinThanhTien";
            this.spinThanhTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinThanhTien.Properties.DisplayFormat.FormatString = "n0";
            this.spinThanhTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThanhTien.Properties.EditFormat.FormatString = "n0";
            this.spinThanhTien.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThanhTien.Properties.MaxValue = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.spinThanhTien.Size = new System.Drawing.Size(122, 20);
            this.spinThanhTien.TabIndex = 3;
            // 
            // spinDonGia
            // 
            this.spinDonGia.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinDonGia.Location = new System.Drawing.Point(157, 70);
            this.spinDonGia.Name = "spinDonGia";
            this.spinDonGia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia.Properties.DisplayFormat.FormatString = "n0";
            this.spinDonGia.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia.Properties.EditFormat.FormatString = "n0";
            this.spinDonGia.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia.Properties.MaxValue = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.spinDonGia.Size = new System.Drawing.Size(95, 20);
            this.spinDonGia.TabIndex = 2;
            this.spinDonGia.EditValueChanged += new System.EventHandler(this.spinDonGia_EditValueChanged);
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(157, 25);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Properties.ReadOnly = true;
            this.txtTenSP.Size = new System.Drawing.Size(336, 20);
            this.txtTenSP.TabIndex = 13;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(17, 96);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(71, 13);
            this.labelControl8.TabIndex = 8;
            this.labelControl8.Text = "Ngày đặt hàng";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Số lượng";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(157, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Tên sản phẩm";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(379, 51);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(51, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "Chiết khấu";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(258, 51);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(51, 13);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "Thành tiền";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(157, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Đơn giá";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(17, 141);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(40, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Diễn giải";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Mã sản phẩm (*)";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DIPCRM.Need.Properties.Resources.Luu;
            this.btnSave.Location = new System.Drawing.Point(367, 321);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::DIPCRM.Need.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(448, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 358);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddProduct";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật <Sản phẩm>";
            this.Load += new System.EventHandler(this.frmAddProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDH.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinChietKhau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinThanhTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenSP.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.SpinEdit spinDonGia;
        private DevExpress.XtraEditors.TextEdit txtTenSP;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit btnProduct;
        private DevExpress.XtraEditors.SpinEdit spinSoLuong;
        private DevExpress.XtraEditors.SpinEdit spinChietKhau;
        private DevExpress.XtraEditors.SpinEdit spinThanhTien;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.DateEdit dateNgayDH;
    }
}