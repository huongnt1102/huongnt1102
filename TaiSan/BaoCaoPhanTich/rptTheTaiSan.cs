using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Library;
using System.Linq;
using System.Data.Linq;

namespace TaiSan.BaoCaoPhanTich
{
    public partial class rptTheTaiSan : DevExpress.XtraReports.UI.XtraReport
    {
        MasterDataContext db = new MasterDataContext();
        public rptTheTaiSan(int? MaTS, DateTime? NgayLT)
        {
            InitializeComponent();
            var objTS = db.tsTaiSans.Where(p => p.ID == MaTS)
                .Select(q => new { 
                    q.MaTS,q.TenTS,
                    q.NgayGT,
                    q.NuocSX,
                    q.NamSX,
                    q.tsDonViSuDung.TenDV,
                    q.NgayBDSD,
                    q.GiaTriConLai,
                    q.SoGT,
                    q.NguyenGia
                }).SingleOrDefault();
            var objgg = db.tsGhiGiamChiTiets.Where(p => p.MaTS == MaTS)
                .Select(q => new { 
                    q.tsGhiGiam.SoCT,
                    q.tsGhiGiam.NgayCT,
                    q.tsGhiGiam.tsGhiGiamLyDo.TenLD
                }).SingleOrDefault();
            var objPT = db.tsDungCu_PhuTungs.Where(p => p.MaTS == MaTS)
                .Select(q => new { 
                    q.TenDCPT,
                    q.DVT,
                    q.GiaTri,
                    q.SoLuong
                });
            this.DataSource = objPT;

            lbNgayLT.Text = string.Format("Ngày : {0} tháng: {1} năm: {2} lập thẻ", NgayLT.Value.Day, NgayLT.Value.Month, NgayLT.Value.Year);
            lblTenTS.Text = string.Format("Tên, ký mã hiệu, quy cách (cấp hạng) TSCĐ: {0}    Số hiệu: {1}", objTS.TenTS, objTS.MaTS);
            lblNuocSX.Text = string.Format("Nước sản xuất (xây dựng): {0}      Năm sản xuất: {1}", objTS.NuocSX, objTS.NamSX);
            lblBoPhanQL.Text = string.Format("Bộ phận quản lý, sử dụng {0}      Năm đưa vào sử dụng: {1}", objTS.TenDV, objTS.NgayBDSD.Value.Year);
            if(objgg!=null)
            {
                lblTTghiGiam.Text = string.Format("Ghi giảm TSCĐ chứng từ số: {0}     Ngày {1} Tháng {2} Năm {3}", objgg.SoCT, objgg.NgayCT.Value.Day, objgg.NgayCT.Value.Month, objgg.NgayCT.Value.Year);
                lblLyDoGG.Text = string.Format("Lý do: {0}", objgg.TenLD);
            }
            lbSoHieu.Text = objTS.SoGT;
            lbNgayGT.Text = string.Format("{0:dd/MM/yyyy}", objTS.NgayGT);
         //   lblDienGiai.Text
            lblNguyenGia.Text = string.Format("{0:#,0.##}", objTS.NguyenGia);
            lblNamHT.Text = NgayLT.Value.Year.ToString();
           // lblGTHaoMon.Text = string.Format("{0:#,0.##}");
            #region Bind
            cSTT.DataBindings.Add(new XRBinding("Text", DataSource, ""));
            cTenPT.DataBindings.Add(new XRBinding("Text", DataSource, "TenDCPT"));
            cDVT.DataBindings.Add(new XRBinding("Text", DataSource, "DVT"));
            cSoLuong.DataBindings.Add(new XRBinding("Text", DataSource, "SoLuong"));
            cGiaTri.DataBindings.Add(new XRBinding("Text", DataSource, "GiaTri", "{0:#,0.##}"));
            #endregion

        }

    }
}
