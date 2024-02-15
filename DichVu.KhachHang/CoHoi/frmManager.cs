using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using KyThuat.KhachHang;
using Library;

namespace DichVu.KhachHang.CoHoi
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        //public tnNhanVien objnhanvien;
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

                LoadDataChiTiet(DichVu.KhachHang.Class.KhachHang.Loai.NCC, maTN);
                #endregion

                grvNhaCungCap.FocusedRowHandle = -1;
            }
            else
            {
                if (this.IsCaNhan)
                {
                    #region Ca nhan

                    LoadDataChiTiet(DichVu.KhachHang.Class.KhachHang.Loai.CA_NHAN, maTN);
                    #endregion

                    grvCaNhan.FocusedRowHandle = -1;
                    if ((int?)grvCaNhan.GetFocusedRowCellValue("MaKH") == null)
                    {
                        LoadChiTiet();
                    }
                }
                else
                {
                    #region Cty

                    LoadDataChiTiet(DichVu.KhachHang.Class.KhachHang.Loai.CTY, maTN);
                    #endregion
                    grvDoanhNghiep.FocusedRowHandle = -1;
                    if ((int?)grvDoanhNghiep.GetFocusedRowCellValue("MaKH") == null)
                    {
                        LoadChiTiet();
                    }
                }
            }

            #endregion
        }

        private void LoadDataChiTiet(string loai, int? maTN)
        {
            switch (loai)
            {
                case DichVu.KhachHang.Class.KhachHang.Loai.NCC:
                    gcNhaCungCap.DataSource = from c in db.tnKhachHangs
                        join d in db.khNhomKhachHangs on c.MaNKH equals d.ID
                            into tblNhomKh
                        from d in tblNhomKh.DefaultIfEmpty()
                        where c.isNCC != null & c.isNCC == true & c.MaTN == maTN & c.IsCoHoi == true
                        select new
                        {
                            c.KyHieu,
                            c.MaKH,
                            c.CtyTenVT,
                            c.CtyTen,
                            CtyDiaChi=c.DCLL,
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
                            HoTenKh = c.CtyTen
                        };
                    break;

                case DichVu.KhachHang.Class.KhachHang.Loai.CA_NHAN:
                    gcCaNhan.DataSource = from c in db.tnKhachHangs
                                          join d in db.khNhomKhachHangs on c.MaNKH equals d.ID into tblNhomKh from d in tblNhomKh.DefaultIfEmpty()
                                          join lkh in db.khLoaiKhachHangs on c.MaLoaiKH equals lkh.ID into loaiKh from lkh in loaiKh.DefaultIfEmpty()
                                          where c.IsCaNhan == this.IsCaNhan & c.MaTN == maTN & (c.isNCC == null | c.isNCC != true) & c.IsCoHoi == true
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
                                              c.QuocTich,
                                              c.MaSoThue,
                                              c.tnNhanVien.HoTenNV,
                                              TenNKH = d.TenNKH,
                                              lkh.TenLoaiKH,
                                              c.NguoiDongSoHuu,

                                              LoaiCoHoiName = c.LoaiTiemNangName,
                                              NguonDenName = c.NguonDenName,
                                              NhanVienPhuTrachName = c.NhanVienPhuTrachName,
                                              HoTenKh = c.HoKH+" "+c.TenKH
                                          };
                    break;

                case DichVu.KhachHang.Class.KhachHang.Loai.CTY:
                    gcDoanhNghiep.DataSource = from c in db.tnKhachHangs
                                               join d in db.khNhomKhachHangs on c.MaNKH equals d.ID
                                               into tblNhomKh
                                               from d in tblNhomKh.DefaultIfEmpty()
                                               join lkh in db.khLoaiKhachHangs on c.MaLoaiKH equals lkh.ID into loaiKH
                                               from lkh in loaiKH.DefaultIfEmpty()
                                               where c.IsCaNhan == this.IsCaNhan & c.MaTN == maTN & (c.isNCC == null | c.isNCC != true) & c.IsCoHoi == true
                                               select new
                                               {
                                                   c.KyHieu,
                                                   c.MaKH,
                                                   c.CtyTenVT,
                                                   c.CtyTen,
                                                   c.NguoiLH,
                                                   c.SDTNguoiLH,
                                                   c.CtyNoiDKKD,
                                                   CtyDiaChi=c.DCLL,
                                                   CtyDienThoai=c.DienThoaiKH,
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
                                                   HoTenKh = c.CtyTen
                                               };
                    break;
            }
        }

        private void LoadChiTiet()
        {
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    LoadLichSuLienLac(0);
                    break;
                //case 1:
                //    LoadTheThangMay(0);
                //    break;
                //case 2:
                //    LoadDichVuKhac(0);
                //    break;
                //case 3:
                //    LoadNhanKhau(0);
                //    break;
                //case 4:
                //    LoadLichSuCapNhat(0);
                //    break;
                //case 5:
                //    LoadLichSuGiaoDich(0);
                //    break;

                //case 7:
                //    LoadBieuMau(0);
                //    break;
                case 1:
                    ctlTaiLieu2.FormID = 19;
                    ctlTaiLieu2.LinkID = 0;
                    ctlTaiLieu2.MaNV = Library.Common.User.MaNV;
                    ctlTaiLieu2.objNV = Library.Common.User;
                    ctlTaiLieu2.TaiLieu_Load();
                    break;
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

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

            indexs = this.IsNhaCungCap ? grvNhaCungCap.GetSelectedRows() : this.IsCaNhan ? grv.GetSelectedRows() : grvDoanhNghiep.GetSelectedRows();


            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            foreach (int i in indexs)
            {
                int MaKH = 0;
                MaKH = this.IsNhaCungCap ? (int) grvNhaCungCap.GetRowCellValue(i, "MaKH") : this.IsCaNhan ? (int) grv.GetRowCellValue(i, "MaKH") : (int) grvDoanhNghiep.GetRowCellValue(i, "MaKH");

                db.NhiemVus.DeleteAllOnSubmit(db.NhiemVus.Where(_ => _.MaKH == MaKH));
                db.dvHoaDons.DeleteAllOnSubmit(db.dvHoaDons.Where(_ => _.MaKH == MaKH));
                db.tnKhachHangs.DeleteOnSubmit(db.tnKhachHangs.Single(p => p.MaKH == MaKH));
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
            if (grv.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            DichVu.KhachHang.CoHoi.FrmEdit frm = new DichVu.KhachHang.CoHoi.FrmEdit() { maTN = (byte)itemToaNha.EditValue, objnv = Library.Common.User, objKH = db.tnKhachHangs.Single(p => p.MaKH == (int)grv.GetFocusedRowCellValue("MaKH")), IsCaNhan = this.IsCaNhan };
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                this.IsNhaCungCap = frm.IsNCC;
                if (!this.IsNhaCungCap)
                    this.IsCaNhan = frm.IsCaNhan;
                LoadData();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (tabKhachHang.SelectedTabPageIndex)
            {
                case 2:
                    this.Edit(grvNhaCungCap);
                    break;
                default:
                    this.Edit(this.IsCaNhan ? grvCaNhan : grvDoanhNghiep);
                    break;
            }
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DichVu.KhachHang.CoHoi.FrmEdit frm = new DichVu.KhachHang.CoHoi.FrmEdit() {maTN = (byte) itemToaNha.EditValue, objnv = Library.Common.User, IsNCC = IsNhaCungCap};
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                IsCaNhan = frm.IsCaNhan;
                LoadData();
            }
        }

        private void tabKhachHang_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (tabKhachHang.SelectedTabPageIndex)
            {
                //LoadData();
                case 2:
                    this.IsNhaCungCap = true;
                    break;
                default:
                    this.IsNhaCungCap = false;
                    break;
            }

            LoadData();
        }
        #endregion

        #region Load Detail Data

        private void LoadDetail()
        {
            //var wait = DialogBox.WaitingForm();
            try
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        LoadLichSuLienLac(MaKH);
                        break;
                    case 1:
                        ctlTaiLieu2.FormID = 19;
                        ctlTaiLieu2.LinkID = MaKH;
                        ctlTaiLieu2.MaNV = Library.Common.User.MaNV;
                        ctlTaiLieu2.objNV = Library.Common.User;
                        ctlTaiLieu2.TaiLieu_Load();
                        break;
                    case 2:
                        LoadLichHen(MaKH);
                        break;
                }
            }
            catch { }
        }

        private void LoadLichSuLienLac(int maKh)
        {
            using (var db = new Library.MasterDataContext())
            {
                gcLichSuLienLac.DataSource = db.ch_KhachHang_NhatKies.Where(_ => _.KhachHangId == maKh);
            }
        }

        private void LoadLichHen(int maKh)
        {
            using (var db = new Library.MasterDataContext())
            {
                gcScheduler.DataSource = (from lh in db.LichHens
                        join cd in db.LichHen_ChuDes on lh.MaCD equals cd.MaCD into chuDe
                        from cd in chuDe.DefaultIfEmpty()
                        join td in db.LichHen_ThoiDiems on lh.MaTD equals td.MaTD into thoiDiem
                        from td in thoiDiem.DefaultIfEmpty()
                        join kh in db.tnKhachHangs on lh.MaKH equals kh.MaKH into khachHang
                        from kh in khachHang.DefaultIfEmpty()
                        join nv in db.tnNhanViens on lh.MaNV equals nv.MaNV into nhanVien
                        from nv in nhanVien.DefaultIfEmpty()
                        where lh.MaKH == maKh
                        select new
                        {
                            lh.MaLH,
                            lh.TieuDe,
                            lh.DienGiai,
                            nv.HoTenNV,
                            lh.NgayBD,
                            lh.NgayKT,
                            LabelID = cd.STT,
                            StatusID = td.STT,
                            HoTenKH = kh.HoKH,
                            lh.DiaDiem,
                            NguoiLienQuan = String.Join(", ",
                                db.LichHen_tnNhanViens.Where(o => o.MaLH == lh.MaLH).Select(o => o.tnNhanVien.HoTenNV)
                                    .ToArray()),
                        })
                    .AsEnumerable()
                    .Select((lh, index) => new
                    {
                        STT = index + 1,
                        lh.MaLH,
                        lh.TieuDe,
                        lh.HoTenNV,
                        lh.DienGiai,
                        lh.NgayBD,
                        lh.NgayKT,
                        lh.LabelID,
                        lh.StatusID,
                        lh.HoTenKH,
                        lh.DiaDiem,
                        lh.NguoiLienQuan,
                    })
                    .ToList();
            }
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
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import khách hàng cá nhân", "Import", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            using (KyThuat.KhachHang.Import.frmImport frm = new KyThuat.KhachHang.Import.frmImport() { objnhanvien = Library.Common.User })
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
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Import khách hàng doanh nghiệp", "Import", "Dự án: " + lookUpToaNha.GetDisplayText(itemToaNha.EditValue));
            using (KyThuat.KhachHang.Import.frmImportDoanhNghiep frm = new KyThuat.KhachHang.Import.frmImportDoanhNghiep() { objnhanvien = Library.Common.User })
            {
                frm.MaTN = (byte)maTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadData();
            }
        }

        private void exportCaNhan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcCaNhan);
        }

        private void exportDoanhNghiep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcDoanhNghiep);
        }

        private void btnAddTM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //using (DichVu.ThangMay.frmEdit frm = new DichVu.ThangMay.frmEdit() { objnhanvien = objnhanvien })
            //using (var frm = new DichVu.Khac.frmEdit())
            //{
            //    try
            //    {
                    
            //    }
            //    catch
            //    {
            //    }
            //    var mb = db.mbMatBangs.Where(p => p.MaKH == MaKH).FirstOrDefault();
            //    //if(mb==null)return;
            //    frm.MaTN =(byte?) itemToaNha.EditValue;
            //    frm.MaLDV = 26;
            //    frm.MaMB = mb!=null ? mb.MaMB : 0;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //        LoadData();
            //}
        }

        private void itemUpdatePerson_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maTN = itemToaNha.EditValue != null ? Convert.ToInt32(itemToaNha.EditValue) : 0;
            if (maTN == 0)
            {
                DialogBox.Alert("Vui lòng chọn [Dự án], xin cảm ơn.");
                return;
            }

            using (KyThuat.KhachHang.Import.frmImport frm = new KyThuat.KhachHang.Import.frmImport() { objnhanvien = Library.Common.User })
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

            using (KyThuat.KhachHang.Import.frmImportDoanhNghiep frm = new KyThuat.KhachHang.Import.frmImportDoanhNghiep() { objnhanvien = Library.Common.User })
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
            //using (var frm = new EmailAmazon.frmSendMail())
            //{
            //    frm.MaTN = (byte?)itemToaNha.EditValue;
            //    frm.ShowDialog();
            //}
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

        private void itemThemLichSuLienLac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditLichSu(null);
        }

        private void itemSuaLichSuLienLac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvLichSuLienLac.FocusedRowHandle < 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch sử.");
                return;
            }

            EditLichSu((int?) gvLichSuLienLac.GetFocusedRowCellValue("Id"));
        }

        private void itemXoaLichSuLienLac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvLichSuLienLac.FocusedRowHandle < 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch sử.");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            db = new Library.MasterDataContext();
            var ls = db.ch_KhachHang_NhatKies.FirstOrDefault(_=>_.Id == (int?) gvLichSuLienLac.GetFocusedRowCellValue("Id"));
            if (ls != null)
            {
                db.ch_KhachHang_NhatKies.DeleteOnSubmit(ls);
                MaKH = (int)ls.KhachHangId;
                db.SubmitChanges();
                LoadDetail();
            }
        }

        private void ItemThemLichSu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditLichSu(null);
        }

        private void EditLichSu(int? lichSuId)
        {
            switch (tabKhachHang.SelectedTabPageIndex)
            {
                case 2:
                    this.ThemLichSu(grvNhaCungCap, lichSuId);
                    break;
                default:
                    this.ThemLichSu(this.IsCaNhan ? grvCaNhan : grvDoanhNghiep, lichSuId);
                    break;
            }
        }

        private void ThemLichSu(DevExpress.XtraGrid.Views.Grid.GridView gv, int? lichSuId)
        {
            using (var frm = new DichVu.KhachHang.CoHoi.FrmLichSu() {BuildingId = (byte) itemToaNha.EditValue, KhachHangId = GetKhachHangId(gv), NhatKyId = lichSuId})
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK & frm.KhachHangId != null)
                {
                    MaKH = (int) frm.KhachHangId;
                    LoadDetail();
                }
            }
        }

        private int? GetKhachHangId(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            if (gv.FocusedRowHandle < 0) return null;
            return db.tnKhachHangs.First(_ => _.MaKH == (int)gv.GetFocusedRowCellValue("MaKH")).MaKH;
        }

        private string GetHoTenKh(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            return gv.GetFocusedRowCellValue("HoTenKh").ToString();
        }

        private void ItemThemLichHen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gv;
            switch (tabKhachHang.SelectedTabPageIndex)
            {
                case 2: gv = grvNhaCungCap; break;
                default: gv = IsCaNhan ? grvCaNhan : grvDoanhNghiep; break;
            }

            int khachHangId = GetKhachHangId(gv)!=null?(int)GetKhachHangId(gv):0;
            Building.WorkSchedule.LichHen.AddNew_frm frm = new Building.WorkSchedule.LichHen.AddNew_frm(0, 0, khachHangId, 0, 0, GetHoTenKh(gv));
            frm.objNV = Common.User;
            frm.ShowDialog();
            if (frm.IsUpdate)
            {
                MaKH = khachHangId;
                LoadDetail();
            }
        }

        private void ItemThemNhiemVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Building.WorkSchedule.NhiemVu.AddNew_frm frm = new Building.WorkSchedule.NhiemVu.AddNew_frm();
            frm.ShowDialog();
        }

        private void ItemThoiDiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Building.WorkSchedule.LichHen.ThoiDiem_frm frm = new Building.WorkSchedule.LichHen.ThoiDiem_frm();
            frm.ShowDialog();
        }

        private void ItemXoaLichHen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvScheduler.FocusedRowHandle < 0)
            {
                Library.DialogBox.Alert("Vui lòng chọn lịch hẹn.");
                return;
            }

            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;

            db = new Library.MasterDataContext();
            var ls = db.LichHens.FirstOrDefault(_ => _.MaLH == (int?)gvScheduler.GetFocusedRowCellValue("MaLH"));
            if (ls != null)
            {
                db.LichHen_XuLies.DeleteAllOnSubmit(db.LichHen_XuLies.Where(_ => _.MaLH == ls.MaLH));
                db.LichHen_tnNhanViens.DeleteAllOnSubmit(db.LichHen_tnNhanViens.Where(_ => _.MaLH == ls.MaLH));
                db.LichHens.DeleteOnSubmit(ls);
                if (ls.MaKH != null) MaKH = (int) ls.MaKH;
                db.SubmitChanges();
                LoadDetail();
            }
        }

        private void ItemSuaLichHen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView gv;
            switch (tabKhachHang.SelectedTabPageIndex)
            {
                case 2:
                    gv = grvNhaCungCap;
                    break;
                default:
                    gv = IsCaNhan ? grvCaNhan : grvDoanhNghiep;
                    break;
            }

            int khachHangId = GetKhachHangId(gv) != null ? (int) GetKhachHangId(gv) : 0;
            Building.WorkSchedule.LichHen.AddNew_frm frm = new Building.WorkSchedule.LichHen.AddNew_frm((int?) gvScheduler.GetFocusedRowCellValue("MaLH"), 0, khachHangId, 0, 0, GetHoTenKh(gv));
            frm.objNV = Common.User;
            frm.ShowDialog();

            if (frm.IsUpdate)
            {
                MaKH = khachHangId;
                LoadDetail();
            }
        }
    }

    class ImportItem
    {
        public string KyHieu { get; set; }
        public string MaPhu { get; set; }
    }
}