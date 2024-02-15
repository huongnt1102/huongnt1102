using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace LandsoftBuildingGeneral.PhanQuyen
{
    public partial class frmUserGroup : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;

        public frmUserGroup()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        private void frmUserGroup_Load(object sender, EventArgs e)
        {
            var ltTN = Common.TowerList.Select(p => (byte?)p.MaTN).ToList();
            glkNhanVien.DataSource = db.tnNhanViens
                .Where(p => !p.IsSuperAdmin.Value & ltTN.Contains(p.MaTN))
                .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV }).ToList();
            //if (objnhanvien.IsSuperAdmin.Value)
            //{
            //    LookNhanVien.DataSource = db.tnNhanViens
            //        .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV }).ToList();
            //}
            //else
            //{
            //    var collectionNV = db.pqNhomNhanViens.Select(p => p.MaNV).ToList();
            //    LookNhanVien.DataSource = db.tnNhanViens
            //        .Where(p=>!collectionNV.Contains(p.MaNV))
            //        .Select(p => new { p.MaNV, p.MaSoNV, p.HoTenNV }).ToList();
            //    //var nvtrongNhom = db.pqNhomNhanViens.Where(p => collectionNhom.Contains(p.GroupID)).Select(p => p.MaNV).ToList();
            //}
            LoadData();
        }

        private void LoadData()
        {
            gcUserGroup.DataSource = db.pqNhoms;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu");
                LoadData();
            }
            catch 
            {
                DialogBox.Error("Lưu không thành công. Có thể do đường truyền không ổn định. Vui lòng thử lại");
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvUserGroup.DeleteSelectedRows();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvUserGroup.AddNewRow();
        }

        private void grvUserGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var g = (pqNhom)grvUserGroup.GetFocusedRow();
            gcUser.DataSource = g.pqNhomNhanViens;
        }
        
        private void LookNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit look = sender as LookUpEdit;
            for (int i = 0; i < grvUser.RowCount; i++)
            {
                if (grvUser.GetRowCellValue(i, colNhanVien) != null)
                {
                    if (look.EditValue.ToString() == grvUser.GetRowCellValue(i, colNhanVien).ToString())
                    {
                        DialogBox.Alert("Người dùng này đã có trong nhóm rồi!");
                        return;
                    }
                }
                
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnXoaNguoiDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvUser.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Click chuột phải vào người dùng muốn xóa");
                return;
            }
            var useringroup = db.pqNhomNhanViens.Single(p => p.MaNV == (int)grvUser.GetFocusedRowCellValue("MaNV")
                & p.GroupID == (int)grvUserGroup.GetFocusedRowCellValue("GroupID"));
            db.pqNhomNhanViens.DeleteOnSubmit(useringroup);
            try
            {
                db.SubmitChanges();
                grvUser.DeleteSelectedRows();
            }
            catch { }
        }

        private void grvUser_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvUser.SetFocusedRowCellValue(colIsTruongNhom, false);
        }
    }
}