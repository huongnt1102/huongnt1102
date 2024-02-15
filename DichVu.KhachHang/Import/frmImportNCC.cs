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
using LinqToExcel;
using DevExpress.XtraSplashScreen;
using DevExpress.Utils.Extensions;
using System.Threading;
using DxSample;
using DevExpress.XtraBars;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.Model;

namespace KyThuat.KhachHang.Import
{
    public partial class frmImportNCC : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public byte? MaTN;
        public bool IsUpdate = false;

        TaskHelper taskHelper;
        OverlayTextPainter overlayLabel;
        OverlayImagePainter overlayButton;

        public frmImportNCC()
        {
            

            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this,barManager1);
        }

        bool getSex(string val)
        {
            try
            {
                return Convert.ToBoolean(val.Trim());
            }
            catch { }

            return false;
        }

        private void btnChonTapTin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new OpenFileDialog();
            try
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();

                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }


        private void LoadData()
        {
            
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            LoadData();

            if (IsUpdate)
                this.Text = "Cập nhật thông tin (Cá nhân)";
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvCaNhan.DataSource == null)
            {
                DialogBox.Alert("Không có dữ liệu lưu");
                return;
            }

            if (IsUpdate)
                Update();
            else
                insert1();
        }

        void insert1()
        {
            try
            {
                handle = ShowProgressPanel();

                var ltSource = (List<ImportItem>)gcCaNhan.DataSource;
                var ltError = new List<ImportItem>();

                foreach (var mb in ltSource)
                {
                    db = new MasterDataContext();
                    var tam = mb.MaKhachHang;
                    var obj = db.tnKhachHangs.FirstOrDefault(p => p.KyHieu == mb.MaKhachHang & p.MaTN == MaTN);
                    if (obj != null)
                    {
                        continue;
                    }
                    try
                    {

                        var objNhomKH = db.khNhomKhachHangs.FirstOrDefault(p => p.TenNKH == mb.NhomKhachHang & p.MaTN == MaTN);
                        if (objNhomKH == null)
                        {
                            mb.Error = "Nhóm khách hàng không tồn tại.";
                            ltError.Add(mb);
                            continue;
                        }
                        var khu_vuc = db.tnKhuVucs.FirstOrDefault(_ => _.TenKV == mb.KhuVuc);
                        if (khu_vuc == null)
                        {
                            mb.Error = "Khu vực không tồn tại.";
                            ltError.Add(mb);
                            continue;
                        }

                        var model = new { MaKH = 0, MaKhachHang = mb.MaKhachHang, TenVietTat = mb.TenVietTat, TenCongTy = mb.TenCongTy, MaSoThue = mb.MaSoThue, KhuVuc = khu_vuc.MaKV, DiaChi = mb.DiaChi, DienThoai = mb.DienThoai, Email = mb.Email, EmailKhachHang = mb.EmailKhachHang, NhomKhachHang = objNhomKH.ID, NguoiDaiDien = mb.NguoiDaiDien, NguoiLienHe = mb.NguoiLienHe, QuocTich = mb.QuocTich, ChucVu = mb.ChucVu, NganhNghe = mb.NganhNghe, DiaPhan = mb.DiaPhan, DiaChiNhanThu = mb.DiaChiNhanThu, NgayDKKD = mb.NgayDKKD, NoiDKKD = mb.NoiDKKD, SoTaiKhoan = mb.SoTaiKhoan, NganHang = mb.NganHang, Fax = mb.Fax, Website = mb.Website, IsNCC = true, MaNV = Library.Common.User.MaNV, MaTN = MaTN };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        var result = Library.Class.Connect.QueryConnect.Query<bool>("tnkhachhang_update_doanh_nghiep", param);
                    }
                    catch (Exception ex)
                    {
                        CloseProgressPanel(handle);
                        //OnCancelButtonClick();
                        string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                        mb.Error = mes;
                        ltError.Add(mb);

                    }
                }
                CloseProgressPanel(handle);
                //OnCancelButtonClick();
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcCaNhan.DataSource = ltError;
                }
                else
                {
                    gcCaNhan.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CloseProgressPanel(handle);
                //OnCancelButtonClick();
                string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                DevExpress.XtraEditors.XtraMessageBoxArgs args = new DevExpress.XtraEditors.XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 1000;
                args.Caption = ex.GetType().FullName;
                args.Text = ex.Message + " - " + mes;
                args.Buttons = new System.Windows.Forms.DialogResult[] { System.Windows.Forms.DialogResult.OK, System.Windows.Forms.DialogResult.Cancel };
                DevExpress.XtraEditors.XtraMessageBox.Show(args).ToString();
            }
        }

        #region Overlay
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

        private void btn_hide_Click(object sender, EventArgs e)
        {
            CloseProgressPanel(handle);
        }

        async void OnRunTaskItemClick()
        {
            string taskResult;
            IOverlaySplashScreenHandle overlayHandle = SplashScreenManager.ShowOverlayForm(panelControl1, customPainter: new OverlayWindowCompositePainter(overlayLabel));
            try
            {
                taskResult = await RunTask();
            }
            finally
            {
                SplashScreenManager.CloseOverlayForm(overlayHandle);
            }
            //XtraMessageBox.Show(this, taskResult, "Task Result");
        }

        async Task<string> RunTask()
        {
            string taskResult;
            this.taskHelper = new TaskHelper();
            try
            {
                taskResult = await taskHelper.StartTask(new Progress<int>(OnProgressChanged));
            }
            catch (OperationCanceledException)
            {
                taskResult = "Operation is Cancelled";
            }
            finally
            {
                taskHelper.Dispose();
                taskHelper = null;
            }
            return taskResult;
        }

        void OnCancelButtonClick()
        {
            if (taskHelper != null) taskHelper.Cancel();
        }
        void OnProgressChanged(int value)
        {
            overlayLabel.Text = value.ToString() + "%";
        }
        #endregion

        void Update()
        {
            try
            {
                handle = ShowProgressPanel();
                //this.overlayLabel = new OverlayTextPainter();
                //OnRunTaskItemClick();

                var ltSource = (List<ImportItem>)gcCaNhan.DataSource;
                var ltError = new List<ImportItem>();

                foreach (var mb in ltSource)
                {
                    db = new MasterDataContext();
                    var tam = mb.MaKhachHang;
                    var obj = db.tnKhachHangs.FirstOrDefault(p => p.KyHieu == mb.MaKhachHang & p.MaTN == MaTN);
                    if (obj == null)
                    {
                        continue;
                    }
                    try
                    {

                        var objNhomKH = db.khNhomKhachHangs.FirstOrDefault(p => p.TenNKH == mb.NhomKhachHang & p.MaTN == MaTN);
                        if (objNhomKH == null)
                        {
                            mb.Error = "Nhóm khách hàng không tồn tại.";
                            ltError.Add(mb);
                            continue;
                        }
                        var khu_vuc = db.tnKhuVucs.FirstOrDefault(_ => _.TenKV == mb.KhuVuc);
                        if (khu_vuc == null)
                        {
                            mb.Error = "Khu vực không tồn tại.";
                            ltError.Add(mb);
                            continue;
                        }

                        var model = new { MaKH = obj.MaKH, MaKhachHang = mb.MaKhachHang, TenVietTat = mb.TenVietTat, TenCongTy = mb.TenCongTy, MaSoThue = mb.MaSoThue, KhuVuc = khu_vuc.MaKV, DiaChi = mb.DiaChi, DienThoai = mb.DienThoai, Email = mb.Email, EmailKhachHang = mb.EmailKhachHang, NhomKhachHang = objNhomKH.ID, NguoiDaiDien = mb.NguoiDaiDien, NguoiLienHe = mb.NguoiLienHe, QuocTich = mb.QuocTich, ChucVu = mb.ChucVu, NganhNghe = mb.NganhNghe, DiaPhan = mb.DiaPhan, DiaChiNhanThu = mb.DiaChiNhanThu, NgayDKKD = mb.NgayDKKD, NoiDKKD = mb.NoiDKKD, SoTaiKhoan = mb.SoTaiKhoan, NganHang = mb.NganHang, Fax = mb.Fax, Website = mb.Website, IsNCC = true, MaNV = Library.Common.User.MaNV, MaTN = MaTN };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        var result = Library.Class.Connect.QueryConnect.Query<bool>("tnkhachhang_update_doanh_nghiep", param);
                    }
                    catch (Exception ex)
                    {
                        CloseProgressPanel(handle);
                        //OnCancelButtonClick();
                        string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                        mb.Error = mes;
                        ltError.Add(mb);
                        
                    }
                }
                CloseProgressPanel(handle);
                //OnCancelButtonClick();
                DialogBox.Success();

                if (ltError.Count > 0)
                {
                    gcCaNhan.DataSource = ltError;
                }
                else
                {
                    gcCaNhan.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CloseProgressPanel(handle);
                //OnCancelButtonClick();
                string mes = Translate.TranslateGoogle.TranslateText(ex.Message, "en-us", "vi-vn");
                DevExpress.XtraEditors.XtraMessageBoxArgs args = new DevExpress.XtraEditors.XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 1000;
                args.Caption = ex.GetType().FullName;
                args.Text =ex.Message+ " - " + mes;
                args.Buttons = new System.Windows.Forms.DialogResult[] { System.Windows.Forms.DialogResult.OK, System.Windows.Forms.DialogResult.Cancel };
                DevExpress.XtraEditors.XtraMessageBox.Show(args).ToString();
            }

        }


        private void grvCaNhan_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //try
            //{
            //    if (e.RowHandle < 0) return;
            //    if (db.tnKhachHangs.Where(p => p.KyHieu == grvCaNhan.GetRowCellValue(e.RowHandle, colKyHieu).ToString().Trim()).Count() > 0)
            //    {
            //        e.Appearance.BackColor = Color.LightGreen;
            //    }
            //}
            //catch { }
        }

        private void btnXoaDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvCaNhan.DeleteSelectedRows();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void itemExportMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gcCaNhan);
        }


        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            handle = ShowProgressPanel();
            //OnRunTaskItemClick();
            //The Wait Form is opened in a separate thread. To change its Description, use the SetWaitFormDescription method.
            try
            {
                var excel = new ExcelQueryFactory(this.Tag.ToString());

                System.Collections.Generic.List<ImportItem> list = Library.Import.ExcelAuto.ConvertDataTable<ImportItem>(Library.Import.ExcelAuto.GetDataExcel(excel, grvCaNhan, itemSheet));

                gcCaNhan.DataSource = list;


                excel = null;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            //OnCancelButtonClick();
            CloseProgressPanel(handle);
        }

        class ImportItem
        {
            public string MaKhachHang { get; set; }
            public string TenVietTat { get; set; }
            public string TenCongTy { get; set; }
            public string MaSoThue { get; set; }
            public string KhuVuc { get; set; }
            public string DiaChi { get; set; }
            public string DienThoai { get; set; }
            public string Email { get; set; }
            public string EmailKhachHang { get; set; }
            public string NhomKhachHang { get; set; }
            public string NguoiDaiDien { get; set; }
            public string NguoiLienHe { get; set; }
            public string QuocTich { get; set; }
            public string ChucVu { get; set; }
            public string NganhNghe { get; set; }
            public string DiaPhan { get; set; }
            public string DiaChiNhanThu { get; set; }
            public string NgayDKKD { get; set; }
            public string NoiDKKD { get; set; }
            public string SoTaiKhoan { get; set; }
            public string NganHang { get; set; }
            public string Fax { get; set; }
            public string Website { get; set; }
            public string Error { get; set; }
        }

        public class tnkhachhang_update_ca_nhan_model
        {
            public byte? matn { get; set; }

            public int? mankh { get; set; }

            public int? makh { get; set; }

            public int? makv { get; set; }

            public int? manv { get; set; }

            public bool? iscanhan { get; set; }

            public bool? gioitinh { get; set; }

            public string taikhoannganhang { get; set; }

            public string kyhieu { get; set; }

            public string maphu { get; set; }

            public string diachilienlac { get; set; }

            public string diachithuongtru { get; set; }

            public string tenkh { get; set; }

            public string masothue { get; set; }

            public string dienthoai { get; set; }

            public string cmnd { get; set; }

            public string email { get; set; }

            public string ngaycap { get; set; }

            public string noicap { get; set; }
            public string emailchuhopdong { get; set; }
            public string mathanhtoan { get; set; }

        }
    }




}