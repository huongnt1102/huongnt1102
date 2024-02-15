using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.Dien
{
    public partial class frmDinhMuc : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db = new MasterDataContext();

        public frmDinhMuc()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                var _MaLMB = (int?)itemLoaiMatBang.EditValue;
                var _MaMB = (int?)itemMatBang.EditValue;

                if (_MaMB != null)
                    gcDinhMuc.DataSource = db.dvDienDinhMucs.Where(p => p.MaMB == _MaMB);
                else if (_MaLMB != null)
                    gcDinhMuc.DataSource = db.dvDienDinhMucs.Where(p => p.MaLMB == _MaLMB & p.MaMB == null);
                else
                    gcDinhMuc.DataSource = db.dvDienDinhMucs.Where(p => p.MaTN == _MaTN & p.MaLMB == null & p.MaMB == null);
            }
            catch
            {
                gcDinhMuc.DataSource = null;
            }
        }

        void Delete()
        {
            var collection = gvDinhMuc.GetSelectedRows();
            if (collection.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            gvDinhMuc.DeleteSelectedRows();
        }

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            
            gvDinhMuc.InvalidRowException += Library.Common.InvalidRowException;
            gcDinhMuc.KeyUp += Common.GridViewKeyUp;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDinhMuc.AddNewRow();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {

            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;

                lkLoaiMatBang.DataSource = (from n in db.mbLoaiMatBangs where n.MaTN == _MaTN select new { n.MaLMB, n.TenLMB }).ToList();
                itemLoaiMatBang.EditValue = null;

                glkMatBang.DataSource = (from mb in db.mbMatBangs
                                         join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                         where mb.MaTN == _MaTN
                                         select new { mb.MaMB, mb.MaSoMB, TenKH = kh.IsCaNhan.GetValueOrDefault() ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen })
                                         .ToList();
                itemMatBang.EditValue = null;
            }
            catch { }

            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvDinhMuc.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");

                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }
        }

        private void gvDinhMuc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvDinhMuc.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvDinhMuc.SetFocusedRowCellValue("MaLMB", itemLoaiMatBang.EditValue);  
            gvDinhMuc.SetFocusedRowCellValue("MaMB", itemMatBang.EditValue);
        }

        private void gvDinhMuc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var tenDM = (gvDinhMuc.GetRowCellValue(e.RowHandle, "TenDM") ?? "").ToString();
            if (tenDM.Length == 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập Tên Định Mức !";
                return;
            }
            else if (Common.Duplication(gvDinhMuc, e.RowHandle, "TenDM", tenDM))
            {
                e.Valid = false;
                e.ErrorText = "Tên Định Mức trùng, vui lòng nhập lại !";
                return;
            }
        }

        private void lkLoaiMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                itemLoaiMatBang.EditValue = null;
            }
        }

        private void glkMatBang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                itemMatBang.EditValue = null;
            }
        }
    }
}