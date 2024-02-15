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

namespace DichVu.Dien.Dien3Pha
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
        dvDien3Pha objDien;

        void LoadRecord()
        {
            try
            {
                db = new MasterDataContext();
                if (this.ID != null)
                {
                    objDien = db.dvDien3Phas.Single(p => p.ID == this.ID);
                    itemThang.EditValue = objDien.NgayTB.Value.Month;
                    itemNam.EditValue = objDien.NgayTB.Value.Year;
                }
                else
                {
                    objDien = new dvDien3Pha();
                    itemThang.EditValue = Thang;
                    itemNam.EditValue = Nam;
                }

                glkMatBang.EditValue = objDien.MaMB;
                glkDongHo.EditValue = objDien.MaDH;
                dateNgayTT.EditValue = objDien.NgayTT;
                dateTuNgay.EditValue = objDien.TuNgay;
                dateDenNgay.EditValue = objDien.DenNgay;
                spSoTieuThu.EditValue = objDien.SoTieuThu;
                spThanhTien.EditValue = objDien.ThanhTien;
                spTyLeVAT.EditValue = objDien.TyLeVAT;
                spTienVAT.EditValue = objDien.TienVAT;
                spTienTT.EditValue = objDien.TienTT;
                txtDienGiai.EditValue = objDien.DienGiai;
                spHeSo.EditValue = objDien.HeSo;

                gcChiTiet.DataSource = objDien.dvDien3PhaChiTiets;
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

        int? GetMaKhachHang()
        {
            var r = glkMatBang.Properties.GetRowByKeyValue(glkMatBang.EditValue);
            if (r != null)
            {
                var type = r.GetType();
                return (type.GetProperty("MaKH").GetValue(r, null) as int?);
            }
            return null;
        }

        void TinhThanhTien()
        {
            spThanhTien.EditValue = objDien.dvDien3PhaChiTiets.Sum(p => p.ThanhTien);
        }
        
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            gvChiTiet.InvalidRowException += Common.InvalidRowException;

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
                                                    mb.MaLMB,
                                                    mb.MaSoMB,
                                                    mb.IsCanHoCaNhan,
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
                    var _MaKH = (int)type.GetProperty("MaKH").GetValue(r, null);
                    var _MaLMB = (int?)type.GetProperty("MaLMB").GetValue(r, null);

                    glkDongHo.Properties.DataSource = (from dh in db.dvDien3PhaDongHos where dh.MaMB == _MaMB select new { dh.ID, dh.SoDH, dh.HeSo }).ToList();
                    glkDongHo.EditValue = null;

                    var list = (from dm in db.dvDien3PhaDinhMucs where dm.MaTN == this.MaTN & dm.MaKH==_MaKH & dm.MaMB==_MaMB select new { dm.ID, dm.TenDM, dm.DonGia }).ToList();
                    if(list.Count==0)
                        list = (from dm in db.dvDien3PhaDinhMucs where dm.MaTN == this.MaTN & dm.MaLMB==_MaLMB select new { dm.ID, dm.TenDM, dm.DonGia }).ToList();
                    if(list.Count==0)
                        list = (from dm in db.dvDien3PhaDinhMucs where dm.MaTN == this.MaTN select new { dm.ID, dm.TenDM, dm.DonGia }).ToList();

                    lkDinhMuc.DataSource = list;

                    txtKhachHang.EditValue = type.GetProperty("TenKH").GetValue(r, null);
                    txtKhachHang.Tag = _MaKH;
                    
                    var _TuNgay = (from g in db.dvDien3Phas where g.MaMB == _MaMB & g.MaKH == _MaKH select g.DenNgay).Max();
                    dateTuNgay.EditValue = _TuNgay ?? db.GetSystemDate();
                }
            }
            catch { }
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateDenNgay.EditValue = dateTuNgay.DateTime.AddMonths(1);
            }
            catch { }
        }

        private void spThanhTien_EditValueChanged(object sender, EventArgs e)
        {
            spTienVAT.EditValue = spTyLeVAT.Value * spThanhTien.Value;
            spTienTT.EditValue = spTienVAT.Value + spThanhTien.Value;
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

            if (dateNgayTT.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Ngày TT]. Xin cảm ơn!");
                dateNgayTT.Focus();
                return;
            }

            if (dateDenNgay.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [Đến ngày]. Xin cảm ơn!");
                dateDenNgay.Focus();
                return;
            }

            if (SqlMethods.DateDiffDay(dateTuNgay.DateTime, dateDenNgay.DateTime) < 0)
            {
                DialogBox.Alert("Khoảng thời gian không hợp lý. Vui lòng kiểm tra lại!");
                dateDenNgay.Focus();
                return;
            }

            gvChiTiet.UpdateCurrentRow();
            if (gvChiTiet.RowCount < 2)
            {
                DialogBox.Error("Vui lòng nhập thông tin chi tiết");
                return;
            }

            var count = db.dvDien3Phas.Where(p => p.ID != objDien.ID & p.MaMB == (int)glkMatBang.EditValue & p.MaKH == (int)txtKhachHang.Tag & p.MaDH== (int?)glkDongHo.EditValue
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
                db.dvDien3Phas.InsertOnSubmit(objDien);
            }
            else
            {
                objDien.MaNVS = Common.User.MaNV;
                objDien.NgaySua = db.GetSystemDate();
            }

            objDien.MaMB = (int?)glkMatBang.EditValue;
            objDien.MaKH = (int?)txtKhachHang.Tag;
            objDien.NgayTT = dateNgayTT.DateTime;
            objDien.TuNgay = dateTuNgay.DateTime;
            objDien.DenNgay = dateDenNgay.DateTime;
            objDien.SoTieuThu = Convert.ToInt32(spSoTieuThu.Value);
            objDien.ThanhTien = spThanhTien.Value;
            objDien.TyLeVAT = spTyLeVAT.Value;
            objDien.TienVAT = spTienVAT.Value;
            objDien.TienTT = spTienTT.Value;
            objDien.DienGiai = txtDienGiai.Text;
            objDien.NgayTB = new DateTime((int)itemNam.Value, (int)itemThang.Value, 1);
            objDien.MaDH = (int?)glkDongHo.EditValue;
            objDien.HeSo = Convert.ToInt32(spHeSo.EditValue);
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

        private void lkDinhMuc_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var lkDM = sender as LookUpEdit;
             
                    gvChiTiet.SetFocusedRowCellValue("DonGia", lkDM.GetColumnValue("DonGia"));
             

                if (glkMatBang.EditValue != null)
                {
                    var _MaMB = (int)glkMatBang.EditValue;
                    

                    if(glkDongHo.EditValue != null)
                    {
                        var _ChiSoCu = (from ct in db.dvDien3PhaChiTiets
                                        join d in db.dvDien3Phas on ct.MaDien equals d.ID
                                        where ct.MaDM == (int)lkDM.EditValue 
                                        & d.MaMB == _MaMB 
                                        & d.MaKH == this.GetMaKhachHang()
                                        & d.MaDH == (int)glkDongHo.EditValue
                                        select ct.ChiSoMoi).Max().GetValueOrDefault();
                        gvChiTiet.SetFocusedRowCellValue("ChiSoCu", _ChiSoCu);
                    }
                    else
                    {
                        var _ChiSoCu = (from ct in db.dvDien3PhaChiTiets
                                        join d in db.dvDien3Phas on ct.MaDien equals d.ID
                                        where ct.MaDM == (int)lkDM.EditValue & d.MaMB == _MaMB & d.MaKH == this.GetMaKhachHang()
                                        select ct.ChiSoMoi).Max().GetValueOrDefault();
                        gvChiTiet.SetFocusedRowCellValue("ChiSoCu", _ChiSoCu);
                    }
                }
            }
            catch { }
        }

        private void gvChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "ChiSoCu":
                case "ChiSoMoi":
                case "DonGia":
                    var objCT = (dvDien3PhaChiTiet)gvChiTiet.GetRow(e.RowHandle);
                    objCT.SoLuong = objCT.ChiSoMoi - objCT.ChiSoCu;

                    int? heSo = 1;
                    try
                    {
                        var objDongHo = db.dvDien3PhaDongHos.FirstOrDefault(_ => _.ID == (int?)glkDongHo.EditValue);
                        if (objDongHo != null)
                        {
                            heSo = objDongHo.HeSo.GetValueOrDefault();
                        }
                    }
                    catch
                    {

                    }

                    objCT.ThanhTien = objCT.SoLuong * objCT.DonGia * heSo;
                    break;
            }
        }
        
        private void gvChiTiet_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            this.TinhThanhTien();
        }

        private void gvChiTiet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var ct = e.Row as dvDien3PhaChiTiet;
            if (ct == null) return;

            if (ct.MaDM == null)
            {
                e.ErrorText = "Vui lòng chọn [Định mức]";
                e.Valid = false;
                return;
            }

            if (ct.ChiSoCu == null)
            {
                e.ErrorText = "Vui lòng nhập [Chỉ số cũ]";
                e.Valid = false;
                return;
            }

            if (ct.ChiSoMoi.GetValueOrDefault() <= 0)
            {
                e.ErrorText = "Vui lòng nhập [Chỉ số mới]";
                e.Valid = false;
                return;
            }

            if (ct.DonGia.GetValueOrDefault() <= 0)
            {
                e.ErrorText = "Vui lòng nhập [Đơn giá]";
                e.Valid = false;
                return;
            }
        }

        private void gcChiTiet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.No) return;
                gvChiTiet.DeleteSelectedRows();

                this.TinhThanhTien();
            }
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            dateNgayTT.EditValue = dateDenNgay.EditValue;
        }
    }
}