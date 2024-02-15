using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.SqlClient;

namespace DIPCRM.PriceAlert
{
    public partial class frmChonPhongGhe : DevExpress.XtraEditors.XtraForm
    {
        public int maMB;
        public int soLuong;
        private string maMoTaPhong;
        private int? maTN;
        public bool IsGhe = false;

        MasterDataContext db = new MasterDataContext();

        public frmChonPhongGhe(string MaMTP, byte? MaTN)
        {
            InitializeComponent();
            this.maMoTaPhong = MaMTP;
            this.maTN = MaTN;
        }

        private void frmChonPhongGhe_Load(object sender, EventArgs e)
        {
            var chiTiets = (from p in db.ctLichThanhToans
                            join mb in db.mbMatBangs on p.MaMB equals mb.MaMB
                            join hd in db.ctHopDongs on p.MaHD equals hd.ID
                            where p.ctHopDong.MaTN == maTN
                            //& mb.IsGhe.GetValueOrDefault() == this.IsGhe & !mb.IsNgungSuDung.GetValueOrDefault()
                            //group p by new { p.MaMB, p.MaHD, mb.MaSoMB, hd.MaHDGoc, hd.SoHDCT, isThanhLy = hd.ctThanhLies.Any() } into gr
                            group p by new { p.MaMB, p.MaHD, mb.MaSoMB, hd.SoHDCT, isThanhLy = hd.ctThanhLies.Any() } into gr
                            select new
                            {
                                gr.Key.MaMB,
                                gr.Key.MaHD,
                                gr.Key.MaSoMB,
                                //gr.Key.MaHDGoc,
                                gr.Key.SoHDCT,
                                NgayHL = gr.Min(o => o.TuNgay),
                                NgayHH = gr.Key.isThanhLy ? gr.Where(o => db.dvHoaDons.Any(i => i.MaLDV == 2 & i.LinkID == o.ID)).Max(o => o.DenNgay) : gr.Max(o => o.DenNgay),
                            }).ToList();

            var matBangs = (from mb in db.mbMatBangs
                            //join sgn in db.SoGheNgois on mb.idSoGheNgoi equals sgn.Ma_sgn into soghengoi
                            //from sgn in soghengoi.DefaultIfEmpty()
                            where mb.MaTN == this.maTN 
                            //& mb.idMoTaPhong == this.maMoTaPhong & !mb.IsNgungSuDung.GetValueOrDefault()
                            //& mb.IsGhe.GetValueOrDefault() == this.IsGhe
                            select new
                            {
                                mb.MaSoMB,
                                mb.MaMB,
                                //SoLuongGhe = sgn != null ? sgn.SoLuongGhe : 1,
                            }).ToList();

            gcMatBang.DataSource = (from p in matBangs
                                    join ct in chiTiets on p.MaMB equals ct.MaMB into chitiet from ct in chitiet.DefaultIfEmpty()
                                    where ct == null || (SqlMethods.DateDiffDay(ct.NgayHL, DateTime.Now) >= 0 & SqlMethods.DateDiffDay(DateTime.Now, ct.NgayHH) >= 0)
                                    select new
                                    {
                                        p.MaMB,
                                        p.MaSoMB,
                                        SoHDCT = ct == null ? "" : ct.SoHDCT,
                                        NgayHL = ct == null ? null : ct.NgayHL,
                                        NgayHH = ct == null ? null : ct.NgayHH,
                                        //p.SoLuongGhe,
                                    });
        }

        private void gvMatBang_DoubleClick(object sender, EventArgs e)
        {
            var id = (int?)gvMatBang.GetFocusedRowCellValue("MaMB");
            var sl = (int?)gvMatBang.GetFocusedRowCellValue("SoLuongGhe");

            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn phòng/ghế");
                return;
            }

            this.maMB = id.Value;
            this.soLuong = sl.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}