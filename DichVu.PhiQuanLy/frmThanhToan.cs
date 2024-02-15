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
using DevExpress.XtraEditors.Popup;
using DevExpress.Utils.Win;
using DevExpress.XtraReports.UI;

namespace DichVu.PhiQuanLy
{
    public partial class frmThanhToan : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public int MaMB;
        string sSoPhieu;
        public DateTime? date;
        public decimal soTien = 0;

        MasterDataContext db;
        DateTime now;

        public frmThanhToan()
        {
            InitializeComponent();
            db = new MasterDataContext();
            TranslateLanguage.TranslateControl(this);
        }

        private void datePhiQuanLy_Properties_Popup(object sender, EventArgs e)
        {
            DateEdit edit = sender as DateEdit;
            PopupDateEditForm form = (edit as IPopupControl).PopupWindow as PopupDateEditForm;
            form.Calendar.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.YearsInfo;
        }

        private void btnChapNhan_Click(object sender, EventArgs e)
        {
            var PhiQLDaThu = db.PhiQuanLies.Where(p => p.mbMatBang.MaMB == MaMB & p.ThangThanhToan.Value.Month == datePhiQuanLy.DateTime.Month & p.ThangThanhToan.Value.Year == datePhiQuanLy.DateTime.Year);
            if (PhiQLDaThu.Count() > 0)
            {
                DialogBox.Alert("Phí quản lý tháng vừa chọn đã thanh toán rồi");
                return;
            }

            db.btPhieuThu_getNewMaPhieuThu(ref sSoPhieu);
            try
            {
                decimal phinop = decimal.Parse(txtPhiQuaLy.Text);
                List<Library.PhiQuanLy> lstthuphi = new List<Library.PhiQuanLy>();

                if (phinop < tienql)
                {
                    if (ckDaTTDu.Checked)
                    {
                        Library.PhiQuanLy objpqlx = new Library.PhiQuanLy()
                        {
                            ConNo = 0,
                            DaThanhToan = phinop,
                            MaMB = MaMB,
                            MaNV = objnhanvien.MaNV,
                            ThangThanhToan = datePhiQuanLy.DateTime,
                            NgayThanhToan = now,
                            ChuyenKhoan = ckChuyenKhoan.Checked,
                            SoPhieuThu = "PTPQL/" + sSoPhieu
                        };
                        lstthuphi.Add(objpqlx);
                    }
                    else
                    {
                        Library.PhiQuanLy objpqlx = new Library.PhiQuanLy()
                        {
                            ConNo = tienql - phinop,
                            DaThanhToan = phinop,
                            MaMB = MaMB,
                            MaNV = objnhanvien.MaNV,
                            ThangThanhToan = datePhiQuanLy.DateTime,
                            NgayThanhToan = now,
                            ChuyenKhoan = ckChuyenKhoan.Checked,
                            SoPhieuThu = "PTPQL/" + sSoPhieu
                        };
                        lstthuphi.Add(objpqlx);
                    }
                }
                else
                {
                    int i = 0;
                    
                    while (phinop >= tienql)
                    {
                        Library.PhiQuanLy objpqlx = new Library.PhiQuanLy()
                        {
                            ConNo = 0,
                            DaThanhToan = tienql,
                            MaMB = MaMB,
                            MaNV = objnhanvien.MaNV,
                            ThangThanhToan = datePhiQuanLy.DateTime.AddMonths(i),
                            NgayThanhToan = now,
                            ChuyenKhoan = ckChuyenKhoan.Checked,
                            SoPhieuThu = "PTPQL/" + sSoPhieu
                        };
                        lstthuphi.Add(objpqlx);
                        phinop = phinop - tienql;
                        i++;
                    }
                }
                db.PhiQuanLies.InsertAllOnSubmit(lstthuphi);

                #region Lưu phiếu
                PhieuThu objphieuthu = new PhieuThu()
                {
                    DiaChi = txtDiaChi.Text.Trim(),
                    NguoiNop = txtNguoiNop.Text.Trim(),
                    DichVu = 100,
                    DienGiai = txtDienGiai.Text.Trim(),
                    DotThanhToan = db.GetSystemDate(),
                    MaHopDong = MaMB.ToString(),
                    MaNV = objnhanvien.MaNV,
                    NgayThu = db.GetSystemDate(),
                    SoTienThanhToan = txtPhiQuaLy.Value,
                    SoPhieu = "PTPQL/" + sSoPhieu,
                    KeToanDaDuyet = false,
                    MaMB = MaMB
                };

                db.PhieuThus.InsertOnSubmit(objphieuthu);
                #endregion

                db.SubmitChanges();
                if (DialogBox.Question("Bạn có muốn in phiếu thu không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptPhieuThu rpt = new ReportMisc.DichVu.Quy.rptPhieuThu(objphieuthu.MaPhieu, "no");
                    //rpt.ShowPreviewDialog();
                }
                if (DialogBox.Question("Bạn có muốn in hóa đơn không?") == DialogResult.Yes)
                {
                    //ReportMisc.DichVu.Quy.rptHoaDonHDTH rpt = new ReportMisc.DichVu.Quy.rptHoaDonHDTH(objphieuthu);
                    //rpt.ShowPreviewDialog();
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                DialogBox.Error("Không lưu được dữ liệu, vui lòng thử lại sau");
            }
        }

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            now = db.GetSystemDate();
            var objmb = db.mbMatBangs.Single(p => p.MaMB == MaMB);
            txtMatBang.Text = objmb.MaSoMB;
            ckDaTTDu.Checked = true;
            //Dien ten mac dinh
            try
            {
                txtNguoiNop.Text = objmb.tnKhachHang.IsCaNhan.Value ? String.Format("{0} {1}", objmb.tnKhachHang.HoKH, objmb.tnKhachHang.TenKH) : objmb.tnKhachHang.CtyTen;
                txtDiaChi.Text = objmb.tnKhachHang.DCLL;
                txtDienGiai.Text = string.Format("Thanh toán phí dịch vụ cho mặt bằng {0}", objmb.MaSoMB);
            }
            catch { }
            if (date != null)
                datePhiQuanLy.DateTime = date.Value;
            txtPhiQuaLy.EditValue = soTien;
        }

        decimal tienql = 0;
        private void datePhiQuanLy_EditValueChanged(object sender, EventArgs e)
        {
            var PhiQLDaThu = db.PhiQuanLies.Where(p => p.mbMatBang.MaMB == MaMB & p.ThangThanhToan.Value.Month == datePhiQuanLy.DateTime.Month & p.ThangThanhToan.Value.Year == datePhiQuanLy.DateTime.Year);
            if (PhiQLDaThu.Count() <= 0)
            {
                //var objhdql = db.thueHopDongs.Where(p => p.MaMB == MaMB);
                var objpql = db.mbMatBangs.Single(p => p.MaMB == MaMB);

                //tienql = (objpql.PhiQuanLy ?? 0) + (db.PhiQuanLies.Where(p => p.mbMatBang.MaMB == MaMB).Sum(p=>p.ConNo) ?? 0);

                //if (objhdql.Count() == 0)
                //{
                //    tienql = objpql.PhiQuanLy ?? 0;
                //}
                //else
                //{
                //    tienql = (objhdql.First().PhiQL ?? 0) + (objpql.PhiQuanLy ?? 0);
                //}

                txtPhiQuaLy.Value = tienql;
            }
            else
            {
                DialogBox.Alert("Phí quản lý tháng vừa chọn đã thanh toán rồi");
                txtPhiQuaLy.Value = 0;
            }
            //txtPhiQuaLy.Text = string.Format("{0:#,0.#} VNĐ", tienql);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}