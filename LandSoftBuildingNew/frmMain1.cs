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

namespace LandSoftBuildingMain
{
    public partial class frmMain1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        MasterDataContext db;
        public bool IsAdmin { get; set; }
        public tnNhanVien User;

        public frmMain1()
        {
            InitializeComponent();
            using (DefaultLookAndFeel dlf = new DefaultLookAndFeel())
            {
                dlf.LookAndFeel.SkinName = Library.Properties.Settings.Default.SkinCurrent;
            }
            InitSkinGallery();
            TranslateLanguage.TranslateControl(this, null,ribbon);
            db = new MasterDataContext();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            wait.SetCaption("Đang tải màn hình làm việc");
            itemCopyRight.EditValue = Properties.Resources.dip;
                        
            LoadForm(new frmMainIntro() { objnhanvien = User });
            var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == User.MaTN);
            if (objTN != null)
            {
                Building_Statistic.ThongKeClass.InsertData(objTN.TenTN, "THÀNH THÀNH CÔNG", User.MaSoNV, User.HoTenNV, User.DienThoai, User.Email);
            }

            LoadNhacKeHoachBaoTri();
            barStaticItemLogin.Caption = string.Format("Người dùng: {0}", User.HoTenNV);

            wait.SetCaption("Đang kiểm tra quyền sử dụng và nạp dữ liệu người dùng");

            //LoadForm(new frmDesktop() { objnhanvien = User });
            Library.HeThongCls.PhanQuyenCls.PhanQuyenRibon(this, Common.User, ribbon);
            AnNinh.AnNinhCls.CheckAnNinhJobs(User);
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đăng nhập phần mềm", "Đăng nhập");
            wait.Close();
            wait.Dispose();

