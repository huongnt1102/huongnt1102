namespace LandSoftBuilding.Marketing.Mail.Templates
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.htmlContent = new MSDN.Html.Editor.HtmlEditorControl();
            this.lookCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTempName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.btnFields = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTempName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.htmlContent);
            this.panelControl1.Controls.Add(this.lookCategory);
            this.panelControl1.Controls.Add(this.txtTempName);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(743, 405);
            this.panelControl1.TabIndex = 0;
            // 
            // htmlContent
            // 
            this.htmlContent.InnerText = null;
            this.htmlContent.Location = new System.Drawing.Point(14, 43);
            this.htmlContent.Name = "htmlContent";
            this.htmlContent.Size = new System.Drawing.Size(714, 348);
            this.htmlContent.TabIndex = 4;
            this.htmlContent.ToolbarDock = System.Windows.Forms.DockStyle.Top;
            // 
            // lookCategory
            // 
            this.lookCategory.Location = new System.Drawing.Point(534, 17);
            this.lookCategory.Name = "lookCategory";
            this.lookCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CateName", "Name1")});
            this.lookCategory.Properties.DisplayMember = "CateName";
            this.lookCategory.Properties.NullText = "<Vui lòng chọn>";
            this.lookCategory.Properties.ShowHeader = false;
            this.lookCategory.Properties.ValueMember = "CateID";
            this.lookCategory.Size = new System.Drawing.Size(194, 20);
            this.lookCategory.TabIndex = 3;
            // 
            // txtTempName
            // 
            this.txtTempName.Location = new System.Drawing.Point(82, 17);
            this.txtTempName.Name = "txtTempName";
            this.txtTempName.Size = new System.Drawing.Size(369, 20);
            this.txtTempName.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(465, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Loại mẫu (*):";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên mẫu (*):";
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(676, 423);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(79, 23);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy - ESC";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.Location = new System.Drawing.Point(579, 423);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(91, 23);
            this.btnDongY.TabIndex = 4;
            this.btnDongY.Text = "Lưu && Đóng";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // btnFields
            // 
            this.btnFields.Location = new System.Drawing.Point(482, 423);
            this.btnFields.Name = "btnFields";
            this.btnFields.Size = new System.Drawing.Size(91, 23);
            this.btnFields.TabIndex = 4;
            this.btnFields.Text = "Trường trộn";
            this.btnFields.Click += new System.EventHandler(this.btnFields_Click);
            // 
            // frmTemplates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 458);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnFields);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTemplates";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mẫu mail";
            this.Load += new System.EventHandler(this.frmTemplates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTempName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtTempName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.LookUpEdit lookCategory;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnFields;
        private MSDN.Html.Editor.HtmlEditorControl htmlContent;
    }
}