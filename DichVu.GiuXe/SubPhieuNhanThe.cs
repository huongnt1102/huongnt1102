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
    public partial class SubPhieuNhanThe : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public SubPhieuNhanThe(int? MaCapThe)
        {
            InitializeComponent();

            #region DataBindding
            cSTT.DataBindings.Add("Text", null, "STT");
            cSoTheNgungSD.DataBindings.Add("Text", null, "MaTheCu");
            cBienSo.DataBindings.Add("Text", null, "BienSo");
            cLoaiThe.DataBindings.Add("Text", null, "TenLoaiThe");
            cSoTheDK.DataBindings.Add("Text", null, "MaThe");
            cLoaiXe.DataBindings.Add("Text", null, "TenLX");
            cBanGiao.DataBindings.Add("Text", null, "BanGiao");
            cDoiThe.DataBindings.Add("Text", null, "DoiThe");
            cMuaMoi.DataBindings.Add("Text", null, "Moi");
            cHuy.DataBindings.Add("Text", null, "Huy");
            cTichHop.DataBindings.Add("Text", null, "TheTichHop");
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

            var ltNhanThe = (from tx in db.CapThe_ChiTiets
                               join lx in db.dvgxLoaiXes on tx.MaLX equals lx.MaLX into loaixe
                               from lx in loaixe.DefaultIfEmpty()
                               join t in db.KhoThe_DSCapThes on tx.MaCapThe equals t.ID
                               join lt in db.Kho_LoaiThes on tx.LoaiThe equals lt.ID into dslt from lt in dslt.DefaultIfEmpty()
                               join cu in db.dvgxTheXes on tx.MaTheCu equals cu.ID into dscu
                               from cu in dscu.DefaultIfEmpty()
                               join moi in db.dvgxTheXes on tx.MaThe equals moi.ID into dsmoi
                               from moi in dsmoi.DefaultIfEmpty()
                               where tx.MaCapThe == MaCapThe
                               select new
                               {
                                   t.NgayNhap,
                                   MaThe = moi.SoThe,
                                   MaTheCu = cu.SoThe,
                                   lt.TenLoaiThe,
                                   TenLX = tx.LoaiThe == 1 ? "" : lx.TenLX,
                                   BienSo = tx.LoaiThe == 1 ? "" : tx.BienSo,
                                   tx.ChuThe,
                                   HangXe = tx.LoaiThe == 1 ? "" : tx.HangXe,
                                   MauSac = tx.LoaiThe == 1 ? "" : tx.MauSac,
                                   tx.PhiGiuXe,
                                   tx.KyTT,
                                   tx.LoaiThe,
                                   tx.NgayTT,
                                   Moi = tx.MaLoaiDK == 1 ? "x" : "",
                                   BanGiao = tx.MaLoaiDK == 4 ? "x" : "",
                                   Huy = tx.MaLoaiDK == 3 ? "x" : "",
                                   DoiThe = tx.MaLoaiDK == 5 ? "x" : "",
                                   TheTichHop = tx.MaLoaiDK == 2 ? "x" : "",
                                   tx.TheCuBackupID,
                                   tx.TheMoiBackupID,
                                   tx.MaLoaiDK,
                                   moi.IsTheOto,
                               }).AsEnumerable()
                               .Select((p, Index) => new ItemNhanThe
                               {
                                   STT = Index +1,
                                   TenLoaiThe = p.TenLoaiThe,
                                   BienSo = p.BienSo,
                                   TenLX = p.TenLX,
                                   HangXe = p.HangXe,
                                   MauSac = p.MauSac,
                                   MaThe = p.MaThe,
                                   MaTheCu = p.MaTheCu,
                                   Moi = p.Moi,
                                   BanGiao = p.BanGiao,
                                   Huy = p.Huy,
                                   DoiThe = p.DoiThe,
                                   TheTichHop = p.TheTichHop,
                                   TheCuBackupID = p.TheCuBackupID,
                                   TheMoiBackupID = p.TheMoiBackupID,
                                   MaLoaiDK = p.MaLoaiDK,
                                   IsTheOto = p.IsTheOto,
                               }).ToList();

            // Kiểm tra trương hợp nếu thẻ ô tô chọn vào bàn giao
            // Lấy thông tin lịch sử thẻ cũ trước khi cấp thẻ
            foreach (var item in ltNhanThe)
            {
                if (item.MaLoaiDK == 1 & item.IsTheOto == true) // tích hợp thẻ
                {
                    item.BanGiao = "x";
                    item.Moi = item.Huy = item.DoiThe = item.TheTichHop = "";
                }
            }

            this.DataSource = ltNhanThe;
        }

    }
    public class ItemNhanThe
    {
        public int STT { get; set; }
        public string TenLoaiThe { get; set; }
        public string BienSo { get; set; }
        public string TenLX { get; set; }
        public string HangXe { get; set; }
        public string MauSac { get; set; }
        public string MaThe { get; set; }
        public string MaTheCu { get; set; }
        public string Moi { get; set; }
        public string BanGiao { get; set; }
        public string Huy { get; set; }
        public string DoiThe { get; set; }
        public string TheTichHop { get; set; }
        public int? TheCuBackupID { get; set; }
        public int? TheMoiBackupID { get; set; }
        public int? MaLoaiDK { get; set; }
        public bool? IsTheOto { get; set; }

    }
}
