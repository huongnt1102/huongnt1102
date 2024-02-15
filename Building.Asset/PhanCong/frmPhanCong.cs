using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace Building.Asset.PhanCong
{
    public partial class frmPhanCong : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmPhanCong()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }
        void ExportRecord()
        {
            var db = new MasterDataContext();
            try
            {

                var ltData = from ds in db.tbl_PhanCongs
                             join tn in db.tnToaNhas on ds.MaTN equals tn.MaTN into tnr
                             from tn in tnr.DefaultIfEmpty()

                             join pc_nvct in db.tbl_PhanCong_NhanVienChiTiets on ds.ID equals pc_nvct.IDPhanCong into pc_nvctr
                             from pc_nvct in pc_nvctr.DefaultIfEmpty()
                             //join kn in db.mbKhoiNhas on pc_nvct.DanhSachThapID equals kn.MaKN into _knha
                             //from kn in _knha.DefaultIfEmpty()

                             join nv in db.tnNhanViens on pc_nvct.MaNV equals nv.MaNV into nvr
                             from nv in nvr.DefaultIfEmpty()

                             select new
                             {
                                 tn.TenTN,
                                 ds.Ngay,
                                 ds.MaSoPC,
                                 ds.NoiDungCongViec,
                                 //Name = kn.TenKN,
                                 nv.HoTenNV,
                                 ds.NguoiTao,
                                 ds.NgayTao,
                                 ds.NguoiSua,
                                 ds.NgaySua,
                             };

                var tblData = SqlCommon.LINQToDataTable(ltData);
                ExportToExcel.exportDataToExcel(tblData);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
        void LoadData()
        {
            db = new MasterDataContext();
            gc.DataSource = db.tbl_PhanCongs.Where(o => o.MaTN == (byte?)itemToaNha.EditValue);
        }
        void LoadDetail()
        {
            var ID_PC = (int?)gv.GetFocusedRowCellValue("ID");
            gcChiTiet.DataSource = from p in db.tbl_PhanCong_NhanVienChiTiets
                                   //join kn in db.mbKhoiNhas on p.DanhSachThapID equals kn.MaKN
                                   group new {p} by new {p.tnNhanVien.HoTenNV,p.IDPhanCong} into g
                                       where g.Key.IDPhanCong == ID_PC
                                       select new
                                       {
                                           //Name = kn.TenKN,
                                           g.Key.HoTenNV
                                       };
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            repToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmPhanCongEdit
            {
                MaTn = (byte)itemToaNha.EditValue,
                IsSua = 0,
                Id = 0,
            };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (DialogBox.QuestionDelete() == DialogResult.No)
            {
                return;
            }
            else
            {

                // kiểm tra xóa table tbl_phancong (Trường hợp 2 bảng kia null)
                db.tbl_PhanCong_NhanVienChiTiets.DeleteAllOnSubmit(db.tbl_PhanCong_NhanVienChiTiets.Where(p => p.IDPhanCong == (int)gv.GetFocusedRowCellValue("ID")));
                var objPC = db.tbl_PhanCongs.FirstOrDefault(o => o.ID == (int)gv.GetFocusedRowCellValue("ID"));
                db.tbl_PhanCongs.DeleteOnSubmit(objPC);
            }
            try
            {
                db.SubmitChanges();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Xóa không thành công vì bị ràng buộc dữ liệu");
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmPhanCongEdit
            {
                MaTn = (byte)itemToaNha.EditValue,
                IsSua = 1,
                Id = (int)gv.GetFocusedRowCellValue("ID"),
            };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ExportRecord();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //var maTN = (byte?)itemToaNha.EditValue ?? 0;
                //using (var frm = new Import.frmPhanCong_Import())
                //{
                //    frm.MaTn = maTN;
                //    frm.ShowDialog();
                //    if (frm.IsSave)
                //        LoadData();
                //}
            }
            catch
            {
                //
            }
        }
    }
}