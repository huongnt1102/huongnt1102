namespace DichVu.KhachHang.CSKH
{
    partial class frmLoaiBaoGia
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
			this.gcCapDo = new DevExpress.XtraGrid.GridControl();
			this.grvCapDo = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
			this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
			this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.gcCapDo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grvCapDo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
			this.SuspendLayout();
			// 
			// gcCapDo
			// 
			this.gcCapDo.Location = new System.Drawing.Point(12, 12);
			this.gcCapDo.MainView = this.grvCapDo;
			this.gcCapDo.Name = "gcCapDo";
			this.gcCapDo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemColorEdit1});
			this.gcCapDo.ShowOnlyPredefinedDetails = true;
			this.gcCapDo.Size = new System.Drawing.Size(449, 237);
			this.gcCapDo.TabIndex = 0;
			this.gcCapDo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvCapDo});
			// 
			// grvCapDo
			// 
			this.grvCapDo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
			this.grvCapDo.GridControl = this.gcCapDo;
			this.grvCapDo.Name = "grvCapDo";
			this.grvCapDo.OptionsCustomization.AllowGroup = false;
			this.grvCapDo.OptionsSelection.MultiSelect = true;
			this.grvCapDo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
			this.grvCapDo.OptionsView.ShowGroupPanel = false;
			this.grvCapDo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grvCapDo_KeyUp);
			// 
			// gridColumn1
			// 
			this.gridColumn1.Caption = "Loại báo giá";
			this.gridColumn1.FieldName = "Name";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.Visible = true;
			this.gridColumn1.VisibleIndex = 0;
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
			// frmLoaiBaoGia
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(473, 290);
			this.Controls.Add(this.btnLuu);
			this.Controls.Add(this.btnHuy);
			this.Controls.Add(this.gcCapDo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmLoaiBaoGia";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Loại báo giá";
			((System.ComponentModel.ISupportInitialize)(this.gcCapDo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grvCapDo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCapDo;
        private DevExpress.XtraGrid.Views.Grid.GridView grvCapDo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
    }
}