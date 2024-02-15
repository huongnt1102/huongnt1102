namespace Building.SMSZalo
{
    partial class frmGetCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGetCustomer));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblThatBai = new DevExpress.XtraEditors.LabelControl();
            this.txtProgess = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblThanhCong = new DevExpress.XtraEditors.LabelControl();
            this.lblTong = new DevExpress.XtraEditors.LabelControl();
            this.glkDuAn = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProgess.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDuAn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(578, 81);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 28);
            this.btnCancel.TabIndex = 2;
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
            this.btnSave.Location = new System.Drawing.Point(509, 81);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnChapNhan_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblThatBai);
            this.panelControl1.Controls.Add(this.txtProgess);
            this.panelControl1.Controls.Add(this.lblThanhCong);
            this.panelControl1.Controls.Add(this.lblTong);
            this.panelControl1.Controls.Add(this.glkDuAn);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(644, 75);
            this.panelControl1.TabIndex = 0;
            // 
            // lblThatBai
            // 
            this.lblThatBai.Location = new System.Drawing.Point(321, 57);
            this.lblThatBai.Name = "lblThatBai";
            this.lblThatBai.Size = new System.Drawing.Size(43, 13);
            this.lblThatBai.TabIndex = 16;
            this.lblThatBai.Text = "Thất bại:";
            // 
            // txtProgess
            // 
            this.txtProgess.Location = new System.Drawing.Point(70, 33);
            this.txtProgess.Name = "txtProgess";
            this.txtProgess.Properties.ShowTitle = true;
            this.txtProgess.Size = new System.Drawing.Size(569, 18);
            this.txtProgess.TabIndex = 15;
            // 
            // lblThanhCong
            // 
            this.lblThanhCong.Location = new System.Drawing.Point(201, 57);
            this.lblThanhCong.Name = "lblThanhCong";
            this.lblThanhCong.Size = new System.Drawing.Size(60, 13);
            this.lblThanhCong.TabIndex = 14;
            this.lblThanhCong.Text = "Thành công:";
            // 
            // lblTong
            // 
            this.lblTong.Location = new System.Drawing.Point(74, 57);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(42, 13);
            this.lblTong.TabIndex = 13;
            this.lblTong.Text = "Tổng số:";
            // 
            // glkDuAn
            // 
            this.glkDuAn.EditValue = "";
            this.glkDuAn.Location = new System.Drawing.Point(70, 12);
            this.glkDuAn.Name = "glkDuAn";
            this.glkDuAn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkDuAn.Properties.DisplayMember = "TenTN";
            this.glkDuAn.Properties.NullText = "";
            this.glkDuAn.Properties.PopupView = this.gridLookUpEdit1View;
            this.glkDuAn.Properties.ValueMember = "MaTN";
            this.glkDuAn.Size = new System.Drawing.Size(569, 20);
            this.glkDuAn.TabIndex = 12;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowAutoFilterRow = true;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mã dự án";
            this.gridColumn3.FieldName = "MaTN";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên viết tắt";
            this.gridColumn1.FieldName = "TenVT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 153;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Dự án";
            this.gridColumn2.FieldName = "TenTN";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 915;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên dự án:";
            // 
            // frmGetCustomer
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(644, 114);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmGetCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lấy danh sách khách hàng";
            this.Load += new System.EventHandler(this.frmDinhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProgess.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDuAn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GridLookUpEdit glkDuAn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.ProgressBarControl txtProgess;
        private DevExpress.XtraEditors.LabelControl lblThanhCong;
        private DevExpress.XtraEditors.LabelControl lblTong;
        private DevExpress.XtraEditors.LabelControl lblThatBai;
    }
}