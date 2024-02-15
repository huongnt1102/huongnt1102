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
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.Data.PivotGrid;

namespace LandSoftBuilding.Lease.Reports
{
    public partial class frmCongNoDatCoc : DevExpress.XtraEditors.XtraForm
    {
        public frmCongNoDatCoc()
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

        List<byte?> GetToaNha()
        {
            var ltToaNha = new List<byte?>();
            var arrMaTN = (itemToaNha.EditValue ?? "").ToString().Split(',');
            foreach (var s in arrMaTN)
                if (s != "")
                    ltToaNha.Add(byte.Parse(s));
            return ltToaNha;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            var db = new MasterDataContext();
            try
            {
                var _TuNgay = (DateTime)itemTuNgay.EditValue;
                var _DenNgay = (DateTime)itemDenNgay.EditValue;
                var ltToaNha = this.GetToaNha();

                var ltData = from thue in db.ctHopDongs
                             join kh in db.tnKhachHangs on thue.MaKH equals kh.MaKH
                             join tn in db.tnToaNhas on thue.MaTN equals tn.MaTN
                             where ltToaNha.Contains(thue.MaTN) & thue.NgungSuDung.GetValueOrDefault() == false
                             select new
                             {
                                 SoHD = thue.SoHDCT,
                                 thue.NgayKy,
                                 thue.NgayHL,
                                 thue.NgayHH,
                                 kh.KyHieu,
                                 TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                 tn.TenTN,
                                 DauKy = (from ltt in db.ctLichThanhToans
                                          where ltt.MaHD == thue.ID & ltt.MaLDV == 3 & SqlMethods.DateDiffDay(ltt.TuNgay, _TuNgay) > 0
                                          select ltt.SoTienQD - (from ct in db.ptChiTietPhieuThus
                                                                 join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                                 join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                                                 where hd.TableName == "ctLichThanhToan" & hd.LinkID == ltt.ID & SqlMethods.DateDiffDay(pt.NgayThu, _TuNgay) > 0
                                                                 select ct.SoTien).Sum().GetValueOrDefault()
                                                              - (from ct in db.ktttChiTiets
                                                                 join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                                                 join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                                                 where hd.TableName == "ctLichThanhToan" & hd.LinkID == ltt.ID & SqlMethods.DateDiffDay(pt.NgayCT, _TuNgay) > 0
                                                                 select ct.SoTien).Sum().GetValueOrDefault()
                                              ).Sum().GetValueOrDefault(),
                                 PhatSinh = (from ltt in db.ctLichThanhToans
                                             where ltt.MaHD == thue.ID & ltt.MaLDV == 3 & SqlMethods.DateDiffDay(_TuNgay, ltt.TuNgay) >= 0 & SqlMethods.DateDiffDay(ltt.TuNgay, _DenNgay) >= 0
                                             select ltt.SoTienQD).Sum().GetValueOrDefault(),
                                 DaThu = (from ct in db.ptChiTietPhieuThus
                                          join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                          join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                          join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                          where ltt.MaHD == thue.ID & ltt.MaLDV == 3 & SqlMethods.DateDiffDay(_TuNgay, pt.NgayThu) >= 0 & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                          select ct.SoTien).Sum().GetValueOrDefault(),
                                 KhauTru = (from ct in db.ktttChiTiets
                                            join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                            join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                            join ltt in db.ctLichThanhToans on new { hd.TableName, hd.LinkID } equals new { TableName = "ctLichThanhToan", LinkID = (int?)ltt.ID }
                                            where ltt.MaHD == thue.ID & ltt.MaLDV == 3 & SqlMethods.DateDiffDay(_TuNgay, pt.NgayCT) >= 0 & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                            select ct.SoTien).Sum().GetValueOrDefault(), // cái này ban đầu nó là 3, còn lại tất cả = 4
                                 CuoiKy = (from ltt in db.ctLichThanhToans
                                           where ltt.MaHD == thue.ID & ltt.MaLDV == 3 & SqlMethods.DateDiffDay(ltt.TuNgay, _DenNgay) >= 0
                                           select ltt.SoTienQD - (from ct in db.ptChiTietPhieuThus
                                                                  join pt in db.ptPhieuThus on ct.MaPT equals pt.ID
                                                                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                                                  where hd.TableName == "ctLichThanhToan" & hd.LinkID == ltt.ID & SqlMethods.DateDiffDay(pt.NgayThu, _DenNgay) >= 0
                                                                  select ct.SoTien).Sum().GetValueOrDefault()
                                                               - (from ct in db.ktttChiTiets
                                                                  join pt in db.ktttKhauTruThuTruocs on ct.MaCT equals pt.ID
                                                                  join hd in db.dvHoaDons on new { ct.TableName, ct.LinkID } equals new { TableName = "dvHoaDon", LinkID = (long?)hd.ID }
                                                                  where hd.TableName == "ctLichThanhToan" & hd.LinkID == ltt.ID & SqlMethods.DateDiffDay(pt.NgayCT, _DenNgay) >= 0
                                                                  select ct.SoTien).Sum().GetValueOrDefault()
                                              ).Sum().GetValueOrDefault()
                             };

                pvDatCoc.DataSource = ltData;
            }
            catch { }
            finally
            {
                db.Dispose();
                wait.Close();
            }
        }

        void Print()
        {
            var rpt = new rptCongNoDatCoc(Common.User.MaTN.Value);
            var stream = new System.IO.MemoryStream();
            var _KyBC = (itemKyBaoCao.EditValue ?? "").ToString();
            var _TuNgay = (DateTime)itemTuNgay.EditValue;
            var _DenNgay = (DateTime)itemDenNgay.EditValue;

            pvDatCoc.OptionsView.ShowColumnHeaders = false;
            pvDatCoc.OptionsView.ShowDataHeaders = false;
            pvDatCoc.OptionsView.ShowFilterHeaders = false;
            pvDatCoc.SavePivotGridToStream(stream);
            pvDatCoc.OptionsView.ShowColumnHeaders = true;
            pvDatCoc.OptionsView.ShowDataHeaders = true;
            pvDatCoc.OptionsView.ShowFilterHeaders = true;

            rpt.LoadData(_KyBC, _TuNgay, _DenNgay, stream);
            rpt.ShowPreviewDialog();
        }

        private void frmCongNoTheXe_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);

            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            ckbToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN.ToString();

            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Initialize(cmbKyBaoCao);
            var index = DateTime.Now.Month + 8;
            itemKyBaoCao.EditValue = objKBC.Source[index];
            SetDate(index);

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }
    }
}