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
using Remotion.FunctionalProgramming;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class frmCongNo_old : DevExpress.XtraEditors.XtraForm
    {
        public frmCongNo_old()
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

        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s));
            return ltToaNha;
        }
        public class ThongKeCongNo
        {
            public string KyBC { set; get; }
            public byte? MaTN { set; get; }
            public int? MaKH { set; get; }
            public int? MaMB { set; get; }
            public int? MaLDV { set; get; }
            public decimal? SoTien { set; get; }
        }
        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                db.CommandTimeout = 100000;
                var ltToaNha = this.GetToaNha();
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                #region "Code cũ"
                //Số dư đầu kỳ
                var ltData = (from hd in db.dvHoaDons
                              where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0 & hd.IsDuyet == true
                              // && hd.MaKH == 12895
                              group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV, hd.ID } into gr
                              select new
                              {
                                  KyBC = "1. Đầu kỳ",
                                  gr.Key.MaTN,
                                  gr.Key.MaKH,
                                  gr.Key.MaMB,
                                  gr.Key.MaLDV,
                                  SoTien = gr.Sum(p => p.PhaiThu - (db.SoQuy_ThuChis.Where(sq => sq.LinkID == p.ID && sq.MaKH == p.MaKH && sq.TableName == "dvHoaDon" && sq.IsPhieuThu == true && SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0 && ltToaNha.Contains(sq.MaTN)).Select(sq => new { SoTien = sq.DaThu + sq.KhauTru - sq.ThuThua }).Sum(s => s.SoTien).GetValueOrDefault())).GetValueOrDefault()
                              }).Select(p => new
                              {
                                  p.KyBC,
                                  p.MaTN,
                                  p.MaKH,
                                  p.MaMB,
                                  p.MaLDV,
                                  SoTien = p.SoTien
                              }).ToList();
                //Phát sinh trong kỳ
                ltData = ltData.Concat(from hd in db.dvHoaDons
                                       where ltToaNha.Contains(hd.MaTN) & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0
                                       & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                       & hd.IsDuyet == true
                                       // & hd.MaKH == 12895
                                       group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV } into gr
                                       select new 
                                       {
                                           KyBC = "2. Phát sinh",
                                           MaTN= gr.Key.MaTN,
                                           MaKH= gr.Key.MaKH,
                                           MaMB=gr.Key.MaMB,
                                           MaLDV=gr.Key.MaLDV,
                                           SoTien = gr.Sum(p => p.PhaiThu).GetValueOrDefault()
                                       }).ToList();

                ////Đã thu trong kỳ
                ltData = ltData.Concat(from ct in db.SoQuy_ThuChis
                                       join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                       where ltToaNha.Contains(ct.MaTN) && ct.MaLoaiPhieu != 24
                                       & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                                       & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                                       & hd.IsDuyet == true
                                       // & hd.MaKH == 12895
                                       group ct by new { ct.MaTN, ct.MaKH, hd.MaMB, hd.MaLDV } into gr
                                       select new 
                                       {
                                           KyBC = "3. Đã thu",
                                           MaTN = gr.Key.MaTN,
                                           MaKH = gr.Key.MaKH,
                                           MaMB = gr.Key.MaMB,
                                           MaLDV = gr.Key.MaLDV,
                                           SoTien = -gr.Sum(p => p.DaThu.GetValueOrDefault())
                                       }).ToList();
                //////Đã thu trong kỳ
                ////ltData = ltData.Concat(from ct in db.ptChiTietPhieuThus
                ////                       join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                ////                       where ltToaNha.Contains(pt.MaTN)
                ////                       & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0
                ////                       & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                ////                       & ct.LinkID == null && pt.MaPL != 24
                ////                     //  & pt.MaKH == 12895
                ////                       group ct by new { pt.MaTN, pt.MaKH, pt.MaMB } into gr
                ////                       select new
                ////                       {
                ////                           KyBC = "3. Đã thu",
                ////                           gr.Key.MaTN,
                ////                           gr.Key.MaKH,
                ////                           gr.Key.MaMB,
                ////                           MaLDV = (int?)0,
                ////                           SoTien = -gr.Sum(p => p.SoTien.GetValueOrDefault())
                ////                       });

                ////Đã khau tru trong kỳ
                ltData = ltData.Concat(from ct in db.SoQuy_ThuChis
                                       join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                       where ltToaNha.Contains(ct.MaTN)
                                       & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0 & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0 & hd.IsDuyet == true
                                       //  & hd.MaKH == 12895

                                       group ct by new { ct.MaTN, ct.MaKH, hd.MaMB, hd.MaLDV } into gr
                                       select new 
                                       {
                                           KyBC = "4. Đã khấu trừ",
                                           MaTN = gr.Key.MaTN,
                                           MaKH = gr.Key.MaKH,
                                           MaMB = gr.Key.MaMB,
                                           MaLDV = gr.Key.MaLDV,
                                           SoTien = -gr.Sum(p => p.KhauTru).GetValueOrDefault()
                                       }).ToList();
                ////Thu Trước trong kỳ
                //ltData = ltData.Concat(from ct in db.SoQuy_ThuChis
                //                       where ltToaNha.Contains(ct.MaTN)
                //                       & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0 
                //                       & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                //                       //  & hd.MaKH == 12895

                //                       group ct by new { ct.MaTN, ct.MaKH,ct.MaMB } into gr
                //                       select new
                //                       {
                //                           KyBC = "5. Thu Trước",
                //                           gr.Key.MaTN,
                //                           gr.Key.MaKH,
                //                           gr.Key.MaMB,
                //                           MaLDV=(int?)0,
                //                           SoTien = -gr.Sum(p => p.ThuThua-p.KhauTru).GetValueOrDefault()
                //                       });
                ltData = ltData.Concat(from ct in ltData
                                       where ltToaNha.Contains(ct.MaTN)
                                       group ct by new { ct.MaTN, ct.MaKH, ct.MaMB, ct.MaLDV } into gr
                                     //  group ct by new { ct.MaTN, ct.MaKH } into gr
                                       select new 
                                       {
                                           KyBC = "5. Tổng cộng",
                                           MaTN = gr.Key.MaTN,
                                           MaKH = gr.Key.MaKH,
                                           MaMB = gr.Key.MaMB,
                                           MaLDV = gr.Key.MaLDV,
                                           SoTien = gr.Sum(p => p.SoTien)
                                       }).ToList(); 
                //Nap vào pivot
                var objList=(from hd in ltData
                                       join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                       join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID into ldvu
                                       from l in ldvu.DefaultIfEmpty()
                                       join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB into tblMatBang
                                       from mb in tblMatBang.DefaultIfEmpty()
                                       join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL into tblTangLau
                                       from tl in tblTangLau.DefaultIfEmpty()
                                       join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN into tblKhoiNha
                                       from kn in tblKhoiNha.DefaultIfEmpty()
                                       join tn in db.tnToaNhas on hd.MaTN equals tn.MaTN
                                       join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB into tblLoaiMatBang
                                       from lmb in tblLoaiMatBang.DefaultIfEmpty()
                                       select new
                                       {
                                           hd.KyBC,
                                           lmb.TenLMB,
                                           tn.TenTN,
                                           kn.TenKN,
                                           tl.TenTL,
                                           mb.MaSoMB,
                                           kh.KyHieu,
                                           kh.MaPhu,
                                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                           TenLDV =hd.MaLDV==0?"Phí khác":l.TenHienThi,
                                           SoTien=hd.SoTien
                                       }).ToList();
                pvHoaDon.DataSource = objList;
                #endregion
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
            var rpt = new rptCongNo(Common.User.MaTN.Value);
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
            rpt.ShowPreviewDialog();
        }

        private void frmCongNo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            var index = DateTime.Now.Month + 8;
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