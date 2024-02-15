namespace Library.Controls.NhuCau
{
    partial class frmChuyenNhanVien
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl46 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnThucHien = new DevExpress.XtraEditors.SimpleButton();
            this.ctlStaffCheckListEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlStaffCheckListEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ctlStaffCheckListEdit1);
            this.panelControl1.Controls.Add(this.labelControl46);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(335, 60);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl46
            // 
            this.labelControl46.Location = new System.Drawing.Point(19, 24);
            this.labelControl46.Name = "labelControl46";
            this.labelControl46.Size = new System.Drawing.Size(52, 13);
            this.labelControl46.TabIndex = 90;
            this.labelControl46.Text = "Nhân viên:";
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(272, 78);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnThucHien
            // 
            this.btnThucHien.Location = new System.Drawing.Point(163, 78);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(103, 23);
            this.btnThucHien.TabIndex = 1;
            this.btnThucHien.Text = "Thực hiện";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // ctlStaffCheckListEdit1
            // 
            this.ctlStaffCheckListEdit1.EditValue = "";
            this.ctlStaffCheckListEdit1.Location = new System.Drawing.Point(77, 21);
            this.ctlStaffCheckListEdit1.Name = "ctlStaffCheckListEdit1";
            this.ctlStaffCheckListEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctlStaffCheckListEdit1.Properties.DisplayMember = "HoTenNV";
            this.ctlStaffCheckListEdit1.Properties.ValueMember = "MaNV";
            this.ctlStaffCheckListEdit1.Size = new System.Drawing.Size(253, 20);
            this.ctlStaffCheckListEdit1.TabIndex = 106;
            // 
            // frmChuyenNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 114);
            this.Controls.Add(this.btnThucHien);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChuyenNhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cập nhật nhân viên hổ trợ";
            this.Load += new System.EventHandler(this.frmChuyenNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlStaffCheckListEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnThucHien;
        private DevExpress.XtraEditors.LabelControl labelControl46;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ctlStaffCheckListEdit1;
        //private Controls.ctlStaffCheckListEdit ctlStaffCheckListEdit1;
    }
}