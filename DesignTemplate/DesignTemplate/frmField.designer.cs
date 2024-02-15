namespace LandSoft.DuAn.BieuMau
{
    partial class frmField
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeField = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.lblDienGiai = new DevExpress.XtraEditors.LabelControl();
            this.lblTenBT = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeField)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeField);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.lblDienGiai);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblTenBT);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(286, 512);
            this.splitContainerControl1.SplitterPosition = 56;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeField
            // 
            this.treeField.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Navy;
            this.treeField.Appearance.FocusedRow.Options.UseForeColor = true;
            this.treeField.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeField.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeField.KeyFieldName = "MaBT";
            this.treeField.Location = new System.Drawing.Point(0, 0);
            this.treeField.LookAndFeel.UseDefaultLookAndFeel = false;
            this.treeField.LookAndFeel.UseWindowsXPTheme = true;
            this.treeField.Name = "treeField";
            this.treeField.OptionsView.ShowColumns = false;
            this.treeField.OptionsView.ShowHorzLines = false;
            this.treeField.OptionsView.ShowIndicator = false;
            this.treeField.OptionsView.ShowVertLines = false;
            this.treeField.ParentFieldName = "MaLBT";
            this.treeField.Size = new System.Drawing.Size(286, 451);
            this.treeField.TabIndex = 1;
            this.treeField.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeField_FocusedNodeChanged);
            this.treeField.DoubleClick += new System.EventHandler(this.treeField_DoubleClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.FieldName = "TenBT";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // lblDienGiai
            // 
            this.lblDienGiai.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblDienGiai.Location = new System.Drawing.Point(6, 20);
            this.lblDienGiai.Name = "lblDienGiai";
            this.lblDienGiai.Size = new System.Drawing.Size(270, 13);
            this.lblDienGiai.TabIndex = 0;
            this.lblDienGiai.Text = "Diễn giải";
            // 
            // lblTenBT
            // 
            this.lblTenBT.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTenBT.Location = new System.Drawing.Point(6, 1);
            this.lblTenBT.Name = "lblTenBT";
            this.lblTenBT.Size = new System.Drawing.Size(53, 13);
            this.lblTenBT.TabIndex = 0;
            this.lblTenBT.Text = "Biểu thức";
            // 
            // frmField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 512);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(302, 550);
            this.Name = "frmField";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trường thông tin";
            this.Load += new System.EventHandler(this.frmField_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmField_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmField_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeField;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.LabelControl lblDienGiai;
        private DevExpress.XtraEditors.LabelControl lblTenBT;

    }
}