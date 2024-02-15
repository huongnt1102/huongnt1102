using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;

namespace Provider
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public frmManager()
        {
            InitializeComponent();
            db = new MasterDataContext();

            TranslateLanguage.TranslateControl(this, barManager1);
        }

        void LoadData()
        {
            if (objnhanvien.IsSuperAdmin.Value)
                gcDataSource.DataSource = db.NhaCungCaps.Select(p => new
                {
                    p.MaNCC,
                    p.MaNV,
                    p.MaNVCN,
                    p.DiaChi,
                    p.DiDongNLH,
                    p.DienThoai,
                    p.DienThoaiNLH,
                    p.Email,
                    p.EmailNLH,
                    p.Fax,
                    p.GhiChu,
                    p.NgayCN,
                    p.NgayTao,
                    p.NguoiLienHe,
                    p.TenNCC,
                    NhanVien = p.tnNhanVien.HoTenNV,
                    NhanVienCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : ""
                });
            else
            {
                var GetNhomOfNV = db.pqNhomNhanViens.Where(p => p.IsTruongNhom.Value & p.MaNV == objnhanvien.MaNV).Select(p => p.GroupID).ToList();
                if (GetNhomOfNV.Count > 0)
                {
                    var GetListNV = db.pqNhomNhanViens.Where(p => GetNhomOfNV.Contains(p.GroupID)).Select(p => p.MaNV).ToList();

                    gcDataSource.DataSource = db.NhaCungCaps.Where(p => GetListNV.Contains(p.MaNV.Value)).Select(p => new
                    {
                        p.MaNCC,
                        p.MaNV,
                        p.MaNVCN,
                        p.DiaChi,
                        p.DiDongNLH,
                        p.DienThoai,
                        p.DienThoaiNLH,
                        p.Email,
                        p.EmailNLH,
                        p.Fax,
                        p.GhiChu,
                        p.NgayCN,
                        p.NgayTao,
                        p.NguoiLienHe,
                        p.TenNCC,
                        NhanVien = p.tnNhanVien.HoTenNV,
                        NhanVienCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : ""
                    });
                }
                else
                {
                    gcDataSource.DataSource = db.NhaCungCaps.Where(p => p.MaNV == objnhanvien.MaNV).Select(p => new
                    {
                        p.MaNCC,
                        p.MaNV,
                        p.MaNVCN,
                        p.DiaChi,
                        p.DiDongNLH,
                        p.DienThoai,
                        p.DienThoaiNLH,
                        p.Email,
                        p.EmailNLH,
                        p.Fax,
                        p.GhiChu,
                        p.NgayCN,
                        p.NgayTao,
                        p.NguoiLienHe,
                        p.TenNCC,
                        NhanVien = p.tnNhanVien.HoTenNV,
                        NhanVienCN = p.tnNhanVien1 != null ? p.tnNhanVien1.HoTenNV : ""
                    });
                }
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit() { objnhanvien = objnhanvien };
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        void Edit()
        {
            if (gvDataSource.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhà cung cấp], xin cảm ơn.");
                return;
            }

            var frm = new frmEdit() { objnhanvien = objnhanvien };
            frm.MaNCC = (int)gvDataSource.GetFocusedRowCellValue("MaNCC");
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void gvDataSource_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDataSource.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Nhà cung cấp], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == System.Windows.Forms.DialogResult.No) return;

            try
            {
                var obj = db.NhaCungCaps.Single(p => p.MaNCC == (int)gvDataSource.GetFocusedRowCellValue("MaNCC"));
                db.NhaCungCaps.DeleteOnSubmit(obj);

                db.SubmitChanges();

                gvDataSource.DeleteSelectedRows();
            }
            catch { DialogBox.Alert("Xóa không thành công vì: [Nhà cung cấp] đã được sử dụng.\r\n\r\nVui lòng kiểm tra lại, xin cảm ơn."); }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}