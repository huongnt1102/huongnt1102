﻿namespace DichVu.TheTichHop
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookNhanVien = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtSoThe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookMatBang = new DevExpress.XtraEditors.LookUpEdit();
            this.dateNgayDK = new DevExpress.XtraEditors.DateEdit();
            this.txtChuThe = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDienGiai = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.ckbDaTT = new DevExpress.XtraEditors.CheckEdit();
            this.spinPhiLamThe = new DevExpress.XtraEditors.SpinEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lookTrangThai = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoThe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDK.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChuThe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbDaTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPhiLamThe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 121);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mặt bằng:";
            // 
            // lookNhanVien
            // 
            this.lookNhanVien.Location = new System.Drawing.Point(297, 66);
            this.lookNhanVien.Name = "lookNhanVien";
            this.lookNhanVien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNhanVien.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoNV", "Name7", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNV", "Name8", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookNhanVien.Properties.DisplayMember = "HoTenNV";
            this.lookNhanVien.Properties.NullText = "";
            this.lookNhanVien.Properties.ShowHeader = false;
            this.lookNhanVien.Properties.ShowLines = false;
            this.lookNhanVien.Properties.ValueMember = "MaNV";
            this.lookNhanVien.Size = new System.Drawing.Size(132, 20);
            this.lookNhanVien.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(233, 69);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Nhân viên:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 95);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Diễn giải:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(364, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnSave.Location = new System.Drawing.Point(266, 195);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(233, 17);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(46, 13);
            this.labelControl15.TabIndex = 0;
            this.labelControl15.Text = "Ngày ĐK:";
            // 
            // txtSoThe
            // 
            this.txtSoThe.Location = new System.Drawing.Point(82, 14);
            this.txtSoThe.Name = "txtSoThe";
            this.txtSoThe.Size = new System.Drawing.Size(126, 20);
            this.txtSoThe.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 17);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Số thẻ:";
            // 
            // lookMatBang
            // 
            this.lookMatBang.Location = new System.Drawing.Point(82, 118);
            this.lookMatBang.Name = "lookMatBang";
            this.lookMatBang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookMatBang.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoMB", "Name20", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKH", "Name6", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookMatBang.Properties.DisplayMember = "MaSoMB";
            this.lookMatBang.Properties.NullText = "";
            this.lookMatBang.Properties.ShowHeader = false;
            this.lookMatBang.Properties.ShowLines = false;
            this.lookMatBang.Properties.ValueMember = "MaMB";
            this.lookMatBang.Size = new System.Drawing.Size(347, 20);
            this.lookMatBang.TabIndex = 3;
            // 
            // dateNgayDK
            // 
            this.dateNgayDK.EditValue = null;
            this.dateNgayDK.Location = new System.Drawing.Point(297, 14);
            this.dateNgayDK.Name = "dateNgayDK";
            this.dateNgayDK.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayDK.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayDK.Size = new System.Drawing.Size(132, 20);
            this.dateNgayDK.TabIndex = 8;
            // 
            // txtChuThe
            // 
            this.txtChuThe.Location = new System.Drawing.Point(82, 40);
            this.txtChuThe.Name = "txtChuThe";
            this.txtChuThe.Size = new System.Drawing.Size(347, 20);
            this.txtChuThe.TabIndex = 1;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(10, 43);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(42, 13);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Chủ thẻ:";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(82, 92);
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(347, 20);
            this.txtDienGiai.TabIndex = 1;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(10, 69);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(56, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Phí làm thẻ:";
            // 
            // ckbDaTT
            // 
            this.ckbDaTT.Location = new System.Drawing.Point(11, 195);
            this.ckbDaTT.Name = "ckbDaTT";
            this.ckbDaTT.Properties.Caption = "Đã thanh toán";
            this.ckbDaTT.Size = new System.Drawing.Size(110, 19);
            this.ckbDaTT.TabIndex = 9;
            // 
            // spinPhiLamThe
            // 
            this.spinPhiLamThe.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinPhiLamThe.Location = new System.Drawing.Point(82, 66);
            this.spinPhiLamThe.Name = "spinPhiLamThe";
            this.spinPhiLamThe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinPhiLamThe.Properties.DisplayFormat.FormatString = "c0";
            this.spinPhiLamThe.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinPhiLamThe.Properties.Mask.EditMask = "c0";
            this.spinPhiLamThe.Size = new System.Drawing.Size(126, 20);
            this.spinPhiLamThe.TabIndex = 10;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtSoThe);
            this.panelControl1.Controls.Add(this.spinPhiLamThe);
            this.panelControl1.Controls.Add(this.labelControl15);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lookNhanVien);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.txtDienGiai);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.dateNgayDK);
            this.panelControl1.Controls.Add(this.lookTrangThai);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.lookMatBang);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.txtChuThe);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(448, 177);
            this.panelControl1.TabIndex = 11;
            // 
            // lookTrangThai
            // 
            this.lookTrangThai.Location = new System.Drawing.Point(82, 144);
            this.lookTrangThai.Name = "lookTrangThai";
            this.lookTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookTrangThai.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTrangThai", "Trạng Thái", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookTrangThai.Properties.DisplayMember = "TenTrangThai";
            this.lookTrangThai.Properties.NullText = "";
            this.lookTrangThai.Properties.ShowHeader = false;
            this.lookTrangThai.Properties.ShowLines = false;
            this.lookTrangThai.Properties.ValueMember = "MaTrangThai";
            this.lookTrangThai.Size = new System.Drawing.Size(347, 20);
            this.lookTrangThai.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(10, 147);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(53, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Trạng thái:";
            // 
            // frmEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(471, 234);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ckbDaTT);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thẻ tích hợp";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoThe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMatBang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDK.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayDK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChuThe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienGiai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbDaTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPhiLamThe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookTrangThai.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookNhanVien;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.TextEdit txtSoThe;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookMatBang;
        private DevExpress.XtraEditors.DateEdit dateNgayDK;
        private DevExpress.XtraEditors.TextEdit txtChuThe;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtDienGiai;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.CheckEdit ckbDaTT;
        private DevExpress.XtraEditors.SpinEdit spinPhiLamThe;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookTrangThai;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}