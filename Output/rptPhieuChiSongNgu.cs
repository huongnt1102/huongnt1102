using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;

namespace LandSoftBuilding.Fund.Output
{
    public partial class rptPhieuChiSongNgu : DevExpress.XtraReports.UI.XtraReport
    {
        public int _ID { get; set; }
        public int _MaTN { get; set; }
        public rptPhieuChiSongNgu(int ID, byte MaTN)
        {
            _ID = ID;
            _MaTN = MaTN;
            InitializeComponent();

            Library.frmPrintControl.LoadLayout(this, 3, MaTN);

            if (ID == 0) return;

            var db = new Library.MasterDataContext();
            try
            {
                #region Thong tin toa nha
                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == MaTN);
               // cTenTN.Text = objTN.CongTyQuanLy;
               // cDiaChiTN.Text = objTN.DiaChiCongTy;
               // cDienThoaiTN.Text = "Tel: " + objTN.DienThoai;
                //picLogo.ImageUrl = objTN.Logo;
                #endregion
                
                var Tien = db.tnTyGias.Single(p => p.MaTG == 2);
                var objTien = new TienTeCls();
                var objPT = (from p in db.pcPhieuChis
                             join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                             where p.ID == ID
                             select new
                             {
                                 p.MaTN,
                                 p.SoPC,
                                 p.NgayChi,
                                 p.NguoiNhan,MaKH=p.MaNCC,
                                 p.DiaChiNN,p.MaNVNhan,
                                 p.LyDo,
                                 p.SoTien,//p.IsLyDoPT,
                                 SoTien_BangChu = objTien.DocTienBangChu(p.SoTien.Value, "đồng chẵn"),
                                 nv.HoTenNV
                             }).FirstOrDefault();
                var objKH = db.tnKhachHangs.Where(p => p.MaKH == objPT.MaKH).FirstOrDefault();
           
                cPC.Text = string.Format("PHIẾU CHI/PAYMENT {0}", objPT.NgayChi.Value.Year);
                cKeToan.Text = "Thái Thị Mỹ Lệ"; //objPT.HoTenNV.ToUpper();
                cSoPhieu.Text = "Số/No: " + objPT.SoPC;
                cNgayPT.Text = string.Format("Ngày/date {0:dd} tháng/month {0:MM} năm/year {0:yyyy}", objPT.NgayChi);
                cNguoiNop.Text = objKH != null ? objPT.NguoiNhan : db.tnNhanViens.Single(p => p.MaNV == objPT.MaNVNhan).HoTenNV;
                cDiaChi.Text = objKH != null ? (objKH.DCLL) : "";
                cSoTien.Text = string.Format("{0:#,0.##} VND",objPT.SoTien.Value);
                //cSoTienBC.Text = objPT.SoTien_BangChu;
                //cNguoiLap.Text = objPT.HoTenNV;
                //cLyDoTA.Text =  GetDienGiaiThangLongTA();
                double _TongTien = Math.Round((double) (objPT.SoTien), 0, MidpointRounding.AwayFromZero);
                Library.NumberToEnglish num = new NumberToEnglish();
                //cTienBCTA.Text =cBangChuTA1.Text= string.Format("US Dollards {0}.",num.changeNumericToWords(_TongTien));
                cSoTienBC.Text = objPT.SoTien_BangChu;cTien.Text= string.Format("({0}VND)", num.changeNumericToWords(_TongTien));
               // cTienTA.Text = string.Format("~{0} USD", _TongTien);
               // Library.NumberToEnglish num=new NumberToEnglish();
                //cTienBCTA.Text = string.Format("US Dollards {0}.",num.changeNumericToWords(_TongTien));
                //Dien giai
                var strDienGiai = "";
                var ltChiTiet = (from ct in db.ptChiTietPhieuThus
                                 where ct.MaPT == ID
                                 select new
                                 {
                                     ct.DienGiai,
                                     ct.SoTien
                                 }).ToList();
                //string sDienGiai=GetDienGiaiThangLong();
                cLyDo.Text = objPT.LyDo;

            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }
       
    }
}
