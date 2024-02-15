using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace LandSoftBuilding.Fund.Input
{
    public partial class rptPhieuThu257 : DevExpress.XtraReports.UI.XtraReport
    {
        public int _ID { get; set; }
        public int _MaTN { get; set; }
        public rptPhieuThu257(int ID, byte MaTN, int Lien)
        {
            _ID = ID;
            _MaTN = MaTN;
            InitializeComponent();

            //Library.frmPrintControl.LoadLayout(this, 3, MaTN);

            if (ID == 0) return;

            var db = new Library.MasterDataContext();
            try
            {
                #region Thong tin toa nha
                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
                cTenTN.Text = cTenCty2.Text = cTenCty3.Text = objTN.CongTyQuanLy;
                cDiaChiTN.Text = cDiaChiCty2.Text = cDiaChiCty3.Text = objTN.DiaChiCongTy;
                //cDienThoaiTN.Text = "Tel: " + objTN.DienThoai;
                xrPictureBox2.ImageUrl = xrPictureBox1.ImageUrl = picLogo.ImageUrl = objTN.Logo;
                #endregion

                var objTien = new TienTeCls();
                var objPT = (from p in db.ptPhieuThus
                             join nv in db.tnNhanViens on p.MaNVN equals nv.MaNV
                             where p.ID == ID
                             select new
                             {
                                 p.MaTN,
                                 p.SoPT,
                                 p.NgayThu,
                                 p.NguoiNop,
                                 p.DiaChiNN,
                                 p.LyDo,
                                 p.SoTien,
                                 SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn"),
                                 nv.HoTenNV,
                                 //p.IsDienGiaiPT
                             }).FirstOrDefault();
                //cLien.Text = string.Format("(Liên {0})", Lien);
                cSoPT3.Text = cSoPT2.Text = cSoPT.Text = "Số phiếu: " + objPT.SoPT;
                cNgayPT3.Text = cNgayPT2.Text = cNgayPT.Text = string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", objPT.NgayThu);
                cNguoiNop3.Text = cNguoiNop2.Text = cNguoiNop.Text = objPT.NguoiNop;
                cDiaChi3.Text = cDiaChi2.Text = cDiaChi.Text = objPT.DiaChiNN;
                cSoTien3.Text = cSoTien2.Text = cSoTien1.Text = string.Format("{0}", objPT.SoTien.Value.ToString("c0"));
                cSoTienBC3.Text = cSoTienBC2.Text = cSoTienBC1.Text = objPT.SoTien_BangChu;
                //cNguoiLap.Text = objPT.HoTenNV;
                cDaNhanDuTien3.Text = cDaNhanDuTien2.Text = cDaNhanDuTien.Text = string.Format("Đã nhận đủ tiền (ghi bằng chữ) {0}", objPT.SoTien_BangChu);
                //Dien giai
                var strDienGiai = "";
                var ltChiTiet = (from ct in db.ptChiTietPhieuThus
                                 where ct.MaPT == ID
                                 select new
                                 {
                                     ct.DienGiai,
                                     ct.SoTien
                                 }).ToList();
                if (_MaTN != 27)
                //cLyDo.Text = 
                {
                    //if (objPT.IsDienGiaiPT.GetValueOrDefault()
                    //    == true)
                    //{
                    //    cLyDo.Text = objPT.LyDo;
                    //    cLyDo2.Text = objPT.LyDo;
                    //    cLyDo3.Text = objPT.LyDo;
                    //}
                    //else
                    //{
                        test();
                        test1();
                   // }

                }// GetDienGiaiThangLong();
                else
                {
                    if (_MaTN == 39 | _MaTN == 40)
                    {
                        cLyDo.Text = objPT.LyDo;
                    }
                    else
                    {
                        foreach (var i in ltChiTiet)
                        {
                            strDienGiai += i.DienGiai + string.Format(" ({0:#,0}đ)", i.SoTien) + "; ";
                        }

                        cLyDo.Text = strDienGiai.Trim().Trim(';');
                    }
                }

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
        string GetDienGiaiThangLong()
        {
            var db = new Library.MasterDataContext();
            string strDienGiai = "";
            try
            {

                var ltData = (from hd in db.dvHoaDons
                              join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                              join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                              from tx in the.DefaultIfEmpty()
                              where ptct.MaPT == _ID
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
                                                   where ldv.ID == i.MaLDV & ptct.MaPT == _ID & ptct.DienGiai.Contains(i.BienSo.ToString())
                                                   group hd by new { hd.NgayTT.Value.Month, hd.NgayTT.Value.Year } into gr
                                                   orderby gr.Max(p => p.NgayTT)
                                                   select gr.Max(p => p.NgayTT)).ToList() : (from hd in db.dvHoaDons
                                                                                             join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                                                                                             join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                                                                                             //join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                                                                                             //from tx in the.DefaultIfEmpty()
                                                                                             where ldv.ID == i.MaLDV & ptct.MaPT == _ID //& ptct.DienGiai.Contains(i.BienSo.ToString())
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
                                if (i.MaLDV == 8 | i.MaLDV == 22)
                                {
                                    if (ltDV[_Start].Value.Year != ltDV[j].Value.Year)
                                        strTime += string.Format("T{0:MM/yyyy}-T{1:MM/yyyy},", ltDV[_Start].Value.AddMonths(-1));
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
                                if (i.MaLDV == 8 | i.MaLDV == 22)
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
                        strDienGiai += string.Format("{0} {1} ({2}) , ", i.TenLDV, tam.BienSo, strTime);
                    }
                    if (ltLDVXe.Count == 0)
                    {
                        strDienGiai += string.Format("{0} ({1}), ", i.TenLDV, strTime);
                    }
                }
            }
            catch { }

            return strDienGiai.Trim().TrimEnd(',');
        }

        void test()
        {
            MasterDataContext db = new MasterDataContext();
            var ltData = (from hd in db.dvHoaDons
                          join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                          join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                          join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                          from tx in the.DefaultIfEmpty()

                          //join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID into nhom
                          //from nhomxe in nhom.DefaultIfEmpty()
                          where ptct.MaPT == _ID & hd.MaLDV != 6 & hd.LinkID != null

                          group hd by new { hd.MaLDV, ldv.TenLDV, tx.BienSo, Thang = hd.NgayTT.Value.Month, Nam = hd.NgayTT.Value.Year, } into gr
                          orderby gr.Key.Nam, gr.Key.Thang
                          select new GiuXeItem2()
                          {

                              BienSo = "",
                              MaLDV = gr.Key.MaLDV,
                              TenLDV = gr.Key.TenLDV,
                              Thang = gr.Key.Thang,
                              Nam = gr.Key.Nam,

                          }).ToList();
            var ltDataNo = (from hd in db.dvHoaDons
                            join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
                            join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                            join tx in db.dvgxTheXes on hd.LinkID equals tx.ID into the
                            from tx in the.DefaultIfEmpty()

                            //join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID into nhom
                            //from nhomxe in nhom.DefaultIfEmpty()
                            where ptct.MaPT == _ID & hd.LinkID == null
                            //group hd by new { hd.MaLDV, ldv.TenLDV, tx.BienSo, Thang = hd.NgayTT.Value.Month, Nam = hd.NgayTT.Value.Year, } into gr
                            select new GiuXeItem5()
                            {
                                //                                                            .Sum(p => p.SoTien)).GetValueOrDefault()),
                                DienGiai = hd.DienGiai,
                                TenLDV = ldv.TenLDV
                            }).ToList();
            foreach (var i in ltDataNo)
            {
                if (cLyDo.Text == "")
                {
                    cLyDo3.Text = cLyDo2.Text = cLyDo.Text += i.TenLDV + " " + i.DienGiai + ", ";
                }
                else
                {
                    cLyDo3.Text = cLyDo2.Text = cLyDo.Text += i.TenLDV + " " + i.DienGiai + ", ";
                }
            }
            string Quy = "";
            string QuyNew = ",";
            string TenLDV = "";
            string TenNew = ",";
            string ThangCu = "";
            string ThangMoi = ",";
            foreach (var i in ltData)
            {


                if (i.Thang >= 1 & i.Thang <= 3)
                {
                    Quy = "Quý I" + "/" + i.Nam; string tam = "";

                    TenLDV = i.TenLDV;
                    ThangCu = i.Thang.ToString();

                    if (Quy == QuyNew)
                    {

                        if (TenLDV == TenNew)
                        {

                            if (ThangCu == ThangMoi)
                            {

                            }
                            else
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }

                            }

                        }

                        else
                        {
                            tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += i.TenLDV + string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }
                            }
                            else
                            {
                                i.BienSo += i.TenLDV + " " + Quy + ", ";
                            }


                        }




                    }
                    else
                    {
                        tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                        if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                        {
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                    i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                else
                                    i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                            }
                        }
                        else
                        {
                            i.BienSo += i.TenLDV + " " + Quy + ", ";
                        }
                    }
                    QuyNew = Quy;
                    TenNew = TenLDV;
                    ThangMoi = ThangCu;
                }
                if (i.Thang >= 4 & i.Thang <= 6)
                {
                    Quy = "Quý II" + "/" + i.Nam; ; string tam = "";

                    TenLDV = i.TenLDV;
                    ThangCu = i.Thang.ToString();


                    if (Quy == QuyNew)
                    {

                        if (TenLDV == TenNew)
                        {

                            if (ThangCu == ThangMoi)
                            {

                            }
                            else
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }

                            }

                        }

                        else
                        {
                            tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += i.TenLDV + string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }
                            }
                            else
                            {
                                i.BienSo += i.TenLDV + " " + Quy + ", ";
                            }


                        }




                    }
                    else
                    {
                        tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                        if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                        {
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                    i.BienSo += i.TenLDV + string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                else
                                    i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                            }
                        }
                        else
                        {
                            i.BienSo += i.TenLDV + " " + Quy + ", ";
                        }
                    }
                    QuyNew = Quy;
                    TenNew = TenLDV;
                    ThangMoi = ThangCu;
                }
                if (i.Thang >= 7 & i.Thang <= 9)
                {
                    Quy = "Quý III" + "/" + i.Nam; ; string tam = "";

                    TenLDV = i.TenLDV;
                    ThangCu = i.Thang.ToString();

                    if (Quy == QuyNew)
                    {

                        if (TenLDV == TenNew)
                        {

                            if (ThangCu == ThangMoi)
                            {

                            }
                            else
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }

                            }

                        }

                        else
                        {
                            tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += i.TenLDV + string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }
                            }
                            else
                            {
                                i.BienSo += i.TenLDV + " " + Quy + ", ";
                            }


                        }




                    }
                    else
                    {
                        tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                        if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                        {
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                    i.BienSo += i.TenLDV + string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                else
                                    i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                            }
                        }
                        else
                        {
                            i.BienSo += i.TenLDV + " " + Quy + ", ";
                        }
                    }
                    QuyNew = Quy;
                    TenNew = TenLDV;
                    ThangMoi = ThangCu;


                }
                if (i.Thang >= 10 & i.Thang <= 12)
                {
                    Quy = "Quý IV" + "/" + i.Nam;
                    ; string tam = "";
                    TenLDV = i.TenLDV;
                    ThangCu = i.Thang.ToString();

                    if (Quy == QuyNew)
                    {

                        if (TenLDV == TenNew)
                        {

                            if (ThangCu == ThangMoi)
                            {

                            }
                            else
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += string.Format("tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }

                            }

                        }

                        else
                        {
                            tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                                {
                                    if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                        i.BienSo += i.TenLDV + string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                    else
                                        i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                                }
                            }
                            else
                            {
                                i.BienSo += i.TenLDV + " " + Quy + ", ";
                            }


                        }




                    }
                    else
                    {
                        tam += string.Format("tháng {0}/{1}", i.Thang, i.Nam);
                        if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                        {
                            if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11  )
                            {
                                if (i.MaLDV == 9 | i.MaLDV == 8 | i.MaLDV == 5 | i.MaLDV == 11 )
                                    i.BienSo += i.TenLDV + string.Format("tháng {0}/{1}", i.Thang - 1 == 0 ? 12 : i.Thang - 1, i.Thang - 1 == 0 ? i.Nam - 1 : i.Nam) + ", ";
                                else
                                    i.BienSo += i.TenLDV + string.Format(" tháng {0}/{1}", i.Thang, i.Nam) + ", ";
                            }
                        }
                        else
                        {
                            i.BienSo += i.TenLDV + " " + Quy + ", ";
                        }
                    }
                    QuyNew = Quy;
                    TenNew = TenLDV;
                    ThangMoi = ThangCu;
                }
                //i.BienSo = i.BienSo.Trim(' ').Trim(';');
                cLyDo.Text += string.Format("{0}", i.BienSo);
                cLyDo2.Text += string.Format("{0}", i.BienSo);
                cLyDo3.Text += string.Format("{0}", i.BienSo);
            }
            cLyDo.Text = cLyDo.Text.Length > 0 ? cLyDo.Text.Remove(cLyDo.Text.Length - 2) : "";
            cLyDo2.Text = cLyDo2.Text.Length > 0 ? cLyDo2.Text.Remove(cLyDo2.Text.Length - 2) : "";
            cLyDo3.Text = cLyDo3.Text.Length > 0 ? cLyDo3.Text.Remove(cLyDo3.Text.Length - 2) : "";
        }

        void test1()
        {

       //     MasterDataContext db = new MasterDataContext();
       //     var ltGiuXete = (from tx in db.dvgxTheXes
       //                      join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX// into dslx
       //                      //from lx in dslx.DefaultIfEmpty()
       //                      join dv in db.dvHoaDons on tx.ID equals dv.LinkID
       //                      join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID
       //                      join ptct in db.ptChiTietPhieuThus on dv.ID equals ptct.LinkID
       //                      join pt in db.ptPhieuThus on ptct.MaPT equals pt.ID
       //                      where pt.ID == _ID//& dv.NgayTT.Value.Month == _Thang 
       //                          // & dv.NgayTT.Value.Year == _Nam
       //                          //& nhomxe.TenNX=="o to"
       //                 & dv.IsDuyet == true & dv.IsHoaDonTaoTay.GetValueOrDefault() == false

       //                      group new { tx, dv } by new { MaNX = nhomxe.ID, nhomxe.TenNX, Thang = dv.NgayTT.Value.Month, Nam = dv.NgayTT.Value.Year } into gr
       //                      select new GiuXeItem4()
       //                      {
       //                          MaLX = gr.Key.MaNX,
       //                          TenLX = gr.Key.TenNX,
       //                          TenNX = gr.Key.TenNX,
       //                          BienSo = "",
       //                          SoLuong = gr.Count(),

       //                          Thang = gr.Key.Thang,
       //                          Nam = gr.Key.Nam,
       //                      }).ToList();
       //     if (ltGiuXete.Count > 0)
       //     {
       //         if (cLyDo.Text.Length > 0)
       //         {
       //             cLyDo.Text += ", Phí giữ xe  ";
       //             cLyDo2.Text += ", Phí giữ xe  ";
       //             cLyDo3.Text += ", Phí giữ xe  ";
       //         }
       //         else
       //         {
       //             // cLyDo.Text =", Phí giữ xe  "
       //         }


       //         string Quy = "";
       //         string QuyNew = ",";
       //         string ThangCu = "";
       //         string ThangMoi = ",";
       //         string XeCu = "";
       //         string XeMoi = ",";

       //         foreach (var i in ltGiuXete)
       //         {
       //             #region Xu ly
       //             var ltBienSo = (from tx in db.dvgxTheXes
       //                             join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX
       //                             join nhomxe in db.dvgxNhomXes on lx.MaNX equals nhomxe.ID
       //                             join hd in db.dvHoaDons on tx.ID equals hd.LinkID
       //                             join ptct in db.ptChiTietPhieuThus on hd.ID equals ptct.LinkID
       //                             join pt in db.ptPhieuThus on ptct.MaPT equals pt.ID
       //                             where tx.MaTN == _MaTN & pt.ID == _ID & nhomxe.ID == i.MaLX
       //                             & hd.MaLDV == 6 & hd.IsHoaDonTaoTay.GetValueOrDefault() == false
       //                             select new { TenLX = nhomxe.TenNX, LX = nhomxe.TenNX, SoLuong = i.SoLuong }).ToList().Take(1);

       //             if (i.Thang >= 1 & i.Thang <= 3)
       //             {

       //                 Quy = "Quý I" + "/" + i.Nam; ThangCu = i.Thang.ToString();
       //                 string tam = "";
       //                 XeMoi = i.TenNX;
       //                 foreach (var bs in ltBienSo)
       //                     if (!string.IsNullOrEmpty(bs.TenLX))
       //                     {
       //                         if (Quy == QuyNew)
       //                         {

       //                             if (i.TenNX == "o to")
       //                             {
       //                                 // if (XeCu != XeMoi)
       //                                 // {
       //                                 if (ThangCu != ThangMoi)
       //                                 {
       //                                     tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                 }
       //                                 else
       //                                 {
       //                                     tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 1 & p.Thang <= 3 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.LX;
       //                                 }

       //                                 // }

       //                             }
       //                             else
       //                             {

       //                                 if (XeCu != XeMoi)
       //                                 {
       //                                     //tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                     tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 1 & p.Thang <= 3 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                                 }

       //                             }

       //                         }
       //                         else
       //                         {
       //                             if (i.TenNX == "o to")
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                             }
       //                             else
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 1 & p.Thang <= 3 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                             }
       //                         }


       //                     }
       //                 if (Quy == QuyNew)
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         //if (ThangCu != ThangMoi)
       //                         i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += tam == "" ? "" : Quy + " " + tam + ", ";
       //                     }

       //                 }
       //                 else
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += Quy + " " + tam + ", ";
       //                     }

       //                 }
       //                 QuyNew = Quy;
       //                 ThangMoi = ThangCu;
       //                 XeCu = XeMoi;
       //             }
       //             if (i.Thang >= 4 & i.Thang <= 6)
       //             {
       //                 Quy = "Quý II" + "/" + i.Nam;
       //                 ThangCu = i.Thang.ToString();
       //                 string tam = ""; XeMoi = i.TenNX;
       //                 foreach (var bs in ltBienSo)
       //                     if (!string.IsNullOrEmpty(bs.TenLX))
       //                     {
       //                         if (Quy == QuyNew)
       //                         {

       //                             if (i.TenNX == "o to")
       //                             {
       //                                 if (ThangCu != ThangMoi)
       //                                 {
       //                                     tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                 }
       //                                 else
       //                                 {
       //                                     tam +=
       //                                         string.Format("{0:#,0.##}",
       //                                             ltGiuXete.Where(p => p.Thang >= 4 & p.Thang <= 6 & p.TenNX == i.TenNX)
       //                                                 .Sum(p => p.SoLuong)) + " " + bs.LX;
       //                                 }

       //                             }
       //                             else
       //                             {

       //                                 if (XeCu != XeMoi)
       //                                 {
       //                                     //tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                     tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 4 & p.Thang <= 6 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                                 }

       //                             }

       //                         }
       //                         else
       //                         {
       //                             if (i.TenNX == "o to")
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                             }
       //                             else
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 4 & p.Thang <= 6 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                             }
       //                         }


       //                     }
       //                 if (Quy == QuyNew)
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         //  if (ThangCu != ThangMoi)
       //                         i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += tam == "" ? "" : tam + ", ";
       //                     }

       //                 }
       //                 else
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += Quy + " " + tam + ", ";
       //                     }

       //                 }
       //                 QuyNew = Quy;
       //                 ThangMoi = ThangCu;
       //                 XeCu = XeMoi;
       //             }

       //             if (i.Thang >= 7 & i.Thang <= 9)
       //             {
       //                 Quy = "Quý III" + "/" + i.Nam; ;
       //                 ThangCu = i.Thang.ToString();
       //                 string tam = ""; XeMoi = i.TenNX;
       //                 foreach (var bs in ltBienSo)
       //                     if (!string.IsNullOrEmpty(bs.TenLX))
       //                     {
       //                         if (Quy == QuyNew)
       //                         {

       //                             if (i.TenNX == "o to")
       //                             {
       //                                 if (ThangCu != ThangMoi)
       //                                 {
       //                                     tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                 }
       //                                 else
       //                                 {
       //                                     tam +=
       //                                         string.Format("{0:#,0.##}",
       //                                             ltGiuXete.Where(p => p.Thang >= 7 & p.Thang <= 9 & p.TenNX == i.TenNX)
       //                                                 .Sum(p => p.SoLuong)) + " " + bs.LX;
       //                                 }

       //                             }
       //                             else
       //                             {

       //                                 if (XeCu != XeMoi)
       //                                 {
       //                                     //tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                     tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 7 & p.Thang <= 9 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                                 }

       //                             }

       //                         }
       //                         else
       //                         {
       //                             if (i.TenNX == "o to")
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                             }
       //                             else
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 7 & p.Thang <= 9 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                             }
       //                         }


       //                     }
       //                 if (Quy == QuyNew)
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         // if (ThangCu != ThangMoi)
       //                         i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += tam == "" ? "" : tam + ", ";
       //                     }

       //                 }
       //                 else
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += Quy + " " + tam + ", ";
       //                     }

       //                 }
       //                 QuyNew = Quy;
       //                 ThangMoi = ThangCu;
       //                 XeCu = XeMoi;
       //             }
       //             if (i.Thang >= 10 & i.Thang <= 12)
       //             {
       //                 Quy = "Quý IV" + "/" + i.Nam; ;
       //                 ThangCu = i.Thang.ToString();
       //                 XeMoi = i.TenNX;
       //                 string tam = "";
       //                 foreach (var bs in ltBienSo)
       //                     if (!string.IsNullOrEmpty(bs.TenLX))
       //                     {
       //                         if (Quy == QuyNew)
       //                         {

       //                             if (i.TenNX == "o to")
       //                             {
       //                                 if (ThangCu != ThangMoi)
       //                                 {
       //                                     tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                 }
       //                                 else
       //                                 {
       //                                     tam +=
       //                                         string.Format("{0:#,0.##}",
       //                                             ltGiuXete.Where(p => p.Thang >= 10 & p.Thang <= 12 & p.TenNX == i.TenNX)
       //                                                 .Sum(p => p.SoLuong)) + " " + bs.LX;
       //                                 }

       //                             }
       //                             else
       //                             {

       //                                 if (XeCu != XeMoi)
       //                                 {
       //                                     //tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                                     tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 10 & p.Thang <= 12 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                                 }

       //                             }

       //                         }
       //                         else
       //                         {
       //                             if (i.TenNX == "o to")
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", bs.SoLuong) + " " + bs.LX;
       //                             }
       //                             else
       //                             {
       //                                 tam += string.Format("{0:#,0.##}", ltGiuXete.Where(p => p.Thang >= 10 & p.Thang <= 12 & p.TenNX == i.TenNX).Sum(p => p.SoLuong)) + " " + bs.TenLX;
       //                             }
       //                         }


       //                     }
       //                 if (Quy == QuyNew)
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         //if(ThangCu!=ThangMoi)
       //                         i.BienSo += string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += tam == "" ? "" : tam + ", ";
       //                     }

       //                 }
       //                 else
       //                 {
       //                     if (i.TenNX == "o to")
       //                     {
       //                         i.BienSo += " " + string.Format("T{0}/{1}", i.Thang, i.Nam) + " " + tam + ", ";
       //                     }
       //                     else
       //                     {
       //                         i.BienSo += Quy + " " + tam + ", ";
       //                     }

       //                 }
       //                 QuyNew = Quy;
       //                 ThangMoi = ThangCu;
       //                 XeCu = XeMoi;
       //             }
       //             //i.BienSo = i.BienSo.Trim(' ').Trim(';');
       //             cLyDo.Text += string.Format("{0}", i.BienSo);
       //             cLyDo2.Text += string.Format("{0}", i.BienSo);
       //             cLyDo3.Text += string.Format("{0}", i.BienSo);
       //             #endregion
       //         }
       //         var ltGiuXeteLe = (from
       //                           dv in db.dvHoaDons

       //                            join ptct in db.ptChiTietPhieuThus on dv.ID equals ptct.LinkID
       //                            join pt in db.ptPhieuThus on ptct.MaPT equals pt.ID
       //                            where pt.ID == _ID & dv.MaLDV == 6
       //                       & dv.IsDuyet == true & dv.IsHoaDonTaoTay.GetValueOrDefault() == true


       //                            select new
       //                            {
       //                                dv.DienGiai

       //                            }).ToList();
       //         foreach (var i in ltGiuXeteLe)
       //         {
       //             cLyDo.Text += i.DienGiai + ", ";
       //             cLyDo2.Text += i.DienGiai + ", ";
       //             cLyDo3.Text += i.DienGiai + ", ";
       //         }
       //         cLyDo.Text = cLyDo.Text.Length > 0 ? cLyDo.Text.Remove(cLyDo.Text.Length - 2) :
       //         "";
       //         cLyDo2.Text = cLyDo2.Text.Length > 0 ? cLyDo2.Text.Remove(cLyDo2.Text.Length - 2) :
       //    "";
       //         cLyDo3.Text = cLyDo3.Text.Length > 0 ? cLyDo3.Text.Remove(cLyDo3.Text.Length - 2) :
       //"";
       //     }
       //     else
       //     {
       //         var ltGiuXeteLe = (from
       //                       dv in db.dvHoaDons

       //                            join ptct in db.ptChiTietPhieuThus on dv.ID equals ptct.LinkID
       //                            join pt in db.ptPhieuThus on ptct.MaPT equals pt.ID
       //                            where pt.ID == _ID & dv.MaLDV == 6
       //                       & dv.IsDuyet == true & dv.IsHoaDonTaoTay.GetValueOrDefault() == true


       //                            select new
       //                            {
       //                                dv.DienGiai

       //                            }).ToList();
       //         foreach (var i in ltGiuXeteLe)
       //         {
       //             cLyDo.Text += i.DienGiai + ", ";
       //             cLyDo2.Text += i.DienGiai + ", ";
       //             cLyDo3.Text += i.DienGiai + ", ";
       //         }
       //         if (ltGiuXeteLe.Count > 0)
       //         {
       //             cLyDo.Text = cLyDo.Text.Length > 0
       //                 ? cLyDo.Text.Remove(cLyDo.Text.Length - 2)
       //                 : "";
       //             cLyDo2.Text = cLyDo2.Text.Length > 0
       //                 ? cLyDo2.Text.Remove(cLyDo2.Text.Length - 2)
       //                 : "";
       //             cLyDo3.Text = cLyDo3.Text.Length > 0
       //                 ? cLyDo3.Text.Remove(cLyDo3.Text.Length - 2)
       //                 : "";
       //         }
       //     }
        }

        class GiuXeItem2
        {
            public int? MaLX { get; set; }
            public string TenLDV { get; set; }
            public string BienSo { get; set; }
            public int? MaLDV { get; set; }
            public int? Thang { get; set; }
            public int? Nam { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? ThanhTien { get; set; }
            public string TenNX { get; set; }
        }
        class GiuXeItem3
        {
            public int? MaLX { get; set; }
            public string TenLDV { get; set; }
            public string BienSo { get; set; }
            public int? MaLDV { get; set; }
            public int? Thang { get; set; }
            public int? Nam { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? SoLuong { get; set; }
            public decimal? ThanhTien { get; set; }
            public string TenNX { get; set; }
            public string TenLX { get; set; }
        }
        class GiuXeItem4
        {
            public int? MaLX { get; set; }
            public string TenLDV { get; set; }
            public string BienSo { get; set; }
            public int? MaLDV { get; set; }
            public int? Thang { get; set; }
            public int? Nam { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? ThanhTien { get; set; }
            public decimal? SoLuong { get; set; }
            public string TenNX { get; set; }
            public string TenLX { get; set; }
        }
        class GiuXeItem5
        {
            public int? MaLX { get; set; }
            public string TenLDV { get; set; }
            public string BienSo { get; set; }
            public int? MaLDV { get; set; }
            public int? Thang { get; set; }
            public int? Nam { get; set; }
            public decimal? DonGia { get; set; }
            public decimal? ThanhTien { get; set; }
            public decimal? SoLuong { get; set; }
            public string TenNX { get; set; }
            public string TenLX { get; set; }
            public string DienGiai { get; set; }
        }
    }
}
