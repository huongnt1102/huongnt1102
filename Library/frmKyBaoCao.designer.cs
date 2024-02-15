namespace Library
{
    partial class frmKyBaoCao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKyBaoCao));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.cbbKBC = new DevExpress.XtraEditors.ComboBoxEdit();
            this.DateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.DateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.cbbKBC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.ImageOptions.ImageIndex = 0;
            this.btnOK.ImageOptions.ImageList = this.imageCollection1;
            this.btnOK.Location = new System.Drawing.Point(172, 43);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(103, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&Chấp nhận";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(281, 43);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbbKBC
            // 
            this.cbbKBC.Location = new System.Drawing.Point(12, 12);
            this.cbbKBC.Name = "cbbKBC";
            this.cbbKBC.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbKBC.Properties.NullText = "Chọn kỳ báo cáo";
            this.cbbKBC.Properties.PopupSizeable = true;
            this.cbbKBC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbbKBC.Size = new System.Drawing.Size(120, 20);
            this.cbbKBC.TabIndex = 0;
            this.cbbKBC.EditValueChanged += new System.EventHandler(this.cbbKBC_EditValueChanged);
            // 
            // DateTuNgay
            // 
            this.DateTuNgay.EditValue = null;
            this.DateTuNgay.Location = new System.Drawing.Point(138, 12);
            this.DateTuNgay.Name = "DateTuNgay";
            this.DateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateTuNgay.Properties.Mask.EditMask = "";
            this.DateTuNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.DateTuNgay.Properties.NullText = "Từ ngày";
            this.DateTuNgay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.DateTuNgay.Size = new System.Drawing.Size(120, 20);
            this.DateTuNgay.TabIndex = 0;
            this.DateTuNgay.EditValueChanged += new System.EventHandler(this.DateTuNgay_EditValueChanged);
            // 
            // DateDenNgay
            // 
            this.DateDenNgay.EditValue = null;
            this.DateDenNgay.Location = new System.Drawing.Point(264, 12);
            this.DateDenNgay.Name = "DateDenNgay";
            this.DateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateDenNgay.Properties.Mask.EditMask = "";
            this.DateDenNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.DateDenNgay.Properties.NullText = "Đến ngày";
            this.DateDenNgay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.DateDenNgay.Size = new System.Drawing.Size(120, 20);
            this.DateDenNgay.TabIndex = 0;
            this.DateDenNgay.EditValueChanged += new System.EventHandler(this.DateDenNgay_EditValueChanged);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");  
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");  
            // 
            // frmKyBaoCao
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(398, 80);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbbKBC);
            this.Controls.Add(this.DateTuNgay);
            this.Controls.Add(this.DateDenNgay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmKyBaoCao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kỳ báo cáo";
            this.Load += new System.EventHandler(this.frmKyBaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbbKBC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ComboBoxEdit cbbKBC;
        private DevExpress.XtraEditors.DateEdit DateTuNgay;
        private DevExpress.XtraEditors.DateEdit DateDenNgay;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}