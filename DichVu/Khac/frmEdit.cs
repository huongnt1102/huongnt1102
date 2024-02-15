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

namespace DichVu.Khac
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }
        public int? MaLDV { get; set; }
        public int? MaMB { get; set; }
        MasterDataContext db = new MasterDataContext();
        dvDichVuKhac objDVK;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        void TinhThanhTien()
        {
            if (spKyTT.Value == 0)
            {
                spTienTT.EditValue = spThanhTien.Value * (1 + spVAT.Value);
                spTienVAT.Value = spThanhTien.Value * spVAT.Value;
            }
            else
            {
                // tiền trước thuế 1 tháng
                var TienTruocThue = Math.Round((decimal)((spDonGia.Value * spSoLuong.Value)), 0, MidpointRounding.AwayFromZero);
                var TienVat1Thang = Math.Round((decimal)((TienTruocThue * spVAT.Value)), 0, MidpointRounding.AwayFromZero); 

                spTienVAT.Value = TienVat1Thang * spKyTT.Value;
                spTienTT.EditValue = TienTruocThue * spKyTT.Value + spTienVAT.Value;

                objDVK.TienTruocThue = TienTruocThue * spKyTT.Value;

                //spTienTT.EditValue = spKyTT.Value * Math.Round((decimal)((spThanhTien.Value * (1 + spVAT.Value))),0,MidpointRounding.AwayFromZero);
                //spTienVAT.Value = Math.Round((decimal)(spThanhTien.Value * spVAT.Value), 0, MidpointRounding.AwayFromZero) *spKyTT.Value;

            }

            spTienQD.Value = spTienTT.Value * spTyGia.Value;
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);

            TranslateLanguage.TranslateControl(this);

            try
            {
                lkLoaiDichVu.Properties.DataSource = (from l in db.dvLoaiDichVus
                                                      join bg in db.dvBangGiaDichVus on new { MaLDV = (int?)l.ID, this.MaTN } equals new { bg.MaLDV, bg.MaTN } into tblBangGia
                                                      from bg in tblBangGia.DefaultIfEmpty()
                                                      where l.ParentID == 12
                                                      select new
                                                      {
                                                          l.ID,
                                                          TenLDV = l.TenHienThi,
                                                          bg.DonGia,
                                                          bg.MaLT,
                                                          bg.MaDVT
                                                      }).ToList();

                lkDonViTinh.Properties.DataSource = (from d in db.DonViTinhs select new { d.ID, d.TenDVT }).ToList();
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
                                                        TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                        mb.DienTich,
                                                    }).ToList();
                if (MaMB != null) glkMatBang.EditValue = MaMB;
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
                    objDVK = db.dvDichVuKhacs.Single(p => p.ID == this.ID);
                    txtSoCT.EditValue = objDVK.SoCT;
                    dateNgayCT.EditValue = objDVK.NgayCT;
                    glkMatBang.EditValue = objDVK.MaMB;
                    glkKhachHang.EditValue = objDVK.MaKH;
                    lkLoaiDichVu.EditValue = objDVK.MaLDV;
                    lkLoaiTien.EditValue = objDVK.MaLT;
                    spTyGia.EditValue = objDVK.TyGia;
                    lkDonViTinh.EditValue = objDVK.MaDVT;
                    spSoLuong.EditValue = objDVK.SoLuong;
                    spDonGia.EditValue = objDVK.DonGia;
                    spThanhTien.EditValue = objDVK.ThanhTien;
                    dateNgayTT.EditValue = objDVK.NgayTT;
                    spKyTT.EditValue = objDVK.KyTT;
                    ckbIsLapLai.EditValue = objDVK.IsLapLai;
                    spTienTT.EditValue = objDVK.TienTT;
                    spThanhTien.EditValue = objDVK.ThanhTien;
                    spVAT.EditValue = objDVK.ThueGTGT;
                    spTienVAT.EditValue = objDVK.TienThueGTGT;
                    spTienQD.EditValue = objDVK.TienTTQD;
                    dateTuNgay.EditValue = objDVK.TuNgay;
                    dateDenNgay.EditValue = objDVK.DenNgay;
                    ckbNgungSuDung.EditValue = objDVK.IsNgungSuDung;
                    txtDienGiai.EditValue = objDVK.DienGiai;
                    objDVK.MaNVS = Common.User.MaNV;
                    objDVK.NgaySua = db.GetSystemDate();
                }
                else
                {
                    objDVK = new dvDichVuKhac();
                    objDVK.MaTN = this.MaTN;
                    objDVK.NgayNhap = db.GetSystemDate();
                    objDVK.MaNVN = Common.User.MaNV;
                    db.dvDichVuKhacs.InsertOnSubmit(objDVK);

                    txtSoCT.EditValue = db.CreateSoChungTu(13, this.MaTN);
                    dateNgayCT.EditValue = db.GetSystemDate();
                    dateNgayTT.EditValue = dateNgayCT.EditValue;
                    lkLoaiTien.ItemIndex = 0;
                    if (this.MaLDV != null)
                    {
                        lkLoaiDichVu.EditValue = this.MaLDV;
                    }
                }
            }
            catch { }

            itemHuongDan.Click += ItemHuongDan_Click;
            itemClearText.Click += ItemClearText_Click;
        }

        private void ItemClearText_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        private void ItemHuongDan_Click(object sender, EventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
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

            if (glkMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");  
                return;
            }

            if (lkLoaiDichVu.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn loại dịch vụ");  
                return;
            }

            if (lkLoaiTien.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn loại tiền");
                return;
            }

            if (lkDonViTinh.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn đơn vị tính");  
                return;
            }

            if (spSoLuong.Value <= 0)
            {
                DialogBox.Alert("Vui lòng nhập số lượng");  
                spSoLuong.Focus();
                return;
            }

            if (spDonGia.Value <= 0)
            {
                DialogBox.Alert("Vui lòng nhập đơn giá");  
                spDonGia.Focus();
                return;
            }

            if (spThanhTien.Value <= 0)
            {
                DialogBox.Alert("Vui lòng nhập thành tiền");  
                spThanhTien.Focus();
                return;
            }
            if (db.dvDichVuKhacs.FirstOrDefault(p => p.MaMB == (int?)glkMatBang.EditValue
                & p.MaLDV == this.MaLDV & p.MaTN == this.MaTN) != null
                )
            {
                DialogBox.Error("Phí dịch vụ hiện tại của căn này đã tồn tại trong hệ thống!");  

                return;
            }
            #endregion
            if (this.ID != null)
            {
                if (objDVK.DonGia != (decimal) spDonGia.EditValue)
                {
                    using (var dbo = new MasterDataContext())
                    {
                        LichSuThayDoiDG_DVCB objLS = new LichSuThayDoiDG_DVCB();
                        objLS.DonGiaMoi = (decimal)spDonGia.EditValue;
                        objLS.DonGiaCu = objDVK.DonGia;
                        objLS.NgayNhap = DateTime.Now;
                        objLS.MaDVCB = objDVK.ID;
                        objLS.NguoiNhap = Common.User.MaNV;
                        dbo.LichSuThayDoiDG_DVCBs.InsertOnSubmit(objLS);
                        dbo.SubmitChanges();
                    }
                }
            }
            objDVK.SoCT = txtSoCT.Text;
            objDVK.NgayCT = dateNgayCT.DateTime;
            objDVK.MaMB = (int?)glkMatBang.EditValue;
            objDVK.MaKH = (int)glkKhachHang.EditValue;
            objDVK.MaLDV = (int)lkLoaiDichVu.EditValue;
            objDVK.MaLT = (int)lkLoaiTien.EditValue;
            objDVK.TyGia = spTyGia.Value;
            objDVK.MaDVT = (int)lkDonViTinh.EditValue;
            objDVK.SoLuong = spSoLuong.Value;
            objDVK.DonGia = spDonGia.Value;
            objDVK.ThanhTien = spThanhTien.Value;
            objDVK.NgayTT = dateNgayTT.DateTime;
            objDVK.KyTT = Convert.ToInt32(spKyTT.Value);
            objDVK.IsLapLai = ckbIsLapLai.Checked;
            //objDVK.TienTruocThue = objDVK.KyTT == 0 ? objDVK.ThanhTien : Math.Round((decimal)((spThanhTien.Value * (1 + spVAT.Value))),0,MidpointRounding.AwayFromZero);
            objDVK.ThueGTGT = spVAT.Value;
            objDVK.TienThueGTGT = spTienVAT.Value;
            objDVK.TienTT = spTienTT.Value;
            objDVK.TienTTQD = spTienQD.Value;
            objDVK.TuNgay = (DateTime?)dateTuNgay.EditValue;
            objDVK.DenNgay = (DateTime?)dateDenNgay.EditValue;
            objDVK.IsNgungSuDung = ckbNgungSuDung.Checked;
            objDVK.DienGiai = txtDienGiai.Text;
            
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

        private void spSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            spThanhTien.EditValue = spSoLuong.Value * spDonGia.Value;
        }

        private void spin_EditValueChanged(object sender, EventArgs e)
        {
            this.TinhThanhTien();

            //Set Den ngay
            try
            {
                dateDenNgay.EditValue = dateTuNgay.DateTime.AddDays(Convert.ToDouble(spKyTT.Value * 30));
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

                var _mb = db.mbMatBangs.FirstOrDefault(o => o.MaMB == (int?)glkMatBang.EditValue);
                //if ((int?)lkLoaiDichVu.EditValue == 13 & _mb != null)
                if (_mb != null)
                    spSoLuong.EditValue = _mb.DienTich;
            }
            catch { }
        }

        private void glkMatBang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                db.Dispose();
            }
            catch { }
        }

        private void lkLoaiDichVu_EditValueChanged(object sender, EventArgs e)
        {
            var _mb = db.mbMatBangs.FirstOrDefault(o => o.MaMB == (int?)glkMatBang.EditValue);
            if ((int?)lkLoaiDichVu.EditValue == 13 & _mb != null)
                spSoLuong.EditValue = _mb.DienTich;

            lkLoaiTien.EditValue = lkLoaiDichVu.GetColumnValue("MaLT");  
            lkDonViTinh.EditValue = lkLoaiDichVu.GetColumnValue("MaDVT");  
            spDonGia.EditValue = lkLoaiDichVu.GetColumnValue("DonGia");  
        }

        private void dateNgayTT_EditValueChanged(object sender, EventArgs e)
        {
            dateTuNgay.EditValue = dateNgayTT.EditValue;
        }

        private void spVAT_EditValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }
    }
}