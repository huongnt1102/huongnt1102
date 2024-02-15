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

namespace DichVu.DienLanh
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
        //public int MaLD { get; set; }
        MasterDataContext db;
        dvDienLanh objDien;
        CachTinhCls objCachTinh;
        

        void LoadRecord()
        {
            try
            {
                db = new MasterDataContext();
                if (this.ID != null)
                {
                    objDien = db.dvDienLanhs.Single(p => p.ID == this.ID);
                    lkLoaiDien.EditValue = objDien.MaLoaiDien;
                    //this.MaLD = (int)objDien.MaLoaiDien;
                    glkMatBang.EditValue = objDien.MaMB;
                    lkDongHo.EditValue = objDien.MaDH;
                    spHeSo.EditValue = objDien.HeSo ?? 1;
                    dateTuNgay.EditValue = objDien.TuNgay;
                    dateDenNgay.EditValue = objDien.DenNgay;
                    spChiSoCu.EditValue = objDien.ChiSoCu;
                    spChiSoMoi.EditValue = objDien.ChiSoMoi;
                    spSoTieuThu.EditValue = objDien.SoTieuThu;
                    spThanhTien.EditValue = objDien.ThanhTien;
                    spTyLeVAT.EditValue = objDien.TyLeVAT;
                    spTyLeHaoHut.EditValue = objDien.TyLeHaoHut;
                    spTienHH.EditValue = objDien.TienHaoHut;
                    spTienVAT.EditValue = objDien.TienVAT;
                    spTienTT.EditValue = objDien.TienTT;
                    txtDienGiai.EditValue = objDien.DienGiai;
                    dateNgayTT.EditValue = objDien.NgayTT;
                    spDHCu.EditValue = objDien.SoTieuThuDHCu.GetValueOrDefault();
                    itemThang.EditValue = objDien.NgayTB.Value.Month;
                    itemNam.EditValue = objDien.NgayTB.Value.Year;
                    spSoTieuThu.EditValue = objDien.SoTieuThu;

                    //gcChiTiet.DataSource = objDien.dvDienLanhChiTiets;

                }
                else
                {
                    objDien = new dvDienLanh();
                    itemThang.EditValue = Thang;
                    itemNam.EditValue = Nam;
                }
            }
            catch { }
        }

        void TinhThanhTien()
        {
            try
            {
                spThanhTien.EditValue = objCachTinh.GetThanhTien();
            }
            catch { }
        }

        void GetChiSoVaThoiGian()
        {
            try
            {
                var _MaMB = (int)glkMatBang.EditValue;
                var r = glkMatBang.Properties.GetRowByKeyValue(glkMatBang.EditValue);
                var type = r.GetType();
                var _MaKH = (int)type.GetProperty("MaKH").GetValue(r, null);
                var _MaDH = (int?)lkDongHo.EditValue;

                //Set tu ngay
                var _TuNgay = (from g in db.dvDienLanhs where g.MaMB == _MaMB & g.MaKH == _MaKH & g.MaDH == _MaDH select g.DenNgay).Max();
                if (_TuNgay != null) _TuNgay = _TuNgay.Value.AddDays(1);
                dateTuNgay.EditValue = _TuNgay ?? db.GetSystemDate().AddMonths(-1);

                //Set chi so
                var _ChiSoCu = (from g in db.dvDienLanhs where g.MaMB == _MaMB & g.MaKH == _MaKH & g.MaDH == _MaDH select g.ChiSoMoi).Max();
                spChiSoCu.EditValue = _ChiSoCu ?? 0;
                spChiSoMoi.EditValue = 0;
            }
            catch { }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            objCachTinh = new CachTinhCls();
            objCachTinh.MaTN = this.MaTN.Value;
            //objCachTinh.MaLD = this.MaLD.Value;
            db = new MasterDataContext();
            glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                where mb.MaTN == this.MaTN
                                                orderby mb.MaSoMB
                                                select new
                                                {
                                                    mb.MaMB,
                                                    mb.MaSoMB,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    kh.MaKH,
                                                    kh.KyHieu,
                                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                }).ToList();
            lkLoaiDien.Properties.DataSource = db.dvDienLoaiDiens.Select(p=> new { p.ID, p.TenLoaiDien}).ToList();
            lkLoaiDien.EditValue = db.dvDienLoaiDiens.FirstOrDefault().ID;
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
                    var _MaLD = (int)lkLoaiDien.EditValue;
                    //Nap dong ho
                    lkDongHo.Properties.DataSource = (from dh in db.dvDienLanh_DongHos where dh.MaMB == _MaMB select new { dh.ID, dh.SoDH, dh.HeSo }).ToList();
                    lkDongHo.EditValue = null;

                    //Load dinh muc
                    objCachTinh.MaMB = _MaMB;
                    objCachTinh.MaLD = _MaLD;
                    //Set khach hang
                    var _MaKH = (int)type.GetProperty("MaKH").GetValue(r, null);
                    txtKhachHang.Tag = _MaKH;
                    txtKhachHang.EditValue = type.GetProperty("TenKH").GetValue(r, null);
                   
                    objCachTinh.LoadDinhMuc();
                    //Nap vao gird
                    gcChiTiet.DataSource = objCachTinh.ltChiTiet;

                    //Set chi so va thoi gian
                    this.GetChiSoVaThoiGian();


                }
            }
            catch { }
        }

        private void spChiSoCu_EditValueChanged(object sender, EventArgs e)
        {
            if (spChiSoMoi.Value > spChiSoCu.Value)
            {
                spSoTieuThu.EditValue = (spChiSoMoi.Value - spChiSoCu.Value) * (spHeSo.Value <= 0 ? 1 : spHeSo.Value);
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
                //if ((decimal) spDHCu.EditValue == 0)
                //{
                //    objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
                //}
                //else
                //{
                //    objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value) + Convert.ToInt32(spDHCu.Value);
                //}

                objCachTinh.SoTieuThu = spSoTieuThu.Value;
                
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
            spTienVAT.EditValue = Math.Round((decimal)(spTyLeVAT.Value * spThanhTien.Value),0,MidpointRounding.AwayFromZero);
            //spTienHH.EditValue = Math.Round((decimal)(spTyLeHaoHut.Value * spThanhTien.Value), 0, MidpointRounding.AwayFromZero);
            spTienTT.EditValue = spTienVAT.Value + spThanhTien.Value ;
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

            if (dateNgayTT.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Ngày TT]. Xin cảm ơn!");
                dateNgayTT.Focus();
                return;
            }

            if (SqlMethods.DateDiffDay(dateTuNgay.DateTime, dateDenNgay.DateTime) < 0)
            {
                DialogBox.Alert("Khoảng thời gian không hợp lý. Vui lòng kiểm tra lại!");
                dateDenNgay.Focus();
                return;
            }

            var count = db.dvDienLanhs.Where(p => p.ID != objDien.ID & p.MaMB == (int)glkMatBang.EditValue & p.MaKH == (int)txtKhachHang.Tag &p.MaDH == (int?)lkDongHo.EditValue
                & SqlMethods.DateDiffMonth(dateDenNgay.DateTime, p.DenNgay) == 0).Count();
            if (count > 0)
            {
                if (DialogBox.Question(string.Format("[Mặt bằng] này đã nhập chỉ số tháng {0:MM/yyyy} rồi.\r\nBạn có muốn nhập chỉ số cho [Mặt bằng] khác không?", dateDenNgay.DateTime)) == System.Windows.Forms.DialogResult.Yes)
                {
                    goto NhapMoi;
                }
                else
                {
                    return;
                }
            }
            #endregion

            if (objDien.ID == 0)
            {
                objDien.MaTN = this.MaTN;
                objDien.NgayNhap = db.GetSystemDate();
                objDien.MaNVN = Common.User.MaNV;
                db.dvDienLanhs.InsertOnSubmit(objDien);
            }
            else
            {
                objDien.MaNVS = Common.User.MaNV;
                objDien.NgaySua = db.GetSystemDate();

                //Xoa chi tiet cu
                db.dvDienLanhChiTiets.DeleteAllOnSubmit(objDien.dvDienLanhChiTiets);
            }

            objDien.MaMB = (int?)glkMatBang.EditValue;
            objDien.MaKH = (int?)txtKhachHang.Tag;
            objDien.MaDH = (int?)lkDongHo.EditValue;
            objDien.SoTieuThuDHCu = (decimal?)spDHCu.EditValue;
            objDien.TuNgay = dateTuNgay.DateTime;
            objDien.DenNgay = dateDenNgay.DateTime;
            objDien.NgayTT = dateNgayTT.DateTime;
            objDien.ChiSoCu = Convert.ToInt32(spChiSoCu.Value);
            objDien.ChiSoMoi = Convert.ToInt32(spChiSoMoi.Value);
            objDien.HeSo = Convert.ToInt32(spHeSo.Value);
            objDien.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
            objDien.ThanhTien = spThanhTien.Value;
            objDien.TyLeVAT = spTyLeVAT.Value;
            objDien.TienVAT = spTienVAT.Value;
            objDien.TienTT = spTienTT.Value;
            objDien.DienGiai = txtDienGiai.Text;
            objDien.NgayTB = new DateTime((int)itemNam.Value, (int)itemThang.Value, 1);
            //objDien.TyLeHaoHut = (decimal)spTyLeHaoHut.EditValue;
            //objDien.TienHaoHut = (decimal)spTienHH.EditValue;
            objDien.MaLoaiDien = (int)lkLoaiDien.EditValue;
            //Them moi chi tiet
            foreach (var ct in objCachTinh.ltChiTiet)
            {
                if (ct.SoLuong > 0)
                {
                    var objCTD = new dvDienLanhChiTiet();
                    objCTD.MaDM = ct.MaDM;
                    objCTD.SoLuong = ct.SoLuong;
                    objCTD.DonGia = ct.DonGia;
                    objCTD.ThanhTien = ct.ThanhTien;
                    objCTD.DienGiai = ct.DienGiai;
                    objDien.dvDienLanhChiTiets.Add(objCTD);
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
                var objCT = (ChiTietDienItem)gvChiTiet.GetRow(e.RowHandle);
                objCT.ThanhTien = objCT.SoLuong * objCT.DonGia;
                //Tinh thanh tien
                this.TinhThanhTien();
            }
        }

        private void lkDongHo_EditValueChanged(object sender, EventArgs e)
        {
            //Set he so
            spHeSo.EditValue = lkDongHo.GetColumnValue("HeSo") ?? 1;

            //Set chi so va thoi gian
            this.GetChiSoVaThoiGian();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            dateNgayTT.EditValue = dateDenNgay.EditValue;
        }

        private void spDHCu_EditValueChanged(object sender, EventArgs e)
        {
            //if ((decimal)spDHCu.EditValue == 0)
            //{
            //    objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
            //}
            //else
            //{
            //    objCachTinh.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value) + Convert.ToInt32(spDHCu.Value);
            //}

            ////objCachTinh.XuLy();
            ////Lam moi gird
            //gvChiTiet.RefreshData();
            ////Tinh thanh tien
            //this.TinhThanhTien();
        }

        private void spTyLeHaoHut_EditValueChanged(object sender, EventArgs e)
        {
            //spTienVAT.EditValue = Math.Round((decimal)(spTyLeVAT.Value * spThanhTien.Value), 0, MidpointRounding.AwayFromZero);
            //spTienHH.EditValue = Math.Round((decimal)(spTyLeHaoHut.Value * spThanhTien.Value), 0, MidpointRounding.AwayFromZero);
            //spTienTT.EditValue = spTienVAT.Value + spThanhTien.Value + spTienHH.Value;
        }
    }

    public class ChiTietDienItem
    {
        public int? ID { get; set; }
        public int? MaGas { get; set; }
        public int? MaDM { get; set; }
        public string TenDM { get; set; }
        public decimal? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public int? DinhMuc { get; set; }      
        public string DienGiai { get; set; }
    }
}