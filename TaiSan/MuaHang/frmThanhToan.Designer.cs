namespace TaiSan.MuaHang
{
    partial class frmThanhToan
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaSoMH = new DevExpress.XtraEditors.TextEdit();
            this.lookNhanVien = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtNguoiTT = new DevExpress.XtraEditors.TextEdit();
            this.dateNgayTT = new DevExpress.XtraEditors.DateEdit();
            this.spinSoTien = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSoMH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTT.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã số MH:";
            // 
            // txtMaSoMH
            // 
            this.txtMaSoMH.Location = new System.Drawing.Point(72, 12);
            this.txtMaSoMH.Name = "txtMaSoMH";
            this.txtMaSoMH.Properties.ReadOnly = true;
            this.txtMaSoMH.Size = new System.Drawing.Size(195, 20);
            this.txtMaSoMH.TabIndex = 0;
            // 
            // lookNhanVien
            // 
            this.lookNhanVien.Location = new System.Drawing.Point(72, 38);
            this.lookNhanVien.Name = "lookNhanVien";
            this.lookNhanVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhanVien.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoNV", 30, "Mã NV"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNV", 70, "Họ tên NV")});
            this.lookNhanVien.Properties.DisplayMember = "HoTenNV";
            this.lookNhanVien.Properties.NullText = "";
            this.lookNhanVien.Properties.ShowHeader = false;
            this.lookNhanVien.Properties.ShowLines = false;
            this.lookNhanVien.Properties.ValueMember = "MaNV";
            this.lookNhanVien.Size = new System.Drawing.Size(195, 20);
            this.lookNhanVien.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Nhân viên:";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(72, 64);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(565, 198);
            this.txtDienGiai.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Diễn giải:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::TaiSan.Properties.Resources.cancel;
            this.btnCancel.Location = new System.Drawing.Point(562, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::TaiSan.Properties.Resources.Save1;
            this.btnSave.Location = new System.Drawing.Point(464, 268);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNguoiTT
            // 
            this.txtNguoiTT.Location = new System.Drawing.Point(367, 12);
            this.txtNguoiTT.Name = "txtNguoiTT";
            this.txtNguoiTT.Size = new System.Drawing.Size(270, 20);
            this.txtNguoiTT.TabIndex = 11;
            // 
            // dateNgayTT
            // 
            this.dateNgayTT.EditValue = null;
            this.dateNgayTT.Location = new System.Drawing.Point(530, 38);
            this.dateNgayTT.Name = "dateNgayTT";
            this.dateNgayTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayTT.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayTT.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayTT.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateNgayTT.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateNgayTT.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayTT.Size = new System.Drawing.Size(107, 20);
            this.dateNgayTT.TabIndex = 12;
            // 
            // spinSoTien
            // 
            this.spinSoTien.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoTien.Location = new System.Drawing.Point(367, 38);
            this.spinSoTien.Name = "spinSoTien";
            this.spinSoTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoTien.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinSoTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoTien.Properties.EditFormat.FormatString = "{0:#,0.##}";
            this.spinSoTien.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoTien.Size = new System.Drawing.Size(110, 20);
            this.spinSoTien.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(273, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(88, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Người thanh toán:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(273, 41);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "Số tiền TT:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(483, 41);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "Ngày TT:";
            // 
            // frmThanhToan
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(650, 297);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.spinSoTien);
            this.Controls.Add(this.dateNgayTT);
            this.Controls.Add(this.txtNguoiTT);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtDienGiai);
            this.Controls.Add(this.lookNhanVien);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtMaSoMH);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThanhToan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thanh toán đơn hàng";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaSoMH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTT.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMaSoMH;
        private DevExpress.XtraEditors.LookUpEdit lookNhanVien;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtNguoiTT;
        private DevExpress.XtraEditors.DateEdit dateNgayTT;
        private DevExpress.XtraEditors.SpinEdit spinSoTien;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}