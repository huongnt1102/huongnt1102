using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.Nuoc
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
                    gcDinhMuc.DataSource = db.dvNuocDinhMucs.Where(p => p.MaMB == _MaMB);
                else if (_MaLMB != null)
                    gcDinhMuc.DataSource = db.dvNuocDinhMucs.Where(p => p.MaLMB == _MaLMB & p.MaMB == null);
                else
                    gcDinhMuc.DataSource = db.dvNuocDinhMucs.Where(p => p.MaTN == _MaTN & p.MaLMB == null & p.MaMB == null);
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

            var db = new MasterDataContext();
            try
            {
                foreach (var item in collection)
                {
                    var obj = db.dvNuocDinhMucs.Single(p => p.ID == (int)gvDinhMuc.GetRowCellValue(item, "ID"));
                    db.dvNuocDinhMucs.DeleteOnSubmit(obj);
                }
                db.SubmitChanges();
                gvDinhMuc.DeleteSelectedRows();
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

        private void frmDinhMuc_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            
            gvDinhMuc.InvalidRowException += Library.Common.InvalidRowException;

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                lkLoaiMatBang.DataSource = (from l in db.mbLoaiMatBangs
                                              where l.MaTN == _MaTN
                                              select new { l.MaLMB, l.TenLMB })
                                            .ToList();
                itemLoaiMatBang.EditValue = null;

                glkMatBang.DataSource = (from mb in db.mbMatBangs
                                         join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                         join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                         where mb.MaTN == _MaTN
                                         select new { mb.MaMB, mb.MaSoMB, tl.TenTL, kn.TenKN })
                                         .ToList();
                itemMatBang.EditValue = null;
            }
            catch { }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gvDinhMuc.AddNewRow();
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

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void gvDinhMucNuoc_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var maTN = gvDinhMuc.GetRowCellValue(e.RowHandle, "MaTN");
            if (maTN == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn Dự án !";
                return;
            }

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

        private void gvDinhMucNuoc_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvDinhMuc.SetFocusedRowCellValue("MaTN", itemToaNha.EditValue);
            gvDinhMuc.SetFocusedRowCellValue("MaLMB", itemLoaiMatBang.EditValue);  
            gvDinhMuc.SetFocusedRowCellValue("MaMB", itemMatBang.EditValue);
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