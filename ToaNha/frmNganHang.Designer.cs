namespace ToaNha
{
    partial class frmNganHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNganHang));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcBank = new DevExpress.XtraGrid.GridControl();
            this.grvBank = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.itemImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBank)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ImageOptions.ImageIndex = 2;
            this.btnCancel.ImageOptions.ImageList = this.imageCollection1;
            this.btnCancel.Location = new System.Drawing.Point(578, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(51, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Delete1.png");
            this.imageCollection1.Images.SetKeyName(1, "Save1.png");
            this.imageCollection1.Images.SetKeyName(2, "Cancel1.png");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageOptions.ImageIndex = 1;
            this.btnSave.ImageOptions.ImageList = this.imageCollection1;
            this.btnSave.Location = new System.Drawing.Point(483, 354);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu && Đóng";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gcBank
            // 
            this.gcBank.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcBank.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcBank.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcBank.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcBank.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcBank.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcBank.EmbeddedNavigator.TextStringFormat = "{0}/{1}";
            this.gcBank.Location = new System.Drawing.Point(12, 12);
            this.gcBank.MainView = this.grvBank;
            this.gcBank.Name = "gcBank";
            this.gcBank.ShowOnlyPredefinedDetails = true;
            this.gcBank.Size = new System.Drawing.Size(617, 336);
            this.gcBank.TabIndex = 1;
            this.gcBank.UseEmbeddedNavigator = true;
            this.gcBank.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBank});
            // 
            // grvBank
            // 
            this.grvBank.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2});
            this.grvBank.GridControl = this.gcBank;
            this.grvBank.Name = "grvBank";
            this.grvBank.OptionsSelection.MultiSelect = true;
            this.grvBank.OptionsView.ColumnAutoWidth = false;
            this.grvBank.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvBank.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "HOUSE_BANK";
            this.gridColumn3.FieldName = "HOUSE_BANK";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 103;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên ngân hàng";
            this.gridColumn1.FieldName = "TenNH";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 294;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Địa chỉ";
            this.gridColumn2.FieldName = "DiaChi";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 233;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.ImageOptions.ImageIndex = 0;
            this.btnDelete.ImageOptions.ImageList = this.imageCollection1;
            this.btnDelete.Location = new System.Drawing.Point(428, 354);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(49, 23);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // itemImport
            // 
            this.itemImport.Location = new System.Drawing.Point(331, 354);
            this.itemImport.Name = "itemImport";
            this.itemImport.Size = new System.Drawing.Size(75, 23);
            this.itemImport.TabIndex = 2;
            this.itemImport.Text = "Import";
            this.itemImport.UseVisualStyleBackColor = true;
            this.itemImport.Click += new System.EventHandler(this.itemImport_Click);
            // 
            // frmNganHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 389);
            this.Controls.Add(this.itemImport);
            this.Controls.Add(this.gcBank);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmNganHang";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ngân hàng";
            this.Load += new System.EventHandler(this.frmNganHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl gcBank;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBank;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.Button itemImport;
    }
}