            timerReminder.Start();
            timerCapNhat.Start();

          
        }

        void InitSkinGallery()
        {
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(itemSkins, true);
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
                .Select(p=>new
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
            frm.Show();
        }

        private void itemToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Dự án", "Xem");
            LoadForm(new ToaNha.frmToaNha() {objNV = User });
        }

        private void itemLoaiMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại mặt bằng", "Xem");
            LoadForm(new MatBang.frmLoaiMatBang() { objnhanvien = User });
        }

        

        private void itemNhaCungCap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhà cung cấp", "Xem");
            LoadForm(new ToaNha.frmNhaCungCap() { objnv = User });
        }

       

        private void itemThamQuan_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới khách tham quan Dự án", "Thêm");
            using (KyThuat.ThamQuan.frmEdit frm = new KyThuat.ThamQuan.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách tham quan Dự án", "Xem");
                    LoadForm(new KyThuat.ThamQuan.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemThamQuan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách tham quan Dự án", "Xem");
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
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch bảo trì", "Xem");
                    LoadForm(new KyThuat.KeHoach.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemKeHoachBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch bảo trì", "Xem");
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
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách bảo trì", "Xem");
                    LoadForm(new KyThuat.BaoTri.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách bảo trì", "Xem");
            LoadForm(new KyThuat.BaoTri.frmManager() { objnhanvien = User });
        }

        private void itemMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng", "Xem");
            LoadForm(new DichVu.MatBang.frmManager());
        }

        private void itemMatBang_TrangThai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái mặt bằng", "Xem");
            LoadForm(new MatBang.frmTrangThai() { objnhanvien = User });
        }

        private void itemChoThue_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới hợp đồng cho thuê", "Thêm");
            using (var frm = new LandSoftBuilding.Lease.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng cho thuê", "Xem");
                    LoadForm(new LandSoftBuilding.Lease.frmManager());
                }
            }
        }

        private void itemChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {

            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng cho thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.frmManager());
        }

        private void itemTheXe_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới thẻ xe", "Thêm");
            using (DichVu.GiuXe.frmEdit frm = new DichVu.GiuXe.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe", "Xem");
                    LoadForm(new DichVu.GiuXe.frmTheXe());
                }
            }
        }

        private void itemLoaiXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại xe", "Xem");
            LoadForm(new DichVu.GiuXe.frmLoaiXe());
        }

        private void itemThangMay_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới thẻ thang máy", "Thêm");
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

        private void itemThangMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ thang máy", "Xem");
            LoadForm(new DichVu.ThangMay.frmManager() { objnhanvien = User });
        }

        private void itemNhanKhau_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới nhân khẩu", "Thêm");
            using (DichVu.NhanKhau.frmEdit frm = new DichVu.NhanKhau.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhân khẩu", "Xem");
                    LoadForm(new DichVu.NhanKhau.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemNhanKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhân khẩu", "Xem");
            LoadForm(new DichVu.NhanKhau.frmManager() { objnhanvien = User });
        }

        private void itemDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ điện", "Xem");
            LoadForm(new DichVu.Dien.frmManager());
        }

        private void itemNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ nước", "Xem");
            LoadForm(new DichVu.Nuoc.frmManager());
        }

        private void itemDien_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức dịch vụ điện", "Xem");
            LoadForm(new DichVu.Dien.frmDinhMuc());
        }

        private void itemNuoc_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức dịch vụ nước", "Xem");
            using (DichVu.Nuoc.frmDinhMuc frm = new DichVu.Nuoc.frmDinhMuc())
            {
                frm.ShowDialog();
            }
        }

        private void itemThueNgoai_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hợp đồng thuê ngoài", "Thêm");
            using (DichVu.ThueNgoai.frmEdit frm = new DichVu.ThueNgoai.frmEdit() { objnhanvien = User })
            {   
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê ngoài", "Xem");
                    LoadForm(new DichVu.ThueNgoai.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemThueNgoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê ngoài", "Xem");
            LoadForm(new DichVu.ThueNgoai.frmManager() { objnhanvien = User });
        }

        private void itemHopTac_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hợp đồng hợp tác", "Thêm");
            using (DichVu.HopTac.frmEdit frm = new DichVu.HopTac.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng hợp tác", "Xem");
                    LoadForm(new DichVu.HopTac.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemHopTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng hợp tác", "Xem");
            LoadForm(new DichVu.HopTac.frmManager() { objnhanvien = User });
        }

        private void itemDichVuKhac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ khác","Xem");
            LoadForm(new DichVu.Khac.frmManager());
        }

        private void itemSuaChua_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm yêu cầu sửa chữa của khách hàng", "Thêm");
            using (KyThuat.SuaChua.frmEdit frm = new KyThuat.SuaChua.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu sửa chữa của khách hàng", "Xem");
                    LoadForm(new KyThuat.SuaChua.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu sửa chữa của khách hàng", "Xem");
            LoadForm(new KyThuat.SuaChua.frmManager() { objnhanvien = User });
        }

        private void itemYeuCau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu của khách hàng", "Xem");
            LoadForm(new DichVu.YeuCau.frmManager());
        }

        private void itemYeuCau_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm yêu cầu của khách hàng", "Thêm");
            using (DichVu.YeuCau.frmEdit frm = new DichVu.YeuCau.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu của khách hàng", "Xem");
                    LoadForm(new DichVu.YeuCau.frmManager());
                }
            }
        }

        private void itemKhachHang_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khách hàng", "Thêm");
            using (KyThuat.KhachHang.frmEdit frm = new KyThuat.KhachHang.frmEdit() { objnv = User })
            {
                frm.maTN = (byte)Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách hàng", "Xem");
                    LoadForm(new KyThuat.KhachHang.frmManager() { objnhanvien = User });
                }
            }
        }

        private void itemKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách hàng", "Xem");
            LoadForm(new KyThuat.KhachHang.frmManager() { objnhanvien = User });
        }

        private void itemMatBang_Them_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mặt bằng Dự án", "Thêm");
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

        private void itemKhoiNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khối nhà", "Xem");
            LoadForm(new MatBang.frmKhoiNha() { objnhanvien = User });
        }

        private void itemMatBang_View_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng Dự án", "Xem");
            LoadForm(new MatBang.frmMatBang() { objnhanvien = User });
            
        }

        private void itemTangLau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tầng lầu", "Xem");
            LoadForm(new MatBang.frmTangLau() { objnhanvien = User });
        }

       

        private void itemTrangThaiKHBT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái kế hoạch", "Xem");
            LoadForm(new KyThuat.KeHoach.frmTrangThai() { objnhanvien = User });
        }

        private void itemTrangThaiChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái cho thuê", "Xem");
            LoadForm(new DichVu.ChoThue.frmTrangThai() { objnhanvien = User });
        }

        private void itemTrangThaiThueNgoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái thuê ngoài", "Xem");
            LoadForm(new DichVu.ThueNgoai.frmTrangThai() { objnhanvien = User });
        }

        private void itemTrangThaiHopTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái hợp tác", "Xem");
            LoadForm(new DichVu.HopTac.frmTrangThai() { objnhanvien = User });
        }

        private void barButtonItem18_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ReportMisc.KhachHang.rptKHChoose frmKhachHang = new ReportMisc.KhachHang.rptKHChoose();
            frmKhachHang.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thoát phần mềm", "Thoát");
            this.Close();
        }
        
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đăng xuất phần mềm", "Đăng xuất");
            Application.Restart();
        }
       
        private void btnNguoiDung_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách người dùng Dự án", "Xem");
            LoadForm(new LandsoftBuildingGeneral.NguoiDung.UserManager() { objnhanvien = User });
        }

        private void btnexit_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thoát phần mềm", "Thoát");
            if (DialogBox.Question("Bạn có chắc muốn thoát khỏi chương trình không?") == DialogResult.Yes)
                Application.Exit();
        }

        private void btnlogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đăng xuất phần mềm", "Đăng xuất");
            if (DialogBox.Question("Bạn có chắc muốn đăng xuất không?") == DialogResult.Yes)
                Application.Restart();
        }

        private void btnchangepass_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thay đổi mật khẩu", "Thay đổi");
            using (LandsoftBuildingGeneral.NguoiDung.frmChangePassword frmchange = new LandsoftBuildingGeneral.NguoiDung.frmChangePassword())
            {
                frmchange._user = this.User;
                frmchange.ShowDialog();
            }
        }

        private void btnGiayBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn giấy báo", "Xem");
            LoadForm(new DichVu.HoaDon.frmManager() { objnhanvien = User });
        }

        private void btnHoaDon_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn dịch vụ khách hàng", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.frmManager());
        }

        private void itemSkins_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thay đổi giao diện", "Xem");
            Library.Properties.Settings.Default.SkinCurrent = e.Item.Caption;
            Library.Properties.Settings.Default.Save();
        }

        

        private void btnNhanVienManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách người dùng Dự án", "Xem");
            LoadForm(new LandsoftBuildingGeneral.NguoiDung.UserManager() { objnhanvien = User });
        }

        private void btnThemNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm người dùng cho Dự án", "Thêm");
            using (LandsoftBuildingGeneral.NguoiDung.UserEdit frm = new LandsoftBuildingGeneral.NguoiDung.UserEdit()) 
            {
                frm.ShowDialog();
            }
        }

        private void btnPhongBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phòng ban", "Xem");
            LoadForm(new LandsoftBuildingGeneral.NguoiDung.frmPhongBanManager() { objnhanvien = User });
        }

        private void btnPhanQuyenNV_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân quyền theo nhân viên", "Phân quyền");
            LoadForm(new LandsoftBuildingGeneral.PhanQuyen.frmPhanQuyenManager() { objnhanvien = User });
        }

        private void navBarItemLock_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Khởi động lại ứng dụng", "Khởi động lại");
            Application.Restart();
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái nhân khẩu", "Xem");
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
            using (KyThuat.KhachHang.frmEdit frm = new KyThuat.KhachHang.frmEdit() { objnv = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách hàng", "Xem");
                    LoadForm(new KyThuat.KhachHang.frmManager() { objnhanvien = User });
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tham quan", "Xem");
            LoadForm(new KyThuat.ThamQuan.frmThongKe() { objnhanvien = User });
        }

        private void butnTkMBTheoTT_ItemClick(object sender, ItemClickEventArgs e)
        {
            //LoadForm(new DichVu.MatBang.ThongKe.frmThongKe() { objnhanvien = User });
        }

        private void btnTKDienNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê dịch vụ điện nước", "Thống kê");
            LoadForm(new DichVu.ThongKe.frmDienNuoc() { objnhanvien = User });
        }

        private void btnUpdater_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cập nhật phần mềm", "Cập nhật");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Xem giới thiệu", "Xem");
            using (LandsoftBuildingGeneral.AboutUs.AboutUs abf = new LandsoftBuildingGeneral.AboutUs.AboutUs())
            {
                abf.ShowDialog();
            }
        }

        private void btnTrangThaiThangMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái của thang máy", "Xem");
            LoadForm(new DichVu.ThangMay.frmTrangThai());
        }

        private void btnTrangThaiYC_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái của yêu cầu", "Xem");
            LoadForm(new DichVu.YeuCau.frmTrangThai());
        }

        private void btnDoUuTienYC_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách độ ưu tiên của yêu cầu", "Xem");
            LoadForm(new DichVu.YeuCau.frmDoUuTien());
        }

        private void btnThongBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thông báo nhắc việc chung", "Thông báo");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ theo khách hàng", "Báo cáo");
            using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.CongNoTheoKhachHang, objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void btnBaoCaothu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ thu theo khách hàng", "Báo cáo");
            using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoThu, objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void BtnBaoCaoChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ chi theo khách hàng", "Báo cáo");
            using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoChi, objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void btnBaoCaoCongNoTheoMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ theo mặt bằng của khách hàng", "Báo cáo");
            using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.CongNoTheoMatBang, objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemTKThuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê thu chi dịch vụ", "Thống kê");
            LoadForm(new DichVu.ThongKe.frmThuChi() { objnhanvien = User });
        }

        private void navBarItemBaoCaoThu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ thu theo khách hàng", "Báo cáo");
            using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoThu, objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void navBarItemBaoCaoChi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ chi theo khách hàng", "Báo cáo");
            using (ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang frm = new ReportMisc.TongHop.BaoCaoCongNoTheoKhachHang() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoChi, objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemTKHDTTT_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new DichVu.ThongKe.frmHopDong() { objnhanvien = User });
        }

        private void itemDoanhThuHopDongThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê doanh thu hợp đồng", "Xem");
            LoadForm(new DichVu.ThongKe.frmDoanhThuHopDong());
        }

        private void itemTTNhacNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái nhắc nợ", "Xem");
            LoadForm(new DichVu.frmTrangThaiNhacNo() { objnhanvien = User });
        }

        private void itemNhacNoTheoKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhắc nợ dịch vụ khách hàng", "Xem");
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
                        .Where(p=>p.mbMatBang.mbTangLau.mbKhoiNha.tnToaNha.MaTN == User.MaTN)
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ thuê ngoài", "Xem");
            LoadForm(new DichVu.ThueNgoai.frmCongNoManager() { objnhanvien = User });
        }

        private void navBarItemPhieuChi_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu chi", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Output.frmManager());
        }

        private void btnCongNoHDHT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ hợp tác", "Xem");
            LoadForm(new DichVu.HopTac.frmCongNoManager() { objnhanvien = User });
        }

        private void itemNhacViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thông báo nhắc việc", "Xem");
            LoadForm(new ToaNha.NhacViec_ThongBao.frmNhacViec() { objnhanvien = User });
        }

        private void itemEmailAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tài khoản email", "Xem");
            LoadForm(new DichVu.SendMail.frmEmailAccountManager());
        }

        private void itemNewEmail_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm gửi mail", "Thêm");
            using (DichVu.SendMail.frmSendMailEdit frm = new DichVu.SendMail.frmSendMailEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemEmailListManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách gửi mail", "Xem");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê yêu cầu", "Thống kê");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đề xuất", "Báo cáo");
            using (ReportMisc.TongHop.frmBaoCaoTheoThoiGian frm = new ReportMisc.TongHop.frmBaoCaoTheoThoiGian() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoDeXuat })
            {
                frm.ShowDialog();
            }
        }

        private void itemBaoCaoMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo mua hàng", "Báo cáo");
            using (ReportMisc.TongHop.frmBaoCaoTheoThoiGian frm = new ReportMisc.TongHop.frmBaoCaoTheoThoiGian() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoMuaHang })
            {
                frm.ShowDialog();
            }
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm gửi mail", "Gửi mail");
            using (DichVu.SendMail.frmSendMailEdit frm = new DichVu.SendMail.frmSendMailEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itmBaoCaoDonDatHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đặt hàng theo thời gian", "Báo cáo");
            using (ReportMisc.TongHop.frmBaoCaoTheoThoiGian frm = new ReportMisc.TongHop.frmBaoCaoTheoThoiGian() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCao.BaoCaoDatHang })
            {
                frm.ShowDialog();
            }
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo danh sách mặt bằng", "Báo cáo");
            ReportMisc.MatBang.rptDanhSachMatBang rpt = new ReportMisc.MatBang.rptDanhSachMatBang();
            rpt.ShowPreviewDialog();
        }

        private void itemThongBaoThuPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo giấy thông báo phí", "Báo cáo");
            ReportMisc.TongHop.rptGiayBaoThanhToanTong rpt = new ReportMisc.TongHop.rptGiayBaoThanhToanTong(User);
            rpt.ShowPreviewDialog();
        }

        private void itemThongBaoCatNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thông báo tạm ngừng cung cấp nước", "Báo cáo");
            ReportMisc.TongHop.rptThongBaoTamNgungCapNuoc rpt = new ReportMisc.TongHop.rptThongBaoTamNgungCapNuoc(User);
            rpt.ShowPreviewDialog();
        }

        private void itemBaoCaoTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hpwpj số tiền thực thu", "Báo cáo");
            ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.BaoCaoTongHopSoTienThucThu, objnhanvien = User };
            frm.ShowDialog();
        }

        private void itemBaoCaoKetQauKinhDoanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo kết quả kinh doanh", "Báo cáo");
            ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.BaoCaoKetQuaKinhDoanh, objnhanvien = User };
            frm.ShowDialog();
        }

        private void itemChiTietNopTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết nộp tiền", "Báo cáo");
            ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.ChiTietNopTien, objnhanvien = User };
            frm.ShowDialog();
        }

        private void itemChiTietThuPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết thu  phí quản lý", "Báo cáo");
            ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.ChiTietThuPhiQuanLy, objnhanvien = User };
            frm.ShowDialog();
        }

        private void itemTheoDoiCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo theo dõi công nợ tổng hợp", "Báo cáo");
            ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.TheoDoiCongNoTongHop, objnhanvien = User };
            frm.ShowDialog();
        }

        private void itemCongNoTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp công nợ", "Báo cáo");
            ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.BaoCaoTongHopCongNo, objnhanvien = User };
            frm.ShowDialog();
        }

        private void itemTheoDoiCongNoPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo theo dõi công nợ phí quản lý", "Báo cáo");
            ReportMisc.TongHop.frmKyBaoCao frm = new ReportMisc.TongHop.frmKyBaoCao() { LoaiBaoCao = ReportMisc.TongHop.EnumLoaiBaoCaoTongHop.TheoDoiCongNoPhiQuanLy, objnhanvien = User };
            frm.ShowDialog();
        }

        private void itemTQQuanLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Quản lý vận hành", "Xem");
            LoadForm(new KyThuat.ThamQuan.frmManager() { objnhanvien = User });
        }

        private void btnAnhNinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch", "Xem");
            LoadForm(new AnNinh.frmKeHoach() { objnhanvien = User });
        }

        private void btnMyMisson_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch của nhân viên", "Xem");
            AnNinh.frmKeHoachCuaToi frm = new AnNinh.frmKeHoachCuaToi() { objnhanvien = User, ViewLog = false };
            frm.Show();
        }

        private void btnNhatKyAN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kế hoạch của nhân viên", "Xem");
            AnNinh.frmKeHoachCuaToi frm = new AnNinh.frmKeHoachCuaToi() { objnhanvien = User, ViewLog = true };
            frm.Show();
        }

        private void btnGhiNhanSV_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Ghi nhận sự việc", "Xem");
            LoadForm(new AnNinh.frmGhiNhanSuViec() { objnhanvien = User });
        }
        
        private void btnDinhNghiaTruongBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trường định nghĩa của biểu mẫu", "Xem");
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmFieldDefine());
        }

        private void btnQuanLyBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách biểu mẫu", "Xem");
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmManager() { objnhanvien = User });
        }

        private void btnLoaiBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại biểu mẫu", "Xem");
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmLoaiBieuMau() { objnhanvien = User });
        }

        private void btnAdminLogAN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhật ký login", "Xem");
            LoadForm(new AnNinh.frmAdminLogGhiNhanSuViec() { objnhanvien = User });
        }

        private void btnChucVuMNG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chức vụ", "Xem");
            LoadForm(new ToaNha.frmChucVu() { objnhanvien = User });
        }

        private void btnPhiQuanLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ phí quản lý", "Xem");
            LoadForm(new DichVu.PhiQuanLy.frmManager() { objnhanvien = User });
        }

        private void btnQuyTac_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đánh số tự động", "Xem");
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmDanhSoTuDong());
        }

        private void btnKhuVuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khu vực Dự án", "Xem");
            LoadForm(new ToaNha.frmKhuVuc() { objnhanvien = User });
        }

        private void btnThucHienKeHoach_ItemClick(object sender, ItemClickEventArgs e)
        {
           // LoadForm(new KyThuat.KeHoach.frmThucHien() { objnhanvien = User });
        }

        private void btnDichVuCongCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ công cộng", "Xem");
            LoadForm(new DichVu.DichVuCongCong.frmManager());
        }

        private void btnDichVuCCManger_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ công nợ công cộng", "Xem");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bảng kê phiếu xuất", "Xem");
            ReportMisc.TaiSan.NhapXuat.frmKyBaoCao frm = new ReportMisc.TaiSan.NhapXuat.frmKyBaoCao() { objnhanvien = User, typebc = ReportMisc.TaiSan.NhapXuat.enumXuatNhapKho.XuatKho };
            frm.ShowDialog();
        }

        private void btnBangKePhieuNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bảng kê phiếu nhập", "Xem");
            ReportMisc.TaiSan.NhapXuat.frmKyBaoCao frm = new ReportMisc.TaiSan.NhapXuat.frmKyBaoCao() { objnhanvien = User, typebc = ReportMisc.TaiSan.NhapXuat.enumXuatNhapKho.NhapKho };
            frm.ShowDialog();
        }

        private void rptThongKeKhoaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thống kê kho hàng", "Báo cáo");
            ReportMisc.TaiSan.frmPickStore frm = new ReportMisc.TaiSan.frmPickStore() { objnhanvien = User };
            frm.ShowDialog();
        }

        private void btnThemLichThanhToanDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm lịch thanh toán điện", "Thêm");
            using (KyThuat.ThanhToanDichVu.frmEdit_Dien frm = new KyThuat.ThanhToanDichVu.frmEdit_Dien())
            {
                frm.ShowDialog();
            }
        }

        private void btnLichThanhToanDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch thanh toán điện", "Xem");
            LoadForm(new KyThuat.ThanhToanDichVu.frmManager() { objnhanvien = User });
        }

        private void btnNgonNgu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ngôn ngữ", "Xem");
            LoadForm(new LandsoftBuildingGeneral.frmNgonNgu());
        }

        private void btnLoaiThePhongTap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại thẻ phòng tập", "Xem");
            LoadForm(new DichVu.PhongTap.frmLoaiThe());
        }

        private void btnAddThePhongTap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm thẻ phòng tập", "Thêm");
            using (DichVu.PhongTap.frmEdit frm = new DichVu.PhongTap.frmEdit() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void btnQuanLyThePhongTap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ phòng tập", "Xem");
            LoadForm(new DichVu.PhongTap.frmManager() { objnhanvien = User });
        }

        private void btnVietPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmVietPhieuThu frm = new frmVietPhieuThu() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadForm(new DichVu.Quy.frmPhieuThu() { objnhanvien = User });
            }
        }

        private void btnVietPhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmVietPhieuChi frm = new frmVietPhieuChi() { objnhanvien = User })
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    LoadForm(new DichVu.Quy.frmPhieuChi() { objnhanvien = User });
            }
        }

        private void btnHoaDonGiayBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn", "Xem");
            LoadForm(new DichVu.HoaDon.frmManager() { objnhanvien = User });
        }

        private void btnKhachHangBaoCao_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (ReportMisc.MatBang.frmChoose frm = new ReportMisc.MatBang.frmChoose())
            {
                frm.ShowDialog();
            }
        }

        private void btnGiuXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo danh sách gửi xe", "Báo cáo");
            ReportMisc.DichVu.GiuXe.rptDanhSachGuiXe frm = new ReportMisc.DichVu.GiuXe.rptDanhSachGuiXe();
            frm.ShowPreviewDialog();
        }

        private void btnTieuThuDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tiêu thụ điện", "Thống kê");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 1;
            f.ShowDialog();
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tiêu thụ nước", "Thống kê");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 3;
            f.ShowDialog();
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tiêu thu điện nước", "Thống kê");
            using (ReportMisc.TongHop.frmThongKeDienNuoc frm = new ReportMisc.TongHop.frmThongKeDienNuoc())
            {
                frm.objnhanvien = User;
                frm.ShowDialog();
                frm.Close();
            }
        }

        private void btnGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ Gas", "Xem");
            LoadForm(new DichVu.Gas.frmManager());
        }

        private void btnsmGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức Gas", "Cài đặt");
            LoadForm(new DichVu.Gas.frmDinhMuc());
        }

        private void btnSupport_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region Check version
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                var Version = client.DownloadString("http://support.dip.vn/dll/v12/support.txt");
                if (Version != DIPCRM.Support.Library.Common.Version)
                {
                    //DIPCRM.Support.Updater.UpdateVer f = new DIPCRM.Support.Updater.UpdateVer();
                    //f.ShowDialog();
                    DIPCRM.Support.Library.Common.Version = Version;
                }
            }
            catch
            { }
            #endregion
            var support = new DIPCRM.Support.Library.SupportConfig();
            DIPCRM.Support.Library.Common.ConnectionString = global::Library.Properties.Settings.Default.Building_dbConnectionString;
            try
            {
                support.GetAccount();
            }
            catch (Exception ex) { DialogBox.Alert(ex.Message); }
            DIPCRM.Support.Library.Common.ClientNo = support.ClientNo;
            DIPCRM.Support.Library.Common.ClientPass = support.ClientPass;
            DIPCRM.Support.Library.Common.ClientEmail = support.Email;
            DIPCRM.Support.Library.Common.ClientName = support.Name;
            DIPCRM.Support.Library.Common.StaffName = "(" + User.MaSoNV + ") " + User.HoTenNV;

            LoadForm(new frmSupport());
        }

        private void btnPhiVeSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phí vệ sinh", "Xem");
            LoadForm(new DichVu.PhiVeSinh.frmManager() { objnhanvien = User });
        }

        private void btnChietKhauPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt chiết khấu cho phí quản lý", "Cài đặt");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt phí quản lý", "Cài đặt");
            LoadForm(new DichVu.PhiQuanLy.frmSetting() { objnhanvien = User });
        }

        private void itemSetupPhiChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt phí cho thuê", "Cài đặt");
            LoadForm(new DichVu.ChoThue.frmSetting() { objnhanvien = User });
        }

        private void itemAddProvider_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm nhà cung cấp", "Thêm");
            var frm = new Provider.frmEdit();
            frm.ShowDialog();
        }

        private void itemListProvider_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhà cung cấp", "Xem");
            LoadForm(new Provider.frmManager() { objnhanvien = User });
        }

        private void itemDMNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức dịch vụ nước", "Xem");
            LoadForm(new DichVu.Nuoc.frmDinhMuc());
        }

        private void itemTXDinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức dịch vụ giữ xe", "Xem");
            LoadForm(new DichVu.GiuXe.frmDinhMuc());
        }

        private void itemBCBangKeThuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê thu chi", "Báo cáo");
            using (var f = new ReportMisc.DichVu.Option.fromMonthYear())
            {
                f.objnhanvien = User;
                f.ShowDialog();
            }
        }

        private void itemCustomerDeb_ItemClick(object sender, ItemClickEventArgs e)
        {
            using(var frm = new ReportMisc.frmChooseFromTo()){
                frm.ShowDialog();
            }
        }

        private void itemBCGasChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo Gas chi tiết", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 6;
            f.ShowDialog();
        }

        private void itemBCNuocChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo nước chi tiết", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 5;
            f.ShowDialog();
        }

        private void itemBCDienChiTiet_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo điện chi tiết", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 4;
            f.ShowDialog();
        }

        private void itemBCDSGasNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo doanh số Gas- Nước", "Báo cáo");
            var f = new ReportMisc.frmPickDateTower();
            f.objNV = User;
            f.ShowDialog();
        }

        private void itemBCGasPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu Gas", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 8;
            f.ShowDialog();
        }

        private void itemBCNuocPhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu nước", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 7;
            f.ShowDialog();
        }

        private void itemBCPhatSinhPQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phát sinh phí quản lý", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 9;
            f.ShowDialog();
        }

        private void itemBCTongHopThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp thu", "Báo cáo");
            var f = new ReportMisc.frmPickDateTower();
            f.objNV = User;
            f.CateID = 2;
            f.ShowDialog();            
        }

        private void itemBCTongPhaiThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp phải thu", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 10;
            f.ShowDialog();
        }

        private void itemBCTongHopChuaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp chưa thu", "Báo cáo");
            var f = new ReportMisc.frmPickDateTower();
            f.objNV = User;
            f.CateID = 4;
            f.ShowDialog();  
        }

        private void itemBCDoanhThuTheoNgay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo doanh thu theo ngày", "Báo cáo");
            var f = new ReportMisc.frmPickDateTowerV2();
            f.objNV = User;
            f.CateID = 1;
            f.ShowDialog();
        }

        private void itemBCPQLBangThuPhi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phí quản lý thu chi", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 11;
            f.ShowDialog();
        }

        private void itemBCPQLChiTietPhatSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phí quản lý chi tiết phát sinh", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 12;
            f.ShowDialog();
        }

        private void itemBCLuyKeNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo lũy kế theo năm", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOptionV2();
            f.objNV = User;
            f.CateID = 1;
            f.ShowDialog();
        }

        private void itemBCGasTTNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo Gas", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOptionV2();
            f.objNV = User;
            f.CateID = 2;
            f.ShowDialog();
        }

        private void itemBCDienTTNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo điện", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOptionV2();
            f.objNV = User;
            f.CateID = 3;
            f.ShowDialog();
        }

        private void itemBCNuocTTNam_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo nước", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOptionV2();
            f.objNV = User;
            f.CateID = 4;
            f.ShowDialog();
        }

        private void itemNgungCungCapDV_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void itemBCPQL_PhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu phí quản lý", "Báo cáo");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 14;
            f.ShowDialog();
        }

        private void itemKhoaSoAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khóa sổ", "Thêm");
            var f = new DichVu.KhoaSo.frmEdit() { objnhanvien = User };
            f.ShowDialog();
        }

        private void itemKhoaSoList_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khóa sổ", "Xem");
            LoadForm(new DichVu.KhoaSo.frmManager() { objnhanvien = User });
        }

        private void itemPhieuTHuPVS_ItemClick(object sender, ItemClickEventArgs e)
        {
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 15;
            f.ShowDialog();
        }

        private void navPhieuThuChiTiet_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            LoadForm(new DichVu.Quy.frmPhieuThuV2() { objnhanvien = User });
        }

        private void itemBCTX_DanhSachDangKy_ItemClick(object sender, ItemClickEventArgs e)
        {
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 17;
            f.ShowDialog();
        }

        private void itemBCTX_ChiTietPhatSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 16;
            f.ShowDialog();
        }

        private void itemBCTX_PhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 18;
            f.ShowDialog();
        }

        private void itemBCTX_ChuaThanhToan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem");
            var f = new ReportMisc.DichVu.frmOption();
            f.objNV = User;
            f.CateID = 19;
            f.ShowDialog();
        }

        private void itemTXList_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe", "Xem");
            LoadForm(new DichVu.GiuXe.frmTheXe());
        }

        private void itemConfigFTP_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình FTP", "Cấu hình");
            var f = new FTP.frmConfig();
            f.ShowDialog();
        }

        private void itemDKUuDai_List_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ưu đãi nhân khẩu", "Xem");
            LoadForm(new DichVu.NhanKhau.UuDai.frmManager() { objnhanvien = User });
        }

        private void itemDKUuDai_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm ưu đãi cho nhân khẩu", "Thêm");
            var f = new DichVu.NhanKhau.UuDai.frmEdit();
            f.objnhanvien = User;
            f.ShowDialog();
        }

        private void itemHoBoiThe_list_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ hồ bơi", "Xem");
            LoadForm(new DichVu.HoBoi.frmManager() { objnhanvien = User });
        }

        private void itemHoBoiCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ hồ bơi", "Xem");
            LoadForm(new DichVu.HoBoi.frmHBManager() { objnhanvien = User });
        }

        private void itemHoiBoiLoaiThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại thẻ", "Xem");
            LoadForm(new DichVu.HoBoi.LoaiThe.frmLoaiThe()); 
        }

        private void itemHoBoiDinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức hồ bơi", "Xem");
            LoadForm(new DichVu.HoBoi.DinhMuc.frmManager() { objnhanvien = User });
        }

        private void itemLaiSuatChamNop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt lãi suất", "Xem");
            LoadForm(new ToaNha.frmLaiSuat()); 
        }

        private void itemCaiDatTTCanhBao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái cảnh báo", "Xem");
            LoadForm(new DichVu.CanhBao.frmManager());
        }

        private void itemPhiBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ phí bảo trì", "Xem");
            LoadForm(new DichVu.PhiBaoTri.frmManager() { objnhanvien = User });
        }


        private void itemDSCVLuoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đầu mục công việc", "Xem");
            LoadForm(new KyThuat.DauMucCongViec.frmManager() { objnhanvien = User });
        }

        private void itemDSCVLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch công việc", "Xem");
            LoadForm(new KyThuat.DauMucCongViec.frmLichCongViec() { objnhanvien = User });
        }

        private void itemDSCongViecDG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhiệm vụ của nhân viên", "Xem");
            LoadForm(new KyThuat.NhiemVu.frmManager() { objnhanvien = User });
        }

        private void itemDSCVDGLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch làm việc của nhân viên", "Xem");
            LoadForm(new KyThuat.NhiemVu.frmLichLamViecNV() { objnhanvien = User });
        }

       

        private void itemTNTyGia_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tỷ giá", "Xem");
            LoadForm(new ToaNha.frmTyGia() { objnv = User});
        }

        

        private void itemDMTK_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định mức tài khoản", "Xem");
            LoadForm(new ToaNha.frmTaiKhoan());
        }

        private void itemMayDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem");
            LoadForm(new DichVu.Dien.DieuHoaNgoaiGio.frmManager());
        }

        private void itemThemDHNG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm điện điều hòa ngoài giờ", "Thêm");
            using (var frm = new DichVu.Dien.DieuHoaNgoaiGio.frmEdit())
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem");
                    LoadForm(new DichVu.Dien.DieuHoaNgoaiGio.frmManager());
                }
            }
        }

        private void itemDSDHNG_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa ngoài giờ", "Xem");
            LoadForm(new DichVu.Dien.DieuHoaNgoaiGio.frmManager());
        }

        private void itemBanCDPSCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng cân đối phát sinh công nợ", "Báo cáo");
            var rpt = new ReportMisc.BaoCaoCTM.BaoCao.BangCanDoiPhatSinhCongNo(1, DateTime.Now, DateTime.Now.AddDays(30));
            rpt.ShowPreviewDialog();
        }

        private void itemBangKeCT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê chứng từ", "Báo cáo");
            var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rpt_BangKeChungTu();
            rpt.ShowPreviewDialog();
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê thu phí chung cư", "Báo cáo");
            var rpt = new ReportMisc.BaoCaoCTM.BaoCao.BangKeThuTienPhiChungCu();
            rpt.ShowPreviewDialog();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các căn hộ dân còn nợ phí", "Báo cáo");
            var rpt = new ReportMisc.BaoCaoCTM.BaoCao.DanhSachCacCanHoDanNoPhi();
            rpt.ShowPreviewDialog();
        }

        private void itemSoCTCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var rpt = new ReportMisc.BaoCaoCTM.BaoCao.SoChiTietCongNo();
            //rpt.ShowPreviewDialog();
        }

        private void itemKhoanThuHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tiền thuê diện tích văn phòng", "Báo cáo");
            var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rptBCTienThueDienTichVP();
            rpt.ShowPreviewDialog();
        }

        private void itemBCCacKhoanPhiDV_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các khoản thu phí dịch vụ", "Báo cáo");
            var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rptBCCacKhoanThuPhiDV();
            rpt.ShowPreviewDialog();
        }

        private void itemBCCacKhoanTon_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các khoản tồn động", "Báo cáo");
            var rpt = new ReportMisc.BaoCaoCTM.BaoCao.rptBCCacKhoanTonDong();
            rpt.ShowPreviewDialog();
        }

        private void itemLoaiHDTN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm dịch vụ thuê ngoài", "Thêm");
            using (var frm = new DichVu.ThueNgoai.frmLoaiDV() { objnhanvien = User })
            {
                frm.ShowDialog();
            }
        }

        private void itemBCTongHopCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo bảng kê công nợ", "Xem");
            LoadForm(new ReportMisc.DichVu.Quy.frmManager() { objnhanvien = User });
        }

       

        private void itemDCChiTietCN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết công nợ", "Xem");
            LoadForm(new ReportMisc.BaoCaoCTM.BaoCao.frmManager() { objnhanvien = User });
        }

        private void itemTHemDVGS_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm ghi sổ cuối kỳ", "Thêm");
            var f = new DichVu.GhiSo.frmEdit() { objnhanvien = User };
            f.ShowDialog();
        }

        private void itemDSGS_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ghi sổ cuối kỳ", "Xem");
            LoadForm(new DichVu.GhiSo.frmManager() { objnhanvien = User });
        }

        private void itemDKThanhToan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đăng ký phí quản lý", "Xem");
            LoadForm(new DichVu.PhiQuanLy.DangKy.frmManager() { objnhanvien = User });
        }

        private void itemLSDCCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch sử điều chỉnh công nợ", "Xem");
            LoadForm(new DichVu.LichSuDieuChinhCN.frmManager() { objnhanvien = User });
        }

        private void itemLoaiTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Loại tiền", "Xem");
           //them moi
            LoadForm(new ToaNha.frmLoaiTien());
        }

        private void itemLoaiGiaThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loiaj giá thuê", "Xem");
            LoadForm(new ToaNha.frmLoaiGiaThue());
        }

        private void itemHoaDonAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tạo hóa đơn tự động", "Thêm");
            using (var frm = new LandSoftBuilding.Receivables.frmAddAuto())
            {
                frm.ShowDialog();
            }
        }

        private void itemNhacNoKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ tổng hợp theo khách hàng", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.frmReceivables());
        }

        private void itemLoaiDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại dịch vụ Dự án", "Xem");
            LoadForm(new ToaNha.frmLoaiDichVu());
        }

        private void itemCaiDatChietKhau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chiết khấu dịch vụ Dự án", "Xem");
            LoadForm(new ToaNha.frmChietKhau());
        }

        private void itemTheXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe", "Xem");
            LoadForm(new DichVu.GiuXe.frmTheXe());
        }

        private void itemThueNgoaiGio_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cho thuê ngoài giờ", "Xem");
            LoadForm(new LandSoftBuilding.Lease.Overtime.frmManager());
        }

        private void itemThueNganHanAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm cho thuê ngắn hạng", "Thêm");
            using (var frm = new LandSoftBuilding.Lease.ShortTerm.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cho thuê ngắn hạng", "Xem");
                    LoadForm(new LandSoftBuilding.Lease.ShortTerm.frmManager());
                }
            }
        }

        private void itemThueNganHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cho thuê ngắn hạng", "Xem");
            LoadForm(new LandSoftBuilding.Lease.ShortTerm.frmManager());
        }

        private void itemDichVuKhacAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Dịch vụ khác", "Thêm");
            using (var frm = new DichVu.Khac.frmEdit())
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách dịch vụ khác", "Xem");
                    LoadForm(new DichVu.Khac.frmManager());
                }
            }
        }

        private void itemHoaDon_ThongKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ dịch vụ", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.Reports.frmCongNo());
        }

        private void itemReport_PhiDaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết các khoản đã thu dịch vụ", "Xem");
            LoadForm(new DichVu.Reports.frmPhieuThu());
        }

        private void itemReportDaThuChungCu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo các khoản đã thu của Dự án", "Xem");
            LoadForm(new DichVu.frmBaoCaoCacKhoanDaThuChungCu());
        }

        private void itemReport_BaoCaoDatCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê công nợ đặt cọc", "Xem");
            LoadForm(new LandSoftBuilding.Lease.Reports.frmCongNoDatCoc());
        }

        private void itemReport_HopDongChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê cho thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.Reports.frmManager());
        }

        private void itemNuoc_UuDai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách ưu đãi của dịch vụ nước", "Xem");
            LoadForm(new DichVu.Nuoc.frmUuDai());
        }

        private void itemDonViTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đơn vị tính", "Xem");
            LoadForm(new ToaNha.frmDonViTinh());
        }

        private void itemBangGiaDichVuCoBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách bảng giá dịch vụ Dự án", "Xem");
            LoadForm(new ToaNha.frmBangGiaDichVu());
        }

        private void itemDien3Pha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện 3 pha", "Xem");
            LoadForm(new DichVu.Dien.Dien3Pha.frmManager());
        }

        private void itemDien3Pha_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức điện 3 pha", "Xem");
            LoadForm(new DichVu.Dien.Dien3Pha.frmDinhMuc());
        }

        private void itemThanhLyChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng thanh lý", "Xem");
            LoadForm(new LandSoftBuilding.Lease.Liquidate.frmManager());
        }

        private void itemChoThue_LTT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng đang thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.PaymentSchedule.frmManager());
        }

        private void itemBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách báo cáo", "Xem");
            this.LoadForm(new frmReportList());
        }

        private void itemNuocCachTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cách tinh nước", "Xem");
            this.LoadForm(new DichVu.Nuoc.frmCachTinh());
        }

        private void itemGiuXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách giữ xe", "Xem");
            this.LoadForm(new DichVu.GiuXe.frmManager());
        }

        private void itemPhanNhomMatBang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phân nhóm mặt bằng", "Xem");
            this.LoadForm(new DichVu.MatBang.PhanNhom.frmPhanNhom());
        }

        private void itemDienDongHo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đồng hồ điện", "Xem");
            this.LoadForm(new DichVu.Dien.frmDongHo());
        }

        private void itemNuocDongHo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đồng hồ nước", "Xem");
            this.LoadForm(new DichVu.Nuoc.frmDongHo());
        }

        private void itemNganHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm ngân hàng", "Thêm");
            using (var frm = new ToaNha.frmNganHang())
            {
                frm.ShowDialog();
            }
        }

        private void itemPhieuThuAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm phiếu thu", "Thêm");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Input.frmManager());
        }

        private void itemPhieuChiAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm phiếu chi", "Thêm");
            using (var frm = new LandSoftBuilding.Fund.Output.frmEdit() )
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.LoadForm(new LandSoftBuilding.Fund.Output.frmManager());
            }
        }

        private void itemPhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu chi", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Output.frmManager() );
        }

        private void itemKhauTruAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khấu trừ thu trước", "Thêm");
            using (var frm = new LandSoftBuilding.Fund.Deduct.frmEdit() )
            {
                frm.MaTN = Common.User.MaTN;
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    this.LoadForm(new LandSoftBuilding.Fund.Deduct.frmManager() );
            }
        }

        private void itemKhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu khấu trừ thu trước", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Input.frmManager_KhauTru() );
        }

        private void itemDichVu_Report_KhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void itemNuocNong_DongHo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đồng hồ nước", "Xem");
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmDongHo());
        }

        private void itemNuocNong_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức nước nóng", "Xem");
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmDinhMuc());
        }

        private void itemNuocNong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nước nóng", "Xem");
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmManager());
        }

        private void itemNuocSinhHoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nước sinh hoạt", "Xem");
            this.LoadForm(new DichVu.Nuoc.NuocSinhHoat.frmManager());
        }

        private void itemEmail_Config_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình Email", "Xem");
            this.LoadForm(new LandSoftBuilding.Marketing.Mail.Config.frmManager());
        }

        private void itemEmail_SettingStaff_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt Email nhận", "Xem");
            this.LoadForm(new LandSoftBuilding.Marketing.Mail.frmMailStaff());
        }

        private void itemEmail_Category_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chủ đề email marketing", "Xem");
            var frm = new LandSoftBuilding.Marketing.Mail.Templates.frmCategory();
            frm.ShowDialog();
        }

        private void itemEmail_Templates_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu email marketing", "Xem");
            this.LoadForm(new LandSoftBuilding.Marketing.Mail.Templates.frmManager());
        }

        private void itemReport_PhieuThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thu", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Reports.frmPhieuThu());
        }

        private void itemReport_PhieuChi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo phiếu thi", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Reports.frmPhieuChi());
        }

        private void itemReport_PhieuKhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thống kê khấu trừ", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Reports.frmKhauTru());
        }

        private void itemNhomKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhóm khách hàng", "Xem");
            //this.LoadForm(new KhachHang.frmNhomKH());
        }

        private void itemCaiDatDuyetHoaDon_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cài đặt duyệt hóa đơn", "Xem");
            this.LoadForm(new ToaNha.frmCaiDatDuyetHoaDon());
        }

        private void itemQuanHe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách quan hệ nhân khẩu", "Xem");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cài đặt yêu cầu", "Xem");
            this.LoadForm(new DichVu.YeuCau.frmCatDat());
        }

        private void itemNguonDen_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nguồn đến", "Xem");
            this.LoadForm(new DichVu.YeuCau.frmNguonDen());
        }

        private void itemLeTan_Add_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm khách ra vào Dự án", "Thêm");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách khách ra vào Dự án", "Xem");
            this.LoadForm(new DichVu.YeuCau.LeTan.frmManager());
        }

        private void itemChoThue_LTT_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch thanh toán cho thuê", "Xem");
            this.LoadForm(new LandSoftBuilding.Lease.PaymentSchedule.frmSchedule());
        }

        private void itemDienDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách điện điều hòa", "Xem");
            this.LoadForm(new DichVu.Dien.DieuHoa.frmManager());
        }

        private void itemDienDieuHoa_DinhMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách định mức điện", "Xem");
            this.LoadForm(new DichVu.Dien.DieuHoa.frmDinhMuc());
        }

        private void itemReport_KeHoachThuTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo kế hoạch thu tiền", "Xem");
            this.LoadForm(new LandSoftBuilding.Lease.Reports.frmKeHoachThuTien());
        }


        private void itemDanhSachLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch hẹn", "Xem");
            this.LoadForm(new Building.WorkSchedule.LichHen.frmManager());
        }

        private void itemThoiDiemLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thời điểm", "Xem");
            this.LoadForm(new Building.WorkSchedule.LichHen.ThoiDiem_frm());
        }

        private void itemPhanLoaiLich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chủ đề", "Xem");
            this.LoadForm(new Building.WorkSchedule.LichHen.ChuDe_frm());
        }

        private void itemPhanLoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loiaj nhiệm vụ", "Xem");
            this.LoadForm(new Building.WorkSchedule.NhiemVu.LoaiNhiemVu_frm());
        }

        private void itemTrangThai_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tình trạng", "Xem");
            this.LoadForm(new Building.WorkSchedule.NhiemVu.TinhTrang_frm());
        }

        private void itemMucDo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mức độ", "Xem");
            this.LoadForm(new Building.WorkSchedule.NhiemVu.MucDo_frm());
        }

        private void itemTienDo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tiến độ", "Xem");
            this.LoadForm(new Building.WorkSchedule.NhiemVu.frmTienDo());
        }

        private void itemThemMoi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm mới nhiệm vụ", "Thêm");
            this.LoadForm(new Building.WorkSchedule.NhiemVu.AddNew_frm());
        }

        private void itemDanhSach_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhiệm vụ", "Xem");
            this.LoadForm(new Building.WorkSchedule.NhiemVu.frmManager());
        }

        private void itemTaiLieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách biểu mẫu tài liệu", "Xem");
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmManager() { objnhanvien = User });
        }

        private void itemTaiLieu_Loai_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new LandsoftBuildingGeneral.DynBieuMau.frmLoaiBieuMau() { objnhanvien = User });
        }

        private void itemPhanQuyenBaoCao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân quyền báo cáo", "Xem");
            LoadForm(new LandSoftBuilding.Report.frmPhanQuyenBaoCao());
        }
        private void itemBaoCaoHDThueGianHangThueKHTHue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo hợp đồng thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.ReportsTT.frmManager());
        }

        private void itemGanHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê gần hết hạn", "Xem");
            LoadForm(new LandSoftBuilding.Lease.GanHetHan.frmManager());
        }

        private void itemDaHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê đã hết hạn", "Xem");
            LoadForm(new LandSoftBuilding.Lease.DaHetHan.frmManager());
        }

        private void itemBaoCaoTienVeCongTy_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemHoaDonDaXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn đã xóa", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.frmHoaDonDaXoa());
        }

        private void itemPhieuThuDaXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu đã xóa", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Input.frmPhieuThuDaXoa());
        }

        private void itemTheXeDaXoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ xe đã xóa", "Xem");
            LoadForm(new DichVu.GiuXe.frmTheXeDaXoa());
        }

        private void itemBaoCaoCongNoTongHopTheoThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void itemQuocTich_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách quốc tịch của nhân khẩu", "Xem");
            LoadForm(new DichVu.NhanKhau.frmQuocTich());
        }

        private void itemLoaiYeuCau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại yêu cầu lễ tân", "Xem");
             LoadForm(new DichVu.YeuCau.frmLoaiYeuCau());
        }

        private void itemMucDoLeTan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mức độ yêu cầu lễ tân", "Xem");
            LoadForm(new DichVu.YeuCau.LeTan.frmMucDo());
        }

        private void itemTrangThaiLeTan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách trạng thái lễ tân", "Xem");
            LoadForm(new DichVu.YeuCau.LeTan.frmTrangThaiLeTan());
        }

        private void itemTheThangMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ tháng máy", "Xem");
            LoadForm(new DichVu.ThangMay.frmManager() { objnhanvien = User });
        }

        private void itemTheTichHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách thẻ tích hợp", "Xem");
            LoadForm(new DichVu.TheTichHop.frmManager() { objnhanvien = User });
        }

        private void itemKhoThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách kho thẻ", "Xem");
            LoadForm(new DichVu.GiuXe.frmKhoThe());
        }

        private void itemDSCapThe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách cấp thẻ", "Xem");
            LoadForm(new DichVu.GiuXe.frmDSCapThe());

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thoát phần mềm", "Thoát");
        }

        private void itemNhatKyHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhật ký hệ thống", "Xem");
            LoadForm(new Building.SystemLog.ctlSysLog());
        }

        private void itemLoaiKhachHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại khách hàng", "Xem");
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
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhật ký cuộc gọi", "Xem");
            //LoadForm(new DIP.SwitchBoard.frmHistoryControl());
        }

        private void bbiDanhSachYeuCau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách yêu cầu của khách hàng", "Xem");
            LoadForm(new DichVu.YeuCau.frmManager());
        }

        private void bbiCongViecCuaToi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công việc của tôi", "Xem");
            LoadForm(new DichVu.YeuCau.frmCongViecNhanVien());
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LoadForm(new EmailAmazon.Templates.frmManager());
        }

        private void itemDanhSachNhan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhận mail", "Xem");
            LoadForm(new EmailAmazon.Receive.frmReceive());
        }

        private void itemThuongHieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Thương hiệu mail amazon", "Xem");
            LoadForm(new EmailAmazon.Brand.frmManager());
        }

        private void itemDanhSachGui_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Gửi mail", "Xem");
            LoadForm(new EmailAmazon.Sending.frmManager());
        }

        private void itemCheckTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            MailCommon.cmd = new EmailAmazon.API.APISoapClient();
            MailCommon.cmd.Open();
            DialogBox.Alert(string.Format("Tài khoản của bạn hiện có {0} email", (object)MailCommon.cmd.GetTaiKhoan_SoDu(MailCommon.MaHD, MailCommon.MatKhau)));
            //DialogBox.Alert(string.Format("{0}", (object)MailCommon.cmd.CheckTaiKhoan(MailCommon.MaHD, MailCommon.MatKhau)));
        }

        private void barButtonItem29_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            LoadForm(new LandSoftBuilding.Marketing.Mail.Config.LichSu());
        }

        private void itemCapNhatBangGiaDichVuCoBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new ToaNha.frmCapNhatBangGiaDichVu());
        }

        private void itemAppConfig_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new Building.AppVime.frmConfig();
            frm.ShowDialog();
        }

        private void itemTowerSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Building.AppVime.Tower.frmManager());
        }

        private void itemRegisterEmployeeAPP_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Building.AppVime.Employee.frmManager());
        }

        private void itemRegisterAPP_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Building.AppVime.frmManager());
        }

        private void itemVimeNewsGenereal_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Building.AppVime.NewsGeneral.frmManager());
        }

        private void itemAppNews_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new Building.AppVime.News.frmManager());
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new QuangForm());
        }

        private void itemServiceExtension_ItemClick(object sender, ItemClickEventArgs e)
        {
            //LoadForm(new Building.AppVime.ServiceExtension.frmManager());
        }

        private void itemApp_SettingService_ItemClick(object sender, ItemClickEventArgs e)
        {
            //LoadForm(new Building.AppVime.Services.frmManager());
        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tài liệu", "Xem");
            //LoadForm(new DichVu.KhachHang.frmTaiLieu() { objnhanvien = User });          
        }

        private void itemTienIch_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tiện ích", "Xem");
            //LoadForm(new Building.AppVime.ServiceBasic.frmManager());
        }

        private void itemHDCTNgoai_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new HopDongThueNgoai.frmManager());

        }

        private void itemHDCTNSapHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new HopDongThueNgoai.frmGanHetHan());
        }

        private void itemHopDongDaHetHan_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new HopDongThueNgoai.frmDaHetHan());
        }

        private void itemHDCTNCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new HopDongThueNgoai.frmDanhSachCongViec());
        }

        private void itemDSHDTNThanhLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadForm(new HopDongThueNgoai.frmThanhLy());
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thiết lập loại dịch vụ khấu trừ tự động", "Xem");
            LoadForm(new ToaNha.frmDichVuKhauTruTuDong());
        }

        private void itemCongNoDongTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Công nợ theo dõi dòng tiền mặt", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.frmManagerCongNoToaNha());
        }

        private void itemChayLaiSoQuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Library.frmChayLaiSoQuy_ThuChi frm = new frmChayLaiSoQuy_ThuChi();
            //frm.ShowDialog();
        }

        private void itemDSChuyenTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách chuyển tiền", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Transfer.frmManager());
        }

        private void itemPhieuChiKyQuy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu chi tiền ký quỹ", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Input.frmManager_TienKyQuy());
        }

        private void itemThongKeTheoNhomCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê phản ánh của cư dân theo nhóm công việc", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmNhomCongViec());
        }

        private void itemBDTinhTrangXuLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê phản ánh của cư dân theo tình trạng xử lý", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmTinhTrangXuLy());
        }

        private void itemTheoDanhGiaCuaCuDan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê theo đánh giá của cư dân", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmDanhGiaSao());
        }

        private void itemTheoNguonPhanAnh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê theo nguồn phản ánh", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmNguonDen());
        }

        private void itemDoUuTien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê theo độ ưu tiên", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmDoUuTien());
        }

        private void itemPhanAnhTheoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê phản ánh theo tòa nhà", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmPhanAnhTheoToaNha());
        }

        private void itemNhomCongViecMuiltiTN_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê nhóm công việc theo tòa nhà", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmNhomCongViecMuiltiTN());
        }

        private void itemTinhTrangXuLyTheoNhieuToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê tình trạng xử lý theo tòa nhà", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmTinhTrangXuLyMuiltiTN());
        }

        private void itemDanhGiaCuaCuDanTheoMuiltiToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê đánh giá của cư dân theo tòa nhà", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmDanhGiaSaoMuiltiTN());
        }

        private void itemNguonTiepNhanTheoMuilti_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê nguồn tiếp nhận theo tòa nhà", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.NhieuToaNha.frmNguonTiepNhanMuiltiTN());
        }

        private void itemViewBieuDoAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê nguồn tiếp nhận theo tòa nhà", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.MotToaNha.frmViewAll());
        }

        private void itemTTTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tình trạng tài sản", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmTinhTrangTaiSan());
        }

        private void itemNhaCCTS_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhà cung cấp tài sản", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmNhaCungCapTaiSan());
        }

        private void itemDMHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Hệ thống tài sản", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmNhomTaiSan());
        }

        private void itemDMLoaiTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách loại tài sản", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmLoaiTaiSan());
        }

        private void itemDMTenTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tên tài sản", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmTenTaiSan());
        }

        private void ItemCaiDatHeThongChoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt hệ thống", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmNhomTaiSan_CaiDat());
        }

        private void itemDMChiTietTaiSan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Chi tiết tài sản", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmListTaiSan());
        }

        private void ItemTanSuat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tần suất", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmTanSuat());
        }

        private void itemNhomCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm công việc", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmNhomCongViec());
        }

        private void itemLoaiCaTruc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục Loại ca trực", "Xem");
            LoadForm(new Building.Asset.PhanCong.frmPhanCong_PLCV());
        }

        private void itemCongCuThietBi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Công cụ thiết bị", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmThietBi_Manager());
        }

        private void itemProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Profile", "Xem");
            LoadForm(new Building.Asset.BaoTri.frmProfile_Manager());
        }

        private void itemGiaoNhanCa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách Bàn giao ca", "Xem");
            LoadForm(new Building.Asset.PhanCong.frmBanGiaoCa_Manager());
        }

        private void itemDeXuatDoiCa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đề xuất đổi ca", "Xem");
            LoadForm(new Building.Asset.PhanCong.frmDeXuatDoiCa_Manager());
        }

        private void itemLichTruc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch trực", "Xem");
            LoadForm(new Building.Asset.PhanCong.frmLichTruc_Manager());
        }

        private void itemBangPhanCong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bảng phân công", "Xem");
            LoadForm(new Building.Asset.PhanCong.frmPhanCong());
        }

        private void ItemKeHoachVanHanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách vận hành", "Xem");
            LoadForm(new Building.Asset.VanHanh.frmKeHoachVanHanh_Manager());
        }

        private void itemPhieuVanHanh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu vận hành", "Xem");
            LoadForm(new Building.Asset.VanHanh.frmPhieuVanHanh_Manager());
        }

        private void itemVHKeHoachBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kế hoạch bảo trì", "Xem");
            LoadForm(new Building.Asset.BaoTri.frmKeHoachBaoTri_Manager());
        }

        private void itemVHPhieuBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu bảo trì", "Xem");
            LoadForm(new Building.Asset.BaoTri.frmPhieuBaoTri_Manager());
        }

        private void itemGanProFileChoHeThong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Gán profile cho hệ thống tài sản", "Xem");
            LoadForm(new Building.Asset.BaoTri.frmGanProfileChoHeThong());
        }

        private void itemDanhMucProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Profile Hệ thống", "Xem");
            LoadForm(new Building.Asset.BaoTri.frmProfile_DM_Manager());
        }

        private void itemGanProfileChoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Profile gán cho tòa nhà", "Xem");
            LoadForm(new Building.Asset.BaoTri.frmProfile_GanChoToaNha());
        }

        private void itemNhomProfile_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm profile", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmProfileGroup());
        }

        private void itemCauHinhCapDuyet_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình cấp duyệt", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmManagerCauHinhDuyet());
        }

        private void itemVTDonViTinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục Đơn Vị Tính", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmDVT());
        }

        private void itemVTDanhMucVatTu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục Vật tư", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmVatTu_Manager());
        }

        private void itemDeXuatMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách đề xuất", "Xem");
            LoadForm(new Building.Asset.VatTu.frmDeXuat_Manager());
        }

        private void itemDanhSachMuaHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mua hàng", "Xem");
            LoadForm(new Building.Asset.VatTu.frmMuaHang_Manager());
        }

        private void itemVTNhapKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhập kho", "Xem");
            LoadForm(new Building.Asset.VatTu.frmNhapKho_Manager());
        }

        private void itemVTDanhMucLoaiNhapKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục loại nhập kho", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmLoaiNhap());
        }

        private void itemVTDanhMucKhoHang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục kho hàng", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmKhoHang());
        }

        private void itemVTDanhMucLoaiXuatKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mục đích xuất kho", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmLoaiXuat());
        }

        private void itemVTXuatKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách xuất kho", "Xem");
            LoadForm(new Building.Asset.VatTu.frmXuatKho_Manager());
        }

        private void itemVTTonKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tồn kho", "Xem");
            LoadForm(new Building.Asset.VatTu.frmTonKho_Manager());
        }

        private void itemCauHinhAPITheXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình API thẻ xe", "Xem");
            LoadForm(new DichVu.GiuXe.frmConfigAPI());
        }

        private void itemQuanLyHoSo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Quản lý hồ sơ", "Xem");
            LoadForm(new Building.Asset.HoSo.frmHoSo_Manager());
        }

        private void itemQLHS_KhoGiay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Khổ giấy", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmKhoGiay());
        }

        private void itemQLHS_HoSo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục hồ sơ", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmDanhMucHoSo());
        }

        private void itemQLHS_DayKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục dãy - kệ", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmKhoDayKe());
        }

        private void itemQLHS_MucDoBaoMat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mức độ bảo mật", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmMucDoBaoMat());
        }

        private void itemQLHS_MucDoKhanCap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Mức độ khẩn cấp", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmDoKhanCap());
        }

        private void itemQLHS_LoaiVanBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phân loại văn bản", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmLoaiVanBan());
        }

        private void itemXuatKhoSuDung_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách xuất kho sử dụng", "Xem");
            LoadForm(new Building.Asset.VatTu.frmXuatKhoSuDung_Manager());

        }

        private void itemDeXuatSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Đề xuất sửa chữa", "Xem");
            LoadForm(new Building.Asset.BaoTri.frmDeXuatSuaChua_Manager());
        }

        private void itemCauHinhNgayNghi_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình danh mục ngày nghỉ", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmThietLapNgayNghi());
        }

        private void itemCauHinhNgayNghiTheoToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình ngày nghỉ theo tòa nhà", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmThietLapNgayNghi_TheoToaNha());
        }

        private void itemThongKeThoiGian_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thống kê thời gian", "Xem");
            LoadForm(new DichVu.YeuCau.BieuDo.ThoiGian.frmBieuDoThoiGian());
        }

        private void itemNhomCongViec_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm công việc", "Xem");
            LoadForm(new DichVu.YeuCau.FrmAppGroupProcess());
        }

        private void itemCauHinhTinhTrangPhieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm trạng thái phiếu vận hành ", "Xem");
            LoadForm(new Building.Asset.DanhMuc.frmStatusLevels());
        }

        private void itemTKCheckList_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo checklist hằng ngày ", "Xem");
            LoadForm(new Building.Asset.BaoCao.frmBaoCao_CheckListVanHanhHangNgay());
        }

        private void itemTKTinhHinhThucHien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tình hình công việc ", "Xem");
            LoadForm(new DichVu.YeuCau.frmTinhHinhCongViec());
        }

        private void itemTKTinhHinhKiemTraDinhKy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tình hình kiểm tra định kỳ ", "Xem");
            LoadForm(new Building.Asset.BaoCao.frmTinhHinhKiemTraDinhKy());
        }

        private void itemViewKeHoachBaoTri_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("View kiểm tra định kỳ ", "Xem");
            LoadForm(new Building.Asset.BaoCao.frmKiemTraDinhKy());
        }

        private void itemTinTuc_TyLeDangTin_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức tỷ lệ đăng tin ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TyLeDangTin());
        }

        private void itemTinTuc_ThongKeTheoTungToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức theo từng tòa nhà ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TinTucTungToaNha());
        }

        private void itemTinTuc_ThongKeTheoTongToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức theo tổng tòa nhà ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TinTucToaNha());
        
        }

        private void itemTinTuc_ThongKeTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tin tức theo tổng hợp ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TinTucTong());
        }

        private void itemApp_TyLeSuDungTheoTungToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tỷ lệ sử dụng từng tòa của App ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TyLeSuDungTungToaNha());
        }

        private void itemApp_TyLeSuDungTongToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tỷ lệ sử dụng tổng của App ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_TyLeSuDungTong());
        }

        private void itemApp_ThongKeSuDungTungToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê sử dụng từng tòa của App ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_ThongKeSuDungTungToaNha());
        }

        private void itemApp_ThongKeSuDungCacToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê sử dụng các tòa của App ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_ThongKeSuDungCacToaNha());
        }

        private void itemNhieuTN_SoLuongNhomCongViec_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê Số lượng phản ánh theo nhóm công việc ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_CountNhomCongViec());
        }

        private void itemNhieuTN_PhanAnhTheoThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê phản ánh theo tháng ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDo_ThongKePhanAnhTheoThang());
        }

        private void itemNhieuTN_TongPhanAnh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê tổng phản ánh ", "Xem");
            LoadForm(new Building.Asset.BieuDo.frmBieuDoTongPhanAnh());
        }

        private void itemNhieuTN_BaoCaoTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ thống kê tổng hợp ", "Xem");
            LoadForm(new Building.Asset.BaoCao.frmBaoCaoVanHanh());
        }

        private void itemViewKeHoachBaoTri_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("View kế hoạch bảo trì ", "Xem");
            LoadForm(new Building.Asset.BaoCao.frmViewKeHoachBaoTri());
        }

        private void itemSMSTruongTron_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định nghĩa trường trộn ", "Xem");
            LoadForm(new LandSoftBuilding.Marketing.SMS.frmFields());
        }

        private void itemSMSSoDuTaiKhoan_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Số dư tài khoản ", "Xem");
            LandSoftBuilding.Marketing.sms.frmMoney frm = new LandSoftBuilding.Marketing.sms.frmMoney();
            frm.ShowDialog();
        }

        private void itemSMS_Mau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Định nghĩa mẫu SMS ", "Xem");
            LandSoftBuilding.Marketing.SMS.frmTemplates frm = new LandSoftBuilding.Marketing.SMS.frmTemplates();
            frm.ShowDialog();
        }

        private void itemSMSLichSu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch sử gửi SMS ", "Xem");
            LoadForm(new LandSoftBuilding.Marketing.sms.frmHistory());
        }

        private void BtnCauHinhColor_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình Color dịch vụ", "Xem");
            LoadForm(new DichVu.FrmCaiDatColor());
        }

        private void ItemCaiDatBieuMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình biểu mẫu", "Xem");
            LoadForm(new BuildingDesignTemplate.FrmManager());
            //DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái xác nhận", "Xem");
            //LoadForm(new DichVu.BanGiaoMatBang.FrmScheduleConfirm());
        }

        private void ItemScheduleStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái gửi lịch", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmScheduleStatus());
        }

        private void ItemScheduleComfirm_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái xác nhận", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmScheduleConfirm());
        }

        private void ItemScheduleGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Nhóm lịch bàn giao", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmScheduleGroup());
        }

        private void ItemSchedule_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thiết lập lịch bàn giao", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmSchedule());
        }

        private void itemKeHoachBanGiao_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kế hoạch bàn giao mặt bằng", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmPlan());
        }

        private void ItemScheduleComfirms_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái xác nhận", "Xem");
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
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch sử bàn giao", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHistoryLocal());
        }

        private void itemCarTyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại xe", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Xe.frmLoaiXe());
        }

        private void itemCarSetup_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình phương tiện di chuyển", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Xe.frmCauHinhLX());
        }

        private void itemCarSchedule_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình lịch di chuyển", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Xe.frmCauHinhLichDC());
        }

        private void itemNewsManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Quản lý tin tức", "Xem");
            LoadForm(new ToaNha.TinTuc.frmTinTuc());
        }

        private void itemNewsTyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại tin tức", "Xem");
            LoadForm(new ToaNha.TinTuc.frmLoaiTinTuc());
        }

        private void itemEmailSetup_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt gửi email", "Xem");
            LoadForm(new DichVu.YeuCau.Email.FrmEmailSetup());
        }

        private void itemBcThuChiTm_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Sổ kế toán chi tiết quỹ tiền mặt", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Reports.FrmBcThuChiTm());
        }

        private void itemBcCongNoDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void itemBaoCaoDaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đã thu", "Xem");
            LoadForm(new LandSoftBuilding.Fund.Reports.FrmBcDaThu());
        }

        private void ItemBcChiTietThuPqlThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết PQL theo tháng", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBcChiTietThuPqlThang());
        }

        private void itemHoaDonThongKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ dịch vụ", "Xem");
            ////LoadForm(new LandSoftBuilding.Receivables.Reports.frmCongNo());
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBcCongNo());
        }

        private void ItemReportPhiDaThu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết các khoản đã thu dịch vụ", "Xem");
            LoadForm(new DichVu.Reports.frmPhieuThu());
        }

        private void ItemBaoCaoTongHopCongNoDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo tổng hợp công nợ dịch vụ", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmBcCongNoDichVu());
        }

        private void ItemBcCongNoTongHopTheoThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.Reports.frmCongNoTrongThang());
        }

        private void ItemReportChiTietCongNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("In sổ chi tiết công nợ", "Xem");
            var frm = new Building.PrintControls.PrintForm();
            frm.Text = "Sổ chi tiết công nợ";
            frm.PrintControl.FilterForm = new LandSoftBuilding.Receivables.Reports.frmChiTietCongNo();
            this.LoadForm(frm);
        }

        private void ItemBaoCaoCacKhoanDaKhauTru_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo khấu trừ", "Xem");
            this.LoadForm(new DichVu.Reports.frmKhauTru());
        }

        private void ItemReportBaoCaoTienDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thu chi tiền điện", "Xem");
            LoadForm(new DichVu.frmBaoCaoThuChiTienDien());
        }

        private void ItemRreportDienDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thu chi tiền điện điều hòa ngoài giờ", "Xem");
            LoadForm(new DichVu.frmBaoCaoThuChiTienDieuHoaNG());
        }

        private void ItemBieuDoTieuThuDien_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ điện", "Xem");
            this.LoadForm(new DichVu.Dien.BieuDo.frmBieuDoTieuThuDien());
        }

        private void ItemBieuDoTieuThuDien3Pha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ điện 3 pha", "Xem");
            this.LoadForm(new DichVu.Dien.BieuDo.frmBieuDoTieuThuDien3Pha());
        }

        private void itemBieuDoTieuThuDienDieuHoa_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ điện điều hòa", "Xem");
            this.LoadForm(new DichVu.Dien.BieuDo.frmBieuDoTieuThuDienDH());
        }

        private void itemBaoCaoDichVuNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo thu chi tiền nước", "Xem");
            LoadForm(new DichVu.frmBaoCaoThuChiTienNuoc());
        }

        private void itemBieuDoTieuThuNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ nước", "Xem");
            this.LoadForm(new DichVu.Nuoc.frmBieuDoMucTieuThuNuoc());
        }

        private void itemBieuDoTieuThuNuocNong_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ nước nước nóng", "Xem");
            this.LoadForm(new DichVu.Nuoc.NuocNong.frmBieuDoMucTieuThuNuocNong());
        }

        private void itemBieuDoTieuThuNuocSinhHoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ nước sinh hoạt", "Xem");
            this.LoadForm(new DichVu.Nuoc.NuocSinhHoat.frmBieuDoMucTieuThuNuocSinhHoat());
        }

        private void itemBaoCaoDichVuGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo dịch vụ Gas", "Xem");
            this.LoadForm(new DichVu.Gas.Reports.frmGAS());
        }

        private void itemBieuDoTieuThuGas_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Biểu đồ tiêu thụ Gas", "Xem");
            this.LoadForm(new DichVu.Gas.Reports.frmBieuDoGas());
        }

        private void itemBaoCaoDichVuGuiXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách tiền gửi xe", "Xem");
            LoadForm(new DichVu.Reports.frmTheXe());
        }

        private void itemSoCongNoTheXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ thẻ xe", "Xem");
            this.LoadForm(new DichVu.GiuXe.Reports.frmCongNoTheXe());
        }

        private void itemBaoCaoDaThuTheXePhatSinh_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo đã thu thẻ xe", "Xem");
            LoadForm(new DichVu.GiuXe.Reports.frmDaThuTheXe());
        }

        private void itemBaoCaoChiTietPhiGiuXeThang_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe tháng", "Xem");
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuXeThang());
        }

        private void itemBaoCaoDoanhThuGiuXeOTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe Oto", "Xem");
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuOtoThang());
        }

        private void itemBaoCaoDoanhThuGiuXeMay_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe máy", "Xem");
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuXeMayThang());
        }

        private void itemBaoCaoDoanhThuGiuXeDap_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo chi tiết phí giữ xe đạp", "Xem");
            LoadForm(new LandSoftBuilding.Report.TheXe.FrmBaoCaoChiTietPhiGiuXeDapThang());
        }

        private void itemCauHinhNhanVienQuanLyNhanMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cấu hình nhân viên quản lý nhận mail dịch vụ", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.FrmNhanVienNhanEmail());
        }

        private void itemDrTyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Loại đặt cọc", "Xem");
            LoadForm(new Deposit.FrmDrTyle());
        }

        private void ItemDeposit_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu thu đặt cọc", "Xem");
            LoadForm(new Deposit.FrmDeposit());
        }

        private void itemBaoCaoCongNoDichVu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Báo cáo công nợ dịch vụ", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.Reports.FrmCongNoDichVu());
        }

        private void itemDepositDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu thu đặt cọc đã xóa", "Xem");
            LoadForm(new Deposit.FrmDepositDelete());
        }

        private void itemDepositManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu thu đặt cọc đã xóa", "Xem");
            LoadForm(new Deposit.FrmDepositManager());
        }

        private void ItemWithDraw_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Phiếu chi đặt cọc", "Xem");
            LoadForm(new Deposit.FrmWithDraw());
        }

        private void ItemSoQuyDatCoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Sổ quỹ đặt cọc", "Xem");
            LoadForm(new Deposit.Report.FrmBcThuChiTmDc());
        }

        private void itemHangMuc_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh mục hạng mục", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmChecklistDetails());
        }

        private void itemChecklist_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu checklist", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmCheckList());
        }

        private void ItemChecklistToaNha_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Checklist Tòa nhà", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Checklist.FrmBuildingChecklist());
        }

        private void ItemHandoverLocal_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bàn giao nội bộ", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHandover());
        }

        private void ItemUserHandover_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cài đặt nhân viên bàn giao", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.UserHandover.FrmUserHandover());
        }

        private void ItemPlanCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Kế hoạch bàn giao khách hàng", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Customer.FrmPlan());
        }

        private void ItemScheduleCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch bàn giao khách hàng", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Customer.FrmSchedule());
        }

        private void ItemHandoverCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Bàn giao khách hàng", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Customer.FrmHandover());
        }

        private void ItemDuty_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Ca bàn giao", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Category.FrmDuty());
        }

        private void itemStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Trạng thái bàn giao", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Category.FrmStatus());
        }

        private void ItemHistoryCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Lịch sử bàn giao khách hàng", "Xem");
            LoadForm(new DichVu.BanGiaoMatBang.Local.FrmHistoryCustomer());
        }

        private void itemHopDongChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng cho thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.frmManager());
        }

        private void itemHopDongChoThue_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng đang thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.PaymentSchedule.frmManager());
        }

        private void itemLichThanhToanChoThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách lịch thanh toán cho thuê", "Xem");
            this.LoadForm(new LandSoftBuilding.Lease.PaymentSchedule.frmSchedule());
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê gần hết hạn", "Xem");
            LoadForm(new LandSoftBuilding.Lease.GanHetHan.frmManager());
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hợp đồng thuê đã hết hạn", "Xem");
            LoadForm(new LandSoftBuilding.Lease.DaHetHan.frmManager());
        }

        private void itemThanhLy_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng thanh lý", "Xem");
            LoadForm(new LandSoftBuilding.Lease.Liquidate.frmManager());
        }

        private void itemMatBangDangThue_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng đang thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.PaymentSchedule.frmManager());
        }

        private void itemLapHoaDonTuDong_ItemClick(object sender, ItemClickEventArgs e)
        {
            LandSoftBuilding.Receivables.frmAddAutoHDT FRM = new LandSoftBuilding.Receivables.frmAddAutoHDT();
            FRM.IsHDThue = true;
            FRM.ShowDialog();
        }

        private void itemHoaDon_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn dịch vụ khách hàng HĐT", "Xem");
            this.LoadForm(new LandSoftBuilding.Receivables.HopDongThue.frmManagerHDT(), "Hóa đơn dịch vụ - Hợp đồng thuê");
        }

        private void itemPhieuThuHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu HĐT", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Input.HopDongThue.frmManagerHDT(), "Phiếu thu - Hợp đồng thuê ");
        }

        private void itemPhieuThuDaXoaHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu thu đã xóa HĐT", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Input.frmPhieuThuDaXoaHDT(), "Phiếu thu đã xóa - Hợp đồng thuê");
        }

        private void itemPhieuChiHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách phiếu chi", "Xem");
            this.LoadForm(new LandSoftBuilding.Fund.Output.PhieuChi.frmManagerHDT(), "Phiếu chi - Hợp đồng thuê");
        }

        private void itemHoaDonDaXoaHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách hóa đơn đã xóa", "Xem");
            this.LoadForm(new LandSoftBuilding.Receivables.frmHoaDonDaXoaHDT(), "Hóa đơn đã xóa - Hợp đồng thuê");
        }

        private void itemCongNoHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách công nợ tổng hợp theo khách hàng", "Xem");
            LoadForm(new LandSoftBuilding.Receivables.frmReceivablesHDT() { IsHDThue = true }, "Công nợ tổng hợp theo khách hàng - Hợp đồng thuê");
        }

        private void ItemTongHop_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mặt bằng tổng hợp theo diện tích", "Xem");
            LoadForm(new DichVu.MatBang.frmDienTichSuDung());
        }

        private void itemNMB_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách nhóm mặt bằng", "Xem");
            LoadForm(new MatBang.frmNhomMatBang() { objnhanvien = User });
        }

        private void itemBieuMauHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu hợp đồng thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.Mau.frmManager());
        }

        private void itemTruongTronHDT_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu hợp đồng thuê", "Xem");
            LoadForm(new LandSoftBuilding.Lease.Mau.frmFieldDefine());
        }

        private void itemMau_ItemClick(object sender, ItemClickEventArgs e)
        {
            DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Danh sách mẫu hợp đồng thuê new", "Xem");
            LoadForm(new LandSoftBuilding.Lease.MauNew.Template());
        }

        private void itemPhieuDieuChuyen_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void itemPhieuKhauTruHDT_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
