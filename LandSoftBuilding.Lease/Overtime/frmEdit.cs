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

namespace LandSoftBuilding.Lease.Overtime
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
        ctNgoaiGio objCTNG;

        void TinhThanhTien()
        {
            spThanhTien.Value = spSoGio.Value * spDonGia.Value;
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
                objCTNG = db.ctNgoaiGios.Single(p => p.ID == this.ID);
                dateNgayCT.EditValue = objCTNG.NgayCT;
                glkMatBang.EditValue = objCTNG.MaMB;
                glkKhachHang.EditValue = objCTNG.MaKH;
                spSoGio.EditValue = objCTNG.SoGio;
                spDonGia.EditValue = objCTNG.DonGia;
                spThanhTien.EditValue = objCTNG.ThanhTien;
                lkLoaiTien.EditValue = objCTNG.MaLT;
                spTyGia.EditValue = objCTNG.TyGia;
                spTienQD.EditValue = objCTNG.ThanhTienQD;
                txtDienGiai.EditValue = objCTNG.DienGiai;
                objCTNG.MaNVS = Common.User.MaNV;
                objCTNG.NgaySua = db.GetSystemDate();
            }
            else
            {
                objCTNG = new ctNgoaiGio();
                objCTNG.MaTN = this.MaTN;
                objCTNG.NgayNhap = db.GetSystemDate();
                objCTNG.MaNVN = Common.User.MaNV;
                db.ctNgoaiGios.InsertOnSubmit(objCTNG);

                dateNgayCT.EditValue = db.GetSystemDate();
                lkLoaiTien.ItemIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (glkMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn mặt bằng");
                return;
            }

            if (glkMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            if (spSoGio.Value <= 0)
            {
                DialogBox.Alert("Vui lòng nhập số FCU");
                spSoGio.Focus();
                return;
            }

            if (spDonGia.Value <= 0)
            {
                DialogBox.Alert("Vui lòng điền giá cho thuê");
                spDonGia.Focus();
                return;
            }
            #endregion

            objCTNG.NgayCT = dateNgayCT.DateTime;
            objCTNG.MaMB = (int)glkMatBang.EditValue;
            objCTNG.MaKH = (int)glkKhachHang.EditValue;
            objCTNG.SoGio = spSoGio.Value;
            objCTNG.DonGia = spDonGia.Value;
            objCTNG.ThanhTien = spThanhTien.Value;
            objCTNG.MaLT = (int)lkLoaiTien.EditValue;
            objCTNG.TyGia = spTyGia.Value;
            objCTNG.ThanhTienQD = spTienQD.Value;
            objCTNG.DienGiai = txtDienGiai.Text;

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
    }
}