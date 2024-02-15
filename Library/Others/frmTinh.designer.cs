namespace Library.Other
{
    partial class frmTinh
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
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.txtTenTinh = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenTinh.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //this.btnHuy.Image = global::Library.Properties.Resources.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(190, 87);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            //this.btnDongY.Image = global::Library.Properties.Resources.OK;
            this.btnDongY.Location = new System.Drawing.Point(89, 87);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(89, 23);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // txtTenTinh
            // 
            this.txtTenTinh.Location = new System.Drawing.Point(97, 27);
            this.txtTenTinh.Name = "txtTenTinh";
            this.txtTenTinh.Size = new System.Drawing.Size(201, 20);
            this.txtTenTinh.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(35, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Tên tỉnh (*):";
            // 
            // Tinh_frm
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(359, 132);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtTenTinh);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tinh_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tỉnh (TP)";
            this.Load += new System.EventHandler(this.Huong_frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTenTinh.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.TextEdit txtTenTinh;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}