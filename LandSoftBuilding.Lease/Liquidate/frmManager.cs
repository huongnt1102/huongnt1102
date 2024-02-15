using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;


namespace LandSoftBuilding.Lease.Liquidate
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        public frmManager()
        {
            InitializeComponent();
        }

        #region Ham xu ly
        void LoadData()
        {
            try
            {
                gcThanhLy.DataSource = null;
                gcThanhLy.DataSource = linqInstantFeedbackSource1;
            }
            catch { }
        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }

        void Edit()
        {
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn [hợp đồng], xin cảm ơn.");
                    return;
                }

                using (frmEdit frm = new frmEdit() { MaTN = (byte?)itemToaNha.EditValue, ID = id })
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch { }
        }

        void Delete()
        {
            int[] indexs = gvThanhLy.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những Hợp đồng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();
            try
            {
                foreach (int i in indexs)
                {
                    int id = (int)gvThanhLy.GetRowCellValue(i, "ID");

                    var CheckPT = from pt in db.ptPhieuThus
                                join hd in db.ctHopDongs on pt.MaKH equals hd.MaKH
                                join tl in db.ctThanhLies on hd.ID equals tl.MaHD
                                where tl.ID == id && pt.MaPL == 32
                                select pt;
                    var CheckPC = from pc in db.pcPhieuChis
                                join hd in db.ctHopDongs on pc.MaNCC equals hd.MaKH
                                join tl in db.ctThanhLies on hd.ID equals tl.MaHD
                                where tl.ID == id && pc.MaPhanLoai == 8
                                select pc;
                    if (CheckPT.Any())
                    {
                        DialogBox.Alert("Đã tồn tại phiếu thu/chi của Hợp đồng thanh lý này. Xóa phiếu thu trước. Xin cảm ơn!");
                        return;
                    }
                    if (CheckPC.Any())
                    {
                        DialogBox.Alert("Đã tồn tại phiếu thu/chi của Hợp đồng thanh lý này. Xóa phiếu thu trước. Xin cảm ơn!");
                        return;
                    }

                    var objHD = db.ctThanhLies.Single(p => p.ID == id);

                    db.ctThanhLies.DeleteOnSubmit(objHD);
                }

                db.SubmitChanges();

                this.RefreshData();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
            }
            finally
            {
                db.Dispose();
            }
        }

        void Detail()
        {

            var maTN = (byte)itemToaNha.EditValue;

            var db = new MasterDataContext();
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("ID");
                if (id == null)
                {
                    switch (tabMain.SelectedTabPageIndex)
                    {
                        case 0:
                            gcPhuLuc.DataSource = null;
                            break;
                        case 1:

                            break;
                        case 2:
                            gvPhieuChi.DataSource = null;
                            break;
                        case 3:
                            gcPhieuThu.DataSource = null;
                            break;
                        case 4:
                            gcPhieuThuKT.DataSource = null;
                            break;
                    }
                    return;
                }

                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        // gcPhuLuc.DataSource = from 
                        break;
                    case 1:

                        // gcPhuLuc.DataSource = from pc in db.pcPhieuChis

                        break;
                    case 2:

                        gvPhieuChi.DataSource = from p in db.pcPhieuChis
                                                    //join pl in db.PhanLoaiChis on p.LoaiChi equals pl.ID into tblPhanLoai
                                                    //from pl in tblPhanLoai.DefaultIfEmpty()
                                                join k in db.tnKhachHangs on p.MaNCC equals k.MaKH
                                                into tblKhachHang
                                                from k in tblKhachHang.DefaultIfEmpty()
                                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                                join nvd in db.tnNhanViens on p.MaNVN equals nvd.MaNV into tblNguoiDuyet
                                                from nvd in tblNguoiDuyet.DefaultIfEmpty()
                                                join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                                from nh in tblNganHang.DefaultIfEmpty()
                                                join hd in db.ctHopDongs on k.MaKH equals hd.MaKH
                                                join tl in db.ctThanhLies on hd.ID equals tl.MaHD
                                                where p.MaTN == maTN && tl.ID == id && p.MaPhanLoai== 8
                                                select new
                                               {
                                                   p.ID,

                                                   p.SoPC,
                                                   NVDuyet = nvd.HoTenNV,
                                                   p.NgayChi,
                                                   p.SoTien,
                                                   TenKH = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                                                   HTTT = p.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                                   NguoiChi = nv.HoTenNV,
                                                   p.NguoiNhan,
                                                   p.DiaChiNN,
                                                   k.KyHieu,
                                                   p.LyDo,

                                                   p.ChungTuGoc,

                                                   p.NgayNhap,
                                                   NguoiSua = nvs.HoTenNV,
                                                   p.NgaySua,
                                                   tk.SoTK,
                                                   nh.TenNH,
                                               };
                        break;
                    case 3:
                        gcPhieuThu.DataSource = from p in db.ptPhieuThus
                                                    //join pl in db.PhanLoaiChis on p.LoaiChi equals pl.ID into tblPhanLoai
                                                    //from pl in tblPhanLoai.DefaultIfEmpty()
                                                join k in db.tnKhachHangs on p.MaKH equals k.MaKH
                                                into tblKhachHang
                                                from k in tblKhachHang.DefaultIfEmpty()
                                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                                join nvd in db.tnNhanViens on p.MaNVN equals nvd.MaNV into tblNguoiDuyet
                                                from nvd in tblNguoiDuyet.DefaultIfEmpty()
                                                join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                                from nh in tblNganHang.DefaultIfEmpty()
                                                join hd in db.ctHopDongs on k.MaKH equals hd.MaKH
                                                join tl in db.ctThanhLies on hd.ID equals tl.MaHD
                                                where p.MaTN == maTN && tl.ID == id && p.MaPL == 47
                                                select new
                                                {
                                                    p.ID,

                                                    p.SoPT,
                                                    NVDuyet = nvd.HoTenNV,
                                                    p.NgayThu,
                                                    p.SoTien,
                                                    TenKH = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                                                    HTTT = p.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                                    NguoiChi = nv.HoTenNV,
                                                    p.NguoiNop,
                                                    p.DiaChiNN,
                                                    k.KyHieu,
                                                    p.LyDo,

                                                    p.ChungTuGoc,

                                                    p.NgayNhap,
                                                    NguoiSua = nvs.HoTenNV,
                                                    p.NgaySua,
                                                    tk.SoTK,
                                                    nh.TenNH,
                                                };
                        break;
                    case 4:
                        gcPhieuThuKT.DataSource = from p in db.ptPhieuThus
                                                    //join pl in db.PhanLoaiChis on p.LoaiChi equals pl.ID into tblPhanLoai
                                                    //from pl in tblPhanLoai.DefaultIfEmpty()
                                                join k in db.tnKhachHangs on p.MaKH equals k.MaKH
                                                into tblKhachHang
                                                from k in tblKhachHang.DefaultIfEmpty()
                                                join nv in db.tnNhanViens on p.MaNV equals nv.MaNV
                                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV
                                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into tblNguoiSua
                                                from nvs in tblNguoiSua.DefaultIfEmpty()
                                                join nvd in db.tnNhanViens on p.MaNVN equals nvd.MaNV into tblNguoiDuyet
                                                from nvd in tblNguoiDuyet.DefaultIfEmpty()
                                                join tk in db.nhTaiKhoans on p.MaTKNH equals tk.ID into tblTaiKhoan
                                                from tk in tblTaiKhoan.DefaultIfEmpty()
                                                join nh in db.nhNganHangs on tk.MaNH equals nh.ID into tblNganHang
                                                from nh in tblNganHang.DefaultIfEmpty()
                                                join hd in db.ctHopDongs on k.MaKH equals hd.MaKH
                                                join tl in db.ctThanhLies on hd.ID equals tl.MaHD
                                                where p.MaTN == maTN && tl.ID == id && p.IsKhauTru == true
                                                select new
                                                {
                                                    p.ID,

                                                    p.SoPT,
                                                    NVDuyet = nvd.HoTenNV,
                                                    p.NgayThu,
                                                    SoTien = p.ptChiTietPhieuThus.Sum(ct => ct.KhauTru.GetValueOrDefault() + ct.SoTien.GetValueOrDefault()),
                                                    TenKH = k.IsCaNhan == true ? k.HoKH + " " + k.TenKH : k.CtyTen,
                                                    HTTT = p.MaTKNH != null ? "Chuyển khoản" : "Tiền mặt",
                                                    NguoiChi = nv.HoTenNV,
                                                    p.NguoiNop,
                                                    p.DiaChiNN,
                                                    k.KyHieu,
                                                    p.LyDo,

                                                    p.ChungTuGoc,

                                                    p.NgayNhap,
                                                    NguoiSua = nvs.HoTenNV,
                                                    p.NgaySua,
                                                    tk.SoTK,
                                                    nh.TenNH,
                                                };
                        break;
                }
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void TraTien()
        {
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn bản ghi, xin cảm ơn.");
                    return;
                }

                var _PhaiTra = (decimal)(gvThanhLy.GetFocusedRowCellValue("PhaiTra") ?? 0);
                if (_PhaiTra <= 0)
                {
                    DialogBox.Error("Số còn lại phải trả phải lớn hơn 0");
                    return;
                }

                var objPCCT = new Fund.Output.ChiTietPhieuChiItem();
                objPCCT.LinkID = id;
                objPCCT.SoTien = _PhaiTra;
                objPCCT.DienGiai = string.Format("Chi tiền thanh lý cho thuê số: {0}", gvThanhLy.GetFocusedRowCellValue("SoTL"));

                using (var frm = new Fund.Output.frmEdit())
                {
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.MaKH = (int)gvThanhLy.GetFocusedRowCellValue("MaKH");
                    frm.MaTL = id;
                    frm.TableName = "ctThanhLy";
                    frm.ChiTiets = new List<Fund.Output.ChiTietPhieuChiItem>();
                    frm.ChiTiets.Add(objPCCT);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch { }
        }

        void ThuTien()
        {
            try
            {
                var id = (int?)gvThanhLy.GetFocusedRowCellValue("ID");

                if (id == null)
                {
                    DialogBox.Error("Vui lòng chọn bản ghi, xin cảm ơn.");
                    return;
                }

                var _PhaiThu = (decimal)(gvThanhLy.GetFocusedRowCellValue("PhaiThu") ?? 0);
                if (_PhaiThu <= 0)
                {
                    DialogBox.Error("Số còn lại phải thu phải lớn hơn 0");
                    return;
                }

                var objPTCT = new Fund.Input.ChiTietPhieuThuItem();
                objPTCT.LinkID = id;
                objPTCT.SoTien = _PhaiThu;
                objPTCT.DienGiai = string.Format("Thu tiền thanh lý hợp đồng cho thuê số: {0}", gvThanhLy.GetFocusedRowCellValue("SoTL"));

                using (var frm = new Fund.Input.frmEdit())
                {
                    frm.MaTN = (byte?)itemToaNha.EditValue;
                    frm.MaKH = (int)gvThanhLy.GetFocusedRowCellValue("MaKH");
                    frm.MaTL = id;
                    frm.ChiTiets = new List<Fund.Input.ChiTietPhieuThuItem>();
                    frm.ChiTiets.Add(objPTCT);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        this.RefreshData();
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Event
        private void frmHopDong_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            gvThanhLy.CustomColumnDisplayText += Common.GridViewCustomColumnDisplayText;

            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var db = new MasterDataContext();
            e.QueryableSource = from p in db.ctThanhLies
                                join hd in db.ctHopDongs on p.MaHD equals hd.ID
                                join kh in db.tnKhachHangs on hd.MaKH equals kh.MaKH
                                join lt in db.LoaiTiens on p.MaLT equals lt.ID into loaiTien
                                from lt in loaiTien.DefaultIfEmpty()
                                join nvn in db.tnNhanViens on p.MaNVN equals nvn.MaNV into nhanVienNhap
                                from nvn in nhanVienNhap.DefaultIfEmpty()
                                join nvs in db.tnNhanViens on p.MaNVS equals nvs.MaNV into viewNhanVienSua
                                from nvs in viewNhanVienSua.DefaultIfEmpty()
                                where p.MaTN == (byte?)itemToaNha.EditValue
                                orderby p.NgayTL descending
                                select new
                                {
                                    p.ID,
                                    p.SoTL,
                                    p.NgayTL,
                                    lt.KyHieuLT,
                                    p.TienTL,
                                    p.TyGia,
                                    p.TienTLQD,
                                    p.DaThu,
                                    p.PhaiThu,
                                    p.DaTra,
                                    p.PhaiTra,
                                    SoHD = hd.SoHDCT,
                                    kh.MaKH,
                                    TenKH = kh.IsCaNhan == true ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                    p.LyDo,
                                    p.MaNVN,
                                    TenNVN = nvn.HoTenNV,
                                    p.NgayNhap,
                                    TenNVS = nvs.HoTenNV,
                                    p.NgaySua
                                };
            e.Tag = db;
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete();
        }

        private void gvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Detail();
        }

        private void gvHopDong_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            this.Detail();
        }
        #endregion

        private void btnExportMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new DevExpress.XtraGrid.Design.frmDesigner();
            frm.InitGrid(gcThanhLy);
            frm.ShowDialog();
        }

        private void tabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.Detail();
        }

        private void itemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ThuTien();
        }

        private void itemTraTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.TraTien();
        }
    }
}