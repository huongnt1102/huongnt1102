namespace ToaNha.NhacViec_ThongBao
{
    partial class frmNhacViecEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNhacViecEdit));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNoiDung = new DevExpress.XtraEditors.MemoEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.grvNguoiNhan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lookNguoiNhan = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDaDoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNguoiNhan = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNguoiNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNguoiNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiNhan)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(13, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(51, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nội dung:";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(13, 32);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(479, 96);
            this.txtNoiDung.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(407, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // btnOK
            // 
            this.btnOK.ImageOptions.ImageIndex = 0;
            this.btnOK.ImageOptions.ImageList = this.imageCollection1;
            this.btnOK.Location = new System.Drawing.Point(305, 341);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 30);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&Lưu và thoát";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grvNguoiNhan
            // 
            this.grvNguoiNhan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colDaDoc});
            this.grvNguoiNhan.GridControl = this.gcNguoiNhan;
            this.grvNguoiNhan.Name = "grvNguoiNhan";
            this.grvNguoiNhan.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.grvNguoiNhan.OptionsCustomization.AllowGroup = false;
            this.grvNguoiNhan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvNguoiNhan.OptionsView.ShowGroupPanel = false;
            this.grvNguoiNhan.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvNguoiNhan_InitNewRow);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Người nhận";
            this.gridColumn1.ColumnEdit = this.lookNguoiNhan;
            this.gridColumn1.FieldName = "NguoiNhan";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // lookNguoiNhan
            // 
            this.lookNguoiNhan.AutoHeight = false;
            this.lookNguoiNhan.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookNguoiNhan.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaSoNV", "Mã số NV", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HoTenNV", "Họ tên NV", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.lookNguoiNhan.DisplayMember = "HoTenNV";
            this.lookNguoiNhan.Name = "lookNguoiNhan";
            this.lookNguoiNhan.NullText = "";
            this.lookNguoiNhan.ValueMember = "MaNV";
            // 
            // colDaDoc
            // 
            this.colDaDoc.Caption = "DaDoc";
            this.colDaDoc.FieldName = "DaDoc";
            this.colDaDoc.Name = "colDaDoc";
            // 
            // gcNguoiNhan
            // 
            this.gcNguoiNhan.Location = new System.Drawing.Point(13, 135);
            this.gcNguoiNhan.MainView = this.grvNguoiNhan;
            this.gcNguoiNhan.Name = "gcNguoiNhan";
            this.gcNguoiNhan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lookNguoiNhan});
            this.gcNguoiNhan.Size = new System.Drawing.Size(479, 200);
            this.gcNguoiNhan.TabIndex = 2;
            this.gcNguoiNhan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNguoiNhan});
            // 
            // frmNhacViecEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 382);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gcNguoiNhan);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNhacViecEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhiệm vụ";
            this.Load += new System.EventHandler(this.frmNhacViecEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNoiDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNguoiNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookNguoiNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguoiNhan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtNoiDung;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNguoiNhan;
        private DevExpress.XtraGrid.GridControl gcNguoiNhan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookNguoiNhan;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn colDaDoc;
    }
}