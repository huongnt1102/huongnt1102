namespace KyThuat.NhiemVu
{
    partial class Rings_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rings_frm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcRings = new DevExpress.XtraGrid.GridControl();
            this.gvRings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.btnListen = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRings)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcRings);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(318, 323);
            this.panelControl1.TabIndex = 0;
            // 
            // gcRings
            // 
            this.gcRings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRings.Location = new System.Drawing.Point(0, 0);
            this.gcRings.MainView = this.gvRings;
            this.gcRings.Name = "gcRings";
            this.gcRings.Size = new System.Drawing.Size(318, 323);
            this.gcRings.TabIndex = 0;
            this.gcRings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRings});
            // 
            // gvRings
            // 
            this.gvRings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gvRings.GridControl = this.gcRings;
            this.gvRings.Name = "gvRings";
            this.gvRings.OptionsView.ShowAutoFilterRow = true;
            this.gvRings.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Tên nhạc chuông";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.FieldName = "FileName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           // this.btnClose.Image = global::Building.WorkSchedule.Properties.Resources.Cancel;
            this.btnClose.Location = new System.Drawing.Point(255, 341);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Bỏ qua";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAccept
            // 
           // this.btnAccept.Image = global::Building.WorkSchedule.Properties.Resources.finished_2;
            this.btnAccept.Location = new System.Drawing.Point(174, 341);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Đồng ý";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnListen
            // 
           // this.btnListen.Image = global::Building.WorkSchedule.Properties.Resources.ringtone;
            this.btnListen.Location = new System.Drawing.Point(93, 341);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(75, 23);
            this.btnListen.TabIndex = 1;
            this.btnListen.Text = "Nghe thử";
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnStop
            // 
            //this.btnStop.Image = global::Building.WorkSchedule.Properties.Resources.stop;
            this.btnStop.Location = new System.Drawing.Point(12, 341);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Dừng";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Rings_frm
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(342, 376);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Rings_frm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn nhạc chuông";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Rings_frm_FormClosing);
            this.Shown += new System.EventHandler(this.Rings_frm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnAccept;
        private DevExpress.XtraGrid.GridControl gcRings;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRings;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.SimpleButton btnListen;
        private DevExpress.XtraEditors.SimpleButton btnStop;
    }
}