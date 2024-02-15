using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using LinqToExcel.Extensions;

using DevExpress.XtraReports.UI;

namespace LandSoftBuilding.Fund.Input.HopDongThue
{
    public partial class frmManagerHDT : DevExpress.XtraEditors.XtraForm
    {
        public frmManagerHDT()
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
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
           
            var query= (from p in db.ptPhieuThus
                                join pl in db.ptPhanLoais on p.MaPL equals pl.ID into tblPhanLoai
                                from pl in tblPhanLoai.DefaultIfEmpty()
                                join k in db.tnKhachHangs on p.MaKH equals k.MaKH
                            
                                join nkh in db.khNhomKhachHangs on k.MaNKH equals nkh.ID into tblNhomKhachHang
                                from nkh in tblNhomKhachHang.DefaultIfEmpty()
                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                from nh in tblNganHang.DefaultIfEmpty()

                                //join httt in db.ptHinhThucThanhToans on p.MaHTHT equals httt.ID into hinhthucTT from httt in hinhthucTT.DefaultIfEmpty()

                                //join hd in db.ctHopDongs on p.idctHopDong equals hd.ID into hopdong
                                //from hd in hopdong.DefaultIfEmpty()

                                where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayThu) >= 0 & SqlMethods.DateDiffDay(p.NgayThu, denNgay) >= 0
                                & !p.IsDieuChuyen.GetValueOrDefault()
                                //& p.IsHDThue.GetValueOrDefault()
                                select new
                                {
                                    p.ID,
                                    p.SoPT,
                                    //SoHD = hd.SoHDCT,
                                   // Gop_Goc = db.ptPhieuThus.Any(o => o.ParentID == p.ID),
                                    //DieuChuyen_Goc = db.ptPhieuThus.Any(o => o.idPhieuThuGoc == p.ID),
                                    //NgayPTGoc = goc.NgayThu,
                                    //SoPTGoc = goc.SoPT,
                                    //p.IsPhieuGoc,
                                    p.NgayThu,
                                    p.MaKH,
                                    SoTien=p.SoTien,
                                    k.KyHieu,
                                    //TT = p.TrangThaiHDDT,
                                    //p.ParentID,
                                    //TrangThaiHDDT = p.TrangThaiHDDT == 1 ? "Đã phát hành HĐĐT" : (p.TrangThaiHDDT == 0 ? "Gạch bỏ" : "Chưa xuất HĐĐT"),
                                    TenKH = (bool)k.IsCaNhan ? String.Format("{0} {1}", k.HoKH, k.TenKH) : k.CtyTen,
                                    NguoiThu = nv.HoTenNV,
                                    //p.IsGop,
                                    p.NguoiNop,
                                    p.DiaChiNN,
                                    LyDo=p.LyDo,
                                    pl.TenPL,
                                    p.ChungTuGoc,
                                    NguoiNhap = nvn.HoTenNV,
                                    p.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    p.NgaySua,
                                    //PhuongThuc = httt.TenHTTT,
                                    tk.SoTK,
                                    nh.TenNH,
                                    nkh.TenNKH,
                                    //p.SoCTNH,
                                }).ToList();

            gcPhieuThu.DataSource = query;
            // Hiện 3 cột phí quản lý, phí xe, phí nước PMT
            if (Common.User.MaTN == 34 | Common.User.MaTN == 39)
            {
                gvPhieuThu.Columns["PhiQL"].Visible = true;
                gvPhieuThu.Columns["PhiXe"].Visible = true;
                gvPhieuThu.Columns["PhiNuoc"].Visible = true;
            }
            else
            {
                gvPhieuThu.Columns["PhiQL"].Visible = false;
                gvPhieuThu.Columns["PhiXe"].Visible = false;
                gvPhieuThu.Columns["PhiNuoc"].Visible = false;
            }
        }

        void RefreshData()
        {
            //linqInstantFeedbackSource1.Refresh();
            LoadData();
        }

        void AddRecord()
        {
            using (var frm = new frmEditHDT())
            {
                frm.IsHDThue = true;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditRecord()
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            var Goc = (bool?)gvPhieuThu.GetFocusedRowCellValue("IsGop");
            var Gocg = (int?)gvPhieuThu.GetFocusedRowCellValue("ParentID");
            if (id == null)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin");
                return;
            }

            using (var frm = new LandSoftBuilding.Fund.Input.HopDongThue.frmEditHDT())
            {
                frm.IsHDThue = true;
                frm.MaPT = id;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.RefreshData();
                    if ((int?) gvPhieuThu.GetFocusedRowCellValue("TT") != null)
                    {
                        try
                        {
                           // HoaSuaDonDienTu();
                            if (Goc == true)
                            {
                                ThayTheHoaDonGop();
                            }
                            else
                            {
                                ThayTheHoaDon();
                            }
                           
                        }
                        catch (Exception)
                        {
                            
                           DialogBox.Error("Đã xảy ra lỗi trong quá trình chỉnh sửa HĐĐT. Vui lòng kiểm tra lại");
                        }
                      
                    }
                  
                }
                 
            }
            
        }

        void DeleteRecord()
        {
                                       
            List<int> ltIDHoaDon = new List<int>();
            var indexs = gvPhieuThu.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin muốn xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
                var objTL_U = (from tl in db.ctThanhLies
                             join hd in db.ctHopDongs on tl.MaHD equals hd.ID
                             join pt in db.ptPhieuThus on hd.MaKH equals pt.MaKH
                             where pt.ID == id
                             select tl).FirstOrDefault();
                var objPT_D = (from tl in db.ctThanhLies
                             join hd in db.ctHopDongs on tl.MaHD equals hd.ID
                             join pt in db.ptPhieuThus on hd.MaKH equals pt.MaKH
                             where pt.ID == id
                             select pt).FirstOrDefault();
                objTL_U.PhaiThu = objTL_U.PhaiThu + objPT_D.SoTien;
                objTL_U.DaThu= objTL_U.DaThu - objPT_D.SoTien;
                db.SubmitChanges();

                LandSoftBuilding.Fund.Class.PhieuThu.DeletePhieuThu((int)gvPhieuThu.GetRowCellValue(i, "ID"));

            }
            this.LoadData();
            /*   using (var frmlydo = new frmLyDoXoa())
               {
                   frmlydo.ShowDialog();
                   if (frmlydo.DialogResult == System.Windows.Forms.DialogResult.OK)
                 {
                    foreach (var i in indexs)
                       {
                           var pt = db.ptPhieuThus.Single(p => p.ID == (int)gvPhieuThu.GetRowCellValue(i, "ID"));
                           #region Luu lai phieu thu bi xoa PL Xuan MAi

                          if (pt != null)
                           {
                               var PTDX = new ptPhieuThuDaXoa();
                               PTDX.LyDo = pt.LyDo;
                              PTDX.MaKH = pt.MaKH;
                               PTDX.MaNV = pt.MaNV;
                              PTDX.MaNVN = Common.User.MaNV;
                               // PTDX.MaNVS = pt.MaNVS;
                               PTDX.MaPL = pt.MaPL;
                               PTDX.MaTKNH = pt.MaTKNH;
                               PTDX.MaTN = pt.MaTN;
                               PTDX.NguoiNop = pt.NguoiNop;
                              PTDX.NgayNhap = DateTime.Now;

                             PTDX.NgayThu = pt.NgayThu;
                               //PTDX.NgaySua = pt.NgaySua;
                              PTDX.SoPT = pt.SoPT;
                              PTDX.SoTien = pt.SoTien;
                               PTDX.ChungTuGoc = pt.ChungTuGoc;
                             PTDX.DiaChiNN = pt.DiaChiNN;
                              PTDX.LyDoXoa = frmlydo.LyDo;
                              PTDX.IsHDThue = true;
                               db.ptPhieuThuDaXoas.InsertOnSubmit(PTDX);
                               var queryChiTietPT = db.ptChiTietPhieuThus.Where(p => p.MaPT == pt.ID).ToList();

                              if (queryChiTietPT.Count() > 0)
                               {
                                  foreach (var qe in queryChiTietPT)
                                  {
                                       if (qe.TableName == "dvHoaDon")
                                       {
                                           var hd = db.dvHoaDons.FirstOrDefault(o => o.ID == qe.LinkID & o.TableName == "ctLichThanhToan");

                                           if (hd != null)
                                              ltIDHoaDon.Add(hd.LinkID.Value);
                                       }

                                       if (qe.TableName == "ctLichThanhToan")
                                       {
                                           var ltt = db.ctLichThanhToans.FirstOrDefault(o => o.ID == qe.LinkID);

                                           if (ltt != null)
                                               ltIDHoaDon.Add(ltt.ID);
                                       }

                                    var PTDXChiTiet = new ptChiTietPhieuThuDaXoa();
                                       PTDXChiTiet.LinkID = qe.LinkID;
                                       PTDXChiTiet.MaPT = pt.SoPT;
                                       PTDXChiTiet.SoTien = qe.SoTien;
                                       PTDXChiTiet.TableName = qe.TableName;
                                       PTDXChiTiet.DienGiai = qe.DienGiai;
                                       db.ptChiTietPhieuThuDaXoas.InsertOnSubmit(PTDXChiTiet);
                                       var Hoadon = db.dvHoaDons.SingleOrDefault(p => p.ID == qe.LinkID);
                                      if (Hoadon != null)
                                       {
                                           //Hoadon.MaNVThu = null;
                                           //Hoadon.NgayThu = null;
                                           //Hoadon.LoaiThu = "";
                                       }
                                  }

                               }

                          }

                           #endregion

                           db.ptPhieuThus.DeleteOnSubmit(pt);
                       }
                  }
                }*/


