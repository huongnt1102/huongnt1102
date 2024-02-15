using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using Library;
using System.Data.Linq.SqlClient;
namespace DichVu.YeuCau
{
    public partial class frmYeuCauKHThangLong : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }
        public int? ID { get; set; }

        MasterDataContext db;
        tnycYeuCau objYC;

        public frmYeuCauKHThangLong()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        string getNewMaYC()
        {
            string MaYC = "";
            db.tnycYeuCau_getNewMaYC(ref MaYC);
            return db.DinhDang(4, int.Parse(MaYC));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lkYeuCau.Properties.DataSource = db.tnycLoaiYeuCaus;
            searchLookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaTN == this.MaTN).Select(p => new
            {
                p.MaMB,
                p.MaSoMB,
                p.mbTangLau.TenTL,
                p.mbTangLau.mbKhoiNha.TenKN,
                p.mbTangLau.mbKhoiNha.tnToaNha.TenTN,
                p.MaKH,
                TenKH = p.tnKhachHang.IsCaNhan.GetValueOrDefault() ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen
            }).ToList();
            lookBoPhan.Properties.DataSource = db.tnPhongBans.Select(p => new { p.MaPB, p.TenPB }).ToList();
            lookTrangThai.Properties.DataSource = db.tnycTrangThais.Select(p => new { p.MaTT, p.TenTT });
            lookDoUuTien.Properties.DataSource = db.tnycDoUuTiens;
            lookNguonDen.Properties.DataSource = db.tnycNguonDens;

            if (this.ID != null)
            {
                objYC = db.tnycYeuCaus.Single(p => p.ID == this.ID);
                searchLookMatBang.EditValue = objYC.MaMB;
                dateNgayYC.EditValue = objYC.NgayYC;
                lookBoPhan.EditValue = objYC.MaBP;
                lookCuDan.EditValue = objYC.MaKH;
                txtTieuDe.Text = objYC.TieuDe;
                txtNoiDung.Text = objYC.NoiDung;
                lookDoUuTien.EditValue = objYC.MaDoUuTien;
                lookNguonDen.EditValue = objYC.MaNguonDen;
                txtMaYC.Text = objYC.MaYC;
                txtMaYC.Properties.ReadOnly = true;
                txtNguoiGui.Text = objYC.NguoiGui;
                lookTrangThai.EditValue = objYC.MaTT;
                lkYeuCau.EditValue = objYC.MaLoaiYeuCau;
            }
            else
            {
                objYC = new tnycYeuCau();
                objYC.MaNV = Common.User.MaNV;
                objYC.MaTN = this.MaTN;
                txtMaYC.Text = getNewMaYC();
                dateNgayYC.DateTime = db.GetSystemDate();
                db.tnycYeuCaus.InsertOnSubmit(objYC);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMaYC.Text.Trim().Length == 0)
            {
                DialogBox.Error("Mã yêu cầu không được để trống");
                txtMaYC.Focus();
                return;
            }

            if (dateNgayYC.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập ngày yêu cầu");
                dateNgayYC.Focus();
                return;
            }

            if (txtTieuDe.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập tiêu đề");
                txtTieuDe.Focus();
                return;
            }
            if (txtNoiDung.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập nội dung");
                txtNoiDung.Focus();
                return;
            }

            if (lookDoUuTien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn độ ưu tiên");
                return;
            }

            if (lookBoPhan.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn độ bộ phận nhận yêu cầu");
                return;
            }

            objYC.MaYC = txtMaYC.Text;
            objYC.NgayYC = dateNgayYC.DateTime;
            objYC.MaTT = (int?)lookTrangThai.EditValue;
            objYC.MaBP = (int?)lookBoPhan.EditValue;
            objYC.MaKH = (int?)lookCuDan.EditValue;
            objYC.MaMB = (int?)searchLookMatBang.EditValue;
            objYC.TieuDe = txtTieuDe.Text;
            objYC.NoiDung = txtNoiDung.Text;
            objYC.MaDoUuTien = (int?)lookDoUuTien.EditValue;
            objYC.MaNguonDen = (int?)lookNguonDen.EditValue;
            objYC.NguoiGui = txtNguoiGui.Text.Trim();
            objYC.MaLoaiYeuCau = (int?)lkYeuCau.EditValue;
            //save LS
            var objLS = new tnycLichSuCapNhat();
            objYC.tnycLichSuCapNhats.Add(objLS);
            objLS.MaNV = Common.User.MaNV;
            objLS.NgayCN = db.GetSystemDate();
            objLS.MaTT = (int?)lookTrangThai.EditValue;
            objLS.NoiDung = objYC.ID != 0 ? "[Cập nhật yêu cầu]" : "[Thêm mới yêu cầu]";

        save:
            try
            {
                if (objYC.ID == 0)
                {
                    var ltTB = db.tnycCaiDats.Where(p => p.MaTN == objYC.MaTN & p.MaPB == p.MaPB & p.IsRemind == true).Select(p => new { p.MaNV }).Distinct().ToList();
                    foreach (var i in ltTB)
                    {
                        tnycThongBao objTB = new tnycThongBao();
                        objTB.NgayBD = db.GetSystemDate();
                        objTB.NgayKT = db.GetSystemDate().AddDays(1);
                        objTB.MaNV = i.MaNV;
                        objTB.IsNhac = true;
                        objTB.IsRepeat = true;
                        objTB.TimeID = (byte)1;
                        objTB.TimeID2 = (byte)1;                        
                        var obj = db.Times.Single(p => p.TimeID == (byte?)1);
                        objTB.NgayNhac = objTB.NgayBD.Value.AddMinutes(-obj.Minutes.GetValueOrDefault());
                        objTB.tnycYeuCau = objYC;
                        db.tnycThongBaos.InsertOnSubmit(objTB);
                    }
                }

                db.SubmitChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch
            {
                txtMaYC.Text = getNewMaYC();
                goto save;
            }
        }

        void GuiMailList()
        {
            using (var db = new MasterDataContext())
            {
                var objCaiDat = db.tnCaiDatMails.Where(p => SqlMethods.DateDiffDay(objYC.NgayYC, p.TuNgay) >= 0 &
                        SqlMethods.DateDiffDay(p.TuNgay, objYC.NgayYC) >= 0 & p.MaPB == objYC.MaBP).ToList();
                foreach (var cd in objCaiDat)
                {
                    string[] dsmnv = cd.DSMaNhanVien.Split(':');
                    var listNhanvien = db.tnNhanViens.Where(p => dsmnv.Contains(p.MaNV.ToString())).Select(p => new { p.Email }).ToList();
                    foreach (var nv in listNhanvien)
                    {
                        MailProviderCls objMail = new MailProviderCls();
                        objMail.MailFrom = cd.SendMailAccount.DiaChi;
                        var objMailForm = new MailAddress(cd.SendMailAccount.DiaChi, "Indochina IPH");
                        objMail.MailAddressFrom = objMailForm;
                        var objMailTo = new MailAddress(nv.Email);
                        objMail.MailAddressTo = objMailTo;
                        objMail.SmtpServer = cd.SendMailAccount.Server;
                        objMail.EnableSsl = true;
                        objMail.PassWord =Commoncls.DecodeString(cd.SendMailAccount.Password);
                        objMail.Subject = objYC.TieuDe;
                        objMail.Content = objYC.NoiDung;
                        try
                        {
                            objMail.SendMailV2();
                        }
                        catch { }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void searchLookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var objMB = db.mbMatBangs.Where(p => p.MaMB == (int?)searchLookMatBang.EditValue).Select(p => new { TenKH = p.tnKhachHang.IsCaNhan.GetValueOrDefault() ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen, p.tnKhachHang.DienThoaiKH }).FirstOrDefault();
                txtChuMatBang.EditValue = objMB.TenKH;
                txtDienThoai.EditValue = objMB.DienThoaiKH;
                lookCuDan.Properties.DataSource = db.tnNhanKhaus.Where(p => p.MaMB == (int)searchLookMatBang.EditValue).Select(p => new { p.ID, p.HoTenNK, SoCMND = p.CMND, p.DienThoai }).ToList();
            }
            catch { }
        }

        private void lookCuDan_EditValueChanged(object sender, EventArgs e)
        {
            txtNguoiGui.Text = lookCuDan.Text;
        }

        private void searchLookMatBang_SizeChanged(object sender, EventArgs e)
        {
            searchLookMatBang.Properties.PopupFormSize = new Size(searchLookMatBang.Size.Width, 0);
        }

        private void lookCuDan_SizeChanged(object sender, EventArgs e)
        {
            lookCuDan.Properties.PopupFormSize = new Size(lookCuDan.Size.Width, 0);
        }
    }
}