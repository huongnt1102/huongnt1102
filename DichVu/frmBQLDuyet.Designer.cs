﻿namespace DichVu
{
    partial class frmBQLDuyet
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
            this.checkDuyet = new DevExpress.XtraEditors.CheckEdit();
            this.btnSave = new System.Windows.Forms.Button();
            this.meNoiDung = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDuyet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meNoiDung.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // checkDuyet
            // 
            this.checkDuyet.Location = new System.Drawing.Point(378, 201);
            this.checkDuyet.Name = "checkDuyet";
            this.checkDuyet.Properties.Caption = "Duyệt";
            this.checkDuyet.Size = new System.Drawing.Size(75, 19);
            this.checkDuyet.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(459, 194);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "LƯU";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // meNoiDung
            // 
            this.meNoiDung.Location = new System.Drawing.Point(22, 14);
            this.meNoiDung.Name = "meNoiDung";
            this.meNoiDung.Size = new System.Drawing.Size(534, 168);
            this.meNoiDung.TabIndex = 6;
            // 
            // frmBQLDuyet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 241);
            this.Controls.Add(this.checkDuyet);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.meNoiDung);
            this.Name = "frmBQLDuyet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BAN QUẢN LÝ DUYỆT";
            this.Load += new System.EventHandler(this.frmBQLDuyet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkDuyet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meNoiDung.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit checkDuyet;
        private System.Windows.Forms.Button btnSave;
        private DevExpress.XtraEditors.MemoEdit meNoiDung;
    }
}