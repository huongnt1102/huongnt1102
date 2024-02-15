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
using DevExpress.XtraGrid.Views.Base;
using System.Data.Linq.SqlClient;

namespace DichVu.GiuXe
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public byte? MaTN { get; set; }

        int? MaDMGX;
        dvgxGiuXe objGX;
        MasterDataContext db;
        CachTinhCls objCT;

        string getNewMaTX()
        {
            string MaNK = "";
            db.txTheXe_getNewMaTX(ref MaNK);
            return db.DinhDang(6, int.Parse(MaNK));
        }

        void SetTienTT()
        {
            decimal _TienTT = 0;
            for (int i = 0; i < gvTheXe.RowCount; i++)
            {
                _TienTT += Convert.ToDecimal(gvTheXe.GetRowCellValue(i, "GiaThang") ?? 0);
            }

            spTienTT.EditValue = spKyTT.Value * _TienTT;
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);

            tabbedControlGroup1.SelectedTabPageIndex = 0;

            objCT = new CachTinhCls();

            gvTheXe.InvalidRowException += Common.InvalidRowException;
            gcTheXe.KeyUp += Common.GridViewKeyUp;

            db = new MasterDataContext();
            lkLoaiXe.Properties.DataSource = lkLoaiXeGird.DataSource = (from l in db.dvgxLoaiXes
                                                                        where l.MaTN == this.MaTN
                                                                        orderby l.STT
                                                                        select new { l.MaLX, l.TenLX })
                                                                        .ToList();
            glkMatBang.Properties.DataSource = (from mb in db.mbMatBangs
                                                join tl in db.mbTangLaus on mb.MaTL equals tl.MaTL
                                                join kn in db.mbKhoiNhas on tl.MaKN equals kn.MaKN
                                                join kh in db.tnKhachHangs on mb.MaKH equals kh.MaKH
                                                where kn.MaTN == this.MaTN
                                                orderby mb.MaSoMB descending
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
            
            if (this.ID != null)
            {
                objGX = db.dvgxGiuXes.Single(p => p.ID == this.ID);
                glkMatBang.EditValue = objGX.MaMB;
                txtSoDK.Text = objGX.SoDK;
                dateNgayDK.EditValue = objGX.NgayDK;
                dateNgayTT.EditValue = objGX.NgayTT;
                spKyTT.EditValue = objGX.KyTT;
                spTienTT.EditValue = objGX.TienTT;
                txtDienGiai.Text = objGX.DienGiai;
                ckbNgungSuDung.EditValue = objGX.NgungSuDung;
            }
            else
            {
                objGX = new dvgxGiuXe();
                txtSoDK.Text = this.getNewMaTX();
                dateNgayDK.EditValue = db.GetSystemDate();
            }

            gcTheXe.DataSource = objGX.dvgxTheXes;
        }

        private void glkMatBang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
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

                    objCT.MaMB = _MaMB;
                    objCT.MaTN = (byte)MaTN;

                    objGX.MaMB = (int?)glkMatBang.EditValue;
                    objGX.MaKH = (int?)type.GetProperty("MaKH").GetValue(r, null);
                }
            }
            catch { }
        }

        private void lkNhanKhau_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _TenCT = (sender as LookUpEdit).GetColumnValue("HoTenNK").ToString();
                gvTheXe.SetFocusedRowCellValue("ChuThe", _TenCT);
            }
            catch { }
        }

        private void btnThemTheXe_Click(object sender, EventArgs e)
        {
            var _MaLX = (int?)lkLoaiXe.EditValue;
            var _MaMB=(int?)glkMatBang.EditValue;
            int? SoLuong = 0;

            if (_MaLX == null)
            {
                DialogBox.Error("Vui lòng chọn loại xe");
                return;
            }

            if (spSoLuong.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập số lượng");
                spSoLuong.Focus();
                return;
            }

            objCT.LoadDinhMuc();

            var Count = gvTheXe.RowCount - 1;
            for (int i = 0; i < Count; i++)
            {
                if ((bool)gvTheXe.GetRowCellValue(i, "NgungSuDung") == false & (int)gvTheXe.GetRowCellValue(i, "MaLX") == (int)lkLoaiXe.EditValue)
                {
                    SoLuong++;
                }
            }

            for (var i = 0; i < spSoLuong.Value; i++)
            {
                gvTheXe.AddNewRow();
                gvTheXe.SetFocusedRowCellValue("SoThe", "");
                gvTheXe.SetFocusedRowCellValue("NgayDK", dateNgayDK.EditValue);
                gvTheXe.SetFocusedRowCellValue("MaLX", _MaLX);
                SoLuong++;

                var objGia = (from bg in objCT.ltBangGia
                              where bg.SoLuong <= SoLuong
                              orderby bg.SoLuong descending
                              select bg).First();

                gvTheXe.SetFocusedRowCellValue("MaDM", objGia.MaDM);
                gvTheXe.SetFocusedRowCellValue("GiaThang", objGia.DonGiaThang);
            }
        }
        
        #region Nut xu ly luu, huy
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (glkMatBang.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn mặt bằng");
                glkMatBang.Focus();
                return;
            }

            if (txtSoDK.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số thẻ");
                txtSoDK.Focus();
                return;
            }

            if (dateNgayDK.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày đăng ký");
                dateNgayDK.Focus();
                return;
            }

            gvTheXe.UpdateCurrentRow();
            if (gvTheXe.RowCount < 1)
            {
                DialogBox.Error("Vui lòng nhập thẻ xe");
                return;
            }
            #endregion

            try
            {
                if (objGX.ID == 0)
                {
                    objGX.MaTN = this.MaTN;
                    objGX.NgayNhap = db.GetSystemDate();
                    objGX.MaNVN = Common.User.MaNV;
                    db.dvgxGiuXes.InsertOnSubmit(objGX);
                }
                else
                {
                    objGX.NgaySua = db.GetSystemDate();
                    objGX.MaNVS = Common.User.MaNV;
                }

                objGX.SoDK = txtSoDK.Text;
                objGX.NgayDK = dateNgayDK.DateTime;
                objGX.NgayTT = (DateTime?)dateNgayTT.EditValue;
                objGX.KyTT = spKyTT.Value;
                objGX.TienTT = spTienTT.Value;
                objGX.DienGiai = txtDienGiai.Text;
                objGX.NgungSuDung = ckbNgungSuDung.Checked;

                //Update the xe
                foreach (var tx in objGX.dvgxTheXes)
                {
                    tx.MaTN = objGX.MaTN;
                    tx.MaKH = objGX.MaKH;
                    tx.MaMB = objGX.MaMB;
                    if (objGX.NgungSuDung == true)
                    {
                        tx.NgungSuDung = true;
                    }
                    if (tx.ID == 0)
                    {
                        tx.MaNVN = Common.User.MaNV;
                        tx.NgayNhap = db.GetSystemDate();
                    }
                    else
                    {
                        tx.MaNVS = Common.User.MaNV;
                        tx.NgaySua = db.GetSystemDate();
                    }
                }

                //Luu vao database
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                db.Dispose();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void lkLoaiXe_EditValueChanged(object sender, EventArgs e)
        {
            objCT.MaLX = (int)lkLoaiXe.EditValue;
        }

        private void ckbNgungSuDung_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvTheXe.RowCount; i++)
            {
                gvTheXe.SetRowCellValue(i, "NgungSuDung", ckbNgungSuDung.Checked);
            }

            gvTheXe.UpdateCurrentRow();
        }

        private void gvTheXe_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvTheXe.SetFocusedRowCellValue("NgungSuDung", ckbNgungSuDung.Checked);
        }

        private void gvTheXe_RowUpdated(object sender, RowObjectEventArgs e)
        {
            this.SetTienTT();
        }

        private void spKyTT_EditValueChanged(object sender, EventArgs e)
        {
            this.SetTienTT();
        }
    }

    //public class ChiTietGiuXeItem
    //{
    //    public int? MaDM { get; set; }
    //    public string TenDM { get; set; }
    //    public int? SoLuong { get; set; }
    //    public decimal? DonGiaThang { get; set; }
    //    public decimal? DonGiaNgay { get; set; }
    //}

}