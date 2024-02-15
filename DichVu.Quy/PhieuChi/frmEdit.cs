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

namespace DichVu.Quy.PhieuChi
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaPC { get; set; }
        public int? MaMB { get; set; }
        public byte? MaTN { get; set; }
        public decimal? SoTien { get; set; }
        public DateTime? DotChi { get; set; }
        public string NguoiNhan = "", DiaChi = "";
        public tnNhanVien objNV;
        bool first = true;

        MasterDataContext db = new MasterDataContext();
        Library.PhieuChi objPC;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (objNV.IsSuperAdmin.Value)
            {
                var list = db.tnToaNhas.Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();
                lookUpToaNha.Properties.DataSource = list;
                if (list.Count > 0)
                    lookUpToaNha.EditValue = list[0].MaTN;
            }
            else
            {
                var list2 = db.tnToaNhas.Where(p => p.MaTN == objNV.MaTN).Select(p => new
                {
                    p.MaTN,
                    p.TenTN
                }).ToList();

                lookUpToaNha.Properties.DataSource = list2;
                if (list2.Count > 0)
                    lookUpToaNha.EditValue = list2[0].MaTN;
            }

            if (MaTN != 0)
                lookUpToaNha.EditValue = (byte)MaTN;
            LoadMB();
            //var listTaiKhoan = db.TaiKhoans;
            //lookUpTKCo.Properties.DataSource = listTaiKhoan;
            //lookUpTKNo.Properties.DataSource = listTaiKhoan;

            if (this.MaPC != null)
            {
                objPC = db.PhieuChis.Single(p => p.MaPhieu == this.MaPC);
                txtSoPhieu.EditValue = objPC.SoPhieu;
                dateNgayChi.EditValue = objPC.NgayChi;
                lookUpTKNo.EditValue = objPC.TKNo;
                lookUpTKCo.EditValue = objPC.TKCo;
                spinThucThu.EditValue = objPC.SoTienThanhToan;
                lookUpToaNha.EditValue = (byte?)objPC.MaTN;
                lookMatBang.EditValue = objPC.MaMB;
                lookChuThe.EditValue = objPC.MaKH;
                txtNguoiNhan.EditValue = objPC.NguoiNhan;
                txtDiaChi.EditValue = objPC.DiaChi;
                txtDienGiai.EditValue = objPC.DienGiai;
                txtChungTu.EditValue = objPC.SoChungTu;
                dateDotTT.EditValue = objPC.DotThanhToan;
            }
            else
            {
                objPC = new Library.PhieuChi();

                string soPhieu = "";
                db.btPhieuChi_getNewMaPhieuChi(ref soPhieu);
                txtSoPhieu.Text = "PCDV/" + soPhieu;
                txtSoPhieu.EditValue = soPhieu;
                dateNgayChi.EditValue = DateTime.Now;

                spinThucThu.EditValue = this.SoTien;
                lookUpToaNha.EditValue = MaTN;
                lookMatBang.EditValue = MaMB;

                txtNguoiNhan.Text = NguoiNhan;
                txtDiaChi.Text = DiaChi;
                dateDotTT.EditValue = DotChi;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSoPhieu.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu");
                txtSoPhieu.Focus();
                return;
            }
            else
            {
                var count = db.PhieuChis.Where(p => p.SoPhieu == txtSoPhieu.Text && p.MaPhieu != this.MaPC).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng số phiếu, vui lòng nhập lại");
                    txtSoPhieu.Focus();
                    return;
                }
            }

            if (dateNgayChi.Text == "")
            {
                DialogBox.Error("Vui lòng nhập ngày chi");
                dateNgayChi.Focus();
                return;
            }

            if (spinThucThu.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền phải chi");
                return;
            }

            if (txtNguoiNhan.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập người nộp");
                txtNguoiNhan.Focus();
                return;
            }

            objPC.SoPhieu = txtSoPhieu.Text;
            objPC.NgayChi = dateNgayChi.DateTime;
            objPC.TKCo = lookUpTKCo.Text;
            objPC.TKNo = lookUpTKNo.Text;
            objPC.SoTienThanhToan = spinThucThu.Value;
            objPC.MaTN = Convert.ToByte(lookUpToaNha.EditValue);
            objPC.MaMB = Convert.ToInt32(lookMatBang.EditValue);
            objPC.MaKH = (int?)lookChuThe.EditValue;
            objPC.NguoiNhan = txtNguoiNhan.Text;
            objPC.DiaChi = txtDiaChi.Text;
            objPC.DienGiai = txtDienGiai.Text;
            objPC.SoChungTu = txtChungTu.Text;
            if (dateDotTT.DateTime.Year != 1)
                objPC.DotThanhToan = dateDotTT.DateTime;
            else
                objPC.DotThanhToan = null;

            objPC.MaNV = objNV.MaNV;

            if (this.MaPC == null)
            {
                db.PhieuChis.InsertOnSubmit(objPC);
            }

            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookKhachHang_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void lookUpToaNha_EditValueChanged(object sender, EventArgs e)
        {
            MaTN = Convert.ToByte(lookUpToaNha.EditValue);
            LoadMB();
        }

        void LoadMB()
        {
            lookMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKH != null & p.mbTangLau.mbKhoiNha.tnToaNha.MaTN == MaTN)
                    .Select(p => new
                    {
                        p.MaMB,
                        p.MaKH,
                        p.MaSoMB,
                        TenKH = (bool)p.tnKhachHang.IsCaNhan ? String.Format("{0} {1}", p.tnKhachHang.HoKH, p.tnKhachHang.TenKH) : p.tnKhachHang.CtyTen
                    });
        }

        private void lookMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lookChuThe.Properties.DataSource = db.tnNhanKhaus.Where(p => p.MaMB == (int)lookMatBang.EditValue).Select(p => new { p.ID, p.HoTenNK });
                lookChuThe.ItemIndex = 0;
            }
            catch { }
        }

        private void lookChuThe_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtNguoiNhan.Text = lookChuThe.GetColumnValue("HoTenNK").ToString();
                txtDiaChi.Text = lookMatBang.Text;
            }
            catch { }
        }
    }
}