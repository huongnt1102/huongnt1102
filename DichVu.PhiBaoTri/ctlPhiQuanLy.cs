using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace DichVu.PhiBaoTri
{
    public partial class ctlPhiQuanLy : UserControl
    {
        public int? MaMB { get; set; }
        private MasterDataContext db;
        public ctlPhiQuanLy()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            if (this.MaMB == null)
            {
                gcPhiQuanLy.DataSource = null;
            }
            else
            {
                db = new MasterDataContext();
                gcPhiQuanLy.DataSource = db.PhiQuanLy_selectByMaMB(this.MaMB);
            }
        }
    }
}
