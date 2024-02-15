using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Linq;
using System.Linq;
using Library;

namespace DichVu.GiuXe
{
    public partial class SubPhieuDangKy : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public SubPhieuDangKy(int? MaCapThe)
        {
            InitializeComponent();

            #region DataBindding
            cSTT.DataBindings.Add("Text", null, "STT");
            cBienSo.DataBindings.Add("Text", null, "BienSo");
            cLoaiXe.DataBindings.Add("Text", null, "TenLX");
            cHangXe.DataBindings.Add("Text", null, "HangXe");
            cMauSac.DataBindings.Add("Text", null, "MauSac");
            cSoThe.DataBindings.Add("Text", null, "SoThe");
            cMoi.DataBindings.Add("Text", null, "Moi");
            cTheTichHop.DataBindings.Add("Text", null, "TheTichHop");
            cHuy.DataBindings.Add("Text", null, "Huy");
            #endregion

            var objPhieu = (from o in db.KhoThe_DSCapThes
                            join nk in db.tnNhanKhaus on o.MaNhanKhau equals nk.ID into dsnk
                            from nk in dsnk.DefaultIfEmpty()
                            where o.ID == MaCapThe
                            select new
                            {
                                o.SoCT,
                                o.NgayNhap,
                                o.mbMatBang.MaSoMB,
                                nk.HoTenNK,
                                o.MaNhanKhau,
                                o.SDT,
                                o.Email,
                                TenKH = o.tnKhachHang == null ? "" : ((bool)o.tnKhachHang.IsCaNhan ? o.tnKhachHang.HoKH + " " + o.tnKhachHang.TenKH : o.tnKhachHang.CtyTen),
                                o.GhiChu,
                            }).FirstOrDefault();

            cSoDK.Text = objPhieu.SoCT;
            cNgayDK.Text = string.Format(cNgayDK.Text, objPhieu.NgayNhap);
            cSoCanHo.Text = objPhieu.MaSoMB;
            cTenCuDan.Text = objPhieu.HoTenNK;
            cSDT.Text = objPhieu.SDT;
            cEmail.Text = objPhieu.Email;
            cChuHo.Text = objPhieu.TenKH;
            cGhiChu.Text = objPhieu.GhiChu;
            var ltCapThe = (from tx in db.CapThe_ChiTiets
                               join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into loaixe from lx in loaixe.DefaultIfEmpty()
                               join t in db.KhoThe_DSCapThes on tx.MaCapThe equals t.ID
                               join cu in db.dvgxTheXes on tx.MaTheCu equals cu.ID into dscu from cu in dscu.DefaultIfEmpty()
                               join moi in db.dvgxTheXes on tx.MaThe equals moi.ID into dsmoi from moi in dsmoi.DefaultIfEmpty()
                               where tx.MaCapThe == MaCapThe
                               select new
                               {
                                   t.NgayNhap,
                                   moi.SoThe,
                                   MaThe = moi.SoThe,
                                   MaTheCu = cu.SoThe,
                                   TenLX = tx.LoaiThe == 1 ? "" : lx.TenLX,
                                   tx.MaLoaiDK,
                                   BienSo = tx.LoaiThe == 1 ? "" :  tx.BienSo ,
                                   tx.ChuThe,
                                   HangXe = tx.LoaiThe == 1 ? "" : tx.HangXe,
                                   MauSac = tx.LoaiThe == 1 ? "" : tx.MauSac,
                                   tx.PhiGiuXe,
                                   tx.KyTT,
                                   tx.NgayTT,
                                   Moi = tx.MaLoaiDK == 1 ? "x" : "",
                                   TheTichHop = tx.MaLoaiDK == 2 ? "x" : "",
                                   Huy = tx.MaLoaiDK == 3 ? "x" : "",
                                   tx.TheCuBackupID,
                               }).AsEnumerable()
                               .Select((p, Index) => new ItemDangKy
                               {
                                   STT = Index +1,
                                   SoThe =  p.MaLoaiDK == 1 ? p.MaThe
                                           : (p.MaLoaiDK == 2 | p.MaLoaiDK == 3) ? p.MaTheCu : "", 
                                   BienSo = p.BienSo,
                                   TenLX = p.TenLX,
                                   HangXe = p.HangXe,
                                   MauSac = p.MauSac,
                                   Moi = p.Moi,
                                   TheTichHop = p.TheTichHop,
                                   Huy = p.Huy,
                                   TheCuBackupID = p.TheCuBackupID,
                                   MaLoaiDK = p.MaLoaiDK,
                               }).ToList();

            // Kiểm tra trương hợp nếu tích hợp thẻ thang máy thêm thẻ xe ==> Thêm mới
            // Lấy thông tin lịch sử thẻ cũ trước khi cấp thẻ
            foreach (var item in ltCapThe)
            {
                if (item.MaLoaiDK == 2) // tích hợp thẻ
                {
                    var TheCuBK = db.dvgxTheXe_Backups.Single(o => o.ID == item.TheCuBackupID);
                    if (TheCuBK.IsThangMay == true)
                    {
                        item.Moi = "x";
                        item.TheTichHop = item.Huy = "";

                    }
                }
            }

            this.DataSource = ltCapThe;


        }

        public class ItemDangKy
        {
            public int STT { get; set; }
            public string SoThe { get; set; }
            public string BienSo { get; set; }
            public string TenLX { get; set; }
            public string HangXe { get; set; }
            public string MauSac { get; set; }
            public string Moi { get; set; }
            public string TheTichHop { get; set; }
            public string Huy { get; set; }
            public int? TheCuBackupID { get; set; }
            public int? MaLoaiDK { get; set; }

        }

    }
}
