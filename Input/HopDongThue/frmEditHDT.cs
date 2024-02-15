using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraLayout.Utils;
using Library;
using DevExpress.XtraReports.UI;


namespace LandSoftBuilding.Fund.Input.HopDongThue
{
    public partial class frmEditHDT : DevExpress.XtraEditors.XtraForm
    {
        public bool IsHDThue = false;
        public frmEditHDT()
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

        private void SavePhieuThu(bool _IsPrint, int _Lien)
        {
            gvChiTietPhieuThu.UpdateCurrentRow();

            #region Rang buoc
            if (lkHTTT.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn phương thức thanh toán");
                lkHTTT.Focus();
                return;
            }

            if (glKhachHang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn khách hàng");
                return;
            }


            if ((bool?)lkHTTT.GetColumnValue("IsChuyenKhoan") == true && lkTaiKhoanNganHang.EditValue == null)
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
                        TaoMaSo();
                    }
                }
                else
                {

                    var objKTCT = db.ptPhieuThus.Where(p => p.MaTN == MaTN & p.SoPT.Equals(txtSoPT.Text.Trim())).ToList();
                    if (objKTCT != null)
                    {
                        if (objKTCT.Count > 1)
                        {
                            txtSoPT.Text = db.CreateSoChungTu(10, this.MaTN);
                        }
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

            if (glkHopDong.EditValue == "Chọn hợp đồng để")
            {

                DialogBox.Error("Vui lòng chọn hợp đồng");
                glkHopDong.Focus();
                return;
            }
            if (lkMatBang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                lkMatBang.Focus();
                return;
            }
            #endregion

            #region "   Set thong tin"
            //thông tin chứng từ
            objPT.SoPT = txtSoPT.Text;
            objPT.NgayThu = (DateTime?)dateNgayThu.EditValue;
            objPT.SoTien = (decimal)spSoTien.EditValue;
            objPT.MaNV = (int)lkNhanVien.EditValue;

            // objPT.TenNganHang = txtTenNHCK.Text;
            //objPT.SoCTNH = (DateTime?)dateNgayCT.EditValue;
            objPT.MaPL = (int?)lkPhanLoai.EditValue;
            //objPT.MaHTHT = (int?)lkHTTT.EditValue;
            //thong tin chung
            objPT.MaKH = (int)glKhachHang.EditValue;
            objPT.NguoiNop = txtNguoiNop.EditValue + "";
            objPT.DiaChiNN = txtDiaChiNN.EditValue + "";
            objPT.MaMB = (int?)lkMatBang.EditValue;
            //objPT.idctHopDong = (int?)glkHopDong.EditValue;
            if (lkTaiKhoanNganHang.EditValue != null)
                objPT.MaTKNH = (int)lkTaiKhoanNganHang.EditValue;
            else
                objPT.MaTKNH = null;
            objPT.ChungTuGoc = txtChungTuGoc.Text;
            objPT.LyDo = txtDienGiai.EditValue + "";
            if (IsHDThue == true)
            {
                objPT.IsHDThue = true;
            }
            else
                objPT.IsHDThue = false;
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

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            gvChiTietPhieuThu.InvalidRowException += Common.InvalidRowException;
            layoutControlItem18.Visibility = LayoutVisibility.Never;
            // layoutControlItem19.Visibility = LayoutVisibility.Never;
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

            lkPhanLoai.Properties.DataSource = (from pl in db.ptPhanLoais where pl.ID == 22 select new { pl.ID, pl.TenPL }).ToList();
            //lkHTTT.Properties.DataSource = lkHTTT.Properties.DataSource = db.ptHinhThucThanhToans;
            lkPhanLoai.EditValue = 22;
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
                txtNguoiNop.EditValue = objPT.NguoiNop;
                txtDiaChiNN.EditValue = objPT.DiaChiNN;
                lkTaiKhoanNganHang.EditValue = objPT.MaTKNH;
                lkMatBang.EditValue = objPT.MaMB;
                txtDienGiai.EditValue = objPT.LyDo;

                //lkHTTT.EditValue = (int)objPT.MaHTHT;

                txtChungTuGoc.Text = objPT.ChungTuGoc;
                lkNhanVien.EditValue = objPT.MaNV;
                objPT.MaNVS = Library.Common.User.MaNV;
                objPT.NgaySua = db.GetSystemDate();
                txtSoPT.EditValue = objPT.SoPT;
                txtSoPT.Enabled = false;
                //dateNgayCT.EditValue = objPT.SoCTNH;

            }
            else
            {
                objPT = new ptPhieuThu();
                objPT.MaTN = this.MaTN;
                objPT.MaNVN = Library.Common.User.MaNV;
                objPT.NgayNhap = db.GetSystemDate();
                db.ptPhieuThus.InsertOnSubmit(objPT);
                // chkDC.Checked = false;
                lkHTTT.ItemIndex = 0;
                if (IsHDThue == false)
                {
                    txtSoPT.EditValue = db.CreateSoChungTu(10, this.MaTN);
                }
                else
                    txtSoPT.EditValue = db.CreateSoChungTu(11, this.MaTN);

                dateNgayThu.EditValue = DateTime.Now;
                lkNhanVien.EditValue = Library.Common.User.MaNV;
                glKhachHang.EditValue = this.MaKH;

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
            Close();
        }

        void TaoMaSo()
        {
            lkTaiKhoanNganHang.Enabled = ((bool?)lkHTTT.GetColumnValue("IsChuyenKhoan")).GetValueOrDefault();

            try
            {
                lkTaiKhoanNganHang.Enabled = ((bool?)lkHTTT.GetColumnValue("IsChuyenKhoan")).GetValueOrDefault();
                MasterDataContext db = new MasterDataContext();
                layoutControlItem18.Visibility = LayoutVisibility.Always;

                switch ((int?)lkHTTT.EditValue)
                {
                    // CHUYỂN KHOẢN
                    case 2:
                        if (this.MaPT == null & IsHDThue == true)
                            txtSoPT.EditValue = db.CreateSoChungTu(34, MaTN);
                        else
                            txtSoPT.EditValue = db.CreateSoChungTu(33, MaTN);
                        break;
                    //POS
                    case 3:
                        if (this.MaPT == null & IsHDThue == true)
                            txtSoPT.EditValue = db.CreateSoChungTu(47, MaTN);
                        else
                            txtSoPT.EditValue = db.CreateSoChungTu(45, MaTN);
                        break;
                    // Thu hộ
                    case 4:
                        if (this.MaPT == null & IsHDThue == true)
                            txtSoPT.EditValue = db.CreateSoChungTu(48, MaTN);
                        else
                            txtSoPT.EditValue = db.CreateSoChungTu(46, MaTN);
                        break;
                    //Phiếu thu
                    default:
                        if (this.MaPT == null & IsHDThue == true)
                            txtSoPT.EditValue = db.CreateSoChungTu(11, MaTN);
                        else
                            txtSoPT.EditValue = db.CreateSoChungTu(10, MaTN);
                        break;
                }
            }
            catch { }
        }

        private void cmbPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaoMaSo();
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
                    TinhSoTien();
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
                TinhSoTien();
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

                    if ((int?)lkPhanLoai.EditValue != null)
                    {
                        if ((int)lkPhanLoai.EditValue != 1)
                            lkMatBang.Properties.DataSource = db.mbMatBangs.Where(p => p.MaKHF1 == (int)glKhachHang.EditValue).ToList();
                    }

                    txtDiaChiNN.Text = (from mb in db.mbMatBangs
                                        join tn in db.tnToaNhas on mb.MaTN equals tn.MaTN
                                        where mb.MaKHF1 == (int)glKhachHang.EditValue
                                        select mb.MaSoMB + " - " + tn.TenTN).FirstOrDefault();

                    glkHopDong.Properties.DataSource = (from hd in db.ctHopDongs
                                                        where hd.MaKH == (int)glKhachHang.EditValue
                                                        select new
                                                        {
                                                            MaHD = hd.ID,
                                                            SoHD = hd.SoHDCT,
                                                            NgayKy = hd.NgayKy,
                                                        }).AsEnumerable()
                                                        .Select(o => new
                                                         {
                                                             o.MaHD,
                                                             HienThi = o.SoHD,
                                                             o.SoHD,
                                                             o.NgayKy,
                                                         }).ToList();
                }
            }
            catch { }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SavePhieuThu(false, 0);
        }

        private void itemLuu_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SavePhieuThu(true, 1);
        }

        private void ItemSave_Print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SavePhieuThu(true, 2);
        }

        void ResetHoaDon()
        {

        }

        private void BtnLuuIn_Click(object sender, EventArgs e)
        {
            SavePhieuThu(true, 1);
        }

        private void txtSoCT_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lkPhanLoai_EditValueChanged(object sender, EventArgs e)
        {
            if ((int?)lkPhanLoai.EditValue != null)
            {
                if ((int)lkPhanLoai.EditValue == 1)
                {
                    layoutControlItem19.Visibility = LayoutVisibility.Never;
                    lkMatBang.EditValue = null;
                }
                else
                {
                    layoutControlItem19.Visibility = LayoutVisibility.Always;
                }
            }
        }

        private void txtPhieuThu_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}