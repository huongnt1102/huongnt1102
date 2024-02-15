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

namespace DichVu.Nuoc.NuocSinhHoat
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public byte? MaTN { get; set; }
        public int? ID { get; set; }
        public bool IsSave { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }

        MasterDataContext db;
        dvNuocSinhHoat objNuoc;
        CachTinhCls objCachTinh;

        void LoadRecord()
        {
            try
            {
                db = new MasterDataContext();
                if (this.ID != null)
                {
                    objNuoc = db.dvNuocSinhHoats.Single(p => p.ID == this.ID);
                    spinThang.EditValue = objNuoc.NgayTB.Value.Month;
                    spinNam.EditValue = objNuoc.NgayTB.Value.Year;
                }
                else
                {
                    objNuoc = new dvNuocSinhHoat();
                    spinThang.EditValue = Thang;
                    spinNam.EditValue = Nam;
                }

                txtKhachHang.EditValue = null;
                glkMatBang.EditValue = objNuoc.MaMB;
                dateTuNgay.EditValue = objNuoc.TuNgay;
                dateDenNgay.EditValue = objNuoc.DenNgay;
                spChiSoCu.EditValue = objNuoc.ChiSoCu;
                spChiSoMoi.EditValue = objNuoc.ChiSoMoi;
                spSoTieuThuNL.EditValue = objNuoc.SoTieuThuNL;
                spDauCap_Cu.EditValue = objNuoc.DauCap_Cu;
                spDauCap_Moi.EditValue = objNuoc.DauCap_Moi;
                spDauHoi_Cu.EditValue = objNuoc.DauHoi_Cu;
                spDauHoi_Moi.EditValue = objNuoc.DauHoi_Moi;
                spSoTieuThuNN.EditValue = objNuoc.SoTieuThuNN;
                spSoTieuThu.EditValue = objNuoc.SoTieuThu;
                spThanhTien.EditValue = objNuoc.ThanhTien;
                spTyLeVAT.EditValue = objNuoc.TyLeVAT;
                spTienVAT.EditValue = objNuoc.TienVAT;
                spTyLeBVMT.EditValue = objNuoc.TyLeBVMT;
                spTienBVMT.EditValue = objNuoc.TienBVMT;
                spTienTT.EditValue = objNuoc.TienTT;
                txtDienGiai.EditValue = objNuoc.DienGiai;
                dateNgayTT.EditValue = objNuoc.NgayTT;
            }
            catch { }
        }

        void TinhThanhTien()
        {
            spThanhTien.EditValue = objCachTinh.GetThanhTien();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            db = new MasterDataContext();

            objCachTinh = new CachTinhCls();
            objCachTinh.MaTN = this.MaTN.Value;

            glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                join ud in db.dvNuocUuDais on mb.MaMB equals ud.MaMB into tblUuDai
                                                from ud in tblUuDai.DefaultIfEmpty()
                                                where mb.MaTN == this.MaTN
                                                orderby mb.MaSoMB
                                                select new
                                                {
                                                    mb.MaMB,
                                                    mb.MaSoMB,
                                                    ud.MucUuDai,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    kh.MaKH,
                                                    kh.KyHieu,
                                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                }).ToList();

            

            this.LoadRecord();
        }

        private void glkMatBang_SizeChanged(object sender, EventArgs e)
        {
            glkMatBang.Properties.PopupFormSize = new Size(glkMatBang.Size.Width, 0);
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glkMatBang.Properties.GetRowByKeyValue(glkMatBang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    var _MaMB = (int)glkMatBang.EditValue;
                    var _MucUuDai = (int?)type.GetProperty("MucUuDai").GetValue(r, null) ?? 0;

                    //Load dinh muc
                    objCachTinh.MaMB = _MaMB;
                    objCachTinh.SoUuDai = _MucUuDai;
                    objCachTinh.LoadDinhMuc();
                    gcChiTiet.DataSource = objCachTinh.ltChiTiet;

                    //Set khach hang
                    var _MaKH = (int)type.GetProperty("MaKH").GetValue(r, null);
                    txtKhachHang.EditValue = type.GetProperty("TenKH").GetValue(r, null);
                    txtKhachHang.Tag = _MaKH;

                    //Set tu ngay
                    var _TuNgay = (from g in db.dvNuocSinhHoats where g.MaMB == _MaMB & g.MaKH == _MaKH select g.DenNgay).Max();
                    if (_TuNgay != null) _TuNgay = _TuNgay.Value.AddDays(1);
                    dateTuNgay.EditValue = _TuNgay ?? db.GetSystemDate().AddMonths(-1);

                    //Set chi so
                    var _ChiSoCu = (from g in db.dvNuocSinhHoats where g.MaMB == _MaMB & g.MaKH == _MaKH select g.ChiSoMoi).Max();
                    spChiSoCu.EditValue = _ChiSoCu ?? 0;

                    var _DauCapCu = (from g in db.dvNuocSinhHoats where g.MaMB == _MaMB & g.MaKH == _MaKH select g.DauCap_Cu).Max();
                    spDauCap_Cu.EditValue = _DauCapCu ?? 0;

                    var _DauHoiCu = (from g in db.dvNuocSinhHoats where g.MaMB == _MaMB & g.MaKH == _MaKH select g.DauHoi_Cu).Max();
                    spDauHoi_Cu.EditValue = _DauHoiCu ?? 0;
                }
            }
            catch { }
        }

        private void spChiSoCu_EditValueChanged(object sender, EventArgs e)
        {
            if (spChiSoMoi.Value > spChiSoCu.Value)
            {
                spSoTieuThuNL.EditValue = spChiSoMoi.Value - spChiSoCu.Value;
            }
            else
            {
                spSoTieuThuNL.EditValue = 0;
            }
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateDenNgay.EditValue = dateTuNgay.DateTime.AddMonths(1).AddDays(-1);
            }
            catch { }
        }

        private void spSoTieuThu_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //Tinh tien theo chi so
                objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
                objCachTinh.XuLy();
                //Lam moi gird
                gvChiTiet.RefreshData();
                //Tinh thanh tien
                this.TinhThanhTien();
            }
            catch { }
        }

        private void spThanhTien_EditValueChanged(object sender, EventArgs e)
        {
            spTienVAT.EditValue = spTyLeVAT.Value * spThanhTien.Value;
            spTienBVMT.EditValue = spTyLeBVMT.Value * spThanhTien.Value;
            spTienTT.EditValue = spTienVAT.Value + spTienBVMT.Value + spThanhTien.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (glkMatBang.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Mặt bằng], xin cảm ơn.");
                glkMatBang.Focus();
                return;
            }

            if (dateTuNgay.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Từ ngày]. Xin cảm ơn!");
                dateTuNgay.Focus();
                return;
            }

            if (dateDenNgay.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Đến ngày]. Xin cảm ơn!");
                dateDenNgay.Focus();
                return;
            }

            if (SqlMethods.DateDiffDay(dateTuNgay.DateTime, dateDenNgay.DateTime) <= 0)
            {
                DialogBox.Alert("Khoảng thời gian không hợp lý. Vui lòng kiểm tra lại!");
                dateDenNgay.Focus();
                return;
            }

            if (spSoTieuThu.Value <= 0)
            {
                DialogBox.Alert("Nước: [Số tiêu thụ] phải lớn hơn 0.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                spSoTieuThu.Focus();
                return;
            }

            var count = db.dvNuocSinhHoats.Where(p => p.ID != objNuoc.ID & p.MaMB == (int)glkMatBang.EditValue & p.MaKH == (int)txtKhachHang.Tag
                & SqlMethods.DateDiffMonth(dateDenNgay.DateTime, p.DenNgay) == 0).Count();
            if (count > 0)
            {
                if (DialogBox.Question(string.Format("[Mặt bằng] này đã nhập chỉ số [Nước] tháng {0:MM/yyyy} rồi.\r\nBạn có muốn nhập chỉ số cho [Mặt bằng] khác không?", dateDenNgay.DateTime)) == System.Windows.Forms.DialogResult.Yes)
                {
                    goto NhapMoi;
                }
                else
                {
                    return;
                }
            }

            if (dateNgayTT.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Ngày thanh toán]. Xin cảm ơn!");
                dateNgayTT.Focus();
                return;
            }
            #endregion

            if (objNuoc.ID == 0)
            {
                objNuoc.MaTN = this.MaTN;
                objNuoc.NgayNhap = db.GetSystemDate();
                objNuoc.MaNVN = Common.User.MaNV;
                db.dvNuocSinhHoats.InsertOnSubmit(objNuoc);
            }
            else
            {
                objNuoc.MaNVS = Common.User.MaNV;
                objNuoc.NgaySua = db.GetSystemDate();

                //Xoa chi tiet cu
                db.dvNuocSinhHoatChiTiets.DeleteAllOnSubmit(objNuoc.dvNuocSinhHoatChiTiets);
            }

            objNuoc.MaMB = (int?)glkMatBang.EditValue;
            objNuoc.MaKH = (int?)txtKhachHang.Tag;
            objNuoc.TuNgay = dateTuNgay.DateTime;
            objNuoc.DenNgay = dateDenNgay.DateTime;

            objNuoc.ChiSoCu = Convert.ToInt32(spChiSoCu.Value);
            objNuoc.ChiSoMoi = Convert.ToInt32(spChiSoMoi.Value);
            objNuoc.SoTieuThuNL = Convert.ToInt32(spSoTieuThuNL.Value);

            objNuoc.DauCap_Cu = Convert.ToInt32(spDauCap_Cu.Value);
            objNuoc.DauCap_Moi = Convert.ToInt32(spDauCap_Moi.Value);
            objNuoc.DauCap = objNuoc.DauCap_Moi - objNuoc.DauCap_Cu;
            objNuoc.DauHoi_Cu = Convert.ToInt32(spDauHoi_Cu.Value);
            objNuoc.DauHoi_Moi = Convert.ToInt32(spDauHoi_Moi.Value);
            objNuoc.DauHoi = objNuoc.DauHoi_Moi - objNuoc.DauHoi_Cu;
            objNuoc.SoTieuThuNN = Convert.ToInt32(spSoTieuThuNN.Value);

            objNuoc.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
            objNuoc.ThanhTien = spThanhTien.Value;
            objNuoc.TyLeVAT = spTyLeVAT.Value;
            objNuoc.TienVAT = spTienVAT.Value;
            objNuoc.TyLeBVMT = spTyLeBVMT.Value;
            objNuoc.TienBVMT = spTienBVMT.Value;
            objNuoc.TienTT = spTienTT.Value;
            objNuoc.DienGiai = txtDienGiai.Text;
            objNuoc.NgayTT = dateNgayTT.DateTime;
            //if (dateDenNgay.DateTime.Day >= 15) objNuoc.NgayTB = new DateTime(dateDenNgay.DateTime.Year, dateDenNgay.DateTime.Month, 1);
            //else objNuoc.NgayTB = new DateTime(dateTuNgay.DateTime.Year, dateTuNgay.DateTime.Month, 1);
            objNuoc.NgayTB = new DateTime((int)spinNam.Value, (int)spinThang.Value, 1);

            //Them moi chi tiet
            foreach (var ct in objCachTinh.ltChiTiet)
            {
                if (ct.SoLuong > 0)
                {
                    var objCTD = new dvNuocSinhHoatChiTiet();
                    objCTD.MaDM = ct.MaDM;
                    objCTD.SoLuong = ct.SoLuong;
                    objCTD.DonGia = ct.DonGia;
                    objCTD.ThanhTien = ct.ThanhTien;
                    objCTD.DienGiai = ct.DienGiai;
                    objNuoc.dvNuocSinhHoatChiTiets.Add(objCTD);
                }
            }
            
            db.SubmitChanges();

            this.IsSave = true;

            NhapMoi:
            this.ID = null;
            this.LoadRecord();
            if (glkMatBang.Properties.View.FocusedRowHandle < 0)
            {
                glkMatBang.Properties.View.FocusedRowHandle = 0;
            }

            glkMatBang.Properties.View.FocusedRowHandle += 1;
            glkMatBang.EditValue = (int?)glkMatBang.Properties.View.GetFocusedRowCellValue("MaMB");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DonGia")
            {
                var objCT = (ChiTietNuocItem)gvChiTiet.GetRow(e.RowHandle);
                objCT.ThanhTien = objCT.SoLuong * objCT.DonGia;
                this.TinhThanhTien();
            }
        }

        private void spDauCap_Cu_EditValueChanged(object sender, EventArgs e)
        {
            decimal dauCap = 0, dauHoi = 0;
            if (spDauCap_Moi.Value > spDauCap_Cu.Value)
            {
                dauCap = spDauCap_Moi.Value - spDauCap_Cu.Value;
            }

            if (spDauHoi_Moi.Value > spDauHoi_Cu.Value)
            {
                dauHoi = spDauHoi_Moi.Value - spDauHoi_Cu.Value;
            }

            spSoTieuThuNN.EditValue = dauCap - dauHoi > 0 ? dauCap - dauHoi : 0;
        }

        private void spSoTieuThuNL_EditValueChanged(object sender, EventArgs e)
        {
            spSoTieuThu.EditValue = spSoTieuThuNL.Value + spSoTieuThuNN.Value;
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            var _MaMB = (int)glkMatBang.EditValue;
            if (this.ID != null)
            {
                dateNgayTT.EditValue = (from g in db.dvNuocs where g.MaMB == _MaMB select g.NgayTT).Max();
            }
            else
            {
                dateNgayTT.EditValue = dateDenNgay.EditValue;
            }
        }
    }

    public class ChiTietNuocItem
    {
        public int? ID { get; set; }
        public int? MaGas { get; set; }
        public int? MaDM { get; set; }
        public string TenDM { get; set; }
        public int? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public int? DinhMuc { get; set; }
        public string DienGiai { get; set; }
    }
}