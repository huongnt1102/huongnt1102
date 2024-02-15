using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Library;
using System.Linq;
using System.Data.Linq.SqlClient;

namespace AnNinh.BaoCao
{
    public partial class rptBaoCaoSuViecTrongCa : DevExpress.XtraReports.UI.XtraReport
    {
        public rptBaoCaoSuViecTrongCa(DateTime tungay, DateTime denngay, Library.tnNhanVien objnhanvien)
        {
            InitializeComponent();
            lblNgayThang.Text = string.Format("Từ {0} đến {1}", tungay.ToShortDateString(), denngay.ToShortDateString());
            lblNguoiIn.Text = objnhanvien.HoTenNV;
            using (MasterDataContext db = new MasterDataContext())
            {
                DataSource = db.AnNinhSuViecTrongCas.Where(p =>
                           SqlMethods.DateDiffDay(tungay, p.ThoiGianGhiNhan.Value) >= 0 &
                           SqlMethods.DateDiffDay(p.ThoiGianGhiNhan.Value, denngay) >= 0)
                           .Select(p => new
                           {
                               p.SuViec,
                               p.tnNhanVien.HoTenNV,
                               p.MaSuViec,
                               p.ThoiGianGhiNhan
                           });

                lblNV.DataBindings.Add(new XRBinding("Text", this.DataSource, "HoTenNV"));
                lblSV.DataBindings.Add(new XRBinding("Text", this.DataSource, "SuViec"));
                lblThoiGian.DataBindings.Add(new XRBinding("Text", this.DataSource, "ThoiGianGhiNhan", "{0:dd/MM/yyyy hh:mm}"));
            }

        }

        int stt = 0;

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            stt++;
            lblSTT.Text = stt.ToString();
        }
    }
}
