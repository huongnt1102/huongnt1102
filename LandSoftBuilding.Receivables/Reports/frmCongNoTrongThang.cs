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
    public partial class frmCongNoTrongThang : DevExpress.XtraEditors.XtraForm
    {
        public frmCongNoTrongThang()
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

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var ltToaNha = this.GetToaNha();
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                string HinhThuc = itemHinhThucThu.EditValue.ToString();
                //Số dư đầu kỳ
                var ltData = (from hd in db.dvHoaDons
                              where ltToaNha.Contains(hd.MaTN)
                              & SqlMethods.DateDiffDay(hd.NgayTT, _TuNgay) > 0
                              & hd.IsDuyet == true
                              group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV } into gr
                              select new
                              {
                                  KyBC = "1. Đầu kỳ",
                                  gr.Key.MaTN,
                                  gr.Key.MaKH,
                                  gr.Key.MaMB,
                                  gr.Key.MaLDV,
                                  SoTien = gr.Sum(p => p.PhaiThu - (from sq in db.SoQuy_ThuChis
                                                                    where ltToaNha.Contains(sq.MaTN)
                                                                    && SqlMethods.DateDiffDay(sq.NgayPhieu, _TuNgay) > 0
                                                                    && sq.IsPhieuThu == true && sq.MaLoaiPhieu != 24 && sq.LinkID != null && sq.TableName == "dvHoaDon" && sq.LinkID == p.ID
                                                                    select new { sq.DaThu, sq.KhauTru, sq.ThuThua }).Sum(x => x.DaThu + x.KhauTru - x.ThuThua).GetValueOrDefault()).Value
                              });

                //Phát sinh trong kỳ
                ltData = ltData.Concat(from hd in db.dvHoaDons
                                       where ltToaNha.Contains(hd.MaTN)
                                       & SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0
                                       & SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0
                                       & hd.IsDuyet == true
                                       group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV } into gr
                                       select new
                                       {
                                           KyBC = "2. Phát sinh",
                                           gr.Key.MaTN,
                                           gr.Key.MaKH,
                                           gr.Key.MaMB,
                                           gr.Key.MaLDV,
                                           SoTien = gr.Sum(p => p.PhaiThu).GetValueOrDefault()
                                       });
                ltData = ltData.Concat(from ct in db.SoQuy_ThuChis
                                       join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID } into hoaDon
                                       from hd in hoaDon.DefaultIfEmpty()
                                       where ltToaNha.Contains(ct.MaTN)
                                       & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                                       & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                                        & ct.IsPhieuThu == true & ct.MaLoaiPhieu != 24 & ct.IsDvApp == false & ct.IsKhauTru == false
                                       group ct by new { ct.MaTN, ct.MaKH, hd.MaMB, hd.MaLDV } into gr
                                       select new
                                       {
                                           KyBC = "3. Đã thu",
                                           gr.Key.MaTN,
                                           gr.Key.MaKH,
                                           gr.Key.MaMB,
                                           gr.Key.MaLDV,
                                           SoTien = -gr.Sum(p => p.DaThu.GetValueOrDefault() + p.KhauTru.GetValueOrDefault() - p.ThuThua.GetValueOrDefault())
                                       });
                ltData = ltData.Concat(from ct in db.SoQuy_ThuChis
                                       where ltToaNha.Contains(ct.MaTN)
                                       & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                                       & ct.IsPhieuThu == true & ct.MaLoaiPhieu != 24
                                       group ct by new { ct.MaTN, ct.MaKH, ct.MaMB } into gr
                                       select new
                                       {
                                           KyBC = "4. Thu trước",
                                           gr.Key.MaTN,
                                           gr.Key.MaKH,
                                           gr.Key.MaMB,
                                           MaLDV = (int?)-1,
                                           SoTien = -gr.Sum(p => p.ThuThua - p.KhauTru).GetValueOrDefault()
                                       });
                ltData = ltData.Concat(from ct in db.SoQuy_ThuChis
                                       join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                       where ltToaNha.Contains(ct.MaTN)
                                       & SqlMethods.DateDiffDay(_TuNgay, ct.NgayPhieu) >= 0
                                       & SqlMethods.DateDiffDay(ct.NgayPhieu, _DenNgay) >= 0
                                       & ct.IsPhieuThu == true & ct.IsKhauTru == true & ct.LinkID != null
                                       group ct by new { ct.MaTN, ct.MaKH, hd.MaMB, hd.MaLDV } into gr
                                       select new
                                       {
                                           KyBC = "5. Khấu trừ thu trước",
                                           gr.Key.MaTN,
                                           gr.Key.MaKH,
                                           gr.Key.MaMB,
                                           gr.Key.MaLDV,
                                           SoTien = -gr.Sum(p => p.KhauTru.GetValueOrDefault() + p.DaThu.GetValueOrDefault())
                                       });
                #region code cũ - 2023/12/08 - Quang

                ////Số dư đầu kỳ
                //var ltData = (from hd in db.dvHoaDons
                //             where ltToaNha.Contains(hd.MaTN)
                //             & SqlMethods.DateDiffDay(hd.NgayTT, (hd.MaLDV == 5 || hd.MaLDV == 8 || hd.MaLDV == 9 || hd.MaLDV == 10 || hd.MaLDV == 11 || hd.MaLDV == 20 || hd.MaLDV == 22 || hd.MaLDV == 23 || hd.MaLDV == 30) ? _TuNgay : _TuNgay) > 0 
                //             & hd.IsDuyet == true// hd.MaKH == 9855 &
                //             group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV} into gr
                //             select new
                //             {
                //                 KyBC = "1. Đầu kỳ",
                //                 gr.Key.MaTN,
                //                 gr.Key.MaKH,
                //                 gr.Key.MaMB,
                //                 gr.Key.MaLDV,
                //                 SoTien = gr.Sum(p => p.PhaiThu - (from ct in db.ptChiTietPhieuThus
                //                                                   join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                                                   where ct.TableName == "dvHoaDon" & ct.LinkID == p.ID
                //                                                   & SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0
                //                                                   & ( HinhThuc == "Tất cả" || (HinhThuc == "Tiền mặt" && pt.MaTKNH == null) || (HinhThuc == "Chuyển khoản" && pt.MaTKNH != null) ) 
                //                                                   select ct.SoTien).Sum().GetValueOrDefault() -(from ct in db.ktttChiTiets
                //                                                                                                   join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                //                                                                                                  where ct.TableName == "dvHoaDon" & ct.LinkID == p.ID
                //                                                                                                   & SqlMethods.DateDiffDay(pt.NgayCT, _TuNgay) > 0
                //                                                                                                   select ct.SoTien).Sum().GetValueOrDefault()).GetValueOrDefault()


                //             });

                ////Phát sinh trong kỳ
                //ltData = ltData.Concat(from hd in db.dvHoaDons
                //                       where ltToaNha.Contains(hd.MaTN)
                //                       & SqlMethods.DateDiffDay((hd.MaLDV == 5 || hd.MaLDV == 8 || hd.MaLDV == 9 || hd.MaLDV == 10 || hd.MaLDV == 11 || hd.MaLDV == 20 || hd.MaLDV == 22 || hd.MaLDV == 23 || hd.MaLDV == 30) ? _TuNgay.AddMonths(+1) : _TuNgay, hd.NgayTT) >= 0
                //                       & SqlMethods.DateDiffDay(hd.NgayTT, (hd.MaLDV == 5 || hd.MaLDV == 8 || hd.MaLDV == 9 || hd.MaLDV == 10 || hd.MaLDV == 11 || hd.MaLDV == 20 || hd.MaLDV == 22 || hd.MaLDV == 23 || hd.MaLDV == 30) ? _DenNgay.AddMonths(+1) : _DenNgay) >= 0
                //                       & hd.IsDuyet == true

                //                       group hd by new { hd.MaTN, hd.MaKH, hd.MaMB, hd.MaLDV } into gr
                //                       select new
                //                       {
                //                           KyBC = "2. Phát sinh",
                //                           gr.Key.MaTN,
                //                           gr.Key.MaKH,
                //                           gr.Key.MaMB,
                //                           gr.Key.MaLDV,
                //                           SoTien = gr.Sum(p => p.PhaiThu).GetValueOrDefault()
                //                       });

                //ltData = ltData.Concat(from ct in db.ptChiTietPhieuThus
                //                       join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                       join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                //                       where ltToaNha.Contains(pt.MaTN)
                //                       & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0
                //                       & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                //                        & (HinhThuc == "Tất cả" || (HinhThuc == "Tiền mặt" && pt.MaTKNH == null) || (HinhThuc == "Chuyển khoản" && pt.MaTKNH != null))
                //                       //& SqlMethods.DateDiffDay(_TuNgay, hd.NgayTT) >= 0
                //                       //& SqlMethods.DateDiffDay(hd.NgayTT, _DenNgay) >= 0 
                //                       //& hd.IsDuyet == true
                //                       group ct by new { pt.MaTN, pt.MaKH, hd.MaMB, hd.MaLDV } into gr
                //                       select new
                //                       {
                //                           KyBC = "3. Đã thu",
                //                           gr.Key.MaTN,
                //                           gr.Key.MaKH,
                //                           gr.Key.MaMB,
                //                           gr.Key.MaLDV,
                //                           SoTien = -gr.Sum(p => p.SoTien).GetValueOrDefault()
                //                       });
                // Thu trước trong kỳ
                //ltData = ltData.Concat(from ct in db.ptChiTietPhieuThus
                //                       join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                       join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                //                       where ltToaNha.Contains(pt.MaTN)
                //                       & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0
                //                       & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                //                       & SqlMethods.DateDiffDay(_DenNgay, hd.NgayTT) > 0
                //                       & ((HinhThuc == "Tất cả") || (HinhThuc == "Tiền mặt" && pt.MaTKNH == null) || (HinhThuc == "Chuyển khoản" && pt.MaTKNH != null))
                //                       group ct by new { pt.MaTN, pt.MaKH, hd.MaMB, hd.MaLDV } into gr
                //                       select new
                //                       {
                //                           KyBC = "4. Thu trước",
                //                           gr.Key.MaTN,
                //                           gr.Key.MaKH,
                //                           gr.Key.MaMB,
                //                           gr.Key.MaLDV,
                //                           SoTien = gr.Sum(p => p.SoTien).GetValueOrDefault()
                //                       });

                //ltData = ltData.Concat(from ct in db.ptChiTietPhieuThus
                //                       join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                //                       join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                //                       where ltToaNha.Contains(pt.MaTN)
                //                       & SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0
                //                       & SqlMethods.DateDiffDay((hd.MaLDV == 5 || hd.MaLDV == 8 || hd.MaLDV == 9 || hd.MaLDV == 10 || hd.MaLDV == 11 || hd.MaLDV == 20 || hd.MaLDV == 22 || hd.MaLDV == 23 || hd.MaLDV == 30) ? _TuNgay.AddMonths(+1) : _TuNgay, hd.NgayTT) >= 0
                //                       & SqlMethods.DateDiffDay(hd.NgayTT, (hd.MaLDV == 5 || hd.MaLDV == 8 || hd.MaLDV == 9 || hd.MaLDV == 10 || hd.MaLDV == 11 || hd.MaLDV == 20 || hd.MaLDV == 22 || hd.MaLDV == 23 || hd.MaLDV == 30) ? _DenNgay.AddMonths(+1) : _DenNgay) >= 0
                //                       & (HinhThuc == "Tất cả" || (HinhThuc == "Tiền mặt" && pt.MaTKNH == null) || (HinhThuc == "Chuyển khoản" && pt.MaTKNH != null))
                //                       & hd.IsDuyet == true
                //                       group ct by new { pt.MaTN, pt.MaKH, hd.MaMB, hd.MaLDV } into gr
                //                       select new
                //                       {
                //                           KyBC = "5. Khấu trừ thu trước",
                //                           gr.Key.MaTN,
                //                           gr.Key.MaKH,
                //                           gr.Key.MaMB,
                //                           gr.Key.MaLDV,
                //                           SoTien = -gr.Sum(p => p.SoTien).GetValueOrDefault()
                //                       });
                #endregion đóng code cũ 
                //Nap vào pivot
                pvHoaDon.DataSource = (from hd in ltData
                                       join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                       join l in db.dvLoaiDichVus on hd.MaLDV equals l.ID into tblLoaiDichVu
                                       from l in tblLoaiDichVu.DefaultIfEmpty()
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
                                           TenLDV = hd.MaLDV != -1 ? l.TenHienThi : "Thu trước",
                                           hd.SoTien
                                       }).ToList();
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
            itemHinhThucThu.EditValue = "Tất cả";
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
            else if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.IsColumn == true)
                e.DisplayText = "Số dư cuối kỳ";
            else if(e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.IsColumn == false)
                e.DisplayText = "Tổng cộng";
        }
    }
}