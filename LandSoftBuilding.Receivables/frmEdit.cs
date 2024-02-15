﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace LandSoftBuilding.Receivables
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public long? ID { get; set; }
        public byte? MaTN { get; set; }
        MasterDataContext db = new MasterDataContext();
        dvHoaDon objHD;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public frmEdit()
        {
            InitializeComponent();
        }

        void TinhThanhTien()
        {
            try
            {
                spTienVAT.Value = spTienTT.Value * spTyLeVAT.Value;
                spTienTT.EditValue = spPhiDV.Value * spKyTT.Value;
                spThanhTien.EditValue = spPhiDV.Value * spKyTT.Value;
                spinTienCK.EditValue = spTienTT.Value * spinTyLeCK.Value;
                spinThanhTien.EditValue = spTienTT.Value - spinTienCK.Value + spTienVAT.Value;
               // dateDenNgay.EditValue = dateTuNgay.DateTime.AddDays(Convert.ToDouble(spKyTT.Value * 30));
            }
            catch { }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void CapNhatTheXe()
        {
            using (var dbo = new MasterDataContext())
            {
                var cn = dbo.dvgxTheXes.Single(p => p.ID == (int)lkTheXe.EditValue & p.IsThangMay == null);
                if (cn.NgayTT <= (DateTime) dateNgayTT.EditValue)
                {
                    cn.NgayTT = cn.NgayTT.Value.AddMonths(1);
                    dbo.SubmitChanges();
                }
                
            }
        }
           

        private void btnLuu_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (glkKhachHang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            if (glkMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn mặt bằng");
                return;
            }

            if (glkLoaiDichVu.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn loại dịch vụ");
                return;
            }
            if ((int)glkLoaiDichVu.EditValue == 6 & lkTheXe.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn thẻ xe");
                return;
            }
            if (spPhiDV.Value == 0)
            {
                DialogBox.Alert("Vui lòng nhập phí dịch vụ");
                return;
            }

            if (spKyTT.Value == 0)
            {
                DialogBox.Alert("Vui lòng nhập kỳ thanh toán");
                return;
            }

            if (glkCompanyCode.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn CompanyCode");
                return;
            }

            #endregion

            #region Kiểm tra khóa hóa đơn
            // Cần trả về là có được phép sửa hay return
            // truyền vào form service, từ ngày đến ngày, tòa nhà

            var result = DichVu.KhoaSo.Class.ClosingEntry.Closing(objHD.MaTN, objHD.NgayTT, DichVu.KhoaSo.Class.Enum.BILL);

            if (result.Count() > 0)
            {
                DialogBox.Error("Hóa đơn đã khóa.");
                return;
            }

            #endregion

            objHD.MaMB = (int?)glkMatBang.EditValue;
            objHD.MaKH = (int)glkKhachHang.EditValue;
            objHD.MaLDV = (int)glkLoaiDichVu.EditValue;
            objHD.NgayTT = (DateTime?)dateNgayTT.EditValue;
            objHD.KyTT = spKyTT.Value;
            objHD.TienTT = spTienTT.Value;
            objHD.ThueGTGT = spTyLeVAT.Value;
            objHD.TienThueGTGT = spTienVAT.Value;
            objHD.PhiDV = spPhiDV.Value;
            objHD.TuNgay = (DateTime?)dateTuNgay.EditValue;
            objHD.DenNgay = (DateTime?)dateDenNgay.EditValue;
            objHD.DienGiai = txtDienGiai.Text;
            objHD.TyLeCK = spinTyLeCK.Value;
            objHD.TienCK = spinTienCK.Value;
            objHD.PhaiThu = spinThanhTien.Value;
            objHD.IsThuThua = ckThuThua.Checked;
            objHD.GhiChu = txtGhiChu.Text;
            objHD.TienTruocThue = (decimal?)spThanhTien.Value;

            if ((int?)glkLoaiDichVu.EditValue == 6 & lkTheXe.EditValue != null)
            {
                objHD.LinkID = (int?)lkTheXe.EditValue;
                objHD.TableName = (int?)glkLoaiDichVu.EditValue == null ? "" : "dvgxTheXe";

                var objTx = db.dvgxTheXes.FirstOrDefault(_ => _.ID == objHD.LinkID);
                if(objTx != null)
                {
                    objHD.MaLX = objTx.MaLX;
                }
            }
            // LDV Doanh Thu Giảm Trừ Set số tiền âm
            if (objHD.MaLDV == 49)
            {
                objHD.PhiDV = objHD.PhiDV < 0 ? objHD.PhiDV : -objHD.PhiDV;
                objHD.TienTT = objHD.TienTT < 0 ? objHD.TienTT : -objHD.TienTT;
                objHD.PhaiThu = objHD.PhaiThu < 0 ? objHD.PhaiThu : -objHD.PhaiThu;
            }
            objHD.ConNo = objHD.PhaiThu - objHD.DaThu;
            if (ID == null)
            {
                objHD.NgayNhap = DateTime.Now;
                objHD.MaNVN = Common.User.MaNV;
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ", "Lưu","Dịch vụ:"+ glkLoaiDichVu.Text );
            }
            else
            {
                objHD.NgaySua = DateTime.Now;
                objHD.MaNVS = Common.User.MaNV;
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cập nhật hóa đơn dịch vụ", "Lưu", "Dịch vụ:" + glkLoaiDichVu.Text);
            }
            if ((int?)glkLoaiDichVu.EditValue == 6  &lkTheXe.EditValue != null)
            {
                CapNhatTheXe();
            }

            objHD.CompanyCode = Convert.ToInt32(glkCompanyCode.EditValue);

            try
            {
                objHD.IsAuTo = false;
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

        private void frmEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            itemClearText.Click += ItemClearText_Click;
            itemHuongDan.Click += ItemHuongDan_Click;

            TranslateLanguage.TranslateControl(this);

            #region Load tu dien
            glkLoaiDichVu.Properties.DataSource = (from l in db.dvLoaiDichVus
                                                  join bg in db.dvBangGiaDichVus on new { MaLDV = (int?)l.ID, this.MaTN } equals new { bg.MaLDV, bg.MaTN } into tblBangGia
                                                  from bg in tblBangGia.DefaultIfEmpty()
                                                  orderby l.TenLDV
                                                  select new
                                                  {
                                                      l.ID,
                                                      TenLDV = l.TenLDV,
                                                      bg.DonGia,
                                                      bg.MaLT,
                                                      bg.MaDVT,
                                                      l.TenVT
                                                  }).ToList();

            glkKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                  join mb in db.mbMatBangs on kh.MaKH equals mb.MaKH into matBang from mb in matBang.DefaultIfEmpty()
                                                  where kh.MaTN == this.MaTN
                                                  orderby kh.KyHieu descending
                                                  select new
                                                  {
                                                      kh.MaKH,
                                                      kh.KyHieu,
                                                      TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.TenKH.ToString() : kh.CtyTen,
                                                      DiaChi = kh.DCLL,
                                                      mb.MaSoMB
                                                  }).ToList();
            glkCompanyCode.Properties.DataSource = db.CompanyCodes;
            #endregion

            if (ID != null)
            {
                objHD = db.dvHoaDons.FirstOrDefault(p => p.ID == ID);

                glkKhachHang.EditValue = objHD.MaKH;
                dateNgayTT.EditValue = objHD.NgayTT;
                glkMatBang.EditValue = objHD.MaMB;
                glkLoaiDichVu.EditValue = objHD.MaLDV;

                spPhiDV.EditValue = objHD.PhiDV;
                
                dateTuNgay.EditValue = objHD.TuNgay;
                spKyTT.EditValue = objHD.KyTT;
                dateDenNgay.EditValue = objHD.DenNgay;

                spTyLeVAT.EditValue = objHD.ThueGTGT;
                spTienVAT.EditValue = objHD.TienThueGTGT;
                spTienTT.EditValue = objHD.TienTT;
                spThanhTien.EditValue = objHD.TienTruocThue;
                spinTyLeCK.EditValue = objHD.TyLeCK;
                spinTienCK.EditValue = objHD.TienCK;
                spinThanhTien.EditValue = objHD.PhaiThu;
                ckThuThua.Checked = objHD.IsThuThua.GetValueOrDefault();
                txtDienGiai.EditValue = objHD.DienGiai;
                lkTheXe.EditValue = objHD.LinkID;
                objHD.MaNVS = Common.User.MaNV;
                objHD.NgaySua = db.GetSystemDate();
                txtGhiChu.Text = objHD.GhiChu;

                if (objHD.CompanyCode != null)
                {
                    glkCompanyCode.EditValue = objHD.CompanyCode;
                }
            }
            else
            {
                objHD = new dvHoaDon();
                objHD.MaTN = this.MaTN;
                objHD.DaThu = 0;
                objHD.MaNVN = Common.User.MaNV;
                objHD.NgayNhap = db.GetSystemDate();
                db.dvHoaDons.InsertOnSubmit(objHD);

                spKyTT.EditValue = 1;
                dateNgayTT.EditValue = db.GetSystemDate();
                dateTuNgay.EditValue = dateNgayTT.EditValue;
            }
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                db.Dispose();
            }
            catch { }
        }

        private void spinEditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var a = Convert.ToString(glkLoaiDichVu.Properties.GetDisplayText(glkLoaiDichVu.EditValue)); // glkLoaiDichVu.GetColumnValue("TenLDV")
                txtDienGiai.EditValue = String.Format("{0} tháng {1}", a, ((DateTime)dateNgayTT.EditValue).Month);

                if ((int?)glkLoaiDichVu.EditValue != null & (int?)glkMatBang.EditValue != null)
                {
                    if ((int)glkLoaiDichVu.EditValue == 6)
                    {
                        txtDienGiai.EditValue = String.Format("{0} biển số {1} tháng {2}/{3}", a, lkTheXe.GetColumnValue("BienSo").ToString(), ((DateTime)dateTuNgay.EditValue).Month, ((DateTime)dateTuNgay.EditValue).Year);
                    }
                }
            }
            catch { }

            this.TinhThanhTien();
        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                    join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                    join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                    join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                    where mb.MaTN == this.MaTN & mb.MaKH == (int)glkKhachHang.EditValue
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
                glkMatBang.EditValue = glkMatBang.Properties.GetKeyValue(0);
            }
            catch
            {
                glkMatBang.Properties.DataSource = null;
            }

            //glkMatBang.EditValue = null;
        }

        private void lkLoaiDichVu_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateNgayTT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var TenLDV = Convert.ToString(glkLoaiDichVu.Properties.GetDisplayText(glkLoaiDichVu.EditValue));
                txtDienGiai.EditValue = String.Format("{0} tháng {1}", Convert.ToString(TenLDV), ((DateTime)dateNgayTT.EditValue).Month);

                if ((int?)glkLoaiDichVu.EditValue != null & (int?)glkMatBang.EditValue != null)
                {
                    if ((int)glkLoaiDichVu.EditValue == 6)
                    {
                        txtDienGiai.EditValue = String.Format("{0} biển số {1} tháng {2}/{3}", Convert.ToString(TenLDV), lkTheXe.GetColumnValue("BienSo").ToString(), ((DateTime)dateTuNgay.EditValue).Month, ((DateTime)dateTuNgay.EditValue).Year);
                    }
                }
            }
            catch { }
        }

        private void lkTheXe_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var TenLDV = Convert.ToString(glkLoaiDichVu.Properties.GetDisplayText(glkLoaiDichVu.EditValue));
                if ((int?)glkLoaiDichVu.EditValue != null & (int?)glkMatBang.EditValue != null)
                {
                    if ((int)glkLoaiDichVu.EditValue == 6)
                    {
                        txtDienGiai.EditValue = String.Format("{0} biển số {1} tháng {2}/{3}", Convert.ToString(TenLDV), lkTheXe.GetColumnValue("BienSo").ToString(), ((DateTime)dateTuNgay.EditValue).Month, ((DateTime)dateTuNgay.EditValue).Year);
                    }
                }
            }
            catch { }
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            if ((int?)glkLoaiDichVu.EditValue != null & (int?)glkMatBang.EditValue != null)
            {
                if ((int)glkLoaiDichVu.EditValue == 6)
                {
                    lkTheXe.Properties.DataSource =
                        db.dvgxTheXes.Where(p => p.MaMB == (int)glkMatBang.EditValue & p.IsThangMay == null);
                }
                else
                    lkTheXe.Properties.DataSource = null;
            }
        }

        private void spTyLeVAT_EditValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void glkLoaiDichVu_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var TenLDV = Convert.ToString(glkLoaiDichVu.Properties.GetDisplayText(glkLoaiDichVu.EditValue));
                txtDienGiai.EditValue = String.Format("{0} tháng {1}", Convert.ToString( TenLDV), ((DateTime)dateTuNgay.EditValue).Month);
                if ((int?)glkLoaiDichVu.EditValue != null & (int?)glkMatBang.EditValue != null)
                {
                    if ((int)glkLoaiDichVu.EditValue == 6)
                    {
                        lkTheXe.Properties.DataSource =
                            db.dvgxTheXes.Where(p => p.MaMB == (int)glkMatBang.EditValue & p.IsThangMay == null);
                        txtDienGiai.EditValue = String.Format("{0} biển số {1} tháng {2}/{3}", TenLDV, lkTheXe.GetColumnValue("BienSo").ToString(), ((DateTime)dateTuNgay.EditValue).Month, ((DateTime)dateTuNgay.EditValue).Year);
                    }
                    else
                        lkTheXe.Properties.DataSource = null;

                    var companyCodeData = Library.Class.Connect.QueryConnect.QueryData<CompanyCode>("ccCompanyCode_Get",
                        new
                        {
                            MaMB = (int?)glkMatBang.EditValue,
                            MaLDV = (int)glkLoaiDichVu.EditValue
                        });
                    if (companyCodeData.Count() > 0)
                    {
                        glkCompanyCode.Properties.DataSource = companyCodeData.ToList();
                        glkCompanyCode.EditValue = companyCodeData.First().ID;
                    }
                }
            }
            catch { }
        }
    }
}