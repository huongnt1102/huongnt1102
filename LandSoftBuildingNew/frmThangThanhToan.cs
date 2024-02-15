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
using DevExpress.XtraEditors.Controls;

namespace LandSoftBuildingMain
{
    public partial class frmThangThanhToan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public bool objdien = false;
        public bool objnuoc = false;
        public int MaMB = -1;
        public int MaLo = -1;
        public int MaKH = -1;

        public frmThangThanhToan()
        {
            InitializeComponent();
            db = new MasterDataContext();

        }

        private void frmThangThanhToan_Load(object sender, EventArgs e)
        {
            if (objdien)
            {
                lookThangThanhToan.Properties.DisplayMember = "NgayNhap";
                lookThangThanhToan.Properties.ValueMember = "ID";
                lookThangThanhToan.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookThangThanhToan.Properties.SearchMode = SearchMode.AutoComplete;
                lookThangThanhToan.Properties.DropDownRows = 10;
                LookUpColumnInfoCollection col = lookThangThanhToan.Properties.Columns;
                col.Add(new LookUpColumnInfo("NgayNhap", "Tháng thanh toán", 400, DevExpress.Utils.FormatType.Custom, "MM/yyyy",true, DevExpress.Utils.HorzAlignment.Near));
                
                lookThangThanhToan.Properties.DataSource = db.dvdnDiens
                    .Where(p => !p.DaTT.Value & p.ConNo > 0)
                    .Where(p => p.MaMB == MaMB | p.MaKH == MaKH | p.MaLo == MaLo);
                return;
            }

            if (objnuoc)
            {
                lookThangThanhToan.Properties.DisplayMember = "NgayNhap";
                lookThangThanhToan.Properties.ValueMember = "ID";
                lookThangThanhToan.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
                lookThangThanhToan.Properties.SearchMode = SearchMode.AutoComplete;
                lookThangThanhToan.Properties.DropDownRows = 10;
                LookUpColumnInfoCollection col = lookThangThanhToan.Properties.Columns;
                col.Add(new LookUpColumnInfo("NgayNhap", "Tháng thanh toán", 400, DevExpress.Utils.FormatType.Custom, "MM/yyyy", true, DevExpress.Utils.HorzAlignment.Near));
                
                lookThangThanhToan.Properties.DataSource = db.dvdnNuocs
                    .Where(p => !p.DaTT.Value & p.ConNo > 0)
                    .Where(p => p.MaMB == MaMB | p.MaKH == MaKH | p.MaLo == MaLo);
                return;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
        }
    }
}