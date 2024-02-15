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

namespace LandSoftBuilding.Receivables.DuTinh
{
    public partial class frmHopDongThue : DevExpress.XtraEditors.XtraForm
    {
        public frmHopDongThue()
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

        //void LoadData()
        //{
        //    gcHoaDon.DataSource = null;
        //    gcHoaDon.DataSource = linqInstantFeedbackSource1;
        //}

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }
    
        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
  
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[7];
            var date = DateTime.Now;
            itemNam.EditValue = date.Year;
            SetDate(7);
            List<TongThang> TongTienThang = new List<TongThang>();
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
            var _Nam = Convert.ToUInt32(itemNam.EditValue);
            var db = new MasterDataContext();
            //TIỀN KHÔNG GIA HẠN
            List<TongThang> TongTienThang = new List<TongThang>();
            var ListDS_KGiaHan = (from hd in db.ctHopDongs
                                  join ltt in db.ctLichThanhToans on hd.ID equals ltt.MaHD
                                  join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                                  where ltt.TuNgay.Value.Year == _Nam
                                  & ltt.MaLDV == 2
                                  & hd.NgungSuDung == false
                                  & !hd.ctThanhLies.Any()
                                  group ltt by new { tn.MaTN, tn.TenTN } into gr
                                  select new TongThang
                                  {
                                      TenTN = gr.Key.TenTN,
                                      SoTien_kgh = gr.Sum(o => o.SoTienQD.GetValueOrDefault())
                                  }).ToList();

            TongTienThang.AddRange(ListDS_KGiaHan);

            //Lấy tất cả các lịch thanh toán cuối của hợp đồng
            var ListDS_LTTcuoi = (from hd in db.ctHopDongs
                                  join ltt in db.ctLichThanhToans on hd.ID equals ltt.MaHD
                                  join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                                  where hd.NgayHH.Value.Year == _Nam
                                  //& hd.MaTN == 3
                                  & ltt.MaLDV == 2
                                  & hd.NgungSuDung == false
                                  & !hd.ctThanhLies.Any()
                                  & SqlMethods.DateDiffMonth(ltt.DenNgay, hd.NgayHH) == 0
                                  select new
                                  {
                                      hd.SoHDCT,
                                      TenTN = tn.TenTN,
                                      hd.NgayHH,
                                      GetDay_Year = GetLastDayOfMonth(hd.NgayHH.Value.Month, hd.NgayHH.Value.Year),
                                      ltt.TyGia,
                                      ltt.GiaThue,
                                      KyTT = hd.KyTT,
                                  }).ToList();

            foreach (var item in ListDS_LTTcuoi)
            {
                var DuToan = TaoLTT_DuTinh(item.TenTN, item.NgayHH, item.GetDay_Year, item.TyGia, item.GiaThue, item.KyTT);
                TongTienThang.AddRange(DuToan);
            }
            

            //Lấy tất cả các hợp đồng thanh lí
            var ListDS_HDTLI = (from hd in db.ctHopDongs
                                join ct in db.ctChiTiets on hd.ID equals ct.MaHDCT
                                join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                                where hd.ctThanhLies.Any(o => o.NgayTL.Value.Year == _Nam)
                                //& hd.MaTN == 7
                                select new TongThang
                                {
                                    TenTN = tn.TenTN,
                                    SoTien_kgh = ct.ThanhTien.GetValueOrDefault(),
                                }).ToList();

            TongTienThang.AddRange(ListDS_HDTLI);

            var lt = (from tt in TongTienThang
                        group tt by new { tt.TenTN } into gr
                        select new
                        {
                            TenTN = gr.Key.TenTN,
                            KhongGiaHan = gr.Sum(o => o.SoTien_kgh).GetValueOrDefault(),
                            CoGiaHan = gr.Sum(o => o.SoTien_gh).GetValueOrDefault() + gr.Sum(o => o.SoTien_kgh).GetValueOrDefault()
                        }).ToList();
            gcDuTinh.DataSource = lt;
        }
 
        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcDuTinh);
        }
        public class TongThang
        {
            public byte MaTN { get; set; }
            public int Thang { get; set; }
            public string TenTN { get; set; }
            public decimal? KyTT { get; set; }
            public decimal? SoTien_kgh { get; set; }
            public decimal? SoTien_gh { get; set; }
        }
        public List<TongThang> TaoLTT_DuTinh(string TenTN, DateTime? NgayHH, DateTime? NgayCuoiNam, decimal? TyGia, decimal? DonGia, decimal? KyTT)
        {
            DateTime _TuNgay = (DateTime)NgayHH.Value.AddDays(1);
            DateTime date_Stop = (DateTime)NgayCuoiNam;
            List<TongThang> TongTienThang = new List<TongThang>();
            while (_TuNgay.CompareTo(date_Stop) < 0)
            {

                if (TyGia == null)
                    TyGia = 1;
                decimal _KyTT = (decimal)KyTT;

                var _DenNgay = _TuNgay.AddMonths(Convert.ToInt32(_KyTT)).AddDays(-1);

                if (_DenNgay.CompareTo(NgayCuoiNam) > 0)
                {
                    _DenNgay = date_Stop;
                    _KyTT = Common.GetTotalMonth(_TuNgay, _DenNgay);
                }
                TongTienThang.Add(new TongThang { TenTN = TenTN, SoTien_gh = DonGia * TyGia * _KyTT });

                _TuNgay = _DenNgay.AddDays(1);
            }
            return TongTienThang.ToList();
        }

        public static DateTime GetLastDayOfMonth(int iMonth, int iYear)
        {
            DateTime dtResult = new DateTime(iYear, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
    }
}