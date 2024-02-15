using System;
using System.Windows.Forms;
using Library;
using System.Linq;
using DevExpress.XtraEditors;

namespace ToaNha
{
    public partial class frmToaNha : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;

        public frmToaNha()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            var ltMaTN = Common.TowerList.Select(p => p.MaTN).ToList();
            gcToaNha.DataSource = db.tnToaNhas
                .Where(p => p.MaNV == Common.User.MaNV ||  ltMaTN.Contains(p.MaTN))
                .Select(o => new
                {
                    o.ChuTaiKhoan,
                    o.DiaChi,
                    o.DienThoai,
                    o.Email,
                    o.Fax,
                    o.Logo,
                    o.MaTN,
                    o.NganHang,
                    o.NgayCN,
                    o.NgayTao,
                    o.NguoiDaiDien,
                    o.SoTaiKhoan,
                    o.TenTN,
                    o.TenVT,
                    o.NgayCuoiCungThanhToanTrongThang,
                    o.PhanTramLaiSuat,
                    o.CompanyCode,
                    o.TenNgan,
                    //o.BrandName
                    //NhanVien = o.tnNhanVien.HoTenNV,
                    //NhanVienCN = o.tnNhanVien1 == null ? "" : o.tnNhanVien1.HoTenNV
                }).ToList();
        }

        void LoadDetails()
        {
            var maTN = (byte?)grvToaNha.GetFocusedRowCellValue("MaTN");
            if (maTN == null)
            {
                gcNhanVien.DataSource = null;
                return;
            }
            switch (xtraTabControl1.SelectedTabPage.Name)
            {
                case "tabNhanVien":
                    gcNhanVien.DataSource = db.tnToaNhaNguoiDungs.Where(p => p.MaTN == maTN);
                    break;
                case "tabHotline":
                    gcHotline.DataSource = db.tnHotlines.Where(_ => _.BuildingId == maTN);
                    break;
                case "tabCanhBao":
                    gcCanhbao.DataSource = ToaNha.DichVu.CanhBao.GetCanh_Bao_Lists(maTN);
                    break;
            }
        }

        void Edit()
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            var f = new frmEdit();
            f.objNV = objNV;
            f.MaTN = (byte?)grvToaNha.GetFocusedRowCellValue("MaTN");
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        bool isExitNhanVien(int maNV)
        {
            for (int i = 0; i < grvNhanVien.RowCount - 1; i++)
            {
                if (maNV == (int?)grvNhanVien.GetRowCellValue(i, "MaNV")) return true;
            }

            return false;
        }

        private void frmBuilding_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objNV, barManager1);

            barManager1.SetPopupContextMenu(gcNhanVien, popupMenu1);

            var ltMaTN = Common.TowerList.Select(p => p.MaTN).ToList();
            lookNhanVien.DataSource = db.tnNhanViens
                //.Where(p=> ltMaTN.Contains(p.MaTN.GetValueOrDefault()))
                .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV, p.tnPhongBan.TenPB, p.tnChucVu.TenCV }).ToList();

            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;
            
            try
            {
                var delete = db.tnToaNhas.Single(p => p.MaTN == (byte)grvToaNha.GetFocusedRowCellValue("MaTN"));
                db.tnToaNhas.DeleteOnSubmit(delete);
                db.SubmitChanges();
                grvToaNha.DeleteSelectedRows();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Không xóa được Dự án vì bị ràng buộc dữ liệu");
            }
            
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new ToaNha.frmEdit();
            f.objNV = objNV;
            f.ShowDialog();
            if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }        

        private void grvToaNha_DoubleClick(object sender, EventArgs e)
        {
            if (itemEdit.Enabled)
                Edit();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void itemStaffAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmSelect())
            {
                frm.ShowDialog(this);
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var maNV in frm.ListNhanVien)
                    {
                        if (isExitNhanVien(maNV)) continue;

                        grvNhanVien.AddNewRow();
                        grvNhanVien.SetFocusedRowCellValue("MaNV", maNV);
                    }
                    grvNhanVien.RefreshData();
                }
            }
        }

        private void itemStaffSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                grvNhanVien.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Đã cập nhật dữ liệu thành công!");

                LoadDetails();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemStaffDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = grvNhanVien.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var item in rows)
                {
                    var obj = db.tnToaNhaNguoiDungs.Single(p => p.ID == (int)grvNhanVien.GetRowCellValue(item, "ID"));
                    db.tnToaNhaNguoiDungs.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                grvNhanVien.DeleteSelectedRows();

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

        private void grvToaNha_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetails();
        }

        private void lookNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            var sp = (GridLookUpEdit)sender;
            if (sp.EditValue == null) return;

            if (isExitNhanVien((int)sp.EditValue))
            {
                DialogBox.Alert("Trùng nhân viên quản lý, vui lòng chọn nhân viên khác");
                grvNhanVien.SetFocusedRowCellValue("MaNV", grvNhanVien.GetFocusedRowCellValue("MaNV"));
            }
        }

        private void grvNhanVien_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var maTN = (byte?)grvToaNha.GetFocusedRowCellValue("MaTN");
            if (maTN == null) return;

            grvNhanVien.SetFocusedRowCellValue("MaTN", maTN);
        }

        private void XtraTabControl1_Click(object sender, EventArgs e)
        {
            LoadDetails();
        }

        private void GvHotline_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var maTn = (byte?)grvToaNha.GetFocusedRowCellValue("MaTN");
            if (maTn == null) return;
            gvHotline.SetFocusedRowCellValue("BuildingId", maTn);
        }

        private void ItemSaveHotline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvHotline.RefreshData();

                db.SubmitChanges();

                DialogBox.Alert("Đã cập nhật dữ liệu thành công!");

                LoadDetails();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void ItemDeleteHotline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rows = gvHotline.GetSelectedRows();
            if (rows.Length <= 0)
            {
                DialogBox.Alert("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (var item in rows)
                {
                    var obj = db.tnHotlines.FirstOrDefault(_ => _.Id == (int)gvHotline.GetRowCellValue(item, "Id"));
                    if (obj != null)
                        db.tnHotlines.DeleteOnSubmit(obj);
                }

                db.SubmitChanges();
                gvHotline.DeleteSelectedRows();

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

        private void itemKhuDat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn() { Loai = ToaNha.Class.DanhMucDuAnEnum.KHU_DAT })
            {
                frm.ShowDialog(this);
            }
        }

        private void ItemHinhThucXayDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn {Loai = ToaNha.Class.DanhMucDuAnEnum.HINH_THUC_XAY_DUNG})
            {
                frm.ShowDialog();
            }
        }

        private void ItemCapCongTrinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn {Loai = ToaNha.Class.DanhMucDuAnEnum.CAP_CONG_TRINH})
            {
                frm.ShowDialog();
            }
        }

        private void ItemHangToaNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn {Loai = ToaNha.Class.DanhMucDuAnEnum.HANG_TOA_NHA})
            {
                frm.ShowDialog();
            }
        }

        private void ItemNhomNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn {Loai = ToaNha.Class.DanhMucDuAnEnum.NHOM_NHA})
            {
                frm.ShowDialog();
            }
        }

        private void ItemDonViSuDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn {Loai = ToaNha.Class.DanhMucDuAnEnum.DON_VI_SU_DUNG})
            {
                frm.ShowDialog();
            }
        }

        private void ItemDonViQuanLy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new ToaNha.DanhMuc.FrmDanhMucDuAn {Loai = ToaNha.Class.DanhMucDuAnEnum.DON_VI_QUAN_LY})
            {
                frm.ShowDialog();
            }
        }

        private void itemCauHinhDichVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvToaNha.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            using (var frm = new ToaNha.DichVu.frmCanhBao())
            {
                frm.MaTN = (byte)grvToaNha.GetFocusedRowCellValue("MaTN");
                frm.ShowDialog();
            }
        }
    }
}