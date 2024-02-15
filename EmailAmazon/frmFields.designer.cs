namespace EmailAmazon
{
    partial class frmFields
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
            this.lbField = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.lbField)).BeginInit();
            this.SuspendLayout();
            // 
            // lbField
            // 
            this.lbField.DisplayMember = "FieldName";
            this.lbField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbField.Location = new System.Drawing.Point(0, 0);
            this.lbField.Name = "lbField";
            this.lbField.Size = new System.Drawing.Size(199, 207);
            this.lbField.TabIndex = 0;
            this.lbField.ValueMember = "Symbol";
            this.lbField.DoubleClick += new System.EventHandler(this.lbField_DoubleClick);
            // 
            // frmFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 207);
            this.Controls.Add(this.lbField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFields";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trường trộn Mail";
            this.Load += new System.EventHandler(this.frmFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lbField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lbField;

    }
}