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
using DevExpress.XtraReports.UI;

namespace TaiSan.BaoCaoPhanTich
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        byte? MaTN { get; set; }
        int? LoaiTS { get; set; }
        int? DonViSD { get; set; }
        DateTime? TuNgay { get; set; }
        DateTime? DenNgay { get; set; }
        MasterDataContext db = new MasterDataContext();
        private object obj { get; set; }
        public frmManager()
        {
            InitializeComponent();
            cbmKyBC.EditValueChanged += new EventHandler(cbmKyBC_EditValueChanged);
            dateDenNgay.EditValueChanged += new EventHandler(dateDenNgay_EditValueChanged);
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
        }

        void SetDate(int index)
        {
            KyBaoCao objKBC = new KyBaoCao();
            objKBC.Index = index;
            objKBC.SetToDate();

            dateTuNgay.EditValueChanged -= new EventHandler(dateTuNgay_EditValueChanged);
            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
        }

        private void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                 obj = db.tsTaiSans.Where(p => (p.mbMatBang.mbTangLau.mbKhoiNha.MaTN == MaTN|| MaTN==null) & (p.MaDVSD == DonViSD||DonViSD==null) &
                    (p.MaLTS == LoaiTS ||LoaiTS==null) & SqlMethods.DateDiffDay(TuNgay, p.NgayGT) >= 0 & SqlMethods.DateDiffDay(p.NgayGT, DenNgay) >= 0)
                    .OrderByDescending(p => p.NgayGT)
                    .Select(q => new
                    {
                        q.ID,
                        q.NgayGT,
                        q.SoGT,
                        q.MaTS,
                        q.TenTS,
                        q.NgayBDSD,
                        q.NguyenGia,
                        q.GiaTriTinhKH,
                        GTTKHHienThoi = q.GiaTriTinhKH - q.GiaTriKHThang * (SqlMethods.DateDiffDay(q.NgayBDSD, DateTime.Now) / 30),
                        ThoiGianSD = q.DVTTGSD == true ? q.ThoiGianSD * 12 : q.ThoiGianSD,
                        ThoiGianSDCL = (q.DVTTGSD == true ? q.ThoiGianSD * 12 : q.ThoiGianSD) - SqlMethods.DateDiffDay(q.NgayBDSD, DateTime.Now) / 30,
                        q.TyLeKHThang,
                        q.GiaTriKHThang,
                        HaoMonTrongKy = q.GiaTriKHThang * (SqlMethods.DateDiffDay(q.NgayBDSD, DateTime.Now) / 30),
                        q.HaoMonLuyKe,
                        q.GiaTriConLai,
                        q.tsLoaiTaiSan.TenLTS,
                        q.tsDonViSuDung.TenDV,
                     //   db.tsGhiGiams.SingleOrDefault(o=>o.ID==)
                        q.TKNguyenGia,
                        q.TKKhauHao

                    }).ToList();
                gcTaiSan.DataSource = obj;
            }
            catch { }
            finally
            { 
                wait.Close();
                wait.Dispose();
            }
        }

        void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void cbmKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemFilter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            popupControlContainer1.ShowPopup(barManager1, gcTaiSan.PointToScreen(Point.Empty));
        }

        private void btnXoaDK_Click(object sender, EventArgs e)
        {
            lookLTS.EditValue = null;
            LookDVSD.EditValue = null;
            lookToaNha.EditValue = null;
            cbmKyBC.SelectedIndex = 0;
            MaTN = null;
            LoaiTS = null;
            DonViSD = null;
            TuNgay = DateTime.Now;
            DenNgay = DateTime.Now;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            popupControlContainer1.Hide();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            MaTN = (byte?)lookToaNha.EditValue;
            LoaiTS = (int?)lookLTS.EditValue;
            DonViSD = (int?)LookDVSD.EditValue;
            TuNgay = (DateTime?)dateTuNgay.EditValue;
            DenNgay = (DateTime?)dateDenNgay.EditValue;
            LoadData();
            popupControlContainer1.HidePopup();
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            lookToaNha.Properties.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenTN });
            LookDVSD.Properties.DataSource = db.tsDonViSuDungs.Select(p => new { p.MaDV, p.ID, p.TenDV });
            lookLTS.Properties.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS, p.TenLTS });
            KyBaoCao objKBC = new KyBaoCao();
            foreach (string str in objKBC.Source)
                cbmKyBC.Properties.Items.Add(str);
            SetDate(7);

        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnXuatBC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tn = db.tnToaNhas.SingleOrDefault(p=>p.MaTN==MaTN);
            var DVSD=db.tsDonViSuDungs.SingleOrDefault(p=>p.ID== DonViSD);
            var LTS=db.tsLoaiTaiSans.SingleOrDefault(p=>p.MaLTS==LoaiTS);
            TaiSan.BaoCaoPhanTich.rptThongKeTS rpt = new rptThongKeTS(obj, TuNgay, DenNgay, tn == null ? null : tn.TenTN, DVSD == null ? null : DVSD.TenDV, LTS == null ? null : LTS.TenLTS);
            rpt.ShowPreviewDialog();
        }

        private void btnTheTSCD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvTaiSan.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Chọn tài sản để xem thẻ tài sản");
            }
            BaoCaoPhanTich.rptTimeRequre frm = new rptTimeRequre();
            frm.MaTS = (int?)gvTaiSan.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
        }

        private void btnInThongKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tn = db.tnToaNhas.SingleOrDefault(p => p.MaTN == MaTN);
            var DVSD = db.tsDonViSuDungs.SingleOrDefault(p => p.ID == DonViSD);
            var LTS = db.tsLoaiTaiSans.SingleOrDefault(p => p.MaLTS == LoaiTS);
            TaiSan.BaoCaoPhanTich.rptThongKeTS rpt = new rptThongKeTS(obj, TuNgay, DenNgay, tn == null ? null : tn.TenTN, DVSD == null ? null : DVSD.TenDV, LTS == null ? null : LTS.TenLTS);
            rpt.PrintDialog();
        }



    }
}