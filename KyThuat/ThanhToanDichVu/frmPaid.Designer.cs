namespace KyThuat.ThanhToanDichVu
{
    partial class frmPaid
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
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.spinThucChi = new DevExpress.XtraEditors.SpinEdit();
            this.gcNguoiThanhToan = new DevExpress.XtraEditors.GroupControl();
            this.dateNgayChi = new DevExpress.XtraEditors.DateEdit();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.txtDiaChi = new DevExpress.XtraEditors.TextEdit();
            this.txtSoPhieu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtNguoiNop = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinThucChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiThanhToan)).BeginInit();
            this.gcNguoiThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChi.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiaChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPhieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNop.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageIndex = 1;
            this.btnHuy.Location = new System.Drawing.Point(351, 273);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(88, 26);
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "&Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.ImageIndex = 0;
            this.btnChapNhan.Location = new System.Drawing.Point(249, 273);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(96, 26);
            this.btnChapNhan.TabIndex = 6;
            this.btnChapNhan.Text = "&Chấp nhận";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.spinThucChi);
            this.groupControl1.Location = new System.Drawing.Point(12, 205);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(427, 62);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Khoản thanh toán";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(10, 34);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(37, 13);
            this.labelControl11.TabIndex = 0;
            this.labelControl11.Text = "Số tiền:";
            // 
            // spinThucChi
            // 
            this.spinThucChi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinThucChi.Location = new System.Drawing.Point(83, 31);
            this.spinThucChi.Name = "spinThucChi";
            this.spinThucChi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinThucChi.Properties.DisplayFormat.FormatString = "{0:#,0.#}";
            this.spinThucChi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThucChi.Properties.EditFormat.FormatString = "{0:#,0.#}";
            this.spinThucChi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThucChi.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinThucChi.Properties.Mask.EditMask = "n0";
            this.spinThucChi.Properties.MaxValue = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.spinThucChi.Size = new System.Drawing.Size(120, 20);
            this.spinThucChi.TabIndex = 6;
            // 
            // gcNguoiThanhToan
            // 
            this.gcNguoiThanhToan.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcNguoiThanhToan.AppearanceCaption.Options.UseFont = true;
            this.gcNguoiThanhToan.Controls.Add(this.dateNgayChi);
            this.gcNguoiThanhToan.Controls.Add(this.txtDienGiai);
            this.gcNguoiThanhToan.Controls.Add(this.txtDiaChi);
            this.gcNguoiThanhToan.Controls.Add(this.txtSoPhieu);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl5);
            this.gcNguoiThanhToan.Controls.Add(this.txtNguoiNop);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl18);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl4);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl2);
            this.gcNguoiThanhToan.Controls.Add(this.labelControl1);
            this.gcNguoiThanhToan.Location = new System.Drawing.Point(12, 12);
            this.gcNguoiThanhToan.Name = "gcNguoiThanhToan";
            this.gcNguoiThanhToan.Size = new System.Drawing.Size(427, 187);
            this.gcNguoiThanhToan.TabIndex = 4;
            this.gcNguoiThanhToan.Text = "Thông tin chung";
            // 
            // dateNgayChi
            // 
            this.dateNgayChi.EditValue = null;
            this.dateNgayChi.Location = new System.Drawing.Point(307, 27);
            this.dateNgayChi.Name = "dateNgayChi";
            this.dateNgayChi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayChi.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayChi.Size = new System.Drawing.Size(110, 20);
            this.dateNgayChi.TabIndex = 1;
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDienGiai.Location = new System.Drawing.Point(83, 104);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(334, 72);
            this.txtDienGiai.TabIndex = 5;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChi.Location = new System.Drawing.Point(83, 78);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(334, 20);
            this.txtDiaChi.TabIndex = 3;
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoPhieu.Location = new System.Drawing.Point(83, 27);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Size = new System.Drawing.Size(120, 20);
            this.txtSoPhieu.TabIndex = 0;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(235, 30);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(45, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Ngày chi:";
            // 
            // txtNguoiNop
            // 
            this.txtNguoiNop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNguoiNop.Location = new System.Drawing.Point(83, 52);
            this.txtNguoiNop.Name = "txtNguoiNop";
            this.txtNguoiNop.Size = new System.Drawing.Size(334, 20);
            this.txtNguoiNop.TabIndex = 2;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(10, 107);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(44, 13);
            this.labelControl18.TabIndex = 0;
            this.labelControl18.Text = "Diễn giải:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(61, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Số phiếu chi:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 81);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Địa chỉ:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Người nhận:";
            // 
            // frmPaid
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(452, 311);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gcNguoiThanhToan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaid";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu chi";
            this.Load += new System.EventHandler(this.frmPaid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinThucChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiThanhToan)).EndInit();
            this.gcNguoiThanhToan.ResumeLayout(false);
            this.gcNguoiThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChi.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiaChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoPhieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNguoiNop.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SpinEdit spinThucChi;
        private DevExpress.XtraEditors.GroupControl gcNguoiThanhToan;
        private DevExpress.XtraEditors.DateEdit dateNgayChi;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.TextEdit txtDiaChi;
        private DevExpress.XtraEditors.TextEdit txtSoPhieu;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtNguoiNop;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}