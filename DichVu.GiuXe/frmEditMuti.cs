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
//using DevExpress.XtraRichEdit.Utils.NumberConverters;

namespace DichVu.GiuXe
{
    public partial class frmEditMuti : DevExpress.XtraEditors.XtraForm
    {
        public frmEditMuti()
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

            //spTienTT.EditValue = spKyTT.Value * _TienTT;
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
                //txtSoDK.Text = objGX.SoDK;
                dateNgayDK.EditValue = objGX.NgayDK;
                dateNgayTT.EditValue = objGX.NgayTT;
                spKyTT.EditValue = objGX.KyTT;
                //spTienTT.EditValue = objGX.TienTT;
                txtDienGiai.Text = objGX.DienGiai;
                ckbNgungSuDung.EditValue = objGX.NgungSuDung;
            }
            else
            {
                objGX = new dvgxGiuXe();
                //txtSoDK.Text = this.getNewMaTX();
                dateNgayDK.EditValue = db.GetSystemDate();
            }

            gcTheXe.DataSource = //objGX.dvgxTheXes;
            db.dvgxTheXes.Where(p => p.ID == 0);
        }

        private void glkMatBang_SizeChanged(object sender, EventArgs e)
        {
            var glk = sender as GridLookUpEdit;
            glk.Properties.PopupFormSize = new Size(glk.Size.Width, 0);
        }

        private int? MaKH = 0;
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
                    MaKH=objGX.MaKH = (int?)type.GetProperty("MaKH").GetValue(r, null);
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
                              select bg).FirstOrDefault();
                if(objGia==null)
                return;
                gvTheXe.SetFocusedRowCellValue("MaDM", objGia.MaDM);
                gvTheXe.SetFocusedRowCellValue("GiaThang", objGia.DonGiaThang);
                gvTheXe.SetFocusedRowCellValue("TienTruocThue", objGia.DonGiaThang);
                gvTheXe.SetFocusedRowCellValue("ThueGTGT", spVAT.Value);
                gvTheXe.SetFocusedRowCellValue("TienThueGTGT", spVAT.Value * objGia.DonGiaThang* spKyTT.Value);
                gvTheXe.SetFocusedRowCellValue("TienTT", (objGia.DonGiaThang * spKyTT.Value) + (spVAT.Value * objGia.DonGiaThang * spKyTT.Value));
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

       

            if (dateNgayDK.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày đăng ký");
                dateNgayDK.Focus();
                return;
            }
            if (dateNgayTT.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày TT");
                dateNgayTT.Focus();
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

                for (int i = 0; i < gvTheXe.RowCount - 1; i++)
                {
                    var t = (int) gvTheXe.GetRowCellValue(i, "ID");
                    if ((int)gvTheXe.GetRowCellValue(i, "ID") == 0 
                        )
                    {
                        var dbo = new MasterDataContext();
                        var objTheXe = new dvgxTheXe();
                        objTheXe.MaTN = this.MaTN;
                        objTheXe.NgayNhap = dbo.GetSystemDate();
                        objTheXe.MaNVN = Common.User.MaNV;
                        objTheXe.IsTheXe = true;
                        objTheXe.MaKH = MaKH;
                        objTheXe.MaMB = (int)glkMatBang.EditValue;
                        objTheXe.SoThe = gvTheXe.GetRowCellValue(i,"SoThe").ToString();
                        objTheXe.NgayDK = (DateTime?)dateNgayDK.EditValue;
                        objTheXe.MaLX = (int?)gvTheXe.GetRowCellValue(i, "MaLX");
                        objTheXe.GiaThang = (decimal)gvTheXe.GetRowCellValue(i, "GiaThang");
                        objTheXe.TienTruocThue = (decimal)gvTheXe.GetRowCellValue(i, "GiaThang") * spKyTT.Value;
                        objTheXe.ThueGTGT = (decimal)gvTheXe.GetRowCellValue(i, "ThueGTGT");
                        objTheXe.TienThueGTGT = (decimal)gvTheXe.GetRowCellValue(i, "TienThueGTGT");
                        //objTheXe.MaNK = objTheXe.MaNK;
                        var s = gvTheXe.GetRowCellValue(i, "ChuThe");
                        if (s!=null)
                        {
                            objTheXe.ChuThe = gvTheXe.GetRowCellValue(i, "ChuThe").ToString();
                        }
                        if (gvTheXe.GetRowCellValue(i, "BienSo")!=null)
                        objTheXe.BienSo = gvTheXe.GetRowCellValue(i, "BienSo").ToString();
                        if (gvTheXe.GetRowCellValue(i, "MauXe")!=null)
                        objTheXe.MauXe = gvTheXe.GetRowCellValue(i, "MauXe").ToString();
                        if (gvTheXe.GetRowCellValue(i, "DoiXe")!=null)
                        objTheXe.DoiXe = gvTheXe.GetRowCellValue(i, "DoiXe").ToString();
                        objTheXe.NgayTT = (DateTime)dateNgayTT.EditValue;
                        objTheXe.KyTT = (decimal)spKyTT.EditValue;
                        objTheXe.TienTT = (decimal)gvTheXe.GetRowCellValue(i, "TienTT");
                        objTheXe.DienGiai = txtDienGiai.Text;
                        objTheXe.NgungSuDung = false;
                        //objTheXe.NgayNgungSD = objTheXe.NgayNgungSD;
                        dbo.dvgxTheXes.InsertOnSubmit(objTheXe);
                        dbo.SubmitChanges();
                    }
                }
              
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

        private void txtBienSo_EditValueChanged(object sender, EventArgs e)
        {
            var tam = (TextEdit)sender;
            if (!string.IsNullOrEmpty(tam.Text.Trim()))
            {
                gvTheXe.SetFocusedRowCellValue("DienGiai", string.Format("Phí giữ xe biển số {0}",tam.Text));
            }
            else
            {
                gvTheXe.SetFocusedRowCellValue("DienGiai", "");
            }
        }

        private void spTyLeVAT_EditValueChanged(object sender, EventArgs e)
        {
            var tam = (SpinEdit)sender;
            var DG = Convert.ToInt64(gvTheXe.GetFocusedRowCellValue("GiaThang"));
            gvTheXe.SetFocusedRowCellValue("ThueGTGT", tam.Value);
            gvTheXe.SetFocusedRowCellValue("TienThueGTGT", tam.Value * DG * spKyTT.Value);
            gvTheXe.SetFocusedRowCellValue("TienTT", (DG * spKyTT.Value) + (tam.Value * DG * spKyTT.Value));
       
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