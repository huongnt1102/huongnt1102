namespace TaiSan.KhoHang
{
    partial class frmChuyenKhoNB
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lookKhoChuyen = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.spinHSD1 = new DevExpress.XtraEditors.SpinEdit();
            this.spinDonGia1 = new DevExpress.XtraEditors.SpinEdit();
            this.spinSL1 = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lookKhoNhan = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.spinHSD2 = new DevExpress.XtraEditors.SpinEdit();
            this.spinDonGia2 = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateNgayChuyen = new DevExpress.XtraEditors.DateEdit();
            this.SpinSLChuyen = new DevExpress.XtraEditors.SpinEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.slookLoaiTaiSan = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhoChuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHSD1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSL1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhoNhan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHSD2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChuyen.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinSLChuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slookLoaiTaiSan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.lookKhoChuyen);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.spinHSD1);
            this.groupControl1.Controls.Add(this.spinDonGia1);
            this.groupControl1.Controls.Add(this.spinSL1);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(6, 88);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(278, 136);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Kho chuyển";
            // 
            // lookKhoChuyen
            // 
            this.lookKhoChuyen.Location = new System.Drawing.Point(86, 30);
            this.lookKhoChuyen.Name = "lookKhoChuyen";
            this.lookKhoChuyen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKhoChuyen.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKho", "Mã kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKho", "Tên kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DiaChiKho", "Địa chỉ")});
            this.lookKhoChuyen.Properties.DisplayMember = "TenKho";
            this.lookKhoChuyen.Properties.NullText = "[Kho...]";
            this.lookKhoChuyen.Properties.ValueMember = "ID";
            this.lookKhoChuyen.Size = new System.Drawing.Size(171, 20);
            this.lookKhoChuyen.TabIndex = 0;
            this.lookKhoChuyen.EditValueChanged += new System.EventHandler(this.lookKhoChuyen_EditValueChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 33);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(22, 13);
            this.labelControl10.TabIndex = 12;
            this.labelControl10.Text = "Kho:";
            // 
            // spinHSD1
            // 
            this.spinHSD1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinHSD1.Location = new System.Drawing.Point(86, 108);
            this.spinHSD1.Name = "spinHSD1";
            this.spinHSD1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinHSD1.Properties.DisplayFormat.FormatString = "{0:#,0.#} tháng";
            this.spinHSD1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHSD1.Properties.EditFormat.FormatString = "{0:#,0.#} tháng";
            this.spinHSD1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHSD1.Size = new System.Drawing.Size(171, 20);
            this.spinHSD1.TabIndex = 3;
            // 
            // spinDonGia1
            // 
            this.spinDonGia1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinDonGia1.Location = new System.Drawing.Point(86, 82);
            this.spinDonGia1.Name = "spinDonGia1";
            this.spinDonGia1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia1.Properties.DisplayFormat.FormatString = "{0:#,0.##} VNĐ";
            this.spinDonGia1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia1.Properties.EditFormat.FormatString = "{0:#,0.##} VNĐ";
            this.spinDonGia1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia1.Size = new System.Drawing.Size(171, 20);
            this.spinDonGia1.TabIndex = 2;
            // 
            // spinSL1
            // 
            this.spinSL1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSL1.Location = new System.Drawing.Point(86, 56);
            this.spinSL1.Name = "spinSL1";
            this.spinSL1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinSL1.Size = new System.Drawing.Size(171, 20);
            this.spinSL1.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 85);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(41, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Đơn giá:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 111);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Hạn sử dụng:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 59);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Số lượng:";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.lookKhoNhan);
            this.groupControl2.Controls.Add(this.SpinSLChuyen);
            this.groupControl2.Controls.Add(this.labelControl11);
            this.groupControl2.Controls.Add(this.spinHSD2);
            this.groupControl2.Controls.Add(this.spinDonGia2);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Location = new System.Drawing.Point(295, 88);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(276, 136);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Kho nhận";
            // 
            // lookKhoNhan
            // 
            this.lookKhoNhan.Location = new System.Drawing.Point(85, 30);
            this.lookKhoNhan.Name = "lookKhoNhan";
            this.lookKhoNhan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKhoNhan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaKho", "Mã kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenKho", "Tên kho"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DiaChiKho", "Địa chỉ")});
            this.lookKhoNhan.Properties.DisplayMember = "TenKho";
            this.lookKhoNhan.Properties.NullText = "[Kho...]";
            this.lookKhoNhan.Properties.ValueMember = "ID";
            this.lookKhoNhan.Size = new System.Drawing.Size(171, 20);
            this.lookKhoNhan.TabIndex = 0;
            this.lookKhoNhan.EditValueChanged += new System.EventHandler(this.lookKhoNhan_EditValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(11, 33);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(22, 13);
            this.labelControl11.TabIndex = 18;
            this.labelControl11.Text = "Kho:";
            // 
            // spinHSD2
            // 
            this.spinHSD2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinHSD2.Location = new System.Drawing.Point(85, 108);
            this.spinHSD2.Name = "spinHSD2";
            this.spinHSD2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinHSD2.Properties.DisplayFormat.FormatString = "{0:#,0.#} tháng";
            this.spinHSD2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHSD2.Properties.EditFormat.FormatString = "{0:#,0.#} tháng";
            this.spinHSD2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinHSD2.Size = new System.Drawing.Size(171, 20);
            this.spinHSD2.TabIndex = 3;
            // 
            // spinDonGia2
            // 
            this.spinDonGia2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinDonGia2.Location = new System.Drawing.Point(85, 82);
            this.spinDonGia2.Name = "spinDonGia2";
            this.spinDonGia2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinDonGia2.Properties.DisplayFormat.FormatString = "{0:#,0.##} VNĐ";
            this.spinDonGia2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia2.Properties.EditFormat.FormatString = "{0:#,0.##} VNĐ";
            this.spinDonGia2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinDonGia2.Size = new System.Drawing.Size(171, 20);
            this.spinDonGia2.TabIndex = 2;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 85);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(41, 13);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "Đơn giá:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(11, 111);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(65, 13);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "Hạn sử dụng:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(11, 59);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(73, 13);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "Số lượng nhận:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Loại tài sản:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(300, 36);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Ngày chuyển:";
            // 
            // dateNgayChuyen
            // 
            this.dateNgayChuyen.EditValue = null;
            this.dateNgayChuyen.Location = new System.Drawing.Point(374, 33);
            this.dateNgayChuyen.Name = "dateNgayChuyen";
            this.dateNgayChuyen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayChuyen.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateNgayChuyen.Size = new System.Drawing.Size(171, 20);
            this.dateNgayChuyen.TabIndex = 1;
            // 
            // SpinSLChuyen
            // 
            this.SpinSLChuyen.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpinSLChuyen.Location = new System.Drawing.Point(85, 56);
            this.SpinSLChuyen.Name = "SpinSLChuyen";
            this.SpinSLChuyen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SpinSLChuyen.Size = new System.Drawing.Size(171, 20);
            this.SpinSLChuyen.TabIndex = 2;
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.slookLoaiTaiSan);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.dateNgayChuyen);
            this.groupControl3.Location = new System.Drawing.Point(6, 12);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(565, 70);
            this.groupControl3.TabIndex = 17;
            this.groupControl3.Text = "Thông tin chung";
            // 
            // slookLoaiTaiSan
            // 
            this.slookLoaiTaiSan.Location = new System.Drawing.Point(70, 33);
            this.slookLoaiTaiSan.Name = "slookLoaiTaiSan";
            this.slookLoaiTaiSan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slookLoaiTaiSan.Properties.DisplayMember = "TenLTS";
            this.slookLoaiTaiSan.Properties.NullText = "[Tài sản....]";
            this.slookLoaiTaiSan.Properties.ValueMember = "MaLTS";
            this.slookLoaiTaiSan.Properties.View = this.searchLookUpEdit1View;
            this.slookLoaiTaiSan.Size = new System.Drawing.Size(187, 20);
            this.slookLoaiTaiSan.TabIndex = 0;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ColumnAutoWidth = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(415, 230);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(75, 32);
            this.btnXacNhan.TabIndex = 0;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(496, 230);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 32);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Ký hiệu";
            this.gridColumn1.FieldName = "KyHieu";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 54;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tên TS";
            this.gridColumn2.FieldName = "TenLTS";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Đặc tính";
            this.gridColumn3.FieldName = "DacTinh";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 233;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Hạn sử dụng (tháng)";
            this.gridColumn4.FieldName = "HanSuDung";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 113;
            // 
            // frmChuyenKhoNB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 271);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmChuyenKhoNB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chuyển kho nội bộ";
            this.Load += new System.EventHandler(this.frmChuyenKhoNB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhoChuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHSD1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSL1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookKhoNhan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinHSD2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDonGia2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChuyen.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayChuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinSLChuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slookLoaiTaiSan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SpinEdit spinHSD1;
        private DevExpress.XtraEditors.SpinEdit spinDonGia1;
        private DevExpress.XtraEditors.SpinEdit spinSL1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SpinEdit spinHSD2;
        private DevExpress.XtraEditors.SpinEdit spinDonGia2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateNgayChuyen;
        private DevExpress.XtraEditors.SpinEdit SpinSLChuyen;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LookUpEdit lookKhoChuyen;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LookUpEdit lookKhoNhan;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SearchLookUpEdit slookLoaiTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
    }
}