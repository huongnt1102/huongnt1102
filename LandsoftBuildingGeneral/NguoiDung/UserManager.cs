using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using Library.HeThongCls;

namespace LandsoftBuildingGeneral.NguoiDung
{
    public partial class UserManager : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        MasterDataContext db;
        public UserManager()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void UserManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            //lkToaNha.DataSource = db.tnToaNhas;
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            LoadData();
        }
        private void LoadData()
        {
            var db = new MasterDataContext();
            try
            {
                var _MaTN = (byte?)itemToaNha.EditValue;

                gcUser.DataSource = db.tnNhanViens
                        .Where(p => p.MaTN == _MaTN)
                        .OrderBy(p => p.HoTenNV)
                        .Select(p => new
                        {
                            p.MaNV,
                            p.HoTenNV,
                            p.MaSoNV,
                            p.DiaChi,
                            p.DienThoai,
                            p.Email,
                            PhongBan = p.tnPhongBan.TenPB,
                            TenVT = "",
                            p.tnChucVu.TenCV,
                            p.IdZalo,
                            p.IsZalo,
                            p.NameZalo,
                            IsLocked = p.IsLocked.GetValueOrDefault()
                        }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        private int GetMaNhom(int MaNV)
        {
            return db.pqNhomNhanViens.Where(p => p.MaNV == MaNV).Select(p => p.GroupID).FirstOrDefault();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (UserEdit frm = new UserEdit())
            {
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) LoadData();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gvUser.FocusedRowHandle < 0)
                {

                    DialogBox.Alert("Vui lòng chọn nhân viên trong danh sách");
                    return;
                }
                using (UserEdit frm = new UserEdit())
                {
                    using (MasterDataContext db = new MasterDataContext())
                    {
                        frm.objNhanVien = db.tnNhanViens.FirstOrDefault(p => p.MaNV == (int)gvUser.GetFocusedRowCellValue("MaNV"));
                    }

                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) LoadData();
                }
            }
            catch { }
            
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhân viên trong danh sách");
                return;
            }
            using (MasterDataContext db = new MasterDataContext())
            {
                var objNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)gvUser.GetFocusedRowCellValue("MaNV"));
                try
                {
                    if (DialogBox.QuestionDelete() == DialogResult.Yes)
                    {
                        // kiem tra nhan vien dung app
                        var resident_item = db.app_Residents.Where(_ => _.EmployeeIdRefer == objNhanVien.MaNV&_.IsLock.GetValueOrDefault() == false);
                        foreach (var item in resident_item)
                        {
                            if (item.IsResident.GetValueOrDefault() == true)
                            {
                                item.IsEmployee = false;
                                item.EmployeeIdRefer = null;
                            }
                            else
                            {
                                // không có nhân viên
                                item.IsLock = true;
                                try
                                {
                                    db.app_Residents.DeleteOnSubmit(item);
                                    db.SubmitChanges();
                                }
                                catch(System.Exception ex) { }
                            }
                        }

                        db.tnNhanViens.DeleteOnSubmit(objNhanVien);
                        db.SubmitChanges();
                        gvUser.DeleteSelectedRows();
                        LoadData();
                    }
                }
                catch (System.Exception ex)                 
                {
                    DialogBox.Error("Không xóa được nhân viên này");
                }
            }
        }

        private void btnLichSuDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhân viên trong danh sách");
                return;
            }

            using (frmLichSuDangNhap frm = new frmLichSuDangNhap() { objnhanvien = objnhanvien })
            {
                frm.ShowDialog();
            }
        }

        private void btnXemTatCa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhân viên trong danh sách");
                return;
            }

            using (frmLichSuDangNhap frm = new frmLichSuDangNhap() { objnhanvien = null })
            {
                frm.ShowDialog();
            }
        }

        private void itemLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LockAccount(true);
        }

        private void itemUnlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LockAccount(false);
        }

        void LockAccount(bool isLock)
        {
            if(gvUser.FocusedRowHandle < 0){
                DialogBox.Alert("Vui lòng chọn [Nhân viên], xin cảm ơn.");
                return;
            }

            if (objnhanvien.MaNV == (int)gvUser.GetFocusedRowCellValue("MaNV"))
            {
                DialogBox.Alert("Bạn không thể khóa tài khoản của chính mình.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            try
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var objNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)gvUser.GetFocusedRowCellValue("MaNV"));
                    objNhanVien.IsLocked = isLock;
                    db.SubmitChanges();

                    gvUser.SetFocusedRowCellValue("IsLocked", isLock);
                }
            }
            catch { DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn."); }
        }

        private void itemResetPass_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhân viên], xin cảm ơn.");
                return;
            }

            try
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    var objNhanVien = db.tnNhanViens.Single(p => p.MaNV == (int)gvUser.GetFocusedRowCellValue("MaNV"));
                    UserLogin user = new UserLogin();
                    objNhanVien.MatKhau = Library.Common.HashPassword(gvUser.GetFocusedRowCellValue("MaSoNV").ToString());
                    db.SubmitChanges();
                    DialogBox.Success();
                }
            }
            catch { DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng kiểm tra lại, xin cảm ơn."); }
        }

        private void itemToaNha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (var frm = new LandsoftBuildingGeneral.Import.FrmUserImport())
                {
                    frm.MaTn = (byte)itemToaNha.EditValue;
                    frm.ShowDialog();
                    if (frm.IsSave)
                        LoadData();
                }
            }
            catch
            {
                //
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcUser);
        }

        private void itemRestZalo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs = gvUser.GetSelectedRows();

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn nhân viên.");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (int i in indexs)
                {
                    int MaNV = (int)gvUser.GetRowCellValue(i, "MaNV");
                    var obj = db.tnNhanViens.FirstOrDefault(o => o.MaNV == MaNV);
                    if (obj != null)
                    {
                        obj.IdZalo = "";
                        obj.IsZalo = false;
                        obj.NameZalo = "";
                        db.SubmitChanges();
                    }
                }
                DialogBox.Success("Cập nhật thành công!");
                LoadData();
            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}
