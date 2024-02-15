using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Library;

namespace DichVu
{
    public partial class formBaoCaoTongHopDienNuoc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public formBaoCaoTongHopDienNuoc()
        {
            InitializeComponent();
        }

        private void formBaoCaoTongHopDienNuoc_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
            {
                cbbKyBC.Items.Add(str);
            }
            itemKyBC.EditValue = objKBC.Source[34];
            SetDate(35);
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

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void cbbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        void LoadData()
        {
            var wait = new WaitDialogForm("Ðang xử lý. Vui lòng chờ...", "LandSoft Building");
            var tungay = (DateTime?)itemTuNgay.EditValue;
            var denngay = (DateTime?)itemDenNgay.EditValue;
            var den1 = new DateTime(tungay.Value.Year, 2, 1).AddDays(-1);
            var den2 = new DateTime(tungay.Value.Year, 3, 1).AddDays(-1);
            var den3 = new DateTime(tungay.Value.Year, 4, 1).AddDays(-1);
            var den4 = new DateTime(tungay.Value.Year, 5, 1).AddDays(-1);
            var den5 = new DateTime(tungay.Value.Year, 6, 1).AddDays(-1);
            var den6 = new DateTime(tungay.Value.Year, 7, 1).AddDays(-1);
            var den7 = new DateTime(tungay.Value.Year, 8, 1).AddDays(-1);
            var den8 = new DateTime(tungay.Value.Year, 9, 1).AddDays(-1);
            var den9 = new DateTime(tungay.Value.Year, 10, 1).AddDays(-1);
            var den10 = new DateTime(tungay.Value.Year, 11, 1).AddDays(-1);
            var den11 = new DateTime(tungay.Value.Year, 12, 1).AddDays(-1);
            var den12 = new DateTime(tungay.Value.Year, 12, DateTime.DaysInMonth(tungay.Value.Year, 12));

            var D1 = new DateTime(denngay.Value.Year, 2, 1).AddDays(-1);
            var D2 = new DateTime(denngay.Value.Year, 3, 1).AddDays(-1);
            var D3 = new DateTime(denngay.Value.Year, 4, 1).AddDays(-1);
            var D4 = new DateTime(denngay.Value.Year, 5, 1).AddDays(-1);
            var D5 = new DateTime(denngay.Value.Year, 6, 1).AddDays(-1);
            var D6 = new DateTime(denngay.Value.Year, 7, 1).AddDays(-1);
            var D7 = new DateTime(denngay.Value.Year, 8, 1).AddDays(-1);
            var D8 = new DateTime(denngay.Value.Year, 9, 1).AddDays(-1);
            var D9 = new DateTime(denngay.Value.Year, 10, 1).AddDays(-1);
            var D10 = new DateTime(denngay.Value.Year, 11, 1).AddDays(-1);
            var D11 = new DateTime(denngay.Value.Year, 12, 1).AddDays(-1);
            var D12 = new DateTime(denngay.Value.Year, 12, DateTime.DaysInMonth(denngay.Value.Year, 12));

            bandDuAn.Caption = "Dự án     " + db.tnToaNhas.SingleOrDefault(p => p.MaTN == (byte?)itemToaNha.EditValue).TenTN;
            gridBand5.Caption = tungay.Value.Year.ToString();
            gridBand6.Caption = denngay.Value.Year.ToString();
            try
            {
                var data = from mb in db.mbMatBangs
                           join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                           join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                           where
                            mb.MaTN == (byte?)itemToaNha.EditValue
                           select new
                           {
                               Tang = tl.TenTL,
                               ViTri = mb.SoNha,
                               KhoiNha = kn.TenKN,
                               Thang1 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                 && SqlMethods.DateDiffDay(p.TuNgay, den1) >= 0
                                 && SqlMethods.DateDiffDay(p.TuNgay, den1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den1) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,

                               Thang2 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                  && SqlMethods.DateDiffDay(p.TuNgay, den2) >= 0
                                  && SqlMethods.DateDiffDay(p.TuNgay, den2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den2) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,

                               Thang3 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                  && SqlMethods.DateDiffDay(p.TuNgay, den3) >= 0
                                  && SqlMethods.DateDiffDay(p.TuNgay, den3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den3) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang4 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                  && SqlMethods.DateDiffDay(p.TuNgay, den4) >= 0
                                  && SqlMethods.DateDiffDay(p.TuNgay, den4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den4) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,

                               Thang5 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den5) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den5) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang6 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den6) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den6) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang7 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den7) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den7) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang8 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den8) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den8) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang9 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den9) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den9) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang10 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den10) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den10) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang11 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den11) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den11) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               Thang12 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, den12) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, den12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, den12) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, den12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,

                               t1 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D1) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D1) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D1) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t2 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D2) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D2) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D2) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t3 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D3) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D3) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D3) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t4 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D4) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D4) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D4) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t5 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D5) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D5) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D5) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t6 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D6) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D6) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D6) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t7 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D7) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D7) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D7) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t8 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D8) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D8) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D8) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t9 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D9) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D9) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D9) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t10 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D10) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D10) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D10) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t11 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D11) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D11) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D11) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                               t12 = db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                && SqlMethods.DateDiffDay(p.TuNgay, D12) >= 0
                                && SqlMethods.DateDiffDay(p.TuNgay, D12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu != null ?
                                             db.dvDiens.Where(p => p.MaMB == mb.MaMB
                                                && SqlMethods.DateDiffDay(p.TuNgay, D12) >= 0
                                                && SqlMethods.DateDiffDay(p.TuNgay, D12) <= SqlMethods.DateDiffDay(p.TuNgay, p.DenNgay)
                                             ).OrderByDescending(z => z.ID).FirstOrDefault().SoTieuThu : null,
                           };
                wait.Close();
                gc.DataSource = data;
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex);
            }


            gv.FocusedRowHandle = -1;
            gv.ExpandAllGroups();
          
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gv_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.RowHandle >= 0)
            //{
            //    var NoiDung = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Done"]);
            //    if (NoiDung != "1")
            //    {
            //        e.Appearance..BackColor = Color.FromArgb(150, Color.Red);
            //    }
            //}
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName == "Tang" || e.Column.FieldName == "ViTri") return;
            //decimal? requiredDate = (decimal?)e.CellValue;
            //if (requiredDate != null) return;
            //string displayText;
            //if (requiredDate == null)
            //    displayText = null;
            //else
            //    displayText = e.DisplayText;
            //LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.LightGray, LinearGradientMode.ForwardDiagonal);
            //using (brush)
            //{
            //    e.Cache.FillRectangle(brush, e.Bounds);
            //}
            //e.Cache.DrawString(displayText, e.Appearance.Font, Brushes.Black, e.Bounds);
            //e.Handled = true;
        }
    }
}
