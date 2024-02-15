namespace DIP.SoftPhoneAPI.Reports
{
    partial class frmHistory
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTrunk = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cmbStaff = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cmbType = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatus = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.cmbKyBaoCao = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrunk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTrunk);
            this.groupBox1.Controls.Add(this.cmbStaff);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.dateDenNgay);
            this.groupBox1.Controls.Add(this.dateTuNgay);
            this.groupBox1.Controls.Add(this.cmbKyBaoCao);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 193);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kỳ báo cáo";
            // 
            // cmbTrunk
            // 
            this.cmbTrunk.Location = new System.Drawing.Point(140, 161);
            this.cmbTrunk.Name = "cmbTrunk";
            this.cmbTrunk.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTrunk.Size = new System.Drawing.Size(117, 20);
            this.cmbTrunk.TabIndex = 13;
            // 
            // cmbStaff
            // 
            this.cmbStaff.Location = new System.Drawing.Point(17, 161);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStaff.Properties.DisplayMember = "Name";
            this.cmbStaff.Properties.ValueMember = "ID";
            this.cmbStaff.Size = new System.Drawing.Size(117, 20);
            this.cmbStaff.TabIndex = 13;
            // 
            // cmbType
            // 
            this.cmbType.EditValue = "";
            this.cmbType.Location = new System.Drawing.Point(17, 115);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Cuộc gọi đến", "Cuộc gọi đến"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Cuộc gọi đi", "Cuộc gọi đi")});
            this.cmbType.Size = new System.Drawing.Size(117, 20);
            this.cmbType.TabIndex = 13;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(141, 142);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(34, 13);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Đầu số";
            // 
            // cmbStatus
            // 
            this.cmbStatus.EditValue = "";
            this.cmbStatus.Location = new System.Drawing.Point(140, 115);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Thành công", "Thành công"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Cuộc gọi nhỡ", "Cuộc gọi nhỡ"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Không trả lời", "Không trả lời"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Máy bận", "Máy bận"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Không liên lạc được", "Không liên lạc được"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("Thất bại", "Thất bại")});
            this.cmbStatus.Size = new System.Drawing.Size(115, 20);
            this.cmbStatus.TabIndex = 12;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 142);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 13);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Nhân viên";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(140, 96);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(49, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Trạng thái";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 96);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Phân loại";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Từ ngày";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(140, 46);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Đến ngày";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(140, 65);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.Size = new System.Drawing.Size(116, 20);
            this.dateDenNgay.TabIndex = 10;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(18, 65);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.Size = new System.Drawing.Size(116, 20);
            this.dateTuNgay.TabIndex = 9;
            // 
            // cmbKyBaoCao
            // 
            this.cmbKyBaoCao.Location = new System.Drawing.Point(17, 20);
            this.cmbKyBaoCao.Name = "cmbKyBaoCao";
            this.cmbKyBaoCao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKyBaoCao.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbKyBaoCao.Size = new System.Drawing.Size(238, 20);
            this.cmbKyBaoCao.TabIndex = 0;
            this.cmbKyBaoCao.SelectedIndexChanged += new System.EventHandler(this.cmbKyBaoCao_SelectedIndexChanged);
            // 
            // btnAccept
            // 
            this.btnAccept.ImageIndex = 1;
            this.btnAccept.Location = new System.Drawing.Point(127, 211);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "Xem";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageIndex = 0;
            this.btnCancel.Location = new System.Drawing.Point(208, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 1;
            this.btnPrint.Location = new System.Drawing.Point(46, 211);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "In";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 242);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHistory";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tùy chọn";
            this.Load += new System.EventHandler(this.frmHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTrunk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKyBaoCao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.ComboBoxEdit cmbKyBaoCao;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbStatus;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbTrunk;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbStaff;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
    }
}