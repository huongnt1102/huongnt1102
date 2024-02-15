namespace DichVu.PhiQuanLy
{
    partial class frmDinhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDinhMuc));
            this.gcDinhMuc = new DevExpress.XtraGrid.GridControl();
            this.grvDinhMuc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinThang = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDinhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDinhMuc
            // 
            this.gcDinhMuc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcDinhMuc.Location = new System.Drawing.Point(12, 12);
            this.gcDinhMuc.MainView = this.grvDinhMuc;
            this.gcDinhMuc.Name = "gcDinhMuc";
            this.gcDinhMuc.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.spinThang});
            this.gcDinhMuc.Size = new System.Drawing.Size(854, 308);
            this.gcDinhMuc.TabIndex = 0;
            this.gcDinhMuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvDinhMuc});
            // 
            // grvDinhMuc
            // 
            this.grvDinhMuc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn3});
            this.grvDinhMuc.GridControl = this.gcDinhMuc;
            this.grvDinhMuc.Name = "grvDinhMuc";
            this.grvDinhMuc.OptionsCustomization.AllowColumnMoving = false;
            this.grvDinhMuc.OptionsCustomization.AllowFilter = false;
            this.grvDinhMuc.OptionsCustomization.AllowGroup = false;
            this.grvDinhMuc.OptionsCustomization.AllowQuickHideColumns = false;
            this.grvDinhMuc.OptionsCustomization.AllowSort = false;
            this.grvDinhMuc.OptionsMenu.EnableColumnMenu = false;
            this.grvDinhMuc.OptionsSelection.MultiSelect = true;
            this.grvDinhMuc.OptionsView.ColumnAutoWidth = false;
            this.grvDinhMuc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvDinhMuc.OptionsView.ShowGroupPanel = false;
            this.grvDinhMuc.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.grvDinhMuc_InitNewRow);
            this.grvDinhMuc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvDinhMuc_KeyUp);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn1.Caption = "Số tháng thanh toán";
            this.gridColumn1.ColumnEdit = this.spinThang;
            this.gridColumn1.FieldName = "SoThangThanhToan";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 128;
            // 
            // spinThang
            // 
            this.spinThang.AutoHeight = false;
            this.spinThang.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinThang.DisplayFormat.FormatString = "{0:#,0.#} tháng";
            this.spinThang.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThang.EditFormat.FormatString = "{0:#,0.#} tháng";
            this.spinThang.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThang.Name = "spinThang";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn2.Caption = "Tỉ lệ";
            this.gridColumn2.ColumnEdit = this.repositoryItemSpinEdit1;
            this.gridColumn2.FieldName = "TiLeChietKhau";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 79;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.Appearance.Options.UseTextOptions = true;
            this.repositoryItemSpinEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.DisplayFormat.FormatString = "{0:#,0.##}";
            this.repositoryItemSpinEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.EditFormat.FormatString = "{0:#,0.##}";
            this.repositoryItemSpinEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "ID";
            this.gridColumn4.FieldName = "ID";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Diễn giải";
            this.gridColumn3.FieldName = "DienGiai";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 629;
            // 
            // btnHuy
            // 
            this.btnHuy.ImageOptions.ImageIndex = 1;
            this.btnHuy.ImageOptions.ImageList = this.imageCollection1;
            this.btnHuy.Location = new System.Drawing.Point(771, 326);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(93, 30);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Save1.png");
            this.imageCollection1.Images.SetKeyName(1, "Cancel1.png");
            // 
            // btnLuu
            // 
            this.btnLuu.ImageOptions.ImageIndex = 0;
            this.btnLuu.ImageOptions.ImageList = this.imageCollection1;
            this.btnLuu.Location = new System.Drawing.Point(667, 326);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(98, 30);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu && Đóng";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // frmDinhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 361);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.gcDinhMuc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmDinhMuc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chiết khấu PQL";
            this.Load += new System.EventHandler(this.frmDinhMuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvDinhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDinhMuc;
        private DevExpress.XtraGrid.Views.Grid.GridView grvDinhMuc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinThang;
    }
}