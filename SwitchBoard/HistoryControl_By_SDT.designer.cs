namespace DIP.SwitchBoard
{
    partial class HistoryControl_By_SDT
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.historyEditorDB1 = new DIP.SoftPhoneAPI.HistoryEditorDB();
            this.SuspendLayout();
            // 
            // historyEditorDB1
            // 
            this.historyEditorDB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyEditorDB1.Location = new System.Drawing.Point(0, 0);
            this.historyEditorDB1.Name = "historyEditorDB1";
            this.historyEditorDB1.Size = new System.Drawing.Size(1039, 411);
            this.historyEditorDB1.TabIndex = 0;
            this.historyEditorDB1.Tag = "Nhật ký cuộc gọi";
            this.historyEditorDB1.CallHistoryRowClick += new DIP.SoftPhoneAPI.CallHistoryRowClickEventHandler(this.historyEditor1_CallHistoryRowClick);
            // 
            // HistoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.historyEditorDB1);
            this.Name = "HistoryControl";
            this.Size = new System.Drawing.Size(1039, 411);
            this.ResumeLayout(false);

        }

        #endregion

        private SoftPhoneAPI.HistoryEditorDB historyEditorDB1;

    }
}
