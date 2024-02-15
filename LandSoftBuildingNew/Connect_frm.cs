using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace LandSoftBuildingMain
{
    public partial class Connect_frm : DevExpress.XtraEditors.XtraForm
    {
        public Connect_frm()
        {
            InitializeComponent();

            //TranslateLanguage.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            var sqlConnBuild = new System.Data.SqlClient.SqlConnectionStringBuilder();

            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                sqlConnBuild.ConnectionString = it.EncDec.Decrypt(txtKey.Text);
            }
            else
            {
                sqlConnBuild.InitialCatalog = txtDatabaseName.Text;
                sqlConnBuild.DataSource = txtServerName.Text;
                sqlConnBuild.UserID = txtUserName.Text;
                sqlConnBuild.Password = txtPassword.Text;
            }

            if (SqlCommon.sqlTestConnect(sqlConnBuild.ConnectionString))
            {
                var sqlInfo = new Library.SqlSetting();
                sqlInfo.SqlConn = sqlConnBuild.ConnectionString;
                sqlInfo.Conn = it.EncDec.Encrypt(sqlConnBuild.ConnectionString);
                sqlInfo.Server = sqlConnBuild.DataSource;
                sqlInfo.UserID = sqlConnBuild.UserID;
                sqlInfo.Database = sqlConnBuild.InitialCatalog;
                sqlInfo.Save();         

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                DialogBox.Success("Kết nối không thành công. Vui lòng kiểm tra lại, xin cảm ơn.");  
            }
        }

        private void Connect_frm_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabControl1.SelectedTabPageIndex = 0;

            var sqlInfo = new Library.SqlSetting();
            txtDatabaseName.Text = sqlInfo.Database;
            txtServerName.Text = sqlInfo.Server;
            txtUserName.Text = sqlInfo.UserID;
        }
    }
}