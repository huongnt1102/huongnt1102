using System;
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
    public partial class frmEditHDT : DevExpress.XtraEditors.XtraForm
    {
        public bool IsHDThue = false;
        public long? ID { get; set; }
        public byte? MaTN { get; set; }
        MasterDataContext db = new MasterDataContext();
        dvHoaDon objHD;
        int Thangdv = 0;
        public frmEditHDT()
        {
            InitializeComponent();
        }

        void TinhThanhTien()
        {
            try
            {
                spTienVAT.Value = Math.Round((decimal)(spTienTT.Value * spTyLeVAT.Value), 0, MidpointRounding.AwayFromZero);
                spTienTT.EditValue = spPhiDV.Value * spKyTT.Value;
                spinTienCK.EditValue = Math.Round((decimal)(spTienTT.Value * spinTyLeCK.Value), 0, MidpointRounding.AwayFromZero);
                spinThanhTien.EditValue = spTienTT.Value - spinTienCK.Value + spTienVAT.Value;
                dateDenNgay.EditValue = dateTuNgay.DateTime.AddDays(Convert.ToDouble(spKyTT.Value * 30));
            }
            catch { }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (glkKhachHang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn khách hàng");
                return;
            }

            if (lkLoaiDichVu.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn loại dịch vụ");
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
            #endregion

            objHD.MaMB = (int?)glkMatBang.EditValue;
            objHD.MaKH = (int)glkKhachHang.EditValue;
            objHD.MaLDV = (int)lkLoaiDichVu.EditValue;
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
            //objHD.IsDaXuatHoaDon = ckThuThua.Checked;
            //objHD.idctHopDong = (int?)lkHopDongLienQuan.EditValue;
            // LDV Doanh Thu Giảm Trừ Set số tiền âm
            if (objHD.MaLDV == 49)
            {
                objHD.PhiDV = objHD.PhiDV < 0 ? objHD.PhiDV : -objHD.PhiDV;
                objHD.TienTT = objHD.TienTT < 0 ? objHD.TienTT : -objHD.TienTT;
                objHD.PhaiThu = objHD.PhaiThu < 0 ? objHD.PhaiThu : -objHD.PhaiThu;
            }
            objHD.ConNo = objHD.PhaiThu - objHD.DaThu;
            //objHD.MaKH_CSH = (int)grLKChuSoHuu.EditValue;
            if (ID == null)
            {
                objHD.IsDuyet = true;
                objHD.NgayNhap = DateTime.Now;
                objHD.MaNVN = Common.User.MaNV;
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Thêm hóa đơn dịch vụ", "Lưu", "Dịch vụ:" + lkLoaiDichVu.Text);
            }
            else
            {
                objHD.NgaySua = DateTime.Now;
                objHD.MaNVS = Common.User.MaNV;
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Cập nhật hóa đơn dịch vụ", "Lưu", "Dịch vụ:" + lkLoaiDichVu.Text);
            }

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

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            #region Load tu dien


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
            #endregion

            lkLoaiDichVu.Properties.DataSource = (from l in db.dvLoaiDichVus
                                                  where (l.ID == 61 ||l.ID == 60)
                                                  select new
                                                  {
                                                      l.ID,
                                                      TenLDV = l.TenHienThi,
                                                  }).ToList();
            if (ID != null)
            {
                objHD = db.dvHoaDons.FirstOrDefault(p => p.ID == ID);
                glkKhachHang.Enabled = false;
                glkMatBang.Enabled = false;
                lkLoaiDichVu.Enabled = false;
                glkKhachHang.EditValue = objHD.MaKH;
                //lkHopDongLienQuan.EditValue = objHD.idctHopDong;
                dateNgayTT.EditValue = objHD.NgayTT;
                glkMatBang.EditValue = objHD.MaMB;

                lkLoaiDichVu.EditValue = objHD.MaLDV;

                spPhiDV.EditValue = objHD.PhiDV;

                dateTuNgay.EditValue = objHD.TuNgay;
                spKyTT.EditValue = objHD.KyTT;
                dateDenNgay.EditValue = objHD.DenNgay;

                spTyLeVAT.EditValue = objHD.ThueGTGT;
                spTienVAT.EditValue = objHD.TienThueGTGT;
                spTienTT.EditValue = objHD.TienTT;
                spinTyLeCK.EditValue = objHD.TyLeCK;
                spinTienCK.EditValue = objHD.TienCK;
                spinThanhTien.EditValue = objHD.PhaiThu;
                //ckThuThua.Checked = objHD.IsDaXuatHoaDon.GetValueOrDefault();
                txtDienGiai.EditValue = objHD.DienGiai;
                objHD.MaNVS = Common.User.MaNV;
                objHD.NgaySua = db.GetSystemDate();
            }
            else
            {
                objHD = new dvHoaDon();
                objHD.IsHDThue = true;
                objHD.MaTN = this.MaTN;
                objHD.DaThu = 0;
                objHD.MaNVN = Common.User.MaNV;
                objHD.NgayNhap = db.GetSystemDate();
                db.dvHoaDons.InsertOnSubmit(objHD);
                spTyLeVAT.EditValue = (decimal)0.1;
                spKyTT.EditValue = 1;
                dateNgayTT.EditValue = db.GetSystemDate();
                dateTuNgay.EditValue = dateNgayTT.EditValue;
            }
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
            this.TinhThanhTien();
            var tungay = (DateTime?)dateTuNgay.EditValue;
            if (tungay != null)
                dateDenNgay.EditValue = tungay.Value.AddMonths((int)spKyTT.Value).AddDays(-1);
        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                

                lkHopDongLienQuan.Properties.DataSource = (from hd in db.ctHopDongs
                                                           where hd.MaKH == (int)glkKhachHang.EditValue
                                                           select new
                                                           {
                                                               hd.ID,
                                                               hd.SoHDCT,
                                                               hd.NgayKy
                                                           }).ToList();
                lkHopDongLienQuan.EditValue = null;
                

            }
            catch
            {
                glkMatBang.Properties.DataSource = null;
            }

            //glkMatBang.EditValue = null;
        }

        private void lkLoaiDichVu_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtDienGiai.EditValue = String.Format("{0} tháng {1}", lkLoaiDichVu.GetColumnValue("TenLDV"), ((DateTime)dateNgayTT.EditValue).Month);
            }
            catch { }
        }

        private void dateNgayTT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtDienGiai.EditValue = String.Format("{0} tháng {1}", lkLoaiDichVu.GetColumnValue("TenLDV"), ((DateTime)dateNgayTT.EditValue).Month);
            }
            catch { }
        }

        private void lkTheXe_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            var _MaMB = (int)glkMatBang.EditValue;
            if (_MaMB == null) return;

            var _MaKH = db.mbMatBangs.Single(o => o.MaMB == _MaMB).MaKH;
            if (_MaKH == null) return;

            grLKChuSoHuu.Properties.DataSource = (from kh in db.tnKhachHangs
                                                  where kh.MaKH == _MaKH
                                                  select new
                                                  {
                                                      kh.MaKH,
                                                      kh.KyHieu,
                                                      TenKH = kh.IsCaNhan == true ? kh.HoKH + " " + kh.TenKH : kh.CtyTen,
                                                  }).ToList();
            grLKChuSoHuu.EditValue = _MaKH;
        }

        private void spTyLeVAT_EditValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtSoHoaDon_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtSoHoaDon_KeyDown(object sender, KeyEventArgs e)
        { 

        }

        private void frmEdit_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void lkHopDongLienQuan_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                    join ltt in db.ctLichThanhToans on mb.MaMB equals ltt.MaMB
                                                    join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                    join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                    join kh in db.tnKhachHangs on mb.MaKHF1 equals kh.MaKH
                                                    where mb.MaTN == this.MaTN
                                                    & ltt.MaHD == (int)lkHopDongLienQuan.EditValue
                                                    orderby mb.MaSoMB descending
                                                    select new
                                                    {
                                                        mb.MaMB,
                                                        mb.MaSoMB,
                                                        tl.TenTL,
                                                        kn.TenKN,
                                                        kh.MaKH,
                                                        TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                    }).Distinct().ToList();

                glkMatBang.EditValue = glkMatBang.Properties.GetKeyValue(0);
            }
            catch
            {
            }
        }
    }
}