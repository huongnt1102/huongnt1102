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

namespace DichVu.KhachHang.NguoiLienHe
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public int? MaKH { get; set; }
        public bool IsSave { get; set; }
        public string PhoneNumber { get; set; }
        public byte? MaTN;
        MasterDataContext db;
        Library.NguoiLienHe objLH;

        void LienHeLoad()
        {
            objLH = db.NguoiLienHes.Single(p => p.ID == this.ID);
            txtMaHieu.EditValue = objLH.MaHieu;
            lookDanhXung.EditValue = objLH.MaQD;
            txtHoTen.EditValue = objLH.HoTen;
            txtDiaChi.EditValue = objLH.DiaChi;
            glkKhachHang.EditValue = objLH.MaKH;

            var xht = (from t in db.Tinhs
                       join h in db.Huyens on objLH.MaHuyen equals h.MaHuyen
                       join x in db.Xas on objLH.MaXa equals x.MaXa
                       where t.MaTinh == objLH.MaTinh
                       select new { x.TenXa, h.TenHuyen, t.TenTinh }).FirstOrDefault();

            txtDiDong.EditValue = objLH.DiDong;
            txtDiDongKhac.EditValue = objLH.DiDongKhac;
            txtDienThoai.EditValue = objLH.DienThoai;
            txtEmail.EditValue = objLH.Email;
            dateNgaySinh.EditValue = objLH.NgaySinh;
            txtCMND_NLH.EditValue = objLH.SoCMND;
            dateNgayCap_NLH.EditValue = objLH.NgayCap;
            txtNoiCap_NLH.EditValue = objLH.NoiCap;
            txtTenCV.EditValue = objLH.TenCV;
            txtTenPB.EditValue = objLH.TenPB;
            txtDienThoaiCQ.EditValue = objLH.DienThoaiCQ;
            txtFaxCQ.EditValue = objLH.FaxCQ;
            txtGhiChu.EditValue = objLH.GhiChu;
            cmbLoaiBieuMau.SetEditValue(objLH.idLoaiBieuMaus);
            cmbBieuMau.SetEditValue(objLH.idBieuMaus);
            ckbGuiMail.EditValue = objLH.IsMail;
            ckbLock.EditValue = objLH.IsLock;
            SetChiTietMau();
        }

        void LienHeAddNew()
        {
            objLH = new Library.NguoiLienHe();
            txtMaHieu.EditValue = db.DinhDang(14, (db.NguoiLienHes.Max(p => (int?)p.ID) ?? 0) + 1);
            lookDanhXung.EditValue = null;
            txtHoTen.EditValue = null;
            txtDiaChi.EditValue = null;
            glkKhachHang.EditValue = this.MaKH;
            txtDiDong.EditValue = this.PhoneNumber;
            txtDiDongKhac.EditValue = this.PhoneNumber;
            txtDienThoai.EditValue = this.PhoneNumber;
            txtEmail.EditValue = null;
            dateNgaySinh.EditValue = null;
            txtTenCV.EditValue = null;
            txtTenPB.EditValue = null;
            txtDienThoaiCQ.EditValue = null;
            txtFaxCQ.EditValue = null;
            txtGhiChu.EditValue = null;
        }

        void LienHeSave()
        {
            #region Rang buoc
            if (txtMaHieu.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập mã hiệu");
                txtMaHieu.Focus();
                return;
            }
            if (txtHoTen.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên");
                txtHoTen.Focus();
                return;
            }
            #endregion

            objLH.MaHieu = txtMaHieu.Text;
            objLH.MaQD = (short?)lookDanhXung.EditValue;
            objLH.HoTen = txtHoTen.Text;
            objLH.DiaChi = txtDiaChi.Text;
            objLH.MaKH = (int?)glkKhachHang.EditValue;
            objLH.DiDong = txtDiDong.Text;
            objLH.DiDongKhac = txtDiDongKhac.Text;
            objLH.DienThoai = txtDienThoai.Text;
            objLH.Email = txtEmail.Text;
            objLH.NgaySinh = (DateTime?)dateNgaySinh.EditValue;
            objLH.TenCV = txtTenCV.Text;
            objLH.TenPB = txtTenPB.Text;
            objLH.DienThoaiCQ = txtDienThoaiCQ.Text;
            objLH.FaxCQ = txtFaxCQ.Text;
            objLH.GhiChu = txtGhiChu.Text;
            objLH.SoCMND = txtCMND_NLH.Text;
            objLH.NgayCap = (DateTime?)dateNgayCap_NLH.EditValue;
            objLH.NoiCap = txtNoiCap_NLH.Text;
            objLH.idLoaiBieuMaus = Convert.ToString(cmbLoaiBieuMau.EditValue);
            objLH.idBieuMaus = Convert.ToString(cmbBieuMau.EditValue);
            objLH.IsLock = ckbLock.Checked;
            objLH.IsMail = ckbGuiMail.Checked;

            if (objLH.ID == 0)
            {
                objLH.MaNVN = Common.User.MaNV;
                objLH.NgayNhap = DateTime.Now;
                objLH.MaTN = this.MaTN;
                db.NguoiLienHes.InsertOnSubmit(objLH);
            }

            db.SubmitChanges();

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public frmEdit()
        {
            InitializeComponent();

            cmbLoaiBieuMau.Properties.DisplayMember = "TenLBM";
            cmbLoaiBieuMau.Properties.ValueMember = "MaLBM";

            cmbBieuMau.Properties.DisplayMember = "TenBM";
            cmbBieuMau.Properties.ValueMember = "MaBM";

            this.Load += new EventHandler(frmEdit_Load);
        }

        void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LienHeSave();
        }

        void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            lookDanhXung.Properties.DataSource = db.QuyDanhs.ToList();

            glkKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                  where kh.IsChinhThuc.GetValueOrDefault() | kh.IsRoot.GetValueOrDefault()
                                                  orderby kh.KyHieu descending
                                                  select new
                                                  {
                                                      MaKH = kh.MaKH,
                                                      kh.KyHieu,
                                                      TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                      kh.DiaChi
                                                  }).ToList();

            cmbLoaiBieuMau.Properties.DataSource = db.BmLoaiBieuMaus.Select(o => new { o.MaLBM, o.TenLBM }).ToList();

            if (this.ID.GetValueOrDefault() > 0)
                LienHeLoad();
            else
                LienHeAddNew();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LienHeSave();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbLoaiBieuMau_EditValueChanged(object sender, EventArgs e)
        {
            SetChiTietMau();   
        }

        void SetChiTietMau()
        {
            try
            {
                var maLoaiBMs = cmbLoaiBieuMau.EditValue.ToString().Split(',').Where(o => o.Trim().Length > 0).Select(o => int.Parse(o)).ToList();

                cmbBieuMau.Properties.DataSource = db.BmBieuMaus.Where(o => maLoaiBMs.Contains(o.MaLBM.GetValueOrDefault())).Select(o => new { o.MaBM, o.TenBM }).ToList();
            }
            catch
            {
            }
        }
    }
}