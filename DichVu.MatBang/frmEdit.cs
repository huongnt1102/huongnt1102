using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.MatBang
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? MaMB { get; set; }
        public byte? MaTN { get; set; }

        MasterDataContext db = new MasterDataContext();
        mbMatBang objMB;
        
        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            gvGiaThue.InvalidRowException += Common.InvalidRowException;

            #region Load tu dien
            lkLoaiTienThue.Properties.DataSource = (from lt in db.LoaiTiens select new { lt.ID, lt.KyHieuLT, lt.TenLT, lt.TyGia }).ToList();
            lkLoaiGia.DataSource = (from lg in db.LoaiGiaThues 
                                    join lt in db.LoaiTiens on lg.MaLT equals lt.ID
                                    where lg.MaTN == this.MaTN 
                                    select new { lg.ID, lg.TenLG, DonGia = lg.DonGia * lt.TyGia, lg.MaLT }).ToList();

            lkKhoiNha.Properties.DataSource = (from kn in db.mbKhoiNhas where kn.MaTN == this.MaTN select new { kn.MaKN, kn.TenKN }).ToList();

            lkLoaiMB.Properties.DataSource = (from lmb in db.mbLoaiMatBangs where lmb.MaTN == this.MaTN select new { lmb.MaLMB, lmb.TenLMB }).ToList();

            lkTrangThai.Properties.DataSource = (from tt in  db.mbTrangThais select new { tt.MaTT, tt.TenTT }).ToList();

            glkKhachHang.Properties.DataSource = (from kh in db.tnKhachHangs
                                                  where kh.MaTN == this.MaTN
                                                  orderby kh.KyHieu descending
                                                  select new
                                                  {
                                                      kh.MaKH,
                                                      kh.KyHieu,
                                                      TenKH = kh.IsCaNhan.GetValueOrDefault() ? kh.TenKH.ToString() : kh.CtyTen,
                                                      DiaChi = kh.DCLL,
                                                      kh.IsCaNhan
                                                  }).ToList();
            glkChuSoHuu.Properties.DataSource = glkKhachHang.Properties.DataSource;
            #endregion

            if (this.MaMB != null)
            {
                objMB = db.mbMatBangs.Single(p => p.MaMB == this.MaMB);
                txtMaSoMB.Text = objMB.MaSoMB;
                txtSoNha.Text = objMB.SoNha;
                lkKhoiNha.EditValue = (from tl in db.mbTangLaus where tl.MaTL == objMB.MaTL select tl.MaKN).FirstOrDefault();
                lkTangLau.EditValue = objMB.MaTL;
                lkLoaiMB.EditValue = objMB.MaLMB;
                lkTrangThai.EditValue = objMB.MaTT;
                spDienTich.EditValue = objMB.DienTich;
                spGiaThue.EditValue = objMB.GiaThue;
                lkLoaiTienThue.EditValue = objMB.MaLT;
                glkKhachHang.EditValue = objMB.MaKH;
                glkChuSoHuu.EditValue = objMB.MaKHF1;
                dateNgayVaoO.EditValue = objMB.NgayVaoO;
                ckbCanHoCaNnhan.Checked = objMB.IsCanHoCaNhan.GetValueOrDefault();
                ckbDaGiaoChiaKhoa.Checked = objMB.DaGiaoChiaKhoa.GetValueOrDefault();
                dateNgayBanGiao.EditValue = objMB.NgayBanGiao;
                txtDienGiai.Text = objMB.DienGiai;
                spDienTichTimTuong.EditValue = objMB.DienTichTimTuong;
                spDienTichThongThuy.EditValue = objMB.DienTichThongThuy;
                spKhoangLuiSau.EditValue = objMB.KhoangLuiSauCanHo;
                spKhoangLuiTruoc.EditValue = objMB.KhoangLuiTruocCanHo;
                txtNhaThauNoiThat.Text = objMB.NhaThauThiCongHoanThienNoiThat;
                txtNhaThauXD.Text = objMB.NhaThauXayDung;
                txtNVBanGiao.Text = objMB.NhanVienBanGiaoNha;
                dateThueTuNgay.EditValue = objMB.ThueTuNgay;
                dateThueDenNgay.EditValue = objMB.ThueDenNgay;
                txtHinhThucCatDichVu.Text = Convert.ToString(objMB.HinhThucCatDichVu);
            }
            else
            {
                objMB = new mbMatBang();

                string ms = "";
                db.mbMatBang_getNewMaMB(ref ms);
                txtMaSoMB.Text = db.DinhDang(3, int.Parse(ms));
                txtSoNha.Text = txtMaSoMB.Text;
            }

            gcGiaThue.DataSource = objMB.mbGiaThues;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Rang buoc nhap lieu

                var ltToaNha = Common.TowerList.Select(p => p.MaTN.ToString()).ToList();
                var ltMB = db.mbMatBangs.Where(_=>_.MaMB != MaMB).Select(p => p.MaSoMB).ToList(); //Where(p => p.MaMB != objMB.MaMB && p.MaTN == this.MaTN)
                if (txtMaSoMB.Text.Trim().Length == 0)
                {
                    DialogBox.Error("Vui lòng nhập ký hiệu");
                    txtMaSoMB.Focus();
                    return;
                }

                if (ltMB.Contains(txtMaSoMB.Text.Trim()))
                {
                    DialogBox.Error("Ký hiệu này đã tồn tại trong hệ thống vui lòng nhập lại");
                    txtMaSoMB.Focus();
                    return;
                }

                if (lkKhoiNha.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn khối nhà");
                    lkKhoiNha.Focus();
                    return;
                }

                if (lkTangLau.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn tầng lầu");
                    lkTangLau.Focus();
                    return;
                }

                if (lkLoaiMB.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn loại MB");
                    lkLoaiMB.Focus();
                    return;
                }

                if (lkTrangThai.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn trạng thái");
                    lkTrangThai.Focus();
                    return;
                }

                //if (spDienTich.Value <= 0)
                //{
                //    DialogBox.Error("Diện tích mặt bằng không đúng");
                //    spDienTich.Focus();
                //    return;
                //}
                #endregion
                if (this.MaMB != null)
                {
                    using (var dbo = new MasterDataContext())
                    {
                        var objLS = new mbMatBang_copy();
                        objLS.MaTN = objMB.MaTN;
                        objLS.MaNVN = Common.User.MaNV;
                        objLS.NgayNhap = db.GetSystemDate();
                        objLS.MaSoMB = objMB.MaSoMB;
                        objLS.DienTichThongThuy = objMB.DienTichThongThuy;
                        objLS.DienTichTimTuong = objMB.DienTichTimTuong;
                        objLS.SoNha = objMB.SoNha;
                        objLS.MaTL = objMB.MaTL;
                        objLS.MaLMB = objMB.MaLMB;
                        objLS.MaTT = objMB.MaTT;
                        objLS.DienTich = objMB.DienTich;
                        objLS.GiaThue = objMB.GiaThue;
                        objLS.MaLT = objMB.MaLT;
                        objLS.MaKH = objMB.MaKH;
                        objLS.MaKHF1 = objMB.MaKHF1;
                        objLS.IsCanHoCaNhan = objMB.IsCanHoCaNhan;
                        objLS.DaGiaoChiaKhoa = objMB.DaGiaoChiaKhoa;
                        objLS.NgayBanGiao = objMB.NgayBanGiao;
                        objLS.DienGiai = objMB.DienGiai;
                        objLS.IDMaMB = objMB.MaMB;
                        objLS.NgayVaoO = objMB.NgayVaoO;
                        dbo.mbMatBang_copies.InsertOnSubmit(objLS);
                        dbo.SubmitChanges();
                    }

                }
                gvGiaThue.UpdateCurrentRow();

                if (this.MaMB == null)
                {
                    objMB.MaTN = this.MaTN;
                    objMB.MaNVN = Common.User.MaNV;
                    objMB.NgayNhap = db.GetSystemDate();
                    db.mbMatBangs.InsertOnSubmit(objMB);
                }
                else
                {
                    objMB.MaNVS = Common.User.MaNV;
                    objMB.NgaySua = db.GetSystemDate();
                }

                objMB.MaSoMB = txtMaSoMB.Text;
                objMB.DienTichThongThuy = (decimal?)spDienTichThongThuy.EditValue;
                objMB.DienTichTimTuong = (decimal?)spDienTichTimTuong.EditValue;
                objMB.NhanVienBanGiaoNha = txtNVBanGiao.Text;
                objMB.NhaThauThiCongHoanThienNoiThat = txtNhaThauNoiThat.Text;
                objMB.NhaThauXayDung = txtNhaThauXD.Text;
                objMB.SoNha = txtSoNha.Text;
                objMB.MaTL = (int?)lkTangLau.EditValue;
                objMB.MaLMB = (int?)lkLoaiMB.EditValue;
                objMB.MaTT = (int?)lkTrangThai.EditValue;
                objMB.KhoangLuiSauCanHo = (decimal?)spKhoangLuiSau.EditValue;
                objMB.KhoangLuiTruocCanHo = (decimal?)spKhoangLuiTruoc.EditValue;
                objMB.DienTich = (decimal?)spDienTich.Value;
                objMB.GiaThue = (decimal?)spGiaThue.Value;
                objMB.MaLT = (int?)lkLoaiTienThue.EditValue;
                objMB.NgayVaoO = (DateTime?)dateNgayVaoO.EditValue;
                objMB.MaKH = (int?)glkKhachHang.EditValue;
                objMB.MaKHF1 = (int?)glkChuSoHuu.EditValue;
                objMB.IsCanHoCaNhan = ckbCanHoCaNnhan.Checked;
                objMB.DaGiaoChiaKhoa = ckbDaGiaoChiaKhoa.Checked;
                objMB.NgayBanGiao = (DateTime?)dateNgayBanGiao.EditValue;
                objMB.DienGiai = txtDienGiai.Text;
                objMB.ThueTuNgay = (DateTime?)dateThueTuNgay.EditValue;
                objMB.ThueDenNgay = (DateTime?)dateThueDenNgay.EditValue;
                objMB.HinhThucCatDichVu = txtHinhThucCatDichVu.Text;

                // cập nhật lại chủ mặt bằng ở dịch vụ cơ bản
                var dvk = db.dvDichVuKhacs.FirstOrDefault(_ => _.MaMB == objMB.MaMB);
                if (dvk != null)
                {
                    dvk.MaKH = objMB.MaKH;
                }

                try
                {
                    db.SubmitChanges();

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                    this.Close();
                }
                finally
                {
                    db.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error("Không lưu được dữ liệu: " + ex.Message);
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lkKhoiNha_EditValueChanged(object sender, EventArgs e)
        {
            lkTangLau.Properties.DataSource = (from tl in db.mbTangLaus where tl.MaKN == (int)lkKhoiNha.EditValue select new { tl.MaTL, tl.TenTL }).ToList();
            lkTangLau.EditValue = null;
        }

        private void lkLoaiGia_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _DonGia = (decimal)(sender as LookUpEdit).GetColumnValue("DonGia") * (decimal)lkLoaiTienThue.GetColumnValue("TyGia");
                gvGiaThue.SetFocusedRowCellValue("DonGia", _DonGia);
            }
            catch { }
        }

        private void glkKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            glkChuSoHuu.EditValue = glkKhachHang.EditValue;
        }

        private void glkKhachHang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Width, 0);
        }

        private void gvGiaThue_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == coldongia.Name || e.Column.Name == coldientich.Name)
            {
                try
                {
                    var ombGiaThue = (mbGiaThue)gvGiaThue.GetRow(e.RowHandle);
                    ombGiaThue.ThanhTien = ombGiaThue.DienTich * ombGiaThue.DonGia;
                }
                catch { }
            }
        }

        private void gvGiaThue_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvGiaThue.SetFocusedRowCellValue("DienTich", spDienTich.Value);
        }

        private void gvGiaThue_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (gvGiaThue.GetRowCellValue(e.RowHandle, "MaLG") == null)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng chọn loại giá!";
                return;
            }

            var value = (decimal?)gvGiaThue.GetFocusedRowCellValue("DonGia") ?? 0;
            if (value <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập đơn giá!";
                return;
            }
            value = (decimal?)gvGiaThue.GetFocusedRowCellValue("DienTich") ?? 0;
            if (value <= 0)
            {
                e.Valid = false;
                e.ErrorText = "Vui lòng nhập diện tích!";
                return;
            }
        }
        
        private void gvGiaThue_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            spGiaThue.EditValue = objMB.mbGiaThues.Sum(p => p.ThanhTien);
        }

        private void gcGiaThue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                if (DialogBox.QuestionDelete() == System.Windows.Forms.DialogResult.Yes)
                    gvGiaThue.DeleteSelectedRows();
        }

        private void glkChuSoHuu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index ==1)
            {
                glkChuSoHuu.EditValue = null;
            }
            
        }

        private void glkKhachHang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (e.Button.Index == 1)
            //{
            //    glkKhachHang.EditValue = null;
            //}
        }
    }
}