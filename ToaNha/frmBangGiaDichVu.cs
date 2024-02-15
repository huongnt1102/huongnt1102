using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace ToaNha
{
    public partial class frmBangGiaDichVu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmBangGiaDichVu()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                gcBangGia.DataSource = db.dvBangGiaDichVus.Where(p => p.MaTN == _MaTN);
            }
            catch 
            {
                gcBangGia.DataSource = null;
            }
        }

        private void frmBangGiaDichVu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            gvBangGia.InvalidRowException += Library.Common.InvalidRowException;
            gcBangGia.KeyUp += Common.GridViewKeyUp;

            db = new MasterDataContext();

            //Load loai tien
            lkLoaiTienDV.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();

            //Load don vi tinh
            lkDoiViTinhDV.DataSource = (from dvt in db.DonViTinhs select new { dvt.ID, dvt.TenDVT }).ToList();

            //Load loai dich vu trong gird bang gia
            lkLoaiDichVu.DataSource = (from l in db.dvLoaiDichVus
                                       where l.IsCoBan == true
                                       select new
                                       {
                                           l.ID,
                                           TenLDV = l.TenHienThi
                                       }).ToList();

            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvBangGia.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemXoa.Enabled == false) return;
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gvBangGia.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void gvBangGia_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvBangGia.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void gvBangGia_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var ltt = e.Row as dvBangGiaDichVu;
            if (ltt == null) return;
            if (ltt.MaTN == null)
            {
                e.ErrorText = "Vui lòng chọn Dự án";
                e.Valid = false;
                return;
            }

            if (ltt.MaLDV == null)
            {
                e.ErrorText = "Vui lòng chọn [loại dịch vụ]";
                e.Valid = false;
                return;
            }
            else
            {
                if (Common.Duplication(sender as GridView, e.RowHandle, "MaLDV", ltt.MaLDV.ToString()))
                {
                    e.ErrorText = "Loại dịch vụ đã tồn tại";
                    e.Valid = false;
                    return;
                }
            }

            if (ltt.DonGia == null)
            {
                e.ErrorText = "Vui lòng nhập [Đơn giá]";
                e.Valid = false;
                return;
            }

            if (ltt.MaLT == null)
            {
                e.ErrorText = "Vui lòng chọn [Loại tiền]";
                e.Valid = false;
                return;
            }

            if (ltt.MaDVT == null)
            {
                e.ErrorText = "Vui lòng chọn [Đơn vị tính]";
                e.Valid = false;
                return;
            }
        }
    }
}