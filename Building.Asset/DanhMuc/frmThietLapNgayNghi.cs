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
    public partial class frmThietLapNgayNghi : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmThietLapNgayNghi()
        {
            InitializeComponent();
        }

        private void frmThietLapNgayNghi_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            var objData = (from nn in db.tbl_ThietLapNgayNghi_DanhMucs
                           join nvn in db.tnNhanViens on nn.NguoiNhap equals nvn.MaNV
                           join nvs in db.tnNhanViens on nn.NguoiSua equals nvs.MaNV into nvsua
                           from nvs in nvsua.DefaultIfEmpty()
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
            frmThietLapNgayNghi_Edit frm = new frmThietLapNgayNghi_Edit();
            frm.ShowDialog();
            if (frm.IsSave)
                LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDanhMuc.GetFocusedRowCellValue("ID") != null)
            {
                frmThietLapNgayNghi_Edit frm = new frmThietLapNgayNghi_Edit();
                frm.ID = (int?)gvDanhMuc.GetFocusedRowCellValue("ID");
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
                db.tbl_ThietLapNgayNghi_DanhMuc_ChiTiets.DeleteAllOnSubmit(db.tbl_ThietLapNgayNghi_DanhMuc_ChiTiets.Where(p => p.MaThietLapID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID")));
                db.tbl_ThietLapNgayNghi_DanhMucs.DeleteOnSubmit(db.tbl_ThietLapNgayNghi_DanhMucs.FirstOrDefault(p => p.ID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID")));
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
                gcChiTiet.DataSource = db.tbl_ThietLapNgayNghi_DanhMuc_ChiTiets.Where(p => p.MaThietLapID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID"));
                LoadToaNha((int?)gvDanhMuc.GetFocusedRowCellValue("ID"));

            }
            else
            {
                gcChiTiet.DataSource = null;
                gcToaNha.DataSource = null;
            }
        }
        void LoadToaNha(int? DanhMucID)
        {
            gcToaNha.DataSource = (from nn in db.tbl_ThietLapNgayNghis
                                   join tn in db.tnToaNhas on nn.MaTN equals tn.MaTN
                                   where nn.DanhMucID == DanhMucID
                                   select new
                                   {
                                       tn.MaTN,
                                       tn.TenTN
                                   });
        }
        private void ppThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDanhMuc.GetFocusedRowCellValue("ID") == null)
            {
                DialogBox.Alert("Vui lòng chọn thiết lập ngày nghỉ!");
                return;
            }
            frmToaNhaSelect frm = new frmToaNhaSelect();
            frm.ShowDialog();
            if (frm.IsSave)
            {
                foreach (var item in frm.ltToaNhaSelect)
                {
                    var objTT = db.tbl_ThietLapNgayNghi_DanhMucs.FirstOrDefault(p => p.ID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID"));
                    if (objTT != null)
                    {
                        var objtn = new tbl_ThietLapNgayNghi();
                        objtn.DanhMucID = objTT.ID;
                        objtn.IsChuNhat = objTT.IsChuNhat;
                        objtn.IsThuBay = objTT.IsThuBay;
                        objtn.Nam = objTT.Nam;
                        objtn.MaTN = item;
                        objtn.TieuDe = objTT.TieuDe;
                        objtn.NguoiNhap = Common.User.MaNV;
                        objtn.NgayNhap = Common.GetDateTimeSystem();
                        db.tbl_ThietLapNgayNghis.InsertOnSubmit(objtn);
                        //Thêm chi tiết
                        foreach (var item1 in objTT.tbl_ThietLapNgayNghi_DanhMuc_ChiTiets)
                        {
                            var ct = new tbl_CauHinhNgayNghi();
                            ct.DienGiai = item1.DienGiai;
                            ct.NgayBD = item1.NgayBD;
                            ct.NgayKT = item1.NgayKT;
                            ct.TieuDe = item1.TieuDe;
                            objtn.tbl_CauHinhNgayNghis.Add(ct);
                        }
                    }
                }
                db.SubmitChanges();
                LoadToaNha((int?)gvDanhMuc.GetFocusedRowCellValue("ID"));
            }
        }

        private void ppXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvToaNha.GetFocusedRowCellValue("MaTN") == null)
            {
                DialogBox.Alert("Vui lòng chọn tòa nhà muốn xóa");
                return;
            }
            var objTT = db.tbl_ThietLapNgayNghi_DanhMucs.FirstOrDefault(p => p.ID == (int?)gvDanhMuc.GetFocusedRowCellValue("ID"));
            if (objTT != null)
            {
                int Nam = objTT.Nam.Value;
                int ID = objTT.ID;
                byte MaTN = Convert.ToByte(gvToaNha.GetFocusedRowCellValue("MaTN"));
                var obj = db.tbl_ThietLapNgayNghis.FirstOrDefault(p => p.DanhMucID == ID && p.MaTN == MaTN);
                db.tbl_CauHinhNgayNghis.DeleteAllOnSubmit(db.tbl_CauHinhNgayNghis.Where(p => p.MaThietLapID == obj.ID));
                db.tbl_ThietLapNgayNghis.DeleteOnSubmit(obj);
                try
                {
                    db.SubmitChanges();
                    DialogBox.Alert("Xóa tòa nhà thành công!");
                    LoadToaNha((int?)gvDanhMuc.GetFocusedRowCellValue("ID"));
                }
                catch ( Exception ex)
                {
                    DialogBox.Alert("Có lỗi xảy ra:" + ex.Message);
                }
            }
        }

    }
}