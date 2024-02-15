namespace DichVu.ThueNgoai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThanhToan));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtGhiChu = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtsotien = new DevExpress.XtraEditors.SpinEdit();
            this.spinSoTienCanThanhToan = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gcThongTinNguoiThanhToan = new DevExpress.XtraEditors.GroupControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSoPhieu = new DevExpress.XtraEditors.TextEdit();
            this.txtHoVaTen = new DevExpress.XtraEditors.TextEdit();
            this.txtDiaChi = new DevExpress.XtraEditors.TextEdit();
            this.txtSoDienThoai = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.gcThanhToan = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtThangThanhToan = new DevExpress.XtraEditors.TextEdit();
            this.txtNhaCungCap = new DevExpress.XtraEditors.TextEdit();
            this.txtSoHD = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsotien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTienCanThanhToan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcThongTinNguoiThanhToan)).BeginInit();
            this.gcThongTinNguoiThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPhieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoVaTen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiaChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoDienThoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcThanhToan)).BeginInit();
            this.gcThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtThangThanhToan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNhaCungCap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Số tiền:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(366, 421);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_save.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_cancel1.png");  
            // 
            // btnOK
            // 
            this.btnOK.ImageOptions.ImageIndex = 0;
            this.btnOK.ImageOptions.ImageList = this.imageCollection1;
            this.btnOK.Location = new System.Drawing.Point(253, 421);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 30);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Lưu && Đóng";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(104, 154);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(352, 39);
            this.txtGhiChu.TabIndex = 0;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Lí do, mục đích của phiếu chi";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
            this.dxValidationProvider1.SetValidationRule(this.txtGhiChu, conditionValidationRule1);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 157);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Diễn giải:";
            // 
            // txtsotien
            // 
            this.txtsotien.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtsotien.Location = new System.Drawing.Point(104, 24);
            this.txtsotien.Name = "txtsotien";
            this.txtsotien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtsotien.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.txtsotien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtsotien.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtsotien.Size = new System.Drawing.Size(351, 20);
            this.txtsotien.TabIndex = 0;
            // 
            // spinSoTienCanThanhToan
            // 
            this.spinSoTienCanThanhToan.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoTienCanThanhToan.Enabled = false;
            this.spinSoTienCanThanhToan.Location = new System.Drawing.Point(104, 49);
            this.spinSoTienCanThanhToan.Name = "spinSoTienCanThanhToan";
            this.spinSoTienCanThanhToan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoTienCanThanhToan.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinSoTienCanThanhToan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoTienCanThanhToan.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinSoTienCanThanhToan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinSoTienCanThanhToan.Size = new System.Drawing.Size(351, 20);
            this.spinSoTienCanThanhToan.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 53);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(79, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Cần thanh toán:";
            // 
            // gcThongTinNguoiThanhToan
            // 
            this.gcThongTinNguoiThanhToan.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcThongTinNguoiThanhToan.AppearanceCaption.Options.UseFont = true;
            this.gcThongTinNguoiThanhToan.Controls.Add(this.labelControl7);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.labelControl6);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.txtGhiChu);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.labelControl2);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.labelControl5);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.labelControl8);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.labelControl4);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.txtSoPhieu);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.txtHoVaTen);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.txtDiaChi);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.txtSoDienThoai);
            this.gcThongTinNguoiThanhToan.Controls.Add(this.txtEmail);
            this.gcThongTinNguoiThanhToan.Location = new System.Drawing.Point(12, 12);
            this.gcThongTinNguoiThanhToan.Name = "gcThongTinNguoiThanhToan";
            this.gcThongTinNguoiThanhToan.Size = new System.Drawing.Size(461, 203);
            this.gcThongTinNguoiThanhToan.TabIndex = 5;
            this.gcThongTinNguoiThanhToan.Text = "Người thanh toán";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 131);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(28, 13);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "Email:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 105);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(66, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Số điện thoại:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 79);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Địa chỉ:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 27);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(45, 13);
            this.labelControl8.TabIndex = 6;
            this.labelControl8.Text = "Số phiếu:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 53);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(51, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Họ và tên:";
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.Enabled = false;
            this.txtSoPhieu.Location = new System.Drawing.Point(104, 24);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Properties.ReadOnly = true;
            this.txtSoPhieu.Size = new System.Drawing.Size(352, 20);
            this.txtSoPhieu.TabIndex = 0;
            // 
            // txtHoVaTen
            // 
            this.txtHoVaTen.Location = new System.Drawing.Point(104, 50);
            this.txtHoVaTen.Name = "txtHoVaTen";
            this.txtHoVaTen.Properties.ReadOnly = true;
            this.txtHoVaTen.Size = new System.Drawing.Size(352, 20);
            this.txtHoVaTen.TabIndex = 1;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(104, 76);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Properties.ReadOnly = true;
            this.txtDiaChi.Size = new System.Drawing.Size(352, 20);
            this.txtDiaChi.TabIndex = 2;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(104, 102);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Properties.ReadOnly = true;
            this.txtSoDienThoai.Size = new System.Drawing.Size(352, 20);
            this.txtSoDienThoai.TabIndex = 3;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(104, 128);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(352, 20);
            this.txtEmail.TabIndex = 4;
            // 
            // gcThanhToan
            // 
            this.gcThanhToan.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcThanhToan.AppearanceCaption.Options.UseFont = true;
            this.gcThanhToan.Controls.Add(this.txtsotien);
            this.gcThanhToan.Controls.Add(this.labelControl1);
            this.gcThanhToan.Controls.Add(this.labelControl3);
            this.gcThanhToan.Controls.Add(this.spinSoTienCanThanhToan);
            this.gcThanhToan.Location = new System.Drawing.Point(12, 221);
            this.gcThanhToan.Name = "gcThanhToan";
            this.gcThanhToan.Size = new System.Drawing.Size(461, 79);
            this.gcThanhToan.TabIndex = 5;
            this.gcThanhToan.Text = "Thanh toán";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.txtThangThanhToan);
            this.groupControl1.Controls.Add(this.txtNhaCungCap);
            this.groupControl1.Controls.Add(this.txtSoHD);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.labelControl13);
            this.groupControl1.Location = new System.Drawing.Point(12, 306);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(461, 109);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Thông tin hợp đồng";
            // 
            // txtThangThanhToan
            // 
            this.txtThangThanhToan.Location = new System.Drawing.Point(104, 76);
            this.txtThangThanhToan.Name = "txtThangThanhToan";
            this.txtThangThanhToan.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.txtThangThanhToan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtThangThanhToan.Properties.ReadOnly = true;
            this.txtThangThanhToan.Size = new System.Drawing.Size(351, 20);
            this.txtThangThanhToan.TabIndex = 2;
            // 
            // txtNhaCungCap
            // 
            this.txtNhaCungCap.Location = new System.Drawing.Point(104, 50);
            this.txtNhaCungCap.Name = "txtNhaCungCap";
            this.txtNhaCungCap.Properties.ReadOnly = true;
            this.txtNhaCungCap.Size = new System.Drawing.Size(351, 20);
            this.txtNhaCungCap.TabIndex = 1;
            // 
            // txtSoHD
            // 
            this.txtSoHD.Location = new System.Drawing.Point(104, 24);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.Properties.ReadOnly = true;
            this.txtSoHD.Size = new System.Drawing.Size(351, 20);
            this.txtSoHD.TabIndex = 0;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(12, 79);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(49, 13);
            this.labelControl15.TabIndex = 0;
            this.labelControl15.Text = "Tháng TT:";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(12, 53);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(69, 13);
            this.labelControl14.TabIndex = 0;
            this.labelControl14.Text = "Nhà cung cấp:";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(12, 27);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(33, 13);
            this.labelControl13.TabIndex = 0;
            this.labelControl13.Text = "Số HD:";
            // 
            // dxValidationProvider1
            // 
            this.dxValidationProvider1.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // frmThanhToan
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(485, 463);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gcThanhToan);
            this.Controls.Add(this.gcThongTinNguoiThanhToan);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThanhToan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu thanh toán";
            this.Load += new System.EventHandler(this.frmThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsotien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTienCanThanhToan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcThongTinNguoiThanhToan)).EndInit();
            this.gcThongTinNguoiThanhToan.ResumeLayout(false);
            this.gcThongTinNguoiThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPhieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoVaTen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiaChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoDienThoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcThanhToan)).EndInit();
            this.gcThanhToan.ResumeLayout(false);
            this.gcThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtThangThanhToan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNhaCungCap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.MemoEdit txtGhiChu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit txtsotien;
        private DevExpress.XtraEditors.SpinEdit spinSoTienCanThanhToan;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl gcThongTinNguoiThanhToan;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl gcThanhToan;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtSoHD;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txtNhaCungCap;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.TextEdit txtThangThanhToan;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.TextEdit txtHoVaTen;
        private DevExpress.XtraEditors.TextEdit txtDiaChi;
        private DevExpress.XtraEditors.TextEdit txtSoDienThoai;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtSoPhieu;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}