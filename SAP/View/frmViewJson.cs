using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SAP
{
    public partial class frmViewJson : DevExpress.XtraEditors.XtraForm
    {
        public string text { get; set; }

        public class JsonToTableView
        {
            public byte type { get; set; }

            public string key { get; set; }

            public string value { get; set; }

        }

        public frmViewJson()
        {
            InitializeComponent();
        }

        private void frmView_Load(object sender, EventArgs e)
        {
            try
            {
                //var result = Library.Class.Connect.QueryConnect.QueryData<JsonToTableView>("JsonToTableView", 
                //    new {
                //        JsonText = text
                //    });
                //if (result.Count() > 0)
                //{
                //    gc.DataSource = result;
                //}
                richEditControl1.Text = text;
            }
            catch { }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var view = new frmView();
                view.text = text;
                view.ShowDialog();
            }
            catch { }
        }
    }
}