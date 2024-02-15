namespace DichVu.ChoThue
{
    partial class frmThietLapTrangThaiMatBang
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
            this.lookTrangThai = new DevExpress.XtraEditors.LookUpEdit();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Trạng thái:";
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.Location = new System.Drawing.Point(71, 9);
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT", "Trạng thái mặt bằng")});
            this.lookTrangThai.Properties.DisplayMember = "TenTT";
            this.lookTrangThai.Properties.NullText = "";
            this.lookTrangThai.Properties.ValueMember = "MaTT";
            this.lookTrangThai.Size = new System.Drawing.Size(282, 20);
            this.lookTrangThai.TabIndex = 0;
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.Location = new System.Drawing.Point(232, 35);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(121, 25);
            this.btnChapNhan.TabIndex = 1;
            this.btnChapNhan.Text = "Chấp nhận";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // frmThietLapTrangThaiMatBang
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 69);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.lookTrangThai);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThietLapTrangThaiMatBang";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thiết lập trạng thái mặt bằng";
            this.Load += new System.EventHandler(this.frmThietLapTrangThaiMatBang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookTrangThai;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
    }
}