namespace DichVu.DichVuCongCong
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
            this.txtMatBang = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtKhachHang = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtPhiDichVu = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtThangThanhToan = new DevExpress.XtraEditors.TextEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtMatBang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhachHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhiDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThangThanhToan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mặt bằng:";
            // 
            // txtMatBang
            // 
            this.txtMatBang.Location = new System.Drawing.Point(95, 12);
            this.txtMatBang.Name = "txtMatBang";
            this.txtMatBang.Properties.ReadOnly = true;
            this.txtMatBang.Size = new System.Drawing.Size(312, 22);
            this.txtMatBang.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(39, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 17);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Chủ hộ:";
            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Location = new System.Drawing.Point(95, 40);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Properties.ReadOnly = true;
            this.txtKhachHang.Size = new System.Drawing.Size(312, 22);
            this.txtKhachHang.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 73);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Phí dịch vụ:";
            // 
            // txtPhiDichVu
            // 
            this.txtPhiDichVu.Location = new System.Drawing.Point(95, 68);
            this.txtPhiDichVu.Name = "txtPhiDichVu";
            this.txtPhiDichVu.Properties.ReadOnly = true;
            this.txtPhiDichVu.Size = new System.Drawing.Size(312, 22);
            this.txtPhiDichVu.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(48, 99);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 16);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Tháng:";
            // 
            // txtThangThanhToan
            // 
            this.txtThangThanhToan.Location = new System.Drawing.Point(95, 96);
            this.txtThangThanhToan.Name = "txtThangThanhToan";
            this.txtThangThanhToan.Properties.ReadOnly = true;
            this.txtThangThanhToan.Size = new System.Drawing.Size(312, 22);
            this.txtThangThanhToan.TabIndex = 1;
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(307, 124);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 32);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.Location = new System.Drawing.Point(194, 124);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(107, 32);
            this.btnChapNhan.TabIndex = 2;
            this.btnChapNhan.Text = "Chấp nhận";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // frmThanhToan
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(423, 170);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.txtThangThanhToan);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtPhiDichVu);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtKhachHang);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtMatBang);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThanhToan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh toán dịch vụ công cộng";
            this.Load += new System.EventHandler(this.frmThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMatBang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhachHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhiDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThangThanhToan.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMatBang;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtKhachHang;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPhiDichVu;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtThangThanhToan;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
    }
}