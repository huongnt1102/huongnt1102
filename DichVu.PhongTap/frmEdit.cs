using System;
using System.Windows.Forms;
using System.Linq;
using Library;
using System.Data.Linq.SqlClient;
using DevExpress.XtraReports.UI;

namespace DichVu.PhongTap
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {         
        public dvPhongTap objPT;
        public tnNhanVien objnhanvien;
        public tnNhanKhau objnhankhau;
        MasterDataContext db;
        string sSoPhieu;
        bool IsNew = false;

        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        string getNewMaPT()
        {
            string MaPT = "";
            db.pt_GetNewMaPT(ref MaPT);
            return db.DinhDang(26, int.Parse(MaPT));
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookKhachHang.Properties.DataSource = db.tnNhanKhaus
                    .Select(p => new
                    {
                        p.ID,
                        p.HoTenNK,
                        p.mbMatBang.MaSoMB,
                        //p.tnNhanKhauTrangThai.TenTrangThai
                    });
            try
            {
                lookKhachHang.EditValue = objnhankhau.ID;
            }
            catch
            {
            }
            lookLoaiThe.Properties.DataSource = db.dvPhongTap_Loais;

            if (this.objPT != null)
            {
                IsNew = false;
                objPT = db.dvPhongTaps.Single(p => p.ID == objPT.ID);
                txtSoThe.Text = objPT.SoThe;
                dateNgayDK.EditValue = objPT.NgayDK;
                spinPhiLamThe.EditValue = objPT.PhiLamThe;
                lookLoaiThe.EditValue = objPT.MaLoaiThe;
                txtDienGiai.Text = objPT.DienGiai;
                lookKhachHang.EditValue = objPT.MaNK;
                txtLienLacKhanCap.Text = objPT.LienLacKhanCap;
                txtTinhTrangSucKhoe.Text = objPT.TinhTrangSucKhoe;
                spinThoiHan.Value = SqlMethods.DateDiffMonth(objPT.NgayDK, objPT.NgayHetHan) ?? 0;
                ckIsUse.Checked = objPT.IsInUse ?? false;
                dateNgayHetHan.EditValue = objPT.NgayHetHan;
            }
            else
            {
                IsNew = true;
                objPT = new dvPhongTap();
                txtSoThe.Text = getNewMaPT();
                db.dvPhongTaps.InsertOnSubmit(objPT);
                dateNgayDK.DateTime = DateTime.Now;
                lookLoaiThe.ItemIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSoThe.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập số thẻ");
                txtSoThe.Focus();
                return;
            }
            if (spinThoiHan.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng xem lại ngày đăng ký và hết hạn");
                return;
            }
            if (lookKhachHang.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng chọn cư dân");
                lookKhachHang.Focus();
                return;
            }

            objPT.SoThe = txtSoThe.Text;
            objPT.NgayDK = dateNgayDK.DateTime;
            objPT.PhiLamThe = spinPhiLamThe.Value;
            objPT.PhiHangThang = spinPhiHangThang.Value;
            objPT.dvPhongTap_Loai = db.dvPhongTap_Loais.Single(p => p.ID == (int)lookLoaiThe.EditValue);
            objPT.DienGiai = txtDienGiai.Text;
            objPT.tnNhanKhau = db.tnNhanKhaus.Single(p => p.ID == (int)lookKhachHang.EditValue);
            objPT.TinhTrangSucKhoe = txtTinhTrangSucKhoe.Text.Trim();
            objPT.LienLacKhanCap = txtLienLacKhanCap.Text.Trim();
            objPT.IsInUse = ckIsUse.Checked;
            objPT.NgayHetHan = dateNgayHetHan.DateTime;

            try
            {
                db.SubmitChanges();

                if (IsNew)
                {
                    #region Lưu phiếu
                    db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
                    PhieuThu objphieuthu = new PhieuThu()
                    {
                        DiaChi = objPT.tnNhanKhau.mbMatBang.MaSoMB,
                        NguoiNop = objPT.tnNhanKhau.HoTenNK,
                        DichVu = 500,
                        DienGiai = txtDienGiai.Text.Trim(),
                        DotThanhToan = db.GetSystemDate(),
                        MaHopDong = objPT.ID.ToString(),
                        MaNV = objnhanvien.MaNV,
                        NgayThu = db.GetSystemDate(),
                        SoTienThanhToan = objPT.dvPhongTap_Loai.PhiLamThe + objPT.dvPhongTap_Loai.PhiHangThang,
                        SoPhieu = "PTPT/" + sSoPhieu,
                        KeToanDaDuyet = false,
                        MaMB = objPT.tnNhanKhau.mbMatBang.MaMB
                    };

                    db.PhieuThus.InsertOnSubmit(objphieuthu);
                    #endregion

                    db.SubmitChanges();
                    if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                    {
                        //using (ReportMisc.DichVu.PhongTap.rptPhieuThanhToan frm = new ReportMisc.DichVu.PhongTap.rptPhieuThanhToan(objPT.ID, objphieuthu.SoPhieu))
                        //{
                        //    frm.ShowPreviewDialog();
                        //}
                    }

                    if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                    {
                        //using (ReportMisc.DichVu.PhongTap.rptHoaDon frm = new ReportMisc.DichVu.PhongTap.rptHoaDon(objPT.ID))
                        //{
                        //    frm.ShowPreviewDialog();
                        //}
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lookLoaiXe_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var objloaithe = db.dvPhongTap_Loais.Single(p => p.ID == (int)lookLoaiThe.EditValue);
                spinPhiLamThe.EditValue = objloaithe.PhiLamThe ?? 0;
                spinPhiHangThang.EditValue = objloaithe.PhiHangThang ?? 0;
                spinThoiHan.EditValue = objloaithe.ThoiHan ?? 0;
                dateNgayDK_EditValueChanged(null, null);
                TinhTongTien();
            }
            catch { }
        }

        private void dateNgayDK_EditValueChanged(object sender, EventArgs e)
        {
            if (dateNgayDK.EditValue != null & spinThoiHan.EditValue != null)
            {
                dateNgayHetHan.DateTime = dateNgayDK.DateTime.AddMonths((int)spinThoiHan.Value);
            }
        }

        void TinhTongTien()
        {
            try
            {
                spinTongTien.Value = spinPhiLamThe.Value + spinPhiHangThang.Value;
            }
            catch { }
        }

        private void spinPhiLamThe_EditValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private void spinPhiHangThang_EditValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }
    }
}