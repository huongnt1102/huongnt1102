namespace DichVu.PhiQuanLy
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.datePhiQuanLy = new DevExpress.XtraEditors.DateEdit();
            this.txtPhiQuaLy = new DevExpress.XtraEditors.SpinEdit();
            this.gcNguoiThanhToan = new DevExpress.XtraEditors.GroupControl();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.txtMatBang = new DevExpress.XtraEditors.TextEdit();
            this.txtDiaChi = new DevExpress.XtraEditors.TextEdit();
            this.txtNguoiNop = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            this.ckChuyenKhoan = new DevExpress.XtraEditors.CheckEdit();
            this.ckDaTTDu = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datePhiQuanLy.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datePhiQuanLy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhiQuaLy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiThanhToan)).BeginInit();
            this.gcNguoiThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatBang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiaChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckChuyenKhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDaTTDu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl19);
            this.groupControl1.Controls.Add(this.datePhiQuanLy);
            this.groupControl1.Controls.Add(this.txtPhiQuaLy);
            this.groupControl1.Location = new System.Drawing.Point(12, 173);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(460, 82);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Phí quản lý";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Thành tiền:";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(9, 31);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(34, 13);
            this.labelControl19.TabIndex = 0;
            this.labelControl19.Text = "Tháng:";
            // 
            // datePhiQuanLy
            // 
            this.datePhiQuanLy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datePhiQuanLy.EditValue = null;
            this.datePhiQuanLy.Location = new System.Drawing.Point(82, 28);
            this.datePhiQuanLy.Name = "datePhiQuanLy";
            this.datePhiQuanLy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datePhiQuanLy.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.datePhiQuanLy.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.datePhiQuanLy.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datePhiQuanLy.Properties.EditFormat.FormatString = "MM/yyyy";
            this.datePhiQuanLy.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datePhiQuanLy.Properties.Mask.EditMask = "MM/yyyy";
            this.datePhiQuanLy.Properties.NullText = "Chọn tháng thanh toán";
            this.datePhiQuanLy.Properties.ReadOnly = true;
            this.datePhiQuanLy.Properties.Popup += new System.EventHandler(this.datePhiQuanLy_Properties_Popup);
            this.datePhiQuanLy.Size = new System.Drawing.Size(373, 20);
            this.datePhiQuanLy.TabIndex = 0;
            this.datePhiQuanLy.EditValueChanged += new System.EventHandler(this.datePhiQuanLy_EditValueChanged);
            // 
            // txtPhiQuaLy
            // 
            this.txtPhiQuaLy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhiQuaLy.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPhiQuaLy.Location = new System.Drawing.Point(82, 54);
            this.txtPhiQuaLy.Name = "txtPhiQuaLy";
            this.txtPhiQuaLy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPhiQuaLy.Properties.DisplayFormat.FormatString = "{0:#,0.#}";
            this.txtPhiQuaLy.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPhiQuaLy.Properties.EditFormat.FormatString = "{0:#,0.#}";
            this.txtPhiQuaLy.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPhiQuaLy.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtPhiQuaLy.Properties.Mask.EditMask = "n0";
            this.txtPhiQuaLy.Size = new System.Drawing.Size(373, 20);
            this.txtPhiQuaLy.TabIndex = 1;
            // 
            // gcNguoiThanhToan
            // 
            this.gcNguoiThanhToan.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcNguoiThanhToan.AppearanceCaption.Options.UseFont = true;
            this.gcNguoiThanhToan.Controls.Add(this.txtDienGiai);
            this.gcNguoiThanhToan.Controls.Add(this.txtMatBang);
            this.gcNguoiThanhToan.Controls.Add(this.txtDiaChi);
            this.gcNguoiThanhToan.Controls.Add(this.txtNguoiNop);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl18);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl6);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl2);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl1);
            this.gcNguoiThanhToan.Location = new System.Drawing.Point(12, 12);
            this.gcNguoiThanhToan.Name = "gcNguoiThanhToan";
            this.gcNguoiThanhToan.Size = new System.Drawing.Size(460, 155);
            this.gcNguoiThanhToan.TabIndex = 0;
            this.gcNguoiThanhToan.Text = "Người thanh toán";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDienGiai.Location = new System.Drawing.Point(82, 105);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(373, 41);
            this.txtDienGiai.TabIndex = 3;
            // 
            // txtMatBang
            // 
            this.txtMatBang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMatBang.Enabled = false;
            this.txtMatBang.Location = new System.Drawing.Point(82, 79);
            this.txtMatBang.Name = "txtMatBang";
            this.txtMatBang.Size = new System.Drawing.Size(373, 20);
            this.txtMatBang.TabIndex = 2;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChi.Location = new System.Drawing.Point(82, 53);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(373, 20);
            this.txtDiaChi.TabIndex = 1;
            // 
            // txtNguoiNop
            // 
            this.txtNguoiNop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoiNop.Location = new System.Drawing.Point(82, 27);
            this.txtNguoiNop.Name = "txtNguoiNop";
            this.txtNguoiNop.Size = new System.Drawing.Size(373, 20);
            this.txtNguoiNop.TabIndex = 0;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(9, 108);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(44, 13);
            this.labelControl18.TabIndex = 0;
            this.labelControl18.Text = "Diễn giải:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(9, 82);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(49, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Mặt bằng:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Địa chỉ:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Người nộp:";
            // 
            // btnHuy
            // 
            this.btnHuy.ImageOptions.ImageIndex = 1;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(384, 278);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 30);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.ImageOptions.ImageIndex = 0;
            this.btnChapNhan.ImageOptions.ImageList = this.imageCollection1;
            this.btnChapNhan.Location = new System.Drawing.Point(282, 278);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(96, 30);
            this.btnChapNhan.TabIndex = 4;
            this.btnChapNhan.Text = "&Chấp nhận";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // ckChuyenKhoan
            // 
            this.ckChuyenKhoan.Location = new System.Drawing.Point(10, 261);
            this.ckChuyenKhoan.Name = "ckChuyenKhoan";
            this.ckChuyenKhoan.Properties.Caption = "Thanh toán bằng chuyển khoản";
            this.ckChuyenKhoan.Size = new System.Drawing.Size(215, 19);
            this.ckChuyenKhoan.TabIndex = 2;
            // 
            // ckDaTTDu
            // 
            this.ckDaTTDu.Location = new System.Drawing.Point(10, 286);
            this.ckDaTTDu.Name = "ckDaTTDu";
            this.ckDaTTDu.Properties.Caption = "Đã thanh toán đủ";
            this.ckDaTTDu.Size = new System.Drawing.Size(215, 19);
            this.ckDaTTDu.TabIndex = 3;
            // 
            // frmThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 320);
            this.Controls.Add(this.ckDaTTDu);
            this.Controls.Add(this.ckChuyenKhoan);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gcNguoiThanhToan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmThanhToan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh toán phí quản lý";
            this.Load += new System.EventHandler(this.frmThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datePhiQuanLy.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datePhiQuanLy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhiQuaLy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiThanhToan)).EndInit();
            this.gcNguoiThanhToan.ResumeLayout(false);
            this.gcNguoiThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatBang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiaChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckChuyenKhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDaTTDu.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.DateEdit datePhiQuanLy;
        private DevExpress.XtraEditors.GroupControl gcNguoiThanhToan;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.TextEdit txtMatBang;
        private DevExpress.XtraEditors.TextEdit txtDiaChi;
        private DevExpress.XtraEditors.TextEdit txtNguoiNop;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private DevExpress.XtraEditors.CheckEdit ckChuyenKhoan;
        private DevExpress.XtraEditors.SpinEdit txtPhiQuaLy;
        private DevExpress.XtraEditors.CheckEdit ckDaTTDu;
    }
}