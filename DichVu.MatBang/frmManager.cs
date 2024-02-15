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
using DevExpress.XtraSplashScreen;

namespace DichVu.MatBang
{
    public partial class frmManager : DevExpress.XtraEditors.XtraForm
    {
        private MasterDataContext db;

        public frmManager()
        {
            InitializeComponent();
        }

        async void LoadData()
        {
            gcMatBang.DataSource = null;
            //gcMatBang.DataSource = linqInstantFeedbackSource1;
            var db = new MasterDataContext();
            var _MaTN = (byte)itemToaNha.EditValue;

            System.Collections.Generic.List<DataDanhSachMatBangSAP> dataDanhSachMatBangs = new List<DataDanhSachMatBangSAP>();
            await System.Threading.Tasks.Task.Run(() => { dataDanhSachMatBangs = GetDataDanhSachMatBangs(_MaTN); });

            gcMatBang.DataSource = dataDanhSachMatBangs;
        }

        #region overlay
        IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this);
        }

        void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }

        IOverlaySplashScreenHandle handle = null;
        #endregion

        void RefreshData()
        {
            //linqInstantFeedbackSource1.Refresh();
            LoadData();
        }

        void AddMatBang()
        {
            using (var frm = new frmEdit())
            {
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    this.RefreshData();
            }
        }

        void EditMatBang()
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            using (var frm = new frmEdit())
            {
                frm.MaMB = _MaMB;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    this.RefreshData();
            }
        }

        void DeleteMatBang()
        {
            int[] indexs = grvMatBang.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những mặt bằng cần xóa");
                return;
            }

            if (DialogBox.QuestionDelete() == DialogResult.No) return;

            var db = new MasterDataContext();

            try
            {
                foreach (int i in indexs)
                {
                    mbMatBang objMB = db.mbMatBangs.Single(p => p.MaMB == (int)grvMatBang.GetRowCellValue(i, "MaMB"));
                    db.mbMatBangs.DeleteOnSubmit(objMB);
                }

                db.SubmitChanges();

                grvMatBang.DeleteSelectedRows();
                LoadData();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
                return;
            }
            finally
            {
                db.Dispose();
            }
        }

        void DetailMatBang()
        {
            try
            {
                db = new MasterDataContext();
                var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
                if (_MaMB == null)
                {
                    gcGiaThue.DataSource = null;
                    ctlCuDan1.MaMB = null;
                    ctlCuDan1.CuDanLoadData();
                    return;
                }

                switch (tcdetail.SelectedTabPageIndex)
                {
                    case 0:
                        this.LoadGiaThue(_MaMB);
                        break;
                    case 1:
                        this.LoadNhanKhau(_MaMB);
                        break;
                    case 2:
                        this.LoadYeuCau(_MaMB);
                        break;
                    case 3:
                        this.LoadLS(_MaMB);
                        break;
                    case 4:
                        repBoPhan.DataSource = db.tnPhongBans;
                        repNhanVien.DataSource = db.tnNhanViens;
                        gcBienBanBanGiao.DataSource = db.mbMatBang_BienBan_BanGiaos.Where(_ => _.MaMB == _MaMB);
                        break;
                    case 5:
                        gcChinhSachUuDai.DataSource = db.mbMatBang_ChinhSachUuDais.Where(_ => _.MaMB == _MaMB);
                        break;
                    case 6:
                        repThietBi.DataSource = db.mbMatBang_ThietBis
                            .Select(_ => new {_.ID, ThietBi = _.MaThietBi + " - " + _.TenThietBi}).ToList();
                        repNV.DataSource = db.tnNhanViens;
                        gcThietBiKemTheo.DataSource = db.mbMatBang_ThietBiKemTheos.Where(_ => _.MaMB == _MaMB);
                        break;
                    case 7:
                        repThietBi1.DataSource = db.mbMatBang_ThietBis
                            .Select(_ => new {_.ID, ThietBi = _.MaThietBi + " - " + _.TenThietBi}).ToList();
                        repNhanVien1.DataSource = db.tnNhanViens;
                        gcLichSuSuaChua.DataSource = db.mbMatBang_LichSuSuaChuas.Where(_ => _.MaMB == _MaMB);
                        break;
                }
            }
            catch { }
        }

        void LoadNhanKhau(int? MaMB)
        {
            ctlCuDan1.MaMB = MaMB;
            ctlCuDan1.MaTN = (byte)itemToaNha.EditValue;
            ctlCuDan1.CuDanLoadData();
        }

        void LoadGiaThue(int? MaMB)
        {
            var db = new MasterDataContext();
            try
            {
                gcGiaThue.DataSource = (from gt in db.mbGiaThues
                                        join lgt in db.LoaiGiaThues on gt.MaLG equals lgt.ID
                                        where gt.MaMB == MaMB
                                        orderby gt.ID
                                        select new
                                        {
                                            lgt.TenLG,
                                            gt.DienTich,
                                            gt.DonGia,
                                            gt.ThanhTien,
                                            gt.DienGiai
                                        }).ToList();
            }
            catch { }
            finally
            {
                db.Dispose();
            }
        }

        void ImportMatBang()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new Import.frmMatBang())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.IsSave)
                    RefreshData();
            }
        }

        void ImportGiaThue()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new Import.frmGiaThue())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.IsSave)
                    RefreshData();
            }
        }

        void ImportCapNhatLoaiMatBang()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new Import.frmUpdateLoaiMatBang())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }

        void ImportCapNhatTTMatBang()
        {
            var _MaTN = (byte?)itemToaNha.EditValue;
            if (_MaTN == null)
            {
                DialogBox.Alert("Vui lòng chọn Dự án");
                return;
            }

            using (var f = new Import.frmUpdateTTMatBang())
            {
                f.MaTN = _MaTN.Value;
                f.ShowDialog();
                if (f.isSave)
                    RefreshData();
            }
        }
        private void frmMatBang_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            tcdetail.SelectedTabPageIndex = 0;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddMatBang();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditMatBang();
        }

        

        private void grvMatBang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteMatBang();
        }
      
        private void tcdetail_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            DetailMatBang();
        }

        private void grvMatBang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DetailMatBang();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void grvMatBang_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.Caption == "Ký hiệu")
                    e.Appearance.BackColor = Color.FromArgb((int)grvMatBang.GetRowCellValue(e.RowHandle, "MauNen"));
            }
            catch { }
        }

        private void grvMatBang_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            DetailMatBang();
        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }
        private void LoadLS(int? MaMB)
        {
            MasterDataContext db = new MasterDataContext();
            gridControl1.DataSource = 
                (from mb in db.mbMatBang_copies
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
                               
                where mb.IDMaMB == MaMB
                select new
                {
                    mb.MaMB,
                    mb.MaSoMB,
                    mb.SoNha,
                    tl.TenTL,
                    kn.TenKN,
                    lmb.TenLMB,
                    mb.DienTichThongThuy,
                    mb.DienTichTimTuong,
                    mb.NgayVaoO,
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
                    //NguoiSua = nvs.HoTenNV,
                    mb.NgaySua
                }).ToList();
        }
        private void LoadYeuCau(int? MaKH)
        {
            MasterDataContext db = new MasterDataContext();
            gcYeuCau.DataSource = db.tnycYeuCaus
                .Where(p => p.MaMB == MaKH).OrderByDescending(p => p.NgayYC)
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

        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {

        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteMatBang();
        }


        #region Get data

        public class DataDanhSachMatBang
        {
            public System.Int32? MaMB { get; set; }
            public System.String MaSoMB { get; set; }
            public System.String SoNha { get; set; }
            public System.String TenTL { get; set; }
            public System.String TenKN { get; set; }
            public System.String TenLMB { get; set; }
            public System.Decimal? DienTichThongThuy { get; set; }
            public System.Decimal? DienTichTimTuong { get; set; }
            public System.DateTime? NgayVaoO { get; set; }
            public System.Decimal? KhoangLuiSauCanHo { get; set; }
            public System.Decimal? KhoangLuiTruocCanHo { get; set; }
            public System.String NhanVienBanGiaoNha { get; set; }
            public System.String NhaThauXayDung { get; set; }
            public System.String NhaThauThiCongHoanThienNoiThat { get; set; }
            public System.String TenTT { get; set; }
            public System.Decimal? DienTich { get; set; }
            public System.Decimal? GiaThue { get; set; }
            public System.String KyHieuLT { get; set; }
            public System.String TenKH { get; set; }
            public System.String TenCSH { get; set; }
            public System.Boolean? IsCanHoCaNhan { get; set; }
            public System.Boolean? DaGiaoChiaKhoa { get; set; }
            public System.DateTime? NgayBanGiao { get; set; }
            public System.String DienGiai { get; set; }
            public System.String NguoiNhap { get; set; }
            public System.DateTime? NgayNhap { get; set; }
            public System.String NguoiSua { get; set; }
            public System.DateTime? NgaySua { get; set; }
            public DateTime? ThueTuNgay { get; set; }
            public DateTime? ThueDenNgay { get; set; }
            public string KyHieu { get; set; }
            public string KyHieu1 { get; set; }
        }

        public class DataDanhSachMatBangSAP: DataDanhSachMatBang
        {
            public string SAP_CSHLS { get; set; }
            public string SAP_ZZCAN { get; set; }
            public string MaKhoiNha { get; set; }
        }

        private System.Collections.Generic.List<DataDanhSachMatBangSAP> GetDataDanhSachMatBangs(byte? _MaTN)
        {
            var model = new { matn = _MaTN };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            var result = Library.Class.Connect.QueryConnect.Query<DataDanhSachMatBangSAP>("mbmatbang_getdata", param);
            if (result.Count() > 0) return result.ToList();
            else return null;
        }


        #endregion

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportMatBang();
        }

        private void itemImportGiaThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportGiaThue();
        }

        private void btnExportMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcMatBang);
        }

        private void itemImportCapNhatLoaiMatBang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportCapNhatLoaiMatBang();
        }

        private void itemBieuDo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaTN = itemToaNha.EditValue.ToString();//(int)itemToaNha.EditValue; ;
            using (var f = new MatBang.ThongKe.frmThongKe())
            {
                f.MaTN = Convert.ToInt32(MaTN);
                f.ShowDialog();
            }
        }

        private void gcMatBang_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ImportCapNhatTTMatBang();
        }

        private void itemThemBienBanBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmBienBanBanGiao())
            {
                frm.MaMB = _MaMB;
                frm.MaTn = (byte)itemToaNha.EditValue;
                frm.ID = 0;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }

        }

        private void btnSuaBienBanBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var _ID = (long?) gvBienBanBanGiao.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Biên bản bàn giao], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmBienBanBanGiao())
            {
                frm.MaMB = _MaMB;
                frm.MaTn = (byte)itemToaNha.EditValue;
                frm.ID = _ID;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }
        }

        private void btnXoaBienBanBanGiao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (long?) gvBienBanBanGiao.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Biên bản bàn giao], xin cảm ơn.");
                return;
            }

            try
            {
                db = new MasterDataContext();
                db.mbMatBang_BienBan_BanGiaos.DeleteOnSubmit(db.mbMatBang_BienBan_BanGiaos.First(_ => _.ID == _ID));
                db.SubmitChanges();
                
                DetailMatBang();
                DialogBox.Alert("Đã xóa thành công");
            }
            catch
            {
                DialogBox.Error("Xóa không thành công, vui lòng liên hệ kỹ thuật.");
            }
        }

        private void itemThemChinhSachUuDai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmChinhSachUuDai())
            {
                frm.MaMB = _MaMB;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ID = 0;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }
        }

        private void itemSuaChinhSachUuDai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var _ID = (int?)gvChinhSachUuDai.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Chính sách ưu đãi], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmChinhSachUuDai())
            {
                frm.MaMB = _MaMB;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ID = _ID;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }
        }

        private void itemXoaChinhSachUuDai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (int?)gvChinhSachUuDai.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Chính sách ưu đãi], xin cảm ơn.");
                return;
            }

            try
            {
                db = new MasterDataContext();
                db.mbMatBang_ChinhSachUuDais.DeleteOnSubmit(db.mbMatBang_ChinhSachUuDais.First(_ => _.ID == _ID));
                db.SubmitChanges();
                
                DetailMatBang();
                DialogBox.Alert("Đã xóa thành công");
            }
            catch
            {
                DialogBox.Error("Xóa không thành công, vui lòng liên hệ kỹ thuật.");
            }
        }

        private void itemThemThietBiKemTheo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmThietBiKemTheo())
            {
                frm.MaMB = _MaMB;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ID = 0;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }
        }

        private void itemSuaThietBiKemTheo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var _ID = (long?)gvThietBiKemTheo.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Thiết bị kèm theo], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmThietBiKemTheo())
            {
                frm.MaMB = _MaMB;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ID = _ID;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }
        }

        private void itemXoaThietBiKemTheo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (long?)gvThietBiKemTheo.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Thiết bị kèm theo], xin cảm ơn.");
                return;
            }

            try
            {
                db = new MasterDataContext();
                db.mbMatBang_ThietBiKemTheos.DeleteOnSubmit(db.mbMatBang_ThietBiKemTheos.First(_ => _.ID == _ID));
                db.SubmitChanges();

                DetailMatBang();
                DialogBox.Alert("Đã xóa thành công");
            }
            catch
            {
                DialogBox.Error("Xóa không thành công, vui lòng liên hệ kỹ thuật.");
            }
        }

        private void itemThemLichSuSuaChua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmLichSuSuaChua())
            {
                frm.MaMB = _MaMB;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ID = 0;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }
        }

        private void itemSuaLichSuSuaChua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _MaMB = (int?)grvMatBang.GetFocusedRowCellValue("MaMB");
            if (_MaMB == null)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                return;
            }

            var _ID = (long?)gvLichSuSuaChua.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Lịch sử sửa chữa], xin cảm ơn.");
                return;
            }

            using (var frm = new TichHopCRM.frmLichSuSuaChua())
            {
                frm.MaMB = _MaMB;
                frm.MaTN = (byte)itemToaNha.EditValue;
                frm.ID = _ID;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DetailMatBang();
            }
        }

        private void itemXoaLichSuSuaChua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _ID = (long?)gvLichSuSuaChua.GetFocusedRowCellValue("ID");
            if (_ID == null)
            {
                DialogBox.Error("Vui lòng chọn [Lịch sử sửa chữa], xin cảm ơn.");
                return;
            }

            try
            {
                db = new MasterDataContext();
                db.mbMatBang_LichSuSuaChuas.DeleteOnSubmit(db.mbMatBang_LichSuSuaChuas.First(_ => _.ID == _ID));
                db.SubmitChanges();

                DetailMatBang();
                DialogBox.Alert("Đã xóa thành công");
            }
            catch
            {
                DialogBox.Error("Xóa không thành công, vui lòng liên hệ kỹ thuật.");
            }
        }

        /// <summary>
        /// Chốt sổ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var TowerId = (byte)itemToaNha.EditValue;
                if(TowerId == null)
                {
                    DialogBox.Alert("Vui lòng chọn tòa nhà");
                    return;
                }

                using (var frm = new ChotSo.frmChotSo())
                {
                     frm.MaTN= TowerId;
                    frm.ShowDialog();
                }
            }
            catch(System.Exception ex) { }
        }

        private void itemDongBoMatBangDuocChon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] indexs = grvMatBang.GetSelectedRows();

                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn những mặt bằng");
                    return;
                }

                var db = new MasterDataContext();


                    foreach (int i in indexs)
                    {
                        SAP.Funct.SyncCus.DongBo_1_MB((int)grvMatBang.GetRowCellValue(i, "MaMB"));
                    }


                    LoadData();

            }
            catch (System.Exception ex) { }
        }

        /// <summary>
        /// Đồng bộ tất cả mặt bằng trong tòa nhà
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            handle = ShowProgressPanel();
            try
            {
                var TowerId = (byte)itemToaNha.EditValue;
                if (TowerId == null)
                {
                    DialogBox.Alert("Vui lòng chọn tòa nhà");
                    return;
                }

                SAP.Funct.SyncCus.DongBo_N_MB(TowerId);
            }
            catch (System.Exception ex) { }

            CloseProgressPanel(handle);
        }
    }
}