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
using System.Data.Linq.SqlClient;
using System.Collections;
using DevExpress.XtraReports.UI;

namespace DichVu.KhachHang
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public tnNhanVien objnhanvien;
        int MaKH;
        bool first = true;
        private bool IsNhaCungCap = false;
        public delegate void AddItemDelegate(int MaBM);
        
        public frmManager()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this,barManager1);
           
        }
        
        private bool IsCaNhan
        {
            get
            {
                return tabKhachHang.SelectedTabPageIndex == 0;
            }
            set
            {
                tabKhachHang.SelectedTabPageIndex = value ? 0 : 1;
            }
        }

        #region Load Data
        void LoadData()
        {
            db = new MasterDataContext();
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;

            #region Thay het khach hang

            if (this.IsNhaCungCap)
            {

                #region Nhà cung cấp

                gcNhaCungCap.DataSource = from c in db.tnKhachHangs
                                          join d in db.khNhomKhachHangs on c.MaNKH equals d.ID
                                              into tblNhomKH
                                          from d in tblNhomKH.DefaultIfEmpty()
                                          
                                          where c.isNCC != null & c.isNCC == true & c.MaTN == maTN & (c.IsCoHoi == null | c.IsCoHoi == false)
                                          select new
                                          {
                                              c.KyHieu,
                                              c.MaKH,
                                              c.CtyTenVT,
                                              c.CtyTen,
                                              CtyDiaChi = c.DCLL,
                                              CtyDienThoai = c.DienThoaiKH,
                                              c.EmailKH,
                                              TenKV = c.MaKV != null ? c.tnKhuVuc.TenKV : "",
                                              c.CtyMaSoThue,
                                              c.tnNhanVien.HoTenNV,
                                              d.TenNKH,
                                              c.MaPhu,
                                              c.GhiChu,
                                              SoTKNganHang = c.CtySoTKNH,
                                              TenNganHang = c.CtyTenNH,
                                              c.CtyFax,
                                              c.smsZalo,
                                              c.issmsZalo,
                                              c.nameZalo,
                                              c.NganhNgheDoanhNghiep,
                                              c.Reference,
                                              c.SAP_CSHLS
                                          };

                #endregion

                grvNhaCungCap.FocusedRowHandle = -1;
            }
            else
            {
                if (this.IsCaNhan)
                {
                    #region Ca nhan
                    gcCaNhan.DataSource = from c in db.tnKhachHangs
                                          join d in db.khNhomKhachHangs on c.MaNKH equals d.ID
                                          into tblNhomKH
                                          from d in tblNhomKH.DefaultIfEmpty()
                                          join lkh in db.khLoaiKhachHangs on c.MaLoaiKH equals lkh.ID into loaiKH
                                          from lkh in loaiKH.DefaultIfEmpty()
                                          join qt in db.QuocTiches on c.MaQT equals qt.ID into QuocTich from qt in QuocTich.DefaultIfEmpty()
                                          join t in db.Tinhs on c.MaTinh equals t.MaTinh into Tinh from t in Tinh.DefaultIfEmpty()
                                          join h in db.Huyens on c.MaHuyen equals h.MaHuyen into Huyen from h in Huyen.DefaultIfEmpty()
                                          where c.IsCaNhan == this.IsCaNhan & c.MaTN == maTN & (c.isNCC == null | c.isNCC != true) & (c.IsCoHoi == null | c.IsCoHoi == false)
                                          select new
                                          {
                                              c.KyHieu,
                                              c.MaPhu,
                                              c.MaKH,
                                              c.HoKH,
                                              c.TenKH,
                                              c.TaiKhoanNganHang,
                                              c.NoiCap,
                                              c.NgayCap,
                                              GioiTinh = c.GioiTinh.Value ? "Nam" : "Nữ",
                                              c.NgaySinh,
                                              c.CMND,
                                              c.DienThoaiKH,
                                              c.EmailKH,
                                              c.DCLL,
                                              c.DCTT,
                                              TenKV = c.MaKV != null ? c.tnKhuVuc.TenKV : "",
                                              //c.QuocTich,
                                              c.MaSoThue,
                                              c.tnNhanVien.HoTenNV,
                                              TenNKH = d.TenNKH,
                                              lkh.TenLoaiKH,
                                              c.NguoiDongSoHuu,
                                              c.smsZalo,
                                              c.issmsZalo,
                                              c.nameZalo,
                                              c.DiaChiNhanThu,
                                              c.Website,
                                              c.NganhNgheDoanhNghiep
                                              , c.EmailKhachThue
                                              , c.DiaPhan
                                              , c.Reference,
                                              c.SAP_CSHLS,
                                              QuocTich = qt.TenQuocTich,
                                              TenTinh = t.TenHienThi,
                                              TenHuyen = h.TenHienThi
                                          };
                    #endregion

                    grvCaNhan.FocusedRowHandle = -1;
                    if ((int?)grvCaNhan.GetFocusedRowCellValue("MaKH") == null)
                    {
                        switch (xtraTabControl1.SelectedTabPageIndex)
                        {
                            case 0:
                                LoadTheXe(0);
                                break;
                            case 1:
                                LoadTheThangMay(0);
                                break;
                            case 2:
                                LoadDichVuKhac(0);
                                break;
                            case 3:
                                LoadNhanKhau(0);
                                break;
                            case 4:
                                LoadLichSuCapNhat(0);
                                break;
                            case 5:
                                LoadLichSuGiaoDich(0);
                                break;
                            case 6:
                                LoadYeuCau(0);
                                break;
                            case 7:
                                LoadBieuMau(0);
                                break;
                            case 8:
                                ctlTaiLieu1.FormID = 19;
                                ctlTaiLieu1.LinkID = 0;
                                ctlTaiLieu1.MaNV = objnhanvien.MaNV;
                                ctlTaiLieu1.objNV = objnhanvien;
                                ctlTaiLieu1.TaiLieu_Load();
                                break;
                        }
                    }
                }
                else
                {
                    #region Cty
                    gcDoanhNghiep.DataSource = from c in db.tnKhachHangs
                                               join d in db.khNhomKhachHangs on c.MaNKH equals d.ID
                                               into tblNhomKH
                                               from d in tblNhomKH.DefaultIfEmpty()
                                               join lkh in db.khLoaiKhachHangs on c.MaLoaiKH equals lkh.ID into loaiKH
                                               from lkh in loaiKH.DefaultIfEmpty()
                                               where c.IsCaNhan == this.IsCaNhan & c.MaTN == maTN & (c.isNCC == null | c.isNCC != true) & (c.IsCoHoi == null | c.IsCoHoi == false)
                                               select new
                                               {
                                                   c.KyHieu,
                                                   c.MaKH,
                                                   c.CtyTenVT,
                                                   c.CtyTen,
                                                   c.NguoiLH,
                                                   c.SDTNguoiLH,
                                                   c.CtyNoiDKKD,
                                                   CtyDiaChi = c.DCLL,
                                                   c.DienThoaiKH,
                                                   c.EmailKH,
                                                   c.CtyFax,
                                                   c.TaiKhoanNganHang,
                                                   c.CtyNgayDKKD,
                                                   c.CtyTenNH,
                                                   c.CtyNguoiDD,
                                                   c.CtyChucVuDD,
                                                   TenKV = c.MaKV != null ? c.tnKhuVuc.TenKV : "",
                                                   c.CtyMaSoThue,
                                                   c.tnNhanVien.HoTenNV,
                                                   d.TenNKH,
                                                   c.MaPhu,
                                                   lkh.TenLoaiKH,
                                                   c.smsZalo,
                                                   c.issmsZalo,
                                                   c.nameZalo,
                                                   c.DiaChiNhanThu,
                                                   c.Website,
                                                   //NgheNghiep = string.Join(",", 
                                                   //(from nn in db.NgheNghieps
                                                   // join kh in db.NgheNghiepKHs on nn.MaNN equals kh.MaNN
                                                   // where kh.MaKH == c.MaKH
                                                   // select new { nn.TenNN}).Select(p=>p.TenNN))
                                                   c.EmailKhachThue
                                                   , c.DiaPhan
                                                   , c.NganhNgheDoanhNghiep
                                                   , c.Reference,
                                                   c.SAP_CSHLS

                                               };
                    #endregion
                    grvDoanhNghiep.FocusedRowHandle = -1;
                    if ((int?)grvDoanhNghiep.GetFocusedRowCellValue("MaKH") == null)
                    {
                        switch (xtraTabControl1.SelectedTabPageIndex)
                        {
                            case 0:
                                LoadTheXe(0);
                                break;
                            case 1:
                                LoadTheThangMay(0);
                                break;
                            case 2:
                                LoadDichVuKhac(0);
                                break;
                            case 3:
                                LoadNhanKhau(0);
                                break;
                            case 4:
                                LoadLichSuCapNhat(0);
                                break;
                            case 5:
                                LoadLichSuGiaoDich(0);
                                break;
                            case 6:
                                LoadYeuCau(0);
                                break;
                            case 7:
                                LoadBieuMau(0);
                                break;
                            case 8:
                                ctlTaiLieu1.FormID = 19;
                                ctlTaiLieu1.LinkID = 0;
                                ctlTaiLieu1.MaNV = objnhanvien.MaNV;
                                ctlTaiLieu1.objNV = objnhanvien;
                                ctlTaiLieu1.TaiLieu_Load();
                                break;
                        }
                    }
                }
            }

            #endregion
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, objnhanvien, barManager1);

            lookUpToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            if (Common.User.MaTN == 60 | Common.User.MaTN == 59 | Common.User.MaTN == 58)//23/5/2016 ở trung tín thì mở tab doanh nghiệp đầu tiên
            {
                tabKhachHang.SelectedTabPageIndex = 1;

            }
            LoadData();
            grvCaNhan_FocusedRowChanged(null, null);

            first = false;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Delete(DevExpress.XtraGrid.Views.Grid.GridView grv)
        {
            int[] indexs;//= grv.GetSelectedRows();

            if (this.IsNhaCungCap)
                indexs = grvNhaCungCap.GetSelectedRows();
            else
            {
                if (this.IsCaNhan)
                {
                    indexs = grv.GetSelectedRows();
                }
                else
                {
                    indexs = grvDoanhNghiep.GetSelectedRows();
                }
            }


            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in indexs)
            {
                int MaKH = 0;
                if (this.IsNhaCungCap)
                {
                    MaKH = (int)grvNhaCungCap.GetRowCellValue(i, "MaKH");
                }
                else
                {
                    if (this.IsCaNhan)
                    {
                        MaKH = (int)grv.GetRowCellValue(i, "MaKH");
                    }
                    else
                    {
                        MaKH = (int)grvDoanhNghiep.GetRowCellValue(i, "MaKH");
                        var check = db.NgheNghiepKHs.Where(p => p.MaKH == MaKH);
                        if (check.Count() > 0)
                        {
                            db.NgheNghiepKHs.DeleteAllOnSubmit(check);
                            db.SubmitChanges();
                        }
                    }
                }

                tnKhachHang objKH = db.tnKhachHangs.Single(p => p.MaKH == MaKH);
                db.tnKhachHangs.DeleteOnSubmit(objKH);

                // kiểm tra tài khoản dùng app
                var resident_item = db.app_Residents.Where(_ => _.Phone == objKH.DienThoaiKH);
                foreach(var item in resident_item)
                {
                    if (item.IsEmployee.GetValueOrDefault() == true)
                    {
                        item.IsResident = false;
                    }
                    else
                    {
                        // không phải là nhân viên, chỉ lả khách hàng thôi
                        try
                        {
                            db.app_Residents.DeleteOnSubmit(item);
                        }
                        catch (System.Exception ex) { }
                        
                    }
                }
            }
            try
            {
                

                db.SubmitChanges();

                if (this.IsNhaCungCap) grvNhaCungCap.DeleteSelectedRows();
                else
                {
                    if (this.IsCaNhan) grvCaNhan.DeleteSelectedRows();
                    else grvDoanhNghiep.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Không xóa được dữ liệu vì ràng buộc dữ liệu!");
            }

            
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Delete(this.IsCaNhan ? grvCaNhan : grvDoanhNghiep);
        }

        void Edit(DevExpress.XtraGrid.Views.Grid.GridView grv)
        {
            //if (grv.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn khách hàng");
            //    return;
            //}

            //frmEdit frm = new frmEdit() { maTN = (byte)itemToaNha.EditValue, objnv = objnhanvien, objKH = db.tnKhachHangs.Single(p => p.MaKH == (int)grv.GetFocusedRowCellValue("MaKH")), IsCaNhan = this.IsCaNhan };
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    this.IsCaNhan = frm.IsCaNhan;
            //    LoadData();
            //}20/08/2018
            if (grv.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            DichVu.KhachHang.frmEdit frm = new DichVu.KhachHang.frmEdit() { maTN = (byte)itemToaNha.EditValue, objnv = objnhanvien, objKH = db.tnKhachHangs.Single(p => p.MaKH == (int)grv.GetFocusedRowCellValue("MaKH")), IsCaNhan = this.IsCaNhan };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.IsNhaCungCap = frm.IsNCC;
                if (!this.IsNhaCungCap)
                    this.IsCaNhan = frm.IsCaNhan;
                
            }
            LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabKhachHang.SelectedTabPageIndex == 2)
            {
                this.Edit(grvNhaCungCap);
            }
            else
            {
                this.Edit(this.IsCaNhan ? grvCaNhan : grvDoanhNghiep);
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khách hàng", "Thêm", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            //frmEdit frm = new frmEdit();
            //frm.maTN = (byte)itemToaNha.EditValue;
            //frm.objnv = objnhanvien;
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    this.IsCaNhan = frm.IsCaNhan;
            //    LoadData();
            //}
            frmEdit frm = new frmEdit();
            frm.maTN = (byte)itemToaNha.EditValue;
            frm.objnv = objnhanvien;
            if (this.IsNhaCungCap)
            { frm.IsNCC = true; }
            else
            {
                frm.IsNCC = false; 
            }
            
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.IsCaNhan = frm.IsCaNhan;
                LoadData();
            }
        }

        private void tabKhachHang_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //LoadData();
            if (tabKhachHang.SelectedTabPageIndex == 2)
            {
                this.IsNhaCungCap = true;
            }
            else
            {
                this.IsNhaCungCap = false;
            }
            LoadData();
        }
        #endregion

        #region Load Detail Data

        #region Danh sách người liên hệ
        public class tnkhachhang_nguoilienhe_getlist
        {
            public int ID { get; set; }
            public string HoVaTen { get; set; }
            public string Email { get; set; }
            public string DienThoai { get; set; }
            public string DiaChi { get; set; }
            public string GhiChu { get; set; }
            public string BoPhan { get; set; }
            public string NhomLienHe { get; set; }
        }
        #endregion

        private void LoadDetail()
        {
            //var wait = DialogBox.WaitingForm();
            try
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        LoadTheXe(MaKH);
                        break;
                    case 1:
                        LoadTheThangMay(MaKH);
                        break;
                    case 2:
                        LoadDichVuKhac(MaKH);
                        break;
                    case 3:
                        LoadNhanKhau(MaKH);
                        break;
                    case 4:
                        LoadLichSuCapNhat(MaKH);
                        break;
                    case 5:
                        LoadLichSuGiaoDich(MaKH);
                        break;
                    case 6:
                        LoadYeuCau(MaKH);
                        break;
                    case 7:
                        LoadBieuMau(MaKH);
                            break;
                    case 8:
                        ctlTaiLieu1.FormID = 19;
                        ctlTaiLieu1.LinkID = MaKH;
                        ctlTaiLieu1.MaNV = objnhanvien.MaNV;
                        ctlTaiLieu1.objNV = objnhanvien;
                        ctlTaiLieu1.frm = this;
                        ctlTaiLieu1.TaiLieu_Load();
                        break;
                    case 9:
                        gcLSDoiTen.DataSource = (from ls in db.LichSuThayDoiKHs
                                                 where ls.MaKH == MaKH
                                                 select ls
                            ).ToList();
                        break;
                    case 10:
                        gcLichSuQuanTam.DataSource = (from ls in db.web_ZaloFollows
                                                      where ls.MaKH == MaKH
                                                      select ls
                            ).ToList();
                        break;
                    case 11:
                        var model_nlh = new { makh = MaKH };
                        var param_nlh = new Dapper.DynamicParameters();
                        param_nlh.AddDynamicParams(model_nlh);
                        gcNguoiLienHe.DataSource = Library.Class.Connect.QueryConnect.Query<tnkhachhang_nguoilienhe_getlist>("tnkhachhang_nguoilienhe_getlist", param_nlh);
                        break;
                }
            }
            catch { }
            //finally
            //{
            //    wait.Close();
            //    wait.Dispose();
            //}
        }

        void LoadBieuMau(int MaKH)
        {
            gcBieuMau.DataSource = db.tnKhachHang_BieuMaus.Where(p => p.MaKH == MaKH)
                .Select(p => new
                {
                    p.ID,
                    p.tnNhanVien.HoTenNV,
                    p.NoiDung,
                    p.NgayThem,
                    p.TenBieuMau
                });
        }

        private void LoadYeuCau(int MaKH)
        {
            gcYeuCau.DataSource = db.tnycYeuCaus
                .Where(p => p.MaKH == MaKH).OrderByDescending(p => p.NgayYC)
                .Select(p => new
                {
                    p.ID,
                    TieuDeYC = p.TieuDe,
                    NoiDungYC = p.NoiDung,
                    BoPhanYC = p.tnPhongBan.TenPB,
                    TrangThaiYC = p.tnycTrangThai.TenTT,
                    NgayYC = p.NgayYC,
                    NhanVienYC = p.tnNhanVien.HoTenNV
                }).ToList();
        }

        private void LoadLichSuGiaoDich(int MaKH)
        {
            gcLichSuGiaoDich.DataSource = db.lsGiaoDiches
                .OrderByDescending(p=>p.NgayLap)
                .Where(p => p.MaKH == MaKH)
                .Select(p => new
                {
                    p.ID,
                    SoHopDongLS = p.thueHopDong.SoHD,
                    KhachHangLS = string.Format("{0} {1}",p.tnKhachHang.HoKH,p.tnKhachHang.TenKH),
                    NhanVienLS = p.tnNhanVien.HoTenNV,
                    MatBangLS = p.mbMatBang.MaSoMB,
                    TrangThaiLS = p.thueTrangThai.TenTT,
                    NgayLapLS = p.NgayLap,
                    DienGiaiLS = p.DienGiai
                }).ToList();
        }

        private void LoadTheThangMay(int MaKH)
        {
            gcTheThangMay.DataSource = db.dvgxTheXes.Where(p => p.MaKH == MaKH & p.IsThangMay.GetValueOrDefault() == true & p.NgungSuDung == false)
                .Select(p => new
                {
                    p.ID,
                    SoThe = p.SoThe,
                    ChuThe = p.ChuThe,
                    NgayDangKy = p.NgayDK,
                    PhiLamThe = p.PhiLamThe ?? 0,
                    GhiChu = p.DienGiai
                }).ToList();
        }

        private void LoadTheXe(int MaKH)
        {
            gcTheXe.DataSource = db.dvgxTheXes.Where(p => p.MaKH == MaKH & p.IsThangMay.GetValueOrDefault()==false & p.NgungSuDung == false)
                .Select(p => new
                {
                    p.ID,
                    SoThe = p.SoThe,
                    ChuThe = p.ChuThe,
                    NgayDangKy = p.NgayDK,
                    LoaiXe =p.dvgxLoaiXe.TenLX ,
                    BienSo = p.BienSo,
                    PhiLamThe = p.PhiLamThe??0,
                    DienGiai = p.DienGiai
                }).ToList();
        }

        private void LoadDichVuKhac(int MaKH)
        {
            gcDichVuKhac.DataSource = (from dv in db.dvDichVuKhacs
                                       join nv in db.tnNhanViens on dv.MaNVN equals nv.MaNV
                                       join ldv in db.dvLoaiDichVus on dv.MaLDV equals ldv.ID
                                       where dv.MaKH == MaKH & dv.IsNgungSuDung == false
                                       select new
                                       {
                                           LoaiDichVu=ldv.TenHienThi,
                                           NgayDK = dv.NgayCT,
                                           PhiDK = 0,
                                           PhiDV = dv.TienTT,
                                           ThanhToanPhi = dv.ThanhTien,
                                           NhanVien = nv.HoTenNV,
                                           GhiChu = dv.DienGiai,
                                           MaDV = dv.ID
                                       }).ToList();
        }

        private void LoadNhanKhau(int MaKH)
        {
            gcNhanKhau.DataSource = db.tnNhanKhaus
                .Where(p => p.MaKH == MaKH)
                .Select(p => new
                {
                    p.ID,
                    HoTenNK = p.HoTenNK,
                    GioiTinhNK = (bool)p.GioiTinh?"Nam":"Nữ",
                    NgaySinhNK = p.NgaySinh,
                    CMNKNK = p.CMND,
                    CMNDNgayCapNK = p.NgayCap,
                    CMNDNoiCapNK = p.NoiCap,
                    DiaChiTT = p.DCTT,
                    DienThoaiNK = p.DienThoai,
                    EmailNK = p.Email,
                    DienGiaiNK =p.DienGiai,
                    DaDangKyTT = (bool)p.DaDKTT?"Ðã đăng ký":"Chưa đăng ký",
                    NhanVienNK = p.tnNhanVien.HoTenNV
                }).ToList();
        }

        private void LoadLichSuCapNhat(int maKH)
        {
            gcLichSuCN.DataSource = from mb in db.mbMatBangs
                                    join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                    join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                    join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB
                                    join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT

                                    join lt in db.LoaiTiens on mb.MaLT equals lt.ID into tblLoaiTien
                                    from lt in tblLoaiTien.DefaultIfEmpty()
                                    join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH into tblKhachHang
                                    from kh in tblKhachHang.DefaultIfEmpty()
                                    join csh in db.tnKhachHangs on mb.MaKHF1 equals csh.MaKH into tblChuSoHuu
                                    from csh in tblChuSoHuu.DefaultIfEmpty()
                                    join nvn in db.tnNhanViens on mb.MaNVN equals nvn.MaNV
                                    join nvs in db.tnNhanViens on mb.MaNVS equals nvs.MaNV into tblNguoiSua
                                    from nvs in tblNguoiSua.DefaultIfEmpty()
                                    where mb.MaKH == maKH
                                    orderby mb.MaSoMB
                                    select new
                                    {
                                        mb.MaMB,
                                        mb.MaSoMB,
                                        mb.SoNha,
                                        tl.TenTL,
                                        kn.TenKN,
                                        lmb.TenLMB,
                                        tt.TenTT,
                                        mb.DienTich,
                                        mb.GiaThue,
                                        lt.KyHieuLT,
                                        TenKH = kh.IsCaNhan.GetValueOrDefault() ? (kh.HoKH + " " + kh.TenKH) : kh.CtyTen,
                                        TenCSH = csh.IsCaNhan.GetValueOrDefault() ? (csh.HoKH + " " + csh.TenKH) : csh.CtyTen,
                                        mb.IsCanHoCaNhan,
                                        mb.DaGiaoChiaKhoa,
                                        mb.NgayBanGiao,
                                        mb.DienGiai,
                                        NguoiNhap = nvn.HoTenNV,
                                        mb.NgayNhap,
                                        NguoiSua = nvs.HoTenNV,
                                        mb.NgaySua
                                    };
        }
        #endregion

        private void grvCaNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0) return;
            MaKH = (int)grvCaNhan.GetFocusedRowCellValue("MaKH");
            LoadDetail();            
        }

        private void grvDoanhNghiep_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDoanhNghiep.FocusedRowHandle < 0) return;
            MaKH = (int)grvDoanhNghiep.GetFocusedRowCellValue("MaKH");
            LoadDetail();
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import khách hàng cá nhân", "Import", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            using (KyThuat.KhachHang.Import.frmImport frm = new KyThuat.KhachHang.Import.frmImport() { objnhanvien = objnhanvien })
            {
                frm.MaTN = (byte)maTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import khách hàng doanh nghiệp", "Import", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            using (KyThuat.KhachHang.Import.frmImportDoanhNghiep frm = new KyThuat.KhachHang.Import.frmImportDoanhNghiep() { objnhanvien = objnhanvien })
            {
                frm.MaTN = (byte)maTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm biểu mẫu cho khách hàng", "Thêm", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            KyThuat.KhachHang.frmAddBM frm = new KyThuat.KhachHang.frmAddBM();
            frm.AddItemCallback = new AddItemDelegate(DelegateAddBieuMau);
            frm.ShowDialog();
        }

        private void DelegateAddBieuMau(int MaBM)
        {
            var wait = DialogBox.WaitingForm();

            int? mahd;
            switch (tabKhachHang.SelectedTabPage.Name)
            {
                case "tpCaNhan":
                    mahd = (int)grvCaNhan.GetFocusedRowCellValue("MaKH");
                    break;

                case "tpDoanhNghiep":
                    mahd = (int)grvCaNhan.GetFocusedRowCellValue("MaKH");
                    break;

                default:
                    mahd = null;
                    break;
            }

            if (mahd == null)
            {
                wait.Close();
                wait.Dispose();
                return;
            }
            var bm = db.BmBieuMaus.Single(p => p.MaBM == MaBM);
            //var objhd = db.thueHopDongs.Single(p => p.MaHD == mahd);
            //Library.BieuMauCls.ReplaceHopDongCls rpl = new Library.BieuMauCls.ReplaceHopDongCls() { RtfText = bm.Template /*HOP DONG THUE VAN PHONG*/ };
            //rpl.ThayNoiDungHD(objhd);
            var objhdbm = new tnKhachHang_BieuMau()
            {
                MaKH = MaKH,
                NgayThem = DateTime.Now,
                NoiDung = bm.Template,
                TenBieuMau = bm.TenBM,
                MaNV = objnhanvien.MaNV
            };

            db.tnKhachHang_BieuMaus.InsertOnSubmit(objhdbm);
            db.SubmitChanges();
            LoadData();

            switch (tabKhachHang.Name)
            {
                case "tpCaNhan":
                    grvCaNhan_FocusedRowChanged(null, null);
                    break;

                case "tpDoanhNghiep":
                    grvDoanhNghiep_FocusedRowChanged(null, null);
                    break;

                default:
                    break;
            }

            switch (tabKhachHang.SelectedTabPage.Name)
            {
                case "tpCaNhan":
                    grvCaNhan_FocusedRowChanged(null, null);
                    break;

                case "tpDoanhNghiep":
                    grvDoanhNghiep_FocusedRowChanged(null, null);
                    break;

                default:
                    break;
            }

            wait.Close();
            wait.Dispose();
        }

        private void btnXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu");
                return;
            }
            int idbm = (int)grvBieuMau.GetFocusedRowCellValue("ID");
            var bm = db.tnKhachHang_BieuMaus.Single(p => p.ID == idbm);
            using (LandsoftBuildingGeneral.DynBieuMau.frmPreview frm = new LandsoftBuildingGeneral.DynBieuMau.frmPreview())
            {
                frm.RtfText = bm.NoiDung;
                frm.ShowDialog();
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu");
                return;
            }

            using (MasterDataContext db = new MasterDataContext())
            {
                int idbm = (int)grvBieuMau.GetFocusedRowCellValue("ID");
                var bm = db.tnKhachHang_BieuMaus.Single(p => p.ID == idbm);

                using (LandsoftBuildingGeneral.DynBieuMau.frmDesign frm = new LandsoftBuildingGeneral.DynBieuMau.frmDesign())
                {
                    frm.RtfText = bm.NoiDung;
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        bm.NoiDung = frm.RtfText;
                        db.SubmitChanges();
                    }
                }
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu");
                return;
            }
            int idbm = (int)grvBieuMau.GetFocusedRowCellValue("ID");
            var bm = db.tnKhachHang_BieuMaus.Single(p => p.ID == idbm);
            if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
            {
                db.tnKhachHang_BieuMaus.DeleteOnSubmit(bm);
                db.SubmitChanges();
                LoadData();

                switch (tabKhachHang.Name)
                {
                    case "tpCaNhan":
                        grvCaNhan_FocusedRowChanged(null, null);
                        break;

                    case "tpDoanhNghiep":
                        grvDoanhNghiep_FocusedRowChanged(null, null);
                        break;

                    default:
                        break;
                }
            }
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBieuMau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn biểu mẫu");
                return;
            }
            int idbm = (int)grvBieuMau.GetFocusedRowCellValue("ID");
            var bm = db.tnKhachHang_BieuMaus.Single(p => p.ID == idbm);
            DevExpress.XtraRichEdit.RichEditControl rctl = new DevExpress.XtraRichEdit.RichEditControl();
            rctl.RtfText = bm.NoiDung;
            rctl.ShowPrintPreview();
        }

        private void exportKhuVuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var listKhuVuc = db.tnKhuVucs
                .Select(p=> new
                {
                    p.MaKV,
                    p.TenKV
                })
                .ToList();

            DataTable dt = new DataTable();

            dt = SqlCommon.LINQToDataTable(listKhuVuc);

            ExportToExcel.exportDataToExcel("Danh sách khu vực", dt);
        }

        private void exportCaNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcCaNhan);
        }

        private void exportDoanhNghiep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcDoanhNghiep);
        }

        private void btnThemTheXe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (DichVu.GiuXe.frmEdit frm = new DichVu.GiuXe.frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnXoaThe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(grvTheXe.FocusedRowHandle<0)
            {
                DialogBox.Alert("Vui lòng chọn thẻ xe");
                return;
            }
            try
            {
                var deleteitem = db.dvgxTheXes.Single(p => p.ID == (int)grvTheXe.GetFocusedRowCellValue("ID"));
                db.dvgxTheXes.DeleteOnSubmit(deleteitem);
                db.SubmitChanges();
                grvTheXe.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnAddTM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (DichVu.ThangMay.frmEdit frm = new DichVu.ThangMay.frmEdit() { objnhanvien = objnhanvien })
            using (var frm = new DichVu.Khac.frmEdit())
            {
                try
                {
                    
                }
                catch
                {
                }
                var mb = db.mbMatBangs.Where(p => p.MaKH == MaKH).FirstOrDefault();
                //if(mb==null)return;
                frm.MaTN =(byte?) itemToaNha.EditValue;
                frm.MaLDV = 26;
                frm.MaMB = mb!=null ? mb.MaMB : 0;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnXoaTheTM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTheThangMay.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn thẻ thang máy");
                return;
            }
            try
            {
                dvtmTheThangMay objTM = db.dvtmTheThangMays.Single(p => p.ID == (int)grvTheThangMay.GetFocusedRowCellValue("ID"));
                db.dvtmTheThangMays.DeleteOnSubmit(objTM);

                db.SubmitChanges();

                grvTheXe.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnThemNhanKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm nhân khẩu", "Thêm", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            using (DichVu.NhanKhau.frmEdit frm = new DichVu.NhanKhau.frmEdit())
            {
                frm.MaKH = MaKH;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadData();
            }
        }

        private void btnXoaNhanKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNhanKhau.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhân khẩu");
                return;
            }
            try
            {
               
                tnNhanKhau objNK = db.tnNhanKhaus.Single(p => p.ID == (int)grvNhanKhau.GetFocusedRowCellValue("ID"));
                db.tnNhanKhaus.DeleteOnSubmit(objNK);
                db.SubmitChanges();
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xóa nhân khẩu", "Xóa", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue)+"- Nhân khẩu: "+objNK.HoTenNK.ToString());
                grvNhanKhau.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnThemYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (DichVu.YeuCau.frmEdit frm = new DichVu.YeuCau.frmEdit() { objnhanvien = objnhanvien }) 20/5/2016
            //{
            //    try
            //    {
            //        frm.objmatbang = db.mbMatBangs.Where(p => p.MaKH == MaKH).First();
            //    }
            //    catch
            //    {
            //    }
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //        LoadData();
            //}
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Excel file (*.xls)|*.xls";
            if (f.ShowDialog() == DialogResult.OK)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var book = new LinqToExcel.ExcelQueryFactory(f.FileName);
                    var item = book.Worksheet(0).Select(p => new
                    {
                        MaPhu = p[0].ToString().Trim(),
                        KyHieu = p[1].ToString().Trim()
                    });

                    foreach (var it in item)
                    {
                        if (it.MaPhu != "")
                        {
                            var o = db.tnKhachHangs.Single(p => p.KyHieu == it.KyHieu);
                            o.MaPhu = it.MaPhu;
                            db.SubmitChanges();
                        }
                    }
                }
                catch
                {
                    DialogBox.Alert("Mẫu excel dùng để import dữ liệu không đúng định dạng, vui lòng xem lại");
                }
                finally
                {
                    wait.Close();
                    wait.Dispose();
                }
            }
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void itemUpdatePerson_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            using (KyThuat.KhachHang.Import.frmImport frm = new KyThuat.KhachHang.Import.frmImport() { objnhanvien = objnhanvien })
            {
                frm.MaTN = (byte)maTN;
                frm.IsUpdate = true;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemUpdateCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            using (KyThuat.KhachHang.Import.frmImportDoanhNghiep frm = new KyThuat.KhachHang.Import.frmImportDoanhNghiep() { objnhanvien = objnhanvien })
            {
                frm.MaTN = (byte)maTN;
                frm.IsUpdate = true;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDetail();
        }

        int getIDKH()
        {
            int MaKH;
            if (grvDoanhNghiep.FocusedRowHandle < 0)
                MaKH = (int)grvCaNhan.GetFocusedRowCellValue("MaKH");
            else
                MaKH = (int)grvDoanhNghiep.GetFocusedRowCellValue("MaKH");
            return MaKH;

        }

        private void itemThueVPNgoaiGio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBaoPhiNgoaiGio(MaKH, 1);
            //ctl.ShowDialog();
        }

        private void itemTBPhiDV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBaoPhiDV(MaKH, 1);
            //ctl.ShowDialog();
        }

        private void itemThueBTS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBao(MaKH, 1);
            //ctl.ShowDialog();
        }

        private void itemPostATM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBao(MaKH, 2);
            //ctl.ShowDialog();
        }

        private void itemThueBQC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBao(MaKH, 3);
            //ctl.ShowDialog();
        }

        private void itemThueVPLV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBao(MaKH, 4);
            //ctl.ShowDialog();
        }

        private void itemGBDien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBaoPhiDV(MaKH, 2);
            //ctl.ShowDialog();
        }

        private void itemGBNuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBaoPhiDV(MaKH, 3);
            //ctl.ShowDialog();
        }

        private void itemGBPhiGX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.FocusedRowHandle < 0 && grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng để in thông báo!");
                return;
            }
            //var rpt = new ReportMisc.BaoCaoCTM.rpt(MaKH);
            MaKH = getIDKH();
            //var ctl = new ReportMisc.BaoCaoCTM.ctlThongBaoPhiDV(MaKH, 4);
            //ctl.ShowDialog();
        }

        private void itemPhiDN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var rpt = new ReportMisc.BaoCaoCTM.rptThongBaoTienDienNuoc(1, 1, 1, DateTime.Now, 1);
            //rpt.ShowPreviewDialog();
        }

        private void itemPhiBTongHop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var rpt = new ReportMisc.BaoCaoCTM.rptThongBaoTenThueMB(1, 1, 1, DateTime.Now, 1);
            //rpt.ShowPreviewDialog();
        }

        private void btnCall_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var phone = (sender as ButtonEdit).Text ?? "";
            if (phone.Trim() == "") return;
            try
            {
                DIP.SwitchBoard.SwitchBoard.SoftPhone.Call(phone);
            }
            catch { }
        }

        private void btnCall2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var phone = (sender as ButtonEdit).Text ?? "";
            if (phone.Trim() == "") return;
            try
            {
                DIP.SwitchBoard.SwitchBoard.SoftPhone.Call(phone);
            }
            catch { }
        }

        private void itemSendMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new EmailAmazon.frmSendMail())
            {
                frm.MaTN = (byte?)itemToaNha.EditValue;
                frm.ShowDialog();
            }
        }

        private void gcCaNhan_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
            }
        }

        private void gcDoanhNghiep_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
            }
        }

        private void itemXoaSmsZalo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs;//= grv.GetSelectedRows();

            if (this.IsNhaCungCap)
                indexs = grvNhaCungCap.GetSelectedRows();
            else
            {
                if (this.IsCaNhan)
                {
                    indexs = grvCaNhan.GetSelectedRows();
                }
                else
                {
                    indexs = grvDoanhNghiep.GetSelectedRows();
                }
            }


            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in indexs)
            {
                int MaKH = 0;
                if (this.IsNhaCungCap)
                    MaKH = (int)grvNhaCungCap.GetRowCellValue(i, "MaKH");
                else
                {
                    if (this.IsCaNhan)
                    {
                        MaKH = (int)grvCaNhan.GetRowCellValue(i, "MaKH");
                    }
                    else
                    {
                        MaKH = (int)grvDoanhNghiep.GetRowCellValue(i, "MaKH");
                    }
                }
                var objKH = db.tnKhachHangs.FirstOrDefault(o=>o.MaKH == MaKH);
                if(objKH != null)
                {
                    objKH.smsZalo = "";
                    objKH.issmsZalo = false;
                    objKH.nameZalo = "";
                    db.SubmitChanges();
                }
            }
            DialogBox.Success("Cập nhật thành công!");
            LoadData();
        }

        private void itemImportNguoiLienHe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (DichVu.KhachHang.NguoiLienHe.frmImportNguoiLienHe frm = new DichVu.KhachHang.NguoiLienHe.frmImportNguoiLienHe())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemXoaNguoiLienHe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs;//= grv.GetSelectedRows();
            indexs = gvNguoiLienHe.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn người liên hệ");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in indexs)
            {
                int id = (int)gvNguoiLienHe.GetRowCellValue(i, "ID");
                tnKhachHang_NguoiLienHe nlh = db.tnKhachHang_NguoiLienHes.Single(_ => _.ID == id);
                db.tnKhachHang_NguoiLienHes.DeleteOnSubmit(nlh);
            }
            try
            {
                db.SubmitChanges();
                gvNguoiLienHe.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                DialogBox.Alert("Không xóa được dữ liệu vì ràng buộc dữ liệu!");
            }
        }

        private void itemThemNguoiLienHe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDoanhNghiep.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            var frm = new NguoiLienHe.frmEditNguoiLienHe() { MaKH = (int)grvDoanhNghiep.GetFocusedRowCellValue("MaKH"), Id = 0 };
            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK)
            {
                LoadDetail();
            }
        }

        private void itemSuaNguoiLienHe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gvNguoiLienHe.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn người liên hệ");
                return;
            }

            var frm = new NguoiLienHe.frmEditNguoiLienHe() { MaKH = (int)grvDoanhNghiep.GetFocusedRowCellValue("MaKH"), Id = (int)gvNguoiLienHe.GetFocusedRowCellValue("ID") };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadDetail();
            }
        }

        private void itemImportKhachHangNCC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import khách hàng cá nhân", "Import", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            using (KyThuat.KhachHang.Import.frmImport frm = new KyThuat.KhachHang.Import.frmImport() { objnhanvien = objnhanvien })
            {
                frm.MaTN = (byte)maTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void itemSyncToSAP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SAP.Funct.SyncCus.DongBo_N_KH((byte)itemToaNha.EditValue);
            }
            catch (System.Exception ex) { }
        }

        private void itemKiemTraDongBoSAP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (tabKhachHang.SelectedTabPageIndex == 2)
                {
                    this.KiemTraDongBoSAP(grvNhaCungCap);
                }
                else
                {
                    this.KiemTraDongBoSAP(this.IsCaNhan ? grvCaNhan : grvDoanhNghiep);
                }
            }
            catch { }
        }

        void KiemTraDongBoSAP(DevExpress.XtraGrid.Views.Grid.GridView grv)
        {
            if (grv.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            byte maTN = Convert.ToByte( itemToaNha.EditValue);

            var objToaNha = db.tnToaNhas.FirstOrDefault(_ => _.MaTN == maTN);
            if (objToaNha != null)
            {
                SAP.Funct.SyncCus.KiemTraDongBoSAP((int)grv.GetFocusedRowCellValue("MaKH"), objToaNha.TenVT);
            }

            
        }

        /// <summary>
        /// Kiểm tra tồn tại khách hàng trên SAP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (tabKhachHang.SelectedTabPageIndex == 2)
                {
                    this.KiemTraDongBoSAP(grvNhaCungCap);
                }
                else
                {
                    this.KiemTraDongBoSAP(this.IsCaNhan ? grvCaNhan : grvDoanhNghiep);
                }


            }
            catch { }
        }

        /// <summary>
        /// Đồng bộ tất cả khách hàng trong tòa nhà
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SAP.Funct.SyncCus.DongBo_N_KH((byte)itemToaNha.EditValue);

                LoadData();
            }
            catch (System.Exception ex) { }
        }

        /// <summary>
        /// Đồng bộ khách hàng chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs;//= grv.GetSelectedRows();

                if (this.IsNhaCungCap)
                    indexs = grvNhaCungCap.GetSelectedRows();
                else
                {
                    if (this.IsCaNhan)
                    {
                        indexs = grvCaNhan.GetSelectedRows();
                    }
                    else
                    {
                        indexs = grvDoanhNghiep.GetSelectedRows();
                    }
                }

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn khách hàng");
                    return;
                }

                foreach (int i in indexs)
                {
                    if (this.IsCaNhan)
                    {
                        MaKH = (int)grvCaNhan.GetRowCellValue(i, "MaKH");
                    }
                    else
                    {
                        MaKH = (int)grvDoanhNghiep.GetRowCellValue(i, "MaKH");
                    }

                    SAP.Funct.SyncCus.DongBo_1_KH(MaKH);
                }

                LoadData();
            }
            catch (System.Exception ex) { }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                byte maTN = Convert.ToByte(itemToaNha.EditValue);

                var objToaNha = db.tnToaNhas.FirstOrDefault(_ => _.MaTN == maTN);
                if (objToaNha != null)
                {
                    var frm = new PopupKhachHang.frmKiemTraDongBoSAP();
                    frm.TenTN = objToaNha.TenVT;
                    frm.ShowDialog();
                }


            }
            catch { }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            using (KyThuat.KhachHang.Import.frmUpdateInfoById frm = new KyThuat.KhachHang.Import.frmUpdateInfoById() { objnhanvien = objnhanvien })
            {
                frm.MaTN = (byte)maTN;
                frm.IsUpdate = true;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }
    }

    class ImportItem
    {
        public string KyHieu { get; set; }
        public string MaPhu { get; set; }
    }
}