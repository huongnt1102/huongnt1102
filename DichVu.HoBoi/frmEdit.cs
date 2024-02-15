using System;
using System.Windows.Forms;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;

namespace DichVu.HoBoi
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public dvhbHoBoi objHB;
        public tnNhanVien objnhanvien;
        public mbMatBang objmatbang;
        MasterDataContext db;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        void getMucPhi()
        {
            try
            {
                var malt = (short?)lookLoaiThe.EditValue ?? 0;
                var ngaydk = dateNgayDangKy.DateTime;
                var phi = db.dvhbDinhMucs.Where(p => p.MaLT == malt & SqlMethods.DateDiffDay(p.TuNgay, ngaydk) >= 0 & SqlMethods.DateDiffDay(ngaydk, p.DenNgay) >= 0).FirstOrDefault();
                spinMucPhi.EditValue = phi.MucPhi;
            }
            catch
            {
                spinMucPhi.EditValue = 0;
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookLoaiThe.Properties.DataSource = db.dvhbLoaiThes.Select(p => new { p.ID, p.TenLT, p.STT }).OrderBy(p => p.STT).ToList();
            if (objnhanvien.IsSuperAdmin.Value)
            {
                lookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKH != null)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaKH,
                        p.MaSoMB,
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen
                    });
            }
            else
            {
                lookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKH != null & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == objnhanvien.MaTN)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaKH,
                        p.MaSoMB,
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen
                    });
            }
            try
            {
                lookMatBang.EditValue = objmatbang.MaMB;
            }
            catch
            {
            }

            if (this.objHB != null)
            {
                objHB = db.dvhbHoBois.Single(p => p.ID == objHB.ID);
                txtSoThe.Text = objHB.SoThe;
                lookLoaiThe.EditValueChanged -= new EventHandler(lookLoaiThe_EditValueChanged);
                dateNgayDangKy.EditValueChanged -= new EventHandler(dateNgayDangKy_EditValueChanged);
                dateNgayDangKy.EditValue = objHB.NgayDangKy;
                dateNgayHetHan.EditValue = objHB.NgayHetHan;
                lookMatBang.EditValue = objHB.MaMB;
                lookNhanKhau.EditValue = objHB.MaNK;
                txtChuThe.Text = objHB.ChuThe;
                lookLoaiThe.EditValue = objHB.MaLT;
                spinMucPhi.EditValue = objHB.MucPhi??0;
                txtDienGiai.Text = objHB.DienGiai;
                rdgSuDung.EditValue = objHB.IsSuDung.GetValueOrDefault();
                chkTinhDuThang.EditValue = objHB.IsTinhDuThang.GetValueOrDefault();

                lookLoaiThe.EditValueChanged += new EventHandler(lookLoaiThe_EditValueChanged);
                dateNgayDangKy.EditValueChanged += new EventHandler(dateNgayDangKy_EditValueChanged);
                objHB.MaNVCN = objnhanvien.MaNV;
                objHB.NgayCN = db.GetSystemDate();
            }
            else
            {
                objHB = new dvhbHoBoi();
                db.dvhbHoBois.InsertOnSubmit(objHB);
                var MaHB = "";
                db.dvhbHoBoi_getNew(ref MaHB);
                txtSoThe.Text = MaHB;
                objHB.MaNV = objnhanvien.MaNV;
                objHB.NgayTao = db.GetSystemDate();
                rdgSuDung.EditValue = true;
                chkTinhDuThang.EditValue = true;
                dateNgayDangKy.EditValue = db.GetSystemDate();
                dateNgayHetHan.EditValue = db.GetSystemDate().AddMonths(12);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSoThe.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập [Số thẻ]");
                txtSoThe.Focus();
                return;
            }

            if (lookLoaiThe.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn [Loại thẻ]");
                lookLoaiThe.Focus();
                return;
            }

            if (lookMatBang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn [Mặt bằng]");
                lookMatBang.Focus();
                return;
            }

            if (dateNgayDangKy.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập [Ngày đăng ký]");
                dateNgayDangKy.Focus();
                return;
            }

            if (dateNgayDangKy.DateTime.CompareTo(dateNgayHetHan.DateTime) > 0)
            {
                DialogBox.Error("[Ngày đăng ký] phải trước [Ngày hết hạn]");
                dateNgayHetHan.Focus();
                return;
            }

            //if (spinMucPhi.Value <=0)
            //{
            //    DialogBox.Error("[Mức phí] phải lớn hơn 0!");
            //    spinMucPhi.Focus();
            //    return;
            //}

            if (txtChuThe.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập [Chủ thẻ]");
                txtSoThe.Focus();
                return;
            }

            objHB.MaMB = (int)lookMatBang.EditValue;
            objHB.SoThe = txtSoThe.Text;
            objHB.NgayDangKy = (DateTime)dateNgayDangKy.EditValue;
            objHB.NgayHetHan = (DateTime)dateNgayHetHan.EditValue;
            objHB.MaLT = (short)lookLoaiThe.EditValue;
            objHB.MaNK = (int?)lookNhanKhau.EditValue;
            objHB.MucPhi = spinMucPhi.Value;
            objHB.ChuThe = txtChuThe.Text;
            objHB.IsSuDung = (bool)rdgSuDung.EditValue;
            objHB.IsTinhDuThang = (bool)chkTinhDuThang.EditValue;
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được lưu.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                DialogBox.Alert("Lưu dữ liệu không thành công! Ràng buộc dữ liệu không cho phép thực hiện thao tác này hoặc đường truyền không ổn định!");
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lookChuHo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                lookNhanKhau.EditValue = null;
                lookNhanKhau.ClosePopup();
            }
        }

        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lookNhanKhau.Properties.DataSource = db.tnNhanKhaus.Where(p => p.MaMB == (int)lookMatBang.EditValue).Select(p => new { p.ID, p.HoTenNK });
            }
            catch { }
        }

        private void lookNhanKhau_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtChuThe.Text = ((DevExpress.XtraEditors.LookUpEdit)(sender)).GetColumnValue("HoTenNK").ToString();
            }
            catch { }
        }

        private void lookLoaiThe_EditValueChanged(object sender, EventArgs e)
        {
            var sp = ((DevExpress.XtraEditors.LookUpEdit)sender).EditValue;
            lookLoaiThe.EditValue = sp;
            getMucPhi();
        }

        private void dateNgayDangKy_EditValueChanged(object sender, EventArgs e)
        {
            getMucPhi();
        }
    }
}