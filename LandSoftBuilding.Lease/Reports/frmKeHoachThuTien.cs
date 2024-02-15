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
using DevExpress.XtraReports.UI;
using DevExpress.Data.PivotGrid;

namespace LandSoftBuilding.Lease.Reports
{
    public partial class frmKeHoachThuTien : DevExpress.XtraEditors.XtraForm
    {
        public frmKeHoachThuTien()
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

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;

                var ltData = from ltt in db.ctLichThanhToans
                             join ct in db.ctChiTiets on new { ltt.MaHD, ltt.MaMB } equals new { MaHD = ct.MaHDCT, ct.MaMB }
                             join hd in db.ctHopDongs on ct.MaHDCT equals hd.ID
                             join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                             join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                             join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                             join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into viewLoaiMatBang
                             from lmb in viewLoaiMatBang.DefaultIfEmpty()
                             join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                             join ldv in db.dvLoaiDichVus on ltt.MaLDV equals ldv.ID
                             where hd.MaTN == _MaTN & hd.NgungSuDung == false & SqlMethods.DateDiffDay(_TuNgay, ltt.TuNgay) >= 0 & SqlMethods.DateDiffDay(ltt.TuNgay, _DenNgay) >= 0
                             select new
                             {
                                 lmb.TenLMB,
                                 kn.TenKN,
                                 tl.TenTL,
                                 mb.MaSoMB,
                                 kh.MaPhu,
                                 kh.KyHieu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                 ldv.TenLDV,
                                 NgayTT = ltt.TuNgay,
                                 hd.HanTT,
                                 NgayHHTT = ltt.TuNgay.Value.AddDays(hd.HanTT.GetValueOrDefault()),
                                 hd.KyTT,
                                 ltt.DienGiai,
                                 ltt.SoTienQD,
                                 SoHD = hd.SoHDCT,
                                 hd.NgayKy,
                                 hd.NgayHL,
                                 hd.NgayHH,
                                 mb.DienTich
                             };

                pvHoaDon.DataSource = ltData;
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        void Print()
        {
            var _MaTN = (byte)itemToaNha.EditValue;
            var rpt = new rptKeHoachThuTien(_MaTN);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;
           
            pvHoaDon.OptionsView.ShowColumnHeaders = false;
            pvHoaDon.OptionsView.ShowDataHeaders = false;
            pvHoaDon.OptionsView.ShowFilterHeaders = false;
            pvHoaDon.SavePivotGridToStream(stream);
            pvHoaDon.OptionsView.ShowColumnHeaders = true;
            pvHoaDon.OptionsView.ShowDataHeaders = true;
            pvHoaDon.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.CreateDocument();
            rpt.PrintingSystem.Document.AutoFitToPagesWidth = 1;       
            rpt.ShowPreviewDialog();
        }

        private void frmCongNo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            var index = 7;
            itemKyBaoCao.EditValue = objKBC.Source[index];
            SetDate(index);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void pvHoaDon_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
                e.DisplayText = string.Format("{0} ({1})", e.Value, "Tổng");
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                e.DisplayText = "Tổng cộng";
        }
    }
}