/*            try
            {
                db.SubmitChanges();

                //foreach(var _MaLTT in ltIDHoaDon)
                //    DichVu.LaiSuat.LaiSuatCls.TinhLai(_MaLTT);

                this.RefreshData();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }*/
        }

        void Details()
        {
            var db = new MasterDataContext();
            try
            {
                var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    gcChiTiet.DataSource = null;
                    return;
                }

                gcChiTiet.DataSource = (from ct in db.ptChiTietPhieuThus
                                        where ct.MaPT == id
                                        select new { ct.DienGiai, ct.SoTien })
                                       .ToList();
            }
            catch
            {
            }
            finally
            {
                db.Dispose();
            }
        }

        void ImportRecord()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new frmImport())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;

            gvPhieuThu.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;
            gvChiTiet.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cmbKyBaoCao.Items.Add(str);
            }
            itemKyBaoCao.EditValue = objKBC.Source[3];
            SetDate(3);

            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void gvPhieuThu_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Details();
        }

        private void gvPhieuThu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Details();
        }

        void GachBo(int ID1)
        {
            //MasterDataContext dbo = new MasterDataContext();


            //var tam = new BusinessSV.BusinessServiceSoapClient();
            //var TK = dbo.TaiKhoanHDDTs.FirstOrDefault();
            //var a = tam.confirmPaymentFkey(string.Format("{0}", ID1), TK.TKWebServies,TK.PassWebServies);
            //var PT = dbo.ptPhieuThus.SingleOrDefault(p => p.ID == ID1);
           
            //if (a.Contains("OK"))
            //{
            //    if (PT != null)
            //    {
            //        PT.TrangThaiHDDT = 1;
            //        dbo.SubmitChanges();
            //        this.RefreshData();
            //    }
            //}
            //else
            //{
            //    DialogBox.Alert(a.ToString());
            //}
            
        }
        void BoGach(int ID1)
        {
            //MasterDataContext dbo = new MasterDataContext();


            //var tam = new BusinessSV.BusinessServiceSoapClient();
            //var TK = dbo.TaiKhoanHDDTs.FirstOrDefault();
          
            //var a = tam.UnConfirmPaymentFkey(string.Format("{0}", ID1), TK.TKWebServies, TK.PassWebServies);
           
            //DialogBox.Alert(a.ToString());
        }
        void XoaBoHoDonDienTu(int ID1)//BoGachNooHoaDon
        {

            //MasterDataContext dbo = new MasterDataContext();
            //var tam = new BusinessSV.BusinessServiceSoapClient();
            //var TK = dbo.TaiKhoanHDDTs.FirstOrDefault();
            //var a = tam.cancelInv(TK.TKAmin, TK.PassAmin, string.Format("{0}", ID1), TK.TKWebServies,TK.PassWebServies);
            //if (a.Contains("OK"))
            //{
                
            //}
            //else
            //{
            //    DialogBox.Alert(a.ToString());
            //}
          
           // this.RefreshData();

        }
        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DeleteRecord();
           
        }
        void ThayTheHoaDon()//ChinhSuaHoaDon
        {
            MasterDataContext db = new MasterDataContext();
            var PT = (from pt in db.ptPhieuThus
                      join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
                      join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
                      join hd in db.dvHoaDons on ptct.LinkID equals hd.ID
                      join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
                      where pt.ID == (int)gvPhieuThu.GetFocusedRowCellValue("ID")

                      select new
                      {
                          pt.SoPT,
                          SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
                          TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                          pt.LyDo,
                          DiaChiNN = kh.DCLL,
                          PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
                          pt.NgayThu,
                          ptct.DienGiai,
                          ldv.TenLDV,
                          //ldv.DonViTinh,
                          kh.CtyMaSoThue,
                          kh.KyHieu,
                          kh.DienThoaiKH,
                          hd.MaLDV,
                          hd.LinkID,
                          SoTienCT = ptct.SoTien
                      }
                ).ToList();
            var objTien = new TienTeCls();
            XmlDocument xmlMasterObject = new XmlDocument();
            string xml = "";
            xml += "<AdjustInv>";
            //foreach (var i in PT[])
            // {
            // xml += "<Inv>";
            xml += string.Format("<key>{0}</key>", gvPhieuThu.GetFocusedRowCellValue("ID"));
            // xml += "<Invoice>";
            xml += string.Format("<CusCode>{0}</CusCode>", PT.First().KyHieu);
            xml += string.Format("<CusName>{0}</CusName>", PT.First().TenKH);
            xml += string.Format("<CusAddress>{0}</CusAddress>", PT.First().DiaChiNN);
            xml += string.Format("<CusPhone>{0}</CusPhone>", PT.First().DienThoaiKH);
            xml += string.Format("<CusTaxCode>{0}</CusTaxCode>", PT.First().CtyMaSoThue);
            xml += string.Format("<PaymentMethod>{0}</PaymentMethod>", PT.First().PTTT);
            xml += string.Format("<KindOfService>{0:dd/MM/yyyy}</KindOfService>", PT.First().NgayThu);
            xml += string.Format("<Type>{0}</Type>",4);
            var VATNuoc =
                   PT.First().MaLDV == 9
                       ? 15
                       : 10;
            decimal ChiaTyLe =
                       PT.First().MaLDV == 9
                           ? (decimal)1.15
                           : (decimal)1.1;
            var TruocVAt = PT.First().SoTien / (decimal)1.15;
            var VAT =
                 PT.First().SoTien - TruocVAt;
            xml += "<Products>";
            foreach (var j in PT)
            {

                xml += "<Product>";
                var SL =
                    j.MaLDV == 9
                        ? db.dvNuocs.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu.GetValueOrDefault(1)
                        : (PT.First().MaLDV == 8 ? db.dvDiens.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu.GetValueOrDefault(1) : 1);


                xml += string.Format("<ProdName>{0}</ProdName>", j.TenLDV);
                //xml += string.Format("<ProdUnit>{0}</ProdUnit>", j.DonViTinh);
                xml += string.Format("<ProdQuantity>{0}</ProdQuantity>", Math.Round((decimal)SL,0,MidpointRounding.AwayFromZero));
                xml += string.Format("<ProdPrice>{0}</ProdPrice>",
                    Math.Round((decimal)((j.SoTienCT / ChiaTyLe) / SL), 0, MidpointRounding.AwayFromZero));
                xml += string.Format("<Amount>{0}</Amount>",
                    Math.Round((decimal)(j.SoTienCT / ChiaTyLe), 0, MidpointRounding.AwayFromZero));
                xml += "</Product>";

            } xml += "</Products>";
            xml += string.Format("<Total>{0}</Total>", Math.Round((decimal)TruocVAt, 0, MidpointRounding.AwayFromZero));
            xml += string.Format("<DiscountAmount>{0}</DiscountAmount>", 0);
            xml += string.Format("<VATRate>{0}</VATRate>", Math.Round((decimal)VATNuoc, 0, MidpointRounding.AwayFromZero));
            xml += string.Format("<VATAmount>{0}</VATAmount>", Math.Round((decimal)VAT, 0, MidpointRounding.AwayFromZero));
            xml += string.Format("<Amount>{0}</Amount>", Math.Round((decimal)PT.First().SoTien, 0, MidpointRounding.AwayFromZero));
            xml += string.Format("<AmountInWords>{0}</AmountInWords>", objTien.DocTienBangChu((decimal)PT.First().SoTien.Value, "đồng chẵn"));
            xml += "<Extra>1</Extra>";
            xml += string.Format("<ArisingDate>{0:dd/MM/yyyy}</ArisingDate>", PT.First().NgayThu);
            xml +=
                "<PaymentStatus>1</PaymentStatus>";
            // xml += "</Invoice>";
            // xml += "</Inv>";
            //  }

            //xml += "<Inv></Inv>";
            xml += "</AdjustInv>";
            xmlMasterObject.LoadXml(xml);



            //var tam = new BusinessSV.BusinessServiceSoapClient();
            //var TK = db.TaiKhoanHDDTs.FirstOrDefault();
            //var a = tam.adjustInv(TK.TKAmin, TK.PassAmin, xml, TK.TKWebServies, TK.PassWebServies, string.Format("{0}", (int)gvPhieuThu.GetFocusedRowCellValue("ID")), 0);
            //if (a.Contains("OK"))
            //{
            //    //DialogBox.Alert(a.ToString());
            //}
            //else
            //{
            //    DialogBox.Alert(a.ToString());
            //}
            this.RefreshData();

        }
        void ThayTheHoaDonGop()//ChinhSuaHoaDon
        {
           // MasterDataContext db = new MasterDataContext();
           // List<int> termsList = new List<int>();
           // if ((bool?) gvPhieuThu.GetFocusedRowCellValue("IsPhieuGoc") == true)
           // {
           //     var Sub = (from pt in db.ptPhieuThus where pt.ParentID == (int)gvPhieuThu.GetFocusedRowCellValue("ID") select pt.ID).ToList();
           //     foreach (var i in Sub)
           //     {
           //         termsList.Add(i);
           //     }
           //     termsList.Add((int)gvPhieuThu.GetFocusedRowCellValue("ID"));
           // }
           // else
           // {
           //     var t = (int) gvPhieuThu.GetFocusedRowCellValue("ParentID");
           //     var Sub = (from pt in db.ptPhieuThus where pt.ParentID == t & pt.ID != (int)gvPhieuThu.GetFocusedRowCellValue("ID") select pt.ID).ToList();
           //     foreach (var i in Sub)
           //     {
           //         termsList.Add(i);
           //     }
           //     termsList.Add((int)gvPhieuThu.GetFocusedRowCellValue("ID"));
           //     termsList.Add(t);
           // }
         
           // var PT = (from pt in db.ptPhieuThus
           //           join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
           //           join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
           //           join hd in db.dvHoaDons on ptct.LinkID equals hd.ID
           //           join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
           //           where termsList.Contains(pt.ID )

           //           select new
           //           {
           //               pt.SoPT,
           //               SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
           //               TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
           //               pt.LyDo,
           //               DiaChiNN = kh.DCLL,
           //               PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
           //               pt.NgayThu,
           //               ptct.DienGiai,
           //               ldv.TenLDV,
           //               //ldv.DonViTinh,
           //               kh.CtyMaSoThue,
           //               kh.KyHieu,
           //               kh.DienThoaiKH,
           //               hd.MaLDV,
           //               hd.LinkID,
           //               SoTienCT = ptct.SoTien
           //           }
           //     ).ToList();
           // var objTien = new TienTeCls();
           // XmlDocument xmlMasterObject = new XmlDocument();
           // string xml = "";
           // xml += "<AdjustInv>";
           // //foreach (var i in PT[])
           // // {
           // // xml += "<Inv>";
           //var w=
           // gvPhieuThu.GetFocusedRowCellValue("ID");
           // var q =gvPhieuThu.GetFocusedRowCellValue("ParentID");
           // var chk = (bool?) gvPhieuThu.GetFocusedRowCellValue("IsPhieuGoc") == true
           //     ? gvPhieuThu.GetFocusedRowCellValue("ID")
           //     : gvPhieuThu.GetFocusedRowCellValue("ParentID");
           // xml += string.Format("<key>{0}</key>", chk);
           // // xml += "<Invoice>";
           // xml += string.Format("<CusCode>{0}</CusCode>", PT.First().KyHieu);
           // xml += string.Format("<CusName>{0}</CusName>", PT.First().TenKH);
           // xml += string.Format("<CusAddress>{0}</CusAddress>", PT.First().DiaChiNN);
           // xml += string.Format("<CusPhone>{0}</CusPhone>", PT.First().DienThoaiKH);
           // xml += string.Format("<CusTaxCode>{0}</CusTaxCode>", PT.First().CtyMaSoThue);
           // xml += string.Format("<PaymentMethod>{0}</PaymentMethod>", PT.First().PTTT);
           // xml += string.Format("<KindOfService>{0:dd/MM/yyyy}</KindOfService>", PT.First().NgayThu);
           // xml += string.Format("<Type>{0}</Type>", 4);
           // var VATNuoc =
           //        10;
           // decimal ChiaTyLe =
                      
           //               (decimal)1.1;
           // var TruocVAt = PT.Sum(p => p.SoTienCT).GetValueOrDefault() / ChiaTyLe;
           // var VAT =
           //      PT.Sum(p => p.SoTienCT).GetValueOrDefault() - TruocVAt;
           // xml += "<Products>";
           // foreach (var j in PT)
           // {

           //     xml += "<Product>";
           //     var SL =
           //         j.MaLDV == 9
           //             ? db.dvNuocs.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu.GetValueOrDefault(1)
           //             : (PT.First().MaLDV == 8 ? db.dvDiens.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu.GetValueOrDefault(1) : 1);


           //     xml += string.Format("<ProdName>{0}</ProdName>", j.TenLDV);
           //     xml += string.Format("<ProdUnit>{0}</ProdUnit>", j.DonViTinh);
           //     xml += string.Format("<ProdQuantity>{0}</ProdQuantity>", Math.Round((decimal)SL, 0, MidpointRounding.AwayFromZero));
           //     xml += string.Format("<ProdPrice>{0}</ProdPrice>",
           //         Math.Round((decimal)((j.SoTienCT / ChiaTyLe) / SL), 0, MidpointRounding.AwayFromZero));
           //     xml += string.Format("<Amount>{0}</Amount>",
           //         Math.Round((decimal)(j.SoTienCT / ChiaTyLe), 0, MidpointRounding.AwayFromZero));
           //     xml += "</Product>";

           // } xml += "</Products>";
           // xml += string.Format("<Total>{0}</Total>", Math.Round((decimal)TruocVAt, 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<DiscountAmount>{0}</DiscountAmount>", 0);
           // xml += string.Format("<VATRate>{0}</VATRate>", Math.Round((decimal)VATNuoc, 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<VATAmount>{0}</VATAmount>", Math.Round((decimal)VAT, 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<Amount>{0}</Amount>", Math.Round((decimal)PT.Sum(p => p.SoTienCT).GetValueOrDefault(), 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<AmountInWords>{0}</AmountInWords>", objTien.DocTienBangChu((decimal)PT.Sum(p => p.SoTienCT).GetValueOrDefault(), "đồng chẵn"));
           // xml += "<Extra>1</Extra>";
           // xml += string.Format("<ArisingDate>{0:dd/MM/yyyy}</ArisingDate>", PT.First().NgayThu);
           // xml +=
           //     "<PaymentStatus>1</PaymentStatus>";
           // // xml += "</Invoice>";
           // // xml += "</Inv>";
           // //  }

           // //xml += "<Inv></Inv>";
           // xml += "</AdjustInv>";
           // xmlMasterObject.LoadXml(xml);



           // var tam = new BusinessSV.BusinessServiceSoapClient();
           // var TK = db.TaiKhoanHDDTs.FirstOrDefault();
           // var a = tam.adjustInv(TK.TKAmin, TK.PassAmin, xml, TK.TKWebServies, TK.PassWebServies, string.Format("{0}", (int)chk), 0);
           // if (a.Contains("OK"))
           // {
           //     //DialogBox.Alert(a.ToString());
           // }
           // else
           // {
           //     DialogBox.Alert(a.ToString());
           // }
           // this.RefreshData();

        }
        void HoaSuaDonDienTu(int ID)//ChinhSuaHoaDon
        {
           // MasterDataContext db = new MasterDataContext();
           // var PT = (from pt in db.ptPhieuThus
           //     join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
           //     join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
           //     join hd in db.dvHoaDons on ptct.LinkID equals hd.ID
           //     join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
           //     where pt.ID ==ID
            
           //           select new
           //           {
           //               pt.SoPT,
           //               SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
           //               TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
           //               pt.LyDo,
           //               DiaChiNN = kh.DCLL,
           //               PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
           //               pt.NgayThu,
           //               ptct.DienGiai,
           //               ldv.TenLDV,
           //               ldv.DonViTinh,
           //               kh.CtyMaSoThue,
           //               kh.KyHieu,
           //               kh.DienThoaiKH,
           //               hd.MaLDV,
           //               hd.LinkID,
           //               SoTienCT = ptct.SoTien
           //           }
           //     ).ToList().Concat((from pt in db.ptPhieuThus
           //                         join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
           //                         join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
           //                         join hd in db.dvHoaDons on ptct.LinkID equals hd.ID
           //                         join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
           //                        where pt.ParentID == ID

           //                         select new
           //                         {
           //                             pt.SoPT,
           //                             SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
           //                             TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
           //                             pt.LyDo,
           //                             DiaChiNN = kh.DCLL,
           //                             PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
           //                             pt.NgayThu,
           //                             ptct.DienGiai,
           //                             ldv.TenLDV,
           //                             ldv.DonViTinh,
           //                             kh.CtyMaSoThue,
           //                             kh.KyHieu,
           //                             kh.DienThoaiKH,
           //                             hd.MaLDV,
           //                             hd.LinkID,
           //                             SoTienCT = ptct.SoTien
           //                         }
           //     ).ToList());
           // var objTien = new TienTeCls();
           // XmlDocument xmlMasterObject = new XmlDocument();
           // string xml = "";
           // xml += "<ReplaceInv>";
           // //foreach (var i in PT[])
           // // {
           //// xml += "<Inv>";
           // xml += string.Format("<key>{0}</key>", ID);
           //// xml += "<Invoice>";
           // xml += string.Format("<CusCode>{0}</CusCode>", PT.First().KyHieu);
           // xml += string.Format("<CusName>{0}</CusName>", PT.First().TenKH);
           // xml += string.Format("<CusAddress>{0}</CusAddress>", PT.First().DiaChiNN);
           // xml += string.Format("<CusPhone>{0}</CusPhone>", PT.First().DienThoaiKH);
           // xml += string.Format("<CusTaxCode>{0}</CusTaxCode>", PT.First().CtyMaSoThue);
           // xml += string.Format("<PaymentMethod>{0}</PaymentMethod>", PT.First().PTTT);
           // xml += string.Format("<KindOfService>{0:dd/MM/yyyy}</KindOfService>", PT.First().NgayThu);
           // var VATNuoc =
           //        PT.First().MaLDV == 9
           //            ? 15
           //            : 10;
           // decimal ChiaTyLe =
           //            PT.First().MaLDV == 9
           //                ? (decimal)1.15
           //                : (decimal)1.1;
           // var TruocVAt = PT.Sum(p => p.SoTien) / (decimal)1.15;
           // var VAT =
           //      PT.Sum(p => p.SoTien) - TruocVAt;
           // xml += "<Products>";
           // foreach (var j in PT)
           // {

           //     xml += "<Product>";
           //     var SL =
           //         j.MaLDV == 9
           //             ? db.dvNuocs.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu
           //             : (PT.First().MaLDV == 8 ? db.dvDiens.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu : 1);


           //     xml += string.Format("<ProdName>{0}</ProdName>", j.TenLDV);
           //     xml += string.Format("<ProdUnit>{0}</ProdUnit>", j.DonViTinh);
           //     xml += string.Format("<ProdQuantity>{0}</ProdQuantity>", SL);
           //     xml += string.Format("<ProdPrice>{0}</ProdPrice>",
           //         Math.Round((decimal)((j.SoTienCT / ChiaTyLe) / SL), 0, MidpointRounding.AwayFromZero));
           //     xml += string.Format("<Amount>{0}</Amount>",
           //         Math.Round((decimal)(j.SoTienCT / ChiaTyLe), 0, MidpointRounding.AwayFromZero));
           //     xml += "</Product>";

           // } xml += "</Products>";
           // xml += string.Format("<Total>{0}</Total>", Math.Round((decimal)TruocVAt, 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<DiscountAmount>{0}</DiscountAmount>", 0);
           // xml += string.Format("<VATRate>{0}</VATRate>", Math.Round((decimal)VATNuoc, 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<VATAmount>{0}</VATAmount>", Math.Round((decimal)VAT, 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<Amount>{0}</Amount>", Math.Round((decimal)PT.Sum(p => p.SoTien), 0, MidpointRounding.AwayFromZero));
           // xml += string.Format("<AmountInWords>{0}</AmountInWords>", objTien.DocTienBangChu((decimal)PT.Sum(p => p.SoTien), "đồng chẵn"));
           // xml += "<Extra>1</Extra>";
           // xml += string.Format("<ArisingDate>{0:dd/MM/yyyy}</ArisingDate>", PT.First().NgayThu);
           // xml +=
           //     "<PaymentStatus>1</PaymentStatus>";
           //// xml += "</Invoice>";
           //// xml += "</Inv>";
           // //  }

           // //xml += "<Inv></Inv>";
           // xml += "</ReplaceInv>";
           // xmlMasterObject.LoadXml(xml);

           // var tam = new BusinessSV.BusinessServiceSoapClient();
           // var TK = db.TaiKhoanHDDTs.FirstOrDefault();
           // var a = tam.replaceInv(TK.TKAmin, TK.PassAmin, xml, TK.TKWebServies, TK.PassWebServies, string.Format("{0}", ID), 0);
           // if (a.Contains("OK"))
           // {
           //     //DialogBox.Alert(a.ToString());
           // }
           // else
           // {
           //     DialogBox.Alert(a.ToString());
           // }
           // this.RefreshData();

        }
        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditRecord();
            //adjustInv 
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.AddRecord();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }
        
        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            //e.QueryableSource
            e.QueryableSource = from p in db.ptPhieuThus
                                join pl in db.ptPhanLoais on p.MaPL equals pl.ID into tblPhanLoai
                                from pl in tblPhanLoai.DefaultIfEmpty()
                                join k in db.tnKhachHangs on p.MaKH equals k.MaKH into tblKhachHang
                                from k in tblKhachHang.DefaultIfEmpty()
                                join nkh in db.khNhomKhachHangs on k.MaNKH equals nkh.ID into tblNhomKhachHang
                                from nkh in tblNhomKhachHang.DefaultIfEmpty()
                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                from nh in tblNganHang.DefaultIfEmpty()
                                where p.MaTN == maTN & SqlMethods.DateDiffDay(tuNgay, p.NgayThu) >= 0 & SqlMethods.DateDiffDay(p.NgayThu, denNgay) >= 0
                                select new
                                {
                                    p.ID,
                                    //p.SoPT,p.IsPhieuGoc,
                                    p.NgayThu,p.MaKH,
                                    p.SoTien,
                                   // k.KyHieu,TT=p.TrangThaiHDDT,p.ParentID,
                                    //TrangThaiHDDT=p.TrangThaiHDDT==1?"Đã phát hành HĐĐT" :(p.TrangThaiHDDT==0?"Gạch bỏ" :"Chưa xuất HĐĐT"),
                                    //TenKH = (bool)k.IsCaNhan ? String.Format("{0} {1}", k.HoKH, k.TenKH) : k.CtyTen,
                                    //NguoiThu = nv.HoTenNV,p.IsGop,
                                    p.NguoiNop,
                                    p.DiaChiNN,
                                    p.LyDo,
                                    pl.TenPL,
                                    p.ChungTuGoc,
                                    NguoiNhap = nvn.HoTenNV,
                                    p.NgayNhap,
                                    NguoiSua = nvs.HoTenNV,
                                    p.NgaySua,
                                    PhuongThuc = p.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                    tk.SoTK,
                                    nh.TenNH,
                                    nkh.TenNKH,
                                    PhiQL = (from ct in db.ptChiTietPhieuThus
                                             join hd in  db.dvHoaDons on ct.LinkID equals hd.ID
                                             where ct.TableName =="dvHoaDon" & hd.MaLDV == 13 & ct.MaPT == p.ID select ct.SoTien).Sum().GetValueOrDefault(),
                                    PhiXe = (from ct in db.ptChiTietPhieuThus
                                             join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                             where ct.TableName == "dvHoaDon" & hd.MaLDV == 6 & ct.MaPT == p.ID
                                             select ct.SoTien).Sum().GetValueOrDefault(),
                                    PhiNuoc = (from ct in db.ptChiTietPhieuThus
                                               join hd in db.dvHoaDons on ct.LinkID equals hd.ID
                                               where ct.TableName == "dvHoaDon" & hd.MaLDV == 9 & ct.MaPT == p.ID
                                               select ct.SoTien).Sum().GetValueOrDefault(),
                                };
            e.Tag = db;
        }
        
        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DismissQueryable: " + ex.Message);
            }
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportRecord();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcPhieuThu);
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            var db = new MasterDataContext();
            try
            {
                var ltReport = (from rp in db.rptReports
                                join tn in db.rptReports_ToaNhas on rp.ID equals tn.ReportID
                                where tn.MaTN == (byte)itemToaNha.EditValue & rp.GroupID == 5
                                orderby rp.Rank
                                select new { rp.ID, rp.Name }).ToList();

                barPrint.ItemLinks.Clear();
                DevExpress.XtraBars.BarButtonItem itemPrint;
                foreach (var i in ltReport)
                {
                    itemPrint = new DevExpress.XtraBars.BarButtonItem(barManager1, i.Name);
                    itemPrint.Tag = i.ID;
                    itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
                    barManager1.Items.Add(itemPrint);
                    barPrint.ItemLinks.Add(itemPrint);
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn [Phiếu thu] cần xem");
                return;
            }

            var maTN = (byte)itemToaNha.EditValue;

            DevExpress.XtraReports.UI.XtraReport rpt = null;

            switch ((int)e.Item.Tag)
            {
                case 3:
                    rpt = new rptPhieuThu(id.Value, maTN,1);
                    for (int i = 1; i <= 3; i++)
                    {
                        var rpt1 = new rptPhieuThu(id.Value, maTN,i);
                        rpt1.CreateDocument();
                        rpt.Pages.AddRange(rpt1.Pages);
                    }
                    rpt.PrintingSystem.ContinuousPageNumbering = true;
                    break;
                case 19:
                    rpt = new rptDetail(id.Value, maTN);
                    break;
                case 42:
                    rpt = new rptPhieuThuMau3(id.Value, maTN);
                    break;  
            }

            if (rpt != null)
            {
                rpt.ShowPreviewDialog();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //5485
            //var query = (int?) gvPhieuThu.GetFocusedRowCellValue("ID");
            //var MaCH =gvPhieuThu.GetFocusedRowCellValue("KyHieu").ToString();
            //var MaTT = (int?)gvPhieuThu.GetFocusedRowCellValue("TT");
            //var NgayThu = (DateTime?)gvPhieuThu.GetFocusedRowCellValue("NgayThu");
            ////if (query == null | MaTT==null)
            ////{
            ////    DialogBox.Error("Vui lòng kiểm tra HĐĐT có tồn tại không hoặc chưa có bản ghi phiếu thu.");
            ////    return;
            ////}
            //MasterDataContext db = new MasterDataContext(); 
            //var tam = new PortalSV.PortalServiceSoapClient();
            //var TK = db.TaiKhoanHDDTs.FirstOrDefault();
            //var t = tam.downloadInvPDFFkey(string.Format("{0}", 5485),TK.TKWebServies, TK.PassWebServies);
            //if (t.Contains("OK"))
            //{
            //    var PDF = Convert.FromBase64String(t);


            //    byte[] bytes;
            //    BinaryFormatter bf = new BinaryFormatter();
            //    MemoryStream ms = new MemoryStream();
            //    bf.Serialize(ms, PDF);
            //    bytes = ms.ToArray();
            //    var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + NgayThu.Value.Month + NgayThu.Value.Year + "\\"; //Application.StartupPath + "\\cach\\";
            //    if (!Directory.Exists(filePath))
            //        Directory.CreateDirectory(filePath);
            //    System.IO.File.WriteAllBytes(filePath + "/" + string.Format("{0}.pdf", MaCH), bytes);
            //    var Link = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //    DialogBox.Alert("Đã xuất file thành công.");
            //}
            //else
            //{
            //    DialogBox.Error(t);
            //}
        }

        private void itemGachBo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var a = (int) gvPhieuThu.GetFocusedRowCellValue("ID");
            var TT = (int?)gvPhieuThu.GetFocusedRowCellValue("TT");

            if (TT == 1 | TT == null)
            {
                DialogBox.Error("HĐĐT không tồn tại hoặc hóa đơn này đã được gạch nợ");
                return;
            }
            GachBo(a);

        }

        void BoGachGD(int? ID1)
        {
        //    if (DialogBox.Question("Bạn muốn tiếp tục hay không") == DialogResult.No) return;
        //    MasterDataContext dbo = new MasterDataContext();


        //    var tam = new BusinessSV.BusinessServiceSoapClient();
        //    var a = tam.UnConfirmPaymentFkey(string.Format("{0}", ID1), "mikhomeservice", "Abc@123456");
        //    var PT = dbo.ptPhieuThus.SingleOrDefault(p => p.ID == ID1);
          

        //    if (a.Contains("OK"))
        //    {
        //        if (PT != null)
        //        {
        //            PT.TrangThaiHDDT = 0;
        //            dbo.SubmitChanges();
        //            this.RefreshData();
        //        }
        //    }
        //    else
        //    {
        //        DialogBox.Alert(a.ToString());
        //    }
            
        }
       

        private void itemBoGach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var a = (int?) gvPhieuThu.GetFocusedRowCellValue("ID");
            var TT = (int?)gvPhieuThu.GetFocusedRowCellValue("TT");
            if (a == null | TT == null | TT==0)
            {
                DialogBox.Error("HĐĐT này không tồn tại hoặc trạng thái đã được bỏ gạch hóa đơn");
                return;
            }
            BoGachGD(a);
        }
        void HoaDonDienTu(int ID)
        {
            //if (DialogBox.Question("Bạn muốn tiếp tục hay không") == DialogResult.No) return;
            //MasterDataContext db = new MasterDataContext();
            //var PT = (from pt in db.ptPhieuThus
            //          join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
            //          join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
            //          join hd in db.dvHoaDons on ptct.LinkID equals hd.ID
            //          join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
            //          where pt.ID == ID & hd.MaLDV!=9
            //          select new
            //          {
            //              pt.SoPT,
            //              SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
            //              TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
            //              pt.LyDo,
            //              DiaChiNN = kh.DCLL,
            //              PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
            //              pt.NgayThu,
            //              ptct.DienGiai,
            //              ldv.TenLDV,
            //              //ldv.DonViTinh,
            //              kh.CtyMaSoThue,
            //              kh.KyHieu,
            //              kh.DienThoaiKH,
            //              hd.MaLDV,
            //              hd.LinkID,
            //              SoTienCT = ptct.SoTien
            //          }
            //    ).ToList();
            //if (PT.Count > 0)
            //{
            //    var objTien = new TienTeCls();
            //    XmlDocument xmlMasterObject = new XmlDocument();
            //    string xml = "";
            //    xml += "<Invoices>";
            //    //foreach (var i in PT[])
            //    // {
            //    xml += "<Inv>";
            //    xml += string.Format("<key>{0}</key>", (int)ID);
            //    xml += "<Invoice>";
            //    xml += string.Format("<CusCode>{0}</CusCode>", PT.First().KyHieu);
            //    xml += string.Format("<CusName>{0}</CusName>", PT.First().TenKH);
            //    xml += string.Format("<CusAddress>{0}</CusAddress>", PT.First().DiaChiNN);
            //    xml += string.Format("<CusPhone>{0}</CusPhone>", PT.First().DienThoaiKH);
            //    xml += string.Format("<CusTaxCode>{0}</CusTaxCode>", PT.First().CtyMaSoThue);
            //    xml += string.Format("<PaymentMethod>{0}</PaymentMethod>", PT.First().PTTT);
            //    xml += string.Format("<KindOfService>{0:dd/MM/yyyy}</KindOfService>", PT.First().NgayThu);
            //    var VATNuoc =
            //        PT.First().MaLDV == 9
            //            ? 15
            //            : 10;
            //    decimal ChiaTyLe =
            //        PT.First().MaLDV == 9
            //            ? (decimal) 1.15
            //            : (decimal) 1.1;
            //    var TruocVAt = PT.First().SoTien / ChiaTyLe;
            //    var VAT =
            //        PT.First().SoTien - TruocVAt;
            //    xml += "<Products>";
            //    foreach (var j in PT)
            //    {

            //        xml += "<Product>";
            //        var SL =
            //            j.MaLDV == 9
            //            ? 
            //            (
            //            db.dvNuocs.SingleOrDefault(p => p.ID == j.LinkID) == null ? (decimal)1 : (decimal)db.dvNuocs.Single(p => p.ID == j.LinkID).SoTieuThu
            //            )
            //                : (PT.First().MaLDV == 8 ? db.dvDiens.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu.GetValueOrDefault() : (decimal) 1);


            //        xml += string.Format("<ProdName>{0}</ProdName>", j.TenLDV);
            //        //xml += string.Format("<ProdUnit>{0}</ProdUnit>", j.DonViTinh);
            //        xml += string.Format("<ProdQuantity>{0}</ProdQuantity>", Math.Round((decimal)SL,0,MidpointRounding.AwayFromZero));
            //        xml += string.Format("<ProdPrice>{0}</ProdPrice>",
            //            Math.Round((decimal) ((j.SoTienCT/ChiaTyLe)/SL), 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<Amount>{0}</Amount>",
            //            Math.Round((decimal) (j.SoTienCT/ChiaTyLe), 0, MidpointRounding.AwayFromZero));
            //        xml += "</Product>";

            //    }
            //    xml += "</Products>";
            //    xml += string.Format("<Total>{0}</Total>",
            //        Math.Round((decimal) TruocVAt, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<DiscountAmount>{0}</DiscountAmount>", 0);
            //    xml += string.Format("<VATRate>{0}</VATRate>",
            //        Math.Round((decimal) VATNuoc, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<VATAmount>{0}</VATAmount>",
            //        Math.Round((decimal) VAT, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<Amount>{0}</Amount>",
            //        Math.Round((decimal) PT.First().SoTien, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<AmountInWords>{0}</AmountInWords>",
            //        objTien.DocTienBangChu((decimal) PT.First().SoTien.Value, "đồng chẵn"));
            //    xml += "<Extra>1</Extra>";
            //    xml += string.Format("<ArisingDate>{0:dd/MM/yyyy}</ArisingDate>", PT.First().NgayThu);
            //    xml +=
            //        "<PaymentStatus>1</PaymentStatus>";
            //    xml += "</Invoice>";
            //    xml += "</Inv>";
            //    //  }

            //    //xml += "<Inv></Inv>";
            //    xml += "</Invoices>";
            //    xmlMasterObject.LoadXml(xml);
            //    var tam = new PublicSV.PublishServiceSoapClient();
            //    var TK = db.TaiKhoanHDDTs.FirstOrDefault();
            //    var a = tam.ImportAndPublishInv(TK.TKAmin, TK.PassAmin, xml, TK.TKWebServies, TK.PassWebServies, string.Format("{0}", "01GTKT0/001"), string.Format("{0}", "MH/17E"), 0);
            //    if(a.Contains("OK"))
            //    {
            //        MasterDataContext dbo = new MasterDataContext();
            //        var PT1 = dbo.ptPhieuThus.SingleOrDefault(p => p.ID == ID);
            //        PT1.TrangThaiHDDT = 1;
            //        dbo.SubmitChanges();
            //    }
            //    else
            //    {
            //        DialogBox.Alert(a.ToString());
            //    }
                
            //}
            //else
            //{
            //    var PT2 = (from pt in db.ptPhieuThus
            //              join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
            //              join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT

            //               where pt.ID == (int)ID
            //              select new
            //              {
            //                  pt.SoPT,
            //                  SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
            //                  TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
            //                  pt.LyDo,
            //                  DiaChiNN = kh.DCLL,
            //                  PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
            //                  pt.NgayThu,
            //                 // ptct.DienGiai,
            //                  //ldv.TenLDV,
            //                  //ldv.DonViTinh,
            //                  kh.CtyMaSoThue,
            //                  kh.KyHieu,ptct.DienGiai,
            //                  kh.DienThoaiKH,
            //                 // hd.MaLDV,
            //                  //hd.LinkID,
            //                  SoTienCT = ptct.SoTien
            //              }
            //    ).ToList();
            //    var objTien = new TienTeCls();
            //    XmlDocument xmlMasterObject = new XmlDocument();
            //    string xml = "";
            //    xml += "<Invoices>";
            //    //foreach (var i in PT2[])
            //    // {
            //    xml += "<Inv>";
            //    xml += string.Format("<key>{0}</key>", ID);
            //    xml += "<Invoice>";
            //    xml += string.Format("<CusCode>{0}</CusCode>", PT2.First().KyHieu);
            //    xml += string.Format("<CusName>{0}</CusName>", PT2.First().TenKH);
            //    xml += string.Format("<CusAddress>{0}</CusAddress>", PT2.First().DiaChiNN);
            //    xml += string.Format("<CusPhone>{0}</CusPhone>", PT2.First().DienThoaiKH);
            //    xml += string.Format("<CusTaxCode>{0}</CusTaxCode>", PT2.First().CtyMaSoThue);
            //    xml += string.Format("<PaymentMethod>{0}</PaymentMethod>", PT2.First().PTTT);
            //    xml += string.Format("<KindOfService>{0:dd/MM/yyyy}</KindOfService>", PT2.First().NgayThu);
            //    var VATNuoc =
                   
            //            10;
            //    decimal ChiaTyLe =
            //        (decimal)1.1;
            //    var TruocVAt = PT2.First().SoTien / ChiaTyLe;
            //    var VAT =
            //        PT2.First().SoTien - TruocVAt;
            //    xml += "<Products>";
            //    foreach (var j in PT2)
            //    {

            //        xml += "<Product>";
                

            //        xml += string.Format("<ProdName>{0}</ProdName>", j.DienGiai);
            //        xml += string.Format("<ProdUnit>{0}</ProdUnit>", "cái");
            //        xml += string.Format("<ProdQuantity>{0}</ProdQuantity>", 1);
            //        xml += string.Format("<ProdPrice>{0}</ProdPrice>",
            //            Math.Round((decimal)((j.SoTienCT / ChiaTyLe) / 1), 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<Amount>{0}</Amount>",
            //            Math.Round((decimal)(j.SoTienCT / ChiaTyLe), 0, MidpointRounding.AwayFromZero));
            //        xml += "</Product>";

            //    }
            //    xml += "</Products>";
            //    xml += string.Format("<Total>{0}</Total>",
            //        Math.Round((decimal)TruocVAt, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<DiscountAmount>{0}</DiscountAmount>", 0);
            //    xml += string.Format("<VATRate>{0}</VATRate>",
            //        Math.Round((decimal)VATNuoc, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<VATAmount>{0}</VATAmount>",
            //        Math.Round((decimal)VAT, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<Amount>{0}</Amount>",
            //        Math.Round((decimal)PT2.First().SoTien, 0, MidpointRounding.AwayFromZero));
            //    xml += string.Format("<AmountInWords>{0}</AmountInWords>",
            //        objTien.DocTienBangChu((decimal)PT2.First().SoTien.Value, "đồng chẵn"));
            //    xml += "<Extra>1</Extra>";
            //    xml += string.Format("<ArisingDate>{0:dd/MM/yyyy}</ArisingDate>", PT2.First().NgayThu);
            //    xml +=
            //        "<PaymentStatus>1</PaymentStatus>";
            //    xml += "</Invoice>";
            //    xml += "</Inv>";
            //    //  }

            //    //xml += "<Inv></Inv>";
            //    xml += "</Invoices>";
            //    xmlMasterObject.LoadXml(xml);
            //    var tam = new PublicSV.PublishServiceSoapClient();
            //    var TK = db.TaiKhoanHDDTs.FirstOrDefault();
            //    var a = tam.ImportAndPublishInv(TK.TKAmin, TK.PassAmin, xml, TK.TKWebServies, TK.PassWebServies, string.Format("{0}", "01GTKT0/001"), string.Format("{0}", "MH/17E"), 0);
            //    if (a.Contains("OK"))
            //    {
            //        MasterDataContext dbo = new MasterDataContext();
            //        var PT1 = dbo.ptPhieuThus.SingleOrDefault(p => p.ID == ID);
            //        PT1.TrangThaiHDDT = 1;
            //        dbo.SubmitChanges();
            //    }
            //    else
            //    {
            //        DialogBox.Alert(a.ToString());
            //    }
            //}
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvPhieuThu.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu");
                return;
            }

            //if (DialogBox.QuestionDelete() == DialogResult.No) return;
            if (DialogBox.Question("Bạn muốn tiếp tục hay không") == DialogResult.No) return;
            //var db = new MasterDataContext();

            foreach (var i in indexs)
            {
               
                var TT = (int?)gvPhieuThu.GetRowCellValue(i,"TT");
                var ID = (int?)gvPhieuThu.GetRowCellValue(i, "ID");
                if (ID != null & (TT == 0 | TT == null))
                {
                    HoaDonDienTu((int)ID);

                   
                }

                else
                {
                    DialogBox.Alert("Hóa đơn đã được phát hành hoặc đã được phát hành");

                }
            }
            this.RefreshData();



        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            var indexs = gvPhieuThu.GetSelectedRows();
            //using (var frm = new frmEdit())
            //{
            //    frm.MaPT = id;
            //    frm.MaTN = (byte)itemToaNha.EditValue;
              //  frm.ShowDialog();
               // if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
               // {

                    this.RefreshData();
                    foreach (var i in indexs)
            {
                if (i != null)
                {
                    try
                    {
                        var id = (int?)gvPhieuThu.GetRowCellValue(i,"ID");
                        if (id == null)
                        {
                            DialogBox.Alert("Vui lòng chọn mẫu tin");
                            return;
                        }
                        HoaSuaDonDienTu((int)id);
                        // ThayTheHoaDon();
                    }
                    catch (Exception)
                    {

                        DialogBox.Error("Đã xảy ra lỗi trong quá trình chỉnh sửa HĐĐT. Vui lòng kiểm tra lại");
                    }

                }
            }
                    

            //    }

            //}
        }

        private void itemGop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)gvPhieuThu.GetFocusedRowCellValue("ID");
            //var MaTN = (byte)itemToaNha.EditValue;
            //var MaKH = (int?)gvPhieuThu.GetFocusedRowCellValue("MaKH");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn dòng dữ liệu");
            //    return;
            //}
            //var m =new frmGopPT();
            //m.ID = id;
            //m.MaTN = MaTN;
            //m.MaKH = MaKH;
            //m.ShowDialog();
            //LoadData();
        }
        void XuatGop(int MaTN,int MaKH,int ID,int? TT)
        {
            //var tuNgay = (DateTime)itemTuNgay.EditValue;
            //var denNgay = (DateTime)itemDenNgay.EditValue;
            //using (var db = new MasterDataContext())
            //{
            //    var Data = (from pt in db.ptPhieuThus //join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
            //                    join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
            //                    join nv in db.tnNhanViens on pt.MaNV equals nv.MaNV
            //                    where
            //                    pt.MaTN == MaTN & (pt.IsGop.GetValueOrDefault() == false)
            //                    & pt.ID != ID &
            //                    pt.MaKH == MaKH
            //                        & pt.TrangThaiHDDT == null
            //                        & SqlMethods.DateDiffDay(tuNgay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, denNgay) >= 0
            //                    select new 
            //                    {
            //                        TrangThai = pt.TrangThaiHDDT == 1 ? "Đã phát hành HĐĐT" : (pt.TrangThaiHDDT == 0 ? "Gạch bỏ" : "Chưa xuất HĐĐT"),
            //                        TenKH = (bool)kh.IsCaNhan ? String.Format("{0} {1}", kh.HoKH, kh.TenKH) : kh.CtyTen,
            //                        ID = pt.ID,
            //                        SoPT = pt.SoPT,
            //                        IsGop = pt.IsGop.GetValueOrDefault(),
            //                        //SoTien = (decimal)pt.ptChiTietPhieuThus.Sum(p=>p.SoTien),
            //                        // NgayThu=(DateTime)pt.NgayThu,
            //                        HoTenNV = nv.HoTenNV
            //                    }
            //                        ).ToList();
            //    if (Data.Count() == 0 & (db.ptPhieuThus.Single(p => p.ID == ID).TrangThaiHDDT == 0 | db.ptPhieuThus.Single(p => p.ID == ID).TrangThaiHDDT == null))
            //    {


            //        if (ID != null & (db.ptPhieuThus.Single(p => p.ID == ID).TrangThaiHDDT == 0 | db.ptPhieuThus.Single(p => p.ID == ID).TrangThaiHDDT == null))
            //        {
            //            HoaDonDienTu((int)ID);
            //            return;

            //        }
                   
            //    }
            //    if (Data.Count() == 0 &
            //        (db.ptPhieuThus.Single(p => p.ID == ID).TrangThaiHDDT != 0 |
            //         db.ptPhieuThus.Single(p => p.ID == ID).TrangThaiHDDT != null))
            //    {
            //        return;
            //    }
            //    List<int> termsList = new List<int>();
            //    foreach (var i in Data)
                
                    
                
            //    {
            //        //if ((bool)i.IsGop == true)
            //        //{
            //            var id = i.ID;
            //            termsList.Add(id);
            //            var capnhat = db.ptPhieuThus.Single(p => p.ID == id);
            //            capnhat.IsGop = true;
            //            capnhat.TrangThaiHDDT = 1;
            //            capnhat.ParentID = ID;
            //            var LS = new LichSuGopHD();
            //            LS.MaPT1 = ID;
            //            LS.MaPT2 = id;
            //            LS.NgayNhap = DateTime.Now;
            //            LS.MaNVN = Common.User.MaNV;
            //            db.LichSuGopHDs.InsertOnSubmit(LS);
            //       // }

            //    }
            //    var capnhat2 = db.ptPhieuThus.Single(p => p.ID == ID);
            //    capnhat2.IsGop = true;
            //    capnhat2.TrangThaiHDDT = 1;
            //    capnhat2.IsPhieuGoc = true;
            //    termsList.Add(ID);
            //    var PT = (from pt in db.ptPhieuThus
            //              join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
            //              join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT
            //              join hd in db.dvHoaDons on ptct.LinkID equals hd.ID
            //              join ldv in db.dvLoaiDichVus on hd.MaLDV equals ldv.ID
            //              where //pt.ID == (int)grvGop.GetFocusedRowCellValue("ID")
            //                  termsList.Contains(pt.ID)
            //              select new
            //              {
            //                  pt.SoPT,
            //                  SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
            //                  TenKH =
            //                      kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
            //                  pt.LyDo,
            //                  DiaChiNN = kh.DCLL,
            //                  PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
            //                  pt.NgayThu,
            //                  ptct.DienGiai,
            //                  ldv.TenLDV,
            //                  ldv.DonViTinh,
            //                  kh.CtyMaSoThue,
            //                  kh.KyHieu,
            //                  kh.DienThoaiKH,
            //                  hd.MaLDV,
            //                  hd.LinkID,
            //                  SoTienCT = ptct.SoTien
            //              }
            //        ).ToList();
            //    if (PT.Count > 0)
            //    {
            //        var objTien = new TienTeCls();
            //        XmlDocument xmlMasterObject = new XmlDocument();
            //        string xml = "";
            //        xml += "<Invoices>";
            //        //foreach (var i in PT[])
            //        // {
            //        xml += "<Inv>";
            //        xml += string.Format("<key>{0}</key>", ID);
            //        xml += "<Invoice>";
            //        xml += string.Format("<CusCode>{0}</CusCode>", PT.First().KyHieu);
            //        xml += string.Format("<CusName>{0}</CusName>", PT.First().TenKH);
            //        xml += string.Format("<CusAddress>{0}</CusAddress>", PT.First().DiaChiNN);
            //        xml += string.Format("<CusPhone>{0}</CusPhone>", PT.First().DienThoaiKH);
            //        xml += string.Format("<CusTaxCode>{0}</CusTaxCode>", PT.First().CtyMaSoThue);
            //        xml += string.Format("<PaymentMethod>{0}</PaymentMethod>", PT.First().PTTT);
            //        xml += string.Format("<KindOfService>{0:dd/MM/yyyy}</KindOfService>", PT.First().NgayThu);
            //        var VATNuoc =

            //            10;
            //        decimal ChiaTyLe =

            //            (decimal)1.1;
            //        var TruocVAt = PT.Sum(p => p.SoTienCT).GetValueOrDefault() / ChiaTyLe;
            //        var VAT =
            //            PT.Sum(p => p.SoTienCT).GetValueOrDefault() - TruocVAt;
            //        xml += "<Products>";
            //        foreach (var j in PT)
            //        {

            //            xml += "<Product>";
            //            var SL =
            //                j.MaLDV == 9
            //                    ? (
            //                        db.dvNuocs.SingleOrDefault(p => p.ID == j.LinkID) == null
            //                            ? (decimal)1
            //                            : (decimal)
            //                                db.dvNuocs.Single(p => p.ID == j.LinkID).SoTieuThu.GetValueOrDefault()
            //                        )
            //                    : (PT.First().MaLDV == 8
            //                        ? db.dvDiens.SingleOrDefault(p => p.ID == j.LinkID).SoTieuThu.GetValueOrDefault()
            //                        : (decimal)1);


            //            xml += string.Format("<ProdName>{0}</ProdName>", j.TenLDV);
            //            xml += string.Format("<ProdUnit>{0}</ProdUnit>", j.DonViTinh);
            //            xml += string.Format("<ProdQuantity>{0}</ProdQuantity>",
            //                Math.Round((decimal)SL, 0, MidpointRounding.AwayFromZero));
            //            xml += string.Format("<ProdPrice>{0}</ProdPrice>",
            //                Math.Round((decimal)((j.SoTienCT / ChiaTyLe) / SL), 0, MidpointRounding.AwayFromZero));
            //            xml += string.Format("<Amount>{0}</Amount>",
            //                Math.Round((decimal)(j.SoTienCT / ChiaTyLe), 0, MidpointRounding.AwayFromZero));
            //            xml += "</Product>";

            //        }
            //        xml += "</Products>";
            //        xml += string.Format("<Total>{0}</Total>",
            //            Math.Round((decimal)TruocVAt, 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<DiscountAmount>{0}</DiscountAmount>", 0);
            //        xml += string.Format("<VATRate>{0}</VATRate>",
            //            Math.Round((decimal)VATNuoc, 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<VATAmount>{0}</VATAmount>",
            //            Math.Round((decimal)VAT, 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<Amount>{0}</Amount>",
            //            Math.Round((decimal)PT.Sum(p => p.SoTienCT).GetValueOrDefault(), 0,
            //                MidpointRounding.AwayFromZero));
            //        xml += string.Format("<AmountInWords>{0}</AmountInWords>",
            //            objTien.DocTienBangChu((decimal)PT.Sum(p => p.SoTienCT).GetValueOrDefault(), "đồng chẵn"));
            //        xml += "<Extra>1</Extra>";
            //        xml += string.Format("<ArisingDate>{0:dd/MM/yyyy}</ArisingDate>", PT.First().NgayThu);
            //        xml +=
            //            "<PaymentStatus>1</PaymentStatus>";
            //        xml += "</Invoice>";
            //        xml += "</Inv>";
            //        //  }

            //        //xml += "<Inv></Inv>";
            //        xml += "</Invoices>";
            //        xmlMasterObject.LoadXml(xml);
            //        var tam = new PublicSV.PublishServiceSoapClient();
            //        var TK = db.TaiKhoanHDDTs.FirstOrDefault();
            //        var a = tam.ImportAndPublishInv(TK.TKAmin, TK.PassAmin, xml, TK.TKWebServies, TK.PassWebServies,
            //            string.Format("{0}", "01GTKT0/001"), string.Format("{0}", "MH/17E"), 0);
            //        if (a.Contains("OK"))
            //        {
            //            db.SubmitChanges();
            //        }
            //        else
            //        {
            //            DialogBox.Alert(a.ToString());
            //        }

            //    }
            //    else
            //    {
            //        var PT2 = (from pt in db.ptPhieuThus
            //                   join kh in db.tnKhachHangs on pt.MaKH equals kh.MaKH
            //                   join ptct in db.ptChiTietPhieuThus on pt.ID equals ptct.MaPT

            //                   where termsList.Contains(pt.ID)
            //                   select new
            //                   {
            //                       pt.SoPT,
            //                       SoTien = pt.ptChiTietPhieuThus.Sum(p => p.SoTien),
            //                       TenKH =
            //                           kh.IsCaNhan.GetValueOrDefault()
            //                               ? kh.HoKH.ToString() + " " + kh.TenKH.ToString()
            //                               : kh.CtyTen,
            //                       pt.LyDo,
            //                       DiaChiNN = kh.DCLL,
            //                       PTTT = pt.MaTKNH == null ? "Tiền mặt" : "Chuyển khoản",
            //                       pt.NgayThu,
            //                       // ptct.DienGiai,
            //                       //ldv.TenLDV,
            //                       //ldv.DonViTinh,
            //                       kh.CtyMaSoThue,
            //                       kh.KyHieu,
            //                       ptct.DienGiai,
            //                       kh.DienThoaiKH,
            //                       // hd.MaLDV,
            //                       //hd.LinkID,
            //                       SoTienCT = ptct.SoTien
            //                   }
            //            ).ToList();
            //        var objTien = new TienTeCls();
            //        XmlDocument xmlMasterObject = new XmlDocument();
            //        string xml = "";
            //        xml += "<Invoices>";
            //        //foreach (var i in PT2[])
            //        // {
            //        xml += "<Inv>";
            //        xml += string.Format("<key>{0}</key>", ID);
            //        xml += "<Invoice>";
            //        xml += string.Format("<CusCode>{0}</CusCode>", PT2.First().KyHieu);
            //        xml += string.Format("<CusName>{0}</CusName>", PT2.First().TenKH);
            //        xml += string.Format("<CusAddress>{0}</CusAddress>", PT2.First().DiaChiNN);
            //        xml += string.Format("<CusPhone>{0}</CusPhone>", PT2.First().DienThoaiKH);
            //        xml += string.Format("<CusTaxCode>{0}</CusTaxCode>", PT2.First().CtyMaSoThue);
            //        xml += string.Format("<PaymentMethod>{0}</PaymentMethod>", PT2.First().PTTT);
            //        xml += string.Format("<KindOfService>{0:dd/MM/yyyy}</KindOfService>", PT2.First().NgayThu);
            //        var VATNuoc =

            //            10;
            //        decimal ChiaTyLe =
            //            (decimal)1.1;
            //        var TruocVAt = PT2.First().SoTien / ChiaTyLe;
            //        var VAT =
            //            PT2.First().SoTien - TruocVAt;
            //        xml += "<Products>";
            //        foreach (var j in PT2)
            //        {

            //            xml += "<Product>";


            //            xml += string.Format("<ProdName>{0}</ProdName>", j.DienGiai);
            //            xml += string.Format("<ProdUnit>{0}</ProdUnit>", "cái");
            //            xml += string.Format("<ProdQuantity>{0}</ProdQuantity>", 1);
            //            xml += string.Format("<ProdPrice>{0}</ProdPrice>",
            //                Math.Round((decimal)((j.SoTienCT / ChiaTyLe) / 1), 0, MidpointRounding.AwayFromZero));
            //            xml += string.Format("<Amount>{0}</Amount>",
            //                Math.Round((decimal)(j.SoTienCT / ChiaTyLe), 0, MidpointRounding.AwayFromZero));
            //            xml += "</Product>";

            //        }
            //        xml += "</Products>";
            //        xml += string.Format("<Total>{0}</Total>",
            //            Math.Round((decimal)TruocVAt, 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<DiscountAmount>{0}</DiscountAmount>", 0);
            //        xml += string.Format("<VATRate>{0}</VATRate>",
            //            Math.Round((decimal)VATNuoc, 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<VATAmount>{0}</VATAmount>",
            //            Math.Round((decimal)VAT, 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<Amount>{0}</Amount>",
            //            Math.Round((decimal)PT2.First().SoTien, 0, MidpointRounding.AwayFromZero));
            //        xml += string.Format("<AmountInWords>{0}</AmountInWords>",
            //            objTien.DocTienBangChu((decimal)PT2.First().SoTien.Value, "đồng chẵn"));
            //        xml += "<Extra>1</Extra>";
            //        xml += string.Format("<ArisingDate>{0:dd/MM/yyyy}</ArisingDate>", PT2.First().NgayThu);
            //        xml +=
            //            "<PaymentStatus>1</PaymentStatus>";
            //        xml += "</Invoice>";
            //        xml += "</Inv>";
            //        //  }

            //        //xml += "<Inv></Inv>";
            //        xml += "</Invoices>";
            //        xmlMasterObject.LoadXml(xml);
            //        var tam = new PublicSV.PublishServiceSoapClient();
            //        var TK = db.TaiKhoanHDDTs.FirstOrDefault();
            //        var a = tam.ImportAndPublishInv(TK.TKAmin, TK.PassAmin, xml, TK.TKWebServies, TK.PassWebServies,
            //            string.Format("{0}", "01GTKT0/001"), string.Format("{0}", "MH/17E"), 0);
            //        if (a.Contains("OK"))
            //        {
            //            MasterDataContext dbo = new MasterDataContext();
            //            var PT1 = dbo.ptPhieuThus.SingleOrDefault(p => p.ID == ID);
            //            PT1.TrangThaiHDDT = 1;
            //            dbo.SubmitChanges();
            //        }
            //        else
            //        {
            //            DialogBox.Alert(a.ToString());
            //        }

            //    }
            //}
            
            
            ////---------------------------------------------
           
         

        }

       

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           var indexs = gvPhieuThu.GetSelectedRows();

            if (indexs.Length == 0)
            {
                DialogBox.Alert("Vui lòng chọn mẫu tin m");
                return;
            }

            //if (DialogBox.QuestionDelete() == DialogResult.No) return;

            //var db = new MasterDataContext();

            foreach (var i in indexs)
            {
                var id = (int) gvPhieuThu.GetRowCellValue(i, "ID");
                var MaTN = (byte) itemToaNha.EditValue;
                var MaKH = (int) (int) gvPhieuThu.GetRowCellValue(i, "MaKH");
                var TT = (int?)gvPhieuThu.GetRowCellValue(i, "TT");
                XuatGop(MaTN, MaKH, id,TT);
            }
            //var m = new frmGopPT();
            //m.ID = id;
            //m.MaTN = MaTN;
            //m.MaKH = MaKH;
            //m.ShowDialog();
            //LoadData();
        }
    }
}