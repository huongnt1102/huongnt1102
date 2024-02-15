namespace DichVu.SendMail
{
    partial class frmSendMailNhacNo
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
            this.dateTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dateDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnChapNhan = new DevExpress.XtraEditors.SimpleButton();
            this.txtNoiDung = new MSDN.Html.Editor.HtmlEditorControl();
            this.txtTieuDe = new DevExpress.XtraEditors.TextEdit();
            this.lookMailFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.itemHuongDan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMailFrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.EditValue = null;
            this.dateTuNgay.Location = new System.Drawing.Point(69, 12);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTuNgay.Size = new System.Drawing.Size(250, 20);
            this.dateTuNgay.TabIndex = 0;
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.EditValue = null;
            this.dateDenNgay.Location = new System.Drawing.Point(440, 12);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateDenNgay.Size = new System.Drawing.Size(250, 20);
            this.dateDenNgay.TabIndex = 1;
            // 
            // btnChapNhan
            // 
            this.btnChapNhan.Location = new System.Drawing.Point(469, 370);
            this.btnChapNhan.Name = "btnChapNhan";
            this.btnChapNhan.Size = new System.Drawing.Size(107, 30);
            this.btnChapNhan.TabIndex = 5;
            this.btnChapNhan.Text = "Lưu và thoát";
            this.btnChapNhan.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.InnerText = null;
            this.txtNoiDung.Location = new System.Drawing.Point(12, 90);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(678, 274);
            this.txtNoiDung.TabIndex = 4;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(69, 38);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(621, 20);
            this.txtTieuDe.TabIndex = 2;
            // 
            // lookMailFrom
            // 
            this.lookMailFrom.Location = new System.Drawing.Point(69, 64);
            this.lookMailFrom.Name = "lookMailFrom";
            this.lookMailFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookMailFrom.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DiaChi", "Địa chỉ", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Server", "Server", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookMailFrom.Properties.DisplayMember = "DiaChi";
            this.lookMailFrom.Properties.NullText = "";
            this.lookMailFrom.Properties.ValueMember = "ID";
            this.lookMailFrom.Size = new System.Drawing.Size(621, 20);
            this.lookMailFrom.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Từ ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(383, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Đến ngày:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Tiêu đề:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(16, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Mail from:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(582, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // itemHuongDan
            // 
            this.itemHuongDan.Location = new System.Drawing.Point(12, 377);
            this.itemHuongDan.Name = "itemHuongDan";
            this.itemHuongDan.Size = new System.Drawing.Size(75, 23);
            this.itemHuongDan.TabIndex = 7;
            this.itemHuongDan.Text = "Hướng dẫn";
            // 
            // frmSendMailNhacNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 412);
            this.Controls.Add(this.itemHuongDan);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lookMailFrom);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChapNhan);
            this.Controls.Add(this.dateDenNgay);
            this.Controls.Add(this.dateTuNgay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSendMailNhacNo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mail nhắc nợ đến khách hàng";
            this.Load += new System.EventHandler(this.frmSendMailNhacNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTieuDe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookMailFrom.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateTuNgay;
        private DevExpress.XtraEditors.DateEdit dateDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnChapNhan;
        private MSDN.Html.Editor.HtmlEditorControl txtNoiDung;
        private DevExpress.XtraEditors.TextEdit txtTieuDe;
        private DevExpress.XtraEditors.LookUpEdit lookMailFrom;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton itemHuongDan;
    }
}