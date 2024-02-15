using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using System.Data.SqlClient;

namespace LandSoftBuildingMain
{
    public partial class frmConnection : DevExpress.XtraEditors.XtraForm
    {
        public frmConnection()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //global::Library.Properties.Settings.Default.Building_dbConnectionString = string.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", txtServer.Text.Trim(), txtdatabase.Text.Trim(), txtUsername.Text.Trim(), txtpassword.Text.Trim());
            global::Library.Properties.Settings.Default.Save();
            try
            {
                TestConnect(global::Library.Properties.Settings.Default.Building_dbConnectionString);

                this.Close();
            }
            catch
            {
                Library.DialogBox.Error("Không thể kết nối database.");  
            }
        }

        public static bool TestConnect(string Connection)
        {
            SqlConnection SqlConn = new SqlConnection(Connection);
            try
            {
                SqlConn.Open();
                SqlConn.Close();
                return true;
            }
            catch
            {
                SqlConn.Close();
                return false;
            }
        }

        private void frmConnection_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder(global::Library.Properties.Settings.Default.SaveConnectionString);
            txtUsername.Text = con.UserID;
            txtServer.Text = con.DataSource;
            txtdatabase.Text = con.InitialCatalog;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}