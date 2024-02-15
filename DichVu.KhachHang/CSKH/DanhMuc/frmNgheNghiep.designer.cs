namespace DichVu.KhachHang.CSKH
{
    partial class frmNgheNghiep
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
            this.gcNguonKH = new DevExpress.XtraGrid.GridControl();
            this.grvNguonKH = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcNguonKH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNguonKH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcNguonKH
            // 
            this.gcNguonKH.Location = new System.Drawing.Point(12, 12);
            this.gcNguonKH.MainView = this.grvNguonKH;
            this.gcNguonKH.Name = "gcNguonKH";
            this.gcNguonKH.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemColorEdit1});
            this.gcNguonKH.ShowOnlyPredefinedDetails = true;
            this.gcNguonKH.Size = new System.Drawing.Size(449, 237);
            this.gcNguonKH.TabIndex = 0;
            this.gcNguonKH.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvNguonKH});
            // 
            // grvNguonKH
            // 
            this.grvNguonKH.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1});
            this.grvNguonKH.GridControl = this.gcNguonKH;
            this.grvNguonKH.Name = "grvNguonKH";
            this.grvNguonKH.OptionsCustomization.AllowGroup = false;
            this.grvNguonKH.OptionsSelection.MultiSelect = true;
            this.grvNguonKH.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.grvNguonKH.OptionsView.ShowAutoFilterRow = true;
            this.grvNguonKH.OptionsView.ShowGroupPanel = false;
            this.grvNguonKH.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvNguonKH_KeyUp);
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "STT";
            this.gridColumn2.FieldName = "STT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 51;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên nghề nghiệp";
            this.gridColumn1.FieldName = "TenNN";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 380;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.ColorText = DevExpress.XtraEditors.Controls.ColorText.Integer;
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.StoreColorAsInteger = true;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(386, 255);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(287, 255);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(93, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu && Đóng";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(206, 255);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmNgheNghiep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 290);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.gcNguonKH);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmNgheNghiep";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nghề nghiệp";
            ((System.ComponentModel.ISupportInitialize)(this.gcNguonKH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvNguonKH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcNguonKH;
        private DevExpress.XtraGrid.Views.Grid.GridView grvNguonKH;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
    }
}