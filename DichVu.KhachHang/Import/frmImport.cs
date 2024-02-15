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
    public partial class frmImport : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien;
        public byte? MaTN;
        public bool IsUpdate = false;

        TaskHelper taskHelper;
        Image buttonImage;
        Image hotButtonImage;

        OverlayTextPainter overlayLabel;
        OverlayImagePainter overlayButton;

        public frmImport()
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

        private DateTime? MyConvert(LinqToExcel.Cell value)
        {
            try
            {
                //return value.Cast<DateTime>(); 
                return DateTime.FromOADate(Convert.ToInt64(value));
            }
            catch
            {
                return null;
            }
        }

        private int? SearchKV(string chuoi)
        {
            try
            {
                return db.tnKhuVucs.FirstOrDefault(p => SqlMethods.Like(p.TenKV, "%" + chuoi + "%")).MaKV;
            }
            catch
            {
                return null;
            }
        }

        private void LoadData()
        {
            lookKhuVuc.DataSource = db.tnKhuVucs;
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

        void Insert()
        {
            List<tnKhachHang> listMB = new List<tnKhachHang>();
            for (int i = 0; i < grvCaNhan.RowCount; i++)
            {
                var objKH = db.tnKhachHangs.FirstOrDefault(p => p.KyHieu == grvCaNhan.GetRowCellValue(i, colKyHieu).ToString() & p.MaTN == MaTN);
                if (objKH == null)
                {
                    var obj = new tnKhachHang();

                    var objNhomKH = db.khNhomKhachHangs.FirstOrDefault(p => p.TenNKH == grvCaNhan.GetRowCellValue(i, colNhomKH).ToString() & p.MaTN == MaTN);
                    var Str1 = grvCaNhan.GetRowCellValue(i, colTen).ToString();

                    try
                    {
                        var len = Str1.LastIndexOf(" ");
                        var HoTen = Str1.Substring(0, len);
                        var Ten = Str1.Substring(len + 1, Str1.Length - len);
                        obj.HoKH = HoTen;
                        obj.TenKH = Ten;
                    }
                    catch
                    {
                        obj.HoKH = "";
                        obj.TenKH = grvCaNhan.GetRowCellValue(i, colTen).ToString();
                    }

                    obj.KyHieu = grvCaNhan.GetRowCellValue(i, colKyHieu).ToString();
                    if (grvCaNhan.GetRowCellValue(i, colMaPhu) != null)
                        obj.MaPhu = grvCaNhan.GetRowCellValue(i, colMaPhu).ToString();
                    obj.DCLL = grvCaNhan.GetRowCellValue(i, colDiaChi).ToString();
                    obj.DCTT = grvCaNhan.GetRowCellValue(i, colDiaChiThuongTru).ToString();
                    obj.NguoiDongSoHuu = grvCaNhan.GetRowCellValue(i, colNguoiDongSoHuu).ToString();
                    //obj.HoKH = grvCaNhan.GetRowCellValue(i, colHo).ToString();

                    obj.MaSoThue = grvCaNhan.GetRowCellValue(i, colMaSoThue).ToString();
                    obj.DienThoaiKH = grvCaNhan.GetRowCellValue(i, "DienThoai").ToString();
                    obj.CMND = grvCaNhan.GetRowCellValue(i, "CMND").ToString();
                    obj.EmailKH = grvCaNhan.GetRowCellValue(i, "Email").ToString();
                    if (grvCaNhan.GetRowCellValue(i, "NgayCap") != null) obj.NgayCap = grvCaNhan.GetRowCellValue(i, "NgayCap").ToString();
                    if (grvCaNhan.GetRowCellValue(i, "NoiCap") != null) obj.NoiCap = grvCaNhan.GetRowCellValue(i, "NoiCap").ToString();
                    obj.IsCaNhan = true;
                    if (grvCaNhan.GetRowCellValue(i, "TaiKhoanNganHang") != null) obj.TaiKhoanNganHang = grvCaNhan.GetRowCellValue(i, "TaiKhoanNganHang").ToString();
                    if (grvCaNhan.GetRowCellValue(i, "KhuVuc") != null) obj.MaKV = (int)grvCaNhan.GetRowCellValue(i, "KhuVuc");
                    obj.MaNV = objnhanvien.MaNV;
                    obj.MaTN = MaTN;

                    if (objNhomKH != null)
                    {
                        obj.MaNKH = objNhomKH.ID;
                    }
                    else
                        obj.MaNKH = null;
                    if (grvCaNhan.GetRowCellValue(i, "GioiTinh") != null)
                    {
                        var tam = grvCaNhan.GetRowCellValue(i, "GioiTinh").ToString().Trim();
                        obj.GioiTinh = grvCaNhan.GetRowCellValue(i, "GioiTinh").ToString().Trim() == "True" ? true : false;
                    }
                    else
                        obj.GioiTinh = true;

                    listMB.Add(obj);
                
                }
                

            }
            var wait = DialogBox.WaitingForm();
            try
            {
                db.tnKhachHangs.InsertAllOnSubmit(listMB);
                db.SubmitChanges();
                wait.Close();
                wait.Dispose();
                DialogBox.Alert("Đã lưu");
            }
            catch
            {
                DialogBox.Error("Mã khách hàng bị trùng. Vui lòng xem lại dữ liệu");
                wait.Close();
                wait.Dispose();
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        void insert1()
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
                    var tam = mb.KyHieu;
                    var obj = db.tnKhachHangs.FirstOrDefault(p => p.KyHieu == mb.KyHieu & p.MaTN == MaTN);
                    if (obj != null)
                    {
                        continue;
                    }
                    try
                    {

                        var objNhomKH = db.khNhomKhachHangs.FirstOrDefault(p => p.TenNKH == mb.NhomKH & p.MaTN == MaTN);
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
                        var objCCCD = db.tnKhachHangs.FirstOrDefault(p => p.CMND == mb.CMND & p.MaTN == MaTN);
                        if (objCCCD != null)
                        {
                            mb.Error = "CCCD: "+ mb.CMND + " đã tồn tại.";
                            ltError.Add(mb);
                            continue;
                        }
                        var model = new { cmnd = mb.CMND, diachilienlac = mb.DiaChiLL, diachithuongtru = mb.DiaChiThuongTru, dienthoai = mb.DienThoai, email = mb.Email, gioitinh = mb.GioiTinh, iscanhan = true, kyhieu = mb.KyHieu, makh = 0, makv = khu_vuc.MaKV, mankh = objNhomKH.ID, manv = Library.Common.User.MaNV, maphu = mb.MaPhu, masothue = mb.MaSoThue, matn = MaTN, ngaycap = mb.NgayCap, noicap = mb.NoiCap, taikhoannganhang = mb.TaiKhoanNganHang, tenkh = mb.TenKH, diachinhanthu = mb.DiaChiNhanThu, quoctich = mb.QuocTich, emailKhachThue = mb.EmailKhachThue, diaPhan = mb.DiaPhan };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        var result = Library.Class.Connect.QueryConnect.Query<bool>("tnkhachhang_update_ca_nhan", param);
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
                    var tam = mb.KyHieu;
                    var obj = db.tnKhachHangs.FirstOrDefault(p => p.KyHieu == mb.KyHieu & p.MaTN == MaTN);
                    if (obj == null)
                    {
                        continue;
                    }
                    try
                    {

                        var objNhomKH = db.khNhomKhachHangs.FirstOrDefault(p => p.TenNKH == mb.NhomKH & p.MaTN == MaTN);
                        if (objNhomKH == null)
                        {
                            mb.Error = "Nhóm khách hàng không tồn tại.";
                            ltError.Add(mb);
                            continue;
                        }
                        var objCCCD = db.tnKhachHangs.FirstOrDefault(p => p.CMND == mb.CMND & p.MaTN == MaTN & p.MaKH != obj.MaKH);
                        if (objCCCD != null)
                        {
                            mb.Error = "CCCD: "+ mb.CMND + " đã tồn tại.";
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

                        var model = new { cmnd = mb.CMND, diachilienlac = mb.DiaChiLL, diachithuongtru = mb.DiaChiThuongTru, dienthoai = mb.DienThoai, email = mb.Email, gioitinh = mb.GioiTinh, iscanhan = true, kyhieu = mb.KyHieu, makh = obj.MaKH, makv = khu_vuc.MaKV, mankh = objNhomKH.ID, manv = Library.Common.User.MaNV, maphu = mb.MaPhu, masothue = mb.MaSoThue, matn = MaTN, ngaycap = mb.NgayCap, noicap = mb.NoiCap, taikhoannganhang = mb.TaiKhoanNganHang, tenkh = mb.TenKH, diachinhanthu = mb.DiaChiNhanThu, quoctich = mb.QuocTich, emailKhachThue = mb.EmailKhachThue, diaPhan = mb.DiaPhan };
                        var param = new Dapper.DynamicParameters();
                        param.AddDynamicParams(model);
                        var result = Library.Class.Connect.QueryConnect.Query<bool>("tnkhachhang_update_ca_nhan", param);
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

        private string GetNewMaKH()
        {
            string makh = "";
            db.khKhachHang_getNewMaKH(ref makh);
            return makh;
        }

        private void CheckData()
        {
            var data = db.tnKhachHangs.Where(p => p.IsCaNhan.Value).ToList();
            for (int i = 0; i < grvCaNhan.RowCount; i++)
            {
                var MaKH = grvCaNhan.GetRowCellValue(i, colKyHieu).ToString();
                if (data.Where(p => p.KyHieu == MaKH.Trim()).Count() > 0)
                {
                    //grvCaNhan.setr
                    //e.Appearance.BackColor = Color.Blue;
                }
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
            public string KyHieu { get; set; }
            public string MaPhu { get; set; }
            //public string MaThanhToan { get; set; }
            public string HoKH { get; set; }
            public string DongSH { get; set; }
            public string TenKH { get; set; }
            public string DiaChiLL { get; set; }
            public string KhuVuc { get; set; }
            public string DiaChiThuongTru { get; set; }
            public bool GioiTinh { get; set; }
            public string DienThoai { get; set; }
            public string Email { get; set; }
            public string TaiKhoanNganHang { get; set; }
            public string CMND { get; set; }
            public string NgayCap { get; set; }
            public string NoiCap { get; set; }
            public string MaSoThue { get; set; }
            public string NhomKH { get; set; }
            public string DiaChiNhanThu { get; set; }
            public string QuocTich { get; set; }
            //public string EmailChuHopDong { get; set; }
            public string EmailKhachThue { get; set; }
            public string DiaPhan { get; set; }
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