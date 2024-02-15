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

namespace LandSoftBuilding.Fund.Deduct
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? MaPT { get; set; }
        public byte? MaTN { get; set; }

        ktttKhauTruThuTruoc objPT;
        MasterDataContext db = new MasterDataContext();

        void TinhSoTien()
        {
            try
            {
                spSoTien.EditValue = objPT.ktttChiTiets.Sum(o => o.SoTien);

                var strDienGiai = "";
                foreach (var i in objPT.ktttChiTiets)
                {
                    strDienGiai += "; " + i.DienGiai;
                }

                strDienGiai = strDienGiai.Trim().Trim(';');
                txtDienGiai.Text = strDienGiai;
            }
            catch { }
        }

        void SavePhieuThu(bool _IsPrint)
        {
            gvChiTietPhieuThu.UpdateCurrentRow();

            #region Rang buoc
            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if (txtSoPT.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPT.Focus();
                return;
            }
            else
            {
                var count = db.ktttKhauTruThuTruocs.Where(p => p.MaTN == objPT.MaTN & p.SoCT == txtSoPT.Text & p.ID != objPT.ID).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng số phiếu. Vui lòng kiểm tra lại");
                    txtSoPT.Focus();
                    return;
                }
            }

            if (dateNgayThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày chứng từ");
                dateNgayThu.Focus();
                return;
            }

            if (spSoTien.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền");
                spSoTien.Focus();
                return;
            }

            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người thu");
                lkNhanVien.Focus();
                return;
            }

            var r = glKhachHang.Properties.GetRowByKeyValue(glKhachHang.EditValue);
            if (r != null)
            {
                var type = r.GetType();
                decimal thuTruoc = (decimal?)type.GetProperty("ThuTruoc").GetValue(r, null) ?? 0;
                if (objPT.SoTien < spSoTien.Value && thuTruoc < spSoTien.Value)
                {
                    DialogBox.Error("Số tiền phiếu thu lớn hơn số tiền thu trước. Vui kiểm tra lại!");
                    spSoTien.Focus();
                    return;
                }
            }
            #endregion

            #region "   Set thong tin"
            //thông tin chứng từ
            objPT.SoCT = txtSoPT.Text;
            objPT.NgayCT = (DateTime)dateNgayThu.EditValue;
            objPT.SoTien = (decimal)spSoTien.EditValue;
            objPT.MaNV = (int)lkNhanVien.EditValue;
            //thong tin chung
            objPT.MaKH = (int)glKhachHang.EditValue;
            objPT.NguoiYC = txtNguoiNop.EditValue + "";
            objPT.DiaChiYC = txtDiaChiNN.EditValue + "";
            objPT.ChungTuGoc = txtChungTuGoc.Text;
            objPT.LyDo = txtDienGiai.EditValue + "";
            #endregion

            try
            {
                db.SubmitChanges();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            gvChiTietPhieuThu.InvalidRowException += Common.InvalidRowException;

            #region " Show  LookupItem"
            lkNhanVien.Properties.DataSource = (from n in db.tnNhanViens where n.MaTN == this.MaTN select new { n.MaNV, n.MaSoNV, n.HoTenNV }).ToList();
            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                     DiaChi = kh.DCLL,
                                                     kh.ThuTruoc
                                                 }).ToList();
            #endregion

            #region " Show Thong tin"
            if (this.MaPT != null)
            {
                objPT = db.ktttKhauTruThuTruocs.Single(pt => pt.ID == MaPT);
                //thông tin chứng từ
                txtSoPT.EditValue = objPT.SoCT;
                dateNgayThu.EditValue = objPT.NgayCT;
                spSoTien.EditValue = objPT.SoTien;
                lkNhanVien.EditValue = Common.User.MaNV;
                //thong tin chung
                glKhachHang.EditValue = objPT.MaKH;
                txtNguoiNop.EditValue = objPT.NguoiYC;
                txtDiaChiNN.EditValue = objPT.DiaChiYC;
                txtDienGiai.EditValue = objPT.LyDo;
                txtChungTuGoc.Text = objPT.ChungTuGoc;
                lkNhanVien.EditValue = objPT.MaNV;

                objPT.MaNVS = Library.Common.User.MaNV;
                objPT.NgaySua = db.GetSystemDate();
            }

            gcChiTietPhieuThu.DataSource = objPT.ktttChiTiets;
            #endregion
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            db.Dispose();
            this.Close();
        }

        private void gvChiTietPhieuThu_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if ((gvChiTietPhieuThu.GetRowCellValue(e.RowHandle, "DienGiai") ?? "").ToString() == "")
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập diễn giải!";
                return;
            }

            var value = (decimal?)gvChiTietPhieuThu.GetFocusedRowCellValue("SoTien") ?? 0;
            if (value <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập số tiền!";
                return;
            }
        }

        private void gvChiTietPhieuThu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    gvChiTietPhieuThu.DeleteSelectedRows();
                    this.TinhSoTien();
                }
            }
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (db != null)
                db.Dispose();
        }

        private void gvChiTietPhieuThu_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                this.TinhSoTien();
            }
            catch { }
        }

        private void glKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glKhachHang.Properties.GetRowByKeyValue(glKhachHang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNop.Text = type.GetProperty("TenKH").GetValue(r, null).ToString();
                    txtDiaChiNN.Text = (from mb in db.mbMatBangs
                                        join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                        where mb.MaKH == (int)glKhachHang.EditValue
                                        select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();
                }
            }
            catch { }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            this.SavePhieuThu(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.SavePhieuThu(false);
        }
    }
}