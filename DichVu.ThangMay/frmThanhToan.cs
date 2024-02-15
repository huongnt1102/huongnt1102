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
//using ReportMisc.DichVu;

namespace DichVu.ThangMay
{
    public partial class frmThanhToan : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public dvtmTheThangMay objdvtm;
        public dvtmThanhToanThangMay objTTTM;
        string sSoPhieu;
        int tt;
        public frmThanhToan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }
        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            var lstngay = db.dvtmThanhToanThangMays.Where(p => p.TheThangMayID == objdvtm.ID & !p.DaTT.Value);
            if (lstngay.Count() <= 0)
            {
                DialogBox.Alert("Không có hóa đơn nào cần thanh toán");
                this.Close();
            }
            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
            if (objdvtm != null)
            {
                txtSoPhieu.Text = "PTTM-" + sSoPhieu;
                dateNgayThu.Properties.DataSource = lstngay;
                txtSoThe.Text = objdvtm.SoThe;
                txtChuThe.Text = objdvtm.ChuThe;
                txtMatBang.Text = objdvtm.mbMatBang.MaSoMB;
                txtPhiLamThe.EditValue = objdvtm.PhiLamThe;

                //Dien ten mac dinh
                try
                {
                    txtHoVaTen.Text = objdvtm.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objdvtm.tnKhachHang.HoKH, objdvtm.tnKhachHang.TenKH) : objdvtm.tnKhachHang.CtyTen;
                    txtDiaChi.Text = objdvtm.tnKhachHang.DCLL;
                    txtDienGiai.Text = string.Format("Thanh toán tiền thẻ thang máy có số {0}, mặt bằng {1}", objdvtm.SoThe, objdvtm.mbMatBang.MaSoMB);
                }
                catch { }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtHoVaTen.Text.Trim().Length == 0)
            {
                DialogBox.Error("Vui lòng nhập tên người thanh toán");
                return;
            }
            if (dateNgayThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng tháng thanh toán");
                return;
            }
            
            objTTTM.DiaChi = txtDiaChi.Text.Trim();
            objTTTM.NguoiNop = txtHoVaTen.Text.Trim();
            objTTTM.DaTT = true;
            objTTTM.NgayThanhToan = db.GetSystemDate();
            objTTTM.ChuyenKhoan = ckChuyenKhoan.Checked;
            objTTTM.SoPhieuThu = "PTTM-" + sSoPhieu;

            #region Lưu phiếu
            PhieuThu objphieuthu = new PhieuThu()
            {
                DiaChi = txtDiaChi.Text.Trim(),
                NguoiNop = txtHoVaTen.Text.Trim(),
                //DichVu = (int)ReportMisc.DichVu.EnumLoaiDichVu.DichVuDien,
                DienGiai = txtDienGiai.Text.Trim(),
                DotThanhToan = db.GetSystemDate(),
                MaHopDong = objdvtm.ID.ToString(),
                MaNV = objdvtm.MaNV,
                NgayThu = db.GetSystemDate(),
                SoTienThanhToan = txtPhiLamThe.Value,
                SoPhieu = "PTTM-" + sSoPhieu,
                KeToanDaDuyet = false,
                MaMB = objdvtm.MaMB
            };

            db.PhieuThus.InsertOnSubmit(objphieuthu);
            #endregion
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Thanh toán thành công");
            }
            catch { }
            if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
            {
                //using (ReportMisc.DichVu.ThangMay.Report.frmPrintControl frm = new ReportMisc.DichVu.ThangMay.Report.frmPrintControl(objTTTM.ThanhToanID, "", EnumIn.GiayBao))
                //{
                //    frm.ShowDialog();
                //}
            }
            if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
            {
                //ReportMisc.DichVu.ThangMay.Report.frmPrintControl frm = new ReportMisc.DichVu.ThangMay.Report.frmPrintControl(objTTTM.ThanhToanID, txtSoPhieu.Text,EnumIn.HoaDon);
                //frm.ShowDialog();
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (objTTTM == null)
            {
                DialogBox.Error("Vui lòng chọn tháng thanh toán");
                return;
            }
            //using (ReportMisc.DichVu.ThangMay.Report.frmPrintControl frm = new ReportMisc.DichVu.ThangMay.Report.frmPrintControl(objTTTM.ThanhToanID, "", EnumIn.GiayBao))
            //{
            //    frm.ShowDialog();
            //}
        }

        private void dateNgayThu_EditValueChanged(object sender, EventArgs e)
        {
            tt = (int)dateNgayThu.EditValue;
            objTTTM = db.dvtmThanhToanThangMays.Single(p => p.ThanhToanID == tt);
        }

    }
}