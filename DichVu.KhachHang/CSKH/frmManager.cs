using DevExpress.Data;
using DevExpress.Data.Linq;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DIP.SwitchBoard;
//using DIPCRM.Customer.Contact;
//using DIPCRM.Customer.Reports;
using KyThuat.KhachHang;
using Library;
//using Library.Other;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace DichVu.KhachHang.CSKH
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        int SDBID = 6;

        string FileName = "";

        public bool IsViewChinhThuc = false;

        MasterDataContext db = new MasterDataContext();
        
        List<ncNhuCau> ListReminder;

        int SttTask = 0;
        bool IsStopThread = false;
        List<BackgroundWorker> ltThread = new List<BackgroundWorker>();
        public object thisLock = new object();

        List<cKhachHangss> ltKhachHang;
        

        class cKhachHangss
        {
            public int? MaKH { get; set; }
            public int? MaNV { get; set; }
            public string TenVietTat { get; set; }
            public string TenCongTy { get; set; }
            public string DiaChi { get; set; }
            public string idNgheNghiep { get; set; }
            public bool? IsChinhThuc { get; set; }
            public string MaSoThue { get; set; }
            public DateTime? NgayCapMST { get; set; }
            public string Email { get; set; }
            public string NoiCapMST { get; set; }
            public string SoTKNH { get; set; }
            public string DienThoai { get; set; }
            public string TenNH { get; set; }
            public string Website { get; set; }
            public decimal? NoToiDa { get; set; }
            public string GhiChu { get; set; }
            public string Fax { get; set; }
            public decimal? VonDieuLe { get; set; }
            public string SoGPKD { get; set; }
            public string NgayDKKD { get; set; }
            public string DiDong { get; set; }
            public string SoCMND { get; set; }
            public DateTime? NgaySinh { get; set; }
            public string NoiCap { get; set; }
            public string HoChieu { get; set; }
            public string NgayCap { get; set; }
            public DateTime? XuLy_NgayXuLy { get; set; }
            public decimal? XuLy_SoLuong { get; set; }
            public string XuLy_DienGiai { get; set; }

        }

        class cNhomKhachHang
        {
            public int? MaNKH { get; set; }
            public string TenNKH { get; set; }
        }

        class cLoaiKhachHang
        {
            public int? MaLKH { get; set; }
            public string TenLKH { get; set; }
            public string PhanLoai { get; set; }
        }

        class cNguoiLienHe
        {
            public int? MaNLH { get; set; }
            public string TenNLH { get; set; }
            public string DiDongNLH { get; set; }
        }

        class cNhanVien
        {
            public int? MaNV { get; set; }
            public string TenNLH { get; set; }
            public string DiDongNLH { get; set; }
        }


        private void Permisstion()
        {
        }

        private void KhachHangEdit()
        {
            int? iD = (int?)this.grvKH.GetFocusedRowCellValue("MaKH");
            if (!iD.HasValue)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
            }
            else
            {
                using (frmEdit frm = new frmEdit())
                {
                    frm.MaKH = iD;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        this.LoadData();
                    }
                }
            }
        }

        public frmManager(bool isViewChinhThuc)
        {
            this.InitializeComponent();

            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            //ctlNhiemVu1.frm = this;

            ctlContact1.frm = this;

            //ctlMailHistory1.frm = this;

            //ctlContractSale1.frm = this;

            ctlLichHen1.frm = this;

            ctlNhuCau1.frm = this;

            this.IsViewChinhThuc = isViewChinhThuc;

            for (int i = 0; i <= 13; i++)
            {
                var thread = new BackgroundWorker();
                thread.DoWork += thread_DoWork;
                thread.RunWorkerCompleted += thread_RunWorkerCompleted;
                ltThread.Add(thread);
            }

        }

        void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (IsStopThread)
            {
                if (!ltThread.Any(o => o.IsBusy))
                    GetData();
            }
            else
            {
                (sender as BackgroundWorker).RunWorkerAsync();
            }

        }

        void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            GetTask();
        }

        void GetTask()
        {
            DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            using (var dbo = new MasterDataContext())
            {
                dbo.CommandTimeout = 1000000000;

                int index = 0;

                lock (thisLock)
                    index = SttTask++;

                switch (SttTask)
                {

                    case 1:
                        ltKhachHang = (from kh in dbo.tnKhachHangs
                                       where (this.IsViewChinhThuc & (kh.IsChinhThuc.GetValueOrDefault() | kh.IsCSKH.GetValueOrDefault()))
                                            | (!this.IsViewChinhThuc & (kh.IsCSKH.GetValueOrDefault() | !kh.IsRoot.GetValueOrDefault()))
                                       select new cKhachHangss
                                       {
                                           MaKH = kh.MaKH,
                                           MaNV = kh.MaNV,
                                           TenVietTat = kh.KyHieu,
                                           TenCongTy = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                           DiaChi = kh.DiaChi,
                                           idNgheNghiep = kh.idNgheNghiep,
                                           IsChinhThuc = kh.IsChinhThuc,
                                           MaSoThue = kh.MaSoThue,
                                           NgayCapMST = kh.MaSoThue_NgayCap,
                                           Email = kh.Email,
                                           NoiCapMST = kh.MaSoThue_NoiCap,
                                           //SoTKNH = kh.SoTKNH,
                                           DienThoai = kh.DienThoai,
                                           //TenNH = kh.TenNH,
                                           Website = kh.Website,
                                           //NoToiDa = kh.NoToiDa,
                                           GhiChu = kh.GhiChu,

                                           Fax = kh.Fax,
                                           VonDieuLe = kh.VonDieuLe,
                                           SoGPKD = kh.CtySoDKKD,
                                           NgayDKKD = kh.CtyNgayDKKD,

                                           DiDong = kh.DiDong,
                                           SoCMND = kh.CMND,
                                           NgaySinh = kh.NgaySinh,
                                           NoiCap = kh.NoiCap,
                                           HoChieu = kh.CMND,
                                           NgayCap = kh.NgayCap,

                                           XuLy_NgayXuLy = kh.XuLy_NgayXuLy,
                                           XuLy_SoLuong = kh.XuLy_SoLuong,
                                           XuLy_DienGiai = kh.XuLy_DienGiai,
                                       }).ToList();
                        break;

                    case 2:

                        break;

                    case 3:

                        break;

                    case 4:
                        break;

                    case 5:

                        break;

                    case 6:

                        break;

                    case 7:

                        break;

                    case 8:

                        break;

                    case 9:

                        break;

                    case 10:

                        break;

                    case 11:

                        break;

                    case 12:

                        break;

                    case 13:

                        break;

                    case 14:

                        break;

                    default:
                        IsStopThread = true;
                        break;

                }
            }
        }

        void GetData()
        {
 
        }

        private void LoadData()
        {
            //DevExpress.Data.Helpers.ServerModeCore.DefaultForceCaseInsensitiveForAnySource = true;
            string text = (this.itemCongTy.EditValue ?? "").ToString().Replace(" ", "");
            string[] array = text.Split(new char[] { ',' });
            //Stopwatch sw = Stopwatch.StartNew();
            this.gcCustomers.DataSource =
                 (from kh in this.db.tnKhachHangs

                  join nkh in this.db.khNhomKhachHangs on kh.MaNKH equals nkh.ID into nhomKH
                  from nkh in nhomKH.DefaultIfEmpty()

                  join lkh in db.khLoaiKhachHangs on kh.MaLoaiKH equals lkh.ID into loaikh
                  from lkh in loaikh.DefaultIfEmpty()

                  join lh in this.db.NguoiLienHes on kh.MaNLH equals (int?)lh.ID into lienHe
                  from lh in lienHe.DefaultIfEmpty()

                  join nv in this.db.tnNhanViens on kh.MaNV equals (int?)nv.MaNV into nhanvien
                  from nv in nhanvien.DefaultIfEmpty()

                  //join nvql in this.db.tnNhanViens on kh.MaNVQL equals (int?)nvql.MaNV into nvql_kh
                  //from nvql in nvql_kh.DefaultIfEmpty()

                  join x in this.db.Xas on kh.MaXa equals (int?)x.MaXa into dsxa
                  from x in dsxa.DefaultIfEmpty()

                  join h in this.db.Huyens on kh.MaHuyen equals (int?)h.MaHuyen into dshuyen
                  from h in dshuyen.DefaultIfEmpty()

                  join t in this.db.Tinhs on kh.MaTinh equals (int?)t.MaTinh into dstinh
                  from t in dstinh.DefaultIfEmpty()

                  join lhkd in this.db.LoaiHinhKinhDoanhs on kh.MaLHKD equals lhkd.ID into dskd
                  from lhkd in dskd.DefaultIfEmpty()

                  join ngkh in this.db.ncNguonKHs on kh.MaNguon equals ngkh.MaNguon into dsngkh
                  from ngkh in dsngkh.DefaultIfEmpty()

                  join qm in this.db.QuyMoCongTies on kh.MaQM equals qm.ID into dsqm
                  from qm in dsqm.DefaultIfEmpty()

                  join nc in db.ncNhuCaus on kh.MaNC equals nc.MaNC into nhucau
                  from nc in nhucau.DefaultIfEmpty()

                  join tt in db.ncTrangThais on nc.MaTT equals tt.MaTT into trangthai
                  from tt in trangthai.DefaultIfEmpty()

                  join nct in db.NhuCauThues on kh.XuLy_idNhuCauThue equals nct.ID into nhucauthue
                  from nct in nhucauthue.DefaultIfEmpty()

                  join nvxl in db.tnNhanViens on kh.XuLy_MaNV equals nvxl.MaNV into nvxuly
                  from nvxl in nvxuly.DefaultIfEmpty()

                  where (this.IsViewChinhThuc & (kh.IsChinhThuc.GetValueOrDefault() | kh.IsCSKH.GetValueOrDefault()))
                    | (!this.IsViewChinhThuc & (kh.IsCSKH.GetValueOrDefault() | !kh.IsRoot.GetValueOrDefault()))
                  select new
                  {
                      MaKH = kh.MaKH,
                      MaNV = kh.MaNV,
                      TenVietTat = kh.KyHieu,
                      //NhanVienQuanLy = nvql.HoTenNV,
                      TenCongTy = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                      DiaChi = kh.DiaChi,
                      PhanLoai = lkh.TenLoaiKH,
                      TenLKH = lkh.TenLoaiKH,
                      TenNKH = nkh.TenNKH,
                      kh.idNgheNghiep,
                      IsChinhThuc = kh.IsChinhThuc,
                      TenLoaiHinhKD = lhkd.TenLoaiHinhKD,
                      MaSoThue = kh.MaSoThue,
                      TenNguon = ngkh.TenNguon,
                      NgayCapMST = kh.MaSoThue_NgayCap,
                      Email = kh.Email,
                      NoiCapMST = kh.MaSoThue_NoiCap,
                      //SoTKNH = kh.SoTKNH,
                      DienThoai = kh.DienThoai,
                      //TenNH = kh.TenNH,
                      Website = kh.Website,
                      //NoToiDa = kh.NoToiDa,
                      GhiChu = kh.GhiChu,

                      Fax = kh.Fax,
                      TenQM = qm.TenQM,
                      VonDieuLe = kh.VonDieuLe,
                      SoGPKD = kh.CtySoDKKD,
                      NgayDKKD = kh.CtyNgayDKKD,

                      DiDong = kh.DiDong,
                      SoCMND = kh.CMND,
                      NgaySinh = kh.NgaySinh,
                      NoiCap = kh.NoiCap,
                      HoChieu = kh.CMND,
                      NgayCap = kh.NgayCap,

                      TenNLH = lh.HoTen,
                      DiDongNLH = lh.DiDong,
                      TenTinh = t.TenTinh,
                      TenHuyen = h.TenHuyen,
                      TenXa = x.TenXa,

                      TrangThaiXL = tt.TenTT,
                      kh.XuLy_NgayXuLy,
                      nct.TenNhuCau,
                      kh.XuLy_SoLuong,
                      NVXL = nvxl.HoTenNV,
                      kh.XuLy_DienGiai,
                      NoiDung = string.Join("; ", db.ncNhuCaus.Where(o => o.MaKH == kh.MaKH).Select(o => string.Format("{0} - {1}",o.NgayNhap,o.DienGiai)).ToArray()),
                  }).ToList();
            //(from kh in db.tnKhachHangs
            // where (this.IsViewChinhThuc & (kh.IsChinhThuc.GetValueOrDefault() | kh.IsCSKH.GetValueOrDefault()))
            //      | (!this.IsViewChinhThuc & (kh.IsCSKH.GetValueOrDefault() | !kh.IsRoot.GetValueOrDefault()))
            // select new cKhachHangss
            // {
            //     MaKH = kh.MaKH,
            //     MaNV = kh.MaNV,
            //     TenVietTat = kh.KyHieu,
            //     TenCongTy = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
            //     DiaChi = kh.DiaChi,
            //     idNgheNghiep = kh.idNgheNghiep,
            //     IsChinhThuc = kh.IsChinhThuc,
            //     MaSoThue = kh.MaSoThue,
            //     NgayCapMST = kh.MaSoThue_NgayCap,
            //     Email = kh.Email,
            //     NoiCapMST = kh.MaSoThue_NoiCap,
            //     SoTKNH = kh.SoTKNH,
            //     DienThoai = kh.DienThoai,
            //     TenNH = kh.TenNH,
            //     Website = kh.Website,
            //     NoToiDa = kh.NoToiDa,
            //     GhiChu = kh.GhiChu,

            //     Fax = kh.Fax,
            //     VonDieuLe = kh.VonDieuLe,
            //     SoGPKD = kh.CtySoDKKD,
            //     NgayDKKD = kh.CtyNgayDKKD,

            //     DiDong = kh.DiDong,
            //     SoCMND = kh.CMND,
            //     NgaySinh = kh.NgaySinh,
            //     NoiCap = kh.NoiCap,
            //     HoChieu = kh.CMND,
            //     NgayCap = kh.NgayCap,

            //     XuLy_NgayXuLy = kh.XuLy_NgayXuLy,
            //     XuLy_SoLuong = kh.XuLy_SoLuong,
            //     XuLy_DienGiai = kh.XuLy_DienGiai
            // }).ToList();
            //TimeSpan elapsed = sw.Elapsed;
            //DialogBox.Alert(elapsed.ToString());
        }

        public void Find(string name, string maSoThue, string nganhNghe, string diaChi, string nguoiDaiDien, string dienThoai, int maTinh, int x, int y)
        {
            this.gcCustomers.DataSource = null;
        }

        private void LoadGeneral()
        {
            try
            {
                int? num = (int?)this.grvKH.GetFocusedRowCellValue("MaKH");
                if (!num.HasValue)
                {
                    switch (this.xtraTabControl1.SelectedTabPageIndex)
                    {
                        case 1:
                            this.ctlContact1.MaKH = null;
                            this.ctlContact1.LienHeLoad();
                            break;
                        //case 3:
                        //    this.ctlMailHistory1.FormID = null;
                        //    this.ctlMailHistory1.LinkID = null;
                        //    this.ctlMailHistory1.MaKH = null;
                        //    this.ctlMailHistory1.MailHistory_Load();
                        //    break;
                        case 4:
                            ctlLichHen1.LoadData(0,0);
                            break;
                        //case 7:
                        //    this.ctlContractSale1.MaKH = null;
                        //    this.ctlContractSale1.HopDongLoad();
                        //    break;
                    }
                }
                else
                {
                    switch (this.xtraTabControl1.SelectedTabPageIndex)
                    {
                        case 0:
                            {
                                this.lblTenCongTy.Text = string.Format("Khách hàng: <b>{0}</b>", this.grvKH.GetFocusedRowCellValue("TenCongTy"));
                                this.lblDiDong.Text = string.Format("Di động: <b>{0}</b>", this.grvKH.GetFocusedRowCellValue("DiDong"));
                                this.lblDienThoai.Text = string.Format("Điện thoại: <b>{0}</b>", this.grvKH.GetFocusedRowCellValue("DienThoaiCT"));
                                this.lblEmail.Text = string.Format("Email: <b>{0}</b>", this.grvKH.GetFocusedRowCellValue("Email"));
                                this.lblMaSoThue.Text = string.Format("Mã số thuế: <b>{0}</b>", this.grvKH.GetFocusedRowCellValue("MaSoThueCT"));
                                this.lblNguoiDaiDien.Text = string.Format("Người đại diện: <b>{0}</b>", this.grvKH.GetFocusedRowCellValue("TenNLH"));
                                this.lblWebsite.Text = string.Format("Website: <b>{0}</b>", this.grvKH.GetFocusedRowCellValue("Website"));
                                DateTime? dateTime = (DateTime?)this.grvKH.GetFocusedRowCellValue("NgayCN");
                                if (dateTime.HasValue)
                                {
                                    this.lblNgayCN.Text = string.Format("Ngày cập nhật: <b>{0:dd/MM/yyyy}</b>", dateTime);
                                }
                                else
                                {
                                    this.lblNgayCN.Text = string.Format("Ngày cập nhật: ", new object[0]);
                                }
                                break;
                            }
                        case 1:
                            this.ctlContact1.MaKH = num;
                            this.ctlContact1.LienHeLoad();
                            break;
                        case 2:
                            this.ctlNhuCau1.MaKH = num;
                            this.ctlNhuCau1.LoadData();
                            break;
                        //case 3:
                        //    this.ctlMailHistory1.FormID = new int?(9);
                        //    this.ctlMailHistory1.LinkID = num;
                        //    this.ctlMailHistory1.MaKH = num;
                        //    this.ctlMailHistory1.MailHistory_Load();
                        //    break;
                        case 4:
                            ctlLichHen1.LoadData(0,num);
                            break;
                        //case 5:
                        //    this.ctlNhiemVu1.MaKH = num;
                        //    this.ctlNhiemVu1.LoadData();
                        //    break;
                        //case 7:
                        //    this.ctlContractSale1.MaKH = num;
                        //    this.ctlContractSale1.HopDongLoad();
                        //    break;
                    }
                }
            }
            catch
            {
            }
        }

        private void ExportList()
        {
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            try
            {
                this.Permisstion();
                itemChange.Visibility = this.IsViewChinhThuc ? BarItemVisibility.Never : BarItemVisibility.Always;
                this.lookUpTinh.DataSource = this.db.Tinhs;
                this.lkHuyenEdit.DataSource = this.db.Huyens;
                this.lkXaEdit.DataSource = this.db.Xas;
                this.lkNhanVien.DataSource = (from p in this.db.tnNhanViens
                                              select new
                                              {
                                                  MaNV = p.MaNV,
                                                  HoTen = p.HoTenNV
                                              }).ToList();
                this.cmbCongTy.DataSource = (from p in this.db.tnToaNhas
                                             select new
                                             {
                                                 ID = p.MaTN,
                                                 TenCT = p.TenTN
                                             }).ToList();

                cmbNgheNghiep.DataSource = db.NgheNghieps.Select(o => new { o.MaNN, o.TenNN });
                cmbNgheNghiep.DisplayMember = "TenNN";
                cmbNgheNghiep.ValueMember = "MaNN";

                this.LoadData();
            }
            catch
            {
            }
        }

        private void itemRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LoadData();
        }

        private void itemProcess_ItemClick(object sender, ItemClickEventArgs e)
        {
            var maKH = (int?)grvKH.GetFocusedRowCellValue("MaKH");

            if (maKH == null)
            {
                DialogBox.Error("Vui lòng chọn khách hang");
                return;
            }

            if (NhuCauCls.checkHopLe(maKH))
            {
                DialogBox.Error("Khách hàng này còn cơ hội chưa đóng. Không thể thực hiện");
                return;
            }

            using (var frm = new Library.Controls.NhuCau.frmEdit(false))
            {
                frm.MaKH = maKH;
                frm.IsXuLy = true;
                frm.MaTN = Common.User.MaTN;

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var objKH = db.tnKhachHangs.Single(o => o.MaKH == maKH);
                    objKH.XuLy_NgayXuLy = db.GetSystemDate();
                    objKH.XuLy_idkhTrangThaiXuLy = 2;
                    db.SubmitChanges();

                    this.LoadData();
                }
            }
        }

        private void itemExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcCustomers);
        }

        private void itemExportList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "File excel(.xls, .xlsx)|*.xls;*.xlsx";
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    rptCustomer rptCustomer = new rptCustomer();
            //    rptCustomer.ExportToXlsx(saveFileDialog.FileName);
            //    this.FileName = saveFileDialog.FileName;
            //    Thread thread = new Thread(new ThreadStart(this.ExportList));
            //    thread.Start();
            //}
            //if (saveFileDialog.FileName.Trim() != "")
            //{
            //    if (DialogBox.Question("Bạn có muốn mở file này không?") == DialogResult.Yes)
            //    {
            //        Process.Start(saveFileDialog.FileName);
            //    }
            //}
        }

        private void grvKH_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (this.grvKH.FocusedRowHandle >= 0)
            {
                this.LoadGeneral();
            }
        }

        private void grvKH_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void grvKH_DoubleClick(object sender, EventArgs e)
        {
            this.KhachHangEdit();
        }

        private void itemSendMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] selectedRows = this.grvKH.GetSelectedRows();
            try
            {
                if (selectedRows.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn khách hàng, xin cảm ơn.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemSendSMS_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void itemImport_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            this.LoadGeneral();
        }

        private void itemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmEdit frm = new frmEdit())
            {
                frm.IsRoot = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.LoadData();
                }
            }
        }

        private void itemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.KhachHangEdit();
        }

        private void itemDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] indexs = this.grvKH.GetSelectedRows();

            if (indexs.Count() == 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (DialogBox.Question("Bạn có chắc không?") == DialogResult.Yes)
            {
                foreach (var i in indexs)
                {
                    tnKhachHang objKH = db.tnKhachHangs.Single((tnKhachHang p) => p.MaKH == Convert.ToInt32(this.grvKH.GetRowCellValue(i, "MaKH")));

                    if (objKH.IsRoot.GetValueOrDefault())
                    {
                        DialogBox.Error("Khách hàng này thuộc danh sách gốc. Không thể xóa");
                        return;
                    }

                    if (objKH.IsChinhThuc.GetValueOrDefault())
                    {
                        DialogBox.Error("Khách hàng này đang là chính thức. Không thể xóa");
                        return;
                    }



                    db.tnKhachHangs.DeleteOnSubmit(objKH);
                    db.SubmitChanges();
                }

                this.LoadData();
            }
        }

        private void btnCallPhone_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            string text = (sender as ButtonEdit).Text ?? "";
            if (!(text.Trim() == ""))
            {
                try
                {
                    SwitchBoard.SoftPhone.Call(text);
                }
                catch
                {
                }
            }
        }

        private void hlpEmail_Click(object sender, EventArgs e)
        {
        }

        private void grvKH_FocusedRowLoaded(object sender, RowEventArgs e)
        {
            this.LoadGeneral();
        }

        private void btnTestImport_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void itemXem_ItemClick(object sender, ItemClickEventArgs e)
        {
            int? iD = (int?)this.grvKH.GetFocusedRowCellValue("MaKH");
            if (!iD.HasValue)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
            }
            else
            {
                using (frmEdit frm = new frmEdit())
                {
                    frm.MaKH = iD;
                    frm.IsView = true;
                    frm.ShowDialog(this);
                }
            }
        }

        private void itemChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChuyenDoiKH(true);
        }

        private void ItemChuyenDangChamSoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChuyenDoiKH(false);
        }

        void ChuyenDoiKH(bool IsChinhThuc)
        {
            var id = (int?)grvKH.GetFocusedRowCellValue("MaKH");

            var checkCT = (bool?)grvKH.GetFocusedRowCellValue("IsChinhThuc");

            if (id == 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }




            if (IsChinhThuc)
            {
                if (checkCT != null)
                {
                    if ((bool)checkCT)
                    {
                        DialogBox.Alert("Khách hàng đã chính thức");
                        return;
                    }
                }
            }
            else
            {
                var checkHoaDon = db.dvHoaDons.Any(o => o.MaKH == (int?)id);
                var checkHDong = db.dvHoaDons.Any(o => o.MaKH == (int?)id);
                if (checkHoaDon || checkHDong)
                {
                    DialogBox.Alert("Khách hàng đã phát sinh " + (checkHoaDon ? "hóa đơn" : "hợp đồng"));
                    return;
                }
            }


            if (DialogBox.Question("Bạn có muốn chuyển khách hàng này sang " + (IsChinhThuc ? "chính thức" : "đang chăm sóc")) == System.Windows.Forms.DialogResult.No)
                return;



            using (var _db = new MasterDataContext())
            {

                if (IsChinhThuc)
                {
                    using (var frm = new frmEdit())
                    {
                        frm.IsCSKH = true;
                        frm.MaKH = id;

                        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var objKH = _db.tnKhachHangs.Single(p => p.MaKH == id);
                            objKH.IsChinhThuc = IsChinhThuc;
                            _db.SubmitChanges();
                        }
                    }
                }
                else
                {
                    var objKH = _db.tnKhachHangs.Single(p => p.MaKH == id);
                    objKH.IsChinhThuc = IsChinhThuc;
                    _db.SubmitChanges();
                }
            }
        }

        private void itemConvert_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new frmConvert())
            {
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.LoadData();
            }
        }

        private void itemRemoveConvert_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = (int?)grvKH.GetFocusedRowCellValue("MaKH");

            if (id == 0)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            using (var db = new MasterDataContext())
            {
                var objKH = db.tnKhachHangs.Single(p => p.MaKH == id);

                if (!objKH.IsCSKH.GetValueOrDefault())
                {
                    DialogBox.Error("");
                }

                if (!objKH.IsRoot.GetValueOrDefault())
                {
                    DialogBox.Error("Khách hàng này tạo tự CSKH. Không thể thực hiện");
                    return;
                }

                objKH.IsCSKH = false;
                db.SubmitChanges();
                this.LoadData();
            }
        }
    }
}
