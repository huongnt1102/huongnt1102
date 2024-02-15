using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.GiuXe
{
    public partial class frmConfigAPI : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmConfigAPI()
        {
            InitializeComponent();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db.SubmitChanges();
            DialogBox.Success();
        }

        private void frmConfigAPI_Load(object sender, EventArgs e)
        {
            gcToaNha.DataSource = db.tnToaNhas;
        }
    }
}