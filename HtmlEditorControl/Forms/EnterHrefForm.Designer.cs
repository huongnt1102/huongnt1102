namespace MSDN.Html.Editor
{
    partial class EnterHrefForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterHrefForm));
            this.bInsert = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.labelHref = new System.Windows.Forms.Label();
            this.hrefText = new System.Windows.Forms.TextBox();
            this.hrefLink = new System.Windows.Forms.TextBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.listTargets = new System.Windows.Forms.ComboBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bInsert
            // 
            this.bInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bInsert.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.bInsert.Location = new System.Drawing.Point(245, 130);
            this.bInsert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bInsert.Name = "bInsert";
            this.bInsert.Size = new System.Drawing.Size(100, 28);
            this.bInsert.TabIndex = 0;
            this.bInsert.Text = "Insert Href";
            // 
            // bRemove
            // 
            this.bRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRemove.DialogResult = System.Windows.Forms.DialogResult.No;
            this.bRemove.Location = new System.Drawing.Point(352, 130);
            this.bRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(107, 28);
            this.bRemove.TabIndex = 1;
            this.bRemove.Text = "Remove Href";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(469, 130);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(100, 28);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(11, 20);
            this.labelText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(53, 28);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "Text:";
            // 
            // labelHref
            // 
            this.labelHref.Location = new System.Drawing.Point(11, 59);
            this.labelHref.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHref.Name = "labelHref";
            this.labelHref.Size = new System.Drawing.Size(53, 28);
            this.labelHref.TabIndex = 4;
            this.labelHref.Text = "Href:";
            // 
            // hrefText
            // 
            this.hrefText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hrefText.Location = new System.Drawing.Point(75, 20);
            this.hrefText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hrefText.Name = "hrefText";
            this.hrefText.ReadOnly = true;
            this.hrefText.Size = new System.Drawing.Size(489, 22);
            this.hrefText.TabIndex = 5;
            // 
            // hrefLink
            // 
            this.hrefLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hrefLink.Location = new System.Drawing.Point(75, 59);
            this.hrefLink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hrefLink.Name = "hrefLink";
            this.hrefLink.Size = new System.Drawing.Size(373, 22);
            this.hrefLink.TabIndex = 6;
            // 
            // labelTarget
            // 
            this.labelTarget.Location = new System.Drawing.Point(11, 98);
            this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(53, 28);
            this.labelTarget.TabIndex = 7;
            this.labelTarget.Text = "Target:";
            // 
            // listTargets
            // 
            this.listTargets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listTargets.Location = new System.Drawing.Point(75, 98);
            this.listTargets.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listTargets.Name = "listTargets";
            this.listTargets.Size = new System.Drawing.Size(160, 24);
            this.listTargets.TabIndex = 8;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(456, 56);
            this.btnBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(107, 28);
            this.btnBrowser.TabIndex = 9;
            this.btnBrowser.Text = "Browser";
            // 
            // EnterHrefForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(576, 167);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.listTargets);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.hrefLink);
            this.Controls.Add(this.hrefText);
            this.Controls.Add(this.labelHref);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bInsert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterHrefForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Enter Href";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button bInsert;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelHref;
        private System.Windows.Forms.TextBox hrefText;
        private System.Windows.Forms.TextBox hrefLink;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.ComboBox listTargets;
        private System.Windows.Forms.Button btnBrowser;
    }
}

