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
    public partial class frmEdit_Old : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit_Old()
        {
            InitializeComponent();
        }

        public int? MaPT { get; set; }
        public byte? MaTN { get; set; }
        public int? MaKH { get; set; }
        public List<ChiTietPhieuThuItem> ChiTiets { get; set; }

        ptPhieuThu objPT;
        MasterDataContext db = new MasterDataContext();

        void TinhSoTien()
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

        void SavePhieuThu(bool _IsPrint, int _Lien)
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
            if (glkMatBang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                return;
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

            if (spSoTien.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số tiền");
                spSoTien.Focus();
                return;
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

            #region "   Set thong tin"
            //thông tin chứng từ
            objPT.SoPT = txtSoPT.Text;
            objPT.NgayThu = (DateTime)dateNgayThu.EditValue;
            objPT.SoTien = (decimal)spSoTien.EditValue;
            objPT.MaNV = (int)lkNhanVien.EditValue;
            objPT.MaPL = (int?)lkPhanLoai.EditValue;
            //thong tin chung
            objPT.MaKH = (int)glKhachHang.EditValue;
            objPT.MaMB = (int?) glkMatBang.EditValue;
            objPT.NguoiNop = txtNguoiNop.EditValue + "";
            objPT.DiaChiNN = txtDiaChiNN.EditValue + "";
            if (lkTaiKhoanNganHang.EditValue != null)
                objPT.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
            else
                objPT.MaTKNH = null;
            objPT.ChungTuGoc = txtChungTuGoc.Text;
            objPT.LyDo = txtDienGiai.EditValue + "";
            objPT.NgayNhap = DateTime.UtcNow.AddHours(7);
            #endregion

            try
            {
                db.SubmitChanges();

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
        void TaoSoPT()
        {
            if (glkMatBang.EditValue != null)
            {
                int temp = 0;
                if (int.TryParse(glkMatBang.EditValue.ToString(), out temp))
                {
                    var objMB = db.mbMatBangs.FirstOrDefault(p => p.MaMB == Convert.ToInt32(glkMatBang.EditValue));
                    txtSoPT.EditValue = Common.CreatePhieuThu(cmbPTTT.SelectedIndex, dateNgayThu.DateTime.Month, dateNgayThu.DateTime.Year, objMB.mbTangLau.MaKN.Value, MaTN.Value, false);
                }
            }
        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            gvChiTietPhieuThu.InvalidRowException += Common.InvalidRowException;

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
                                                     TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.HoKH.ToString() + " " + kh.TenKH.ToString() : kh.CtyTen,
                                                     DiaChi = kh.DCLL
                                                 }).ToList();
            lkPhanLoai.Properties.DataSource = (from pl in db.ptPhanLoais select new { pl.ID, pl.TenPL }).ToList();
            
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
            }

            gcChiTietPhieuThu.DataSource = objPT.ptChiTietPhieuThus;
            #endregion
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
        }

        private void gvChiTietPhieuThu_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if ((gvChiTietPhieuThu.GetRowCellValue(e.RowHandle, "DienGiai") ?? "").ToString() == "")
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập diễn giải!";
                return;
            }

            var value = (decimal?)gvChiTietPhieuThu.GetFocusedRowCellValue("SoTien") ?? 0;
            if (value <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập số tiền!";
                return;
            }
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

    //public class ChiTietPhieuThuItem
    //{
    //    public string TableName { get; set; }
    //    public long? LinkID { get; set; }
    //    public string DienGiai { get; set; }
    //    public decimal? SoTien { get; set; }
    //    public bool IsThuThua { get; set; }
    //}
}