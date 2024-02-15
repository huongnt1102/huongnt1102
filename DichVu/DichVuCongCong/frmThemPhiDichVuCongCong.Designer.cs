namespace DichVu.DichVuCongCong
{
    partial class frmThemPhiDichVuCongCong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemPhiDichVuCongCong));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookLoaiDichVu = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.spinPhiDV = new DevExpress.XtraEditors.SpinEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateThangThanhToan = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPhiDV.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThangThanhToan.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThangThanhToan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(36, 23);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Loại dịch vụ:";
            // 
            // lookLoaiDichVu
            // 
            this.lookLoaiDichVu.Location = new System.Drawing.Point(107, 19);
            this.lookLoaiDichVu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lookLoaiDichVu.Name = "lookLoaiDichVu";
            this.lookLoaiDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookLoaiDichVu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLoaiDV", "Loại dịch vụ", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DienGiai", "Diễn giải", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookLoaiDichVu.Properties.DisplayMember = "TenLoaiDV";
            this.lookLoaiDichVu.Properties.NullText = "";
            this.lookLoaiDichVu.Properties.ValueMember = "MaDV";
            this.lookLoaiDichVu.Size = new System.Drawing.Size(232, 20);
            this.lookLoaiDichVu.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 48);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Tổng phí dịch vụ:";
            // 
            // spinPhiDV
            // 
            this.spinPhiDV.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinPhiDV.Location = new System.Drawing.Point(107, 46);
            this.spinPhiDV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spinPhiDV.Name = "spinPhiDV";
            this.spinPhiDV.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinPhiDV.Properties.DisplayFormat.FormatString = "{0:#,0.##}";
            this.spinPhiDV.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPhiDV.Properties.EditFormat.FormatString = "{0:#,0.##}";
            this.spinPhiDV.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPhiDV.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinPhiDV.Properties.Mask.EditMask = "n0";
            this.spinPhiDV.Size = new System.Drawing.Size(232, 20);
            this.spinPhiDV.TabIndex = 1;
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.ImageOptions.ImageIndex = 1;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(258, 91);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(81, 27);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "icons8_save.png");  
            this.imageCollection1.Images.SetKeyName(1, "icons8_delete1.png");  
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.ImageOptions.ImageIndex = 0;
            this.btnChapNhan.ImageOptions.ImageList = this.imageCollection1;
            this.btnChapNhan.Location = new System.Drawing.Point(119, 91);
            this.btnChapNhan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(134, 27);
            this.btnChapNhan.TabIndex = 2;
            this.btnChapNhan.Text = "Hình thành công nợ";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(67, 71);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(34, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Tháng:";
            // 
            // dateThangThanhToan
            // 
            this.dateThangThanhToan.EditValue = null;
            this.dateThangThanhToan.Location = new System.Drawing.Point(107, 68);
            this.dateThangThanhToan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateThangThanhToan.Name = "dateThangThanhToan";
            this.dateThangThanhToan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateThangThanhToan.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateThangThanhToan.Properties.DisplayFormat.FormatString = "y";
            this.dateThangThanhToan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateThangThanhToan.Properties.EditFormat.FormatString = "y";
            this.dateThangThanhToan.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateThangThanhToan.Properties.Mask.EditMask = "y";
            this.dateThangThanhToan.Size = new System.Drawing.Size(232, 20);
            this.dateThangThanhToan.TabIndex = 1;
            // 
            // frmThemPhiDichVuCongCong
            // 
            this.AcceptButton = this.btnChapNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(350, 128);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.lookLoaiDichVu);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.spinPhiDV);
            this.Controls.Add(this.dateThangThanhToan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemPhiDichVuCongCong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm phí dịch vụ công cộng";
            this.Load += new System.EventHandler(this.frmThemPhiDichVuCongCong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookLoaiDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPhiDV.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThangThanhToan.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateThangThanhToan.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookLoaiDichVu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit spinPhiDV;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateThangThanhToan;
    }
}