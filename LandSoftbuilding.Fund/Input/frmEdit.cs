using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraReports.UI;
using Library;

namespace LandSoftBuilding.Fund.Input
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        // Đặt cọc giữ chổ
        public object Id_PDC { get; set; }
        public bool IsThuCocGiuCho = false;
        public bool IsHDThue = false;
        
        // Đặt cọc thi công
        public bool IsDatCocThiCong = false;
        public object ThiCongId { get; set; }
        public int? PhanLoaiId { get; set; }

        public int? MaPT { get; set; }
        public int? MaMB { get; set; }
        public byte? MaTN { get; set; }
        public int? MaKH { get; set; }
        public int? MaTL { get; set; }
        public decimal? SoTien { get; set; }
        public List<ChiTietPhieuThuItem> ChiTiets { get; set; }
        public string DienGiai { get; set; }

        ptPhieuThu objPT;
        MasterDataContext db = new MasterDataContext();

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            gvChiTietPhieuThu.InvalidRowException += Common.InvalidRowException;
            glkMatBang.Visible = false;

            #region " Show  LookupItem"
            lkTaiKhoanNganHang.Properties.DataSource = (from tk in db.nhTaiKhoans
                                                        join nh in db.nhNganHangs on tk.MaNH equals nh.ID
                                                        where tk.MaTN == this.MaTN
                                                        select new
                                                        {
                                                            tk.ID,
                                                            tk.SoTK,
                                                            tk.ChuTK,
                                                            nh.TenNH,
                                                            nh.DiaChi
                                                        }).ToList();

            lkNhanVien.Properties.DataSource = (from n in db.tnNhanViens where n.MaTN == this.MaTN select new { n.MaNV, n.MaSoNV, n.HoTenNV }).ToList();
            glKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                 where kh.MaTN == this.MaTN
                                                 orderby kh.KyHieu descending
                                                 select new
                                                 {
                                                     kh.MaKH,
                                                     kh.KyHieu,
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.TenKH.ToString() : kh.CtyTen,
                                                     DiaChi = kh.DCLL
                                                 }).ToList();
            lkPhanLoai.Properties.DataSource = (from pl in db.ptPhanLoais where pl.ID != 24 select new { pl.ID, pl.TenPL, pl.IsDvApp }).ToList();
            glkLoaiDichVu.DataSource = db.dvLoaiDichVus;
            glkCompanyCode.DataSource = db.CompanyCodes;

            #endregion

            #region " Show Thong tin"
            if (this.MaPT != null)
            {
                objPT = db.ptPhieuThus.Single(pt => pt.ID == MaPT);
                //thông tin chứng từ

                dateNgayThu.EditValue = objPT.NgayThu;
                spSoTien.EditValue = objPT.SoTien;
                lkNhanVien.EditValue = Common.User.MaNV;
                lkPhanLoai.EditValue = objPT.MaPL;
                //thong tin chung
                glKhachHang.EditValue = objPT.MaKH;
                glkMatBang.EditValue = objPT.MaMB;
                txtNguoiNop.EditValue = objPT.NguoiNop;
                txtDiaChiNN.EditValue = objPT.DiaChiNN;
                lkTaiKhoanNganHang.EditValue = objPT.MaTKNH;
                txtDienGiai.EditValue = objPT.LyDo;
                cmbPTTT.SelectedIndex = objPT.MaTKNH == null ? 0 : 1;
                txtChungTuGoc.Text = objPT.ChungTuGoc;
                lkNhanVien.EditValue = objPT.MaNV;
                objPT.MaNVS = Library.Common.User.MaNV;
                objPT.NgaySua = db.GetSystemDate();
                txtSoPT.EditValue = objPT.SoPT;
                gvChiTietPhieuThu.Columns["ThuThua"].Visible = true;
                gvChiTietPhieuThu.Columns["KhauTru"].Visible = true;
                gvChiTietPhieuThu.Columns["PhaiThu"].Visible = true;
            }
            else
            {
                objPT = new ptPhieuThu();
                objPT.MaTN = this.MaTN;
                objPT.MaNVN = Library.Common.User.MaNV;
                objPT.NgayNhap = DateTime.UtcNow.AddHours(7);
                db.ptPhieuThus.InsertOnSubmit(objPT);

                cmbPTTT.SelectedIndex = 0;

                dateNgayThu.EditValue = DateTime.UtcNow.AddHours(7);
                lkNhanVien.EditValue = Library.Common.User.MaNV;
                glKhachHang.EditValue = this.MaKH;
                TaoSoPT();
                gvChiTietPhieuThu.Columns["ThuThua"].Visible = false;
                gvChiTietPhieuThu.Columns["KhauTru"].Visible = false;
                gvChiTietPhieuThu.Columns["PhaiThu"].Visible = false;
                if (this.ChiTiets != null)
                {
                    foreach (var i in this.ChiTiets)
                    {
                        var ct = new ptChiTietPhieuThu();
                        ct.LinkID = i.LinkID;
                        ct.DienGiai = i.DienGiai;
                        ct.SoTien = i.SoTien;
                        ct.IsThuThua = i.IsThuThua;
                        objPT.ptChiTietPhieuThus.Add(ct);
                    }

                    this.TinhSoTien();
                }

                if (IsThuCocGiuCho)
                {
                    lkPhanLoai.EditValue = 49;
                }

                if (IsDatCocThiCong)
                {
                    lkPhanLoai.EditValue = 50;
                }
            }

            gcChiTietPhieuThu.DataSource = objPT.ptChiTietPhieuThus;
            if (IsThuCocGiuCho)
            {
                if (MaPT == null)
                {
                    gvChiTietPhieuThu.AddNewRow();
                    gvChiTietPhieuThu.SetFocusedRowCellValue("SoTien", SoTien);
                    gvChiTietPhieuThu.SetFocusedRowCellValue("DienGiai", "Phiếu thu đặt cọc giữ chỗ");
                    gvChiTietPhieuThu.FocusedRowHandle = -1;
                }
                
                glkMatBang.ReadOnly = true;
                //PhieuDatCoc_GiuCho
                
                gvChiTietPhieuThu.SetFocusedRowCellValue("TableName", "PhieuDatCoc_GiuCho");
                if (Id_PDC != null)
                {
                    gvChiTietPhieuThu.SetFocusedRowCellValue("LinkID", Convert.ToDecimal(Id_PDC));
                }
                
            }

            if(IsDatCocThiCong)
            {
                if (MaPT == null)
                {
                    gvChiTietPhieuThu.AddNewRow();
                    gvChiTietPhieuThu.SetFocusedRowCellValue("SoTien", SoTien);

                    if (PhanLoaiId == 50)
                    {
                        gvChiTietPhieuThu.SetFocusedRowCellValue("DienGiai", "Phiếu thu đặt cọc thi công");
                    }
                    else
                    {
                        gvChiTietPhieuThu.SetFocusedRowCellValue("DienGiai", "Phiếu thu tiền phạt thi công");
                    }
                    
                    gvChiTietPhieuThu.FocusedRowHandle = -1;
                }

                glkMatBang.ReadOnly = true;
                //dkThiCong

                gvChiTietPhieuThu.SetFocusedRowCellValue("TableName", "dkThiCong");
                if (Id_PDC != null)
                {
                    gvChiTietPhieuThu.SetFocusedRowCellValue("LinkID", (Guid)Id_PDC);
                }

                if (ThiCongId != null)
                {
                    var obj = db.dkThiCongs.FirstOrDefault(_ => Convert.ToString(_.Id) == Convert.ToString(ThiCongId));

                    if (obj != null)
                    {
                        gvChiTietPhieuThu.SetFocusedRowCellValue("LinkID", Convert.ToDecimal(obj.TaiLieuId));
                    }

                    

                    lkPhanLoai.EditValue = PhanLoaiId;
                    lkPhanLoai.ReadOnly = true;
                }
                else
                {
                    lkPhanLoai.ReadOnly = false;
                }
            }

            try
            {
                if ((int?)lkPhanLoai.EditValue == 49 || (int?)lkPhanLoai.EditValue == 50 || (int?)lkPhanLoai.EditValue == 51)
                {
                    lkPhanLoai.ReadOnly = true;
                }
                else
                {
                    lkPhanLoai.ReadOnly = false;
                }
            }
            catch {
                // Có trường hợp ban đầu thêm mới ở phiếu thu, không có mặc định phân loại
            }
            
            #endregion
        }

        private void TinhSoTien()
        {
            try
            {
                spSoTien.EditValue = objPT.ptChiTietPhieuThus.Sum(o => o.SoTien);

                var strDienGiai = "";
                foreach (var i in objPT.ptChiTietPhieuThus)
                {
                    strDienGiai += "; " + i.DienGiai + string.Format(" ({0:#,0.##}đ)", i.SoTien); 
                }

                strDienGiai = strDienGiai.Trim().Trim(';');
                txtDienGiai.Text = strDienGiai;
            }
            catch { }
        }

        private void SavePhieuThu(bool _IsPrint, int _Lien)
        {
            gvChiTietPhieuThu.UpdateCurrentRow();

            #region Rang buoc
            if (cmbPTTT.SelectedIndex < 0)
            {
                DialogBox.Error("Vui lòng chọn phương thức thanh toán");
                cmbPTTT.Focus();
                return;
            }
            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }

            if(!IsThuCocGiuCho)
            {
                if (glkMatBang.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn mặt bằng");
                    return;
                }
            }

            if(!IsDatCocThiCong)
            {
                if (glkMatBang.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn mặt bằng");
                    return;
                }
            }

            if (cmbPTTT.SelectedIndex == 1 && lkTaiKhoanNganHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn số tài khoản ngân hàng");
                return;
            }
            
            if (txtSoPT.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu thu");
                txtSoPT.Focus();
                return;
            }
            else
            {
                //Trường hợp thêm
                if (MaPT == null)
                {
                    var objKTPT = db.ptPhieuThus.FirstOrDefault(p => p.MaTN == MaTN & p.SoPT.Equals(txtSoPT.Text.Trim()));
                    if (objKTPT != null)
                    {
                        TaoSoPT();
                    }
                }
                else
                {
                    //Trường hợp sửa
                    var count = db.ptPhieuThus.Where(p => p.MaTN == objPT.MaTN & p.SoPT == txtSoPT.Text & p.ID != MaPT).Count();
                    if (count > 0)
                    {
                        TaoSoPT();
                    }
                   
                }
                
            }

            if (dateNgayThu.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày thu");
                dateNgayThu.Focus();
                return;
            }
            if (MaPT == null)
            {
                if (spSoTien.Value == 0)
                {
                    DialogBox.Error("Vui lòng nhập số tiền");
                    spSoTien.Focus();
                    return;
                }
            }

            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn người thu");
                lkNhanVien.Focus();
                return;
            }

            if (lkPhanLoai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phân loại phiếu thu");
                lkPhanLoai.Focus();
                return;
            }
            #endregion
            if (MaPT != null)
            {
                db.SoQuy_ThuChis.DeleteAllOnSubmit(db.SoQuy_ThuChis.Where(p => p.IDPhieu == MaPT && p.IsPhieuThu == true));
            }

            #region Kiểm tra khóa hóa đơn
            // Cần trả về là có được phép sửa hay return
            // truyền vào form service, từ ngày đến ngày, tòa nhà

            var result = DichVu.KhoaSo.Class.ClosingEntry.Closing(objPT.MaTN, dateNgayThu.DateTime, DichVu.KhoaSo.Class.Enum.PAY);

            if (result.Count() > 0)
            {
                DialogBox.Error("Kỳ đã bị khóa");
                return;
            }

            #endregion

            #region "   Set thong tin"
            //thông tin chứng từ
            objPT.SoPT = txtSoPT.Text;
            objPT.NgayThu = (DateTime)dateNgayThu.EditValue;
            objPT.SoTien = (decimal)spSoTien.EditValue;
            objPT.MaNV = (int)lkNhanVien.EditValue;
            objPT.MaPL = (int?)lkPhanLoai.EditValue;
            //thong tin chung
            objPT.MaKH = (int)glKhachHang.EditValue;
            if(!IsThuCocGiuCho || !IsDatCocThiCong)
                objPT.MaMB = (int?) glkMatBang.EditValue;
            objPT.NguoiNop = txtNguoiNop.EditValue + "";
            objPT.DiaChiNN = txtDiaChiNN.EditValue + "";
            if (lkTaiKhoanNganHang.EditValue != null)
            {
                objPT.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
                objPT.MaHTHT = 2;
                objPT.HinhThucThanhToanId = 2;
                objPT.HinhThucThanhToanName = "Chuyển khoản";
            }
            else { 
                objPT.MaTKNH = null;
                objPT.MaHTHT = 1;
                objPT.HinhThucThanhToanId = 1;
                objPT.HinhThucThanhToanName = "Tiền mặt";
            }
                
            objPT.ChungTuGoc = txtChungTuGoc.Text;
            objPT.LyDo = txtDienGiai.EditValue + "";
            objPT.NgayNhap = DateTime.UtcNow.AddHours(7);
            objPT.IsKhauTru = false;

            if (IsThuCocGiuCho == true)
            {
                objPT.TableName = "PhieuDatCoc_GiuCho";
                objPT.LinkID = Convert.ToString(Id_PDC);

                var objPDC = db.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32( Id_PDC));
                if (objPDC != null)
                {
                    foreach (var ob in objPDC.PhieuDatCoc_GiuCho_ChiTiets)
                    {
                        using (var dbe = new MasterDataContext())
                        {
                            var objMB = dbe.mbMatBangs.FirstOrDefault(p => p.MaMB == ob.MaMB);
                            if (objMB != null)
                            {
                                objMB.MaTT = 85;
                            }
                            dbe.SubmitChanges();
                        }
                    }
                }
            }

            if (IsDatCocThiCong == true)
            {
                objPT.TableName = "dkThiCong";

                if (Id_PDC != null)
                {
                    objPT.LinkID = Convert.ToString(Id_PDC);

                    var objPDC = db.dkThiCongs.FirstOrDefault(p => p.Id == (Guid)Id_PDC);
                    if (objPDC != null)
                    {
                        foreach (var ob in objPDC.dkThiCong_ChiTiets)
                        {
                            using (var dbe = new MasterDataContext())
                            {
                                var objMB = dbe.mbMatBangs.FirstOrDefault(p => p.MaMB == ob.MaMB);
                                if (objMB != null)
                                {
                                    objMB.MaTT = 85;
                                }
                                dbe.SubmitChanges();
                            }
                        }
                    }
                }
            }


            db.SubmitChanges();

            // Kiểm tra lại số phiếu 1 lần nữa
            var countKt = db.ptPhieuThus.Where(p => p.MaTN == objPT.MaTN & p.SoPT == txtSoPT.Text & p.ID != MaPT).Count();
            if (countKt > 0)
            {
                TaoSoPT();
                objPT.SoPT = txtSoPT.Text;
                db.SubmitChanges();
            }

            foreach (var hd in objPT.ptChiTietPhieuThus)
            {
                int? iPL = (int?)lkPhanLoai.EditValue;
                var phanloai = db.ptPhanLoais.First(_ => _.ID == iPL);
                var isapp = phanloai.IsDvApp ?? false;
                // Đặt cọc sổ quỹ chưa duyệt sẽ không được đưa vào sổ quỹ
                //if (iPL != 49)
                //{
                    Common.SoQuy_Insert(db, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, this.MaTN, (int)glKhachHang.EditValue, (int?)objPT.MaMB, hd.MaPT, hd.ID, dateNgayThu.DateTime, txtSoPT.Text, cmbPTTT.SelectedIndex, (int?)lkPhanLoai.EditValue, true, hd.PhaiThu.GetValueOrDefault(), hd.SoTien.GetValueOrDefault(), iPL == 2 ? hd.SoTien.GetValueOrDefault() : hd.ThuThua.GetValueOrDefault(), hd.KhauTru.GetValueOrDefault(), hd.LinkID, hd.TableName, hd.DienGiai, Common.User.MaNV, objPT.IsKhauTru.GetValueOrDefault(), isapp);
                //}
                
            }
            #endregion

            #region Cập nhật thi công
            // Cập nhật lại tổng số tiền đặt cọc thi công
            // Cập nhật phiếu thu

            if (ThiCongId != null)
            {
                Library.Class.Connect.QueryConnect.QueryData<bool>("dkThiCong_Update", new
                {
                    ThiCongId = ThiCongId,
                    PhieuThuId = objPT.ID,
                    PhanLoaiId = PhanLoaiId
                });
            }

            #endregion

            try
            {
               

                if (_IsPrint)
                {
                    if (_Lien == 1)
                    {
                        var rpt = new rptPhieuThu(objPT.ID, this.MaTN.Value, 1);
                        for (int i = 1; i <= 3; i++)
                        {
                            var rpt1 = new rptPhieuThu(objPT.ID, this.MaTN.Value, i);
                            rpt1.CreateDocument();
                            rpt.Pages.AddRange(rpt1.Pages);
                        }
                        rpt.PrintingSystem.ContinuousPageNumbering = true;
                        rpt.ShowPreviewDialog();
                    }
                    else
                    {
                        var rpt = new rptDetail(objPT.ID, this.MaTN.Value);
                        rpt.ShowPreviewDialog();
                    }
                }

                SoTien = (decimal) spSoTien.EditValue;
                MaPT = objPT.ID;
                DienGiai = objPT.LyDo;

                if(IsDatCocThiCong == true)
                {
                    using (var dbo = new MasterDataContext())
                    {
                        var objDCTC = dbo.dkThiCongs.FirstOrDefault(p => Convert.ToString(p.Id) == Convert.ToString(Id_PDC));
                        if (objDCTC != null)
                        {
                            objDCTC.MaPT = MaPT;
                            objDCTC.MaTT = 3;
                            objDCTC.TienPhat = SoTien;
                            dbo.SubmitChanges();
                        }
                    }
                }

                else if (IsThuCocGiuCho == true)
                {
                    using (var dbo = new MasterDataContext())
                    {
                        var objPDC = dbo.PhieuDatCoc_GiuChos.FirstOrDefault(p => p.ID == Convert.ToInt32(Id_PDC));
                        if (objPDC != null)
                        {
                            objPDC.MaPT = MaPT;
                            objPDC.MaTT = 3;
                            dbo.SubmitChanges();
                        }
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void TaoSoPT()
        {
            try
            {
                txtSoPT.EditValue = Common.GetPayNumber(cmbPTTT.SelectedIndex, MaTN, Convert.ToInt32(lkTaiKhoanNganHang.EditValue));
            }
            catch (System.Exception ex) { DialogBox.Error(ex.Message); }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            db.Dispose();
            this.Close();
        }

        private void cmbPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            lkTaiKhoanNganHang.Enabled = cmbPTTT.SelectedIndex == 1;
            try
            {
                MasterDataContext db = new MasterDataContext();
                TaoSoPT();
            }
            catch { }
        }

        private void lkTaiKhoanNganHang_EditValueChanged(object sender, EventArgs e)
        {
            if (lkTaiKhoanNganHang.EditValue != null)
                txtTenNH.EditValue = db.nhTaiKhoans.Single(k => k.ID == (int)lkTaiKhoanNganHang.EditValue).nhNganHang.TenNH;
            else
                txtTenNH.EditValue = null;
            TaoSoPT();
        }

        private void gvChiTietPhieuThu_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if ((gvChiTietPhieuThu.GetRowCellValue(e.RowHandle, "DienGiai") ?? "").ToString() == "")
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập diễn giải!";
                return;
            }
            //var value = (decimal?)gvChiTietPhieuThu.GetFocusedRowCellValue("SoTien") ?? 0;
            //if (value <= 0)
            //{
            //    e.Valid = false;
            //    e.ErrorText = "Vui lòng nhập số tiền!";
            //    return;
            //}
        }

        private void gvChiTietPhieuThu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                {
                    gvChiTietPhieuThu.DeleteSelectedRows();
                    this.TinhSoTien();
                }
            }
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (db != null)
                db.Dispose();
        }

        private void gvChiTietPhieuThu_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                this.TinhSoTien();
            }
            catch { }
        }

        private void glKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var r = glKhachHang.Properties.GetRowByKeyValue(glKhachHang.EditValue);
                if (r != null)
                {
                    var type = r.GetType();
                    txtNguoiNop.Text = type.GetProperty("TenKH").GetValue(r, null).ToString();
                    txtDiaChiNN.Text = (from mb in db.mbMatBangs
                                        join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                        where mb.MaKH == (int)glKhachHang.EditValue
                                        select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();
                    if (this.MaMB != null)
                    {
                        glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                            join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                            join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                            join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                            where mb.MaTN == this.MaTN & mb.MaKH == (int)glKhachHang.EditValue && mb.MaMB == this.MaMB
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
                    }
                    else
                    {
                        glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                            join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                            join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                            join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                            where mb.MaTN == this.MaTN & mb.MaKH == (int)glKhachHang.EditValue
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
                }
            }
            catch { }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.SavePhieuThu(false, 0);
        }

        private void itemLuu_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SavePhieuThu(true, 1);
        }

        private void itemSave_Print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SavePhieuThu(true, 2);
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            this.SavePhieuThu(true, 1);
        }

        private void glkMatBang_EditValueChanged(object sender, EventArgs e)
        {
            TaoSoPT();
        }
    }

    public class ChiTietPhieuThuItem
    {
        public string TableName { get; set; }
        public long? LinkID { get; set; }
        public string DienGiai { get; set; }
        public decimal? SoTien { get; set; }
        public bool IsThuThua { get; set; }
    }
}