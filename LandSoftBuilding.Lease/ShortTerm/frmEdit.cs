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
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Lease.ShortTerm
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db = new MasterDataContext();
        ctNganHan objCTNH;

        void TinhNgayGio()
        {
            spSoNgay.EditValue = (dateDenNgay.DateTime - dateTuNgay.DateTime).Days;
            spSoGio.EditValue = (dateDenNgay.DateTime - dateTuNgay.DateTime).Hours;
        }

        void TinhThanhTien()
        {
            spTienNgay.EditValue = spSoNgay.Value * spGiaNgay.Value;
            spTienGio.EditValue = spSoGio.Value * spGiaGio.Value;
            spTienThue.EditValue = spTienNgay.Value + spTienGio.Value;
            spThanhTien.Value = spTienThue.Value + spTienDV.Value;
            spTienQD.Value = spThanhTien.Value * spTyGia.Value;
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            lkLoaiTien.Properties.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TyGia }).ToList();
            glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                where kn.MaTN == this.MaTN
                                                orderby mb.MaSoMB descending
                                                select new
                                                {
                                                    mb.MaMB,
                                                    mb.MaSoMB,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    kh.MaKH,
                                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                }).ToList();
            glkKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                  where kh.MaTN == this.MaTN
                                                  orderby kh.KyHieu descending
                                                  select new
                                                  {
                                                      kh.MaKH,
                                                      kh.KyHieu,
                                                      TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                      DiaChi = kh.DCLL
                                                  }).ToList();

            if (this.ID != null)
            {
                objCTNH = db.ctNganHans.Single(p => p.ID == this.ID);
                txtSoCT.EditValue = objCTNH.SoCT;
                dateNgayCT.EditValue = objCTNH.NgayCT;
                glkMatBang.EditValue = objCTNH.MaMB;
                glkKhachHang.EditValue = objCTNH.MaKH;
                dateTuNgay.EditValue = objCTNH.TuNgay;
                dateDenNgay.EditValue = objCTNH.DenNgay;
                spSoNgay.EditValue = objCTNH.SoNgay;
                spGiaNgay.EditValue = objCTNH.GiaNgay;
                spTienNgay.EditValue = objCTNH.TienNgay;
                spSoGio.EditValue = objCTNH.SoGio;
                spGiaGio.EditValue = objCTNH.GiaGio;
                spTienGio.EditValue = objCTNH.TienGio;
                spTienThue.EditValue = objCTNH.TienThue;
                spTienDV.EditValue = objCTNH.TienDV;
                spThanhTien.EditValue = objCTNH.ThanhTien;
                lkLoaiTien.EditValue = objCTNH.MaLT;
                spTyGia.EditValue = objCTNH.TyGia;
                spTienQD.EditValue = objCTNH.ThanhTienQD;
                txtDienGiai.EditValue = objCTNH.DienGiai;
                objCTNH.MaNVS = Common.User.MaNV;
                objCTNH.NgaySua = db.GetSystemDate();
            }
            else
            {
                objCTNH = new ctNganHan();
                objCTNH.MaTN = this.MaTN;
                objCTNH.NgayNhap = db.GetSystemDate();
                objCTNH.MaNVN = Common.User.MaNV;
                db.ctNganHans.InsertOnSubmit(objCTNH);

                txtSoCT.EditValue = db.CreateSoChungTu(25, this.MaTN);
                dateNgayCT.EditValue = db.GetSystemDate();
                dateTuNgay.EditValue = dateNgayCT.EditValue;
                dateDenNgay.EditValue = dateNgayCT.EditValue;
                lkLoaiTien.ItemIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (txtSoCT.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập số chứng từ");
                txtSoCT.Focus();
                return;
            }
            else
            {
                var count = db.ctNganHans.Where(p => p.SoCT == txtSoCT.Text && p.MaTN == objCTNH.MaTN && p.ID != objCTNH.ID).Count();
                if (count > 0)
                {
                    DialogBox.Alert("Trùng số chứng từ. Vui lòng kiểm tra lại!");
                    txtSoCT.Focus();
                    return;
                }
            }

            if (glkMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn mặt bằng");
                glkMatBang.Focus();
                return;
            }

            if (glkKhachHang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                glkKhachHang.Focus();
                return;
            }

            if (spSoGio.Value <= 0)
            {
                DialogBox.Alert("Vui lòng nhập số FCU");
                spSoGio.Focus();
                return;
            }

            if (spTienThue.Value <= 0)
            {
                DialogBox.Alert("Vui lòng điền giá cho thuê");
                spTienThue.Focus();
                return;
            }
            #endregion

            objCTNH.SoCT = txtSoCT.Text;
            objCTNH.NgayCT = dateNgayCT.DateTime;
            objCTNH.MaMB = (int)glkMatBang.EditValue;
            objCTNH.MaKH = (int)glkKhachHang.EditValue;
            objCTNH.TuNgay = dateTuNgay.DateTime;
            objCTNH.DenNgay = dateDenNgay.DateTime;
            objCTNH.SoNgay = spSoNgay.Value;
            objCTNH.GiaNgay = spGiaNgay.Value;
            objCTNH.TienNgay = spTienNgay.Value;
            objCTNH.SoGio = spSoGio.Value;
            objCTNH.GiaGio = spGiaGio.Value;
            objCTNH.TienGio = spTienGio.Value;
            objCTNH.TienThue = spTienThue.Value;
            objCTNH.TienDV = spTienDV.Value;
            objCTNH.ThanhTien = spThanhTien.Value;
            objCTNH.MaLT = (int)lkLoaiTien.EditValue;
            objCTNH.TyGia = spTyGia.Value;
            objCTNH.ThanhTienQD = spTienQD.Value;
            objCTNH.IsKetChuyen = ckbIsKetChuyen.Checked;
            objCTNH.DienGiai = txtDienGiai.Text;

            try
            {
                db.SubmitChanges();

                DialogBox.Success("Dữ liệu đã được lưu!");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
            finally
            {
                db.Dispose();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void spin_EditValueChanged(object sender, EventArgs e)
        {
            this.TinhThanhTien();
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                db.Dispose();
            }
            catch { }
        }

        private void lkLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spTyGia.EditValue = lkLoaiTien.GetColumnValue("TyGia");
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glkMatBang.Properties.GetRowByKeyValue(glkMatBang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    glkKhachHang.EditValue = type.GetProperty("MaKH").GetValue(r, null);
                }
            }
            catch { }
        }

        private void glkMatBang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            this.TinhNgayGio();
        }
    }
}