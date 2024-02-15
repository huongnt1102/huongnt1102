using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Linq;
using System.Linq;
using Library;

namespace DichVu.NhanKhau
{
    public partial class rptMauDangKy : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public rptMauDangKy(int? MaMB)
        {
            InitializeComponent();
            #region Data Binding
            cSTT.DataBindings.Add("Text",null,"STT");
            cTenNK.DataBindings.Add("Text", null, "HoTenNK");
            cNgaySinhNK.DataBindings.Add("Text", null, "NgaySinh","{0:dd/MM/yyyy}");
            cPassportNK.DataBindings.Add("Text", null, "CMND");
            cQuanHeNK.DataBindings.Add("Text", null, "QuanHe");
            cDienThoaiNK.DataBindings.Add("Text", null, "Email");
            #endregion


            var objMB = (from mb in db.mbMatBangs
                         join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                         join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                         join ct in db.tnKhachHangs on mb.MaKHF1 equals ct.MaKH into dsct from ct in dsct.DefaultIfEmpty()
                         where mb.MaMB == MaMB
                         select new
                         {
                             mb.MaSoMB,
                             mb.NgayBanGiao,
                             ChuMB = ((bool)kh.IsCaNhan ? kh.HoKH + " " + kh.TenKH : kh.CtyTen),
                             kh.QuocTich,
                             kh.NgaySinh,
                             kh.CMND,
                             kh.EmailKH,
                             kh.DienThoaiKH,
                             kh.NgayCap,
                             CtyDiaChi=kh.DCLL,
                             ChuMBF1 = ((bool)ct.IsCaNhan ? ct.HoKH + " " + ct.TenKH : ct.CtyTen),
                             QuocTichF1 = ct.QuocTich,
                             NgaySinhF1 = ct.NgaySinh,
                             CMNDF1 = ct.CMND,
                             EmailKHF1 = ct.EmailKH,
                             DienThoaiKHF1 = ct.DienThoaiKH,
                             NgayCapF1 = ct.NgayCap,
                             CtyDiaChiF1 = ct.DCLL,
                             mb.MaTT,
                             mb.MaKH,
                             mb.MaKHF1,
                             tn.Logo,
                         }).FirstOrDefault();

            cMaSoMB.Text = objMB.MaSoMB;
            checkHoanThien.Checked = objMB.MaTT == 61;
            checkXayTho.Checked = objMB.MaTT == 62;
            cNgayDKChu.Text = string.Format("{0:dd/MM/yyyy}", objMB.NgayBanGiao);
            cTenChuHo.Text = objMB.ChuMB;
            cQuocTichChu.Text = objMB.QuocTich;
            cNoiSinh.Text = objMB.CtyDiaChi;
            cCMND.Text = objMB.CMND;
            cNgayCap.Text = objMB.NgayCap;
            cEmail.Text = objMB.EmailKH;
            cTel.Text = objMB.DienThoaiKH;
            cNgaySinh.Text = string.Format("{0:dd/MM/yyyy}", objMB.NgaySinh);
            xrPictureBox1.ImageUrl = objMB.Logo;
            cNgayThang.Text = string.Format(cNgayThang.Text, DateTime.Now);
            if (objMB.MaKH != objMB.MaKHF1 & objMB.MaKHF1 != null)
            {
                cTenKHF1.Text = objMB.ChuMBF1;
                cQuocTichF1.Text = objMB.QuocTichF1;
                cNoiSinhF1.Text = objMB.CtyDiaChiF1;
                cCMNDF1.Text = objMB.CMNDF1;
                cNgayCapF1.Text = objMB.NgayCapF1;
                cEmailF1.Text = objMB.EmailKHF1;
                cTelF1.Text = objMB.DienThoaiKHF1;
                cNgaySinhF1.Text = string.Format("{0:dd/MM/yyyy}", objMB.NgaySinhF1);
            }

            this.DataSource = (from nk in db.tnNhanKhaus
                               where nk.MaMB == MaMB
                               select new
                               {
                                   nk.HoTenNK,
                                   nk.NgaySinh,
                                   nk.CMND,
                                   Email = nk.DienThoai != "" & nk.Email != "" ? nk.DienThoai + " - " + nk.Email
                                                                               : nk.DienThoai != "" ? nk.DienThoai
                                                                               : nk.Email != "" ? nk.Email
                                                                               : "",
                                   QuanHe = nk.tnQuanHe.Name,
                               }).AsEnumerable()
                              .Select((p, Index) => new
                              {
                                  STT = Index + 1,
                                  p.HoTenNK,
                                  p.NgaySinh,
                                  p.CMND,
                                  p.Email,
                                  p.QuanHe,
                              }).ToList();
        }

    }
}
