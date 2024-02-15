using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace DichVu.ThueNgoai
{
    public partial class frmPaid : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objnhanvien { get; set; }
        public int MaHD = 0, MaNCC = 0;
        string sSoPhieu;
        public int MaPhieu = 0;
        public decimal soTien = 0;
        PhieuChi objPC;
        DateTime now;
        public frmPaid()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
            now = db.GetSystemDate();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPaid_Load(object sender, EventArgs e)
        {
            if (MaPhieu == 0)
            {
                db.btPhieuChi_getNewMaPhieuChi(ref sSoPhieu);
                txtSoPhieu.Text = "PCHDTN/" + sSoPhieu;
                now = db.GetSystemDate();
                dateNgayChi.DateTime = now;
                spinThucChi.EditValue = soTien;

                var objNCC = db.NhaCungCaps.Single(p => p.MaNCC == MaNCC);
                txtNguoiNop.Text = objNCC.TenNCC;
                txtDiaChi.Text = objNCC.DiaChi;
            }
            else
            {
                objPC = db.PhieuChis.Single(p => p.MaPhieu == MaPhieu);
                txtSoPhieu.Text = objPC.SoPhieu;
                txtDiaChi.Text = objPC.DiaChi;
                txtDienGiai.Text = objPC.DienGiai;
                txtNguoiNop.Text = objPC.NguoiNhan;
                if (objPC.NgayChi != null)
                    dateNgayChi.DateTime = objPC.NgayChi.Value;
                spinThucChi.EditValue = objPC.SoTienThanhToan ?? 0;
            }
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (txtSoPhieu.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Số phiếu chi], xin cảm ơn.");  
                txtSoPhieu.Focus();
                return;
            }

            if (dateNgayChi.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Ngày chi], xin cảm ơn.");  
                dateNgayChi.Focus();
                return;
            }

            if (spinThucChi.Value <= 0)
            {
                DialogBox.Alert("Vui lòng nhập [Số tiền], xin cảm ơn.");  
                spinThucChi.Focus();
                return;
            }

            if (MaPhieu == 0)
            {
                objPC = new PhieuChi();
                objPC.NgayChi = now;

                objPC.MaNV = objnhanvien.MaNV;
                objPC.NgayTao = now;
            }
            else
            {
                objPC.MaNVCN = objnhanvien.MaNV;
                objPC.NgayCN = now;
            }

            objPC.SoPhieu = txtSoPhieu.Text.Trim();
            objPC.DiaChi = txtDiaChi.Text.Trim();
            objPC.NguoiNhan = txtNguoiNop.Text.Trim();
            objPC.DichVu = 4;
            objPC.DienGiai = txtDienGiai.Text.Trim();
            objPC.DotThanhToan = db.GetSystemDate();
            objPC.MaHD = MaHD;
            objPC.MaNV = objnhanvien.MaNV;
            if (dateNgayChi.DateTime.Year != 1)
                objPC.NgayChi = dateNgayChi.DateTime;
            objPC.SoTienThanhToan = spinThucChi.Value;
            objPC.MaNCC = MaNCC;

            try
            {
                if (MaPhieu == 0)
                    db.PhieuChis.InsertOnSubmit(objPC);

                db.SubmitChanges();
                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptPhieuChi rpt = new ReportMisc.DichVu.Quy.rptPhieuChi(objPC);
                    //rpt.ShowPreviewDialog();
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu, vui lòng thử lại sau");  
            }
        }
    }
}