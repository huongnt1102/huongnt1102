using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraReports.Parameters;
using Document = DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document;

namespace LandSoftBuilding.Report
{
    public partial class rptPhieuThuBacHaMau2 : DevExpress.XtraReports.UI.XtraReport
    {
        List<GiayBaoBacHa> ListGiayBao = new List<GiayBaoBacHa>();
        MasterDataContext db = new MasterDataContext();
        public int _MaPT;
        public decimal TongTien;
        public decimal XeMay1;
        public decimal XeDap1;
        public decimal Xeoto1;
        int? MaKh { get; set; }
        public rptPhieuThuBacHaMau2(int ID, byte MaTN,int Lien )
        {
            InitializeComponent();
            _MaPT = ID;
            //Library.frmPrintControl.LoadLayout(this, 3, MaTN);
            var objTien = new TienTeCls();
            cSTT.DataBindings.Add("Text", null, "STT");
            cSoLuong.DataBindings.Add("Text", null, "SoLuong");
            //cDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cNoiDung.DataBindings.Add("Text", null, "TenLDV");
            cThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            //cDVT.DataBindings.Add("Text", null, "TenDVT");
            cCSC.DataBindings.Add("Text", null, "ChiSoCu","{0:#,0.##}");
            cCSM.DataBindings.Add("Text", null, "ChiSoMoi","{0:#,0.##}");
           // cDVT2.DataBindings.Add("Text", null, "TenDVT");
            if (Lien == 2)
            {
                cLien.Text = "(Liên 2: lưu kế toán)";
            }
            #region Thong tin toa nha
            var objTN = (from tn in db.tnToaNhas
                         where tn.MaTN == MaTN
                         select new { tn.TenTN, tn.CongTyQuanLy, tn.DiaChiCongTy, tn.Logo ,tn.DienThoai})
                         .FirstOrDefault();
            xrTableCell1.Text =
                xrTableCell1.Text.Replace("[ToaNha]", objTN.CongTyQuanLy)
                    .Replace("[DiaChi]", objTN.DiaChiCongTy)
                    .Replace("[DienThoai]", objTN.DienThoai);
            //picLogo.ImageUrl = objTN.Logo;
            //cTenTN.Text = objTN.TenTN;
            //cDiaChiTN.Text = objTN.DiaChiCongTy;
            #endregion
            //Thay the du lieu
           
            bindingSource1.DataSource = this.ListGiayBao;

                try
                {
                    //var objPT = (from p in db.ptPhieuThus
                    //             join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                    //             where p.ID == _MaPT
                    //             select new
                    //             {
                    //                 p.ID,
                    //                 p.NgayThu,
                    //                 nv.HoTenNV
                    //             }).First();
                    var objPT = (from p in db.ptPhieuThus
                                 join k in db.tnKhachHangs on p.MaKH equals k.MaKH
                                 join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                                 where p.ID == ID
                                 select new
                                 {
                                     p.MaKH,p.LyDo,
                                     p.SoPT,nv.HoTenNV,
                                     p.NgayThu,
                                     p.NguoiNop,
                                     TenKH = (bool)k.IsCaNhan ? String.Format("{0} {1}", k.HoKH, k.TenKH) : k.CtyTen,
                                     k.mbMatBangs.FirstOrDefault().MaSoMB,
SoTien=p.ptChiTietPhieuThus.Sum(p1=>p1.SoTien),
                                     SoTien_BangChu = objTien.DocTienBangChu(p.ptChiTietPhieuThus.Sum(p1 => p1.SoTien).Value, "đồng chẵn"),
                                 }).FirstOrDefault(); MaKh = objPT.MaKH;
                    cTenKh.Text = objPT.TenKH;
                    cTienPT.Text = string.Format("{0:#,0.##} ({1})", objPT.SoTien, objPT.SoTien_BangChu);
                    //cTienBC.Text = objPT.SoTien_BangChu;
                    var test = GetDienGiaiThangLong().Length;//250
                    cDienGiai.Text = test >= 230 ? GetDienGiaiThangLong().Substring(0, 230) + "..." : GetDienGiaiThangLong();//objPT.LyDo;
                    if (cDienGiai.Text == "")
                    {
                        cDienGiai.Text = objPT.LyDo;
                    }
                    #region Chi tiết phiếu thu
                    //var objPT = (from p in db.ptPhieuThus
                    //             join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                    //             where p.ID == _MaPT
                    //             select new
                    //             {
                    //                 p.ID,p.NgayThu,
                    //                 nv.HoTenNV
                    //             }).First();
                    var ltLoaiDichVu = (from //ct in db.ptChiTietPhieuThus
                              // join 
                                   hd in db.dvHoaDons //on ct.LinkID equals hd.ID
                               join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                               join mb in db.mbMatBangs on hd.MaMB equals mb.MaMB
                                        where hd.MaKH == objPT.MaKH
                                        //& ct.TableName == "dvHoaDon" 
                                        & hd.NgayTT.Value.Month == objPT.NgayThu.Value.Month & hd.NgayTT.Value.Year == objPT.NgayThu.Value.Year
                               group hd by new {ldv.STT,hd.MaLDV, ldv.TenLDV,ldv.TenTA,ldv.TenHienThi,hd.NgayTT.Value.Month,hd.NgayTT.Value.Year} into gr
                               select new
                               {
                                   gr.Key.STT,
                                   gr.Key.MaLDV,
                                   TenLDV = gr.Key.TenHienThi,gr.Key.TenTA,
                                   Thang = gr.Key.Month,
                                   ThangDV = (gr.Key.MaLDV == 8 || gr.Key.MaLDV == 9 || gr.Key.MaLDV == 10 || gr.Key.MaLDV == 11 || gr.Key.MaLDV == 22) ? gr.Key.Month - 1 : gr.Key.Month,
                                   Nam = gr.Key.Year,
                               }).ToList();
                    var Nam=ltLoaiDichVu[0].Nam;
                    var Thang = ltLoaiDichVu[0].Thang;
                    var NgayTB = new DateTime(Nam, Thang, 1);
                    var NgayCuoi = DateTime.DaysInMonth(Nam, Thang);
                    var NgayCuoiThang = new DateTime(Nam, Thang, NgayCuoi);
                    var objGB = new GiayBaoBacHa();
                    objGB.DichVus = new List<DichVuBacHa>();
             
                    foreach (var ldv in ltLoaiDichVu)
                    {
                        var objLDV = new DichVuBacHa();
                        objLDV.MaLDV = ldv.MaLDV.GetValueOrDefault();
                        int? _ThangDV = (ldv.MaLDV == 8 || ldv.MaLDV == 5 || ldv.MaLDV == 9 || ldv.MaLDV == 10 || ldv.MaLDV == 11 || ldv.MaLDV == 22) ? (ldv.Thang - 1 == 0 ? 12 : ldv.Thang - 1) : ldv.Thang;

                        objLDV.TenLDV = string.Format("{0} (Tháng {1}/{2})", ldv.TenLDV + "/" + ldv.TenTA, _ThangDV, (ldv.MaLDV == 8 || ldv.MaLDV == 9 || ldv.MaLDV == 10 || ldv.MaLDV == 11 || ldv.MaLDV == 22) ? (ldv.Thang - 1 == 0 ? ldv.Nam - 1 : ldv.Nam) : ldv.Nam);
                        objLDV.SoLuong = string.Format("0");
                        objLDV.TenDVT = ldv.MaLDV == 6
                            ? "Chiếc"
                            : (ldv.MaLDV == 13
                                ? "M2"
                                : (ldv.MaLDV == 9 ? "M3" : (ldv.MaLDV == 8 ? "kWh" : (ldv.MaLDV == 10 ? "M3" : ""))));
                        objLDV.ThanhTien = 0;
                        objLDV.Details = new List<DichVuBacHa>();
                        switch (objLDV.MaLDV)
                        {
                            case 2: //Tien thue ==> OK
                                this.Load_TienThue(objLDV, ldv.Thang, ldv.Nam);
                                break;
                            case 5: //Tien dien ==> OK
                                this.Load_DienKhoan(objLDV, ldv.Thang, ldv.Nam);
                                break;
       
                            case 6: //Phi giu xe ==> OK
                                this.Load_GiuXe(objLDV,ldv.Thang,ldv.Nam);
                                break;
                            case 8: //Tien dien ==> OK
                                this.Load_Dien(objLDV, ldv.Thang, ldv.Nam);
                                break;
                            case 9: //Tien nuoc ==>OK
                                this.Load_Nuoc(objLDV, ldv.Thang, ldv.Nam);
                                break;
                            case 10: //Tien gas
                                this.Load_GAS(objLDV, ldv.Thang, ldv.Nam);
                                break;
                            case 11: //Dieu hoa 
                                this.Load_DieuHoa(objLDV, ldv.Thang, ldv.Nam);
                                break;
                            case 13: //PQL
                                this.Load_PQL(objLDV, ldv.Thang, ldv.Nam);
                                break;
                            case 22: //Tien nuoc sinh hoat
                                this.Load_NuocSinhHoat(objLDV, ldv.Thang, ldv.Nam);
                                break;
                            default: //Dich vu co ban
                                this.Load_DichVuCoBan(objLDV, ldv.Thang, ldv.Nam);
                                break;
                        }
                        objGB.DichVus.Add(objLDV);
                    }
                    this.ListGiayBao.Add(objGB);
                    #endregion
///////////////////////////////////////////////////////////////////////////////////////////////////////
                    #region Thong tin PhieuThu
                   
                    var PhatSinh =
                        (from //ct in db.ptChiTietPhieuThus
                         //join 
                         hd in db.dvHoaDons //on ct.LinkID equals hd.ID
                         where hd.MaTN == MaTN & hd.MaKH == objPT.MaKH
                              & (hd.PhaiThu.GetValueOrDefault() -
                              (from ct in db.ptChiTietPhieuThus
                               join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                               //join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                               where hd.ID == ct.LinkID & SqlMethods.DateDiffDay(hd.NgayTT, pt.NgayThu) < 0
                               select ct.SoTien).Sum().GetValueOrDefault()) - 
                               (from ct in db.ktttChiTiets
                                  join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                  // join hd1 in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd1.ID }
                                  where ct.LinkID == hd.ID// & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0
                                  select ct.SoTien).Sum().GetValueOrDefault() > 0  //moi mo 5/11/2016
                                 
                         & hd.NgayTT.Value.Month==objPT.NgayThu.Value.Month & hd.IsDuyet==true
                         & hd.NgayTT.Value.Year==objPT.NgayThu.Value.Year select hd.PhaiThu.GetValueOrDefault()).Sum();
                    ;
                    var Ngaymoi = new DateTime(objPT.NgayThu.Value.Year, objPT.NgayThu.Value.Month, 1);
                   var _NoCu = (from ct in db.ptChiTietPhieuThus
                               join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                             where ct.MaPT == ID
                            // & hd.IsThuThua.GetValueOrDefault() == false
                            // & (hd.PhaiThu.GetValueOrDefault() - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()) != 0
                             & SqlMethods.DateDiffDay(hd.NgayTT, Ngaymoi) > 0
                             & hd.IsDuyet==true
                                 //& (_MaLDVs.Contains(hd.MaLDV.Value) | _MaLDVs.Count == 0) & hd.IsDuyet == true
                                select 
                                (ct.SoTien 
                               // - (db.ptChiTietPhieuThus.Where(p => p.LinkID == hd.ID & p.TableName == "dvHoaDon").Sum(p => p.SoTien)).GetValueOrDefault()
                                )
                                ).Sum().GetValueOrDefault();
                    cPhatSinhThangNay.Text = string.Format("{0:#,0.##}", PhatSinh);
                    cSumTien.Text = string.Format("{0:#,0.##}", objPT.SoTien);
                    //cSoTienBC.Text = objTien.DocTienBangChu(objPT.SoTien.GetValueOrDefault(), "đồng");
                     var objKH = (from mb in db.mbMatBangs
                         join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                         where mb.MaKH == objPT.MaKH
                         select new
                         {
                             TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                             mb.MaSoMB,
                             kh.ThuTruoc
                         }).First();
                     cPhong.Text = string.Format("Căn hộ    : {0}", objKH.MaSoMB);
                    cSOPT.Text =string.Format   ("Số phiếu : {0}", objPT.SoPT);
                    var tungay =new DateTime(objPT.NgayThu.Value.Year,objPT.NgayThu.Value.Month,1);
                    var denngay = new DateTime(objPT.NgayThu.Value.Year, objPT.NgayThu.Value.Month, DateTime.DaysInMonth(objPT.NgayThu.Value.Year, objPT.NgayThu.Value.Month));
                    var thutruocdauky = (from pt in db.ptPhieuThus
                        where pt.MaKH == this.MaKh & pt.MaPL == 2
                        //& SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                        select pt.SoTien).Sum().GetValueOrDefault()
                                        - (from pt in db.ktttKhauTruThuTruocs
                                            where
                                                pt.MaKH == this.MaKh & SqlMethods.DateDiffDay(pt.NgayCT, denngay) >= 0
                                            select pt.SoTien).Sum().GetValueOrDefault()

                        ;
                    
                    cTienThuTruocThangSau.Text = string.Format(
                        "{0:#,0.##}", (_NoCu-thutruocdauky  ) + PhatSinh - objPT.SoTien);
                    //cTienThuTruocThangTruoc.Text =thutruocdauky==0 ? "0" : string.Format(
                    //   "({0:#,0.##})", thutruocdauky);
                    cTienThuTruocThangTruoc.Text = string.Format("{0:#,0.##}", _NoCu-thutruocdauky);
            // Chi tiết phiếu thu
            cctNoiDung.DataBindings.Add("Text", null, "TenLDV");
            cctSoLuong.DataBindings.Add("Text", null, "SoLuong", "{0:#,0.##}");
            //cctDonGia.DataBindings.Add("Text", null, "DonGia", "{0:#,0.##}");
            cctThanhTien.DataBindings.Add("Text", null, "ThanhTien", "{0:#,0.##}");
            cCSCCT.DataBindings.Add("Text", null, "ChiSoCu","{0:#,0.##}");
            cCSMCT.DataBindings.Add("Text", null, "ChiSoMoi", "{0:#,0.##}");
            cSTTct.DataBindings.Add("Text", null, "STT");
            var ctlRTF = new DevExpress.XtraRichEdit.RichEditControl();
            var _NgayIn = db.GetSystemDate();                  
                            cNgayIn.Text = string.Format("Hà Nội, ngày {0} tháng {1} năm {2}", _NgayIn.Day, _NgayIn.Month, _NgayIn.Year);

                    #endregion

                }
                catch { }
                finally
                {
                    db.Dispose();
                }
        }

   
        void Load_GiuXe(DichVuBacHa objLDV, int? Thang, int Nam)
        {
            try
            {  var objPT = (from p in db.ptPhieuThus
                             join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                             where p.ID == _MaPT
                             select new
                             {
                                 p.ID,p.NgayThu,
                                 nv.HoTenNV
                             }).First();
                //var ltGiuXe = (from ct in db.ptChiTietPhieuThus
                //               join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                //               join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into thexe
                //               from tx in thexe.DefaultIfEmpty()
                //               join lx in db.dvgxLoaiXes on tx.MaLX equals  lx.MaLX into loaixe
                //               from lx in loaixe.DefaultIfEmpty()
                //               where ct.MaPT == _MaPT & hd.TableName == "dvgxTheXe" & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                //               select new GiuXeBacHa()
                //               {
                //                   SoLuong = 1,
                //                   DonGia = tx.GiaThang,
                //                   ThanhTien = tx.GiaThang,
                //                   //TenLX = "Biển số: " + tx.BienSo,
                //                   TenLX=lx.TenLX
                //               }).ToList();


                var ltGiuXe = (from //ct in db.ptChiTietPhieuThus
                                 // join
                                      hd in db.dvHoaDons //on ct.LinkID equals hd.ID
                                  join xe in db.dvgxTheXes on hd.LinkID equals  xe.ID
                                  //join lx in db.dvgxLoaiXes on xe.MaLX equals lx.MaLX
                                  
                                  where //ct.MaPT == _MaPT 
                                  //& 
                               hd.MaKH ==this.MaKh 
                               &
                                  hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                                   & (hd.PhaiThu.GetValueOrDefault() -
                              (from ct in db.ptChiTietPhieuThus
                               join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                               //join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                               where hd.ID == ct.LinkID & SqlMethods.DateDiffDay(hd.NgayTT, pt.NgayThu) < 0
                               select ct.SoTien).Sum().GetValueOrDefault()
                               - (from ct in db.ktttChiTiets
                                  join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                  // join hd1 in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd1.ID }
                                  where ct.LinkID == hd.ID// & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0
                                  select ct.SoTien).Sum().GetValueOrDefault())
                               > 0  //moi mo 5/11/2016
                                    group  new {//lx,
                                        hd,xe} by new {//lx.TenLX,
                                        xe.GiaThang,hd.NgayTT.Value.Month,hd.NgayTT.Value.Year} into gr
                                  select new
                                  {
                                     // gr.Key.TenLX,
                                      DonGia=gr.Key.GiaThang,
                                      SoLuong = gr.Count(),
                                      ThanhTien = gr.Sum(p=>(decimal?)p.hd.PhaiThu).GetValueOrDefault(),
                                      gr.Key.Month,gr.Key.Year

                                  }).AsEnumerable().Select((p, index) => new
                                  {
                                     // p.TenLX,
                                     p. DonGia,
                                     SoLuong = string.Format("{0:#,0.##}", p.SoLuong),
                                      p.ThanhTien,
                                     p.Month,
                                      p.Year,STT=index+1,
                                  }).ToList();
               
                cNguoilap.Text = objPT.HoTenNV;
               // lbNhanVienLap.Text = objPT.HoTenNV;
                //if (ltGiuXe.Count == 0)
                //{
                //    var tam = (from ct in db.ptChiTietPhieuThus
                //               join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                //               where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 6
                //               select ct.SoTien).ToList();//.Sum().GetValueOrDefault();
                //    objLDV.ThanhTien = tam.Sum().GetValueOrDefault();
                //    objLDV.SoLuong = tam.Count;

                //}
                //else//Moi them de show len Parent
                //{
                    objLDV.ThanhTien = ltGiuXe.Sum(p=>p.ThanhTien);
                objLDV.SoLuong = string.Format("0"); //ltGiuXe.Sum(p => p.SoLuong);
                // }


                //foreach (var i in ltGiuXe)
                //{
                //    string tam = "";
                //    objLDV.SoLuong += i.SoLuong;
                //    objLDV.ThanhTien += i.ThanhTien;
                //    var ltBienSo = (from tx in db.dvgxTheXes
                //                    join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
                //                    join hd in db.dvHoaDons on tx.ID equals hd.LinkID
                //                    join ct in db.ptChiTietPhieuThus on hd.ID equals ct.LinkID
                //                    where lx.TenLX == i.TenLX & hd.MaLDV == 6 & ct.MaPT == objPT.ID & hd.NgayTT.Value.Month == i.Month & hd.NgayTT.Value.Year == i.Year
                //                    select tx.BienSo).ToList();

                //    foreach (var bs in ltBienSo)
                //        if (!string.IsNullOrEmpty(bs))
                //            tam += bs + "; ";
                //    tam = tam.Trim();
                //   // i.BienSo = i.BienSo.Trim(' ').Trim(';');
                //    var objGB = new DichVuBacHa();
                //    objGB.TenLDV ="Phí trông xe";// i.TenLX + (tam.Length>0?  string.Format("({0})",tam.ToString().TrimEnd(';')):"");
                //    objGB.SoLuong = (decimal?)ltGiuXe.Where(p=>p.Month==i.Month & p.Year==i.Year&p.TenLX==i.TenLX).Sum(p=>p.SoLuong);
                //    objGB.DonGia = (decimal?)i.DonGia;
                //    objGB.STT = i.STT;
                //    objGB.ThanhTien = //(decimal?)i.ThanhTien;

                //        (decimal?)ltGiuXe.Where(p => p.Month == i.Month & p.Year == i.Year & p.TenLX == i.TenLX).Sum(p => p.ThanhTien);
                //    objGB.TenDVT = "Chiếc";
                //    objLDV.Details.Add(objGB);
                //}


            }
            catch { }
        }
        string GetDienGiaiThangLong()
        {
            var db = new Library.MasterDataContext();
            string strDienGiai = "";
            try
            {
                int dem = 0;
                var ltData = (from hd in db.dvHoaDons
                              join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                              join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                              from tx in the.DefaultIfEmpty()
                              where ptct.MaPT == _MaPT
                              group hd by new { hd.MaLDV, ldv.TenLDV, tx.BienSo } into gr
                              select new
                              {

                                  gr.Key.BienSo,
                                  gr.Key.MaLDV,
                                  gr.Key.TenLDV,
                                  // gr.Key.NgayTT,
                              }).ToList();

                foreach (var i in ltData)
                {
                    var ltLDVXe = (from l in ltData
                                   //join xe in db.dvgxTheXes on l.LinkID equals xe.ID into t
                                   //from xe in t.DefaultIfEmpty()
                                   where l.MaLDV == i.MaLDV & l.BienSo == i.BienSo
                                   group l by new { l.MaLDV, l.BienSo } into gr
                                   select new { gr.Key.MaLDV, BienSo = gr.Key.BienSo }).Distinct().ToList();
                    var ltDV = i.BienSo != null ? (from hd in db.dvHoaDons
                                                   join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                                                   join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                                   //join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                                                   //from tx in the.DefaultIfEmpty()
                                                   where ldv.ID == i.MaLDV & ptct.MaPT == _MaPT & ptct.DienGiai.Contains(i.BienSo.ToString())
                                                   group hd by new { hd.NgayTT.Value.Month, hd.NgayTT.Value.Year } into gr
                                                   orderby gr.Max(p => p.NgayTT)
                                                   select gr.Max(p => p.NgayTT)).ToList() : (from hd in db.dvHoaDons
                                                                                             join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                                                                                             join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                                                                             //join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                                                                                             //from tx in the.DefaultIfEmpty()
                                                                                             where ldv.ID == i.MaLDV & ptct.MaPT == _MaPT //& ptct.DienGiai.Contains(i.BienSo.ToString())
                                                                                             group hd by new { hd.NgayTT.Value.Month, hd.NgayTT.Value.Year } into gr
                                                                                             orderby gr.Max(p => p.NgayTT)
                                                                                             select gr.Max(p => p.NgayTT)).ToList();
                    var j = 0;
                    var _Start = j;
                    var strTime = "";

                    while (j < ltDV.Count)
                    {
                        if ((j + 1) == ltDV.Count || System.Data.Linq.SqlClient.SqlMethods.DateDiffMonth(ltDV[j], ltDV[j + 1]) != 1)
                        {


                            if (_Start != j)
                            {
                                if (i.MaLDV == 8| i.MaLDV == 5 | i.MaLDV == 9 | i.MaLDV == 22)
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
                                    else
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1), ltDV[j].Value.AddMonths(-1));
                                }
                                else
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);////Loi
                                    else
                                        strTime += string.Format("T{0:MM}-T{1:MM/yyyy},", ltDV[_Start], ltDV[j]);
                                }

                            }
                            else
                            {
                                if (i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 9 | i.MaLDV == 22)
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
                    //strDienGiai += string.Format("{0} {1} ({2}), ", i.TenLDV, i.BienSo, strTime);
                    
                    foreach (var tam in ltLDVXe)
                    {

                        if (tam.MaLDV == 6)
                        {
                            if (dem == 0)
                                strDienGiai += string.Format("{0} {1} ({2}) , ", i.TenLDV, tam.BienSo, strTime);
                            else
                            {
                                strDienGiai += string.Format("{0} ({1}) , ", tam.BienSo, strTime);
                            }
                        }
                        else
                        {
                            strDienGiai += string.Format("{0} {1} ({2}) , ", i.TenLDV, tam.BienSo, strTime);
                        }

                        if (tam.MaLDV == 6)
                        {
                            dem++;
                        }
                    }
                    if (ltLDVXe.Count == 0)
                    {
                        if (dem == 0)
                        strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
                        else
                        {
                            strDienGiai += string.Format("({0}), ",  strTime);
                        }
                        if (i.MaLDV == 6)
                        {
                            dem++;
                        }
                    }
                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        void Load_Nuoc(DichVuBacHa objLDV, int? Thang, int? Nam)
        {
            try
            {
                var objNuoc = (from //ct in db.ptChiTietPhieuThus
                               //join 
                                   hd in db.dvHoaDons //on ct.LinkID equals hd.ID
                               join tn in db.dvNuocs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuoc", LinkID = (int?)tn.ID }
                               where hd.MaKH == this.MaKh & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam 

                              &(hd.PhaiThu.GetValueOrDefault() -
                              (from ct in db.ptChiTietPhieuThus
                               join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                               //join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                               where hd.ID == ct.LinkID & SqlMethods.DateDiffDay(hd.NgayTT, pt.NgayThu) < 0
                               select ct.SoTien).Sum().GetValueOrDefault()
                               - (from ct in db.ktttChiTiets
                                  join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                  // join hd1 in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd1.ID }
                                  where ct.LinkID == hd.ID// & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0
                                  select ct.SoTien).Sum().GetValueOrDefault())
                               > 0  //moi mo 5/11/2016
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,
                                   tn.ChiSoMoi,
                                   tn.SoTieuThu,
                                   SoUuDai = (from ud in db.dvNuocUuDais where ud.MaMB == tn.MaMB select ud.SoNguoi).FirstOrDefault(),
                                   tn.TienTT,
                               }).FirstOrDefault();
                var objGB = new DichVuBacHa();
                //if (objNuoc == null)
                //{
                //     var tam =(from ct in db.ptChiTietPhieuThus
                //                        join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                //                        where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 9
                //                        select ct.SoTien).ToList();//.Sum().GetValueOrDefault();
                //     objLDV.ThanhTien = tam.Sum().GetValueOrDefault();
                    
                //   // objLDV.SoLuong = tam.Count;
                //}
               // else
               // {
                    objLDV.ChiSoCu = objNuoc.ChiSoCu;
                    objLDV.ChiSoMoi = objNuoc.ChiSoMoi;
                    objLDV.ThanhTien = objNuoc.TienTT.GetValueOrDefault();
                    objLDV.SoLuong = string.Format("{0}",objNuoc.SoTieuThu.GetValueOrDefault());
              //  }

                //var ltCT = (from ct in db.dvNuocChiTiets
                //            join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                //            where ct.MaNuoc == objNuoc.ID
                //            orderby dm.STT
                //            select new
                //            {
                //                dm.TenDM,
                //                ct.SoLuong,
                //                ct.DonGia,dm.STT,
                //                ct.ThanhTien,
                //            }).AsEnumerable().Select((p, index) => new
                //            {
                //                STTa=index+1,
                //                p.TenDM,
                //                p.SoLuong,
                //                p.DonGia,
                //                p.STT,
                //                p.ThanhTien,
                //            }).ToList();
               
                //foreach (var n in ltCT)
                //{
                //    objLDV.SoLuong += n.SoLuong;
                //    var objGB = new DichVuBacHa();
                //    objGB.TenLDV ="Phí sử dụng nước"
                //        ;//n.TenDM;
                //    objGB.SoLuong = n.SoLuong;
                //    objGB.DonGia = n.DonGia;
                //    objGB.ThanhTien = n.ThanhTien;
                //    objGB.TenDVT = "M3";
                //    objGB.STT = n.STTa;
                //    objGB.ChiSoCu = n.STT == 1 ? 1 : (n.STT == 2 ? 10 : (n.STT == 3 ? 20 : (n.STT == 4 ? 30 : 0)));
                //    objGB.ChiSoMoi = n.STT == 1 ? (objNuoc.SoTieuThu < 10 ? objNuoc.SoTieuThu : 10) : (n.STT == 2 ? (objNuoc.SoTieuThu < 20 ? objNuoc.SoTieuThu : 20) : (n.STT == 3 ? (objNuoc.SoTieuThu < 30 ? objNuoc.SoTieuThu : 30) : (n.STT == 4 ? objNuoc.SoTieuThu : 0)))
                //    ;
                //    // (n.STT == 2 ? 20 : (n.STT == 3 ? 30 : (n.STT == 4 ? objNuoc.SoTieuThu : 0)));
                //    objLDV.Details.Add(objGB);

                //}
            }
            catch { }
        }

        void Load_DienKhoan(DichVuBacHa objLDV, int? Thang, int? Nam)
        {
            try
            {
                var ltDien = (from //ctpt in db.ptChiTietPhieuThus
                                  //join 
                                  hd in db.dvHoaDons //on ctpt.LinkID equals hd.ID
                              join td in db.dvDien3Phas on hd.LinkID  equals  (int?)td.ID 
                              join ct in db.dvDien3PhaChiTiets on td.ID equals ct.MaDien
                              join dm in db.dvDien3PhaDinhMucs on ct.MaDM equals dm.ID
                              join mb in db.mbMatBangs on td.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              where hd.MaKH == this.MaKh & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                              orderby mb.MaSoMB, dm.STT
                              select new
                              {
                                  td.SoTieuThu,
                                  TienDien = td.ThanhTien,
                                  ct.ChiSoCu,
                                  ct.ChiSoMoi,
                                  dm.TenDM,
                                  ct.SoLuong,
                                  dm.STT,
                                  ct.DonGia,
                                  ct.ThanhTien,
                                  TongTien = td.ThanhTien + td.TienVAT,
                                  td.TienTT,
                              }).FirstOrDefault();

                //if (ltDien == null)
                //{
                //    objLDV.ThanhTien = (from ct in db.ptChiTietPhieuThus
                //                        join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                //                        where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 11
                //                        select ct.SoTien).Sum().GetValueOrDefault();
                //}
                //else
                // {
                objLDV.ChiSoCu = ltDien.ChiSoCu;
                objLDV.ChiSoMoi = ltDien.ChiSoMoi;
                objLDV.ThanhTien = ltDien.TienTT.GetValueOrDefault();
                objLDV.SoLuong = string.Format("{0:#,0.##} kwh", ltDien.SoTieuThu.GetValueOrDefault());
                // }

                //foreach (var d in ltDien)
                //{

                //    objLDV.SoLuong += d.SoLuong;
                //    var objGB = new DichVuBacHa();
                //    objGB.TenLDV = d.TenDM;
                //    objGB.SoLuong = d.SoLuong;
                //    objGB.DonGia = d.DonGia;
                //    objGB.ThanhTien = d.ThanhTien;
                //    objGB.TenDVT = "kWh";
                //    objGB.ChiSoCu = d.STT==1 ? 1 : (d.STT==2 ? 10 : (d.STT==3 ? 20 : (d.STT==4 ?30 : 0)));
                //    objGB.ChiSoMoi = d.STT == 10 ? 1 : (d.STT == 2 ? 20 : (d.STT == 3 ? 30 : (d.STT == 4 ? d.SoTieuThu : 0)));
                //    objLDV.Details.Add(objGB);

                //}
            }
            catch { }
        }
        void Load_Dien(DichVuBacHa objLDV, int? Thang, int? Nam)
        {
            try
            {
                var ltDien = (from //ctpt in db.ptChiTietPhieuThus
                              //join 
                                  hd in db.dvHoaDons //on ctpt.LinkID equals hd.ID
                              join td in db.dvDiens on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDien", LinkID = (int?)td.ID }
                              join ct in db.dvDienChiTiets on td.ID equals ct.MaDien
                              join dm in db.dvDienDinhMucs on ct.MaDM equals dm.ID
                              join mb in db.mbMatBangs on td.MaMB equals mb.MaMB
                              join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                              where hd.MaKH==this.MaKh & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                              orderby mb.MaSoMB, dm.STT
                              select new
                              {
                                  td.SoTieuThu,
                                  TienDien = td.ThanhTien,td.ChiSoCu,td.ChiSoMoi,
                                  dm.TenDM,
                                  ct.SoLuong,dm.STT,
                                  ct.DonGia,
                                  ct.ThanhTien,
                                  TongTien = td.ThanhTien + td.TienVAT,
                                  td.TienTT,
                              }).FirstOrDefault();

                //if (ltDien == null)
                //{
                //    objLDV.ThanhTien = (from ct in db.ptChiTietPhieuThus
                //                        join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                //                        where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 11
                //                        select ct.SoTien).Sum().GetValueOrDefault();
                //}
                //else
               // {
                    objLDV.ChiSoCu = ltDien.ChiSoCu;
                    objLDV.ChiSoMoi = ltDien.ChiSoMoi;
                    objLDV.ThanhTien = ltDien.TienTT.GetValueOrDefault();
                    objLDV.SoLuong = string.Format("{0:#,0.##} kwh",ltDien.SoTieuThu.GetValueOrDefault());
               // }

                //foreach (var d in ltDien)
                //{
        
                //    objLDV.SoLuong += d.SoLuong;
                //    var objGB = new DichVuBacHa();
                //    objGB.TenLDV = d.TenDM;
                //    objGB.SoLuong = d.SoLuong;
                //    objGB.DonGia = d.DonGia;
                //    objGB.ThanhTien = d.ThanhTien;
                //    objGB.TenDVT = "kWh";
                //    objGB.ChiSoCu = d.STT==1 ? 1 : (d.STT==2 ? 10 : (d.STT==3 ? 20 : (d.STT==4 ?30 : 0)));
                //    objGB.ChiSoMoi = d.STT == 10 ? 1 : (d.STT == 2 ? 20 : (d.STT == 3 ? 30 : (d.STT == 4 ? d.SoTieuThu : 0)));
                //    objLDV.Details.Add(objGB);

                //}
            }
            catch { }
        }

        void Load_TienThue(DichVuBacHa objLDV, int? Thang, int? Nam)
        {
            try
            {
                var ltData = (from ctpt in db.ptChiTietPhieuThus
                              join hd in db.dvHoaDons on ctpt.LinkID equals hd.ID
                              join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                              join hdct in db.ctHopDongs on ltt.MaHD equals hdct.ID
                              where ctpt.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                              select new
                              {
                                  ltt.DienGiai,
                                  SoLuong = db.ctChiTiets.Where(o => o.MaHDCT == ltt.MaHD).Sum(o => o.DienTich).GetValueOrDefault(),
                                  HeSo = hd.KyTT,
                                  ltt.TuNgay,
                                  ltt.DenNgay,
                                  hd.KyTT,
                                  hd.NgayTT,
                                  ltt.SoTienQD,
                                  HanTT = ltt.TuNgay.Value.AddDays(hdct.HanTT.GetValueOrDefault()),
                              }).AsEnumerable()
                                .Select((p, Index) => new
                                {
                                    p.DienGiai,
                                    p.SoLuong,
                                    p.HeSo,
                                    p.TuNgay,
                                    p.DenNgay,
                                    p.KyTT,
                                    //p.NgayTT,
                                    DonGia = (p.SoTienQD / p.SoLuong) / p.HeSo,
                                    ThanhTien = p.SoTienQD,
                                    NgayHanTT = p.HanTT,
                                }).ToList();

                if (ltData == null)
                {
                    objLDV.ThanhTien = (from ct in db.ptChiTietPhieuThus
                                        join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                        where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 2
                                        select ct.SoTien).Sum().GetValueOrDefault();
                }
    
                    foreach (var i in ltData)
                    {
                        objLDV.SoLuong += i.SoLuong;
                        objLDV.ThanhTien += i.ThanhTien;
                        var objGB = new DichVuBacHa();
                        objGB.TenLDV = objLDV.TenLDV;
                        objGB.SoLuong = string.Format("{0:#,0.##}",i.SoLuong);
                        objGB.DonGia = i.DonGia;
                        objGB.ThanhTien = i.ThanhTien;
                        objLDV.Details.Add(objGB);
                    }
            }
            catch { }
        }

        void Load_NuocSinhHoat(DichVuBacHa objLDV, int? Thang, int? Nam)
        {
            try
            {
                var objNuoc = (from ctpt in db.ptChiTietPhieuThus
                               join hd in db.dvHoaDons on ctpt.LinkID equals hd.ID
                               join tn in db.dvNuocSinhHoats on new { hd.TableName, hd.LinkID } equals new { TableName = "dvNuocSinhHoat", LinkID = (int?)tn.ID }
                               where ctpt.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                               select new
                               {
                                   tn.ID,
                                   tn.ChiSoCu,
                                   tn.ChiSoMoi,
                                   tn.SoTieuThuNL,
                                   tn.DauCap_Cu,
                                   tn.DauCap_Moi,
                                   tn.DauHoi_Cu,
                                   tn.DauHoi_Moi,
                                   tn.SoTieuThuNN,
                                   tn.SoTieuThu,
                                   hd.ConNo,
                                   ThueVAT = (hd.ConNo * 100 / 115) * 5 / 100,
                                   PhiMT = (hd.ConNo * 100 / 115) * 10 / 100,
                                   TienNuoc = hd.ConNo * 100 / 115,
                                   tn.TienTT,
                               }).First();

                if (objNuoc == null)
                {
                    objLDV.ThanhTien = (from ct in db.ptChiTietPhieuThus
                                        join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                        where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 22
                                        select ct.SoTien).Sum().GetValueOrDefault();
                }
                else
                {
                    objLDV.ThanhTien = objNuoc.TienTT;
                }


                var ltNuoc = (from ct in db.dvNuocSinhHoatChiTiets
                    join dm in db.dvNuocDinhMucs on ct.MaDM equals dm.ID
                    where ct.MaNuoc == objNuoc.ID
                    orderby dm.STT
                    select new
                    {
                        dm.TenDM,
                        ct.SoLuong,
                        ct.DonGia,
                        ct.ThanhTien,
                        ct.DienGiai,
                    }).AsEnumerable().Select((p, index) => new
                    {
                        STT=index+1,
                        p.TenDM,
                        p.SoLuong,
                        p.DonGia,
                        p.ThanhTien,
                        p.DienGiai,
                    }).ToList();

                foreach (var n in ltNuoc)
                {
                    objLDV.SoLuong += n.SoLuong;
                    var objGB = new DichVuBacHa();
                    objGB.STT = n.STT;
                    objGB.TenLDV = n.TenDM;
                    objGB.SoLuong = string.Format("0:#,0.##",n.SoLuong);
                    objGB.DonGia = n.DonGia;
                    objGB.ThanhTien = n.ThanhTien;
                    objGB.TenDVT = "M3";
                    objLDV.Details.Add(objGB);
                }
            }
            catch { }
        }


        void Load_DieuHoa(DichVuBacHa objLDV, int? Thang, int? Nam)
        {
            try
            {
                var ltData = (from ctpt in db.ptChiTietPhieuThus
                              join hd in db.dvHoaDons on ctpt.LinkID equals hd.ID
                              join dv in db.dvDienDHs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDienDH", LinkID = (int?)dv.ID }
                              join ct in db.dvDienDH_ChiTiets on dv.ID equals ct.MaDien
                              join dm in db.dvDienDH_DinhMucs on ct.MaDM equals dm.ID
                              join mb in db.mbMatBangs on dv.MaMB equals mb.MaMB
                              where ctpt.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                              select new
                              {
                                  dv.NgayTT,
                                  dv.HeSo,
                                  dv.ChiSoCu,
                                  dv.ChiSoMoi,
                                  dv.SoTieuThu,

                                  dm.TenDM,CTCSC=ct.ChiSoCu,CTCSM=ct.ChiSoMoi,
                                  hd.ID,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien,
                                  dv.TienTT,
                              }).ToList();

                if (ltData == null)
                {
                    objLDV.ThanhTien = (from ct in db.ptChiTietPhieuThus
                                        join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                        where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 2
                                        select ct.SoTien).Sum().GetValueOrDefault();
                }
                else
                {
                    objLDV.ThanhTien = ltData.First().TienTT;
                }

                foreach (var i in ltData)
                {
                    objLDV.SoLuong += i.SoLuong;
                    var objGB = new DichVuBacHa();
                    objGB.TenLDV = i.TenDM;
                    objGB.SoLuong = string.Format("{0:#,0.##}",i.SoLuong);
                    objGB.DonGia = i.DonGia;
                    objGB.ThanhTien = i.ThanhTien;
                    objGB.TenDVT = "kWh";
                    objLDV.Details.Add(objGB);
                }
            }
            catch { }

        }

        void Load_GAS(DichVuBacHa objLDV, int? Thang, int? Nam)
        {
            var ltGAS = (from ctpt in db.ptChiTietPhieuThus
                         join hd in db.dvHoaDons on ctpt.LinkID equals hd.ID
                         from g in db.dvGas
                         join dvt in db.dvgDonViTinhs on g.MaDVT equals dvt.ID
                         where ctpt.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam
                         select new
                         {
                             g.ID,
                             g.ChiSoCu,
                             g.ChiSoMoi,
                             SoTieuThu = g.ChiSoMoi - g.ChiSoCu,
                             dvt.TenDVT,
                             HeSo = g.TyLe,
                             g.ThanhTien,
                             g.DienGiai,
                             g.TienTT,
                         }).First();

            if (ltGAS == null)
            {
                objLDV.ThanhTien = (from ct in db.ptChiTietPhieuThus
                                    join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                    where ct.MaPT == _MaPT & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 10
                                    select ct.SoTien).Sum().GetValueOrDefault();

            }
            else
            {
                objLDV.ThanhTien = ltGAS.TienTT;
            }

                var ltData = (from ct in db.dvGasChiTiets
                              join dm in db.dvGasDinhMucs on ct.MaDM equals dm.ID
                              where ct.MaGas == ltGAS.ID
                              orderby dm.STT
                              select new
                              {
                                  dm.TenDM,
                                  ct.SoLuong,
                                  ct.DonGia,
                                  ct.ThanhTien
                              }).ToList();

                foreach (var i in ltData)
                {
                    objLDV.SoLuong += i.SoLuong;
                    var objGB = new DichVuBacHa();
                    objGB.TenLDV = i.TenDM;
                    objGB.SoLuong = string.Format("0:#,0.##" ,
                                                  i.SoLuong);
                    objGB.DonGia = i.DonGia;
                    objGB.ThanhTien = i.ThanhTien;
                    objGB.TenDVT = "M3";
                    objLDV.Details.Add(objGB);
                }
        }



        void Load_DichVuCoBan(DichVuBacHa objLDV, int? Thang, int Nam)
        {
            try
            {
                var ltData = (from //ctpt in db.ptChiTietPhieuThus
                              //join 
                                  hd in db.dvHoaDons //on ctpt.LinkID equals hd.ID
                             // join dv in db.dvDichVuKhacs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDichVuKhac", LinkID = (int?)dv.ID }
                             // join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                              where hd.MaKH == this.MaKh & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == objLDV.MaLDV
                              select new
                              {
                                  hd.DienGiai,
                                  SoLuong=1,
                                  TenDVT="",
                                  DonGia = hd.PhaiThu,
                                  ThanhTien = hd.PhaiThu
                              }).AsEnumerable().Select((p, index) => new
                              {
                                  p.DienGiai,
                                  p.SoLuong,
                                  p.TenDVT,
                                  p.DonGia,
                                  p.ThanhTien,STT=index+1,
                              }).ToList();
                if (ltData.Count == 0)
                {
                    var tam = (from ct in db.ptChiTietPhieuThus
                               join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                               where ct.MaPT == _MaPT & (hd.MaLDV == 13 | hd.MaLDV == 45)
                               select ct.SoTien).ToList();//.Sum().GetValueOrDefault();
                    objLDV.ThanhTien = tam.Sum().GetValueOrDefault();
                   // objLDV.SoLuong = tam.Count;
                }
                else
                {
                    if (ltData.Count == 1)
                    {
                        if (ltData[0].SoLuong > 0 & ltData[0].DonGia > 0)
                        {
                            objLDV.SoLuong = string.Format("{0} ",1);
                            objLDV.TenDVT = ltData[0].TenDVT.ToString().ToUpper();
                            objLDV.DonGia = ltData[0].DonGia;
                            objLDV.ThanhTien = ltData[0].ThanhTien;
                        }
                    }
                    else if (ltData.Count > 1)
                    {
                        //foreach (var i in ltData)
                        //{
                        //    var objGB = new DichVuBacHa();
                        //    objGB.TenLDV = i.DienGiai;
                        //    if (i.SoLuong > 0 & i.DonGia > 0)
                        //    {
                        //        objLDV.SoLuong += i.SoLuong;
                        //        objLDV.ThanhTien += i.ThanhTien;
                        //        objGB.SoLuong = i.SoLuong;

                        //        objGB.DonGia = i.DonGia;
                        //    }
                        //    objGB.ThanhTien = i.ThanhTien;
                        //    objGB.TenDVT = i.TenDVT.ToLower();
                        //    objLDV.Details.Add(objGB);
                        //}
                    }
                }
                
            }
            catch { }
        }
        void Load_PQL(DichVuBacHa objLDV, int? Thang, int Nam)
        {
            try
            {
                var ltData = (from //ctpt in db.ptChiTietPhieuThus
                                  //join 
                                  hd in db.dvHoaDons //on ctpt.LinkID equals hd.ID
                              join dv in db.dvDichVuKhacs on new { hd.TableName, hd.LinkID } equals new { TableName = "dvDichVuKhac", LinkID = (int?)dv.ID }
                              join dvt in db.DonViTinhs on dv.MaDVT equals dvt.ID
                              where hd.MaKH == this.MaKh & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam & hd.MaLDV == 13
                                   & (hd.PhaiThu.GetValueOrDefault() -
                              (from ct in db.ptChiTietPhieuThus
                               join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                               //join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                               where hd.ID == ct.LinkID & SqlMethods.DateDiffDay(hd.NgayTT, pt.NgayThu) < 0
                               select ct.SoTien).Sum().GetValueOrDefault())
                               - (from ct in db.ktttChiTiets
                                  join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                  // join hd1 in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd1.ID }
                                  where ct.LinkID == hd.ID// & SqlMethods.DateDiffMonth(pt.NgayCT, _TuNgay) == 0
                                  select ct.SoTien).Sum().GetValueOrDefault()
                               > 0  //moi mo 5/11/2016
                                 
                              select new
                              {
                                  dv.DienGiai,
                                  dv.SoLuong,
                                  dvt.TenDVT,
                                  DonGia = dv.DonGia * dv.TyGia,
                                  ThanhTien = dv.TienTTQD
                              }).AsEnumerable().Select((p, index) => new
                              {
                                  p.DienGiai,
                                  p.SoLuong,
                                  p.TenDVT,
                                  p.DonGia,
                                  p.ThanhTien,
                                  STT = index + 1,
                              }).ToList();
                if (ltData.Count == 0)
                {
                    var tam = (from ct in db.ptChiTietPhieuThus
                               join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                               where ct.MaPT == _MaPT & (hd.MaLDV == 13 | hd.MaLDV == 45) & hd.NgayTT.Value.Month == Thang & hd.NgayTT.Value.Year == Nam 
                               select ct.SoTien).ToList();//.Sum().GetValueOrDefault();
                    objLDV.ThanhTien = tam.Sum().GetValueOrDefault();
                    // objLDV.SoLuong = tam.Count;
                }
                else
                {
                    if (ltData.Count == 1)
                    {
                        if (ltData[0].SoLuong > 0 & ltData[0].DonGia > 0)
                        {
                            objLDV.SoLuong = string.Format("{0:#,0.##} {1}",ltData[0].SoLuong, objLDV.MaLDV == 13 ? ltData[0].TenDVT.ToUpper() : "");
                            objLDV.TenDVT = ltData[0].TenDVT.ToString().ToUpper();
                            objLDV.DonGia = ltData[0].DonGia;
                            objLDV.ThanhTien = ltData[0].ThanhTien;
                        }
                    }
                    else if (ltData.Count > 1)
                    {
                        //foreach (var i in ltData)
                        //{
                        //    var objGB = new DichVuBacHa();
                        //    objGB.TenLDV = i.DienGiai;
                        //    if (i.SoLuong > 0 & i.DonGia > 0)
                        //    {
                        //        objLDV.SoLuong += i.SoLuong;
                        //        objLDV.ThanhTien += i.ThanhTien;
                        //        objGB.SoLuong = i.SoLuong;

                        //        objGB.DonGia = i.DonGia;
                        //    }
                        //    objGB.ThanhTien = i.ThanhTien;
                        //    objGB.TenDVT = i.TenDVT.ToLower();
                        //    objLDV.Details.Add(objGB);
                        //}
                    }
                }

            }
            catch { }
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (DetailReport.GetCurrentRow() is DichVuBacHa)
                {
                    var objLDV = DetailReport.GetCurrentRow() as DichVuBacHa;
                    if (objLDV.Details.Count > 0)
                    {
                        DetailReport1.DataSource = objLDV.Details;
                        DetailReport1.Visible = true;
                    }
                    else
                    {
                        DetailReport1.Visible = false;
                    }
                }
            }
            catch { }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var objLDV = this.GetCurrentRow() as GiayBaoBacHa;
                DetailReport.DataSource = objLDV.DichVus;
                TongTien = objLDV.DichVus.Sum(o => o.ThanhTien).GetValueOrDefault();
            }
            catch { }
        }


