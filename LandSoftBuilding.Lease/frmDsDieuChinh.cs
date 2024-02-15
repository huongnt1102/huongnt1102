using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
namespace LandSoftBuilding.Lease
{
    public partial class frmDsDieuChinh : DevExpress.XtraEditors.XtraForm
    {
        public frmDsDieuChinh(int? ID)
        {
            InitializeComponent();
            var db= new MasterDataContext();

            var objCT = db.ctChiTiets.Single(p => p.ID == ID);
            gcChiTiet.DataSource = objCT.ctLichSuDieuChinhGias.OrderByDescending(p=>p.NgayDC);
        }

        private void frmDsDieuChinh_Load(object sender, EventArgs e)
        {

        }
    }
}