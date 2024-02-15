using System;
using System.Windows.Forms;
using Library;
using System.Linq;

namespace DichVu.GiuXe
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

                gcDinhMuc.DataSource = (from dm in db.dvgxDinhMucs join lx in db.dvgxLoaiXes on dm.MaLX equals lx.MaLX join lmb in db.mbLoaiMatBangs on dm.MaLMB equals lmb.MaLMB into loaiMatBang from lmb in loaiMatBang.DefaultIfEmpty() join mb in db.mbMatBangs on dm.MaMB equals mb.MaMB into matBang from mb in matBang.DefaultIfEmpty() where dm.MaTN == _MaTN select new { dm.TenDM, dm.GiaNgay, dm.GiaThang, lx.TenLX, dm.SoLuong, dm.ID, dm.MaMB, dm.MaLMB, lmb.TenLMB, mb.MaSoMB }).ToList();

            }
            catch
            {
                gcDinhMuc.DataSource = null;
            }
        }

        void Delete()
        {
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var id = (int?)gvDinhMuc.GetFocusedRowCellValue("ID");
            var tx = db.dvgxDinhMucs.Single(p => p.ID == id);
            db.dvgxDinhMucs.DeleteOnSubmit(tx);
            db.SubmitChanges();
            gvDinhMuc.DeleteSelectedRows();
            LoadData();
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

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _MaTN = (byte)itemToaNha.EditValue;
                lkLoaiXe.DataSource = (from lx in db.dvgxLoaiXes
                                       where lx.MaTN == _MaTN
                                       orderby lx.STT
                                       select new { lx.MaLX, lx.TenLX })
                                      .ToList();
                itemLoaiXe.EditValue = null;

                lkLoaiMatBang.DataSource = (from l in db.mbLoaiMatBangs
                                            where l.MaTN == _MaTN
                                            select new { l.MaLMB, l.TenLMB })
                                            .ToList();
                itemLoaiMatBang.EditValue = null;

                glkMatBang.DataSource = (from mb in db.mbMatBangs
                                         where mb.MaTN == _MaTN
                                         select new { mb.MaMB, mb.MaSoMB, mb.MaKH })
                                         .ToList();
                itemMatBang.EditValue = null;
            }
            catch { }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gvDinhMuc.AddNewRow();
            using(var frm = new DichVu.GiuXe.DinhMuc.FrmDinhMucEdit())
            {
                frm.MaTn = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                LoadData();
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvDinhMuc.UpdateCurrentRow();

                db.SubmitChanges();

                DialogBox.Alert("Dữ liệu đã được lưu");
            }
            catch
            {
                DialogBox.Alert("Không lưu được, dữ liệu bị ràng buộc");
            }

            LoadData();
        }

        private void gcDinhMucNuoc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Delete();
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
            gvDinhMuc.SetFocusedRowCellValue("MaLX", itemLoaiXe.EditValue);
            gvDinhMuc.SetFocusedRowCellValue("MaLMB", itemLoaiMatBang.EditValue);
            gvDinhMuc.SetFocusedRowCellValue("MaMB", itemMatBang.EditValue);
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
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

        private void glkKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gvDinhMuc.GetFocusedRowCellValue("ID") == null) { DialogBox.Error("Vui lòng chọn phiếu cần sửa, xin cảm ơn."); return; }
            using(var frm = new DichVu.GiuXe.DinhMuc.FrmDinhMucEdit() { MaTn = (byte?)itemToaNha.EditValue, Id = (int?)gvDinhMuc.GetFocusedRowCellValue("ID") }) { frm.ShowDialog(); LoadData(); }
        }
    }
}