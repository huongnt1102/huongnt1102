namespace TaiSan
{
    partial class frmLoaiTaiSanChiTiet
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lookLTSCaptren = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lookToaNha = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDacTinh = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gcChiTiet = new DevExpress.XtraEditors.GroupControl();
            this.btnChonHinh = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.Qu = new DevExpress.XtraEditors.LabelControl();
            this.lookDVT = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lookType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtKyHieu = new DevExpress.XtraEditors.TextEdit();
            this.txtTenLTS = new DevExpress.XtraEditors.TextEdit();
            this.txtQuocGia = new DevExpress.XtraEditors.TextEdit();
            this.txtHinhAnh = new System.Windows.Forms.TextBox();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookLTSCaptren.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDacTinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).BeginInit();
            this.gcChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookDVT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKyHieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenLTS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuocGia.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.txtTenLTS);
            this.groupControl1.Controls.Add(this.txtKyHieu);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.lookLTSCaptren);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lookToaNha);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 13);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(531, 98);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Tài sản";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 63);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(35, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Ký hiệu";
            // 
            // lookLTSCaptren
            // 
            this.lookLTSCaptren.Location = new System.Drawing.Point(343, 34);
            this.lookLTSCaptren.Name = "lookLTSCaptren";
            this.lookLTSCaptren.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLTSCaptren.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KyHieu", "Ký hiệu"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLTS", "Tên loại TS")});
            this.lookLTSCaptren.Properties.DisplayMember = "TenLTS";
            this.lookLTSCaptren.Properties.NullText = "[Loại tài sản...]";
            this.lookLTSCaptren.Properties.ValueMember = "MaLTS";
            this.lookLTSCaptren.Size = new System.Drawing.Size(176, 20);
            this.lookLTSCaptren.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(255, 37);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(66, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "LTS Cấp trên:";
            // 
            // lookToaNha
            // 
            this.lookToaNha.Location = new System.Drawing.Point(83, 34);
            this.lookToaNha.Name = "lookToaNha";
            this.lookToaNha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookToaNha.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenVT", "Tên viết tắt"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Tên tòa nhà")});
            this.lookToaNha.Properties.DisplayMember = "TenTN";
            this.lookToaNha.Properties.NullText = "[Tòa nhà...]";
            this.lookToaNha.Properties.ValueMember = "MaTN";
            this.lookToaNha.Size = new System.Drawing.Size(161, 20);
            this.lookToaNha.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 37);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(43, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Tòa nhà:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(255, 63);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên loại tài sản:";
            // 
            // txtDacTinh
            // 
            this.txtDacTinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDacTinh.Location = new System.Drawing.Point(83, 111);
            this.txtDacTinh.Name = "txtDacTinh";
            this.txtDacTinh.Size = new System.Drawing.Size(254, 80);
            this.txtDacTinh.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 114);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Đặc tính:";
            // 
            // gcChiTiet
            // 
            this.gcChiTiet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gcChiTiet.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcChiTiet.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.gcChiTiet.AppearanceCaption.Options.UseFont = true;
            this.gcChiTiet.AppearanceCaption.Options.UseForeColor = true;
            this.gcChiTiet.AppearanceCaption.Options.UseTextOptions = true;
            this.gcChiTiet.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcChiTiet.Controls.Add(this.txtQuocGia);
            this.gcChiTiet.Controls.Add(this.pictureBox1);
            this.gcChiTiet.Controls.Add(this.btnChonHinh);
            this.gcChiTiet.Controls.Add(this.txtHinhAnh);
            this.gcChiTiet.Controls.Add(this.labelControl8);
            this.gcChiTiet.Controls.Add(this.Qu);
            this.gcChiTiet.Controls.Add(this.lookDVT);
            this.gcChiTiet.Controls.Add(this.labelControl7);
            this.gcChiTiet.Controls.Add(this.lookType);
            this.gcChiTiet.Controls.Add(this.labelControl6);
            this.gcChiTiet.Controls.Add(this.txtDacTinh);
            this.gcChiTiet.Controls.Add(this.labelControl2);
            this.gcChiTiet.Location = new System.Drawing.Point(12, 120);
            this.gcChiTiet.Name = "gcChiTiet";
            this.gcChiTiet.Size = new System.Drawing.Size(531, 208);
            this.gcChiTiet.TabIndex = 0;
            this.gcChiTiet.Text = "Thông tin chi tiết";
            // 
            // btnChonHinh
            // 
            this.btnChonHinh.Location = new System.Drawing.Point(255, 82);
            this.btnChonHinh.Name = "btnChonHinh";
            this.btnChonHinh.Size = new System.Drawing.Size(75, 23);
            this.btnChonHinh.TabIndex = 12;
            this.btnChonHinh.Text = "Chọn hình";
            this.btnChonHinh.Click += new System.EventHandler(this.btnChonHinh_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(20, 87);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(46, 13);
            this.labelControl8.TabIndex = 10;
            this.labelControl8.Text = "Hình ảnh:";
            // 
            // Qu
            // 
            this.Qu.Location = new System.Drawing.Point(20, 60);
            this.Qu.Name = "Qu";
            this.Qu.Size = new System.Drawing.Size(61, 13);
            this.Qu.TabIndex = 8;
            this.Qu.Text = "Quốc gia SX:";
            // 
            // lookDVT
            // 
            this.lookDVT.Location = new System.Drawing.Point(343, 31);
            this.lookDVT.Name = "lookDVT";
            this.lookDVT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookDVT.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDVT", "Đơn vị tính")});
            this.lookDVT.Properties.DisplayMember = "TenDVT";
            this.lookDVT.Properties.NullText = "[Đơn vị tính...]";
            this.lookDVT.Properties.ValueMember = "MaDVT";
            this.lookDVT.Size = new System.Drawing.Size(176, 20);
            this.lookDVT.TabIndex = 7;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(255, 38);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(56, 13);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "Đơn vị tính:";
            // 
            // lookType
            // 
            this.lookType.Location = new System.Drawing.Point(83, 31);
            this.lookType.Name = "lookType";
            this.lookType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeNam", "Loại")});
            this.lookType.Properties.DisplayMember = "TypeNam";
            this.lookType.Properties.NullText = "[Loại...]";
            this.lookType.Properties.ValueMember = "TypeID";
            this.lookType.Size = new System.Drawing.Size(161, 20);
            this.lookType.TabIndex = 5;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(20, 34);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(23, 13);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "Loại:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(343, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 134);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // txtKyHieu
            // 
            this.txtKyHieu.Location = new System.Drawing.Point(83, 60);
            this.txtKyHieu.Name = "txtKyHieu";
            this.txtKyHieu.Size = new System.Drawing.Size(161, 20);
            this.txtKyHieu.TabIndex = 7;
            // 
            // txtTenLTS
            // 
            this.txtTenLTS.Location = new System.Drawing.Point(343, 60);
            this.txtTenLTS.Name = "txtTenLTS";
            this.txtTenLTS.Size = new System.Drawing.Size(176, 20);
            this.txtTenLTS.TabIndex = 8;
            // 
            // txtQuocGia
            // 
            this.txtQuocGia.Location = new System.Drawing.Point(83, 57);
            this.txtQuocGia.Name = "txtQuocGia";
            this.txtQuocGia.Size = new System.Drawing.Size(161, 20);
            this.txtQuocGia.TabIndex = 9;
            // 
            // txtHinhAnh
            // 
            this.txtHinhAnh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtHinhAnh.Enabled = false;
            this.txtHinhAnh.Location = new System.Drawing.Point(83, 84);
            this.txtHinhAnh.Name = "txtHinhAnh";
            this.txtHinhAnh.Size = new System.Drawing.Size(161, 21);
            this.txtHinhAnh.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(360, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu đóng";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(456, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmLoaiTaiSanChiTiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 370);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gcChiTiet);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoaiTaiSanChiTiet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết tài sản";
            this.Load += new System.EventHandler(this.frmLoaiTaiSanChiTiet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookLTSCaptren.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookToaNha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDacTinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChiTiet)).EndInit();
            this.gcChiTiet.ResumeLayout(false);
            this.gcChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookDVT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKyHieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenLTS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuocGia.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl gcChiTiet;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtDacTinh;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookToaNha;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookLTSCaptren;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit lookType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lookDVT;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl Qu;
        private DevExpress.XtraEditors.SimpleButton btnChonHinh;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.TextEdit txtTenLTS;
        private DevExpress.XtraEditors.TextEdit txtKyHieu;
        private DevExpress.XtraEditors.TextEdit txtQuocGia;
        private System.Windows.Forms.TextBox txtHinhAnh;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}