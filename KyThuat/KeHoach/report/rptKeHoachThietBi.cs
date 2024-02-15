using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Library;
using System.Data;

namespace KyThuat.KeHoach.report
{
    public partial class rptKeHoachThietBi : DevExpress.XtraReports.UI.XtraReport
    {
        public rptKeHoachThietBi(int MaKH)
        {
            InitializeComponent();

            using (MasterDataContext db = new MasterDataContext())
            {
                var objbt = db.khbtKeHoaches.Single(p => p.MaKH == MaKH);
                lblMaKeHoach.Text = objbt.MaSoKH;
                lblTinhTrang.Text = objbt.khbtTrangThai.TenTT;
                lblChiPhi.Text = objbt.ChiPhi.Value.ToString("C");
                lblNguoiLap.Text = objbt.tnNhanVien.HoTenNV;
                lblThoiGian.Text = objbt.NgayKH.Value.ToShortDateString();

                DataTable dttaisan = new DataTable();
                var lsttaisan = db.khbtTaiSans.Where(p => p.MaKH == MaKH)
                    .Select(p => new
                    {
                        p.MaTS,
                        p.MaKH,
                        //<chus y chinh lai cho nay>
                        p.tsTaiSan.TenTS,
                        KyHieu= p.tsTaiSan.MaTS,
                        p.tsTaiSan.tsTinhTrang.TenTT,
                        p.DienGiai
                    });
                dttaisan = SqlCommon.LINQToDataTable(lsttaisan);

                DataTable dttsct = new DataTable();
                var lstkehoach = db.tsTaiSanChiTiets
                    .Select(p => new
                    {
                        p.MaTS,
                        p.MaChiTiet,
                        p.ChiTietTaiSan.TenChiTiet,
                        p.tsTrangThai.TenTT,
                        p.DienGiai,
                        p.NgayNhap
                    });
                dttsct = SqlCommon.LINQToDataTable(lstkehoach);

                DataSet ds = new DataSet();
                ds.Tables.Add(dttaisan);
                ds.Tables.Add(dttsct);

                DataColumn taisancl = new DataColumn();
                taisancl = ds.Tables[0].Columns["MaTS"];
                DataColumn tschitietcl = new DataColumn();
                tschitietcl = ds.Tables[1].Columns["MaTS"];

                bool HaveDetail = false;
                try
                {
                    DataRelation dtrelation = new DataRelation("KeHoach_TaiSan", taisancl, tschitietcl, false);
                    ds.Relations.Add(dtrelation);
                    HaveDetail = true;
                }
                catch {
                    HaveDetail = false;
                }

                DetailReport.DataSource = ds;
                DetailReport.DataMember = ds.Tables[0].TableName;
                DetailReport1.DataSource = ds;
                DetailReport1.DataMember = ds.Tables[0].TableName + ".KeHoach_TaiSan";

                lblTaiSan.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "TenLTS"));
                lblKyHieu.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "KyHieu"));
                lblTrangThaiTS.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "TenTT"));
                //lblTuNgay.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "TuNgay","{0:dd/MM/yyyy}"));
                //lblDenNgay.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "DenNgay", "{0:dd/MM/yyyy}"));
                lblDienGiaiTS.DataBindings.Add(new XRBinding("Text", DetailReport.DataSource, "DienGiai"));

                if (HaveDetail)
                {
                    DetailReport1.Visible = true;
                    lblTenChiTiet.DataBindings.Add(new XRBinding("Text", DetailReport1.DataSource, ds.Tables[0].TableName + ".KeHoach_TaiSan.TenChiTiet"));
                    lblTrangThai.DataBindings.Add(new XRBinding("Text", DetailReport1.DataSource, ds.Tables[0].TableName + ".KeHoach_TaiSan.TenTT"));
                    lbldiengiaichitiet.DataBindings.Add(new XRBinding("Text", DetailReport1.DataSource, ds.Tables[0].TableName + ".KeHoach_TaiSan.DienGiai"));
                    lblNgay.DataBindings.Add(new XRBinding("Text", DetailReport1.DataSource, ds.Tables[0].TableName + ".KeHoach_TaiSan.NgayNhap", "{0:dd/MM/yyyy}"));
                }
                else
                {
                    DetailReport1.Visible = false;
                }


                DetailReport2.DataSource = db.khbtThietBis.Where(p => p.MaKH == MaKH)
                    .Select(p => new
                        {
                            p.tsLoaiTaiSan.TenLTS,
                            p.SoLuong,
                            p.DienGiai
                        });
                lblTenThietBi.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "TenLTS"));
                lblSoLuongTB.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "SoLuong"));
                lblDienGiaiTB.DataBindings.Add(new XRBinding("Text", DetailReport2.DataSource, "DienGiai"));

                DetailReport3.DataSource = db.khbtDoiTacs.Where(p => p.MaKH == MaKH)
                    .Select(p => new
                        {
                            p.tnNhaCungCap.TenNCC,
                            p.DienGiai
                        });
                lblDoiTac.DataBindings.Add(new XRBinding("Text", DetailReport3.DataSource, "TenNCC"));
                lblDienGiaiDT.DataBindings.Add(new XRBinding("Text", DetailReport3.DataSource, "DienGiai"));

                DetailReport4.DataSource = db.khbtNhanViens.Where(p => p.MaKH == MaKH)
                    .Select(p => new
                    {
                        p.tnNhanVien.MaSoNV,
                        p.tnNhanVien.HoTenNV,
                        p.DienGiai
                    });
                lblMaNhanVien.DataBindings.Add(new XRBinding("Text", DetailReport4.DataSource, "MaSoNV"));
                lblHotenNV.DataBindings.Add(new XRBinding("Text", DetailReport4.DataSource, "HoTenNV"));
                lblDienGiaiNV.DataBindings.Add(new XRBinding("Text", DetailReport4.DataSource, "DienGiai"));
            }
        }

    }
}
