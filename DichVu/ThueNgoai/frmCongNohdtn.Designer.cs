namespace DichVu.ThueNgoai
{
    partial class frmCongNohdtn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCongNohdtn));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSoHoaDon = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.spinSoTien = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtDienGiai = new DevExpress.XtraEditors.MemoEdit();
            this.btnok = new DevExpress.XtraEditors.SimpleButton();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaHopDong = new DevExpress.XtraEditors.TextEdit();
            this.spinDaTT = new DevExpress.XtraEditors.SpinEdit();
            this.spinConLai = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtTenHopDong = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.dateNgayThanhToan = new DevExpress.XtraEditors.DateEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.itemHuongDan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHoaDon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaHopDong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDaTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinConLai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHopDong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayThanhToan.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayThanhToan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_save.png");
            this.imageCollection1.Images.SetKeyName(1, "icons8_cancel1.png");
            this.imageCollection1.Images.SetKeyName(2, "Open2.png");
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 120);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Số hóa đơn:";
            // 
            // txtSoHoaDon
            // 
            this.txtSoHoaDon.Location = new System.Drawing.Point(106, 117);
            this.txtSoHoaDon.Name = "txtSoHoaDon";
            this.txtSoHoaDon.Size = new System.Drawing.Size(307, 20);
            this.txtSoHoaDon.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(15, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Mã hợp đồng:";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Location = new System.Drawing.Point(15, 68);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(74, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Đã thanh toán:";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(15, 94);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Còn lại:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 146);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(37, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Số tiền:";
            // 
            // spinSoTien
            // 
            this.spinSoTien.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoTien.Location = new System.Drawing.Point(106, 143);
            this.spinSoTien.Name = "spinSoTien";
            this.spinSoTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSoTien.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinSoTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoTien.Properties.EditFormat.FormatString = "{0:#,0.##}";
            this.spinSoTien.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoTien.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinSoTien.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinSoTien.Size = new System.Drawing.Size(307, 20);
            this.spinSoTien.TabIndex = 5;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 198);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Diễn giải:";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(106, 195);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(307, 56);
            this.txtDienGiai.TabIndex = 7;
            // 
            // btnok
            // 
            this.btnok.ImageOptions.ImageIndex = 0;
            this.btnok.ImageOptions.ImageList = this.imageCollection1;
            this.btnok.Location = new System.Drawing.Point(237, 283);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(97, 30);
            this.btnok.TabIndex = 2;
            this.btnok.Text = "Lưu và &đóng";
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancel
            // 
            this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancel.ImageOptions.ImageIndex = 1;
            this.btncancel.ImageOptions.ImageList = this.imageCollection1;
            this.btncancel.Location = new System.Drawing.Point(340, 283);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(100, 30);
            this.btncancel.TabIndex = 2;
            this.btncancel.Text = "&Hủy";
            // 
            // txtMaHopDong
            // 
            this.txtMaHopDong.Enabled = false;
            this.txtMaHopDong.Location = new System.Drawing.Point(106, 13);
            this.txtMaHopDong.Name = "txtMaHopDong";
            this.txtMaHopDong.Size = new System.Drawing.Size(307, 20);
            this.txtMaHopDong.TabIndex = 0;
            // 
            // spinDaTT
            // 
            this.spinDaTT.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinDaTT.Enabled = false;
            this.spinDaTT.Location = new System.Drawing.Point(106, 65);
            this.spinDaTT.Name = "spinDaTT";
            this.spinDaTT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDaTT.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinDaTT.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDaTT.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinDaTT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinDaTT.Size = new System.Drawing.Size(307, 20);
            this.spinDaTT.TabIndex = 2;
            // 
            // spinConLai
            // 
            this.spinConLai.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinConLai.Enabled = false;
            this.spinConLai.Location = new System.Drawing.Point(106, 91);
            this.spinConLai.Name = "spinConLai";
            this.spinConLai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinConLai.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinConLai.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinConLai.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinConLai.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spinConLai.Size = new System.Drawing.Size(307, 20);
            this.spinConLai.TabIndex = 3;
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl7.Location = new System.Drawing.Point(15, 42);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(70, 13);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Tên hợp đồng:";
            // 
            // txtTenHopDong
            // 
            this.txtTenHopDong.Enabled = false;
            this.txtTenHopDong.Location = new System.Drawing.Point(106, 39);
            this.txtTenHopDong.Name = "txtTenHopDong";
            this.txtTenHopDong.Size = new System.Drawing.Size(307, 20);
            this.txtTenHopDong.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(15, 172);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(85, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Ngày thanh toán:";
            // 
            // dateNgayThanhToan
            // 
            this.dateNgayThanhToan.EditValue = null;
            this.dateNgayThanhToan.Location = new System.Drawing.Point(106, 169);
            this.dateNgayThanhToan.Name = "dateNgayThanhToan";
            this.dateNgayThanhToan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayThanhToan.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayThanhToan.Properties.Mask.EditMask = "";
            this.dateNgayThanhToan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateNgayThanhToan.Size = new System.Drawing.Size(307, 20);
            this.dateNgayThanhToan.TabIndex = 6;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtMaHopDong);
            this.panelControl1.Controls.Add(this.dateNgayThanhToan);
            this.panelControl1.Controls.Add(this.spinConLai);
            this.panelControl1.Controls.Add(this.txtTenHopDong);
            this.panelControl1.Controls.Add(this.spinDaTT);
            this.panelControl1.Controls.Add(this.txtDienGiai);
            this.panelControl1.Controls.Add(this.txtSoHoaDon);
            this.panelControl1.Controls.Add(this.spinSoTien);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(428, 265);
            this.panelControl1.TabIndex = 3;
            // 
            // itemHuongDan
            // 
            this.itemHuongDan.ImageOptions.ImageIndex = 2;
            this.itemHuongDan.ImageOptions.ImageList = this.imageCollection1;
            this.itemHuongDan.Location = new System.Drawing.Point(12, 289);
            this.itemHuongDan.Name = "itemHuongDan";
            this.itemHuongDan.Size = new System.Drawing.Size(89, 23);
            this.itemHuongDan.TabIndex = 4;
            this.itemHuongDan.Text = "Hướng dẫn";
            // 
            // frmCongNohdtn
            // 
            this.AcceptButton = this.btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btncancel;
            this.ClientSize = new System.Drawing.Size(454, 326);
            this.Controls.Add(this.itemHuongDan);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCongNohdtn";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh toán hợp đồng thuê ngoài";
            this.Load += new System.EventHandler(this.frmCongNohdtn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoHoaDon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaHopDong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDaTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinConLai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenHopDong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayThanhToan.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayThanhToan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSoHoaDon;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SpinEdit spinSoTien;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.MemoEdit txtDienGiai;
        private DevExpress.XtraEditors.SimpleButton btnok;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.TextEdit txtMaHopDong;
        private DevExpress.XtraEditors.SpinEdit spinDaTT;
        private DevExpress.XtraEditors.SpinEdit spinConLai;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtTenHopDong;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.DateEdit dateNgayThanhToan;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton itemHuongDan;
    }
}