using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;

namespace ToaNha
{
    public partial class frmDichVuKhauTruTuDong : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;

        public frmDichVuKhauTruTuDong()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                gcDVKhauTru.DataSource = db.dvDichVuKhauTrus.Where(p => p.MaTN == _MaTN);
            }
            catch 
            {
                gcDVKhauTru.DataSource = null;
            }
        }

        private void frmDichVuKhauTruTuDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            
            gvDVKhauTru.InvalidRowException += Library.Common.InvalidRowException;
            gcDVKhauTru.KeyUp += Common.GridViewKeyUp;

            db = new MasterDataContext();

            itemToaNha.EditValue = Common.User.MaTN;
            //Load loai dich vu trong gird bang gia
            lkLoaiDichVu.DataSource = (from l in db.dvLoaiDichVus
                                       join hd in db.dvHoaDons on l.ID equals hd.MaLDV
                                       where hd.MaTN == (byte)itemToaNha.EditValue
                                       select new
                                       {
                                           l.ID,
                                           TenLDV = l.TenHienThi
                                       }).Distinct().ToList();

            
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            lkLoaiDichVu.DataSource = (from l in db.dvLoaiDichVus
                                       join hd in db.dvHoaDons on l.ID equals hd.MaLDV
                                       where hd.MaTN == (byte)itemToaNha.EditValue
                                       select new
                                       {
                                           l.ID,
                                           TenLDV = l.TenHienThi
                                       }).Distinct().ToList();
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDVKhauTru.AddNewRow();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (itemXoa.Enabled == false) return;
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                gvDVKhauTru.DeleteSelectedRows();
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
            gvDVKhauTru.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
        }

        private void gvBangGia_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var ltt = e.Row as dvDichVuKhauTru;
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

            
        }
    }
}