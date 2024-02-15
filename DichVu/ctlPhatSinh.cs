using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace DichVu
{
    public partial class ctlPhatSinh : UserControl
    {
        public int? MaMB { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public byte? MaLDV { get; set; }
        private MasterDataContext db;
        public ctlPhatSinh()
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
                    gcDichVu.DataSource = /*db.cnCongNo_getByMaMBAndMaLDV(this.MaMB, this.MaLDV); */db.cnLichSu_getPhatSinh(this.MaMB, this.Month, this.Year, this.MaLDV).ToList();
                }
                catch { gcDichVu.DataSource = null; }
            }
        }
    }
}
