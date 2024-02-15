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

namespace DichVu.Nuoc
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
        dvNuoc objNuoc;
        CachTinhCls objCachTinh;

        void LoadRecord()
        {
            try
            {
                db = new MasterDataContext();
                if (this.ID != null)
                {
                    objNuoc = db.dvNuocs.Single(p => p.ID == this.ID);
                    glkMatBang.EditValue = objNuoc.MaMB;
                    spSoNguoiUD.EditValue = objNuoc.SoNguoiUD;
                    spSoM3UD1Nguoi.EditValue = objNuoc.SoM3UD1Nguoi;
                    spSoM3UD.EditValue = objNuoc.SoM3UD;
                    lkDongHo.EditValue = objNuoc.MaDH;
                    spHeSo.EditValue = objNuoc.HeSo;
                    dateTuNgay.EditValue = objNuoc.TuNgay;
                    dateDenNgay.EditValue = objNuoc.DenNgay;
                    spChiSoCu.EditValue = objNuoc.ChiSoCu;
                    spChiSoMoi.EditValue = objNuoc.ChiSoMoi;
                    spHeSo.EditValue = objNuoc.HeSo ?? 1;
                    spSoTieuThu.EditValue = objNuoc.SoTieuThu;
                    spTieuthuDHCu.EditValue = objNuoc.SoTieuThuDHCu.GetValueOrDefault();
                    spThanhTien.EditValue = objNuoc.ThanhTien;
                    spTyLeVAT.EditValue = objNuoc.TyLeVAT;
                    spTienVAT.EditValue = objNuoc.TienVAT;
                    spTyLeBVMT.EditValue = objNuoc.TyLeBVMT;
                    spTienBVMT.EditValue = objNuoc.TienBVMT;
                    spTienTT.EditValue = objNuoc.TienTT;
                    txtDienGiai.EditValue = objNuoc.DienGiai;
                    dateEditNgayThanhToan.EditValue = objNuoc.NgayTT;
                    spinThang.EditValue = objNuoc.NgayTB.Value.Month;
                    spinNam.EditValue = objNuoc.NgayTB.Value.Year;
                }
                else
                {
                    objNuoc = new dvNuoc();
                    spinThang.EditValue = Thang;
                    spinNam.EditValue = Nam;
                }                
            }
            catch { }
        }

        void TinhThanhTien()
        {
            spThanhTien.EditValue = objCachTinh.GetThanhTien();
        }

        void TinhSoUuDai()
        {
            try
            {
                spSoM3UD.EditValue = spSoNguoiUD.Value * spSoM3UD1Nguoi.Value;
                objCachTinh.SoUuDai = Convert.ToInt32(spSoM3UD.Value);
                this.TinhTienChiTiet();
            }
            catch { }
        }

        void TinhTienChiTiet()
        {
            try
            {
                //Tinh chi tiet
                objCachTinh.XuLy();
                //Lam moi gird
                gvChiTiet.RefreshData();
                //Tinh thanh tien
                this.TinhThanhTien();
            }
            catch { }
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
                                                    ud.SoNguoi,
                                                    ud.SoLuong,
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

                    //Load uu dai
                    spSoNguoiUD.EditValue = (int?)type.GetProperty("SoNguoi").GetValue(r, null) ?? 0;
                    spSoM3UD1Nguoi.EditValue = (int?)type.GetProperty("SoLuong").GetValue(r, null) ?? 0;
                    spSoM3UD.EditValue = (int?)type.GetProperty("MucUuDai").GetValue(r, null) ?? 0;

                    //Nap dong ho
                    lkDongHo.Properties.DataSource = (from dh in db.dvNuocDongHos where dh.MaMB == _MaMB select new { dh.ID, dh.SoDH, dh.HeSo }).ToList();
                    lkDongHo.EditValue = null;

                    //Load dinh muc
                    objCachTinh.MaMB = _MaMB;
                    objCachTinh.LoadDinhMuc();
                    gcChiTiet.DataSource = objCachTinh.ltChiTiet;

                    //Set khach hang
                    var _MaKH = (int)type.GetProperty("MaKH").GetValue(r, null);
                    txtKhachHang.EditValue = type.GetProperty("TenKH").GetValue(r, null);
                    txtKhachHang.Tag = _MaKH;

                    //Set tu ngay
                    var _TuNgay = (from g in db.dvNuocs where g.MaMB == _MaMB & g.MaKH == _MaKH select g.DenNgay).Max();
                    if (_TuNgay != null) _TuNgay = _TuNgay.Value.AddDays(1);
                    dateTuNgay.EditValue = _TuNgay ?? db.GetSystemDate().AddMonths(-1);

                    //Set chi so
                    var _ChiSoCu = (from g in db.dvNuocs where g.MaMB == _MaMB & g.MaKH == _MaKH select g.ChiSoMoi).Max();
                    spChiSoCu.EditValue = _ChiSoCu ?? 0;
                }
            }
            catch { }
        }

        private void spChiSoCu_EditValueChanged(object sender, EventArgs e)
        {
            if (spChiSoMoi.Value > spChiSoCu.Value)
            {
                spSoTieuThu.EditValue = (spChiSoMoi.Value - spChiSoCu.Value) * spHeSo.Value;
            }
            else
            {
                spSoTieuThu.EditValue = 0;
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
                if (Convert.ToInt32(spTieuthuDHCu.EditValue) != 0)
                {
                    objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value) + Convert.ToInt32(spTieuthuDHCu.Value);
                }
                else
                {
                    objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
                }
               
                //Tinh chi tiet
                this.TinhTienChiTiet();
            }
            catch { }
        }

        private void spThanhTien_EditValueChanged(object sender, EventArgs e)
        {
           
                spTienVAT.EditValue = Math.Round((decimal)(spTyLeVAT.Value * spThanhTien.Value), 0, MidpointRounding.AwayFromZero);
                spTienBVMT.EditValue = Math.Round((decimal)((spTyLeBVMT.Value*spThanhTien.Value)), 0, MidpointRounding.AwayFromZero); //spTyLeBVMT.Value * spThanhTien.Value;
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

            if (dateEditNgayThanhToan.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Ngày thanh toán]. Xin cảm ơn!");
                dateEditNgayThanhToan.Focus();
                return;
            }

            if (SqlMethods.DateDiffDay(dateTuNgay.DateTime, dateDenNgay.DateTime) <= 0)
            {
                DialogBox.Alert("Khoảng thời gian không hợp lý. Vui lòng kiểm tra lại!");
                dateDenNgay.Focus();
                return;
            }

            //if (spChiSoCu.Value >= spChiSoMoi.Value)
            //{
            //    DialogBox.Alert("Nước: [Chỉ số mới] phải lớn hơn [Chỉ số cũ].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
            //    spChiSoCu.Focus();
            //    return;
            //}

            var idDongHo = Convert.ToInt32( lkDongHo.EditValue);

            var count = db.dvNuocs.Where(p => p.ID != objNuoc.ID & p.MaMB == (int)glkMatBang.EditValue & p.MaKH == (int)txtKhachHang.Tag
                & SqlMethods.DateDiffMonth(dateDenNgay.DateTime, p.DenNgay) == 0).Count();
            if (count > 0)
            {
                if(idDongHo != 0)
                {
                    count = db.dvNuocs.Where(p => p.ID != objNuoc.ID & p.MaMB == (int)glkMatBang.EditValue & p.MaKH == (int)txtKhachHang.Tag & p.MaDH == idDongHo
                & SqlMethods.DateDiffMonth(dateDenNgay.DateTime, p.DenNgay) == 0).Count();
                    if(count > 0)
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
                }
                
            }
            #endregion

            if (objNuoc.ID == 0)
            {
                objNuoc.MaTN = this.MaTN;
                objNuoc.NgayNhap = db.GetSystemDate();
                objNuoc.MaNVN = Common.User.MaNV;
                db.dvNuocs.InsertOnSubmit(objNuoc);
            }
            else
            {
                objNuoc.MaNVS = Common.User.MaNV;
                objNuoc.NgaySua = db.GetSystemDate();

                //Xoa chi tiet cu
                db.dvNuocChiTiets.DeleteAllOnSubmit(objNuoc.dvNuocChiTiets);
            }

            objNuoc.MaMB = (int?)glkMatBang.EditValue;
            objNuoc.MaKH = (int?)txtKhachHang.Tag;
            objNuoc.SoNguoiUD = Convert.ToInt32(spSoNguoiUD.Value);
            objNuoc.SoM3UD1Nguoi = Convert.ToInt32(spSoM3UD1Nguoi.Value);
            objNuoc.SoM3UD = Convert.ToInt32(spSoM3UD.Value);
            objNuoc.TuNgay = dateTuNgay.DateTime;
            objNuoc.DenNgay = dateDenNgay.DateTime;
            objNuoc.ChiSoCu = Convert.ToInt32(spChiSoCu.Value);
            objNuoc.ChiSoMoi = Convert.ToInt32(spChiSoMoi.Value);
            objNuoc.HeSo = spHeSo.Value <= 0 ? 1 : Convert.ToInt32(spHeSo.Value);
            objNuoc.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
            objNuoc.SoTieuThuDHCu = Convert.ToInt32(spTieuthuDHCu.Value);
            objNuoc.ThanhTien = spThanhTien.Value;
            objNuoc.TyLeVAT = spTyLeVAT.Value;
            objNuoc.TienVAT = spTienVAT.Value;
            objNuoc.TyLeBVMT = spTyLeBVMT.Value;
            objNuoc.TienBVMT = spTienBVMT.Value;
            objNuoc.TienTT = spTienTT.Value;
            objNuoc.DienGiai = txtDienGiai.Text;
            objNuoc.NgayTT = dateEditNgayThanhToan.DateTime;
            objNuoc.MaDH = (int?)lkDongHo.EditValue;
            //if (dateDenNgay.DateTime.Day >= 15) objNuoc.NgayTB = new DateTime(dateDenNgay.DateTime.Year, dateDenNgay.DateTime.Month, 1);
            //else objNuoc.NgayTB = new DateTime(dateTuNgay.DateTime.Year, dateTuNgay.DateTime.Month, 1);
            objNuoc.NgayTB = new DateTime((int)spinNam.Value, (int)spinThang.Value, 1);
                
                
            //Them moi chi tiet
            foreach (var ct in objCachTinh.ltChiTiet)
            {
                if (ct.SoLuong > 0)
                {
                    var objCTD = new dvNuocChiTiet();
                    objCTD.MaDM = ct.MaDM;
                    objCTD.SoLuong = ct.SoLuong;
                    objCTD.DonGia = ct.DonGia;
                    objCTD.ThanhTien = ct.ThanhTien;
                    objCTD.DienGiai = ct.DienGiai;
                    objNuoc.dvNuocChiTiets.Add(objCTD);
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

        private void lkDongHo_EditValueChanged(object sender, EventArgs e)
        {
            spHeSo.EditValue = lkDongHo.GetColumnValue("HeSo") ?? 1;
        }

        private void spSoNguoiUD_EditValueChanged(object sender, EventArgs e)
        {
            this.TinhSoUuDai();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            dateEditNgayThanhToan.EditValue = dateDenNgay.EditValue;
        }

        private void spTieuthuDHCu_EditValueChanged(object sender, EventArgs e)
        {
            //TinhThanhTien();
            if (Convert.ToInt32(spTieuthuDHCu.EditValue) != 0)
            {
                objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value) + Convert.ToInt32(spTieuthuDHCu.Value);
            }
            else
            {
                objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
            }

            //Tinh chi tiet
            this.TinhTienChiTiet();
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