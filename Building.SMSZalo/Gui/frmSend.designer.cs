namespace Building.SMSZalo.Gui
{
    partial class frmSend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSend));
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblTongSo = new DevExpress.XtraEditors.LabelControl();
            this.lblThanhCong = new DevExpress.XtraEditors.LabelControl();
            this.lblThatBai = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.grlMauGuiSms = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lkMauIn = new DevExpress.XtraEditors.LookUpEdit();
            this.lkTaiKhoan = new DevExpress.XtraEditors.LookUpEdit();
            this.ckbLoaiDichVu = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grlMauGuiSms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkMauIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTaiKhoan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbLoaiDichVu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(19, 279);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(721, 18);
            this.progressBarControl1.TabIndex = 1;
            // 
            // lblTongSo
            // 
            this.lblTongSo.Location = new System.Drawing.Point(19, 303);
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(38, 13);
            this.lblTongSo.TabIndex = 2;
            this.lblTongSo.Text = "Tổng số";
            // 
            // lblThanhCong
            // 
            this.lblThanhCong.Location = new System.Drawing.Point(19, 322);
            this.lblThanhCong.Name = "lblThanhCong";
            this.lblThanhCong.Size = new System.Drawing.Size(56, 13);
            this.lblThanhCong.TabIndex = 2;
            this.lblThanhCong.Text = "Thành công";
            // 
            // lblThatBai
            // 
            this.lblThatBai.Location = new System.Drawing.Point(236, 322);
            this.lblThatBai.Name = "lblThatBai";
            this.lblThatBai.Size = new System.Drawing.Size(39, 13);
            this.lblThatBai.TabIndex = 2;
            this.lblThatBai.Text = "Thất bại";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lkMauIn);
            this.groupBox1.Controls.Add(this.lkTaiKhoan);
            this.groupBox1.Controls.Add(this.ckbLoaiDichVu);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtNoiDung);
            this.groupBox1.Controls.Add(this.grlMauGuiSms);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.progressBarControl1);
            this.groupBox1.Controls.Add(this.lblThatBai);
            this.groupBox1.Controls.Add(this.lblTongSo);
            this.groupBox1.Controls.Add(this.lblThanhCong);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(752, 358);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(88, 70);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(651, 203);
            this.txtNoiDung.TabIndex = 19;
            // 
            // grlMauGuiSms
            // 
            this.grlMauGuiSms.Location = new System.Drawing.Point(88, 11);
            this.grlMauGuiSms.Name = "grlMauGuiSms";
            this.grlMauGuiSms.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.grlMauGuiSms.Properties.DisplayMember = "Name";
            this.grlMauGuiSms.Properties.NullText = "--Chọn mẫu gửi";
            this.grlMauGuiSms.Properties.PopupView = this.gridLookUpEdit1View;
            this.grlMauGuiSms.Properties.ValueMember = "Id";
            this.grlMauGuiSms.Size = new System.Drawing.Size(250, 20);
            this.grlMauGuiSms.TabIndex = 15;
            this.grlMauGuiSms.EditValueChanged += new System.EventHandler(this.grlMauGuiSms_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Mã mẫu";
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mẫu in";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Mẫu gửi (*):";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 62);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 13);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "Nội dung (*):";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(689, 376);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(520, 376);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(82, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Chạy";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(608, 376);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Dừng";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Tiến trình gửi thông báo phí qua email";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // lkMauIn
            // 
            this.lkMauIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lkMauIn.Location = new System.Drawing.Point(453, 10);
            this.lkMauIn.Name = "lkMauIn";
            this.lkMauIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkMauIn.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name13")});
            this.lkMauIn.Properties.DisplayMember = "Name";
            this.lkMauIn.Properties.NullText = "";
            this.lkMauIn.Properties.ShowHeader = false;
            this.lkMauIn.Properties.ValueMember = "ID";
            this.lkMauIn.Size = new System.Drawing.Size(286, 20);
            this.lkMauIn.TabIndex = 25;
            // 
            // lkTaiKhoan
            // 
            this.lkTaiKhoan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lkTaiKhoan.Location = new System.Drawing.Point(454, 33);
            this.lkTaiKhoan.Name = "lkTaiKhoan";
            this.lkTaiKhoan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkTaiKhoan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SoTK", "Số TK", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ChuTK", "Chủ TK", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNH", "Tên ngân hàng", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lkTaiKhoan.Properties.DisplayMember = "SoTK";
            this.lkTaiKhoan.Properties.NullText = "";
            this.lkTaiKhoan.Properties.ShowLines = false;
            this.lkTaiKhoan.Properties.ValueMember = "ID";
            this.lkTaiKhoan.Size = new System.Drawing.Size(286, 20);
            this.lkTaiKhoan.TabIndex = 24;
            // 
            // ckbLoaiDichVu
            // 
            this.ckbLoaiDichVu.Location = new System.Drawing.Point(88, 37);
            this.ckbLoaiDichVu.Name = "ckbLoaiDichVu";
            this.ckbLoaiDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ckbLoaiDichVu.Properties.DisplayMember = "TenLDV";
            this.ckbLoaiDichVu.Properties.SelectAllItemCaption = "Chọn tất cả";
            this.ckbLoaiDichVu.Properties.ValueMember = "ID";
            this.ckbLoaiDichVu.Size = new System.Drawing.Size(250, 20);
            this.ckbLoaiDichVu.TabIndex = 23;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(344, 40);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(104, 13);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "Tài khoản ngân hàng:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(344, 18);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 21;
            this.labelControl6.Text = "Mẫu file :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 13);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Loại dịch vụ:";
            // 
            // frmSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 411);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gửi thông báo phí qua zalo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSend_FormClosing);
            this.Load += new System.EventHandler(this.frmSend_Load);
            this.Resize += new System.EventHandler(this.frmSend_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grlMauGuiSms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkMauIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkTaiKhoan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbLoaiDichVu.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.LabelControl lblTongSo;
        private DevExpress.XtraEditors.LabelControl lblThanhCong;
        private DevExpress.XtraEditors.LabelControl lblThatBai;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.GridLookUpEdit grlMauGuiSms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox txtNoiDung;
        private DevExpress.XtraEditors.LookUpEdit lkMauIn;
        private DevExpress.XtraEditors.LookUpEdit lkTaiKhoan;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ckbLoaiDichVu;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}