namespace DichVu.Quy
{
    partial class frmPhieuThuHuy
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
            this.lbLyDo = new DevExpress.XtraEditors.LabelControl();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.txtLyDoHuy = new DevExpress.XtraEditors.MemoEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.cmbTrangThai = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDoHuy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrangThai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLyDo
            // 
            this.lbLyDo.Location = new System.Drawing.Point(119, 41);
            this.lbLyDo.Name = "lbLyDo";
            this.lbLyDo.Size = new System.Drawing.Size(77, 13);
            this.lbLyDo.TabIndex = 1;
            this.lbLyDo.Text = "Lý do xác nhận:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(67, 171);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Xác nhận";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtLyDoHuy
            // 
            this.txtLyDoHuy.Location = new System.Drawing.Point(12, 60);
            this.txtLyDoHuy.Name = "txtLyDoHuy";
            this.txtLyDoHuy.Size = new System.Drawing.Size(291, 103);
            this.txtLyDoHuy.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbTrangThai
            // 
            this.cmbTrangThai.Location = new System.Drawing.Point(86, 6);
            this.cmbTrangThai.Name = "cmbTrangThai";
            this.cmbTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTrangThai.Properties.Items.AddRange(new object[] {
            "Tồn tại",
            "Hủy bỏ"});
            this.cmbTrangThai.Size = new System.Drawing.Size(181, 20);
            this.cmbTrangThai.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 10);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Trạng thái:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.cmbTrangThai);
            this.panelControl1.Location = new System.Drawing.Point(12, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(291, 31);
            this.panelControl1.TabIndex = 6;
            // 
            // frmPhieuThuHuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 214);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lbLyDo);
            this.Controls.Add(this.txtLyDoHuy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPhieuThuHuy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hủy phiếu thu";
            this.Load += new System.EventHandler(this.frmPhieuThuHuy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDoHuy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrangThai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbLyDo;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.MemoEdit txtLyDoHuy;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTrangThai;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;

    }
}