        class GiayBaoBacHa
        {
            public DateTime? NgayIn { get; set; }
            public int Thang { get; set; }
            public int Nam { get; set; }
            public string ThangTA { get; set; }
            public string ThangTB { get; set; }
            public string TenCongTy { get; set; }
            public string TenToaNha { get; set; }
            public string TenTN { get; set; }
            public string TenCty { get; set; }
            public string DiaChiCty { get; set; }
            public string LoGoUrl { get; set; }
            public string TangLau { get; set; }
            public string Toa { get; set; }
            public string MaSoMB { get; set; }
            public string TenTL { get; set; }
            public string TenKN { get; set; }
            public string MaSoKH { get; set; }
            public string MaPhuKH { get; set; }
            public string TenKH { get; set; }
            public string DiaChiKH { get; set; }

            public string SoTKNH { get; set; }
            public string ChuTKNH { get; set; }
            public string TenNH { get; set; }

            public decimal? PhatSinh { get; set; }
            public decimal? NoDauKy { get; set; }
            public decimal? TienLai { get; set; }
            public decimal? NoVaLai { get; set; }
            public decimal? ThuTruoc { get; set; }
            public decimal? TongTien { get; set; }
            public string TongTienBangChu { get; set; }

            public List<DichVuBacHa> DichVus { get; set; }
        }

        class DichVuBacHa
        {
            public int STT { get; set; }
            public int MaLDV { get; set; }
            public string TenLDV { get; set; }
            public decimal? ChiSoCu { get; set; }
            public decimal? ChiSoMoi { get; set; }
            public string SoLuong { get; set; }
            public string TenDVT { get; set; }
            public decimal? HeSo { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? ThanhTien { get; set; }
            public string HanTT { get; set; }
            public DateTime? NgayTT { get; set; }
            public decimal? SoThang { get; set; }
            public string LoaiMB { get; set; }
            public List<DichVuBacHa> Details { get; set; }
        }

        class GiuXeBacHa
        {
            public int? MaLX { get; set; }
            public string TenLX { get; set; }
            public string BienSo { get; set; }
            public int? SoLuong { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? ThanhTien { get; set; }
            public string DVT { get; set; }
        }
        int _STTGroup = 0;
        private void cSTT_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
             
       
            _STTGroup++;
            var _Cell = (XRTableCell)sender;
            _Cell.Text = RomanNumerals.ToRoman(_STTGroup);
     
        }



    }
}
