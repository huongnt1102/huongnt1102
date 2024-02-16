using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using System.Linq;
using System.Data.Linq.SqlClient;
using DevExpress.XtraEditors;
using System.Threading;
using System.Data;
using DevExpress.XtraBars.Alerter;
using Library;
using DevExpress.XtraReports.UI;
using System.Drawing;
using DichVu.Quy;
using System.ComponentModel;
using EmailAmazon;
using Newtonsoft.Json;
using DIPCRM;

namespace LandSoftBuildingMain
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        MasterDataContext db;
        public bool IsAdmin { get; set; }
        public tnNhanVien User;

        private bool FlagIsPhanQuyen { get; set; }

        private static int NhanVienAdminId = 6;

        public frmMain()
        {
            InitializeComponent();
            db = new MasterDataContext();
            using (DefaultLookAndFeel dlf = new DefaultLookAndFeel())
            {
                dlf.LookAndFeel.SkinName = Library.Properties.Settings.Default.SkinCurrent;
            }
            InitSkinGallery();
            // kiểm tra nhân viên
            if (Library.Common.User != null) Library.HeThongCls.PhanQuyenCls.HidenAllRibbon(itemBoPhanNhanEmail);
            if (User == null)
            {
                //Library.Properties.Settings.Default.NgonNgu = "VI";
                //var nv = GetNhanVienById(NhanVienAdminId);
                //Library.Properties.Settings.Default.Username = nv.MaSoNV;
                //Library.Properties.Settings.Default.Password = nv.MatKhau;
                //User = nv;
                //Library.Common.User = nv;
                //Library.Common.TowerList = GetAllTower(nv);
                //Library.HeThongCls.PhanQuyenCls.KhongPhanQuyen = true;
            }

            // User = null, lấy lại user theo phân quyền
            if (User == null) User = GetNhanVienById(NhanVienAdminId);
            if (User == null) return;
            TranslateLanguage.TranslateControl(this, null, itemBoPhanNhanEmail);

            itemDongHoDien.ItemClick += ItemDongHoDien_ItemClick;
            itemDongHoDienDieuHoa.ItemClick += ItemDongHoDienDieuHoa_ItemClick;
        }

        private void ItemDongHoDienDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.Dien.DieuHoa.frmDongHo(), e.Item.Caption);
        }

        private void ItemDongHoDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.Dien.frmDongHo(), e.Item.Caption);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                var wait = DialogBox.WaitingForm();

                wait.SetCaption("Đang tải màn hình làm việc");
                itemCopyRight.EditValue = Properties.Resources.dip;

                // set cờ phân quyền = false
                FlagIsPhanQuyen = false;

                // kiểm tra và tạo form
                Library.PhanQuyen.CreateForm(GetType().FullName, Text);

                LoadForm(new frmMainIntro() { objnhanvien = User });
                var objTn = db.tnToaNhas.FirstOrDefault(p => p.MaTN == User.MaTN);
                if (objTn != null)
                {
                    //Building_Statistic.ThongKeClass.InsertData(objTn.TenTN, "THÀNH THÀNH CÔNG", User.MaSoNV,
                    //    User.HoTenNV, User.DienThoai, User.Email);
                }

                ////LoadNhacKeHoachBaoTri();
                //LoadYeuCauMoi();

                // Get thông tin version
                try
                {
                    var version = System.IO.File.ReadAllText(Application.StartupPath + "\\version.txt");
                    barStaticItemLogin.Caption = string.Format("Người dùng: {0} - Version: {1}", User.HoTenNV , version);
                }
                catch (Exception em)
                {
                    barStaticItemLogin.Caption = string.Format("Người dùng: {0} - Version: 0.2.2.5", User.HoTenNV);
                }

                //barStaticItemLogin.Caption = string.Format("Người dùng: {0} - Version: 0.2.2.5", User.HoTenNV);

                // test tính năng thông báo cho user
                //Library.Class.ThongBao.ThongBaoNhanVien.ThongBaoParam thongBaoParam = new Library.Class.ThongBao.ThongBaoNhanVien.ThongBaoParam() { UserId = Library.Common.User.MaNV, Description = "Hệ thống mới update cài đặt App, đổi thành link https.", KeyGroup = "Update_change_http_to_https" };
                //Library.Class.ThongBao.ThongBaoNhanVien.ThongBaoReturn thongBao = Library.Class.ThongBao.ThongBaoNhanVien.CheckThongBao(thongBaoParam);
                //if (thongBao.IsView == false) Library.DialogBox.Alert(thongBaoParam.Description);

                wait.SetCaption("Đang kiểm tra quyền sử dụng và nạp dữ liệu người dùng");

                //LoadForm(new frmDesktop() { objnhanvien = User });
                Library.HeThongCls.PhanQuyenCls.PhanQuyenRibon(this, Library.Common.User, itemBoPhanNhanEmail);
                AnNinh.AnNinhCls.CheckAnNinhJobs(User);
                try
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đăng nhập phần mềm", "Đăng nhập");

                    // Ghi lại ftp, để khách hàng có chuyển sang ftp local thì có sẵn
                    //using (var dbo = new Library.MasterDataContext())
                    //{
                    //    var objConfig = dbo.tblConfigs.First();
                    //    objConfig.FtpPass = it.EncDec.Decrypt(objConfig.FtpPass);
                    //    string json = JsonConvert.SerializeObject(objConfig);
                    //    System.IO.File.WriteAllText(@"FtpConnect", json);
                    //}
                }
                catch
                {
                }

                wait.Close();
                wait.Dispose();

                timerReminder.Start();
                timerCapNhat.Start();
            }
            catch (System.Exception ex)
            {
                //DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void InitSkinGallery()
        {
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(itemSkins, true);
        }

        private Library.tnNhanVien GetNhanVienById(int? userId)
        {

            try
            {
                TestConnect(global::Library.Properties.Settings.Default.Building_dbConnectionString);

                Library.MasterDataContext db = new MasterDataContext();
                var nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.MaNV == userId);
                if (nhanVien != null)
                {
                    // Lấy mặc định tòa nhà của nhân viên theo phân quyền
                    nhanVien.MaTN = Library.Common.TowerList.FirstOrDefault().MaTN; 
                    return nhanVien;
                }
                else nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.IsSuperAdmin == true); // nhân viên admin đầu tiên để phân quyền
                return nhanVien;
            }
            catch
            {
                Library.DialogBox.Error("Không thể kết nối database.");
                return null;
            }
        }



        public bool TestConnect(string Connection)
        {
            System.Data.SqlClient.SqlConnection SqlConn = new System.Data.SqlClient.SqlConnection(Connection);
            try
            {
                SqlConn.Open();
                SqlConn.Close();
                return true;
            }
            catch
            {

                string text = System.IO.File.ReadAllText(@"Data.txt");

                var sqlConnBuild = new System.Data.SqlClient.SqlConnectionStringBuilder();
                sqlConnBuild.ConnectionString = text.Trim();

                if (sqlTestConnect(sqlConnBuild.ConnectionString))
                {
                    var sqlInfo = new Library.SqlSetting();
                    sqlInfo.SqlConn = sqlConnBuild.ConnectionString;
                    sqlInfo.Conn = it.EncDec.Encrypt(sqlConnBuild.ConnectionString);
                    sqlInfo.Server = sqlConnBuild.DataSource;
                    sqlInfo.UserID = sqlConnBuild.UserID;
                    sqlInfo.Database = sqlConnBuild.InitialCatalog;
                    sqlInfo.Save();
                }
                else
                {
                    DialogBox.Success("Kết nối không thành công. Vui lòng kiểm tra lại, xin cảm ơn.");
                }

                SqlConn.Close();

                global::Library.Properties.Settings.Default.Save();
                return false;
            }
        }
        public bool sqlTestConnect(string Conn)
        {
            System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(Conn);
            try
            {
                sqlConn.Open();
                sqlConn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //private delegate void dlgAddItemN();
        private void LoadThongBao()
        {
            if (gcthongbao.DataSource != null) gcthongbao.DataSource = null;
            try
            {
                DateTime now = db.GetSystemDate();
                var objthongbao = db.tnThongBaos
                    .Where(p => p.IsEnable.Value
                        & SqlMethods.DateDiffDay(now, p.DenNgay.Value) >= 0
                        & SqlMethods.DateDiffDay(p.TuNgay.Value, now) >= 0)
                        .OrderByDescending(p => p.NgayDang)
                        .Select(p => new
                        {
                            p.MaThongBao,
                            NoiDung = string.Format("{0}\r\n{8}\r\n{1}/{2}/{3}-{4}/{5}/{6}\t\r\n{7}", p.TieuDe, p.TuNgay.Value.Day, p.TuNgay.Value.Month, p.TuNgay.Value.Year,
                            p.DenNgay.Value.Day, p.DenNgay.Value.Month, p.DenNgay.Value.Year, p.NoiDung, p.tnNhanVien.HoTenNV)
                        }).ToList();
                gcthongbao.DataSource = objthongbao;

                if (objthongbao.Count() > (User.SLThongBao ?? 0))
                {
                    dockPanelThongBao.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                    User = db.tnNhanViens.Single(p => p.MaNV == User.MaNV);
                    User.SLThongBao = objthongbao.Count();
                    db.SubmitChanges();
                }
            }
            catch
            {
            }
        }

        private void LoadNhacViec()
        {
            var objnhacviecs = db.tnNhacViec_Details
                    .Where(p => p.NguoiNhan == User.MaNV & (p.DaDoc == false | p.DaDoc == null))
                    .Select(p => new
                    {
                        p.MaNhacViec,
                        p.tnNhacViec.NgayGui,
                        NguoiGui = p.tnNhacViec.tnNhanVien.HoTenNV,
                        NguoiNhan = p.tnNhanVien.HoTenNV,
                        p.tnNhacViec.NoiDung,
                        p.DaDoc,
                    }).ToList();

            if (objnhacviecs == null) return;
            gcNhacViec.DataSource = db.tnNhacViec_Details.Where(p => p.NguoiNhan == User.MaNV & (p.DaDoc == false | p.DaDoc == null))
                .Select(p => new
                {
                    p.ID,
                    p.tnNhacViec.NoiDung,
                    p.DaDoc
                });
            foreach (var obj in objnhacviecs)
            {
                string noidung = string.Format("<color=0, 0, 255><b><i>{0}</b></i></color> <br><b>Người gửi:</b> {1} <br><b>Ngày gửi:</b> {2}", obj.NoiDung, obj.NguoiGui, obj.NgayGui);
                AlertInfo info = new AlertInfo("<b>NHIỆM VỤ - NHẮC VIỆC</b>", noidung);
                alertControlNhacViec.Show(this, info);
            }
        }

        private void LoadNhacKeHoachBaoTri()
        {
            if (KyThuat.ClsXuLy.CheckKeHoachBaoTri() > 0)
            {
                string noidung = string.Format("<color=0, 0, 255><b><i>Hôm này có {0} kế hoạch bảo trì cần thực hiện</b></i></color>", KyThuat.ClsXuLy.CheckKeHoachBaoTri());
                AlertInfo info = new AlertInfo("<b>KẾ HOẠCH BẢO TRÌ</b>", noidung);
                alertControlNhacViec.Show(this, info);
            }
        }

        private void LoadYeuCauMoi()
        {
            try
            {
                // mã trạng thái = 1
                // tiêu đề, nội dung, ngày yêu cầu, mặt bằng
                const int yeuCauMoiStatusId = 1;
                var obj = (from yc in db.tnycYeuCaus
                           join mb in db.mbMatBangs on yc.MaMB equals mb.MaMB into matBang
                           from mb in matBang.DefaultIfEmpty()
                           where yc.MaTN == User.MaTN & yc.MaTT == yeuCauMoiStatusId
                           select new
                           {
                               yc.TieuDe,
                               yc.NoiDung,
                               yc.NgayYC,
                               MatBang = mb.MaSoMB + " - " + mb.mbTangLau.TenTL + " - " + mb.mbTangLau.mbKhoiNha.TenKN,
                               yc.ID
                           }).ToList();
                if (obj == null) return;
                gcNhacViec.DataSource = (from o in obj
                                         select new
                                         {
                                             ID = o.ID,
                                             NoiDung = string.Format("{0} - {1}: {2} - {3}", o.MatBang, o.TieuDe, o.NoiDung, o.NgayYC),
                                             DaDoc = false
                                         });
                foreach (var o in obj)
                {
                    string noiDung = string.Format("<color=0,0,255><b><i>{0}</i></b></color> <br><b>Mặt bằng: </b> {1} <br><b>Nội dung: </b> {2} <br><b>Ngày: </b>{3}", o.TieuDe, o.MatBang, o.NoiDung, o.NgayYC);

                    alertControlNhacViec.ButtonClick += AlertControlNhacViec_ButtonClick;

                    AlertInfo info = new AlertInfo("<b>YÊU CẦU MỚI</b>", noiDung);
                    alertControlNhacViec.Show(this, info);
                }
            }
            catch { }
        }

        private void AlertControlNhacViec_ButtonClick(object sender, AlertButtonClickEventArgs e)
        {
            if (e.ButtonName == "btnView")
            {
                LoadForm(new DichVu.YeuCau.frmManager());
            }
        }

        public void LoadForm(Form frm)
        {
            for (int i = 0; i < pageMain.Pages.Count; i++)
            {
                if (String.Compare(pageMain.Pages[i].Text, frm.Text.ToUpper(), false) != 0)
                    continue;

                pageMain.SelectedPage = pageMain.Pages[i];
                return;
            }

            frm.Text = frm.Text.ToUpper();
            frm.MdiParent = this;

            // kiểm tra và tạo form
            Library.PhanQuyen.CreateForm(frm.GetType().FullName, frm.Text);

            frm.Show();
        }
        public void LoadForm(Form frm, string FormName)
        {
            db = new MasterDataContext();
            for (int i = 0; i < pageMain.Pages.Count; i++)
            {
                if (String.Compare(pageMain.Pages[i].Text, FormName.ToUpper(), false) != 0)
                    continue;

                pageMain.SelectedPage = pageMain.Pages[i];
                return;
            }

            frm.Text = FormName.ToUpper();
            frm.MdiParent = this;

            // kiểm tra và tạo form
            Library.PhanQuyen.CreateForm(frm.GetType().FullName, frm.Text);

            frm.Show();
        }

        private void SetPhanQuyen(ItemClickEventArgs e)
        {
            if (FlagIsPhanQuyen) Library.HeThongCls.PhanQuyenCls.SetParameterPhanQuyen(e);
        }

        private void itemToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Dự án", "Xem"); SetPhanQuyen(e);

            LoadForm(new ToaNha.frmToaNha() { objNV = User });
        }

        private void itemLoaiMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại mặt bằng", "Xem"); SetPhanQuyen(e);

            LoadForm(new MatBang.frmLoaiMatBang() { objnhanvien = User });
        }

        private void itemNhaCungCap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhà cung cấp", "Xem"); SetPhanQuyen(e);

            LoadForm(new ToaNha.frmNhaCungCap() { objnv = User });
        }

        private void itemThamQuan_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới khách tham quan Dự án", "Thêm"); SetPhanQuyen(e);

            using (KyThuat.ThamQuan.frmEdit frm = new KyThuat.ThamQuan.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách tham quan Dự án", "Xem"); SetPhanQuyen(e);
                    LoadForm(new KyThuat.ThamQuan.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemThamQuan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách tham quan Dự án", "Xem"); SetPhanQuyen(e);

            LoadForm(new KyThuat.ThamQuan.frmManager() { objnhanvien = User });
        }

        private void itemKeHoachBaoTri_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới kế hoạch bảo trì", "Thêm");

            using (KyThuat.KeHoach.frmEdit frm = new KyThuat.KeHoach.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch bảo trì", "Xem"); SetPhanQuyen(e);
                    LoadForm(new KyThuat.KeHoach.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemKeHoachBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch bảo trì", "Xem"); SetPhanQuyen(e);

            LoadForm(new KyThuat.KeHoach.frmManager() { objnhanvien = User });
        }

        private void itemBaoTri_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới bảo trì", "Thêm");

            using (KyThuat.BaoTri.frmEdit frm = new KyThuat.BaoTri.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách bảo trì", "Xem"); SetPhanQuyen(e);
                    LoadForm(new KyThuat.BaoTri.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách bảo trì", "Xem"); SetPhanQuyen(e);

            LoadForm(new KyThuat.BaoTri.frmManager() { objnhanvien = User });
        }

        private void itemMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.MatBang.frmManager());
        }

        private void itemMatBang_TrangThai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái mặt bằng", "Xem"); SetPhanQuyen(e);

            LoadForm(new MatBang.frmTrangThai() { objnhanvien = User });
        }

        private void itemChoThue_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới hợp đồng cho thuê", "Thêm");
            SetPhanQuyen(e);
            using (var frm = new LandSoftBuilding.Lease.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng cho thuê", "Xem"); SetPhanQuyen(e);
                    LoadForm(new LandSoftBuilding.Lease.frmManager());
                }
            }
        }

        private void itemChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {

            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng cho thuê", "Xem"); SetPhanQuyen(e);

            LoadForm(new LandSoftBuilding.Lease.frmManager());
        }

        private void itemTheXe_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới thẻ xe", "Thêm");
            SetPhanQuyen(e);

            using (DichVu.GiuXe.frmEdit frm = new DichVu.GiuXe.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.GiuXe.frmTheXe());
                }
            }
        }

        private void itemLoaiXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại xe", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.GiuXe.frmLoaiXe());
        }

        private void itemThangMay_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới thẻ thang máy", "Thêm");
            SetPhanQuyen(e);
            using (DichVu.ThangMay.frmEdit frm = new DichVu.ThangMay.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ thang máy", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.ThangMay.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemThangMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ thang máy", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.ThangMay.frmManager() { objnhanvien = User });
        }

        private void itemNhanKhau_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới nhân khẩu", "Thêm");
            SetPhanQuyen(e);
            using (DichVu.NhanKhau.frmEdit frm = new DichVu.NhanKhau.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhân khẩu", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.NhanKhau.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemNhanKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhân khẩu", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.NhanKhau.frmManager() { objnhanvien = User });
        }

        private void itemDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ điện", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.Dien.frmManager());
        }

        private void itemNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ nước", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.Nuoc.frmManager());
        }

        private void itemDien_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức dịch vụ điện", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.Dien.frmDinhMuc());
        }

        private void itemNuoc_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức dịch vụ nước", "Xem"); SetPhanQuyen(e);

            using (DichVu.Nuoc.frmDinhMuc frm = new DichVu.Nuoc.frmDinhMuc())
            {
                frm.ShowDialog();
            }
        }

        private void itemThueNgoai_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hợp đồng thuê ngoài", "Thêm");
            SetPhanQuyen(e);
            using (DichVu.ThueNgoai.frmEdit frm = new DichVu.ThueNgoai.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê ngoài", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.ThueNgoai.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemThueNgoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê ngoài", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.ThueNgoai.frmManager() { objnhanvien = User });
        }

        private void itemHopTac_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hợp đồng hợp tác", "Thêm");
            SetPhanQuyen(e);
            using (DichVu.HopTac.frmEdit frm = new DichVu.HopTac.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng hợp tác", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.HopTac.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemHopTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng hợp tác", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.HopTac.frmManager() { objnhanvien = User });
        }

        private void itemDichVuKhac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ khác", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.Khac.frmManager());
        }

        private void itemSuaChua_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm yêu cầu sửa chữa của khách hàng", "Thêm"); SetPhanQuyen(e);

            using (KyThuat.SuaChua.frmEdit frm = new KyThuat.SuaChua.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu sửa chữa của khách hàng", "Xem"); SetPhanQuyen(e);
                    LoadForm(new KyThuat.SuaChua.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu sửa chữa của khách hàng", "Xem"); SetPhanQuyen(e);

            LoadForm(new KyThuat.SuaChua.frmManager() { objnhanvien = User });
        }

        private void itemYeuCau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu của khách hàng", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.YeuCau.frmManager());
        }

        private void itemYeuCau_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm yêu cầu của khách hàng", "Thêm"); SetPhanQuyen(e);

            using (DichVu.YeuCau.frmEdit frm = new DichVu.YeuCau.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu của khách hàng", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.YeuCau.frmManager());
                }
            }
        }

        private void itemKhachHang_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khách hàng", "Thêm"); SetPhanQuyen(e);

            using (DichVu.KhachHang.frmEdit frm = new DichVu.KhachHang.frmEdit() { objnv = User })
            {
                frm.maTN = (byte)Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách hàng", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.KhachHang.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách hàng", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.KhachHang.frmManager() { objnhanvien = User });
        }

        private void itemMatBang_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mặt bằng Dự án", "Thê"); SetPhanQuyen(e);

            using (var frm = new DichVu.MatBang.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng Dự án", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.MatBang.frmManager());
                }
            }
        }

        private void itemKhoiNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khối nhà", "Xem"); SetPhanQuyen(e);

            LoadForm(new MatBang.frmKhoiNha() { objnhanvien = User });
        }

        private void itemMatBang_View_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng Dự án", "Xem"); SetPhanQuyen(e);

            LoadForm(new MatBang.frmMatBang() { objnhanvien = User });

        }

        private void itemTangLau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tầng lầu", "Xem"); SetPhanQuyen(e);

            LoadForm(new MatBang.frmTangLau() { objnhanvien = User });
        }



        private void itemTrangThaiKHBT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái kế hoạch", "Xem"); SetPhanQuyen(e);

            LoadForm(new KyThuat.KeHoach.frmTrangThai() { objnhanvien = User });
        }

        private void itemTrangThaiChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái cho thuê", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.ChoThue.frmTrangThai() { objnhanvien = User });
        }

        private void itemTrangThaiThueNgoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái thuê ngoài", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.ThueNgoai.frmTrangThai() { objnhanvien = User });
        }

        private void itemTrangThaiHopTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái hợp tác", "Xem"); SetPhanQuyen(e);

            LoadForm(new DichVu.HopTac.frmTrangThai() { objnhanvien = User });
        }

        private void barButtonItem18_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            //ReportMisc.KhachHang.rptKHChoose frmKhachHang = new ReportMisc.KhachHang.rptKHChoose(); SetPhanQuyen(e);
            //frmKhachHang.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thoát phần mềm", "Thoát");
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            CreateFormControl(barButtonItem1.Name, barButtonItem1.Caption);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đăng xuất phần mềm", "Đăng xuất");
            //CreateFormControl(itemSkins.Name, itemSkins.Caption);
            Application.Restart();
        }

        private void btnexit_ItemClick(object sender, ItemClickEventArgs e)
        {
            //CreateFormControl(itemSkins.Name, itemSkins.Caption);
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thoát phần mềm", "Thoát");
            CreateFormControl(btnexit.Name, btnexit.Caption);
            if (DialogBox.Question("Bạn có chắc muốn thoát khỏi chương trình không?") == DialogResult.Yes)
                Application.Exit();
        }

        private void btnlogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đăng xuất phần mềm", "Đăng xuất");
            CreateFormControl(btnlogout.Name, btnlogout.Caption);
            if (DialogBox.Question("Bạn có chắc muốn đăng xuất không?") == DialogResult.Yes)
                Application.Restart();
        }

        private void btnchangepass_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thay đổi mật khẩu", "Thay đổi");
            CreateFormControl(btnchangepass.Name, btnchangepass.Caption);
            using (LandsoftBuildingGeneral.NguoiDung.frmChangePassword frmchange = new LandsoftBuildingGeneral.NguoiDung.frmChangePassword())
            {
                frmchange._user = this.User;
                frmchange.ShowDialog();
            }
        }

        private void btnGiayBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn giấy báo", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.HoaDon.frmManager() { objnhanvien = User });
        }

        private void btnHoaDon_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn dịch vụ khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.frmManager());
        }

        private void itemSkins_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thay đổi giao diện", "Xem");
            // kiểm tra và tạo form
            //Library.PhanQuyen.CreateForm(e.Item.na, f.Text);
            //SetPhanQuyen(e);  
            CreateFormControl(itemSkins.Name, itemSkins.Caption);
            Library.Properties.Settings.Default.SkinCurrent = e.Item.Caption;
            Library.Properties.Settings.Default.Save();
        }

        private void CreateFormControl(string controlName, string dienGiai)
        {
            var module = Library.PhanQuyen.GetModuleByName(controlName, dienGiai);
            if (module == null) return;
            // tạo form
            var form = Library.PhanQuyen.CreateForm(controlName, dienGiai);
            SaveControl(controlName, form.ID, module.ModuleID, module.ModuleParentID);
        }

        private void SaveControl(string controlName, int? formId, int? moduleId, int? moduleParent)
        {
            Library.PhanQuyen.CreateControl(formId, moduleId, controlName);
            if (moduleParent != 0)
            {
                var module = Library.PhanQuyen.GetModuleById(moduleParent);
                if (module == null) return;
                moduleParent = module.ModuleParentID;
                SaveControl(module.Name, formId, module.ModuleID, module.ModuleParentID);
            }
        }

        private void btnNhanVienManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách người dùng Dự án", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.NguoiDung.UserManager() { objnhanvien = User });
        }

        private void btnThemNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm người dùng cho Dự án", "Thêm"); SetPhanQuyen(e);
            using (LandsoftBuildingGeneral.NguoiDung.UserEdit frm = new LandsoftBuildingGeneral.NguoiDung.UserEdit())
            {
                frm.ShowDialog();
            }
        }

        private void btnPhongBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phòng ban", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.NguoiDung.frmPhongBanManager() { objnhanvien = User });
        }

        private void btnPhanQuyenNV_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân quyền theo nhân viên", "Phân quyền"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.PhanQuyen.frmPhanQuyenManager() { objnhanvien = User });
        }

        private void navBarItemThoat_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thoát ứng dụng", "Thoát");
            Application.Exit();
        }

        private void navBarItemThemNhanVien_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm người dùng Dự án", "Thêm");
            using (LandsoftBuildingGeneral.NguoiDung.UserEdit frm = new LandsoftBuildingGeneral.NguoiDung.UserEdit())
            {
                frm.ShowDialog();
            }
        }

        private void navBarItemQLNV_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách người dùng Dự án", "Xem");
            LoadForm(new LandsoftBuildingGeneral.NguoiDung.UserManager());
        }

        private void navBarItemPhanQuyen_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân quyền nhân viên", "Xem");
            LoadForm(new LandsoftBuildingGeneral.PhanQuyen.frmPhanQuyenManager());
        }

        private void navBarItemThemMatBang_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mặt bằng vào Dự án", "Xem");
            using (var frm = new DichVu.MatBang.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng Dự án", "Xem");
                    LoadForm(new DichVu.MatBang.frmManager());
                }
            }
        }

        private void navBarItemXemThongTheMB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng", "Xem");
            LoadForm(new MatBang.frmMatBang() { objnhanvien = User });
        }






        private void navBarItemThemVanHanh_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm tham quan Dự án", "Thêm");
            using (KyThuat.ThamQuan.frmEdit frm = new KyThuat.ThamQuan.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tham quan Dự án", "Xem");
                    LoadForm(new KyThuat.ThamQuan.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemThemKeHoachBaoTri_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm kế hoạch bảo trì", "Thêm");
            using (KyThuat.KeHoach.frmEdit frm = new KyThuat.KeHoach.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch bảo trì", "Xem");
                    LoadForm(new KyThuat.KeHoach.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemThemBaoTri_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm bảo trì cho khách hàng", "Thêm");
            using (KyThuat.BaoTri.frmEdit frm = new KyThuat.BaoTri.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách bảo trì của khách hàng", "Xem");
                    LoadForm(new KyThuat.BaoTri.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemThemSuaChua_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm sửa chữa thiết bị cho khách hàng", "Thêm");
            using (KyThuat.SuaChua.frmEdit frm = new KyThuat.SuaChua.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách sửa chữa thiết bị cho khách hàng", "Xem");
                    LoadForm(new KyThuat.SuaChua.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemTrangThaiNhanKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái nhân khẩu", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.NhanKhau.frmTrangThai());
        }

        private void navBarItemThanhToanHoaDon_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn dịch vụ", "Xem");
            LoadForm(new DichVu.HoaDon.frmManager());
        }

        private void navBarItemThemHopDongThue_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm dịch vụ cho thuê", "Thêm");
            using (DichVu.ChoThue.frmEdit frm = new DichVu.ChoThue.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ cho thuê", "Xem");
                    LoadForm(new DichVu.ChoThue.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemCongNoHDT_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê công nợ cho thuê", "Thống kê");
            LoadForm(new DichVu.ChoThue.CongNo.frmCongNoManager() { objnhanvien = User });
        }

        private void navBarItemDKTheXe_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm giữ xe", "Thêm");
            using (DichVu.GiuXe.frmEdit frm = new DichVu.GiuXe.frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe", "Xem");
                    LoadForm(new DichVu.GiuXe.frmTheXe());
                }
            }
        }

        private void navBarItemDKTheThangMay_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm thẻ thang máy", "Thêm");
            using (DichVu.ThangMay.frmEdit frm = new DichVu.ThangMay.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ thang máy", "Xem");
                    LoadForm(new DichVu.ThangMay.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemDKNhanKhau_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm nhân khẩu", "Thêm");
            using (DichVu.NhanKhau.frmEdit frm = new DichVu.NhanKhau.frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhân khẩu", "Xem");
                    LoadForm(new DichVu.NhanKhau.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemTienDien_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ điện", "Xem");
            LoadForm(new DichVu.Dien.frmManager());
        }

        private void navBarItemTienNuoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ nước", "Xem");
            LoadForm(new DichVu.Nuoc.frmManager());
        }

        private void navBarItemThemHDTN_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm dịch vụ thuê ngoài", "Thêm");
            using (DichVu.ThueNgoai.frmEdit frm = new DichVu.ThueNgoai.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ thuê ngoài", "Xem");
                    LoadForm(new DichVu.ThueNgoai.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemThemHDHT_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm dịch vụ hợp tác", "Thêm");
            using (DichVu.HopTac.frmEdit frm = new DichVu.HopTac.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách hàng", "Xem");
                    LoadForm(new DichVu.HopTac.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemThemKhachHangMoi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khách hàng", "Thêm");
            using (DichVu.KhachHang.frmEdit frm = new DichVu.KhachHang.frmEdit() { objnv = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách hàng", "Xem");
                    LoadForm(new DichVu.KhachHang.frmManager() { objnhanvien = User });
                }
            }
        }

        private void navBarItemThemYeuCau_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm yêu cầu", "Thêm");
            using (DichVu.YeuCau.frmEdit frm = new DichVu.YeuCau.frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu", "Xem");
                    LoadForm(new DichVu.YeuCau.frmManager());
                }
            }
        }




        private void btnThongKeTQ_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tham quan", "Xem"); SetPhanQuyen(e);
            LoadForm(new KyThuat.ThamQuan.frmThongKe() { objnhanvien = User });
        }

        private void butnTkMBTheoTT_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            //LoadForm(new DichVu.MatBang.ThongKe.frmThongKe() { objnhanvien = User });SetPhanQuyen(e); 
        }

        private void btnTKDienNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê dịch vụ điện nước", "Thống kê"); SetPhanQuyen(e);
            LoadForm(new DichVu.ThongKe.frmDienNuoc() { objnhanvien = User });
        }

        private void btnUpdater_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cập nhật phần mềm", "Cập nhật"); SetPhanQuyen(e);
            try
            {
                Process.Start(Application.StartupPath + "\\Updater.exe");
            }
            catch
            {
                Library.DialogBox.Error("Không tìm thấy file Updater.exe");
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xem giới thiệu", "Xem"); SetPhanQuyen(e);
            using (LandsoftBuildingGeneral.AboutUs.AboutUs abf = new LandsoftBuildingGeneral.AboutUs.AboutUs())
            {
                abf.ShowDialog();
            }
        }

        private void btnTrangThaiThangMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái của thang máy", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.ThangMay.frmTrangThai());
        }

        private void btnTrangThaiYC_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái của yêu cầu", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.frmTrangThai());
        }

        private void btnDoUuTienYC_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách độ ưu tiên của yêu cầu", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.frmDoUuTien());
        }

        private void btnThongBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thông báo nhắc việc chung", "Thông báo"); SetPhanQuyen(e);
            LoadForm(new ToaNha.NhacViec_ThongBao.frmThongBaoChung() { objnhanvien = User });
        }


        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            splitContainerControlRightPanel.SplitterPosition = splitContainerControlRightPanel.Height;
        }

        private void timerCapNhat_Tick(object sender, EventArgs e)
        {
            timerCapNhat.Stop();

            LoadThongBao();
            LoadNhacViec();

            timerCapNhat.Start();
        }

        private void itemCongNoTheoKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ theo khách hàng", "Báo cáo"); SetPhanQuyen(e);
            //using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.CongNoTheoKhachHang, objnhanvien = User })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void btnBaoCaothu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ thu theo khách hàng", "Báo cáo"); SetPhanQuyen(e);
            //using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoThu, objnhanvien = User })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void BtnBaoCaoChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ chi theo khách hàng", "Báo cáo"); SetPhanQuyen(e);
            //using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoChi, objnhanvien = User })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void btnBaoCaoCongNoTheoMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ theo mặt bằng của khách hàng", "Báo cáo"); SetPhanQuyen(e);
            //using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.CongNoTheoMatBang, objnhanvien = User })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemTKThuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê thu chi dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.ThongKe.frmThuChi() { objnhanvien = User });
        }

        private void navBarItemBaoCaoThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ thu theo khách hàng", "Báo cáo");
            //using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoThu, objnhanvien = User })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void navBarItemBaoCaoChi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ chi theo khách hàng", "Báo cáo");
            //using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoChi, objnhanvien = User })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemTKHDTTT_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new DichVu.ThongKe.frmHopDong() { objnhanvien = User });
        }

        private void itemDoanhThuHopDongThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê doanh thu hợp đồng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.ThongKe.frmDoanhThuHopDong());
        }

        private void itemTTNhacNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái nhắc nợ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.frmTrangThaiNhacNo() { objnhanvien = User });
        }

        private void itemNhacNoTheoKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhắc nợ dịch vụ khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.frmNhacNoKhachHang() { objnhanvien = User });
        }

        private void exportTS2Excel_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                DataTable dt = new DataTable();

                if (User.IsSuperAdmin.Value)
                {
                    var ts = db.tsTaiSanSuDungs
                    .Select(p => new
                    {
                        KyHieu = p.KyHieu,
                        MaTaiSan = p.MaTS,
                        TenTaiSan = p.tsLoaiTaiSan.TenLTS,
                        ThuocMatBang = p.mbMatBang.MaSoMB,
                        TrangThai = p.tsTrangThai.TenTT,
                        NhaCungCap = p.tnNhaCungCap.TenVT,
                        HangSanXuat = p.tsHangSanXuat.TenHSX,
                        XuatXu = p.tsXuatXu.TenXX,
                        NgaySanXuat = string.Format("{0:d}", p.NgaySX),
                        HanSuDung = string.Format("{0:d}", p.NgayHH),
                        NgayBDSD = string.Format("{0:d}", p.NgaySD),
                        ThoiHan = string.Format("{0:d} tháng", p.ThoiHan)
                    });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách tài sản", dt);
                }
                else
                {
                    var ts = db.tsTaiSanSuDungs
                        .Where(p => p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == User.MaTN)
                        .Select(p => new
                        {
                            KyHieu = p.KyHieu,
                            MaTaiSan = p.MaTS,
                            TenTaiSan = p.tsLoaiTaiSan.TenLTS,
                            ThuocMatBang = p.mbMatBang.MaSoMB,
                            TrangThai = p.tsTrangThai.TenTT,
                            NhaCungCap = p.tnNhaCungCap.TenVT,
                            HangSanXuat = p.tsHangSanXuat.TenHSX,
                            XuatXu = p.tsXuatXu.TenXX,
                            NgaySanXuat = string.Format("{0:d}", p.NgaySX),
                            HanSuDung = string.Format("{0:d}", p.NgayHH),
                            NgayBDSD = string.Format("{0:d}", p.NgaySD),
                            ThoiHan = string.Format("{0:d} tháng", p.ThoiHan)
                        });
                    dt = SqlCommon.LINQToDataTable(ts);
                    ExportToExcel.exportDataToExcel("Danh sách tài sản", dt);
                }

            }
        }

        private void btnExportDV2Excel_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            frmKyBaoCao kbc = new frmKyBaoCao() { objnhanvien = User };
            kbc.ShowDialog();
        }

        private void navBarItemPhieuThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Input.frmManager());
        }

        private void btnCongNohdtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ thuê ngoài", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.ThueNgoai.frmCongNoManager() { objnhanvien = User });
        }

        private void navBarItemPhieuChi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu chi", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Output.frmManager());
        }

        private void btnCongNoHDHT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ hợp tác", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.HopTac.frmCongNoManager() { objnhanvien = User });
        }

        private void itemNhacViec_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemEmailAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tài khoản email", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.SendMail.frmEmailAccountManager());
        }

        private void itemNewEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm gửi mail", "Thêm"); SetPhanQuyen(e);
            using (DichVu.SendMail.frmSendMailEdit frm = new DichVu.SendMail.frmSendMailEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemEmailListManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách gửi mail", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.SendMail.frmMailManager() { objnhanvien = User });
        }

        private void itemMailNhacNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Gửi mail nhắc nợ", "Gửi mail");
            using (DichVu.SendMail.frmSendMailNhacNo frm = new DichVu.SendMail.frmSendMailNhacNo() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemThongKeYeuCau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê yêu cầu", "Thống kê"); SetPhanQuyen(e);
            LoadForm(new DichVu.ThongKe.frmYeuCau() { objnhanvien = User });
        }

        private void checkIsRead_CheckedChanged(object sender, EventArgs e)
        {
            if (grvNhacViec.GetFocusedRowCellValue("ID") == null) return;

            try
            {
                int manhacviec = (int)grvNhacViec.GetFocusedRowCellValue("ID");
                var objviec = db.tnNhacViec_Details.Single(p => p.ID == manhacviec);
                objviec.DaDoc = true;
                db.SubmitChanges();
            }
            catch { }
        }

        private void grvNhacViec_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //CheckEdit ck = sender as CheckEdit;

            //if (e.RowHandle < 0) return;

            //if (e.Column.FieldName == "DaDoc")
            //{
            //    try
            //    {
            //        int manhacviec = (int)grvNhacViec.GetFocusedRowCellValue("ID");
            //        var objviec = db.tnNhacViec_Details.Single(p => p.ID == manhacviec);
            //        objviec.DaDoc = ck.Checked;
            //        db.SubmitChanges();
            //    }
            //    catch { }
            //}
        }

        private void itemBaoCaoDeXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đề xuất", "Báo cáo");
            //using (ReportMisc.TongHop.frmBaoCaoTheoThoiGian frm = new ReportMisc.TongHop.frmBaoCaoTheoThoiGian() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoDeXuat })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemBaoCaoMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo mua hàng", "Báo cáo");
            //using (ReportMisc.TongHop.frmBaoCaoTheoThoiGian frm = new ReportMisc.TongHop.frmBaoCaoTheoThoiGian() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoMuaHang })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm gửi mail", "Gửi mail"); SetPhanQuyen(e);
            using (DichVu.SendMail.frmSendMailEdit frm = new DichVu.SendMail.frmSendMailEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itmBaoCaoDonDatHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đặt hàng theo thời gian", "Báo cáo");
            //using (ReportMisc.TongHop.frmBaoCaoTheoThoiGian frm = new ReportMisc.TongHop.frmBaoCaoTheoThoiGian() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoDatHang })
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemCopyRight_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://dip.vn/");
            }
            catch { }
        }

        private void itemBaoCaoMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            //using (ReportMisc.TongHop.frmBaoCaoTheoThoiGian frm = new ReportMisc.TongHop.frmBaoCaoTheoThoiGian() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoMatBang })
            //{
            //    frm.ShowDialog();
            //}
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo danh sách mặt bằng", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.MatBang.rptDanhSachMatBang rpt = new ReportMisc.MatBang.rptDanhSachMatBang();
            //rpt.ShowPreviewDialog();
        }

        private void itemThongBaoThuPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo giấy thông báo phí", "Báo cáo");
            //ReportMisc.TongHop.rptGiayBaoThanhToanTong rpt = new ReportMisc.TongHop.rptGiayBaoThanhToanTong(User);
            //rpt.ShowPreviewDialog();
        }

        private void itemThongBaoCatNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thông báo tạm ngừng cung cấp nước", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.rptThongBaoTamNgungCapNuoc rpt = new ReportMisc.TongHop.rptThongBaoTamNgungCapNuoc(User);
            //rpt.ShowPreviewDialog();
        }

        private void itemBaoCaoTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hpwpj số tiền thực thu", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.BaoCaoTongHopSoTienThucThu, objnhanvien = User };
            //frm.ShowDialog();
        }

        private void itemBaoCaoKetQauKinhDoanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo kết quả kinh doanh", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.BaoCaoKetQuaKinhDoanh, objnhanvien = User };
            //frm.ShowDialog();
        }

        private void itemChiTietNopTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết nộp tiền", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.ChiTietNopTien, objnhanvien = User };
            //frm.ShowDialog();
        }

        private void itemChiTietThuPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết thu  phí quản lý", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.ChiTietThuPhiQuanLy, objnhanvien = User };
            //frm.ShowDialog();
        }

        private void itemTheoDoiCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo theo dõi công nợ tổng hợp", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.TheoDoiCongNoTongHop, objnhanvien = User };
            //frm.ShowDialog();
        }

        private void itemCongNoTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp công nợ", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.BaoCaoTongHopCongNo, objnhanvien = User };
            //frm.ShowDialog();
        }

        private void itemTheoDoiCongNoPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo theo dõi công nợ phí quản lý", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.TheoDoiCongNoPhiQuanLy, objnhanvien = User };
            //frm.ShowDialog();
        }

        private void itemTQQuanLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Quản lý vận hành", "Xem"); SetPhanQuyen(e);
            LoadForm(new KyThuat.ThamQuan.frmManager() { objnhanvien = User });
        }

        private void btnAnhNinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch", "Xem"); SetPhanQuyen(e);
            LoadForm(new AnNinh.frmKeHoach() { objnhanvien = User });
        }

        private void btnMyMisson_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch của nhân viên", "Xem"); SetPhanQuyen(e);
            AnNinh.frmKeHoachCuaToi frm = new AnNinh.frmKeHoachCuaToi() { objnhanvien = User, ViewLog = false };
            frm.Show();
        }

        private void btnNhatKyAN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch của nhân viên", "Xem"); SetPhanQuyen(e);
            AnNinh.frmKeHoachCuaToi frm = new AnNinh.frmKeHoachCuaToi() { objnhanvien = User, ViewLog = true };
            frm.Show();
        }

        private void btnGhiNhanSV_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Ghi nhận sự việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new AnNinh.frmGhiNhanSuViec() { objnhanvien = User });
        }

        private void btnDinhNghiaTruongBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trường định nghĩa của biểu mẫu", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmFieldDefine());
        }

        private void btnQuanLyBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách biểu mẫu", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmManager() { objnhanvien = User });
        }

        private void btnLoaiBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại biểu mẫu", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmLoaiBieuMau() { objnhanvien = User });
        }

        private void btnAdminLogAN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhật ký login", "Xem"); SetPhanQuyen(e);
            LoadForm(new AnNinh.frmAdminLogGhiNhanSuViec() { objnhanvien = User });
        }

        private void btnChucVuMNG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chức vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmChucVu() { objnhanvien = User });
        }

        private void btnPhiQuanLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ phí quản lý", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.PhiQuanLy.frmManager() { objnhanvien = User });
        }

        private void btnQuyTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đánh số tự động", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmDanhSoTuDong());
        }

        private void btnKhuVuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khu vực Dự án", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmKhuVuc() { objnhanvien = User });
        }

        private void btnThucHienKeHoach_ItemClick(object sender, ItemClickEventArgs e)
        {
            // LoadForm(new KyThuat.KeHoach.frmThucHien() { objnhanvien = User });
        }

        private void btnDichVuCongCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ công cộng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.DichVuCongCong.frmManager());
        }

        private void btnDichVuCCManger_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ công nợ công cộng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.DichVuCongCong.frmCongNo() { objnhanvien = User });
        }

        private void navBarItemXemThucThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thực thu kế toán", "Xem");
            LoadForm(new DichVu.Quy.frmThucThuPhieuThu());
        }

        private void alertControlNhacViec_AlertClick(object sender, AlertClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thông báo nhắc việc", "Xem");
            LoadForm(new ToaNha.NhacViec_ThongBao.frmNhacViec() { objnhanvien = User });
        }

        private void alertControlKeHoachBaoTri_AlertClick(object sender, AlertClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch của kỹ thuật", "Xem");
            LoadForm(new KyThuat.KeHoach.frmManager() { objnhanvien = User });
        }



        private void btnBangkePhieuXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bảng kê phiếu xuất", "Xem"); SetPhanQuyen(e);
            //ReportMisc.TaiSan.NhapXuat.frmKyBaoCao frm = new ReportMisc.TaiSan.NhapXuat.frmKyBaoCao() { objnhanvien = User, typebc = ReportMisc.TaiSan.NhapXuat.enumXuatNhapKho.XuatKho };
            //frm.ShowDialog();
        }

        private void btnBangKePhieuNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bảng kê phiếu nhập", "Xem"); SetPhanQuyen(e);
            //ReportMisc.TaiSan.NhapXuat.frmKyBaoCao frm = new ReportMisc.TaiSan.NhapXuat.frmKyBaoCao() { objnhanvien = User, typebc = ReportMisc.TaiSan.NhapXuat.enumXuatNhapKho.NhapKho };
            //frm.ShowDialog();
        }

        private void rptThongKeKhoaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thống kê kho hàng", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.TaiSan.frmPickStore frm = new ReportMisc.TaiSan.frmPickStore() { objnhanvien = User };
            //frm.ShowDialog();
        }

        private void btnThemLichThanhToanDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm lịch thanh toán điện", "Thêm"); SetPhanQuyen(e);
            using (KyThuat.ThanhToanDichVu.frmEdit_Dien frm = new KyThuat.ThanhToanDichVu.frmEdit_Dien())
            {
                frm.ShowDialog();
            }
        }

        private void btnLichThanhToanDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch thanh toán điện", "Xem"); SetPhanQuyen(e);
            LoadForm(new KyThuat.ThanhToanDichVu.frmManager() { objnhanvien = User });
        }

        private void btnNgonNgu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ngôn ngữ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.frmNgonNgu());
        }

        private void btnLoaiThePhongTap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại thẻ phòng tập", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.PhongTap.frmLoaiThe());
        }

        private void btnAddThePhongTap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm thẻ phòng tập", "Thêm"); SetPhanQuyen(e);
            using (DichVu.PhongTap.frmEdit frm = new DichVu.PhongTap.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void btnQuanLyThePhongTap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ phòng tập", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.PhongTap.frmManager() { objnhanvien = User });
        }

        private void btnVietPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            using (frmVietPhieuThu frm = new frmVietPhieuThu() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadForm(new DichVu.Quy.frmPhieuThu() { objnhanvien = User });
            }
        }

        private void btnVietPhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            using (frmVietPhieuChi frm = new frmVietPhieuChi() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadForm(new DichVu.Quy.frmPhieuChi() { objnhanvien = User });
            }
        }

        private void btnHoaDonGiayBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.HoaDon.frmManager() { objnhanvien = User });
        }

        private void btnKhachHangBaoCao_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //using (ReportMisc.MatBang.frmChoose frm = new ReportMisc.MatBang.frmChoose())
            //{
            //    frm.ShowDialog();
            //}
        }

        private void btnGiuXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo danh sách gửi xe", "Báo cáo"); SetPhanQuyen(e);
            //ReportMisc.DichVu.GiuXe.rptDanhSachGuiXe frm = new ReportMisc.DichVu.GiuXe.rptDanhSachGuiXe();
            //frm.ShowPreviewDialog();
        }

        private void btnTieuThuDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tiêu thụ điện", "Thống kê");
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 1;
            //f.ShowDialog();
            //using (ReportMisc.TongHop.frmKyBaoCaoTieuThu frm = new ReportMisc.TongHop.frmKyBaoCaoTieuThu())
            //{
            //    frm.objnhanvien = User;
            //    frm.LoaiBaoCao = 1; // dien
            //    frm.ShowDialog();
            //    frm.Close();
            //}
        }

        private void btnTieuThuNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tiêu thụ nước", "Thống kê"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 3;
            //f.ShowDialog();
            //using (ReportMisc.TongHop.frmKyBaoCaoTieuThu frm = new ReportMisc.TongHop.frmKyBaoCaoTieuThu())
            //{
            //    frm.objnhanvien = User;
            //    frm.LoaiBaoCao = 2; // nuoc
            //    frm.ShowDialog();
            //    frm.Close();
            //}
        }

        private void btnThongKeTieuThuDienNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tiêu thu điện nước", "Thống kê"); SetPhanQuyen(e);
            //using (ReportMisc.TongHop.frmThongKeDienNuoc frm = new ReportMisc.TongHop.frmThongKeDienNuoc())
            //{
            //    frm.objnhanvien = User;
            //    frm.ShowDialog();
            //    frm.Close();
            //}
        }

        private void btnGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ Gas", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Gas.frmManager());
        }

        private void btnsmGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức Gas", "Cài đặt"); SetPhanQuyen(e);
            LoadForm(new DichVu.Gas.frmDinhMuc());
        }

        private void btnSupport_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            //try
            //{
            //    //System.Net.WebClient client = new System.Net.WebClient();
            //    //var Version = client.DownloadString("http://support.dip.vn/dll/v12/support.txt");
            //    //if (Version != DIPCRM.Support.Library.Common.Version)
            //    //{
            //    //    //DIPCRM.Support.Updater.UpdateVer f = new DIPCRM.Support.Updater.UpdateVer();
            //    //    //f.ShowDialog();
            //    //    DIPCRM.Support.Library.Common.Version = Version;
            //    //}
            //}
            //catch
            //{ }
            //#endregion
            //var support = new DIPCRM.Support.Library.SupportConfig();
            //DIPCRM.Support.Library.Common.ConnectionString = global::Library.Properties.Settings.Default.Building_dbConnectionString;
            //try
            //{
            //    support.GetAccount();
            //}
            //catch (Exception ex) { DialogBox.Alert(ex.Message); }
            //DIPCRM.Support.Library.Common.ClientNo = support.ClientNo;
            //DIPCRM.Support.Library.Common.ClientPass = support.ClientPass;
            //DIPCRM.Support.Library.Common.ClientEmail = support.Email;
            //DIPCRM.Support.Library.Common.ClientName = support.Name;
            //DIPCRM.Support.Library.Common.StaffName = "(" + User.MaSoNV + ") " + User.HoTenNV;

            //LoadForm(new frmSupport());
            try
            {
                string clientNo = "", clientPass = "", email = "", name = "";
                using (var db = new MasterDataContext())
                {
                    db.Support_getAccount(ref name, ref email, ref clientNo, ref clientPass);
                }
                CallDIPSupport.Call(name, email, clientNo, clientPass);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPhiVeSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phí vệ sinh", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.PhiVeSinh.frmManager() { objnhanvien = User });
        }

        private void btnChietKhauPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt chiết khấu cho phí quản lý", "Cài đặt"); SetPhanQuyen(e);
            using (DichVu.PhiQuanLy.frmDinhMuc frm = new DichVu.PhiQuanLy.frmDinhMuc() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemTongHopDNG_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemSettingPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt phí quản lý", "Cài đặt"); SetPhanQuyen(e);
            LoadForm(new DichVu.PhiQuanLy.frmSetting() { objnhanvien = User });
        }

        private void itemSetupPhiChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt phí cho thuê", "Cài đặt"); SetPhanQuyen(e);
            LoadForm(new DichVu.ChoThue.frmSetting() { objnhanvien = User });
        }

        private void itemAddProvider_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm nhà cung cấp", "Thêm"); SetPhanQuyen(e);
            var frm = new Provider.frmEdit();
            frm.ShowDialog();
        }

        private void itemListProvider_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhà cung cấp", "Xem"); SetPhanQuyen(e);
            LoadForm(new Provider.frmManager() { objnhanvien = User });
        }

        private void itemDMNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức dịch vụ nước", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Nuoc.frmDinhMuc());
        }

        private void itemTXDinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức dịch vụ giữ xe", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.frmDinhMuc());
        }

        private void itemBCBangKeThuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê thu chi", "Báo cáo"); SetPhanQuyen(e);
            //using (var f = new ReportMisc.DichVu.Option.fromMonthYear())
            //{
            //    f.objnhanvien = User;
            //    f.ShowDialog();
            //}
        }

        private void itemCustomerDeb_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //using (var frm = new ReportMisc.frmChooseFromTo())
            //{
            //    frm.ShowDialog();
            //}
        }

        private void itemBCGasChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo Gas chi tiết", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 6;
            //f.ShowDialog();
        }

        private void itemBCNuocChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo nước chi tiết", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 5;
            //f.ShowDialog();
        }

        private void itemBCDienChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo điện chi tiết", "Báo cáo");
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 4;
            //f.ShowDialog();
        }

        private void itemBCDSGasNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo doanh số Gas- Nước", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.frmPickDateTower();
            //f.objNV = User;
            //f.ShowDialog();
        }

        private void itemBCGasPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu Gas", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 8;
            //f.ShowDialog();
        }

        private void itemBCNuocPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu nước", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 7;
            //f.ShowDialog();
        }

        private void itemBCPhatSinhPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phát sinh phí quản lý", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 9;
            //f.ShowDialog();
        }

        private void itemBCTongHopThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp thu", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.frmPickDateTower();
            //f.objNV = User;
            //f.CateID = 2;
            //f.ShowDialog();
        }

        private void itemBCTongPhaiThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp phải thu", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 10;
            //f.ShowDialog();
        }

        private void itemBCTongHopChuaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp chưa thu", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.frmPickDateTower();
            //f.objNV = User;
            //f.CateID = 4;
            //f.ShowDialog();
        }

        private void itemBCDoanhThuTheoNgay_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo doanh thu theo ngày", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.frmPickDateTowerV2();
            //f.objNV = User;
            //f.CateID = 1;
            //f.ShowDialog();
        }

        private void itemBCPQLBangThuPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phí quản lý thu chi", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 11;
            //f.ShowDialog();
        }

        private void itemBCPQLChiTietPhatSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phí quản lý chi tiết phát sinh", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 12;
            //f.ShowDialog();
        }

        private void itemBCLuyKeNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo lũy kế theo năm", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOptionV2();
            //f.objNV = User;
            //f.CateID = 1;
            //f.ShowDialog();
        }

        private void itemBCGasTTNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo Gas", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOptionV2();
            //f.objNV = User;
            //f.CateID = 2;
            //f.ShowDialog();
        }

        private void itemBCDienTTNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo điện", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOptionV2();
            //f.objNV = User;
            //f.CateID = 3;
            //f.ShowDialog();
        }

        private void itemBCNuocTTNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo nước", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOptionV2();
            //f.objNV = User;
            //f.CateID = 4;
            //f.ShowDialog();
        }

        private void itemNgungCungCapDV_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
        }

        private void itemBCPQL_PhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu phí quản lý", "Báo cáo"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 14;
            //f.ShowDialog();
        }

        private void itemKhoaSoAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khóa sổ", "Thêm"); SetPhanQuyen(e);
            var f = new DichVu.KhoaSo.frmEdit() { objnhanvien = User };
            f.ShowDialog();
        }

        private void itemKhoaSoList_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khóa sổ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.KhoaSo.frmManager() { objnhanvien = User });
        }

        private void itemPhieuTHuPVS_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 15;
            //f.ShowDialog();
        }

        private void navPhieuThuChiTiet_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadForm(new DichVu.Quy.frmPhieuThuV2() { objnhanvien = User });
        }

        private void itemBCTX_DanhSachDangKy_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 17;
            //f.ShowDialog();
        }

        private void itemBCTX_ChiTietPhatSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 16;
            //f.ShowDialog();
        }

        private void itemBCTX_PhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 18;
            //f.ShowDialog();
        }

        private void itemBCTX_ChuaThanhToan_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e);
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem"); SetPhanQuyen(e);
            //var f = new ReportMisc.DichVu.frmOption();
            //f.objNV = User;
            //f.CateID = 19;
            //f.ShowDialog();
        }

        private void itemTXList_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.frmTheXe());
        }

        private void itemConfigFTP_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình FTP", "Cấu hình");

            //int ModuleParent = 2;
            //var lControlName = Library.PhanQuyen.GetListControlName(e);
            //// khởi tạo main
            //var formMain = Library.PhanQuyen.KhoiTaoMain(ModuleParent, lControlName);
            //if (formMain == null) return;
            var f = new FTP.frmConfig();
            //// kiểm tra và tạo form
            //var a =Library.PhanQuyen.CreateForm(f.GetType().FullName, f.Text);
            ////Library.PhanQuyen.CreatePhanQuyen(f.GetType().FullName, f.Text, ModuleParent, formMain.ID, lControlName);
            //var module = Library.PhanQuyen.CreateModule("Cấu hình FTP", "Cấu hình FTP", ModuleParent);
            //Library.PhanQuyen.CreateControl(a.ID, module.ModuleID, "itemConfigFTP");
            SetPhanQuyen(e);
            f.ShowDialog();
        }

        private void itemDKUuDai_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ưu đãi nhân khẩu", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.NhanKhau.UuDai.frmManager() { objnhanvien = User });
        }

        private void itemDKUuDai_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm ưu đãi cho nhân khẩu", "Thêm"); SetPhanQuyen(e);
            var f = new DichVu.NhanKhau.UuDai.frmEdit();
            f.objnhanvien = User;
            f.ShowDialog();
        }

        private void itemHoBoiThe_list_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ hồ bơi", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.HoBoi.frmManager() { objnhanvien = User });
        }

        private void itemHoBoiCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ hồ bơi", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.HoBoi.frmHBManager() { objnhanvien = User });
        }

        private void itemHoiBoiLoaiThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại thẻ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.HoBoi.LoaiThe.frmLoaiThe());
        }

        private void itemHoBoiDinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức hồ bơi", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.HoBoi.DinhMuc.frmManager() { objnhanvien = User });
        }

        private void itemLaiSuatChamNop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt lãi suất", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmLaiSuat());
        }

        private void itemCaiDatTTCanhBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái cảnh báo", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.CanhBao.frmManager());
        }

        private void itemPhiBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ phí bảo trì", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.PhiBaoTri.frmManager() { objnhanvien = User });
        }


        private void itemDSCVLuoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đầu mục công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new KyThuat.DauMucCongViec.frmManager() { objnhanvien = User });
        }

        private void itemDSCVLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new KyThuat.DauMucCongViec.frmLichCongViec() { objnhanvien = User });
        }

        private void itemDSCongViecDG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhiệm vụ của nhân viên", "Xem"); SetPhanQuyen(e);
            LoadForm(new KyThuat.NhiemVu.frmManager() { objnhanvien = User });
        }

        private void itemDSCVDGLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch làm việc của nhân viên", "Xem"); SetPhanQuyen(e);
            LoadForm(new KyThuat.NhiemVu.frmLichLamViecNV() { objnhanvien = User });
        }



        private void itemTNTyGia_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tỷ giá", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmTyGia() { objnv = User });
        }



        private void itemDMTK_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức tài khoản", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmTaiKhoan());
        }

        private void itemMayDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Dien.DieuHoaNgoaiGio.frmManager());
        }

        private void itemThemDHNG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm điện điều hòa ngoài giờ", "Thêm"); SetPhanQuyen(e);
            using (var frm = new DichVu.Dien.DieuHoaNgoaiGio.frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.Dien.DieuHoaNgoaiGio.frmManager());
                }
            }
        }

        private void itemDSDHNG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Dien.DieuHoaNgoaiGio.frmManager());
        }

        private void itemBanCDPSCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng cân đối phát sinh công nợ", "Báo cáo"); SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.BangCanDoiPhatSinhCongNo(1, DateTime.Now, DateTime.Now.AddDays(30));
            //rpt.ShowPreviewDialog();
        }

        private void itemBangKeCT_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê chứng từ", "Báo cáo"); SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rpt_BangKeChungTu();
            //rpt.ShowPreviewDialog();
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê thu phí chung cư", "Báo cáo"); SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.BangKeThuTienPhiChungCu();
            //rpt.ShowPreviewDialog();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các căn hộ dân còn nợ phí", "Báo cáo"); SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.DanhSachCacCanHoDanNoPhi();
            //rpt.ShowPreviewDialog();
        }

        private void itemSoCTCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.SoChiTietCongNo();
            //rpt.ShowPreviewDialog();
        }

        private void itemKhoanThuHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tiền thuê diện tích văn phòng", "Báo cáo"); SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rptBCTienThueDienTichVP();
            //rpt.ShowPreviewDialog();
        }

        private void itemBCCacKhoanPhiDV_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các khoản thu phí dịch vụ", "Báo cáo"); SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rptBCCacKhoanThuPhiDV();
            //rpt.ShowPreviewDialog();
        }

        private void itemBCCacKhoanTon_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các khoản tồn động", "Báo cáo"); SetPhanQuyen(e);
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rptBCCacKhoanTonDong();
            //rpt.ShowPreviewDialog();
        }

        private void itemLoaiHDTN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm dịch vụ thuê ngoài", "Thêm"); SetPhanQuyen(e);
            using (var frm = new DichVu.ThueNgoai.frmLoaiDV() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemBCTongHopCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê công nợ", "Xem"); SetPhanQuyen(e);
            //LoadForm(new ReportMisc.DichVu.Quy.frmManager() { objnhanvien = User });
        }



        private void itemDCChiTietCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết công nợ", "Xem"); SetPhanQuyen(e);
            //LoadForm(new ReportMisc.BaoCaoCTM.BaoCao.frmManager() { objnhanvien = User });
        }

        private void itemTHemDVGS_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm ghi sổ cuối kỳ", "Thêm"); SetPhanQuyen(e);
            var f = new DichVu.GhiSo.frmEdit() { objnhanvien = User };
            f.ShowDialog();
        }

        private void itemDSGS_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ghi sổ cuối kỳ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GhiSo.frmManager() { objnhanvien = User });
        }

        private void itemDKThanhToan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đăng ký phí quản lý", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.PhiQuanLy.DangKy.frmManager() { objnhanvien = User });
        }

        private void itemLSDCCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch sử điều chỉnh công nợ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.LichSuDieuChinhCN.frmManager() { objnhanvien = User });
        }

        private void itemLoaiTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Loại tiền", "Xem"); SetPhanQuyen(e);
            //them moi
            LoadForm(new ToaNha.frmLoaiTien());
        }

        private void itemLoaiGiaThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loiaj giá thuê", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmLoaiGiaThue());
        }

        private void itemHoaDonAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tạo hóa đơn tự động", "Thêm"); SetPhanQuyen(e);
            using (var frm = new LandSoftBuilding.Receivables.frmAddAuto())
            {
                frm.ShowDialog();
            }
        }

        private void itemNhacNoKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ tổng hợp theo khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.frmReceivables());
        }

        private void itemLoaiDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại dịch vụ Dự án", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmLoaiDichVu());
        }

        private void itemCaiDatChietKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chiết khấu dịch vụ Dự án", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmChietKhau());
        }

        private void itemTheXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.frmTheXe());
        }

        private void itemThueNgoaiGio_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cho thuê ngoài giờ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.Overtime.frmManager());
        }

        private void itemThueNganHanAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm cho thuê ngắn hạng", "Thêm"); SetPhanQuyen(e);
            using (var frm = new LandSoftBuilding.Lease.ShortTerm.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cho thuê ngắn hạng", "Xem"); SetPhanQuyen(e);
                    LoadForm(new LandSoftBuilding.Lease.ShortTerm.frmManager());
                }
            }
        }

        private void itemThueNganHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cho thuê ngắn hạng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.ShortTerm.frmManager());
        }

        private void itemDichVuKhacAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Dịch vụ khác", "Thêm"); SetPhanQuyen(e);
            using (var frm = new DichVu.Khac.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ khác", "Xem"); SetPhanQuyen(e);
                    LoadForm(new DichVu.Khac.frmManager());
                }
            }
        }

        private void itemHoaDon_ThongKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.Reports.frmCongNo());
        }

        private void itemReport_PhiDaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết các khoản đã thu dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Reports.frmPhieuThu());
        }

        private void itemReportDaThuChungCu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các khoản đã thu của Dự án", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.frmBaoCaoCacKhoanDaThuChungCu());
        }

        private void itemReport_BaoCaoDatCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê công nợ đặt cọc", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.Reports.frmCongNoDatCoc());
        }

        private void itemReport_HopDongChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê cho thuê", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.Reports.frmManager());
        }

        private void itemNuoc_UuDai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ưu đãi của dịch vụ nước", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Nuoc.frmUuDai());
        }

        private void itemDonViTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đơn vị tính", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmDonViTinh());
        }

        private void itemBangGiaDichVuCoBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách bảng giá dịch vụ Dự án", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmBangGiaDichVu());
        }

        private void itemDien3Pha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện 3 pha", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Dien.Dien3Pha.frmManager());
        }

        private void itemDien3Pha_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức điện 3 pha", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Dien.Dien3Pha.frmDinhMuc());
        }

        private void itemThanhLyChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng thanh lý", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.Liquidate.frmManager());
        }

        private void itemChoThue_LTT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng đang thuê", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.PaymentSchedule.frmManager());
        }

        private void itemBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách báo cáo", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new frmReportList());
        }

        private void itemNuocCachTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cách tinh nước", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.frmCachTinh());
        }

        private void itemGiuXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách giữ xe", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.GiuXe.frmManager());
        }

        private void itemPhanNhomMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phân nhóm mặt bằng", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.MatBang.PhanNhom.frmPhanNhom());
        }

        private void itemDienDongHo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đồng hồ điện", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Dien.frmDongHo());
        }

        private void itemNuocDongHo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đồng hồ nước", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.frmDongHo());
        }

        private void itemNganHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm ngân hàng", "Thêm"); SetPhanQuyen(e);
            using (var frm = new ToaNha.frmNganHang())
            {
                frm.ShowDialog();
            }
        }

        private void itemPhieuThuAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm phiếu thu", "Thêm"); SetPhanQuyen(e);
            using (var frm = new LandSoftBuilding.Fund.Input.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.LoadForm(new LandSoftBuilding.Fund.Input.frmManager());
            }
        }

        private void itemPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Input.frmManager());
        }

        private void itemPhieuChiAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm phiếu chi", "Thêm"); SetPhanQuyen(e);
            using (var frm = new LandSoftBuilding.Fund.Output.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.LoadForm(new LandSoftBuilding.Fund.Output.frmManager());
            }
        }

        private void itemPhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu chi", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Output.frmManager());
        }

        private void itemKhauTruAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khấu trừ thu trước", "Thêm"); SetPhanQuyen(e);
            using (var frm = new LandSoftBuilding.Fund.Deduct.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.LoadForm(new LandSoftBuilding.Fund.Deduct.frmManager());
            }
        }

        private void itemKhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu khấu trừ thu trước", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Input.frmManager_KhauTru());
        }

        private void itemDichVu_Report_KhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemNuocNong_DongHo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đồng hồ nước", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmDongHo());
        }

        private void itemNuocNong_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức nước nóng", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmDinhMuc());
        }

        private void itemNuocNong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nước nóng", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmManager());
        }

        private void itemNuocSinhHoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nước sinh hoạt", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.NuocSinhHoat.frmManager());
        }

        private void itemEmail_Config_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình Email", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Marketing.Mail.Config.frmManager());
        }

        private void itemEmail_SettingStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt Email nhận", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Marketing.Mail.frmMailStaff());
        }

        private void itemEmail_Category_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chủ đề email marketing", "Xem"); SetPhanQuyen(e);
            var frm = new LandSoftBuilding.Marketing.Mail.Templates.frmCategory();
            frm.ShowDialog();
        }

        private void itemEmail_Templates_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu email marketing", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Marketing.Mail.Templates.frmManager());
        }

        private void itemReport_PhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Reports.frmPhieuThu());
        }

        private void itemReport_PhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thi", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Reports.frmPhieuChi());
        }

        private void itemReport_PhieuKhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thống kê khấu trừ", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Reports.frmKhauTru());
        }

        private void itemNhomKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhóm khách hàng", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new KhachHang.frmNhomKH());
        }

        private void itemCaiDatDuyetHoaDon_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cài đặt duyệt hóa đơn", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new ToaNha.frmCaiDatDuyetHoaDon());
        }

        private void itemQuanHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách quan hệ nhân khẩu", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.NhanKhau.frmQuanHe());
        }

        private void timerReminder_Tick(object sender, EventArgs e)
        {
            timerReminder.Stop();
            try
            {
                var ltLLV = db.getArlam(Common.User.MaNV).ToList();
                foreach (var l in ltLLV)
                {
                    var infoText = string.Format(" {0}.\n - Khách hàng: {1}.\n - Thời gian: {2: hh:mm tt | dd/MM/yyyy}.",
                        l.TieuDe, l.TenKH, l.NgayBD);
                    var infoCaption = l.FormID == 1 ? "Nhiệm vụ" : l.FormID == 2 ? "Lịch hẹn" : "Thông báo yêu cầu";
                    AlertInfo info = new AlertInfo(infoCaption, infoText, infoText, Properties.Resources.Alarm_clock);
                    info.Tag = string.Format("{0}|{1}", l.FormID, l.LinkID);
                    alctAltert.Show(this, info);
                    try
                    {
                        if (l.Rings != "")
                        {
                            MusicCls.FileName = Application.StartupPath + "\\Musics\\" + l.Rings;
                            MusicCls.Play();
                        }
                    }
                    catch { }
                }
            }
            catch { }
            timerReminder.Start();
        }

        private void alctAltert_AlertClick(object sender, AlertClickEventArgs e)
        {

        }

        private void alctAltert_ButtonClick(object sender, AlertButtonClickEventArgs e)
        {
            try
            {
                if (e.ButtonName == "Stop" || e.ButtonName == "Start")
                {
                    var str = e.Info.Tag.ToString().Split('|');
                    switch (int.Parse(str[0]))
                    {
                        case 1:
                            var objNV = db.NhiemVu_tnNhanViens.Single(p => p.MaNVu == int.Parse(str[1]) & p.MaNV == Common.User.MaNV);
                            objNV.DaNhac = e.ButtonName == "Stop";
                            db.SubmitChanges();
                            break;
                        case 2:
                            var objLHNV = db.LichHen_tnNhanViens.Single(p => p.MaLH == int.Parse(str[1]) & p.MaNV == Common.User.MaNV);
                            objLHNV.DaNhac = e.ButtonName == "Stop";
                            db.SubmitChanges();
                            break;
                        case 3:
                            var objTB = db.tnycThongBaos.Single(p => p.ID == int.Parse(str[1]) & p.MaNV == Common.User.MaNV);
                            objTB.DaNhac = e.ButtonName == "Stop";
                            db.SubmitChanges();
                            break;
                    }

                    if (e.ButtonName == "Stop")
                    {
                        e.Button.Name = "Start";
                        e.Button.Image = Properties.Resources.play;
                        e.Button.Hint = "Nhắc lại";
                    }
                    else
                    {
                        e.Button.Name = "Stop";
                        e.Button.Image = Properties.Resources.stop1;
                        e.Button.Hint = "Không nhắc lại";
                    }
                }
                else
                {
                    if (e.ButtonName == "Close")
                        e.AlertForm.Close();
                    else
                        MusicCls.Close();
                }
            }
            catch { }
        }

        private void alctAltert_FormClosing(object sender, AlertFormClosingEventArgs e)
        {
            try
            {
                MusicCls.Close();
            }
            catch { }
        }

        private void itemYeuCau_CaiDat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cài đặt yêu cầu", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.YeuCau.frmCatDat());
        }

        private void itemNguonDen_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nguồn đến", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.YeuCau.frmNguonDen());
        }

        private void itemLeTan_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khách ra vào Dự án", "Thêm"); SetPhanQuyen(e);
            var frm = new DichVu.YeuCau.LeTan.frmLeTanNew();
            frm.MaTN = Common.User.MaTN;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                this.LoadForm(new DichVu.YeuCau.LeTan.frmManager());
            //var frm = new DichVu.YeuCau.LeTan.frmEdit();
            //frm.MaTN = Common.User.MaTN;
            //frm.ShowDialog();
            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //    this.LoadForm(new DichVu.YeuCau.LeTan.frmManager());
        }

        private void itemLenTan_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách ra vào Dự án", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.YeuCau.LeTan.frmManager());
        }

        private void itemChoThue_LTT_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch thanh toán cho thuê", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Lease.PaymentSchedule.frmSchedule());
        }

        private void itemDienDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Dien.DieuHoa.frmManager());
        }

        private void itemDienDieuHoa_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức điện", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Dien.DieuHoa.frmDinhMuc());
        }

        private void itemReport_KeHoachThuTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo kế hoạch thu tiền", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Lease.Reports.frmKeHoachThuTien());
        }


        private void itemDanhSachLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch hẹn", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.LichHen.frmManager());
        }

        private void itemThoiDiemLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thời điểm", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.LichHen.ThoiDiem_frm());
        }

        private void itemPhanLoaiLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chủ đề", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.LichHen.ChuDe_frm());
        }

        private void itemPhanLoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loiaj nhiệm vụ", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.NhiemVu.LoaiNhiemVu_frm());
        }

        private void itemTrangThai_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tình trạng", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.NhiemVu.TinhTrang_frm());
        }

        private void itemMucDo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mức độ", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.NhiemVu.MucDo_frm());
        }

        private void itemTienDo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tiến độ", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.NhiemVu.frmTienDo());
        }

        private void itemThemMoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới nhiệm vụ", "Thêm"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.NhiemVu.AddNew_frm());
        }

        private void itemDanhSach_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhiệm vụ", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.WorkSchedule.NhiemVu.frmManager());
        }

        private void itemTaiLieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách biểu mẫu tài liệu", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmManager() { objnhanvien = User });
        }

        private void itemTaiLieu_Loai_ItemClick(object sender, ItemClickEventArgs e)
        {
            //LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmLoaiBieuMau() { objnhanvien = User });
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại biểu mẫu", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmGroup());
        }

        private void itemPhanQuyenBaoCao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân quyền báo cáo", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Report.frmPhanQuyenBaoCao());
        }
        private void itemBaoCaoHDThueGianHangThueKHTHue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo hợp đồng thuê", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.ReportsTT.frmManager());
        }

        private void itemGanHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê gần hết hạn", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.GanHetHan.frmManager());
        }

        private void itemDaHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê đã hết hạn", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.DaHetHan.frmManager());
        }

        private void itemBaoCaoTienVeCongTy_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemHoaDonDaXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn đã xóa", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.frmHoaDonDaXoa());
        }

        private void itemPhieuThuDaXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu đã xóa", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Input.frmPhieuThuDaXoa());
        }

        private void itemTheXeDaXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe đã xóa", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.frmTheXeDaXoa());
        }

        private void itemBaoCaoCongNoTongHopTheoThang_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemQuocTich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách quốc tịch của nhân khẩu", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.NhanKhau.frmQuocTich());
        }

        private void itemLoaiYeuCau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại yêu cầu lễ tân", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.frmLoaiYeuCau());
        }

        private void itemMucDoLeTan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mức độ yêu cầu lễ tân", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.LeTan.frmMucDo());
        }

        private void itemTrangThaiLeTan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái lễ tân", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.LeTan.frmTrangThaiLeTan());
        }

        private void itemTheThangMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ tháng máy", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.ThangMay.frmManager() { objnhanvien = User });
        }

        private void itemTheTichHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ tích hợp", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.TheTichHop.frmManager() { objnhanvien = User });
        }

        private void itemKhoThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kho thẻ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.frmKhoThe());
        }

        private void itemDSCapThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cấp thẻ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.frmDSCapThe());

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thoát phần mềm", "Thoát");
        }

        private void itemNhatKyHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhật ký hệ thống", "Xem");
            CreateFormControl(itemNhatKyHeThong.Name, itemNhatKyHeThong.Caption);
            LoadForm(new Building.SystemLog.ctlSysLog());
        }

        private void itemLoaiKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new KhachHang.frmLoaiKH());
        }

        private void itemConfigPhone_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình tổng đài", "Cấu hình");
            //var frm = new DIP.SwitchBoard.Setting();
            //frm.ShowDialog();
        }

        private void itemNhatKyCuocGoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhật ký cuộc gọi", "Xem");  SetPhanQuyen(e);   
            //LoadForm(new DIP.SwitchBoard.frmHistoryControl());
        }

        private void bbiDanhSachYeuCau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu của khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.frmManager());
        }

        private void bbiCongViecCuaToi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công việc của tôi", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.frmCongViecNhanVien());
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            this.LoadForm(new EmailAmazon.Templates.frmManager());
        }

        private void itemDanhSachNhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhận mail", "Xem"); SetPhanQuyen(e);
            LoadForm(new EmailAmazon.Receive.frmReceive());
        }

        private void itemThuongHieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Thương hiệu mail amazon", "Xem"); SetPhanQuyen(e);
            LoadForm(new EmailAmazon.Brand.frmManager());
        }

        private void itemDanhSachGui_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Gửi mail", "Xem"); SetPhanQuyen(e);
            LoadForm(new EmailAmazon.Sending.frmManager());
        }

        private void itemCheckTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            MailCommon.cmd = new EmailAmazon.API.APISoapClient();
            MailCommon.cmd.Open();
            DialogBox.Alert(string.Format("Tài khoản của bạn hiện có {0} email", (object)MailCommon.cmd.GetTaiKhoan_SoDu(MailCommon.MaHD, MailCommon.MatKhau)));
            //DialogBox.Alert(string.Format("{0}", (object)MailCommon.cmd.CheckTaiKhoan(MailCommon.MaHD, MailCommon.MatKhau)));
        }

        private void barButtonItem29_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Marketing.Mail.Config.LichSu());
        }

        private void itemCapNhatBangGiaDichVuCoBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new ToaNha.frmCapNhatBangGiaDichVu());
        }

        private void itemAppConfig_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            var frm = new Building.AppVime.frmConfig();
            frm.ShowDialog();
        }

        private void itemTowerSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new Building.AppVime.Tower.frmManager());
        }

        private void itemRegisterEmployeeAPP_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new Building.AppVime.Employee.frmManager());
        }

        private void itemRegisterAPP_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new Building.AppVime.frmManager());
        }

        private void itemVimeNewsGenereal_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new Building.AppVime.NewsGeneral.frmManager());
        }

        private void itemAppNews_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new Building.AppVime.News.frmManager());
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new QuangForm());
        }

        private void itemServiceExtension_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new Building.AppVime.ServiceExtension.frmManager());
        }

        private void itemApp_SettingService_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new Building.AppVime.Services.frmManager());
        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tài liệu", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.KhachHang.frmTaiLieu() { objnhanvien = User });
        }

        private void itemTienIch_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tiện ích", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.AppVime.ServiceBasic.frmManager());
        }

        private void itemHDCTNSapHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.frmGanHetHan());
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thiết lập loại dịch vụ khấu trừ tự động", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmDichVuKhauTruTuDong());
        }

        private void itemCongNoDongTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Công nợ theo dõi dòng tiền mặt", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.frmManagerCongNoToaNha());
        }

        private void itemChayLaiSoQuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            //Library.frmChayLaiSoQuy_ThuChi frm = new frmChayLaiSoQuy_ThuChi();
            //frm.ShowDialog();
        }

        private void itemDSChuyenTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chuyển tiền", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Transfer.frmManager());
        }

        private void itemPhieuChiKyQuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu chi tiền ký quỹ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Input.frmManager_TienKyQuy());
        }

        private void itemThongKeTheoNhomCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê phản ánh của cư dân theo nhóm công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmNhomCongViec());
        }

        private void itemBDTinhTrangXuLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê phản ánh của cư dân theo tình trạng xử lý", "Xem");
            SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmTinhTrangXuLy());
        }

        private void itemTheoDanhGiaCuaCuDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê theo đánh giá của cư dân", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmDanhGiaSao());
        }

        private void itemTheoNguonPhanAnh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê theo nguồn phản ánh", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmNguonDen());
        }

        private void itemDoUuTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê theo độ ưu tiên", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmDoUuTien());
        }

        private void itemPhanAnhTheoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê phản ánh theo tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmPhanAnhTheoToaNha());
        }

        private void itemNhomCongViecMuiltiTN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê nhóm công việc theo tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmNhomCongViecMuiltiTN());
        }

        private void itemTinhTrangXuLyTheoNhieuToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tình trạng xử lý theo tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmTinhTrangXuLyMuiltiTN());
        }

        private void itemDanhGiaCuaCuDanTheoMuiltiToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê đánh giá của cư dân theo tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmDanhGiaSaoMuiltiTN());
        }

        private void itemNguonTiepNhanTheoMuilti_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê nguồn tiếp nhận theo tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmNguonTiepNhanMuiltiTN());
        }

        private void itemViewBieuDoAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê nguồn tiếp nhận theo tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmViewAll());
        }

        private void itemTTTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tình trạng tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmTinhTrangTaiSan());
        }

        private void itemNhaCCTS_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhà cung cấp tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmNhaCungCapTaiSan());
        }

        private void itemDMHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Hệ thống tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmNhomTaiSan());
        }

        private void itemDMLoaiTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmLoaiTaiSan());
        }

        private void itemDMTenTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tên tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmTenTaiSan());
        }

        private void ItemCaiDatHeThongChoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt hệ thống", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmNhomTaiSan_CaiDat());
        }

        private void itemDMChiTietTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Chi tiết tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmListTaiSan());
        }

        private void ItemTanSuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tần suất", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmTanSuat());
        }

        private void itemNhomCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmNhomCongViec());
        }

        private void itemLoaiCaTruc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục Loại ca trực", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.PhanCong.frmPhanCong_PLCV());
        }

        private void itemCongCuThietBi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Công cụ thiết bị", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmThietBi_Manager());
        }

        private void itemProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Profile", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoTri.frmProfile_Manager());
        }

        private void itemGiaoNhanCa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Bàn giao ca", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.PhanCong.frmBanGiaoCa_Manager());
        }

        private void itemDeXuatDoiCa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đề xuất đổi ca", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.PhanCong.frmDeXuatDoiCa_Manager());
        }

        private void itemLichTruc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch trực", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.PhanCong.frmLichTruc_Manager());
        }

        private void itemBangPhanCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bảng phân công", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.PhanCong.frmPhanCong());
        }

        private void ItemKeHoachVanHanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách vận hành", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VanHanh.frmKeHoachVanHanh_Manager());
        }

        private void itemPhieuVanHanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu vận hành", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VanHanh.frmPhieuVanHanh_Manager());
        }

        private void itemVHKeHoachBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kế hoạch bảo trì", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoTri.frmKeHoachBaoTri_Manager());
        }

        private void itemVHPhieuBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu bảo trì", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoTri.frmPhieuBaoTri_Manager());
        }

        private void itemGanProFileChoHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Gán profile cho hệ thống tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoTri.frmGanProfileChoHeThong());
        }

        private void itemDanhMucProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Profile Hệ thống", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoTri.frmProfile_DM_Manager());
        }

        private void itemGanProfileChoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Profile gán cho tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoTri.frmProfile_GanChoToaNha());
        }

        private void itemNhomProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm profile", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmProfileGroup());
        }

        private void itemCauHinhCapDuyet_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình cấp duyệt", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmManagerCauHinhDuyet());
        }

        private void itemVTDonViTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục Đơn Vị Tính", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmDVT());
        }

        private void itemVTDanhMucVatTu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục Vật tư", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmVatTu_Manager());
        }

        private void itemDeXuatMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đề xuất", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VatTu.frmDeXuat_Manager());
        }

        private void itemDanhSachMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mua hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VatTu.frmMuaHang_Manager());
        }

        private void itemVTNhapKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhập kho", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VatTu.frmNhapKho_Manager());
        }

        private void itemVTDanhMucLoaiNhapKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục loại nhập kho", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmLoaiNhap());
        }

        private void itemVTDanhMucKhoHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục kho hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmKhoHang());
        }

        private void itemVTDanhMucLoaiXuatKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mục đích xuất kho", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmLoaiXuat());
        }

        private void itemVTXuatKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách xuất kho", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VatTu.frmXuatKho_Manager());
        }

        private void itemVTTonKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tồn kho", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VatTu.frmTonKho_Manager());
        }

        private void itemCauHinhAPITheXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình API thẻ xe", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.frmConfigAPI());
        }

        private void itemQuanLyHoSo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Quản lý hồ sơ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.HoSo.FrmHoSoManager());
        }

        private void itemQLHS_KhoGiay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Khổ giấy", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmKhoGiay());
        }

        private void itemQLHS_HoSo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục hồ sơ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmDanhMucHoSo());
        }

        private void itemQLHS_DayKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục dãy - kệ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmKhoDayKe());
        }

        private void itemQLHS_MucDoBaoMat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mức độ bảo mật", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmMucDoBaoMat());
        }

        private void itemQLHS_MucDoKhanCap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mức độ khẩn cấp", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmDoKhanCap());
        }

        private void itemQLHS_LoaiVanBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân loại văn bản", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmLoaiVanBan());
        }

        private void itemXuatKhoSuDung_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách xuất kho sử dụng", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.VatTu.frmXuatKhoSuDung_Manager());

        }

        private void itemDeXuatSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đề xuất sửa chữa", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoTri.frmDeXuatSuaChua_Manager());
        }

        private void itemCauHinhNgayNghi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình danh mục ngày nghỉ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmThietLapNgayNghi());
        }

        private void itemCauHinhNgayNghiTheoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình ngày nghỉ theo tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmThietLapNgayNghi_TheoToaNha());
        }

        private void itemThongKeThoiGian_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê thời gian", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.BieuDo.ThoiGian.frmBieuDoThoiGian());
        }

        private void itemNhomCongViec_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.FrmAppGroupProcess());
        }

        private void itemCauHinhTinhTrangPhieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm trạng thái phiếu vận hành ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.DanhMuc.frmStatusLevels());
        }

        private void itemTKCheckList_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo checklist hằng ngày ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoCao.frmBaoCao_CheckListVanHanhHangNgay());
        }

        private void itemTKTinhHinhThucHien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tình hình công việc ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.frmTinhHinhCongViec());
        }

        private void itemTKTinhHinhKiemTraDinhKy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tình hình kiểm tra định kỳ ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoCao.frmTinhHinhKiemTraDinhKy());
        }

        private void itemViewKeHoachBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("View kiểm tra định kỳ ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoCao.frmKiemTraDinhKy());
        }

        private void itemTinTuc_TyLeDangTin_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức tỷ lệ đăng tin ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TyLeDangTin());
        }

        private void itemTinTuc_ThongKeTheoTungToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức theo từng tòa nhà ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TinTucTungToaNha());
        }

        private void itemTinTuc_ThongKeTheoTongToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức theo tổng tòa nhà ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TinTucToaNha());

        }

        private void itemTinTuc_ThongKeTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức theo tổng hợp ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TinTucTong());
        }

        private void itemApp_TyLeSuDungTheoTungToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tỷ lệ sử dụng từng tòa của App ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TyLeSuDungTungToaNha());
        }

        private void itemApp_TyLeSuDungTongToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tỷ lệ sử dụng tổng của App ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TyLeSuDungTong());
        }

        private void itemApp_ThongKeSuDungTungToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê sử dụng từng tòa của App ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_ThongKeSuDungTungToaNha());
        }

        private void itemApp_ThongKeSuDungCacToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê sử dụng các tòa của App ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_ThongKeSuDungCacToaNha());
        }

        private void itemNhieuTN_SoLuongNhomCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê Số lượng phản ánh theo nhóm công việc ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_CountNhomCongViec());
        }

        private void itemNhieuTN_PhanAnhTheoThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê phản ánh theo tháng ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_ThongKePhanAnhTheoThang());
        }

        private void itemNhieuTN_TongPhanAnh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê tổng phản ánh ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BieuDo.frmBieuDoTongPhanAnh());
        }

        private void itemNhieuTN_BaoCaoTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê tổng hợp ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoCao.frmBaoCaoVanHanh());
        }

        private void itemViewKeHoachBaoTri_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("View kế hoạch bảo trì ", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.BaoCao.frmViewKeHoachBaoTri());
        }

        private void itemSMSTruongTron_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định nghĩa trường trộn ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Marketing.SMS.frmFields());
        }

        private void itemSMSSoDuTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Số dư tài khoản ", "Xem"); SetPhanQuyen(e);
            LandSoftBuilding.Marketing.sms.frmMoney frm = new LandSoftBuilding.Marketing.sms.frmMoney();
            frm.ShowDialog();
        }

        private void itemSMS_Mau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định nghĩa mẫu SMS ", "Xem"); SetPhanQuyen(e);
            LandSoftBuilding.Marketing.SMS.frmTemplates frm = new LandSoftBuilding.Marketing.SMS.frmTemplates();
            frm.ShowDialog();
        }

        private void itemSMSLichSu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch sử gửi SMS ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Marketing.sms.frmHistory());
        }

        private void BtnCauHinhColor_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình Color dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.FrmCaiDatColor());
        }

        private void ItemCaiDatBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình biểu mẫu", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmManager());
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái xác nhận", "Xem");  SetPhanQuyen(e);   
            //LoadForm(new DichVu.BanGiaoMatBang.FrmScheduleConfirm());
        }

        private void ItemScheduleStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái gửi lịch", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmScheduleStatus());
        }

        private void ItemScheduleComfirm_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái xác nhận", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmScheduleConfirm());
        }

        private void ItemScheduleGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm lịch bàn giao", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmScheduleGroup());
        }

        private void ItemSchedule_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thiết lập lịch bàn giao", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmSchedule());
        }

        private void itemKeHoachBanGiao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kế hoạch bàn giao mặt bằng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmPlan());
        }

        private void ItemScheduleComfirms_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái xác nhận", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmScheduleConfirm());
        }

        private void itemHandoverChecklistQuality_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemHandoverChecklistNoQuality_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemHandoverHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch sử bàn giao", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHistoryLocal());
        }

        private void itemCarTyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại xe", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Xe.frmLoaiXe());
        }

        private void itemCarSetup_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình phương tiện di chuyển", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Xe.frmCauHinhLX());
        }

        private void itemCarSchedule_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình lịch di chuyển", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Xe.frmCauHinhLichDC());
        }

        private void itemNewsManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Quản lý tin tức", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.TinTuc.frmTinTuc());
        }

        private void itemNewsTyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại tin tức", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.TinTuc.frmLoaiTinTuc());
        }

        private void itemEmailSetup_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt gửi email", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.Email.FrmEmailSetup());
        }

        private void itemBcThuChiTm_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Sổ kế toán chi tiết quỹ tiền mặt", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Reports.FrmBcThuChiTm());
        }

        private void itemBcCongNoDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemBaoCaoDaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đã thu", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Reports.FrmBcDaThu());
        }

        private void ItemBcChiTietThuPqlThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết PQL theo tháng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBcChiTietThuPqlThang());
        }

        private void itemHoaDonThongKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ dịch vụ", "Xem"); SetPhanQuyen(e);
            ////LoadForm(new LandSoftBuilding.Receivables.Reports.frmCongNo());
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBcCongNo());
        }

        private void ItemReportPhiDaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BCCT các khoản đã thu dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Reports.frmPhieuThu());
        }

        private void ItemBaoCaoTongHopCongNoDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp công nợ dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBcCongNoDichVu());
        }

        private void ItemBcCongNoTongHopTheoThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp công nợ theo tháng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.Reports.frmCongNoTrongThang());
        }

        private void ItemReportChiTietCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("In sổ chi tiết công nợ", "Xem");  SetPhanQuyen(e);   
            //var frm = new Building.PrintControls.PrintForm();
            //frm.Text = "Sổ chi tiết công nợ";
            //frm.PrintControl.FilterForm = new LandSoftBuilding.Receivables.Reports.frmChiTietCongNo();
            //this.LoadForm(frm);
        }

        private void ItemBaoCaoCacKhoanDaKhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC các khoản đã khấu trừ", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Reports.frmKhauTru());
        }

        private void ItemReportBaoCaoTienDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thu chi tiền điện", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.frmBaoCaoThuChiTienDien());
        }

        private void ItemRreportDienDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thu chi tiền điện điều hòa ngoài giờ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.frmBaoCaoThuChiTienDieuHoaNG());
        }

        private void ItemBieuDoTieuThuDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ điện", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Dien.BieuDo.frmBieuDoTieuThuDien());
        }

        private void ItemBieuDoTieuThuDien3Pha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ điện 3 pha", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Dien.BieuDo.frmBieuDoTieuThuDien3Pha());
        }

        private void itemBieuDoTieuThuDienDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ điện điều hòa", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Dien.BieuDo.frmBieuDoTieuThuDienDH());
        }

        private void itemBaoCaoDichVuNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thu chi tiền nước", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.frmBaoCaoThuChiTienNuoc());
        }

        private void itemBieuDoTieuThuNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ nước", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.frmBieuDoMucTieuThuNuoc());
        }

        private void itemBieuDoTieuThuNuocNong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ nước nước nóng", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmBieuDoMucTieuThuNuocNong());
        }

        private void itemBieuDoTieuThuNuocSinhHoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ nước sinh hoạt", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Nuoc.NuocSinhHoat.frmBieuDoMucTieuThuNuocSinhHoat());
        }

        private void itemBaoCaoDichVuGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo dịch vụ Gas", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Gas.Reports.frmGAS());
        }

        private void itemBieuDoTieuThuGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ Gas", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.Gas.Reports.frmBieuDoGas());
        }

        private void itemBaoCaoDichVuGuiXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tiền gửi xe", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.Reports.frmTheXe());
        }

        private void itemSoCongNoTheXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ thẻ xe", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new DichVu.GiuXe.Reports.frmCongNoTheXe());
        }

        private void itemBaoCaoDaThuTheXePhatSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đã thu thẻ xe", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.GiuXe.Reports.frmDaThuTheXe());
        }

        private void itemBaoCaoChiTietPhiGiuXeThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe tháng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuXeThang());
        }

        private void itemBaoCaoDoanhThuGiuXeOTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe Oto", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuOtoThang());
        }

        private void itemBaoCaoDoanhThuGiuXeMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe máy", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuXeMayThang());
        }

        private void itemBaoCaoDoanhThuGiuXeDap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe đạp", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuXeDapThang());
        }

        private void itemCauHinhNhanVienQuanLyNhanMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình nhân viên quản lý nhận mail dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.FrmNhanVienNhanEmail());
        }

        private void itemDrTyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại đặt cọc", "Xem"); SetPhanQuyen(e);
            LoadForm(new Deposit.FrmDrTyle());
        }

        private void ItemDeposit_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu thu tiền phạt", "Xem"); SetPhanQuyen(e);
            LoadForm(new Deposit.PhieuThuTienPhat.FrmDeposit());
        }

        private void itemBaoCaoCongNoDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ dịch vụ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmCongNoDichVu());
        }

        private void itemDepositDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu thu đặt cọc đã xóa", "Xem"); SetPhanQuyen(e);
            LoadForm(new Deposit.FrmDepositDelete());
        }

        private void itemDepositManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu thu đặt cọc đã xóa", "Xem"); SetPhanQuyen(e);
            LoadForm(new Deposit.PhieuThuDatCoc.FrmDepositManager());
        }

        private void ItemWithDraw_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(Deposit.Class.Enum.FormName.PHIEU_CHI_HOAN_TIEN, "Xem"); SetPhanQuyen(e);
            LoadForm(new Deposit.PhieuChi.FrmWithDraw() { FormName = Deposit.Class.Enum.FormName.PHIEU_CHI_HOAN_TIEN });
        }

        private void ItemSoQuyDatCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Sổ quỹ đặt cọc", "Xem"); SetPhanQuyen(e);
            LoadForm(new Deposit.Report.FrmBcThuChiTmDc());
        }

        private void itemHangMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục hạng mục", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmChecklistDetails());
        }

        private void itemChecklist_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu checklist", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmCheckList());
        }

        private void ItemChecklistToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Checklist Tòa nhà", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmBuildingChecklist());
        }

        private void ItemHandoverLocal_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bàn giao nội bộ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHandover());
        }

        private void ItemUserHandover_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt nhân viên bàn giao", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.UserHandover.FrmUserHandover());
        }

        private void ItemPlanCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kế hoạch bàn giao khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Customer.FrmPlan());
        }

        private void ItemScheduleCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch bàn giao khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Customer.FrmSchedule());
        }

        private void ItemHandoverCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bàn giao khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Customer.FrmHandover());
        }

        private void ItemDuty_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Ca bàn giao", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Category.FrmDuty());
        }

        private void itemStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái bàn giao", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Category.FrmStatus());
        }

        private void ItemHistoryCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch sử bàn giao khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHistoryCustomer());
        }

        private void ItemAssetGroups_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Asset.FrmAssetGroup());
        }

        private void ItemAssetCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Asset.FrmAssetCategory());
        }

        private void ItemUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đơn vị tính", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Asset.FrmUnit());
        }

        private void ItemStatusAsset_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tình trạng tài sản", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Asset.FrmStatusAsset());
        }

        private void ItemGroupChecklist_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmGroupItem());
        }

        private void ItemBlockChecklist_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tầng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmBlockItem());
        }

        private void itemDeXuatThayCa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đề xuất thay ca", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.Asset.PhanCong.frmDeXuatThayCa_Manager());
        }

        private void itemNhomMB_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm mặt bằng", "Xem"); SetPhanQuyen(e);
            LoadForm(new MatBang.frmNhomMatBang());
        }

        private void itemDienTichLapDay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.MatBang.frmDienTichSuDung());
        }

        private void itemBieuMauHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu hợp đồng thuê", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.Mau.frmManager());
        }

        private void itemTruongTronHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu hợp đồng thuê", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.Mau.frmFieldDefine());
        }

        private void itemMauHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu hợp đồng thuê new", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.MauNew.Template());
        }

        private void itemLapHoaDonHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            LandSoftBuilding.Receivables.frmAddAutoHDT FRM = new LandSoftBuilding.Receivables.frmAddAutoHDT();
            FRM.IsHDThue = true;
            FRM.ShowDialog();
        }

        private void itemHoaDonHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn dịch vụ khách hàng HĐT", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.HopDongThue.frmManagerHDT(), "Hóa đơn dịch vụ - Hợp đồng thuê");
        }

        private void itemHoaDonDaXoaHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn đã xóa", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Receivables.frmHoaDonDaXoaHDT(), "Hóa đơn đã xóa - Hợp đồng thuê");
        }

        private void itemCongNoHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ tổng hợp theo khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.frmReceivablesHDT() { IsHDThue = true }, "Công nợ tổng hợp theo khách hàng - Hợp đồng thuê");
        }

        private void itemPhieuThuHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu HĐT", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Input.HopDongThue.frmManagerHDT(), "Phiếu thu - Hợp đồng thuê ");
        }

        private void itemPhieuThuDaXoaHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu đã xóa HĐT", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Input.frmPhieuThuDaXoaHDT(), "Phiếu thu đã xóa - Hợp đồng thuê");
        }

        private void itemPhieuChiHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu chi", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Fund.Output.PhieuChi.frmManagerHDT(), "Phiếu chi - Hợp đồng thuê");
        }

        private void itemMoneyPurpose_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kinh phí cả năm", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.KinhPhiDuKien.FrmMoneyPurpose(), "Kinh phí cả năm");
        }

        private void itemMoneyPurposeItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kinh phí hạng mục", "Xem"); SetPhanQuyen(e);
            this.LoadForm(new Building.KinhPhiDuKien.FrmMoneyPurposeItems(), "Kinh phí hạng mục");
        }

        private void itemDuAn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Dự án", "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmToaNha() { objNV = User });
        }

        private void itemHopDongDatCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(Deposit.Class.Enum.FormName.HOP_DONG_DAT_COC, "Xem"); SetPhanQuyen(e);
            LoadForm(new Deposit.HopDong.FrmHopDong());
        }

        private void ItemKhachHangTiemNang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cơ hội khách hàng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.KhachHang.CoHoi.frmManager(), "Cơ hội khách hàng");
        }

        private void itemDanhSachCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.DanhSachCongViec.FrmDanhSachCongViec(), "Danh sách công việc");
        }

        private void itemHopDongThueNgoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê ngoài", "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.FrmManager(), "Hợp đồng thuê ngoài");
        }

        private void itemDeXuatDoiLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đề xuất đổi lịch", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Customer.FrmDeXuatDoiLich());
        }

        private void itemDanhSachThanhCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bàn giao nội bộ thành công", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHandoverSusccess());
        }

        private void itemDanhSachSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bàn giao nội bộ sửa chữa", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHandoverNoSuccess());
        }

        private void itemTrangThaiDuyetDeXuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái duyệt đề xuất", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Category.FrmDeXuatDoiLichStatus());
        }

        private void itemTieuDeThongBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tiêu đề thông báo", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Category.FrmNotifycationTitle());
        }

        private void itemNoiDungThongBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nội dung thông báo", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.BanGiaoMatBang.Category.FrmNotifycationContent());
        }

        private void itemCongNoTienDatCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê công nợ đặt cọc", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.Reports.frmCongNoDatCoc());
        }

        private void itemHopDongThueNgoaiDaHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Hợp đồng thuê ngoài hết hạn", "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.frmDaHetHan());
        }

        private void itemHopDongThueNgoaiGanHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Hợp đồng thuê ngoài gần hết hạn", "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.frmGanHetHan());
        }

        private void itemThanhLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thanh lý hợp đồng thuê ngoài", "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.frmThanhLy());
        }

        private void itemNoiDungCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nội dung công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.DanhMuc.FrmWorkContent());
        }

        private void itemNoiDungNhomCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nội dung nhóm công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.DanhMuc.FrmWorkGroupContent());
        }

        private void itemTruongTron_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nội dung nhóm công việc", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.frmFieldDefine());
        }

        private void itemTruongTronHdtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trường trộn hợp đồng thuê ngoài", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.frmFieldDefine() { GroupId = Library.BieuMauCls.BieuMauConst.NHOM_HOP_DONG_THUE_NGOAI }, "Trường trộn HDTN");
        }

        private void itemBieuMauHdtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu mẫu HDTN", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmManager() { GroupId = Library.BieuMauCls.BieuMauConst.NHOM_HOP_DONG_THUE_NGOAI }, "Biểu mẫu HDTN");
        }

        private void btnPhanQuyenBieuMauHdtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân quyền biểu mẫu Hdtn", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Report.frmPhanQuyenBaoCao() { GroupId = Library.BieuMauCls.BieuMauConst.NHOM_HOP_DONG_THUE_NGOAI }, "Phân quyền biểu mẫu Hdtn");
        }

        private void itemCongNoDoiTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmShowHaveBuilding() { FormTemplateId = BuildingDesignTemplate.Class.Common.ReportIndex.BIEU_MAU_CONG_NO_NCC }, e.Item.Caption);
        }

        private void itemBaoCaoThongKeKinhPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê kinh phí dự kiến", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmShowHaveBuilding() { FormTemplateId = BuildingDesignTemplate.Class.Common.ReportIndex.THONG_KE_KINH_PHI_DU_KIEN }, "Thống kê kinh phí dự kiến");
        }

        private void itemCongNoTongHopHdtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.Report.FrmCongNoTongHop(), e.Item.Caption);
        }

        private void itemLichThanhToan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.LichThanhToan.FrmLichThanhToan());
        }

        private void itemTienTrinhThucHien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new HopDongThueNgoai.LichSuTienTrinh.FrmLichSuThucHien());
        }

        private void itemDanhGiaHdtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);

            LoadForm(new HopDongThueNgoai.DanhGia.FrmDanhGiaCongViec());
        }

        private void ItemPhanQuyenBieuDoMain_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");  //SetPhanQuyen(e);   
            LoadForm(new Building.PhanQuyenBieuDo.FrmBieuDoSetting());
        }

        private void itemCapNhatItemMain_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SetPhanQuyen(e); 
            Library.HeThongCls.PhanQuyenMain.UpdateRibbon(this, itemBoPhanNhanEmail);
        }

        private void ItemPhanQuyenItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");  //SetPhanQuyen(e);   

            LoadForm(new Building.PhanQuyenItemMain.FrmPhanQuyenItem(), e.Item.Caption);
        }

        private void itemCoPhanQuyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            FlagIsPhanQuyen = !FlagIsPhanQuyen;
            Library.HeThongCls.PhanQuyenCls.SetIsPhanQuyen(FlagIsPhanQuyen);

            this.itemCoPhanQuyen.Caption = FlagIsPhanQuyen ? "Đang phân quyền" : "Chưa phân quyền";
            this.itemCoPhanQuyen.ImageOptions.LargeImage = FlagIsPhanQuyen ? global::LandSoftBuildingMain.Properties.Resources.icons8_genealogy_filled_50px : global::LandSoftBuildingMain.Properties.Resources.icons8_genealogy_50px;
            this.itemCoPhanQuyen.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.itemCoPhanQuyen.ItemAppearance.Normal.Options.UseFont = FlagIsPhanQuyen;
        }

        private void itemTruongTronDatCocThiCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.frmFieldDefine { GroupId = Library.BieuMauCls.BieuMauConst.NHOM_DAT_COC_THI_CONG }, e.Item.Caption);
        }

        private void itemBieuMauDatCocThiCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmManager() { GroupId = Library.BieuMauCls.BieuMauConst.NHOM_DAT_COC_THI_CONG }, e.Item.Caption);
        }

        private void itemPhanQuyenBieuMauDatCocThiCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Report.frmPhanQuyenBaoCao() { GroupId = Library.BieuMauCls.BieuMauConst.NHOM_DAT_COC_THI_CONG }, e.Item.Caption);
        }

        private void itemSoDuDauKy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.DauKy.FrmDauKy(), e.Item.Caption);
        }

        private void itemBaoCaoCongNoAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBangKeCongNoAll(), e.Item.Caption);
        }

        private void itemPhiPhaiThuTrongThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmPhiPhaiThuTrongThang(), e.Item.Caption);
        }

        private void ItemNoiBanHanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Asset.DanhMuc.FrmNoiBanHanh(), e.Item.Caption);
        }

        private void itemPhanLoaiDiDen_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Asset.DanhMuc.FrmPhanLoaiDiDen(), e.Item.Caption);
        }

        private void itemVanBanDen_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Asset.HoSo.FrmVanBanDen(), e.Item.Caption);
        }

        private void itemVanBanDi_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Asset.HoSo.FrmVanBanDi(), e.Item.Caption);
        }

        private void itemChucVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new ToaNha.frmChucVu(), e.Item.Caption);
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Asset.DanhMuc.frmKhoDayKe(), e.Item.Caption);
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Asset.DanhMuc.frmKhoGiay(), e.Item.Caption);
        }

        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.SMS.Category.FrmBrandName(), e.Item.Caption);
        }

        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.SMS.Category.FrmCaiDatNhanVien(), e.Item.Caption);
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.SMS.FrmTemplate(), e.Item.Caption);
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.SMS.FrmBuilding(), e.Item.Caption);
        }

        private void itemSoDoPhanLo_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.MatBang.ViewMap.FrmViewMap5(), e.Item.Caption);
        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Help.FrmTotal(), e.Item.Caption);
        }

        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.MatBang.ViewMap.ViewMap.FrmViewMap(), e.Item.Caption);
        }

        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.MatBang.ViewMap.ViewMap.frmViewMap2(), e.Item.Caption);
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.MatBang.ViewMap.ViewMap.FrmViewMap3(), e.Item.Caption);
        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.MatBang.ViewMap.ViewMap.FrmViewMap4(), e.Item.Caption);
        }

        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.MatBang.ViewMap.ViewMap.FrmViewMap6(), e.Item.Caption);
        }

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.MatBang.ViewMap.ViewMap.frmViewMap22(), e.Item.Caption);
        }

        private void istemLichSuGuiSms_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            LoadForm(new Building.SMSZalo.frmHistory(), e.Item.Caption);
        }

        private void itemCauHinhPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            LoadForm(new Building.SMSZalo.DanhMuc.frmConfig(), e.Item.Caption);
        }

        private void itemDanhSachMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            LoadForm(new Building.SMSZalo.Mau.frmTemlate(), e.Item.Caption);
        }

        private void itemLayDanhSachKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            Building.SMSZalo.frmGetCustomer frm = new Building.SMSZalo.frmGetCustomer();
            frm.Show();
        }

        private void IiemDanhsachkhachhangquantam_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem");
            LoadForm(new Building.SMSZalo.frmCustomer(), e.Item.Caption);
        }

        private void barButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            //LoadForm(new DichVu.MatBang.ViewMap.ViewMap.FrmViewMap33(), e.Item.Caption);
        }

        private void itemDuTinhHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new LandSoftBuilding.Receivables.DuTinh.frmHopDongThue(), e.Item.Caption);    
        }

        private void itemDuTinhXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new LandSoftBuilding.Receivables.DuTinh.frmDuTinhDoanhThuXe(), e.Item.Caption);
        }

        private void itemDuTinhPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new LandSoftBuilding.Receivables.DuTinh.frmDuTinhDoanhPQL(), e.Item.Caption);
        }

        private void itemNhanVienQuanTam_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.SMSZalo.NhanVien.frmNhanVien(), e.Item.Caption);
        }

        private void itemLichSuGuiNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.SMSZalo.NhanVien.frmHistory(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Cấu hình api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem53_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            var frm = new Building.AppVime.frmConfig();
            frm.ShowDialog();
        }

        /// <summary>
        /// APP: Cấu hình page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem54_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.Tower.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Đăng ký nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.Employee.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Đăng ký khách hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Tin tức chung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.NewsGeneral.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Tin tức mỗi tòa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem58_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.News.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Dịch vụ app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem59_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.Services.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Dịch vụ cộng thêm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem60_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.ServiceExtension.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// APP: Tiện ích
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem61_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.ServiceBasic.frmManager(), e.Item.Caption);
        }

        private void itemNgheNghiep_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmNgheNghiep())
                frm.ShowDialog();
        }

        private void itemQuyMo_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmQuyMo())
                frm.ShowDialog();
        }

        private void itemNguonKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmNguonKH())
                frm.ShowDialog();
        }

        private void itemXa_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Library.Other.frmXa());
        }

        private void itemHuyen_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Library.Other.frmHuyen());
        }

        private void itemTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Library.Other.frmTinh());
        }

        private void itemHTTX_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmHinhThucTiepXuc())
                frm.ShowDialog();
        }

        private void itemNhuCauThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmNhuCauThue())
                frm.ShowDialog();
        }

        private void itemCauHinhTiemNang_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmCauHinhTiemNang())
                frm.ShowDialog();
        }

        private void itemLoaiBaoGia_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmLoaiBaoGia())
                frm.ShowDialog();
        }

        private void itemTrangThaiCSKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            using (var frm = new DichVu.KhachHang.CSKH.frmTrangThai())
                frm.ShowDialog();
        }

        private void itemCSKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.CSKH.frmManager(false), "CSKH - Danh sách khách hàng đang chăm sóc");
        }

        private void itemCSKHChinhThuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.CSKH.frmManager(false), "CSKH - Danh sách khách hàng chính thức");
        }

        private void itemTraCuuDoanhNghiep_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.CSKH.frmTraCuuDoanhNghiep());
        }

        private void itemNguoiLienHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.NguoiLienHe.ctlManager());
        }

        private void itemCoHoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DIPCRM.NhuCau.ctlManager());
        }

        private void itemBaoGia_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DIPCRM.PriceAlert.ctlManager());
        }

        /// <summary>
        /// Hóa đơn app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem62_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.CongNo.frmManager());
        }

        private void barButtonItem63_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.CongNo.frmReceivables());
        }

        /// <summary>
        /// BÁO CÁO CÔNG NỢ APP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem64_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.CongNo.FrmBangKeCongNoAll());
        }

        /// <summary>
        /// BÁO CÁO ĐÃ THU APP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem67_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.CongNo.frm_bao_cao_da_thu());
        }

        private void itemKhaoSat_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Survey.frmSurveyManager());
        }

        private void itemThongKeKhaoSat_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.Survey.frmReport());
        }

        /// <summary>
        /// BÁO CÁO CÔNG NỢ THEO TUỔI NỢ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem68_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC công nợ theo tuổi nợ", "Xem");
            SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Receivables.Reports.frmBaoCaoTuoiNo2());
        }

        /// <summary>
        /// BÁO CÁO TÍNH LÃI CHẬM NỘP PHÍ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem69_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC tính lãi chậm nộp phí", "Xem");
            SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBaoCaoTinhLaiChamNopPhi());
        }

        /// <summary>
        /// BÁO CÁO TỔNG HỢP LÃI VAY KHÁCH HÀNG 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem70_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BCTH lãi vay khách hàng", "Xem");
            SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBaoCaoTongHopLaiVay());
        }

        /// <summary>
        /// Danh sách đăng ký thi công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem71_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đăng ký thi công", "Xem");
            SetPhanQuyen(e);
            this.LoadForm(new Deposit.DangKy.frmDangKyThiCong());
        }

        private void itemPhieuKhauTruHDT_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// NHÓM DỊCH VỤ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem72_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm dịch vụ", "Xem");
            SetPhanQuyen(e);
            this.LoadForm(new Building.AppExtension.NhomDichVu.FrmManager());
        }

        private void itemVer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình App", "Version");
            SetPhanQuyen(e);
            Building.AppVime.frmVer frmVersion = new Building.AppVime.frmVer();
            frmVersion.ShowDialog();
        }

        private void btnBCDienTichChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo diện tích cho thuê", "Xem"); SetPhanQuyen(e);
            SetPhanQuyen(e);
            LoadForm(new DichVu.MatBang.frmBCTHDienTichChoThue());
        }

        private void btnBaoCaoDienNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp chỉ số điện", "Xem"); SetPhanQuyen(e);
            SetPhanQuyen(e);
            LoadForm(new DichVu.formBaoCaoTongHopDienNuoc());
        }

        private void btnBCTongHopNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp chỉ số nước", "Xem"); SetPhanQuyen(e);
            SetPhanQuyen(e);
            LoadForm(new DichVu.frmBaoCaoTongHopNuoc());
        }

        private void btnLienHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppExtension.frmChiTietLienHe());
        }

        private void btnCauHinhVAT_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new ToaNha.frmDieuChinhVAT());
        }

        private void btnBanner_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new frmQuanLyBanner());
        }

        /// <summary>
        /// ĐỒNG HỒ ĐIỆN 3 PHA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDongHoDien3Pha_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.Dien.Dien3Pha.frmDongHo());
        }

        /// <summary>
        /// Bộ phận liên hệ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemBoPhanLienHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.NguoiLienHe.frmBoPhanLienHe());
        }

        /// <summary>
        /// NHÓM LIÊN HỆ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemNhomLienHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.NguoiLienHe.frmNhomLienHe());
        }

        

        private void itemDienLanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.DienLanh.frmManager());
        }

      

        private void itemDongHoDienLanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            //LoadForm(new DichVu.DienLanh.frmDongHo(), e.Item.Caption);
        }

        private void btnchuyendo1_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.frmDSVanChuyenDo());
        }

        private void btnDSDangKyNV1_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.frmDSDangKyNhanVien());
        }

        private void btnLamNgoaiGio1_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.frmLamViecNgoaiGio());
        }

        private void btnYSSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.frmDSYeuCauSC());
        }

        private void itemDMDienLanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức dịch vụ điện lạnh", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.DienLanh.frmDinhMuc());
        }

        private void itemDongHoDienLanh_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.DienLanh.frmDongHo(), e.Item.Caption);
        }

        private void itemKeHoachXitConTrung_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.ServiceAndUtilities.KeHoach.frmKeHoach() { Loai = 1 }, e.Item.Caption);
        }

        private void itemLichBaoTri_Lich_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.ServiceAndUtilities.Lich.FrmSchedule() { Loai = 2 }, e.Item.Caption);
        }

        private void itemLichBaoTri_KhachHangXacNhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.ServiceAndUtilities.KhachHang.frmManager(), e.Item.Caption);
        }

        private void itemHopDongTOS_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new LandSoftBuilding.Lease.TOS.frmManager(), e.Item.Caption);
        }

        private void itemHopDong_TOS_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new LandSoftBuilding.Lease.TOS_EURO.frmManager(), e.Item.Caption);
        }

        private void itemDoanhSo_TOS_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new LandSoftBuilding.Lease.TOS.frmDoanhSo(), e.Item.Caption);
        }

        private void itemBCChiTietCongNoKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new Building.PrintControls.PrintForm();
            frm.Text = "Chi tiết công nợ khách hàng";
            frm.PrintControl.FilterForm = new LandSoftBuilding.Receivables.Reports.frmChiTietCongNo();
            this.LoadForm(frm);
        }

        private void itemBaoCaoKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo 1. KH Thu", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmShowHaveBuilding() { FormTemplateId = BuildingDesignTemplate.Class.Common.ReportIndex._1KH_THU }, "Báo cáo KH Thu");
        }

        private void itemBaoCaoThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo 2. Báo cáo thu", "Xem"); SetPhanQuyen(e);
            LoadForm(new BuildingDesignTemplate.FrmShowHaveBuilding() { FormTemplateId = BuildingDesignTemplate.Class.Common.ReportIndex.BC_THU }, "Báo cáo thu");
        }

        private void itemKeHoachBaoTriMayDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.ServiceAndUtilities.KeHoach.frmKeHoach() { Loai = 2 }, e.Item.Caption);
        }

        private void itemLichXitConTrung_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.ServiceAndUtilities.Lich.FrmSchedule() { Loai = 1 }, e.Item.Caption);
        }

        private void itemCauHinhBoPhanNhanEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.NguoiLienHe.frmBoPhanNhanEmail(), e.Item.Caption);
        }

        /// <summary>
        /// Danh sách dữ liệu công nợ từ phần mềm để export vào sap, tiền thu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem77_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new SAP.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// Danh sách dữ liệu công nợ từ phần mềm để export vào sap, doanh thu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem78_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new SAP.frmDoanhThu(), e.Item.Caption);
        }

        /// <summary>
        /// Đăng ký người liên hệ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemContactRegistry_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new Building.AppVime.NguoiLienHe.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// Phân quyền nhóm người liên hệ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemPhanQuyenNhomLienHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhachHang.NguoiLienHe.frmPhanQuyenNhom(), e.Item.Caption);
        }

        /// <summary>
        /// Cài đặt khóa sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemKhoaSo_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.KhoaSo.ClosingEntry.frmManager(), e.Item.Caption);
        }

        /// <summary>
        /// Đồng hồ gas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDongHoGass_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.Gas.frmDongHo(), e.Item.Caption);
        }

        private void itemTheXeCuDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.GiuXe.frmTheXeCuDan(), e.Item.Caption);
        }

        private void itemTheXeYeuCauHuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetPhanQuyen(e);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, DIPBMS.SystemLog.Classes.Params.XEM);
            LoadForm(new DichVu.GiuXe.frmTheXeYCHuy(), e.Item.Caption);
        }

        private void itemCauHinhMucDoYeuCau_ThoiGian_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình độ ưu tiên theo thời gian", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.YeuCau.frmDoUuTien_ThoiGianXuLy());
        }

        /// <summary>
        /// Danh sách đặt cọc giữ chổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemDsDatCocGiuCho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đặt cọc giữ chổ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.frmManager_PhieuDatCoc());
        }

        /// <summary>
        /// Danh sách phiếu thu đặt cọc giữ chổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemDanhSachPhieuThuCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu đặt cọc giữ chổ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Input.frmManager_DatCocGiuCho() { IsPhanLoai = 49 }, e.Item.Caption);
        }

        /// <summary>
        /// Danh sách phiếu đặt cọc cần thanh lý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItemThanhLyPhieuCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu đặt cọc cần thanh lý", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.frmManager_PhieuDatCoc_ThanhLy());
        }

        private void barButtonItem79_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình hạng mục thi công", "Xem"); SetPhanQuyen(e);
            LoadForm(new Building.AppVime.frmCauHinhHangMucThiCong());
        }

        /// <summary>
        /// DANH SÁCH ĐẶT CỌC THI CÔNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem80_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đặt cọc thi công", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.DatCoc.ThiCong.frmManager_DatCocThiCong());
        }

        private void itemDanhSachNghiemThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nghiệm thu", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.DatCoc.ThiCong.frmDanhSachNghiemThu());
        }

        private void itemHoanTraCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hoàn trả cọc", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.DatCoc.ThiCong.frmDanhSachHoanTraCoc());
        }

        /// <summary>
        /// DANH SÁCH PHIẾU THU TIỀN CỌC THI CÔNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem81_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu tiền cọc thi công", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Input.frmManager_DatCocGiuCho() { IsPhanLoai = 50 }, e.Item.Caption);
        }

        /// <summary>
        /// DANH SÁCH PHIẾU THU TIỀN PHẠT THI CÔNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem82_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu tiền phạt thi công", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Fund.Input.frmManager_DatCocGiuCho() { IsPhanLoai = 51 }, e.Item.Caption);
        }

        /// <summary>
        /// BC CHI TIẾT CÔNG NỢ - NHÓM BÁO CÁO CÔNG NỢ TỔNG HỢP (BỔ SUNG)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem83_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết công nợ - Nhóm báo cáo công nợ tổng hợp (bổ sung)", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.frmChiTietCongNo(), e.Item.Caption);
        }

        /// <summary>
        /// BC TỔNG HỢP CÔNG NỢ LAYOUT 1 - NHÓM BÁO CÁO CÔNG NỢ TỔNG HỢP (BỔ SUNG)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem84_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC công nợ tổng hợp layout 1 - Nhóm báo cáo công nợ tổng hợp (bổ sung)", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.frmChiTietCongNo_Layout1(), e.Item.Caption);
        }

        private void itemDienTichCanHo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích căn hộ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.MatBang.frmDienTichCanHo());
        }

        private void itemDienTichVanPhong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích văn phòng", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.MatBang.frmDienTichVanPhong());
        }

        /// <summary>
        /// BC TỔNG HỢP CÔNG NỢ LAYOUT 1 - NHÓM BÁO CÁO CÔNG NỢ TỔNG HỢP (BỔ SUNG)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem85_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC công nợ tổng hợp layout 2 - Nhóm báo cáo công nợ tổng hợp (bổ sung)", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.frmChiTietCongNo_Layout2(), e.Item.Caption);
        }
        /// /// <summary>
        /// BC CHI TIẾT THUÊ MẶT BẰNG - NHÓM BÁO CÁO CÔNG NỢ TỔNG HỢP (BỔ SUNG)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem86_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC chi tiết thuê mặt bằng - Nhóm báo cáo công nợ tổng hợp (bổ sung)", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.CongNoTongHop.frmChiTietThueMatBang(), e.Item.Caption);
        }
        /// <summary>
        /// BC CÔNG NỢ THUÊ MẶT BẰNG - NHÓM BÁO CÁO CÔNG NỢ TỔNG HỢP (BỔ SUNG)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem87_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC công nợ thuê mặt bằng - Nhóm báo cáo công nợ tổng hợp (bổ sung)", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.CongNoTongHop.frmCongNoThueMatBang(), e.Item.Caption);
        }

        private void barButtonItem88_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC TH công nợ tháng - Nhóm báo cáo công nợ tổng hợp (bổ sung)", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.CongNoTongHop.frmTongHopCongNoThang(), e.Item.Caption);
        }

        private void barButtonItem89_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC thống kê đơn giá - Nhóm báo cáo công nợ tổng hợp (bổ sung)", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.CongNoTongHop.frmBCThongKeDonGia(), e.Item.Caption);
        }

        private void barButtonItem91_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC tiền cọc form 1 - Nhóm báo cáo tiền cọc", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.TienCoc.frmTienCoc_Form1(), e.Item.Caption);
        }

        private void barButtonItem92_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("BC tiền cọc form 2 - Nhóm báo cáo tiền cọc", "Xem");
            SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.TienCoc.frmTienCoc_Form2(), e.Item.Caption);
        }

        private void barButtonItem93_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ theo tuổi nợ", "Xem");
            SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Receivables.Reports.frmBaoCaoTuoiNo2(), e.Item.Caption);
        }

        private void barButtonItem94_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ theo tuổi nợ Layout2", "Xem");
            SetPhanQuyen(e);
            this.LoadForm(new LandSoftBuilding.Receivables.Reports.frmBaoCaoTuoiNo2_Layout2(), e.Item.Caption);
        }

        /// <summary>
        /// BC TỶ LỆ LẤP ĐẦY - THÔNG TIN CHUNG LAYOUT 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem95_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.MatBang.frmDienTichSuDung(), e.Item.Caption);
        }

        /// <summary>
        /// BC TỶ LỆ LẤP ĐẦY - THÔNG TIN CHUNG LAYOUT 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem96_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.TyLeLapDay.frmTongTinChung_Layout2(), e.Item.Caption);
        }

        /// <summary>
        /// BC TỶ LỆ LẤP ĐẦY- CƯ DÂN LAYUOUT 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem101_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích căn hộ", "Xem"); SetPhanQuyen(e);
            LoadForm(new DichVu.MatBang.frmDienTichCanHo(), e.Item.Caption);
        }

        /// <summary>
        /// BC TỶ LỆ LẤP ĐẦY - CƯ DÂN LAYOUT 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem98_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích căn hộ", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.TyLeLapDay.frmCanHo_Layout2(), e.Item.Caption);
        }

        /// <summary>
        /// BC TỶ LỆ LẤP ĐẦY - CHO THUÊ LAYOUT 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem99_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích văn phòng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.TyLeLapDay.frmChoThue_Layout1(), e.Item.Caption);
        }

        /// <summary>
        /// BC TỶ LỆ LẤP ĐẦY - CHO THUÊ LAYOUT 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem100_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích văn phòng", "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.TyLeLapDay.frmChoThue_Layout2(), e.Item.Caption);
        }

        /// <summary>
        /// BC KẾ HOẠCH THU NĂM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem102_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.KeHoachThu.frmKeHoachThuNam(), e.Item.Caption);
        }

        /// <summary>
        /// Tỷ giá ngân hàng từng thời điểm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem103_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new LandSoftBuilding.Lease.DanhMuc.frmTyGia(), e.Item.Caption);
        }

        /// <summary>
        /// Quốc tịch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem104_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmQuocGia(), e.Item.Caption);
        }

        /// <summary>
        /// Tỉnh/ Thành phố
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem105_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmTinh(), e.Item.Caption);
        }

        /// <summary>
        /// Huyện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem106_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmHuyen(), e.Item.Caption);
        }

        /// <summary>
        /// Công nợ phải thu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem107_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.frmManHinhTrungGianHoaDon(), e.Item.Caption);
        }

        /// <summary>
        /// Giảm trừ công nợ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem108_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.frmManHinhTrungGianPhieuThu(), e.Item.Caption);
        }

        private void itemTieuDA_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmTieuDuAn(), e.Item.Caption);
        }

        /// <summary>
        /// Danh mục công trình
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem111_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new ToaNha.frmCongTrinh(), e.Item.Caption);
        }

        /// <summary>
        /// COMPANY CODE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem112_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.DanhMuc.frmCompanyCode(), e.Item.Caption);
        }

        /// <summary>
        /// CƯ DÂN CDT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem113_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.DanhMuc.frmCuDanCDT(), e.Item.Caption);
        }

        /// <summary>
        /// MAPPING DOANH THU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem116_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.DanhMuc.frmMappingDoanhThu(), e.Item.Caption);
        }

        /// <summary>
        /// MAPPING PHIẾU THU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem115_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.DanhMuc.frmMappingPhieuThu(), e.Item.Caption);
        }

        /// <summary>
        /// MAPPING NHÓM MẶT BẰNG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem117_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.DanhMuc.frmMappingNhomMatBang(), e.Item.Caption);
        }

        /// <summary>
        /// GHI LOG SAP TO DIP WEB API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem118_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.frmLichSuDongBoTuWebApi(), e.Item.Caption);
        }

        /// <summary>
        /// GHI LOG CHIỀU DIP TO SAP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem119_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new SAP.frmLichSuDongBoLSToSap(), e.Item.Caption);
        }

        /// <summary>
        /// DANH MỤC KẾ HOẠCH TUẦN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem120_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.DanhMuc.frmKeHoachTuan(), e.Item.Caption);
        }

        /// <summary>
        /// BÁO CÁO KẾ HOẠCH THU
        /// 1. BÁO CÁO CHI TIẾT THEO HẠN THANH TOÁN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem121_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert(e.Item.Caption, "Xem"); SetPhanQuyen(e);
            LoadForm(new LandsoftBuilding.Receivables.Reports.KeHoachThu.frmKeHoachTuan1(), e.Item.Caption);
        }
    }
}
