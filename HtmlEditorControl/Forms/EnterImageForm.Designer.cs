namespace MSDN.Html.Editor
{
    partial class EnterImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterImageForm));
            this.bInsert = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.labelHref = new System.Windows.Forms.Label();
            this.hrefText = new System.Windows.Forms.TextBox();
            this.hrefLink = new System.Windows.Forms.TextBox();
            this.labelAlign = new System.Windows.Forms.Label();
            this.listAlign = new System.Windows.Forms.ComboBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.spHeight = new System.Windows.Forms.NumericUpDown();
            this.spWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // bInsert
            // 
            this.bInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bInsert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bInsert.Location = new System.Drawing.Point(352, 130);
            this.bInsert.Margin = new System.Windows.Forms.Padding(4);
            this.bInsert.Name = "bInsert";
            this.bInsert.Size = new System.Drawing.Size(107, 28);
            this.bInsert.TabIndex = 4;
            this.bInsert.Text = "Insert Image";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(469, 130);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(100, 28);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Cancel";
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(11, 49);
            this.labelText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(43, 28);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "Text:";
            // 
            // labelHref
            // 
            this.labelHref.Location = new System.Drawing.Point(11, 10);
            this.labelHref.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHref.Name = "labelHref";
            this.labelHref.Size = new System.Drawing.Size(43, 28);
            this.labelHref.TabIndex = 4;
            this.labelHref.Text = "Href:";
            // 
            // hrefText
            // 
            this.hrefText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hrefText.Location = new System.Drawing.Point(64, 49);
            this.hrefText.Margin = new System.Windows.Forms.Padding(4);
            this.hrefText.Name = "hrefText";
            this.hrefText.Size = new System.Drawing.Size(499, 22);
            this.hrefText.TabIndex = 2;
            // 
            // hrefLink
            // 
            this.hrefLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hrefLink.Location = new System.Drawing.Point(64, 10);
            this.hrefLink.Margin = new System.Windows.Forms.Padding(4);
            this.hrefLink.Name = "hrefLink";
            this.hrefLink.Size = new System.Drawing.Size(384, 22);
            this.hrefLink.TabIndex = 1;
            // 
            // labelAlign
            // 
            this.labelAlign.Location = new System.Drawing.Point(11, 98);
            this.labelAlign.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAlign.Name = "labelAlign";
            this.labelAlign.Size = new System.Drawing.Size(43, 28);
            this.labelAlign.TabIndex = 7;
            this.labelAlign.Text = "Align:";
            // 
            // listAlign
            // 
            this.listAlign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listAlign.FormattingEnabled = true;
            this.listAlign.Location = new System.Drawing.Point(64, 98);
            this.listAlign.Margin = new System.Windows.Forms.Padding(4);
            this.listAlign.Name = "listAlign";
            this.listAlign.Size = new System.Drawing.Size(127, 24);
            this.listAlign.TabIndex = 3;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(456, 7);
            this.btnBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(107, 28);
            this.btnBrowser.TabIndex = 8;
            this.btnBrowser.Text = "Browser";
            // 
            // spHeight
            // 
            this.spHeight.Location = new System.Drawing.Point(456, 96);
            this.spHeight.Margin = new System.Windows.Forms.Padding(4);
            this.spHeight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spHeight.Name = "spHeight";
            this.spHeight.Size = new System.Drawing.Size(107, 22);
            this.spHeight.TabIndex = 11;
            // 
            // spWidth
            // 
            this.spWidth.Location = new System.Drawing.Point(269, 96);
            this.spWidth.Margin = new System.Windows.Forms.Padding(4);
            this.spWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spWidth.Name = "spWidth";
            this.spWidth.Size = new System.Drawing.Size(107, 22);
            this.spWidth.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(400, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 28);
            this.label2.TabIndex = 9;
            this.label2.Text = "Height";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(214, 101);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Width";
            // 
            // EnterImageForm
            // 
            this.AcceptButton = this.bInsert;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(576, 167);
            this.Controls.Add(this.spHeight);
            this.Controls.Add(this.spWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.listAlign);
            this.Controls.Add(this.labelAlign);
            this.Controls.Add(this.hrefLink);
            this.Controls.Add(this.hrefText);
            this.Controls.Add(this.labelHref);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bInsert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterImageForm";
            this.ShowInTaskbar = false;
            this.Text = "Enter Image";
            this.Load += new System.EventHandler(this.EnterImageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button bInsert;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelHref;
        private System.Windows.Forms.TextBox hrefText;
        private System.Windows.Forms.TextBox hrefLink;
        private System.Windows.Forms.Label labelAlign;
        private System.Windows.Forms.ComboBox listAlign;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.NumericUpDown spHeight;
        private System.Windows.Forms.NumericUpDown spWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

