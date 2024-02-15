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
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables
{
    public partial class frmManagerCongNoToaNha : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmManagerCongNoToaNha()
        {
            InitializeComponent();
        }

        void SetDate(int index)
        {
            var objKBC = new KyBaoCao()
            {
                Index = index
            };
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }


        void RefreshData()
        {
            LoadData();
        }


        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            // Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBCC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void LoadData()
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            List<ToaNhaItem> listTN1 = Common.TowerList;
            List<byte> listTN = new List<byte>();
            foreach (var item in listTN1)
            {
                listTN.Add(item.MaTN);
            }


            var objPT = (from pt in db.ptPhieuThus
                         join tn in db.tnToaNhas on pt.MaTN equals tn.MaTN
                         where SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 && SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0 && pt.MaTKNH == null && listTN.Contains(tn.MaTN) == true && pt.IsKhauTru==false
                         group new { pt, tn } by new { pt.MaTN, tn.TenVT, tn.TenTN } into g
                         select new
                         {
                             g.Key.MaTN,
                             g.Key.TenVT,
                             g.Key.TenTN,
                             TongThu = g.Sum(p => p.pt.ptChiTietPhieuThus.Sum(k => k.SoTien)),
                             TongChi = (decimal?)0,
                             CongNo = (decimal?)0,
                         }).ToList();
            var objPC = (from pc in db.pcPhieuChis
                         join tn in db.tnToaNhas on pc.MaTN equals tn.MaTN
                         where SqlMethods.DateDiffDay(tuNgay, pc.NgayChi) >= 0 && SqlMethods.DateDiffDay(pc.NgayChi, denNgay) >= 0 && listTN.Contains(tn.MaTN)
                         group new { pc, tn } by new { pc.MaTN, tn.TenVT, tn.TenTN } into g
                         select new
                         {
                             g.Key.MaTN,
                             g.Key.TenVT,
                             g.Key.TenTN,
                             TongThu = (decimal?)0,
                             TongChi = g.Sum(k => k.pc.pcChiTiets.Sum(ct => ct.SoTien)),
                             CongNo = (decimal?)0,
                         }).ToList();
            var objALL = (from pt in objPT
                          join pc in objPC on pt.MaTN equals pc.MaTN into chi
                          from pc in chi.DefaultIfEmpty()
                          group new { pt, pc } by new { pt.MaTN, pt.TenVT, pt.TenTN } into gr
                          select new
                          {
                              gr.Key.MaTN,
                              gr.Key.TenVT,
                              gr.Key.TenTN,
                              TongThu = gr.Sum(p => p.pt.TongThu),
                              TongChi = gr.Sum(p =>p.pc==null?0: p.pc.TongChi),
                              CongNo = gr.Sum(p => p.pt.TongThu) - gr.Sum(p => p.pc == null ? 0 : p.pc.TongChi)
                          });
            gcCongNo.DataSource = objALL.ToList();

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcCongNo);
        }

        private void gvCongNo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvCongNo.GetFocusedRowCellValue("MaTN") != null)
            {
                var tuNgay = (DateTime)itemTuNgay.EditValue;
                var denNgay = (DateTime)itemDenNgay.EditValue;
                var objPT = (from pt in db.ptPhieuThus
                             join tn in db.tnToaNhas on pt.MaTN equals tn.MaTN
                             where SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 && SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0 && pt.MaTKNH == null && tn.MaTN == Convert.ToByte(gvCongNo.GetFocusedRowCellValue("MaTN")) && pt.IsKhauTru==false
                             group new { pt, tn } by new { pt.NgayThu.Value.Month, pt.NgayThu.Value.Year } into g
                             select new
                             {
                                 Thang = g.Key.Month,
                                 Nam = g.Key.Year,
                                 TongThu = g.Sum(p => p.pt.ptChiTietPhieuThus.Sum(k => k.SoTien)),
                                 TongChi = (decimal?)0,
                                 CongNo = (decimal?)0,
                             }).ToList();
                var objPC = (from pc in db.pcPhieuChis
                             join tn in db.tnToaNhas on pc.MaTN equals tn.MaTN
                             where SqlMethods.DateDiffDay(tuNgay, pc.NgayChi) >= 0 && SqlMethods.DateDiffDay(pc.NgayChi, denNgay) >= 0 && tn.MaTN == Convert.ToByte(gvCongNo.GetFocusedRowCellValue("MaTN"))
                             group new { pc, tn } by new { pc.NgayChi.Value.Month, pc.NgayChi.Value.Year } into g
                             select new
                             {
                                 Thang=g.Key.Month,
                                 Nam=g.Key.Year,
                                 TongThu = (decimal?)0,
                                 TongChi = g.Sum(k => k.pc.pcChiTiets.Sum(ct => ct.SoTien)),
                                 CongNo = (decimal?)0,
                             }).ToList();
                var objALL = (from pt in objPT
                              join pc in objPC on new { pt.Nam, pt.Thang } equals new {pc.Nam,pc.Thang } into chi
                              from pc in chi.DefaultIfEmpty()
                              orderby pt.Nam descending
                              group new { pt, pc } by new { pt.Nam, pt.Thang } into gr
                              select new
                              {
                                  gr.Key.Nam,
                                  gr.Key.Thang,
                                  TongThu = gr.Sum(p => p.pt.TongThu),
                                  TongChi = gr.Sum(p =>p.pc==null?0: p.pc.TongChi),
                                  ConNo = gr.Sum(p => p.pt.TongThu) - gr.Sum(p => p.pc == null ? 0 : p.pc.TongChi)
                              }).ToList();
                gcLichSu.DataSource = objALL.OrderByDescending(p=>p.Thang).ToList();
            }
            else
            {
                gcLichSu.DataSource = null;
            }
        }

    }
}