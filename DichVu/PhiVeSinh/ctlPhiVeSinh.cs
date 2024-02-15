using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace DichVu.PhiVeSinh
{
    public partial class ctlPhiVeSinh : UserControl
    {
        public int? MaMB { get; set; }
        private MasterDataContext db;
        public ctlPhiVeSinh()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            if (this.MaMB == null)
            {
                gcPhiVeSinh.DataSource = null;
            }
            else
            {
                db = new MasterDataContext();
                gcPhiVeSinh.DataSource = db.PhiVeSinh_selectByMaMB(this.MaMB);
            }
        }
    }
}
