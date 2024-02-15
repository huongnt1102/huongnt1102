using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;
namespace Library
{
    public partial class frmChayLaiSoQuy_ThuChi : Form
    {
        MasterDataContext db = new MasterDataContext();
        public frmChayLaiSoQuy_ThuChi()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var objPT = (from ct in db.ptChiTietPhieuThus
                         join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                         where !(from sq in db.SoQuy_ThuChis
                                 select sq.IDPhieuChiTiet).Contains(ct.ID)
                         select new { 
                         pt.MaKH,
                         pt.NgayThu,
                         pt.MaTN,
                         pt.MaPL,
                         pt.MaMB,
                         pt.SoPT,
                         pt.MaNVN,
                         pt.IsKhauTru,
                         pt.IsKhauTruTuDong,
                         HinhThuThanhToan=pt.MaTKNH==null?0:1,
                         ct.ID,
                         ct.DienGiai,
                         ct.KhauTru,
                         ct.LinkID,
                         ct.MaPT,
                         ct.SoTien,
                         ct.TableName,
                         ct.ThuThua,
                         ct.PhaiThu
                         }).ToList();
            foreach (var item in objPT)
            {
                Common.SoQuy_InsertIsKhauTruTuDong(db, item.NgayThu.Value.Month, item.NgayThu.Value.Year, item.MaTN, item.MaKH, item.MaMB, item.MaPT, item.ID, item.NgayThu, item.SoPT, item.HinhThuThanhToan, item.MaPL, true, item.PhaiThu, item.SoTien, item.ThuThua, item.KhauTru, item.LinkID, "dvHoaDon", item.DienGiai, item.MaNVN, item.IsKhauTru, item.IsKhauTruTuDong);
            }
            DialogBox.Alert("Đã xong");

        }
    }
}
