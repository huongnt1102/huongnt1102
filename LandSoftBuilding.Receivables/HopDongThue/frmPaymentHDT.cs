using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Xml;
using Library;
using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Receivables
{
    public partial class frmPaymentHDT : DevExpress.XtraEditors.XtraForm
    {

        public frmPaymentHDT()
        {
            InitializeComponent();
        }

        public int? MaTL;

        public int? MaPT { get; set; }
        public int? MaKH { get; set; }
        public byte? MaTN { get; set; }
        private int ID = 0;
        private int IDDC = 0;
        public int MaThiCong = 0;
        private string soptgoc = "";
        private DateTime? ngaythugoc = null;
        public List<HopDongThue.PaymentCls.PaymentItemHDT> ltData;
        List<ThuTuDienGiai> listDienGiai;
        MasterDataContext db = new MasterDataContext();

        string GetDienGiai()
        {
            string strDienGiai = "";
            try
            {
                var ltLDV = (from l in ltData
                             where l.IsChon == true
                             orderby l.SoTTDV
                             select new { l.MaLDV, l.TenLDV, l.PhaiThu }).Distinct().ToList();
                foreach (var i in ltLDV)
                {
                    var ltDV = (from l in ltData
                                where l.IsChon == true & l.MaLDV == i.MaLDV
                                group l by new { l.NgayTT.Value.Month, l.NgayTT.Value.Year } into gr
                                orderby gr.Max(p => p.NgayTT)
                                select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";
                    decimal TienXe = 0;
                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {
                            //TienXe += i.PhaiThu.GetValueOrDefault();
                            if (_Start != j)
                            {
                                if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                    strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                else
                                    strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                            }
                            else
                            {
                                strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start]);
                            }

                            _Start = j + 1;
                        }

                        j++;
                    }

                    strTime = strTime.TrimEnd(',');
                    strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        string GetDienGiai(int MaHD)
        {
            string strDienGiai = "";
            try
            {
                var ltLDV = (from l in ltData
                             where l.IsChon == true
                             & l.MaHD == MaHD
                             orderby l.SoTTDV
                             select new { l.MaLDV, l.TenLDV, l.PhaiThu }).Distinct().ToList();

                foreach (var i in ltLDV)
                {
                    var ltDV = (from l in ltData
                                where l.IsChon == true & l.MaLDV == i.MaLDV
                                & l.MaHD == MaHD
                                group l by new { l.NgayTT.Value.Month, l.NgayTT.Value.Year } into gr
                                orderby gr.Max(p => p.NgayTT)
                                select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";
                    decimal TienXe = 0;
                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {
                            //TienXe += i.PhaiThu.GetValueOrDefault();
                            if (_Start != j)
                            {
                                if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                    strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                else
                                    strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                            }
                            else
                            {
                                strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start]);
                            }

                            _Start = j + 1;
                        }

                        j++;
                    }

                    strTime = strTime.TrimEnd(',');
                    strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        string GetDienGiaiVStar()
        {
            string strDienGiai = "";
            try
            {
                //var ltLDV = (from l in ltData
                //             where l.IsChon == true
                //             group l by new {l.MaLDV, l.TenLDV, l.PhaiThu} into gr
                //             select new { gr.Key.MaLDV, gr.Key.TenLDV, gr.Key.PhaiThu }).Distinct().ToList();
                var ltLDV = (from l in ltData
                             where l.IsChon == true
                             orderby l.SoTTDV
                             //group l by new { l.MaLDV, l.TenLDV, l.PhaiThu } into gr
                             select new { l.MaLDV, l.TenLDV }).Distinct().ToList();
                foreach (var i in ltLDV)
                {
                    var ltLDVSum = (from l in ltData
                                    where l.IsChon == true & l.MaLDV == i.MaLDV
                                    group l by new { l.NgayTT.Value.Year, l.MaLDV } into gr
                                    select new { gr.Key.MaLDV, gr.Key.Year, PhaiThu = gr.Sum(p => p.ThucThu) }).Distinct().ToList();
                    var ltDV = (from l in ltData
                                where l.IsChon == true & l.MaLDV == i.MaLDV
                                group l by new { l.NgayTT.Value.Month, l.NgayTT.Value.Year, PhaiThu = l.ThucThu } into gr
                                orderby gr.Max(p => p.NgayTT)
                                select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";

                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {

                            decimal TienXeC = 0;
                            decimal TienXeD = 0;
                            if (_Start != j)
                            {
                                if (i.MaLDV == 8 | i.MaLDV == 9 | i.MaLDV == 10 | i.MaLDV == 14)
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                    else
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1), ltDV[j].Value.AddMonths(-1));
                                }
                                else
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start]);
                                    else
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                }

                            }
                            else
                            {
                                if (i.MaLDV == 8 | i.MaLDV == 9 | i.MaLDV == 14)
                                    strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                else
                                {
                                    strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start]);
                                }
                            }

                            _Start = j + 1;
                        }

                        j++;
                    }

                    strTime = strTime.TrimEnd(',');

                    foreach (var tam in ltLDVSum)
                    {
                        strDienGiai += string.Format("{0} {1} ({2:#,0.##}) , ", i.TenLDV, strTime, Math.Round((decimal)tam.PhaiThu, 0));
                    }
                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        string GetDienGiaiVStarNew()
        {
            string strDienGiai = "";
            try
            {

                var ltLDV = (from l in ltData
                             where l.IsChon == true
                             orderby l.SoTTDV
                             select new { l.MaLDV, l.TenLDV, l.ThucThu, l.NgayTT }).Distinct().ToList();

                foreach (var i in ltLDV)
                {

                    var strTime = "";

                    if (i.MaLDV == 8 | i.MaLDV == 9 | i.MaLDV == 10 | i.MaLDV == 14)
                        strTime = string.Format("T{0:MM/yyyy},", i.NgayTT.Value.AddMonths(-1));
                    else
                    {
                        strTime = string.Format("T{0:MM/yyyy},", i.NgayTT.Value);
                    }
                    strTime = strTime.TrimEnd(',');
                    strDienGiai += string.Format("{0} {1} ({2:#,0.##}) , ", i.TenLDV, strTime, Math.Round((decimal)i.ThucThu, 0));



                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        void TinhSoTien()
        {
            if (MaTN == 27 | MaTN == 36 | MaTN == 39 | MaTN == 40 | MaTN == 34 | MaTN == 3)
            {
                if (MaTN == 39 | MaTN == 34 | MaTN == 40 | MaTN == 3)
                {
                    spSoTien.EditValue = ltData.Where(p => p.IsChon == true).Sum(p => p.ThucThu);
                    txtDienGiai.Text = MaTN == 39 ? this.GetDienGiaiVStar() : this.GetDienGiaiVStarNew();
                }
                else
                {
                    spSoTien.EditValue = ltData.Where(p => p.IsChon == true).Sum(p => p.ThucThu);

                    var strDienGiai = "";
                    foreach (var i in ltData.Where(p => p.IsChon == true))
                    {


                        strDienGiai += "; " + i.DienGiai;


                    }
                    strDienGiai = strDienGiai.Trim().Trim(';');
                    txtDienGiai.Text = strDienGiai;
                }
            }

            else
            {
                spSoTien.EditValue = ltData.Where(p => p.IsChon == true).Sum(p => p.ThucThu);
                txtDienGiai.Text = this.GetDienGiai();
            }





        }

        decimal GetTyleCK(int _MaLDV, decimal _KyTT)
        {
            var db = new MasterDataContext();
            return (from ck in db.dvChietKhaus
                    where ck.MaTN == this.MaTN & ck.MaLDV == _MaLDV & ck.KyTT >= _KyTT
                    orderby ck.KyTT
                    select ck.TyLeCK).FirstOrDefault().GetValueOrDefault();
        }

        void UpdateHoaDon(List<HopDongThue.PaymentCls.PaymentItemHDT> ltHoaDon)
        {
            var db = new MasterDataContext();
            try
            {
                foreach (var hd in ltHoaDon)
                {
                    if (hd.ID == 0)
                    {
                        var objHD = new dvHoaDon();
                        objHD.MaTN = this.MaTN;
                        objHD.MaKH = (int)glKhachHang.EditValue;
                        objHD.NgayTT = hd.NgayTT;
                        objHD.PhiDV = hd.PhiDV;
                        objHD.MaLDV = hd.MaLDV;
                        objHD.DienGiai = hd.DienGiai;
                        objHD.MaNVN = Common.User.MaNV;
                        objHD.IsDuyet = true;
                        objHD.KyTT = hd.KyTT;
                        objHD.TienTT = hd.TienTT;
                        objHD.TyLeCK = hd.TyLeCK;
                        objHD.TienCK = hd.TienCK;
                        objHD.PhaiThu = hd.PhaiThu.GetValueOrDefault();
                        objHD.ConNo = objHD.PhaiThu - objHD.DaThu.GetValueOrDefault();
                        db.dvHoaDons.InsertOnSubmit(objHD);

                        db.SubmitChanges();

                        hd.ID = objHD.ID;
                    }
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void UpdateLaiPhatSinh()
        {
            var _NgayTT = dateNgayThu.DateTime;
            var db = new MasterDataContext();
            try
            {
                var objHD = ltData.FirstOrDefault(p => p.ID == 0 & p.MaLDV == 23);
                var _TienLai = this.TinhTienLai();
                if (_TienLai > 0)
                {
                    if (objHD == null)
                    {
                        objHD = new HopDongThue.PaymentCls.PaymentItemHDT();
                        objHD.ID = 0;
                        objHD.MaLDV = 23;
                        objHD.TenLDV = (from ldv in db.dvLoaiDichVus where ldv.ID == objHD.MaLDV select ldv.TenHienThi).FirstOrDefault();
                        objHD.SoTTDV = (from ldv in db.dvLoaiDichVus where ldv.ID == objHD.MaLDV select ldv.STT).FirstOrDefault();
                        objHD.IsChon = true;
                        ltData.Add(objHD);
                    }
                    objHD.NgayTT = _NgayTT;
                    objHD.ThangTT = string.Format("{0:yyyy-MM}", _NgayTT);
                    objHD.KyTT = 1;
                    objHD.PhiDV = _TienLai;
                    objHD.TienTT = _TienLai;
                    objHD.TyLeCK = 0;
                    objHD.TienCK = 0;
                    objHD.PhaiThu = _TienLai;
                    objHD.DaThu = 0;
                    objHD.ConNo = _TienLai;
                    objHD.ThucThu = objHD.IsChon == true ? objHD.ConNo : 0;
                    objHD.DienGiai = string.Format("{0} phát sinh đến ngày {1:dd/MM/yyyy}", objHD.TenLDV, _NgayTT);
                }
                else
                {
                    if (objHD != null)
                    {
                        ltData.Remove(objHD);
                    }
                }
                gvHoaDon.PostEditor();

                this.TinhSoTien();

                gvHoaDon.RefreshData();
                gvHoaDon.ExpandAllGroups();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        decimal TinhTienLai()
        {
            decimal _TienLai = 0;
            var _NgayThu = dateNgayThu.DateTime;
            var db = new MasterDataContext();
            foreach (var i in ltData)
            {
                if (i.IsChon & i.MaLDV != 23)
                {
                    _TienLai += db.GetTienLai(this.MaTN, i.MaLDV, i.NgayTT, i.ThucThu, _NgayThu).GetValueOrDefault();
                }
            }

            return _TienLai;
        }

        void GachBo(string ID1)
        {
            //MasterDataContext dbo = new MasterDataContext();


            //var tam = new BusinessSV.BusinessServiceSoapClient();
            //var TK = dbo.TaiKhoanHDDTs.FirstOrDefault();
            //var a = tam.confirmPaymentFkey(string.Format("{0}", ID1), TK.TKWebServies, TK.PassWebServies);




            //if (a.Contains("OK"))
            //{
            //    //DialogBox.Alert();
            //}
            //if (a.Contains("13"))
            //{

            //}
            //else
            //{
            //    DialogBox.Alert(a.ToString());
            //}

        }

        void SaveData(bool _IsPrint, int _ReportID)
        {
            gvHoaDon.FocusedRowHandle = -1;
            var db = new MasterDataContext();

            #region Rang buoc
            if (txtSoPT.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPT.Focus();
                return;
            }

            var objKTPT = db.ptPhieuThus.FirstOrDefault(p => p.MaTN == MaTN & p.SoPT.Equals(txtSoPT.Text.Trim()));

            if (objKTPT != null)
            {
                TaoMaSo();
            }

            if (dateNgayThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày thu");
                dateNgayThu.Focus();
                return;
            }

        
            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người thu");
                lkNhanVien.Focus();
                return;
            }

            if (lkPhanLoai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phân loại phiếu thu");
                lkPhanLoai.Focus();
                return;
            }
           
            if ((int?)lkPhanLoai.EditValue == 22 | (int?)lkPhanLoai.EditValue == 25)//HDT
            {
                var ltSoTien = ltData.Where( o=>o.IsChon).GroupBy( o=> new { o.SoHD, o.MaHD} )
                                     .Select( p => new 
                                     { 
                                         p.Key.MaHD, 
                                         p.Key.SoHD, 
                                         SoTien = p.Sum( o => o.ThucThu).GetValueOrDefault(),
                                     });

                var r = glKhachHang.Properties.GetRowByKeyValue(glKhachHang.EditValue);

                if (r != null)
                {
                    MasterDataContext dbo = new MasterDataContext();

                    foreach (var objKhauTru in ltSoTien)
                    {
                        if (objKhauTru.MaHD == null)
                        {
                            DialogBox.Error("Không thể khấu trừ cho hóa đơn không có số hợp đồng");
                            return;
                        }

                        if ((int)lkPhanLoai.EditValue == 25)
                        {
                            var ThuTruoc = (from pt in dbo.ptPhieuThus
                                            where pt.MaKH == (int)glKhachHang.EditValue & pt.MaPL == (int)lkPhanLoai.EditValue
                                            & pt.idctHopDong == objKhauTru.MaHD
                                            select (decimal?)pt.ptChiTietPhieuThus.Sum(o => o.SoTien).GetValueOrDefault() - dbo.ptChiTietPhieuThus.Where(o => o.ptPhieuThu.idPhieuThuGoc == pt.ID).Sum(o => o.SoTien).GetValueOrDefault()).Sum().GetValueOrDefault()

                                           - (from pt in dbo.ktttKhauTruThuTruocHDTs
                                              where pt.MaKH == (int)glKhachHang.EditValue
                                              & pt.idctHopDong == objKhauTru.MaHD
                                              & pt.MaPL == 25
                                              select pt.SoTien).Sum().GetValueOrDefault()

                                           - (from pt in dbo.pcPhieuChis
                                              where pt.MaNCC == (int)glKhachHang.EditValue & pt.LoaiChi == 7
                                              & pt.idctHopDong == objKhauTru.MaHD
                                              select pt.SoTien).Sum().GetValueOrDefault();

                            if (ThuTruoc < objKhauTru.SoTien)
                            {
                                string TB = string.Format("Số tiền thu trước hợp đồng số {0} còn lại {1:n0} đã vượt quá số tiền sẽ khấu trừ. Vui lòng kiểm tra lại", objKhauTru.SoHD, ThuTruoc);
                                DialogBox.Error(TB);
                                spSoTien.Focus();
                                return;
                            }
                        }

                        if ((int)lkPhanLoai.EditValue == 22)
                        {
                            var ThuTruoc = (from pt in dbo.ptPhieuThus
                                            where pt.MaKH == (int)glKhachHang.EditValue & pt.MaPL == (int)lkPhanLoai.EditValue
                                            & pt.idctHopDong == objKhauTru.MaHD
                                            select (decimal?)pt.ptChiTietPhieuThus.Sum(o => o.SoTien).GetValueOrDefault() - dbo.ptChiTietPhieuThus.Where(o => o.ptPhieuThu.idPhieuThuGoc == pt.ID).Sum(o => o.SoTien).GetValueOrDefault()).Sum().GetValueOrDefault()

                                           - (from pt in dbo.ktttKhauTruThuTruocHDTs
                                              where pt.MaKH == (int)glKhachHang.EditValue
                                              & pt.idctHopDong == objKhauTru.MaHD
                                              & pt.MaPL == 22
                                              select pt.SoTien).Sum().GetValueOrDefault()

                                           - (from pt in dbo.pcPhieuChis
                                              where pt.MaNCC == (int)glKhachHang.EditValue & pt.LoaiChi == 8
                                              & pt.idctHopDong == objKhauTru.MaHD
                                              select pt.SoTien).Sum().GetValueOrDefault();

                            if (ThuTruoc < objKhauTru.SoTien)
                            {
                                string TB = string.Format("Số tiền thu trước hợp đồng số {0} còn lại {1:n0} đã vượt quá số tiền sẽ khấu trừ. Vui lòng kiểm tra lại", objKhauTru.SoHD, ThuTruoc);
                                DialogBox.Error(TB);
                                spSoTien.Focus();
                                return;
                            }
                        }
                    }
                }
            }


            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if ((bool?)lkHTTT.GetColumnValue("IsChuyenKhoan") == true && lkTaiKhoanNganHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn số tài khoản ngân hàng");
                return;
            }

            #endregion

            //Cap nhat lai hoa don
            gvHoaDon.UpdateCurrentRow();
            var ltChiTiet = ltData.Where(p => p.IsChon == true).ToList();

            if (ltChiTiet.Count == 0)
            {
                DialogBox.Error("Vui lòng chọn hạng mục cần thu");
                return;
            }

            this.UpdateHoaDon(ltChiTiet);

            #region Phiếu khấu trừ Hợp đồng thuê

            if ((int?)lkPhanLoai.EditValue == 22 | (int?)lkPhanLoai.EditValue == 25)
            {

                var ltKhauTru = ltData.Where(o => o.IsChon).GroupBy(o => new { o.SoHD, o.MaHD })
                                    .Select(p => new
                                    {
                                        p.Key.MaHD,
                                        p.Key.SoHD,
                                        SoTien = p.Sum(o => o.ThucThu).GetValueOrDefault(),
                                    });

                foreach (var kt in ltKhauTru)
                {
                    var objPT = new ktttKhauTruThuTruocHDT();
                    objPT.idctHopDong = kt.MaHD;
                    objPT.idctThanhLy = MaTL;
                    objPT.MaTN = this.MaTN;
                    TaoMaSo();
                    objPT.SoCT = txtSoPT.Text;                 
                    objPT.SoTien = kt.SoTien;
                    objPT.MaKH = (int)glKhachHang.EditValue;
                    objPT.NguoiYC = txtNguoiNop.Text;
                    objPT.DiaChiYC = txtDiaChi.Text;
                    objPT.LyDo = this.GetDienGiai(kt.MaHD.Value);
                    objPT.ChungTuGoc = txtChungTuGoc.Text;
                    objPT.MaNV = (int)lkNhanVien.EditValue;
                    objPT.MaNVN = Common.User.MaNV;
                    objPT.NgayNhap = db.GetSystemDate();
                    objPT.IsHDThue = true;
                    objPT.MaPL = (int?)lkPhanLoai.EditValue;
                    if (dateNgayCT.EditValue == null)
                    {
                        objPT.NgayCT = dateNgayThu.DateTime;
                    }
                    else
                        objPT.NgayCT = dateNgayCT.DateTime;

                    foreach (var hd in ltChiTiet.Where( o=> o.MaHD == kt.MaHD) )
                    {
                        var objCT = new ktttChiTietHDT();
                        objCT.TableName = hd.TableName;
                        objCT.LinkID = hd.ID;
                        objCT.SoTien = hd.ThucThu;
                        objCT.DienGiai = hd.DienGiai;
                        objPT.ktttChiTietHDTs.Add(objCT);
                    }

                    db.ktttKhauTruThuTruocHDTs.InsertOnSubmit(objPT);

                    db.SubmitChanges();

                    try
                    {
                        foreach (var hd in ltChiTiet)
                        {
                            using (var dbo = new MasterDataContext())
                            {
                                var HD = dbo.dvHoaDons.FirstOrDefault(p => p.ID == hd.ID);
                                if (HD != null)
                                {
                                    HD.MaNVThu = (int)lkNhanVien.EditValue;
                                    HD.NgayThu = (DateTime?)dateNgayThu.EditValue;
                                    HD.LoaiThu = "Khấu trừ dịch vụ hợp đồng thuê";
                                }
                                dbo.SubmitChanges();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khấu trừ thu trước hợp đồng thuê", "Lưu", "Số phiếu: " + txtSoPT.Text + " - Dự án: " + db.tnToaNhas.Single(p => p.MaTN == this.MaTN).TenTN);
                }
            }
            #endregion 

            #region Phiếu thu dv
            if ((int?)lkPhanLoai.EditValue == 1)
            {
                var objPT = new ptPhieuThu();
                //objPT.idctThanhLy = this.MaTL;
                objPT.MaTN = this.MaTN;
                objPT.SoPT = txtSoPT.Text;
                objPT.NgayThu = dateNgayThu.DateTime;
                objPT.SoTien = spSoTien.Value;
                objPT.MaPL = (int)lkPhanLoai.EditValue;
                objPT.MaKH = (int)glKhachHang.EditValue;
                objPT.NguoiNop = txtNguoiNop.Text;
                objPT.DiaChiNN = txtDiaChi.Text;
                objPT.LyDo = txtDienGiai.Text;
                objPT.IsHDThue = true;
                objPT.MaHTHT = (int?)lkHTTT.EditValue;

                //objPT.SoCTNH = (DateTime?)dateNgayCT.EditValue;

                if ( (bool?)lkHTTT.GetColumnValue("IsChuyenKhoan") == true)
                {
                    objPT.MaTKNH = (int?)lkTaiKhoanNganHang.EditValue;
                }
                else
                {
                    objPT.MaTKNH = null;
                }

                objPT.ChungTuGoc = txtChungTuGoc.Text;
                objPT.MaNV = (int)lkNhanVien.EditValue;
                objPT.MaNVN = Common.User.MaNV;
                objPT.NgayNhap = db.GetSystemDate();

                foreach (var hd in ltChiTiet)
                {
                    var objCT = new ptChiTietPhieuThu();
                    objCT.TableName = hd.TableName;
                    objCT.LinkID = hd.ID;
                    objCT.SoTien = hd.ThucThu;
                    objCT.DienGiai = hd.DienGiai;
                    //objCT.SoThe = hd.IsThuThua;
                    objPT.ptChiTietPhieuThus.Add(objCT);
                    if (hd.MaLDV == 6 & hd.LinkID != null)
                    {
                        using (var dbo = new MasterDataContext())
                        {
                            var NgayHetHan = dbo.dvgxTheXes.SingleOrDefault(p => p.ID == hd.LinkID);
                            var Ngay = hd.NgayTT.Value.AddMonths(1);
                            var NgayCuoiThang = DateTime.DaysInMonth(Ngay.Year, Ngay.Month);
                            var ThangHetHan = new DateTime(Ngay.Year, Ngay.Month, NgayCuoiThang);

                        }
                    }

                }

                db.ptPhieuThus.InsertOnSubmit(objPT);
                db.SubmitChanges();
                try
                {
                    
                    foreach (var hd in ltChiTiet)
                    {
                        using (var dbo = new MasterDataContext())
                        {
                            var HD = dbo.dvHoaDons.FirstOrDefault(p => p.ID == hd.ID);
                            if (HD != null)
                            {
                                HD.MaNVThu = (int)lkNhanVien.EditValue;
                                HD.NgayThu = (DateTime?)dateNgayThu.EditValue;
                                HD.LoaiThu = "Thu hóa đơn dịch vụ";
                            }
                            dbo.SubmitChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                ID = objPT.ID;
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm phiếu thu hóa đơn dịch vụ", "Lưu", "Số phiếu: " + txtSoPT.Text + " - Dự án: " + db.tnToaNhas.Single(p => p.MaTN == this.MaTN).TenTN);
                if (_IsPrint)
                {
                    DevExpress.XtraReports.UI.XtraReport rpt = null;
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("In phiếu thu", "In phiếu", "Số phiếu: " + txtSoPT.Text + " - Dự án: " + db.tnToaNhas.Single(p => p.MaTN == this.MaTN).TenTN);
                    switch (_ReportID)
                    {
                        case 3:
                            rpt = new LandSoftBuilding.Fund.Input.rptPhieuThu(objPT.ID, this.MaTN.Value, 1);
                            for (int i = 1; i <= 3; i++)
                            {
                                var rpt1 = new LandSoftBuilding.Fund.Input.rptPhieuThu(objPT.ID, this.MaTN.Value, i);
                                rpt1.CreateDocument();
                                rpt.Pages.AddRange(rpt1.Pages);
                            }
                            rpt.PrintingSystem.ContinuousPageNumbering = true;

                            break;
                        case 19:
                            rpt = new LandSoftBuilding.Fund.Input.rptDetail(objPT.ID, this.MaTN.Value);
                            break;
                        case 42:
                            rpt = new LandSoftBuilding.Fund.Input.rptPhieuThuMau3(objPT.ID, this.MaTN.Value);
                            break;
                    }

                    if (rpt != null)
                    {
                        rpt.ShowPreviewDialog();
                    }
                }




            }//Thu dich vu
            #endregion

            if (Common.User.MaNV == 6)
            {
                int ThangCu = 0;
                int NamCu = 0;
                int ThangMoi = 0;
                int NamMoi = 0;
                #region Xu ly trang thai hoa don
                using (var dbo1 = new MasterDataContext())
                {
                    foreach (var item in ltChiTiet)
                    {
                        ThangMoi = item.NgayTT.Value.Month;
                        NamMoi = item.NgayTT.Value.Year;
                        if (ThangMoi != ThangCu)
                        {
                            if (NamMoi == NamCu)
                            {
                                //khong làm gì
                            }
                            else
                            {
                                var list = (from hd in dbo1.dvHoaDons
                                            where hd.MaKH == this.MaKH & hd.NgayTT.Value.Month == item.NgayTT.Value.Month & hd.NgayTT.Value.Year == item.NgayTT.Value.Year &
                                             hd.IsDuyet == true
                                                // & (hd.FKey != null | hd.FKey != "")
                                             &
                                              hd.PhaiThu
                                                - dbo1.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()

                                                 - dbo1.ktttChiTiets.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()
                                                //- dbo1.ktttChiTietBBQs.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()
                                                //- dbo1.ktttChiTietGYMs.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()
                                                //- dbo1.ktttChiTietTheBois.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()
                                                //- dbo1.ktttChiTietThiCongs.Where(p => p.LinkID == hd.ID).Sum(p => p.SoTien).GetValueOrDefault()

                                                > 0
                                            select hd
                                  ).ToList();

                                var nhomKey = (from hd in dbo1.dvHoaDons
                                               where hd.MaKH == this.MaKH & hd.NgayTT.Value.Month == item.NgayTT.Value.Month & hd.NgayTT.Value.Year == item.NgayTT.Value.Year &
                                                hd.IsDuyet == true & (hd.FKey != null | hd.FKey != "")
                                               group hd by new { hd.FKey } into gr
                                               select new { gr.Key.FKey }
                                            ).ToList();

                                if (list.Count() == 0)//hết nợ
                                {
                                    foreach (var i in nhomKey)
                                    {
                                        try
                                        {
                                            GachBo(i.FKey);
                                        }
                                        catch(Exception ex)
                                        {
                                            DialogBox.Error("Lỗi gạch nợ:" + ex.Message);
                                        }
                                    }
                                }
                            }
                        }

                        ThangCu = item.NgayTT.Value.Month;
                        NamCu = item.NgayTT.Value.Year;
                    }


                }

                #endregion
            }


                // Tính lãi theo từng LTT
                var ltLTT = ltChiTiet.Where(o => o.MaLTT.GetValueOrDefault() > 0).Select(o => o.MaLTT).Distinct();

                //foreach (var idLTT in ltLTT)
                //    DichVu.LaiSuat.LaiSuatCls.TinhLai(idLTT.Value);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        void Load_MauPhieuThu()
        {
            var db = new MasterDataContext();
            try
            {
                var ltReport = (from rp in db.rptReports
                                join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == this.MaTN & rp.GroupID == 5
                                orderby rp.Rank
                                select new { rp.ID, rp.Name, tn.IsDefault }).ToList();

                popupMenu1.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                    barManager1.Items.Add(itemPrint);
                    popupMenu1.ItemLinks.Add(itemPrint);
                }

                var objRP = ltReport.FirstOrDefault(p => p.IsDefault == true);
                if (objRP == null)
                {
                    objRP = ltReport.First();
                }
                btnLuuIn.Tag = objRP.ID;
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            var db = new MasterDataContext();

            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN & kh.MaKH == this.MaKH
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                     ThuTruoc = (from pt in db.ptPhieuThus
                                                                 where pt.MaKH == this.MaKH & pt.MaPL == 2
                                                                 select pt.SoTien).Sum().GetValueOrDefault()
                                               - (from pt in db.ktttKhauTruThuTruocs
                                                  where pt.MaKH == this.MaKH
                                                  select pt.SoTien).Sum().GetValueOrDefault()
                                                 }).ToList();

            lkNhanVien.Properties.DataSource = (from n in db.tnNhanViens
                                                join ng in db.tnToaNhaNguoiDungs on n.MaNV equals ng.MaNV
                                                //where ng.MaTN == this.MaTN
                                                select new
                                                {
                                                    n.MaNV,
                                                    n.MaSoNV,
                                                    n.HoTenNV
                                                }).ToList();
            lkTaiKhoanNganHang.Properties.DataSource = (from tk in db.nhTaiKhoans
                                                        join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                                        where tk.MaTN == this.MaTN
                                                        select new { tk.ID, tk.SoTK, nh.TenNH, tk.ChuTK }).ToList();
            if(this.MaTL.GetValueOrDefault() > 0)
                lkPhanLoai.Properties.DataSource = (from pl in db.ptPhanLoais where pl.ID == 1 | pl.ID == 25 select new { pl.ID, pl.TenPL }).ToList();
            else
                lkPhanLoai.Properties.DataSource = (from pl in db.ptPhanLoais where pl.ID == 1 | pl.ID == 22 | pl.ID == 25 select new { pl.ID, pl.TenPL }).ToList();
            lkPhanLoai.ItemIndex = 0;
            glKhachHang.EditValue = this.MaKH;
            dateNgayThu.EditValue = db.GetSystemDate();
            lkNhanVien.EditValue = Common.User.MaNV;

            lkHTTT.Properties.DataSource = db.ptHinhThucThanhToans;
            lkHTTT.ItemIndex = 0;

            checkButton1.Enabled = false;

            if (this.MaThiCong > 0)
            {
                lkPhanLoai.Properties.ReadOnly = true;
                lkPhanLoai.EditValue = 18;
            }
            //
            this.Load_MauPhieuThu();

            if (MaTL != null)
                glKhachHang.Enabled = false;
            lkNhanVien.Properties.ReadOnly = true;
            lkNhanVien.EditValue = Common.User.MaNV;
        }

        private void glKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var db = new MasterDataContext();

                if (MaTL == null)
                {
                    ltData = HopDongThue.PaymentCls.GetListPaymentByIDCustomer((int?)glKhachHang.EditValue);
                }
                else
                {
                    ltData = HopDongThue.PaymentCls.GetListPaymentByIDLiquidate(this.MaTL.Value);
                }

                gcHoaDon.DataSource = null;
                gcHoaDon.DataSource = ltData;
                gvHoaDon.ExpandAllGroups();

                var r = glKhachHang.Properties.GetRowByKeyValue(glKhachHang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNop.Text = type.GetProperty("TenKH").GetValue(r, null).ToString();
                    txtDiaChi.Text = (from mb in db.mbMatBangs
                                      join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                      where mb.MaKH == (int)glKhachHang.EditValue
                                      select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();
                }
            }
            catch { }
        }

        private void gvHoaDon_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var hd = (HopDongThue.PaymentCls.PaymentItemHDT)gvHoaDon.GetRow(e.RowHandle);
            switch (e.Column.FieldName)
            {
                case "ThucThu":
                    //hd.IsChon = hd.ThucThu > 0;      
                    break;
                case "IsChon":
                    if (hd.IsChon == true)
                    {
                        if (hd.ConNo != 0 || hd.MaLDV == 49)
                            hd.ThucThu = hd.ConNo;
                        else
                            hd.IsChon = false;
                    }
                    else
                    {
                        hd.ThucThu = 0;
                    }
                    break;
                case "KyTT":
                case "TyLeCK":
                case "TienCK":
                    if (hd.KyTT > 0)
                        hd.TienTT = hd.PhiDV * hd.KyTT;
                    else
                        hd.TienTT = hd.PhiDV;

                    if (e.Column.FieldName == "KyTT")
                    {
                        hd.TyLeCK = this.GetTyleCK(hd.MaLDV.Value, hd.KyTT.Value);
                        hd.TienCK = hd.TyLeCK * hd.TienTT;
                    }

                    if (e.Column.FieldName == "TyLeCK") hd.TienCK = hd.TyLeCK * hd.TienTT;

                    hd.PhaiThu = hd.TienTT - hd.TienCK;
                    hd.ConNo = hd.PhaiThu - hd.DaThu;

                    // if (hd.ConNo <= 0) hd.IsChon = false;

                    if (hd.IsChon == true)
                    {
                        hd.ThucThu = hd.ConNo;
                    }
                    else
                    {
                        hd.ThucThu = 0;
                    }
                    break;
            }
            gvHoaDon.PostEditor();

            this.UpdateLaiPhatSinh();
        }

        private void cmbPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dateNgayThu_EditValueChanged(object sender, EventArgs e)
        {
            this.UpdateLaiPhatSinh();
        }

        private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var hd in ltData)
            {
                hd.IsChon = hd.ConNo > 0 & ckbSelectAll.Checked;
                hd.ThucThu = hd.IsChon ? hd.ConNo : 0;
            }
            gvHoaDon.PostEditor();
            this.UpdateLaiPhatSinh();

            gvHoaDon.RefreshData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.SaveData(false, 0);
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SaveData(true, (int)e.Item.Tag);
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            this.SaveData(true, (int)btnLuuIn.Tag);
          
        }

        private void glKhachHang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }

        private void lkPhanLoai_EditValueChanged(object sender, EventArgs e)
        {
            TaoMaSo();
        }

        void SetPhanLoaiSoCT()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    if ((int?)lkPhanLoai.EditValue == 19)//bbq
                        txtSoPT.EditValue = db.CreateSoChungTu(44, this.MaTN);


                    if ((int?)lkPhanLoai.EditValue == 1)
                        txtSoPT.EditValue = db.CreateSoChungTu(11, this.MaTN);
                }
            }
            catch { }
        }

        void TaoMaSo()
        {
            try
            {
                bool IsChuyenKhoan = ((bool?)lkHTTT.GetColumnValue("IsChuyenKhoan")).GetValueOrDefault();
                lkTaiKhoanNganHang.Enabled = IsChuyenKhoan;

                MasterDataContext db = new MasterDataContext();

                if ((int?)lkPhanLoai.EditValue == 22) // Lhấu trừ thu trước
                {
                    switch ((int?)lkHTTT.EditValue)
                    {
                        // CHUYỂN KHOẢN
                        case 2:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(61, MaTN);
                            break;
                        //POS
                        case 3:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(60, MaTN);
                            break;
                        // Thu hộ
                        case 4:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(62, MaTN);
                            break;
                        //Phiếu thu
                        default:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(63, MaTN);
                            break;
                    }
                }
                else
                {
                    switch ((int?)lkHTTT.EditValue)
                    {
                        // CHUYỂN KHOẢN
                        case 2:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(34, MaTN);
                            break;
                        //POS
                        case 3:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(47, MaTN);
                            break;
                        // Thu hộ
                        case 4:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(48, MaTN);
                            break;
                        //Phiếu thu
                        default:
                            if (this.MaPT == null)
                                txtSoPT.EditValue = db.CreateSoChungTu(11, MaTN);
                            break;
                    }
                }
            }
            catch { }
        }

        private void cmbPTTT_EditValueChanged(object sender, EventArgs e)
        {
            TaoMaSo();
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            //this.SaveData(true, 0);
            //HoaDonDienTu();
        }

        private void gcHoaDon_Click(object sender, EventArgs e)
        {

        }

        private void txtPhieuThu_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtPhieuThu_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtSoPT_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateNgayCT_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}