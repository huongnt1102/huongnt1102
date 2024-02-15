using System.Linq;

namespace LandsoftBuildingGeneral.Import
{
    public partial class FrmUserImport : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTn { get; set; }
        public bool IsSave { get; set; }

        public FrmUserImport()
        {
            InitializeComponent();
        }

        private void itemSheet_EditValueChanged(object sender,System.EventArgs e)
        {
            if (itemSheet.EditValue == null)
                gc.DataSource = null;
            else
                try
                {
                    var excel = new LinqToExcel.ExcelQueryFactory(Tag.ToString());

                    System.Collections.Generic.List<ImportUser> list = Library.Import.ExcelAuto.ConvertDataTable<ImportUser>(Library.Import.ExcelAuto.GetDataExcel(excel, gv, itemSheet));

                    gc.DataSource = list;

                    //gc.DataSource = excel.Worksheet(itemSheet.EditValue.ToString()).Select(_ => new ImportUser
                    //{
                    //    TenDangNhap = _["Tên đăng nhập"].ToString().Trim(),
                    //    HoVaTen = _["Họ và tên"].ToString().Trim(),
                    //    DiaChi = _["Địa chỉ"].ToString().Trim(),
                    //    DienThoai = _["Điện thoại"].ToString().Trim(),
                    //    MatKhau = _["Mật khẩu"].ToString().Trim(),
                    //    Email = _["Email"].ToString().Trim(),
                    //    PhongBan = _["Phòng ban"].ToString().Trim(),
                    //    ChucVu = _["Chức vụ"].ToString().Trim(),

                    //    NgaySinh = _["Ngày sinh"].Cast<System.DateTime>(),

                    //    IsToanQuyenHeThong = _["Toàn quyền hệ thống"].Cast<bool>(),
                    //    IsKhoaTaiKhoan = _["Khóa tài khoản"].Cast<bool>(),
                    //}).ToList();

                    excel = null;
                }
                catch (System.Exception ex)
                {
                    Library.DialogBox.Error(ex.Message);
                }
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Library.DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
            gv.DeleteSelectedRows();
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gc.DataSource == null)
            {
                Library.DialogBox.Error("Vui lòng chọn sheet");
                return;
            }

            var wait = Library.DialogBox.WaitingForm();
            var db = new Library.MasterDataContext();
            try
            {
                var obj = (System.Collections.Generic.List<ImportUser>) gc.DataSource;
                var ltError = new System.Collections.Generic.List<ImportUser>();
                foreach (var n in obj)
                {
                    try
                    {
                        db = new Library.MasterDataContext();

                        #region Kiểm tra
                        // kiểm tra phòng ban có tồn tại
                        var phongBan = db.tnPhongBans.FirstOrDefault(_ => _.TenPB.ToLower() == n.PhongBan.ToLower());
                        if(phongBan == null)
                        {
                            n.PhongBan = "Phòng ban: " + n.PhongBan + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        // kiểm tra chức vụ
                        var chucVu = db.tnChucVus.FirstOrDefault(_ => _.TenCV.ToLower() == n.ChucVu.ToLower());
                        if(chucVu == null)
                        {
                            n.ChucVu = "Chức vụ: " + n.ChucVu + " không tồn tại";
                            ltError.Add(n);
                            continue;
                        }

                        #endregion

                        var nhanVien = db.tnNhanViens.FirstOrDefault(_ => _.MaSoNV.ToLower() == n.TenDangNhap.ToLower());
                        if (nhanVien == null)
                        {
                            nhanVien = new Library.tnNhanVien();
                            db.tnNhanViens.InsertOnSubmit(nhanVien);
                        }

                        nhanVien.MaSoNV = n.TenDangNhap.ToLower();
                        nhanVien.HoTenNV = n.HoVaTen;
                        nhanVien.DienThoai = n.DienThoai;
                        nhanVien.DiaChi = n.DiaChi;
                        nhanVien.Email = n.Email;
                        nhanVien.MatKhau = Library.Common.HashPassword(n.MatKhau);
                        nhanVien.MaPB = phongBan.MaPB;
                        nhanVien.MaCV = chucVu.MaCV;
                        nhanVien.MaTN = MaTn;
                        nhanVien.IsSuperAdmin = n.IsToanQuyenHeThong;
                        nhanVien.SLThongBao = 0;
                        nhanVien.IsLocked = n.IsKhoaTaiKhoan;
                        nhanVien.NgaySinh = n.NgaySinh;

                        db.SubmitChanges();
                    }
                    catch (System.Exception ex)
                    {
                        n.Error = ex.Message;
                        ltError.Add(n);
                    }
                }

                IsSave = true;
                Library.DialogBox.Success();

                gc.DataSource = ltError.Count > 0 ? ltError : null;
            }
            catch
            {
                wait.Close();
                Library.DialogBox.Alert("Vui lòng xem lại dữ liệu có bị ràng buộc hay không");
                Close();
            }
            finally
            {
                wait.Dispose();
                db.Dispose();
            }
        }

        private void itemChonFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file = new System.Windows.Forms.OpenFileDialog();
            try
            {
                file.Filter = @"(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;

                var excel = new LinqToExcel.ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (var s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));

                itemSheet.EditValue = null;
                Tag = file.FileName;
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
            finally
            {
                file.Dispose();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Commoncls.ExportExcel(gc);
        }

    }

    public class ImportUser
    {
        public string TenDangNhap { get; set; }
        public string HoVaTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string PhongBan { get; set; }
        public string ChucVu { get; set; }
        public string Error { get; set; }

        public System.DateTime? NgaySinh { get; set; }

        public bool? IsToanQuyenHeThong { get; set; }
        public bool? IsKhoaTaiKhoan { get; set; }
    }
}