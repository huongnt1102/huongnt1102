using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
//using ReportMisc.DichVu;

namespace DichVu
{
    public partial class frmBaoCaoCacKhoanDaThuChungCu : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCaoCacKhoanDaThuChungCu()
        {
            InitializeComponent();
        }
        Boolean first = true;
        private void frmBaoCaoCacKhoanDaThuChungCu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Library.Common.User.MaTN;

            itemThang1.EditValue = 1;
            itemThang2.EditValue = DateTime.Now.Month;
            itemNam1.EditValue = DateTime.Now.Year;
            itemNam2.EditValue = DateTime.Now.Year;
            
            this.LoadData();
            first = false;
        }

        private void itemTuNgay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!first) this.LoadData();
        }

        #region "   LoadData"
        void LoadData()
        {
            var db = new MasterDataContext();
            var _MaTN = (byte)itemToaNha.EditValue;
            var _MaKN = (int)itemKhuNha.EditValue;
            pvPhieuThu.DataSource = from t in db.ptPhieuThus
                                    join c in db.ptChiTietPhieuThus on t.ID equals c.MaPT
                                    join h in db.dvHoaDons on c.LinkID equals h.ID
                                    join d in db.dvLoaiDichVus on h.MaLDV equals d.ID
                                    join m in db.mbMatBangs on h.MaMB equals m.MaMB
                                    join L in db.mbTangLaus on m.MaTL equals L.MaTL
                                    join k in db.mbKhoiNhas on L.MaKN equals k.MaKN
                                    where System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(new DateTime((int)itemNam1.EditValue, (int)itemThang1.EditValue, 1), t.NgayThu) >= 0
                                        & System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(t.NgayThu, new DateTime((int)itemNam2.EditValue, (int)itemThang2.EditValue, 1)) >= 0
                                        & h.MaTN == _MaTN
                                        & L.MaKN == _MaKN

                                    select new
                                    {
                                        nam = t.NgayThu.Value.Year,
                                        thang = t.NgayThu.Value.Month,
                                        c.SoTien,
                                        d.TenLDV,
                                        k.TenKN
                                    };
            //if (((DateTime)itemTuNgay.EditValue).Year < ((DateTime)itemDenNgay.EditValue).Year)
            //{
                pvPhieuThu.OptionsView.ShowColumnGrandTotals = true;
                pvPhieuThu.OptionsView.ShowColumnTotals = false;
            //}
            //else
            //{
            //    pvPhieuThu.OptionsView.ShowColumnGrandTotals = false;
            //    pvPhieuThu.OptionsView.ShowColumnTotals = true;
            //}
        }
        #endregion


        #region "   Export Excel"
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pvPhieuThu.OptionsView.ShowDataHeaders = false;
            pvPhieuThu.OptionsView.ShowFilterHeaders = false;
            pvPhieuThu.OptionsView.ShowColumnHeaders = false;

            Commoncls.ExportExcel(pvPhieuThu);
            pvPhieuThu.OptionsView.ShowDataHeaders = true;
            pvPhieuThu.OptionsView.ShowFilterHeaders = true;
            pvPhieuThu.OptionsView.ShowColumnHeaders = true;
        }
        #endregion

        #region "   itemToaNha_ItemClick"
        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (itemToaNha.EditValue != null)
            {
                var db = new MasterDataContext();
                var list = (from kn in db.mbKhoiNhas where kn.MaTN == (byte)itemToaNha.EditValue select new { kn.MaKN, kn.TenKN }).ToList();
                lkKhoiNha.DataSource = list;
                itemKhuNha.EditValue = list[0].MaKN;
                db.Dispose();
            }
        }
        #endregion

        #region "  itemKhuNha_EditValueChanged "
        private void itemKhuNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) this.LoadData();

        }
        #endregion

        #region "   btnNap_ItemClick"
        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.linqInstantFeedbackSource1.Refresh();
            this.LoadData();
        }
        #endregion


    }
}