using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
//using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using Library;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class FrmPhiPhaiThuTrongThang : DevExpress.XtraEditors.XtraForm
    {
        //private DataTable _data;
        private List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.KhachHang> khachHangs = new List<Class.PhiPhaiThuTrongThang.KhachHang>();
        private object _objLock = new object();
        private bool _isStop = false;
        private int _taskIndex;
        private System.Collections.Generic.List<System.ComponentModel.BackgroundWorker> _threads = new List<BackgroundWorker>();
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.List> objList;
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiQuanLy> phiQuanLies;
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiDien> phiDiens;
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiNuoc> phiNuocs;
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiXe> phiXeOtos;
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiVangLai> phiVangLais;
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiKhac> phiKhacs;
        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.MatBangItem> _lMatBang;
        public FrmPhiPhaiThuTrongThang()
        {
            InitializeComponent();
            LoadDesign();
            #region Khởi tạo đa luồng

            for (int i = 0; i <= 7; i++)
            {
                var thread = new System.ComponentModel.BackgroundWorker();
                thread.DoWork += Thread_DoWork;
                thread.RunWorkerCompleted += Thread_RunWorkerCompleted;
                _threads.Add(thread);
            }

            #endregion
        }

        private void FrmBcCongNo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);

            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);

            LoadData();
        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isStop) { if (!_threads.Any(_ => _.IsBusy)) GetData(); }
            else ((System.ComponentModel.BackgroundWorker)sender).RunWorkerAsync();
        }

        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            GetTask();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.List> GetDataCongNos(System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.KhachHang> objKH, System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiQuanLy> phiQuanLy, System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiDien> phiDien, System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiNuoc> phiNuoc, System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiXe> phiXeOto, System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiVangLai> xeVangLai, System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.MatBangItem> lMatBang, System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiKhac> phiKhac)
        {
            var db = new MasterDataContext();
            return (from kh in objKH
                    join mb in lMatBang on kh.MaMB equals mb.MaMB into matBang
                    from mb in matBang.DefaultIfEmpty()
                    join mb1 in db.mbMatBangs on kh.MaMB equals mb1.MaMB into matBang1
                    from mb1 in matBang1.DefaultIfEmpty()
                    join pql in phiQuanLy on new { kh.MaKH, kh.MaMB } equals new { pql.MaKH, pql.MaMB } into phiQl
                    from pql in phiQl.DefaultIfEmpty()
                    join pd in phiDien on new { kh.MaKH, kh.MaMB } equals new { pd.MaKH, pd.MaMB } into phiD
                    from pd in phiD.DefaultIfEmpty()
                    join pn in phiNuoc on new { kh.MaKH, kh.MaMB } equals new { pn.MaKH, pn.MaMB } into phiN
                    from pn in phiN.DefaultIfEmpty()
                    join oto1 in phiXeOto on new { kh.MaKH, kh.MaMB } equals new { oto1.MaKH, oto1.MaMB } into xeOTo1
                    from oto1 in xeOTo1.DefaultIfEmpty()
                    join xvl in xeVangLai on new { kh.MaKH, kh.MaMB } equals new { xvl.MaKH, xvl.MaMB } into xeVLai
                    from xvl in xeVLai.DefaultIfEmpty()
                    join pk in phiKhac on new { kh.MaKH, kh.MaMB } equals new { pk.MaKH, pk.MaMB } into pKhac
                    from pk in pKhac.DefaultIfEmpty()
                    select new LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.List
                    {
                        MaCan = mb != null ? mb.MaSoMB : "",
                        ChuHo = kh.ChuHo,
                        PhiQuanLyDienTich = mb1 != null && mb1.MaKH == kh.MaKH? mb1.DienTich : 0,
                        PhiQuanLyDonGia = pql != null ? pql.PhiQuanLyDonGia : 0,
                        PhiQuanLyPql = pql != null ? pql.SoTien : 0,

                        PhiDienCsc = pd != null ? pd.PhiDienCsc : 0,
                        PhiDienCsm = pd != null ? pd.PhiDienCsm : 0,
                        PhiDienTieuThu = pd != null ? pd.SoTieuThu : 0,
                        PhiDienDonGia = pd != null ? pd.DonGia : 0,
                        PhiDienThanhTien = pd != null ? pd.ThanhTien : 0,

                        PhiNuocCsc = pn != null ? pn.PhiNuocCsc : 0,
                        PhiNuocCsm = pn != null ? pn.PhiNuocCsm : 0,
                        PhiNuocTieuThu = pn != null ? pn.PhiNuocTieuThu : 0,
                        PhiNuocNhanKhau = pn != null ? pn.PhiNuocNhanKhau : 0,
                        PhiNuocHuongDm = pn != null ? pn.PhiNuocHuongDm : 0,
                        PhiNuocTongNuoc = pn != null ? pn.PhiNuocTongNuoc : 0,

                        PhiNuocSoLuongDm1 = pn != null ? pn.PhiNuocSoLuongDm1 : 0,
                        PhiNuocDonGiaDm1 = pn != null ? pn.PhiNuocDonGiaDm1 : 0,
                        PhiNuocThanhTienDm1 = pn != null ? pn.PhiNuocThanhTienDm1 : 0,

                        PhiNuocSoLuongDm2 = pn != null ? pn.PhiNuocSoLuongDm2 : 0,
                        PhiNuocDonGiaDm2 = pn != null ? pn.PhiNuocDonGiaDm2 : 0,
                        PhiNuocThanhTienDm2 = pn != null ? pn.PhiNuocThanhTienDm2 : 0,

                        PhiNuocSoLuongDm3 = pn != null ? pn.PhiNuocSoLuongDm3 : 0,
                        PhiNuocDonGiaDm3 = pn != null ? pn.PhiNuocDonGiaDm3 : 0,
                        PhiNuocThanhTienDm3 = pn != null ? pn.PhiNuocThanhTienDm3 : 0,

                        PhiNuocSoLuongDm4 = pn != null ? pn.PhiNuocSoLuongDm4 : 0,
                        PhiNuocDonGiaDm4 = pn != null ? pn.PhiNuocDonGiaDm4 : 0,
                        PhiNuocThanhTienDm4 = pn != null ? pn.PhiNuocThanhTienDm4 : 0,

                        PhiXeThangOToTheoDinhMucSoLuong = oto1 != null ? oto1.PhiXeThangOToTheoDinhMucSoLuong : 0,
                        PhiXeThangOToTheoDinhMucDonGia = oto1 != null ? oto1.PhiXeThangOToTheoDinhMucDonGia : 0,
                        PhiXeThangOToTheoDinhMucThanhTien = oto1 != null ? oto1.PhiXeThangOToTheoDinhMucThanhTien : 0,

                        PhiXeThangOToNgoaiDinhMucSoLuong = oto1 != null ? oto1.PhiXeThangOToNgoaiDinhMucSoLuong : 0,
                        PhiXeThangOToNgoaiDinhMucDonGia = oto1 != null ? oto1.PhiXeThangOToNgoaiDinhMucDonGia : 0,
                        PhiXeThangOToNgoaiDinhMucThanhTien = oto1 != null ? oto1.PhiXeThangOToNgoaiDinhMucThanhTien : 0,

                        PhiXeThangXeMayTheoDinhMucSoLuong = oto1 != null ? oto1.PhiXeThangXeMayTheoDinhMucSoLuong : 0,
                        PhiXeThangXeMayTheoDinhMucDonGia = oto1 != null ? oto1.PhiXeThangXeMayTheoDinhMucDonGia : 0,
                        PhiXeThangXeMayTheoDinhMucThanhTien = oto1 != null ? oto1.PhiXeThangXeMayTheoDinhMucThanhTien : 0,

                        PhiXeThangXeMayNgoaiDinhMucSoLuong = oto1 != null ? oto1.PhiXeThangXeMayNgoaiDinhMucSoLuong : 0,
                        PhiXeThangXeMayNgoaiDinhMucDonGia = oto1 != null ? oto1.PhiXeThangXeMayNgoaiDinhMucDonGia : 0,
                        PhiXeThangXeMayNgoaiDinhMucThanhTien = oto1 != null ? oto1.PhiXeThangXeMayNgoaiDinhMucThanhTien : 0,

                        PhiXeThangXeDapSoLuong = oto1 != null ? oto1.PhiXeThangXeDapSoLuong : 0,
                        PhiXeThangXeDapDonGia = oto1 != null ? oto1.PhiXeThangXeDapDonGia : 0,
                        PhiXeThangXeDapThanhTien = oto1 != null ? oto1.PhiXeThangXeDapThanhTien : 0,

                        PhiXeThangTongTien = oto1 != null ? oto1.PhiXeThangTongTien : 0,

                        PhiXeVangLai = xvl != null ? xvl.PhiXeVangLai : 0,

                        ThuKhacPhiVeSinh = xvl != null ? xvl.ThuKhacPhiVeSinh : 0,

                        ThuKhacPhiCoSoHaTang = xvl != null ? xvl.ThuKhacPhiCoSoHaTang : 0,

                        ThuKhacKhac = (xvl != null ? xvl.ThanhTien : 0) + (pk != null ? pk.ThuKhac : 0),
                        ThuKhacTong = (xvl != null ? xvl.ThanhTien : 0) + (pk != null ? pk.ThuKhac : 0),
                        TongCong = (pql != null ? pql.SoTien : 0) + (pd != null ? pd.ThanhTien : 0) + (pn != null ? pn.PhiNuocTongNuoc : 0) + (oto1 != null ? oto1.PhiXeThangTongTien : 0) + (xvl != null ? xvl.TongTien : 0) + (pk != null ? pk.ThuKhac : 0)
                    }).ToList();
        }

        private void GetData()
        {
            var wait = Library.DialogBox.WaitingForm();
            wait.Show();


                objList = GetDataCongNos(khachHangs, phiQuanLies, phiDiens, phiNuocs, phiXeOtos, phiVangLais, _lMatBang, phiKhacs); 


            gc.DataSource = objList;

            //itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            wait.Hide();
        }

        private void GetTask()
        {
            Library.MasterDataContext db = new MasterDataContext();
            db.CommandTimeout = 100000;

            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var toaNha = (byte)itemToaNha.EditValue;

            var index = 0;
            lock (_objLock) index = _taskIndex++;

            var param = new Dapper.DynamicParameters();

            switch (index)
            {
                // Khách hàng
                case 1:
                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    khachHangs = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.KhachHang>("dbo.bc_pttt_KhachHang", param).ToList();
                    break;

                // Phí quản lý
                case 2:

                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    param.Add("@MaLDV", 13, DbType.Int32, null, null);
                    phiQuanLies = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiQuanLy>("dbo.bc_pttt_PhiQuanLy", param).ToList();

                    break;

                // phí điện
                case 3:
                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    param.Add("@MaLDV", 8, DbType.Int32, null, null);
                    phiDiens = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiDien>("dbo.bc_pttt_PhiDien", param).ToList();

                    break;

                // phí nước
                case 4:
                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    param.Add("@MaLDV", 9, DbType.Int32, null, null);
                    phiNuocs = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiNuoc>("dbo.bc_pttt_PhiNuoc", param).ToList();

                    break;

                // phí xe
                case 5:
                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    param.Add("@MaLDV", 6, DbType.Int32, null, null);
                    phiXeOtos = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiXe>("dbo.bc_pttt_PhiXe", param).ToList();

                    break;

                // Xe vãng lai
                case 6:
                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    phiVangLais = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiVangLai>("dbo.bc_pttt_PhiVangLai", param).ToList();

                    break;

                // Đã thu
                case 7:

                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    param.Add("@TuNgay", tuNgay, DbType.DateTime, null, null);
                    param.Add("@DenNgay", denNgay, DbType.DateTime, null, null);
                    phiKhacs = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.PhiKhac>("dbo.bc_pttt_PhiKhac", param).ToList();

                    break;

                // Khấu trừ
                case 8:

                    param = new Dapper.DynamicParameters();
                    param.Add("@TowerId", toaNha, DbType.Byte, null, null);
                    _lMatBang = Library.Class.Connect.QueryConnect.Query<LandSoftBuilding.Receivables.Class.MatBangItem>("dbo.cn_Mat_Bang", param).ToList();

                    break;

                default:
                    _isStop = true;
                    break;
            }
        }

        private void LoadData()
        {
            try
            {
                _taskIndex = 1;
                _isStop = false;
                gc.DataSource = null;

                //itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                khachHangs = null;
                phiQuanLies = null;
                phiDiens = null;
                phiNuocs = null;
                phiXeOtos = null;
                phiVangLais = null;
                phiKhacs = null;
                objList = new System.Collections.Generic.List<LandSoftBuilding.Receivables.Class.PhiPhaiThuTrongThang.List>();

                foreach (var thread in _threads) thread.RunWorkerAsync();

                //itemNap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            catch { }
        }

        private void LoadDesign()
        {
            #region Tạo band, column

            #region band cấp 1 - top level bands
            // 
            //GridBand bandMaHopDong = new GridBand() { Caption = "MÃ HỢP ĐỒNG" };
            GridBand bandMaCan = new GridBand { Caption = "MÃ CĂN" };
            GridBand bandChuHo = new GridBand { Caption = "CHỦ HỘ" };
            GridBand bandPhiQuanLy = new GridBand() { Caption = "PHÍ QUẢN LÝ" };
            GridBand bandPhiDien = new GridBand() { Caption = "PHÍ ĐIỆN" };
            GridBand bandPhiNuoc = new GridBand() { Caption = "PHÍ NƯỚC" };
            GridBand bandPhiXeThang = new GridBand { Caption = "PHÍ XE THÁNG" };
            GridBand bandPhiXeVangLai = new GridBand { Caption = "Phí xe vãng lai" };
            GridBand bandThuKhac = new GridBand { Caption = "THU KHÁC" };
            GridBand bandTongCong = new GridBand { Caption = "TỔNG CỘNG" };
            gv.Bands.AddRange(new GridBand[] {  bandMaCan, bandChuHo, bandPhiQuanLy, bandPhiDien, bandPhiNuoc, bandPhiXeThang, bandPhiXeVangLai, bandThuKhac, bandTongCong }); //bandMaHopDong,

            #endregion

            #region band cấp 2 - nested bands

            // Phí quản lý
            GridBand bandPhiQuanLyDienTich = new GridBand { Caption = "DIỆN TÍCH" };
            GridBand bandPhiQuanLyDonGia = new GridBand { Caption = "ĐƠN GIÁ" };
            GridBand bandPhiQuanLyPql = new GridBand { Caption = "PQL" };

            // Phí điện
            GridBand bandPhiDienCsc = new GridBand { Caption = "CSC" };
            GridBand bandPhiDienCsm = new GridBand { Caption = "CSM" };
            GridBand bandPhiDienTieuThu = new GridBand { Caption = "TIÊU THỤ" };
            GridBand bandPhiDienDonGia = new GridBand { Caption = "ĐƠN GIÁ" };
            GridBand bandPhiDienThanhTien = new GridBand { Caption = "THÀNH TIỀN" };

            // phí nước
            GridBand bandPhiNuocCsc = new GridBand { Caption = "CSC" };
            GridBand bandPhiNuocCsm = new GridBand { Caption = "CSM" };
            GridBand bandPhiNuocTieuThu = new GridBand { Caption = "TIÊU THỤ" };
            GridBand bandPhiNuocNhanKhau = new GridBand { Caption = "Nhân khẩu" };
            GridBand bandPhiNuocHuongDm = new GridBand { Caption = "Hưởng ĐM" };
            GridBand bandPhiNuocDm1 = new GridBand { Caption = "ĐM1" };
            GridBand bandPhiNuocDm2 = new GridBand { Caption = "ĐM2" };
            GridBand bandPhiNuocDm3 = new GridBand { Caption = "ĐM3" };
            GridBand bandPhiNuocDm4 = new GridBand { Caption = "ĐM4" };
            GridBand bandPhiNuocTongNuoc = new GridBand { Caption = "TỔNG NƯỚC" };

            // Phí xe tháng
            GridBand bandPhiXeThangOTo = new GridBand { Caption = "Ô TÔ" };
            GridBand bandPhiXeThangXeMay = new GridBand { Caption = "XE MÁY" };
            GridBand bandPhiXeThangXeDap = new GridBand { Caption = "XE ĐẠP" };
            GridBand bandPhiXeThangTongTien = new GridBand { Caption = "TỔNG TIỀN XE THÁNG" };

            GridBand bandPhiXeThangOToTheoDinhMuc = new GridBand { Caption = "THEO ĐỊNH MỨC" };
            GridBand bandPhiXeThangOToNgoaiDinhMuc = new GridBand { Caption = "NGOÀI ĐỊNH MỨC" };

            GridBand bandPhiXeThangXeMayTheoDinhMuc = new GridBand { Caption = "THEO ĐỊNH MỨC" };
            GridBand bandPhiXeThangXeMayNgoaiDinhMuc = new GridBand { Caption = "NGOÀI ĐỊNH MỨC" };

            // Thu khác
            //GridBand bandThuKhacPhiVeSinh = new GridBand { Caption = "Phí vệ sinh" };
            //GridBand bandThuKhacPhiCoSoHaTang = new GridBand { Caption = "Phí cơ sở hạ tầng" };
            GridBand bandThuKhacKhac = new GridBand { Caption = "Khác" };
            GridBand bandThuKhacTong = new GridBand { Caption = "TỔNG" };

            // add band cấp 1
            bandPhiQuanLy.Children.AddRange(new GridBand[] { bandPhiQuanLyDienTich, bandPhiQuanLyDonGia, bandPhiQuanLyPql });
            bandPhiDien.Children.AddRange(new GridBand[] { bandPhiDienCsc, bandPhiDienCsm, bandPhiDienTieuThu, bandPhiDienDonGia, bandPhiDienThanhTien });
            bandPhiNuoc.Children.AddRange(new GridBand[] { bandPhiNuocCsc, bandPhiNuocCsm, bandPhiNuocTieuThu, bandPhiNuocNhanKhau, bandPhiNuocHuongDm, bandPhiNuocDm1, bandPhiNuocDm2, bandPhiNuocDm3, bandPhiNuocDm4, bandPhiNuocTongNuoc });
            bandPhiXeThang.Children.AddRange(new GridBand[] { bandPhiXeThangOTo, bandPhiXeThangXeMay, bandPhiXeThangXeDap, bandPhiXeThangTongTien });
            bandThuKhac.Children.AddRange(new GridBand[] { /*bandThuKhacPhiVeSinh, bandThuKhacPhiCoSoHaTang,*/ bandThuKhacKhac, bandThuKhacTong });

            bandPhiXeThangOTo.Children.AddRange(new GridBand[] { bandPhiXeThangOToTheoDinhMuc, bandPhiXeThangOToNgoaiDinhMuc });
            bandPhiXeThangXeMay.Children.AddRange(new GridBand[] { bandPhiXeThangXeMayTheoDinhMuc, bandPhiXeThangXeMayNgoaiDinhMuc });

            #endregion

            #region band cấp 3 - banded columns và làm nó visible

            // main
            BandedGridColumn colMaHopDong = new BandedGridColumn() { FieldName = "MaHopDong", Visible = true, Caption = " " };
            BandedGridColumn colMaCan = new BandedGridColumn() { FieldName = "MaCan", Visible = true, Caption = " " };
            BandedGridColumn colChuHo = new BandedGridColumn { FieldName = "ChuHo", Visible = true, Caption = " " };

            // phí quản lý
            BandedGridColumn colPhiQuanLyDienTich = new BandedGridColumn() { FieldName = "PhiQuanLyDienTich", Visible = true, Caption = " " };
            BandedGridColumn colPhiQuanLyDonGia = new BandedGridColumn() { FieldName = "PhiQuanLyDonGia", Visible = true, Caption = " " };
            BandedGridColumn colPhiQuanLyPql = new BandedGridColumn() { FieldName = "PhiQuanLyPql", Visible = true, Caption = "(1)" };

            // phí điện
            BandedGridColumn colPhiDienCsc = new BandedGridColumn { FieldName = "PhiDienCsc", Visible = true, Caption = " " };
            BandedGridColumn colPhiDienCsm = new BandedGridColumn { FieldName = "PhiDienCsm", Visible = true, Caption = " " };
            BandedGridColumn colPhiDienTieuThu = new BandedGridColumn { FieldName = "PhiDienTieuThu", Visible = true, Caption = " " };
            BandedGridColumn colPhiDienDonGia = new BandedGridColumn { FieldName = "PhiDienDonGia", Visible = true, Caption = " " };
            BandedGridColumn colPhiDienThanhTien = new BandedGridColumn { FieldName = "PhiDienThanhTien", Visible = true, Caption = "(2)" };

            // phí nước
            BandedGridColumn colPhiNuocCsc = new BandedGridColumn { FieldName = "PhiNuocCsc", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocCsm = new BandedGridColumn { FieldName = "PhiNuocCsm", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocTieuThu = new BandedGridColumn { FieldName = "PhiNuocTieuThu", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocNhanKhau = new BandedGridColumn { FieldName = "PhiNuocNhanKhau", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocHuongDm = new BandedGridColumn { FieldName = "PhiNuocHuongDm", Visible = true, Caption = " " };

            BandedGridColumn colPhiNuocSoLuongDm1 = new BandedGridColumn { FieldName = "PhiNuocSoLuongDm1", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocDonGiaDm1 = new BandedGridColumn { FieldName = "PhiNuocDonGiaDm1", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiNuocThanhTienDm1 = new BandedGridColumn { FieldName = "PhiNuocThanhTienDm1", Visible = true, Caption = " " };

            BandedGridColumn colPhiNuocSoLuongDm2 = new BandedGridColumn { FieldName = "PhiNuocSoLuongDm2", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocDonGiaDm2 = new BandedGridColumn { FieldName = "PhiNuocDonGiaDm2", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiNuocThanhTienDm2 = new BandedGridColumn { FieldName = "PhiNuocThanhTienDm2", Visible = true, Caption = " " };

            BandedGridColumn colPhiNuocSoLuongDm3 = new BandedGridColumn { FieldName = "PhiNuocSoLuongDm3", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocDonGiaDm3 = new BandedGridColumn { FieldName = "PhiNuocDonGiaDm3", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiNuocThanhTienDm3 = new BandedGridColumn { FieldName = "PhiNuocThanhTienDm3", Visible = true, Caption = " " };

            BandedGridColumn colPhiNuocSoLuongDm4 = new BandedGridColumn { FieldName = "PhiNuocSoLuongDm4", Visible = true, Caption = " " };
            BandedGridColumn colPhiNuocDonGiaDm4 = new BandedGridColumn { FieldName = "PhiNuocDonGiaDm4", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiNuocThanhTienDm4 = new BandedGridColumn { FieldName = "PhiNuocThanhTienDm4", Visible = true, Caption = " " };

            BandedGridColumn colPhiNuocTongNuoc = new BandedGridColumn { FieldName = "PhiNuocTongNuoc", Visible = true, Caption = "(3)" };

            // phí xe tháng
            BandedGridColumn colPhiXeThangOToTheoDinhMucSoLuong = new BandedGridColumn { FieldName = "PhiXeThangOToTheoDinhMucSoLuong", Visible = true, Caption = "SỐ LƯỢNG" };
            BandedGridColumn colPhiXeThangOToTheoDinhMucDonGia = new BandedGridColumn { FieldName = "PhiXeThangOToTheoDinhMucDonGia", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiXeThangOToTheoDinhMucThanhTien = new BandedGridColumn { FieldName = "PhiXeThangOToTheoDinhMucThanhTien", Visible = true, Caption = "THÀNH TIỀN" };
            BandedGridColumn colPhiXeThangOToNgoaiDinhMucSoLuong = new BandedGridColumn { FieldName = "PhiXeThangOToNgoaiDinhMucSoLuong", Visible = true, Caption = "SỐ LƯỢNG" };
            BandedGridColumn colPhiXeThangOToNgoaiDinhMucDonGia = new BandedGridColumn { FieldName = "PhiXeThangOToNgoaiDinhMucDonGia", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiXeThangOToNgoaiDinhMucThanhTien = new BandedGridColumn { FieldName = "PhiXeThangOToNgoaiDinhMucThanhTien", Visible = true, Caption = "THÀNH TIỀN" };

            BandedGridColumn colPhiXeThangXeMayTheoDinhMucSoLuong = new BandedGridColumn { FieldName = "PhiXeThangXeMayTheoDinhMucSoLuong", Visible = true, Caption = "SỐ LƯỢNG" };
            BandedGridColumn colPhiXeThangXeMayTheoDinhMucDonGia = new BandedGridColumn { FieldName = "PhiXeThangXeMayTheoDinhMucDonGia", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiXeThangXeMayTheoDinhMucThanhTien = new BandedGridColumn { FieldName = "PhiXeThangXeMayTheoDinhMucThanhTien", Visible = true, Caption = "THÀNH TIỀN" };
            BandedGridColumn colPhiXeThangXeMayNgoaiDinhMucSoLuong = new BandedGridColumn { FieldName = "PhiXeThangXeMayNgoaiDinhMucSoLuong", Visible = true, Caption = "SỐ LƯỢNG" };
            BandedGridColumn colPhiXeThangXeMayNgoaiDinhMucDonGia = new BandedGridColumn { FieldName = "PhiXeThangXeMayNgoaiDinhMucDonGia", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiXeThangXeMayNgoaiDinhMucThanhTien = new BandedGridColumn { FieldName = "PhiXeThangXeMayNgoaiDinhMucThanhTien", Visible = true, Caption = "THÀNH TIỀN" };

            BandedGridColumn colPhiXeThangXeDapSoLuong = new BandedGridColumn { FieldName = "PhiXeThangXeDapSoLuong", Visible = true, Caption = "SỐ LƯỢNG" };
            BandedGridColumn colPhiXeThangXeDapDonGia = new BandedGridColumn { FieldName = "PhiXeThangXeDapDonGia", Visible = true, Caption = "ĐƠN GIÁ" };
            BandedGridColumn colPhiXeThangXeDapThanhTien = new BandedGridColumn { FieldName = "PhiXeThangXeDapThanhTien", Visible = true, Caption = "THÀNH TIỀN" };

            BandedGridColumn colPhiXeThangTongTien = new BandedGridColumn { FieldName = "PhiXeThangTongTien", Visible = true, Caption = "(4)" };

            // phí xe vãng lai
            BandedGridColumn colPhiXeVangLai = new BandedGridColumn { FieldName = "PhiXeVangLai", Visible = true, Caption = "(5)" };

            // thu khác
            BandedGridColumn colThuKhacPhiVeSinh = new BandedGridColumn { FieldName = "ThuKhacPhiVeSinh", Visible = true, Caption = " " };
            BandedGridColumn colThuKhacPhiCoSoHaTang = new BandedGridColumn { FieldName = "ThuKhacPhiCoSoHaTang", Visible = true, Caption = " " };
            BandedGridColumn colThuKhacKhac = new BandedGridColumn { FieldName = "ThuKhacKhac", Visible = true, Caption = " " };
            BandedGridColumn colThuKhacTong = new BandedGridColumn { FieldName = "ThuKhacTong", Visible = true, Caption = "(6)" };

            // tổng cộng
            BandedGridColumn colTongCong = new BandedGridColumn { FieldName = "TongCong", Visible = true, Caption = "(7)=(1+2+3+4+5+6)" };

            // add gridview
            gv.Columns.AddRange(new BandedGridColumn[] { colMaHopDong, colMaCan, colChuHo, colPhiQuanLyDienTich, colPhiQuanLyDonGia, colPhiQuanLyPql, colPhiDienCsc, colPhiDienCsm, colPhiDienTieuThu, colPhiDienDonGia, colPhiDienThanhTien, colPhiNuocCsc, colPhiNuocCsm, colPhiNuocTieuThu, colPhiNuocNhanKhau, colPhiNuocHuongDm, colPhiNuocSoLuongDm1, colPhiNuocSoLuongDm2, colPhiNuocSoLuongDm3, colPhiNuocSoLuongDm4, colPhiNuocDonGiaDm1, colPhiNuocDonGiaDm2, colPhiNuocDonGiaDm3, colPhiNuocDonGiaDm4, colPhiNuocThanhTienDm1, colPhiNuocThanhTienDm2, colPhiNuocThanhTienDm3, colPhiNuocThanhTienDm4, colPhiXeThangOToNgoaiDinhMucDonGia, colPhiXeThangOToNgoaiDinhMucSoLuong, colPhiXeThangOToNgoaiDinhMucThanhTien, colPhiXeThangOToTheoDinhMucDonGia, colPhiXeThangOToTheoDinhMucSoLuong, colPhiXeThangOToTheoDinhMucThanhTien, colPhiXeThangTongTien, colPhiXeThangXeDapDonGia, colPhiXeThangXeDapSoLuong, colPhiXeThangXeDapThanhTien, colPhiXeThangXeMayNgoaiDinhMucDonGia, colPhiXeThangXeMayNgoaiDinhMucSoLuong, colPhiXeThangXeMayNgoaiDinhMucThanhTien, colPhiXeThangXeMayTheoDinhMucDonGia, colPhiXeThangXeMayTheoDinhMucSoLuong, colPhiXeThangXeMayTheoDinhMucThanhTien, colPhiXeVangLai, colThuKhacKhac, colThuKhacPhiCoSoHaTang, colThuKhacPhiVeSinh, colThuKhacTong, colTongCong });

            #endregion

            #region gắn column vào bands

            // main
            //colMaHopDong.OwnerBand = bandMaHopDong;
            colMaCan.OwnerBand = bandMaCan;
            colChuHo.OwnerBand = bandChuHo;

            // phí quản lý
            colPhiQuanLyDienTich.OwnerBand = bandPhiQuanLyDienTich;
            colPhiQuanLyDonGia.OwnerBand = bandPhiQuanLyDonGia;
            colPhiQuanLyPql.OwnerBand = bandPhiQuanLyPql;

            // phí điện
            colPhiDienCsc.OwnerBand = bandPhiDienCsc;
            colPhiDienCsm.OwnerBand = bandPhiDienCsm;
            colPhiDienTieuThu.OwnerBand = bandPhiDienTieuThu;
            colPhiDienDonGia.OwnerBand = bandPhiDienDonGia;
            colPhiDienThanhTien.OwnerBand = bandPhiDienThanhTien;

            // phí nước
            colPhiNuocCsc.OwnerBand = bandPhiNuocCsc;
            colPhiNuocCsm.OwnerBand = bandPhiNuocCsm;
            colPhiNuocTieuThu.OwnerBand = bandPhiNuocTieuThu;
            colPhiNuocNhanKhau.OwnerBand = bandPhiNuocNhanKhau;
            colPhiNuocHuongDm.OwnerBand = bandPhiNuocHuongDm;

            colPhiNuocSoLuongDm1.OwnerBand = bandPhiNuocDm1;
            colPhiNuocDonGiaDm1.OwnerBand = bandPhiNuocDm1;
            colPhiNuocThanhTienDm1.OwnerBand = bandPhiNuocDm1;

            colPhiNuocSoLuongDm2.OwnerBand = bandPhiNuocDm2;
            colPhiNuocDonGiaDm2.OwnerBand = bandPhiNuocDm2;
            colPhiNuocThanhTienDm2.OwnerBand = bandPhiNuocDm2;

            colPhiNuocSoLuongDm3.OwnerBand = bandPhiNuocDm3;
            colPhiNuocDonGiaDm3.OwnerBand = bandPhiNuocDm3;
            colPhiNuocThanhTienDm3.OwnerBand = bandPhiNuocDm3;

            colPhiNuocSoLuongDm4.OwnerBand = bandPhiNuocDm4;
            colPhiNuocDonGiaDm4.OwnerBand = bandPhiNuocDm4;
            colPhiNuocThanhTienDm4.OwnerBand = bandPhiNuocDm4;

            colPhiNuocTongNuoc.OwnerBand = bandPhiNuocTongNuoc;

            // phí xe tháng
            colPhiXeThangOToTheoDinhMucSoLuong.OwnerBand = bandPhiXeThangOToTheoDinhMuc;
            colPhiXeThangOToTheoDinhMucDonGia.OwnerBand = bandPhiXeThangOToTheoDinhMuc;
            colPhiXeThangOToTheoDinhMucThanhTien.OwnerBand = bandPhiXeThangOToTheoDinhMuc;
            colPhiXeThangOToNgoaiDinhMucSoLuong.OwnerBand = bandPhiXeThangOToNgoaiDinhMuc;
            colPhiXeThangOToNgoaiDinhMucDonGia.OwnerBand = bandPhiXeThangOToNgoaiDinhMuc;
            colPhiXeThangOToNgoaiDinhMucThanhTien.OwnerBand = bandPhiXeThangOToNgoaiDinhMuc;

            colPhiXeThangXeMayTheoDinhMucSoLuong.OwnerBand = bandPhiXeThangXeMayTheoDinhMuc;
            colPhiXeThangXeMayTheoDinhMucDonGia.OwnerBand = bandPhiXeThangXeMayTheoDinhMuc;
            colPhiXeThangXeMayTheoDinhMucThanhTien.OwnerBand = bandPhiXeThangXeMayTheoDinhMuc;
            colPhiXeThangXeMayNgoaiDinhMucSoLuong.OwnerBand = bandPhiXeThangXeMayNgoaiDinhMuc;
            colPhiXeThangXeMayNgoaiDinhMucDonGia.OwnerBand = bandPhiXeThangXeMayNgoaiDinhMuc;
            colPhiXeThangXeMayNgoaiDinhMucThanhTien.OwnerBand = bandPhiXeThangXeMayNgoaiDinhMuc;

            colPhiXeThangXeDapSoLuong.OwnerBand = bandPhiXeThangXeDap;
            colPhiXeThangXeDapDonGia.OwnerBand = bandPhiXeThangXeDap;
            colPhiXeThangXeDapThanhTien.OwnerBand = bandPhiXeThangXeDap;

            colPhiXeThangTongTien.OwnerBand = bandPhiXeThangTongTien;

            // phí xe vãng lai
            colPhiXeVangLai.OwnerBand = bandPhiXeVangLai;

            // thu khác
            colThuKhacKhac.OwnerBand = bandThuKhacKhac;
            //colThuKhacPhiCoSoHaTang.OwnerBand = bandThuKhacPhiCoSoHaTang;
            //colThuKhacPhiVeSinh.OwnerBand = bandThuKhacPhiVeSinh;
            colThuKhacTong.OwnerBand = bandThuKhacTong;

            // tổng cộng
            colTongCong.OwnerBand = bandTongCong;

            #endregion

            #region fix bên trái
            // 
            //bandMaHopDong.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            bandMaCan.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            bandChuHo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            #endregion

            #region canh giữa text
            // main
            //bandMaHopDong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandMaCan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandChuHo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // phí quản lý
            bandPhiQuanLy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiQuanLyDienTich.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiQuanLyDonGia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiQuanLyPql.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            colPhiQuanLyPql.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // phí điện
            bandPhiDien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiDienCsc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiDienCsm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiDienDonGia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiDienTieuThu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiDienThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            colPhiDienThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // phí nước
            bandPhiNuoc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocCsc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocCsm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocDm1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocDm2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocDm3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocDm4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocHuongDm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocNhanKhau.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocTieuThu.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiNuocTongNuoc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            colPhiNuocDonGiaDm1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiNuocDonGiaDm2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiNuocDonGiaDm3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiNuocDonGiaDm4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiNuocTongNuoc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            // phí xe tháng
            bandPhiXeThang.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangOTo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangOToNgoaiDinhMuc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangOToTheoDinhMuc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangTongTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangXeDap.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangXeMay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangXeMayNgoaiDinhMuc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandPhiXeThangXeMayTheoDinhMuc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            colPhiXeThangOToNgoaiDinhMucDonGia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangOToNgoaiDinhMucSoLuong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangOToNgoaiDinhMucThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangOToTheoDinhMucDonGia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangOToTheoDinhMucSoLuong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangOToTheoDinhMucThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangTongTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeDapDonGia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeDapSoLuong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeDapThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeMayNgoaiDinhMucDonGia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeMayNgoaiDinhMucSoLuong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeMayNgoaiDinhMucThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeMayTheoDinhMucDonGia.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeMayTheoDinhMucSoLuong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeThangXeMayTheoDinhMucThanhTien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // Phí xe vãng lai
            bandPhiXeVangLai.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhiXeVangLai.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // thu khác
            bandThuKhac.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandThuKhacKhac.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //bandThuKhacPhiCoSoHaTang.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //bandThuKhacPhiVeSinh.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandThuKhacTong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            colThuKhacTong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // tổng cộng
            bandTongCong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTongCong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            #endregion

            #region Định dạng column
            // count mã căn


            // phí quản lý
            colPhiQuanLyDienTich.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiQuanLyDienTich.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiQuanLyDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiQuanLyDonGia.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiQuanLyPql.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiQuanLyPql.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            // phí điện
            colPhiDienCsc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiDienCsc.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiDienCsm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiDienCsm.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiDienTieuThu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiDienTieuThu.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiDienDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiDienDonGia.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiDienThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiDienThanhTien.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            // phí nước
            colPhiNuocCsc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocCsc.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocCsm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocCsm.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocDonGiaDm1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocDonGiaDm1.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocDonGiaDm2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocDonGiaDm2.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocDonGiaDm3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocDonGiaDm3.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocDonGiaDm4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocDonGiaDm4.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocHuongDm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocHuongDm.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocNhanKhau.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocNhanKhau.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocSoLuongDm1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocSoLuongDm1.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocSoLuongDm2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocSoLuongDm2.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocSoLuongDm3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocSoLuongDm3.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocSoLuongDm4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocSoLuongDm4.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocTieuThu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocTieuThu.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocTongNuoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocTongNuoc.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocThanhTienDm1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocThanhTienDm1.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocThanhTienDm2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocThanhTienDm2.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocThanhTienDm3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocThanhTienDm3.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiNuocThanhTienDm4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiNuocThanhTienDm4.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            // phí xe tháng
            colPhiXeThangOToTheoDinhMucSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangOToTheoDinhMucSoLuong.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangOToTheoDinhMucDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangOToTheoDinhMucDonGia.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangOToTheoDinhMucThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangOToTheoDinhMucThanhTien.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangOToNgoaiDinhMucSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangOToNgoaiDinhMucSoLuong.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangOToNgoaiDinhMucDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangOToNgoaiDinhMucDonGia.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangOToNgoaiDinhMucThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangOToNgoaiDinhMucThanhTien.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeMayTheoDinhMucSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeMayTheoDinhMucSoLuong.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeMayTheoDinhMucDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeMayTheoDinhMucDonGia.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeMayTheoDinhMucThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeMayTheoDinhMucThanhTien.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeMayNgoaiDinhMucSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeMayNgoaiDinhMucSoLuong.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeMayNgoaiDinhMucDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeMayNgoaiDinhMucDonGia.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeMayNgoaiDinhMucThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeMayNgoaiDinhMucThanhTien.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeDapSoLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeDapSoLuong.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeDapDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeDapDonGia.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeThangXeDapThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangXeDapThanhTien.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";
            //_data.Columns.Add("PhiXeThangTongTien", typeof(decimal));

            //_data.Columns.Add("PhiXeVangLai", typeof(decimal));

            //_data.Columns.Add("ThuKhacPhiVeSinh", typeof(decimal));
            //_data.Columns.Add("ThuKhacPhiCoSoHaTang", typeof(decimal));
            //_data.Columns.Add("ThuKhacKhac", typeof(decimal));
            //_data.Columns.Add("ThuKhacTong", typeof(decimal));

            //_data.Columns.Add("TongCong", typeof(decimal));
            // phí còn lại
            colPhiXeThangTongTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeThangTongTien.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colPhiXeVangLai.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPhiXeVangLai.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colThuKhacPhiVeSinh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colThuKhacPhiVeSinh.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colThuKhacPhiCoSoHaTang.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colThuKhacPhiCoSoHaTang.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colThuKhacKhac.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colThuKhacKhac.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colThuKhacTong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colThuKhacTong.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            colTongCong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTongCong.DisplayFormat.FormatString = "{0:#,0.##; (0.##);-}";

            #endregion

            #region Sum tổng

            colPhiQuanLyDienTich.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiQuanLyDienTich", "{0:n0}")
                    });
            colPhiQuanLyPql.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiQuanLyPql", "{0:n0}")
                    });
            colPhiDienThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiDienThanhTien", "{0:n0}")
                    });
            colPhiNuocThanhTienDm1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiNuocThanhTienDm1", "{0:n0}")
                    });
            colPhiNuocThanhTienDm2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiNuocThanhTienDm2", "{0:n0}")
                    });
            colPhiNuocThanhTienDm3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiNuocThanhTienDm3", "{0:n0}")
                    });
            colPhiNuocThanhTienDm4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiNuocThanhTienDm4", "{0:n0}")
                    });
            colPhiNuocTongNuoc.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiNuocTongNuoc", "{0:n0}")
                    });
            colPhiXeThangOToTheoDinhMucThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiXeThangOToTheoDinhMucThanhTien", "{0:n0}")
                    });
            colPhiXeThangOToNgoaiDinhMucThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiXeThangOToNgoaiDinhMucThanhTien", "{0:n0}")
                    });
            colPhiXeThangXeMayTheoDinhMucThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiXeThangXeMayTheoDinhMucThanhTien", "{0:n0}")
                    });
            colPhiXeThangXeMayNgoaiDinhMucThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiXeThangXeMayNgoaiDinhMucThanhTien", "{0:n0}")
                    });
            colPhiXeThangXeDapThanhTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiXeThangXeDapThanhTien", "{0:n0}")
                    });
            colPhiXeThangTongTien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiXeThangTongTien", "{0:n0}")
                    });
            colPhiXeVangLai.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "PhiXeVangLai", "{0:n0}")
                    });
            colThuKhacPhiVeSinh.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "ThuKhacPhiVeSinh", "{0:n0}")
                    });
            colThuKhacPhiCoSoHaTang.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "ThuKhacPhiCoSoHaTang", "{0:n0}")
                    });
            colThuKhacKhac.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "ThuKhacKhac", "{0:n0}")
                    });
            colThuKhacTong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "ThuKhacTong", "{0:n0}")
                    });
            colTongCong.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[]
                    {
                        new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                            "TongCong", "{0:n0}")
                    });
            #endregion

            #endregion

            #region data table
            //_data = new DataTable();

            //_data.Columns.Add("MaHopDong");
            //_data.Columns.Add("MaCan");
            //_data.Columns.Add("ChuHo");

            //_data.Columns.Add("PhiQuanLyDienTich", typeof(decimal));
            //_data.Columns.Add("PhiQuanLyDonGia", typeof(decimal));
            //_data.Columns.Add("PhiQuanLyPql", typeof(decimal));

            //_data.Columns.Add("PhiDienCsc", typeof(decimal));
            //_data.Columns.Add("PhiDienCsm", typeof(decimal));
            //_data.Columns.Add("PhiDienTieuThu", typeof(decimal));
            //_data.Columns.Add("PhiDienDonGia", typeof(decimal));
            //_data.Columns.Add("PhiDienThanhTien", typeof(decimal));

            //_data.Columns.Add("PhiNuocCsc", typeof(decimal));
            //_data.Columns.Add("PhiNuocCsm", typeof(decimal));
            //_data.Columns.Add("PhiNuocTieuThu", typeof(decimal));
            //_data.Columns.Add("PhiNuocNhanKhau", typeof(decimal));
            //_data.Columns.Add("PhiNuocHuongDm", typeof(decimal));

            //_data.Columns.Add("PhiNuocSoLuongDm1", typeof(decimal));
            //_data.Columns.Add("PhiNuocDonGiaDm1", typeof(decimal));
            //_data.Columns.Add("PhiNuocThanhTienDm1", typeof(decimal));

            //_data.Columns.Add("PhiNuocSoLuongDm2", typeof(decimal));
            //_data.Columns.Add("PhiNuocDonGiaDm2", typeof(decimal));
            //_data.Columns.Add("PhiNuocThanhTienDm2", typeof(decimal));

            //_data.Columns.Add("PhiNuocSoLuongDm3", typeof(decimal));
            //_data.Columns.Add("PhiNuocDonGiaDm3", typeof(decimal));
            //_data.Columns.Add("PhiNuocThanhTienDm3", typeof(decimal));

            //_data.Columns.Add("PhiNuocTongNuoc", typeof(decimal));

            //_data.Columns.Add("PhiXeThangOToTheoDinhMucSoLuong", typeof(decimal));
            //_data.Columns.Add("PhiXeThangOToTheoDinhMucDonGia", typeof(decimal));
            //_data.Columns.Add("PhiXeThangOToTheoDinhMucThanhTien", typeof(decimal));
            //_data.Columns.Add("PhiXeThangOToNgoaiDinhMucSoLuong", typeof(decimal));
            //_data.Columns.Add("PhiXeThangOToNgoaiDinhMucDonGia", typeof(decimal));
            //_data.Columns.Add("PhiXeThangOToNgoaiDinhMucThanhTien", typeof(decimal));

            //_data.Columns.Add("PhiXeThangXeMayTheoDinhMucSoLuong", typeof(decimal));
            //_data.Columns.Add("PhiXeThangXeMayTheoDinhMucDonGia", typeof(decimal));
            //_data.Columns.Add("PhiXeThangXeMayTheoDinhMucThanhTien", typeof(decimal));
            //_data.Columns.Add("PhiXeThangXeMayNgoaiDinhMucSoLuong", typeof(decimal));
            //_data.Columns.Add("PhiXeThangXeMayNgoaiDinhMucDonGia", typeof(decimal));
            //_data.Columns.Add("PhiXeThangXeMayNgoaiDinhMucThanhTien", typeof(decimal));

            //_data.Columns.Add("PhiXeThangXeDapSoLuong", typeof(decimal));
            //_data.Columns.Add("PhiXeThangXeDapDonGia", typeof(decimal));
            //_data.Columns.Add("PhiXeThangXeDapThanhTien", typeof(decimal));

            //_data.Columns.Add("PhiXeThangTongTien", typeof(decimal));

            //_data.Columns.Add("PhiXeVangLai", typeof(decimal));

            //_data.Columns.Add("ThuKhacPhiVeSinh", typeof(decimal));
            //_data.Columns.Add("ThuKhacPhiCoSoHaTang", typeof(decimal));
            //_data.Columns.Add("ThuKhacKhac", typeof(decimal));
            //_data.Columns.Add("ThuKhacTong", typeof(decimal));

            //_data.Columns.Add("TongCong", typeof(decimal));

            #endregion
        }

        public class KhachHang
        {
            public int MaKH { get; set; }
            public string TenKH { get; set; }
            public string KyHieu { get; set; }
            public decimal? DienTich { get; set; }
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((ComboBoxEdit)sender).SelectedIndex);
        }
    }
}