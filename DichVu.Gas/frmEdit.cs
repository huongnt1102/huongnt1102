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

namespace DichVu.Gas
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
        public int Thang { get; set; }
        public int Nam { get; set; }
        MasterDataContext db;
        dvGa objGas;
        List<ChiTietGasItem> ltChiTiet;

        void LoadRecord()
        {
            try
            {
                db = new MasterDataContext();
                if (this.ID != null)
                {
                    objGas = db.dvGas.Single(p => p.ID == this.ID);
                    lkDVT.EditValue = objGas.MaDVT;
                    spinTyLe.EditValue = objGas.TyLe;
                    spThang.EditValue = objGas.NgayTB.Value.Month;
                    spNam.EditValue = objGas.NgayTB.Value.Year;
                    if(objGas.MaDH != null)
                    {
                        lkDongHo.EditValue = objGas.MaDH;
                    }
                }
                else
                {
                    objGas = new dvGa();
                    lkDVT.EditValue = (int)1;
                    spinTyLe.EditValue = lkDVT.GetColumnValue("TyGia");
                    spThang.EditValue = Thang;
                    spNam.EditValue = Nam;

                }

                glkMatBang.EditValue = objGas.MaMB;
                dateNgayTT.EditValue = objGas.NgayTT;
                dateTuNgay.EditValue = objGas.TuNgay;
                dateDenNgay.EditValue = objGas.DenNgay;
                spChiSoCu.EditValue = objGas.ChiSoCu;
                spChiSoMoi.EditValue = objGas.ChiSoMoi;
                spSoTieuThu.EditValue = objGas.SoTieuThu;
                spThanhTien.EditValue = objGas.ThanhTien;
                spTyLeVAT.EditValue = objGas.TyLeVAT;
                spTienVAT.EditValue = objGas.TienVAT;
                spTienTT.EditValue = objGas.TienTT;
                txtDienGiai.EditValue = objGas.DienGiai;
               
                spinTieuThuQD.EditValue = objGas.SoTieuThuQD;
                foreach (var ct in ltChiTiet)
                {
                    var objCT = objGas.dvGasChiTiets.FirstOrDefault(p => p.MaDM == ct.MaDM);
                    if (objCT != null)
                    {
                        ct.ID = objCT.ID;
                        ct.SoLuong = objCT.SoLuong;
                        ct.DonGia = objCT.DonGia;
                        ct.ThanhTien = objCT.ThanhTien;
                        ct.DienGiai = objCT.DienGiai;
                    }
                    else
                    {
                        ct.ID = null;
                        ct.SoLuong = 0;
                        ct.DonGia = 0;
                        ct.ThanhTien = 0;
                        ct.DienGiai = null;
                    }


                }
            }
            catch { }
        }

        bool GetIsCanHo()
        {
            var r = glkMatBang.Properties.GetRowByKeyValue(glkMatBang.EditValue);
            if (r != null)
            {
                var type = r.GetType();
                return (type.GetProperty("IsCanHoCaNhan").GetValue(r, null) as bool?) ?? true;
            }
            return true;
        }

        void TinhThanhTien()
        {
            try
            {
                spThanhTien.EditValue = ltChiTiet.Sum(p => p.ThanhTien);
            }
            catch { }
        }

        void TinhTien()
        {
            try
            {
                var _SoTieuThu = spinTieuThuQD.Value;
                var _IsCanHo = this.GetIsCanHo();

                for (var i = 0; i < ltChiTiet.Count; i++)
                {
                    var objCT = ltChiTiet[i];

                    if (_SoTieuThu > objCT.DinhMuc && i < ltChiTiet.Count - 1)
                    {
                        objCT.SoLuong = objCT.DinhMuc;
                        _SoTieuThu -= objCT.DinhMuc.Value;
                    }
                    else
                    {
                        objCT.SoLuong = _SoTieuThu;
                        _SoTieuThu = 0;
                    }

                    objCT.DonGia = objCT.DonGia;
                    objCT.ThanhTien = objCT.DonGia * objCT.SoLuong;
                    objCT.DienGiai = objCT.DienGiai;
                }
                gvChiTiet.RefreshData();

                //Tính thành tiền
                this.TinhThanhTien();
            }
            catch { }
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            db = new MasterDataContext();
            lkDVT.Properties.DataSource = (from dvt in db.dvgDonViTinhs
                                           select new
                                               {
                                                   dvt.ID,
                                                   dvt.KHDVT,
                                                   dvt.TyGia,
                                                   dvt.TenDVT
                                               });
            lkDVT.EditValue = (int)1;
            

            glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                where mb.MaTN == this.MaTN
                                                orderby mb.MaSoMB
                                                select new
                                                {
                                                    mb.MaMB,
                                                    mb.MaLMB,
                                                    mb.MaSoMB,
                                                    mb.IsCanHoCaNhan,
                                                    tl.TenTL,
                                                    kn.TenKN,
                                                    kh.MaKH,
                                                    kh.KyHieu,
                                                    TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen
                                                }).ToList();

            lkDongHo.Properties.DataSource = db.dvGasDongHos;

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
                    var _MaKH = (int)type.GetProperty("MaKH").GetValue(r, null);
                    var _MaLMB = (int)type.GetProperty("MaLMB").GetValue(r, null);
                    ltChiTiet = (from dm in db.dvGasDinhMucs
                                 where dm.MaTN == this.MaTN & dm.MaMB == _MaMB & dm.MaKH == _MaKH
                                 orderby dm.STT
                                 select new ChiTietGasItem()
                                 {
                                     MaDM = dm.ID,
                                     TenDM = dm.TenDM,
                                     DinhMuc = dm.DinhMuc,
                                     DonGia = dm.DonGia,
                                     DienGiai = dm.DienGiai
                                 }).ToList();
                    if (ltChiTiet.Count == 0)
                        ltChiTiet = (from dm in db.dvGasDinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == _MaLMB
                                     orderby dm.STT
                                     select new ChiTietGasItem()
                                     {
                                         MaDM = dm.ID,
                                         TenDM = dm.TenDM,
                                         DinhMuc = dm.DinhMuc,
                                         DonGia = dm.DonGia,
                                         DienGiai = dm.DienGiai
                                     }).ToList();
                    if (ltChiTiet.Count == 0)
                        ltChiTiet = (from dm in db.dvGasDinhMucs
                                     where dm.MaTN == this.MaTN & dm.MaLMB == null & dm.MaKH == null & dm.MaMB == null
                                     orderby dm.STT
                                     select new ChiTietGasItem()
                                     {
                                         MaDM = dm.ID,
                                         TenDM = dm.TenDM,
                                         DinhMuc = dm.DinhMuc,
                                         DonGia = dm.DonGia,
                                         DienGiai = dm.DienGiai
                                     }).ToList();

                    gcChiTiet.DataSource = ltChiTiet;



                    txtKhachHang.EditValue = type.GetProperty("TenKH").GetValue(r, null);
                    txtKhachHang.Tag = _MaKH;

                    var _TuNgay = (from g in db.dvGas where g.MaMB == _MaMB & g.MaKH == _MaKH select g.DenNgay).Max();
                    dateTuNgay.EditValue = _TuNgay ?? db.GetSystemDate();

                    var _ChiSoCu = (from g in db.dvGas where g.MaMB == _MaMB & g.MaKH == _MaKH select g.ChiSoMoi).Max();
                    spChiSoCu.EditValue = _ChiSoCu ?? 0;

                    //Load gia vào chi tiet va tinh thanh tien
                    this.TinhTien();
                }
            }
            catch { }
        }

        private void spChiSoCu_EditValueChanged(object sender, EventArgs e)
        {
            if (spChiSoMoi.Value > spChiSoCu.Value)
            {
                spSoTieuThu.EditValue = spChiSoMoi.Value - spChiSoCu.Value;
            }
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateDenNgay.EditValue = dateTuNgay.DateTime.AddMonths(1);
            }
            catch { }
        }

        private void spSoTieuThu_EditValueChanged(object sender, EventArgs e)
        {
            //Load gia vào chi tiet va tinh thanh tien
            spinTieuThuQD.EditValue = spinTyLe.Value * spSoTieuThu.Value;
            TinhTien();

            
        }

        private void spThanhTien_EditValueChanged(object sender, EventArgs e)
        {
            spTienVAT.EditValue = Math.Round((decimal)(spTyLeVAT.Value * spThanhTien.Value),0,MidpointRounding.AwayFromZero);
            spTienTT.EditValue = Math.Round((decimal)(spTienVAT.Value + spThanhTien.Value),0,MidpointRounding.AwayFromZero);
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
            if (lkDVT.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [đơn vị tính]. Xin cảm ơn!");
                lkDVT.Focus();
                return;
            }
            if (spinTyLe.Value == 0 || spinTyLe.EditValue==null)
            {
                DialogBox.Alert("Vui lòng nhập [Tỷ lệ]. Xin cảm ơn!");
                lkDVT.Focus();
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

            if(lkDongHo.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn đồng hồ gas.");
                lkDongHo.Focus();
                return;
            }

            //if (SqlMethods.DateDiffDay(dateNgayTT.DateTime, dateNgayTT.DateTime) < 0)
            //{
            //    DialogBox.Alert("Khoảng thời gian không hợp lý. Vui lòng kiểm tra lại!");
            //    dateNgayTT.Focus();
            //    return;
            //}

            if (SqlMethods.DateDiffDay(dateTuNgay.DateTime, dateDenNgay.DateTime) < 0)
            {
                DialogBox.Alert("Khoảng thời gian không hợp lý. Vui lòng kiểm tra lại!");
                dateDenNgay.Focus();
                return;
            }

            if (spChiSoCu.Value >= spChiSoMoi.Value)
            {
                DialogBox.Alert("Gas: [Chỉ số mới] phải lớn hơn [Chỉ số cũ].\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                spChiSoCu.Focus();
                return;
            }

            var count = db.dvGas.Where(p => p.ID != objGas.ID & p.MaMB == (int)glkMatBang.EditValue & p.MaKH == (int)txtKhachHang.Tag
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

            if (objGas.ID == 0)
            {
                objGas.MaTN = this.MaTN;
                objGas.NgayNhap = db.GetSystemDate();
                objGas.MaNVN = Common.User.MaNV;
                db.dvGas.InsertOnSubmit(objGas);
            }
            else
            {
                objGas.MaNVS = Common.User.MaNV;
                objGas.NgaySua = db.GetSystemDate();
            }
            objGas.NgayTT = dateNgayTT.DateTime;
            objGas.MaMB = (int?)glkMatBang.EditValue;
            objGas.MaKH = (int?)txtKhachHang.Tag;
            objGas.TuNgay = dateTuNgay.DateTime;
            objGas.DenNgay = dateDenNgay.DateTime;
            objGas.ChiSoCu = Convert.ToInt32(spChiSoCu.Value);
            objGas.ChiSoMoi = Convert.ToInt32(spChiSoMoi.Value);
            objGas.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
            objGas.ThanhTien = spThanhTien.Value;
            objGas.TyLeVAT = spTyLeVAT.Value;
            objGas.TyLe = spinTyLe.Value;
            objGas.SoTieuThuQD = spinTieuThuQD.Value;
            objGas.MaDVT = (int?)lkDVT.EditValue;
            objGas.TienVAT = spTienVAT.Value;
            objGas.TienTT = spTienTT.Value;
            objGas.DienGiai = txtDienGiai.Text;
            objGas.MaDH = (int?)lkDongHo.EditValue;
            //if (dateDenNgay.DateTime.Day >= 15) objGas.NgayTB = new DateTime(dateDenNgay.DateTime.Year, dateDenNgay.DateTime.Month, 1);
            //else objGas.NgayTB = new DateTime(dateTuNgay.DateTime.Year, dateTuNgay.DateTime.Month, 1);
            objGas.NgayTB = new DateTime((int)spNam.Value, (int)spThang.Value, 1);

            foreach (var ct in ltChiTiet)
            {
                if (ct.SoLuong > 0)
                {
                    dvGasChiTiet objCT;
                    if (ct.ID != null)
                    {
                        objCT = db.dvGasChiTiets.Single(p => p.ID == ct.ID);
                    }
                    else
                    {
                        objCT = new dvGasChiTiet();
                        objGas.dvGasChiTiets.Add(objCT);
                    }

                    objCT.MaDM = ct.MaDM;
                    objCT.SoLuong = ct.SoLuong;
                    objCT.DonGia = ct.DonGia;
                    objCT.ThanhTien = ct.ThanhTien;
                    objCT.DienGiai = ct.DienGiai;
                }
                else
                {
                    if (ct.ID != null)
                    {
                        var objCT = db.dvGasChiTiets.Single(p => p.ID == ct.ID);
                        db.dvGasChiTiets.DeleteOnSubmit(objCT);
                    }
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
                var objCT = (ChiTietGasItem)gvChiTiet.GetRow(e.RowHandle);
                objCT.ThanhTien = objCT.SoLuong * objCT.DonGia;
                this.TinhThanhTien();
            }
        }

        private void spinTyLe_EditValueChanged(object sender, EventArgs e)
        {
            spinTieuThuQD.EditValue = spinTyLe.Value * spSoTieuThu.Value;
            this.TinhTien();
        }

        private void spinTieuThuQD_EditValueChanged(object sender, EventArgs e)
        {
            this.TinhTien();
        }

        private void lkDVT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                spinTyLe.Value = (decimal)lkDVT.GetColumnValue("TyGia");
                spinTieuThuQD.EditValue = spinTyLe.Value * spSoTieuThu.Value;
                this.TinhTien();

            }
            catch(Exception ex)
            {
                //DialogBox.Error(ex.ToString());
            }
            
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {

            if (this.ID != null)
            {
                var _MaMB = (int)glkMatBang.EditValue;
                dateNgayTT.EditValue = (from g in db.dvGas where g.MaMB == _MaMB select g.NgayTT).Max();
            }
            else
            {
                dateNgayTT.EditValue = dateDenNgay.EditValue;
            }
        }
       
    }

    public class ChiTietGasItem
    {
        public int? ID { get; set; }
        public int? MaGas { get; set; }
        public int? MaDM { get; set; }
        public string TenDM { get; set; }
        public decimal? SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal? ThanhTien { get; set; }
        public decimal? DinhMuc { get; set; }
        public string DienGiai { get; set; }
    }
}