using Library;
using System;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace Building.SMSZalo.NhanVien
{
    public partial class frmHistory : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        DateTime now;

        public frmHistory()
        {
            InitializeComponent();
            db = new MasterDataContext();
            now = db.GetSystemDate();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            db = new MasterDataContext();
            var wait = DialogBox.WaitingForm();
            var MaTN = (byte?)itemBieuMauToaNha.EditValue;
            var TuNgay = (DateTime?)barEditItemTuNgay.EditValue;
            var DenNgay = (DateTime?)barEditItemDenNgay.EditValue;
            var obj = (from o in db.tnNhanVien_histories
                       join nv in db.tnNhanViens on o.MaNV equals nv.MaNV
                       join nvg in db.tnNhanViens on o.StaffCreate equals nvg.MaNV
                      
                       where o.MaTN == MaTN 
                       & SqlMethods.DateDiffDay(TuNgay, o.DateCreate) > 0
                       & SqlMethods.DateDiffDay(o.DateCreate, DenNgay) > 0
                       select new
                       {
                           o.ID,
                           o.MaTN,
                           o.Content,
                           o.DateCreate,
                           o.Message,
                           o.Status,
                           o.isFile,
                           HoTenNV = nv.HoTenNV,
                           HoTenNVGUI = nvg.HoTenNV,
                       })
                       .AsEnumerable()
                       .Select((o, index) => new
                       {
                           STT = index++,
                           o.ID,
                           o.MaTN,
                           o.Content,
                           o.DateCreate,
                           o.Message,
                           o.Status,
                           o.HoTenNV,
                           o.HoTenNVGUI,
                           o.isFile,
                       })
                       .OrderByDescending(o => o.DateCreate).ToList();  
            gcDanhSach.DataSource = obj;
            wait.Close();
            wait.Dispose();
        }
       
        private void frmManager_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            var lt = db.tnToaNhas.Select(p => new { p.MaTN, p.TenVT, p.TenTN }).ToList();
            itemDuAn.DataSource = lt;
            if (lt.Count() > 0)
                itemBieuMauToaNha.EditValue = lt.FirstOrDefault().MaTN;

            barEditItemTuNgay.EditValue = new DateTime(now.Year, now.Month, 1);
            barEditItemDenNgay.EditValue = Library.Common.GetLastDayOfMonth(now);
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcDanhSach);
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }
       
    }
}