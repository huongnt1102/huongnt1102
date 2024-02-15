namespace LandSoftBuilding.Report
{
    partial class frmKyBaoCaoYeuCauKhieuNaiCuaKH
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lookUpToaNha = new DevExpress.XtraEditors.LookUpEdit();
            this.cbbKyBaoCao = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lkTrangThai = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lkDoUuTien = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBaoCao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTrangThai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkDoUuTien.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(330, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Hủy";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lookUpToaNha);
            this.panelControl1.Controls.Add(this.cbbKyBaoCao);
            this.panelControl1.Controls.Add(this.dateTuNgay);
            this.panelControl1.Controls.Add(this.dateDenNgay);
            this.panelControl1.Controls.Add(this.lkTrangThai);
            this.panelControl1.Controls.Add(this.lkDoUuTien);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(393, 121);
            this.panelControl1.TabIndex = 3;
            // 
            // lookUpToaNha
            // 
            this.lookUpToaNha.Location = new System.Drawing.Point(12, 38);
            this.lookUpToaNha.Name = "lookUpToaNha";
            this.lookUpToaNha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpToaNha.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTN", "Name1")});
            this.lookUpToaNha.Properties.DisplayMember = "TenTN";
            this.lookUpToaNha.Properties.NullText = "[Chọn tòa nhà]";
            this.lookUpToaNha.Properties.ShowHeader = false;
            this.lookUpToaNha.Properties.ValueMember = "MaTN";
            this.lookUpToaNha.Size = new System.Drawing.Size(368, 20);
            this.lookUpToaNha.TabIndex = 20;
            // 
            // cbbKyBaoCao
            // 
            this.cbbKyBaoCao.Location = new System.Drawing.Point(12, 12);
            this.cbbKyBaoCao.Name = "cbbKyBaoCao";
            this.cbbKyBaoCao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbKyBaoCao.Properties.NullText = "Kỳ báo cáo";
            this.cbbKyBaoCao.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbbKyBaoCao.Size = new System.Drawing.Size(150, 20);
            this.cbbKyBaoCao.TabIndex = 0;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(168, 12);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.NullText = "Từ ngày";
            this.dateTuNgay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.Size = new System.Drawing.Size(104, 20);
            this.dateTuNgay.TabIndex = 1;
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(278, 12);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.NullText = "Đến ngày";
            this.dateDenNgay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.Size = new System.Drawing.Size(102, 20);
            this.dateDenNgay.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(224, 139);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Tạo báo cáo";
            // 
            // lkTrangThai
            // 
            this.lkTrangThai.EditValue = "";
            this.lkTrangThai.Location = new System.Drawing.Point(12, 64);
            this.lkTrangThai.Name = "lkTrangThai";
            this.lkTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkTrangThai.Properties.DisplayMember = "TenTT";
            this.lkTrangThai.Properties.NullText = "[Chọn trạng thái]";
            this.lkTrangThai.Properties.NullValuePrompt = "\\";
            this.lkTrangThai.Properties.ValueMember = "MaTT";
            this.lkTrangThai.Size = new System.Drawing.Size(368, 20);
            this.lkTrangThai.TabIndex = 21;
            // 
            // lkDoUuTien
            // 
            this.lkDoUuTien.Location = new System.Drawing.Point(12, 90);
            this.lkDoUuTien.Name = "lkDoUuTien";
            this.lkDoUuTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkDoUuTien.Properties.DisplayMember = "TenDoUuTien";
            this.lkDoUuTien.Properties.NullText = "[Chọn độ ưu tiên]";
            this.lkDoUuTien.Properties.ValueMember = "MaDoUuTien";
            this.lkDoUuTien.Size = new System.Drawing.Size(368, 20);
            this.lkDoUuTien.TabIndex = 22;
            // 
            // frmKyBaoCaoYeuCauKhieuNaiCuaKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 171);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmKyBaoCaoYeuCauKhieuNaiCuaKH";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn kỳ báo cáo";
            this.Load += new System.EventHandler(this.frmKyBaoCaoToaNha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpToaNha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKyBaoCao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTrangThai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkDoUuTien.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpToaNha;
        private DevExpress.XtraEditors.ComboBoxEdit cbbKyBaoCao;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.CheckedComboBoxEdit lkTrangThai;
        private DevExpress.XtraEditors.CheckedComboBoxEdit lkDoUuTien;
    }
}