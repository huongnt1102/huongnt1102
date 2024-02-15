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
namespace Building.Asset.DanhMuc
{
    public partial class frmThietLapNgayNghi_TheoToaNha : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmThietLapNgayNghi_TheoToaNha()
        {
            InitializeComponent();
        }

        private void frmThietLapNgayNghi_Load(object sender, EventArgs e)
        {
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
        }
        void LoadData()
        {
            var objData = (from nn in db.tbl_ThietLapNgayNghis
                           join nvn in db.tnNhanViens on nn.NguoiNhap equals nvn.MaNV into nvnhap
                           from nvn in nvnhap.DefaultIfEmpty()
                           join nvs in db.tnNhanViens on nn.NguoiSua equals nvs.MaNV into nvsua
                           from nvs in nvsua.DefaultIfEmpty()
                           where nn.MaTN==(byte?)itemToaNha.EditValue
                           select new
                           {
                               nn.ID,
                               nn.TieuDe,
                               nn.Nam,
                               nn.IsChuNhat,
                               nn.IsThuBay,
                               nn.NgayNhap,
                               nn.NgaySua,
                               HoTenNguoiNhap = nvn.HoTenNV,
                               HoTenNguoiSua = nvs.HoTenNV,
                           }).ToList();
            gcDanhMuc.DataSource = objData.OrderByDescending(p => p.Nam);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmThietLapNgayNghi_TheoToaNha_Edit frm = new frmThietLapNgayNghi_TheoToaNha_Edit();
            frm.MaTN = Convert.ToByte(itemToaNha.EditValue);
            frm.ShowDialog();
            if (frm.IsSave)
                LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDanhMuc.GetFocusedRowCellValue("ID") != null)
            {
                frmThietLapNgayNghi_TheoToaNha_Edit frm = new frmThietLapNgayNghi_TheoToaNha_Edit();
                frm.ID = (int?)gvDanhMuc.GetFocusedRowCellValue("ID");
                frm.MaTN = Convert.ToByte(itemToaNha.EditValue);
                frm.ShowDialog();
                if (frm.IsSave)
                    LoadData();
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn thiết lập ngày nghỉ cần sửa");
            }

        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDanhMuc.GetFocusedRowCellValue("ID") != null)
            {
                db.tbl_CauHinhNgayNghis.DeleteAllOnSubmit(db.tbl_CauHinhNgayNghis.Where(p => p.MaThietLapID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID")));
                db.tbl_ThietLapNgayNghis.DeleteOnSubmit(db.tbl_ThietLapNgayNghis.FirstOrDefault(p => p.ID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID")));
                try
                {
                    db.SubmitChanges();
                    DialogBox.Alert("Xóa dữ liệu thành công!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    DialogBox.Alert("Dữ liệu ngày nghỉ đã được kế thừa cho tòa nhà rồi. Không thể xóa được");
                }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn thiết lập ngày nghỉ cần sửa");
            }
        }

        private void gvDanhMuc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvDanhMuc.GetFocusedRowCellValue("ID") != null)
            {
                gcChiTiet.DataSource = db.tbl_CauHinhNgayNghis.Where(p => p.MaThietLapID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID"));
            }
            else
            {
                gcChiTiet.DataSource = null;
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}