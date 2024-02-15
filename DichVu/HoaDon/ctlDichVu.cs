using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace DichVu.HoaDon
{
    public partial class ctlDichVu : UserControl
    {
        public int? MaMB { get; set; }
        public byte? MaLDV { get; set; }
        private MasterDataContext db;
        public ctlDichVu()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            if (this.MaMB == null)
            {
                gcDichVu.DataSource = null;
            }
            else
            {
                try
                {
                    db = new MasterDataContext();
                    gcDichVu.DataSource = db.cnCongNo_getByMaMBAndMaLDV(this.MaMB, this.MaLDV);
                }
                catch { gcDichVu.DataSource = null; }
            }
        }
    }
}
