﻿using System;
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

namespace LandSoftBuilding.Lease.ReportsTT
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
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
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var ltToaNha = this.GetToaNha();
            
            var db = new MasterDataContext();
            try
            {
                pvChoThue.DataSource = (
                    /*from ltt in db.ctLichThanhToans
                                       join dv in db.dvHoaDons on new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } equals new { dv.TableName, dv.LinkID } into tblLTT
                                       from dv in tblLTT.DefaultIfEmpty()*/
                                       from ct in db.ctChiTiets /*on new { ltt.MaHD, ltt.MaMB } equals new { MaHD = ct.MaHDCT, ct.MaMB }*/
                                        join hd in db.ctHopDongs on ct.MaHDCT equals hd.ID
                                       join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                        join mb in db.mbMatBangs on ct.MaMB equals mb.MaMB
                                       join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                       join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                       join tn in db.tnToaNhas on kn.MaTN equals tn.MaTN
                                       join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                       where (ltToaNha.Contains(tn.MaTN) | ltToaNha.Count == 0)
                                            & SqlMethods.DateDiffDay(tuNgay, hd.NgayHL) >= 0
                                            & SqlMethods.DateDiffDay(hd.NgayHL, denNgay) >= 0
                                            //group ct by new {ct.DienTich} into gr 
                                       select new
                                       {
                                           Thang = hd.NgayHL.Value.Month,
                                           Nam = hd.NgayHL.Value.Year,
                                           tn.TenTN,
                                           kn.TenKN,
                                           tl.TenTL,
                                           mb.MaSoMB,
                                           ct.DienTich,
                                           GiaThue = ct.TongGiaThue,
                                           hd.TyGia,
                                           //hd.GiaThue,
                                           hd.SoHDCT,
                                           hd.NgayKy,
                                           hd.NgayHH,
                                           TGsudung = hd.ThoiHan,
                                           TGthue = hd.ThoiHan,
                                           hd.ThoiHan,
                                           GiaChuaThue = hd.ThoiHan * ct.TongGiaThue * hd.TyGia,
                                           GiaThueVAT = hd.ThoiHan * ct.TienVAT * hd.TyGia,
                                           TongGiaTriHD = ((hd.ThoiHan * ct.TongGiaThue) + (hd.ThoiHan * ct.TienVAT)) * hd.TyGia,
                                           ThangHH = hd.NgayHH.Value.Month,
                                           NamHH = hd.NgayHH.Value.Year,
                                           TGConLai  = (hd.NgayHH - hd.NgayHL).Value.Days,
                                           TinhTrang = ((hd.NgayHH - hd.NgayHL).Value.Days <=0) ? "Hết hạn" : "Chưa hết hạn",
                                           TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                           DienThoai = kh.DienThoaiKH,
                                           CMNDMaSoThue = kh.IsCaNhan == true ? kh.CMND : kh.CtyMaSoThue,
                                           NgayCap = kh.IsCaNhan == true ? kh.NgayCap : kh.CtyNgayDKKD,
                                           NoiCap = kh.IsCaNhan == true ? kh.NoiCap : kh.CtyNoiDKKD,
                                           DiaChi = kh.DCLL,
                                           GhiChu = ct.DienGiai, 
                                           lmb.TenLMB,
                                           DaThu = (from ltt in db.ctLichThanhToans
                                                    join dv in db.dvHoaDons on new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } equals
                                                        new { dv.TableName, dv.LinkID } into tblLTT
                                                    from dv in tblLTT.DefaultIfEmpty()
                                                    join ctt in db.ptChiTietPhieuThus on new { TableName = "dvHoaDon", LinkID = (long?)dv.ID } equals
                                                        new { ctt.TableName, ctt.LinkID } into cttPhieuThu
                                                    from ctt in cttPhieuThu.DefaultIfEmpty()
                                                    where
                                                        dv.MaMB == ct.MaMB
                                                    /*& SqlMethods.DateDiffDay(tuNgay, ltt.TuNgay) >= 0
                                                                                & SqlMethods.DateDiffDay(ltt.TuNgay, denNgay) >= 0*/
                                                    select new { ctt.SoTien }).Sum(p => p.SoTien).GetValueOrDefault(),
                                           ConNo = (((hd.ThoiHan * ct.TongGiaThue) + (hd.ThoiHan * ct.TienVAT)) * hd.TyGia) - ((from ltt in db.ctLichThanhToans
                                                                                                                                join dv in db.dvHoaDons on new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID } equals
                                                                                                                                    new { dv.TableName, dv.LinkID } into tblLTT
                                                                                                                                from dv in tblLTT.DefaultIfEmpty()
                                                                                                                                join ctt in db.ptChiTietPhieuThus on new { TableName = "dvHoaDon", LinkID = (long?)dv.ID } equals
                                                                                                                                    new { ctt.TableName, ctt.LinkID } into cttPhieuThu
                                                                                                                                from ctt in cttPhieuThu.DefaultIfEmpty()
                                                                                                                                where
                                                                                                                                    dv.MaMB == ct.MaMB
                                                                                                                                /*& SqlMethods.DateDiffDay(tuNgay, ltt.TuNgay) >= 0
                                                                                                                                                            & SqlMethods.DateDiffDay(ltt.TuNgay, denNgay) >= 0*/
                                                                                                                                select new { ctt.SoTien }).Sum(p => p.SoTien).GetValueOrDefault()),

                                           /*PhaiThu = dv.PhaiThu ?? ltt.SoTienQD,
                                           DaThu = dv.DaThu ?? 0,
                                           ConNo = dv.ConNo ?? ltt.SoTienQD,*/
                                           //PhaiThu = dv.PhaiThu ?? ltt.SoTienQD,
                                           kh.KyHieu
                                       }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private decimal getDaThu(int? iMaMB)
        {
            var db = new MasterDataContext();
            return (from ltt in db.ctLichThanhToans
                join dv in db.dvHoaDons on new {TableName = "ctLichThanhToan", LinkID = (int?) ltt.ID} equals
                    new {dv.TableName, dv.LinkID} into tblLTT
                from dv in tblLTT.DefaultIfEmpty()
                join ctt in db.ptChiTietPhieuThus on new {TableName = "dvHoaDon", LinkID = (long?) dv.ID} equals
                    new {ctt.TableName, ctt.LinkID} into cttPhieuThu
                from ctt in cttPhieuThu.DefaultIfEmpty()
                where
                    dv.MaMB == iMaMB
                /*& SqlMethods.DateDiffDay(tuNgay, ltt.TuNgay) >= 0
                                            & SqlMethods.DateDiffDay(ltt.TuNgay, denNgay) >= 0*/
                select new {ctt.SoTien}).Sum(p => p.SoTien).GetValueOrDefault();
        }

        void Print()
        {
            var rpt = new rptManager(Common.User.MaTN.Value);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            pvChoThue.OptionsView.ShowColumnHeaders = false;
            pvChoThue.OptionsView.ShowDataHeaders = false;
            pvChoThue.OptionsView.ShowFilterHeaders = false;
            pvChoThue.SavePivotGridToStream(stream);
            pvChoThue.OptionsView.ShowColumnHeaders = true;
            pvChoThue.OptionsView.ShowDataHeaders = true;
            pvChoThue.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.ShowPreviewDialog();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            itemKyBaoCao.EditValue = objKBC.Source[7];
            this.SetDate(7);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            this.SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(pvChoThue);
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }
    }
}