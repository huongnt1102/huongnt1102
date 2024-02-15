namespace DichVu.HoBoi.DinhMuc
{
    partial class frmEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEdit));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lookLoaiThe = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTenDM = new DevExpress.XtraEditors.TextEdit();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.spinMucPhi = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiThe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMucPhi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(346, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageIndex = 0;
            this.btnSave.ImageOptions.ImageList = this.imageCollection1;
            this.btnSave.Location = new System.Drawing.Point(235, 143);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lookLoaiThe);
            this.panelControl1.Controls.Add(this.txtTenDM);
            this.panelControl1.Controls.Add(this.dateDenNgay);
            this.panelControl1.Controls.Add(this.dateTuNgay);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.spinMucPhi);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(429, 125);
            this.panelControl1.TabIndex = 0;
            // 
            // lookLoaiThe
            // 
            this.lookLoaiThe.Location = new System.Drawing.Point(107, 64);
            this.lookLoaiThe.Name = "lookLoaiThe";
            this.lookLoaiThe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoaiThe.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLT", "Tên loại thẻ")});
            this.lookLoaiThe.Properties.DisplayMember = "TenLT";
            this.lookLoaiThe.Properties.NullText = "[Loại thẻ]";
            this.lookLoaiThe.Properties.ValueMember = "ID";
            this.lookLoaiThe.Size = new System.Drawing.Size(311, 20);
            this.lookLoaiThe.TabIndex = 3;
            // 
            // txtTenDM
            // 
            this.txtTenDM.Location = new System.Drawing.Point(107, 12);
            this.txtTenDM.Name = "txtTenDM";
            this.txtTenDM.Properties.MaxLength = 50;
            this.txtTenDM.Size = new System.Drawing.Size(311, 20);
            this.txtTenDM.TabIndex = 0;
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(318, 41);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNgay.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateDenNgay.Size = new System.Drawing.Size(100, 20);
            this.dateDenNgay.TabIndex = 2;
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(107, 38);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.Properties.DisplayFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.Properties.EditFormat.FormatString = "{0:dd/MM/yyyy}";
            this.dateTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dateTuNgay.Size = new System.Drawing.Size(100, 20);
            this.dateTuNgay.TabIndex = 1;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(235, 41);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(51, 13);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "Đến ngày:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(18, 44);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(44, 13);
            this.labelControl8.TabIndex = 9;
            this.labelControl8.Text = "Từ ngày:";
            // 
            // spinMucPhi
            // 
            this.spinMucPhi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinMucPhi.Location = new System.Drawing.Point(107, 90);
            this.spinMucPhi.Name = "spinMucPhi";
            this.spinMucPhi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinMucPhi.Properties.DisplayFormat.FormatString = "{0:n0}";
            this.spinMucPhi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMucPhi.Properties.EditFormat.FormatString = "{0:n0}";
            this.spinMucPhi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinMucPhi.Properties.MaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.spinMucPhi.Size = new System.Drawing.Size(311, 20);
            this.spinMucPhi.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên định mức";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(38, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Loại thẻ";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(41, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Mức phí:";
            // 
            // frmEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(455, 183);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Định mức";
            this.Load += new System.EventHandler(this.frmDinhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiThe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenDM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMucPhi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.SpinEdit spinMucPhi;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookLoaiThe;
        private DevExpress.XtraEditors.TextEdit txtTenDM;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}