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

namespace HopDongThueNgoai
{
    public partial class frmThanhLy_Edit : DevExpress.XtraEditors.XtraForm
    {
        public frmThanhLy_Edit()
        {
            InitializeComponent();
        }

        public int? ID { get; set; }
        public string MaHopDong { get; set; }
        public byte? MaTN { get; set; }
        private readonly MasterDataContext _db = new MasterDataContext();
        private hdctnThanhLy _objTl;

        public string MaPhieuThanhLy()
        {
            var pTl = (from p in _db.hdctnThanhLies
                       orderby p.SoThanhLy descending
                       select new
                       {
                           p.SoThanhLy
                       }).FirstOrDefault();
            return pTl==null ? "" : pTl.SoThanhLy;
        }

        public string MaKeTiep(string maHienTai, string tienTo)
        {
            if(maHienTai==null)
            {
                return tienTo + "0000001";
            }
            if(maHienTai=="")
            {
                return tienTo + "0000001";
            }
            int soTiep = int.Parse(maHienTai.Remove(0, tienTo.Length)) + 1;
            int chieuDai = maHienTai.Length - tienTo.Length;
            string chuoiZero = "";
            for (int i = 1; i <= chieuDai; i++)
            {
                if (soTiep < Math.Pow(10, i))
                {
                    for (int j = 1; j <= chieuDai - i; i++)
                    {
                        chuoiZero += "0";
                    }
                    return tienTo + chuoiZero + soTiep.ToString();
                }
            }
            return tienTo + soTiep;
        }

        private void frmThanhLy_Edit_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            lueLoaiTien.Properties.DataSource = (from lt in _db.LoaiTiens
                                                 select new
                                                 {
                                                     lt.ID,
                                                     lt.KyHieuLT,
                                                     lt.TyGia
                                                 }).ToList();
            if(this.ID!=null)
            {
                _objTl = _db.hdctnThanhLies.Single(p => p.RowID == this.ID);
                txtSoThanhLy.EditValue = _objTl.SoThanhLy;
                deNgayThanhLy.EditValue = _objTl.NgayThanhLy;
                spneTienThanhLy.EditValue = _objTl.TienThanhLy;
                lueLoaiTien.EditValue =int.Parse( _objTl.MaLoaiTien);
                spneTyGia.EditValue = _objTl.TyGia;
                spneTienQuyDoi.EditValue = _objTl.TienThanhLyQuyDoi;
                txtLyDo.EditValue = _objTl.LyDo;

                _objTl.NhanVienSua = Common.User.MaNV.ToString();
                _objTl.BoPhanSua = Common.User.MaPB.ToString();
                _objTl.NgaySua = _db.GetSystemDate();
            }
            else
            {
                _objTl = new hdctnThanhLy();
                var soThanhLy = MaKeTiep(MaPhieuThanhLy(), "TLHDTN/");
                var hdTl = (from p in _db.hdctnDanhSachHopDongThueNgoais
                            where p.SoHopDong == MaHopDong
                            select p).FirstOrDefault();
                _objTl.MaToaNha = this.MaTN;
                _objTl.MaHopDong = this.MaHopDong;
                _objTl.DaThu = 0;
                _objTl.DaTra = 0;
                _objTl.NhanVienNhap = Common.User.MaNV.ToString();
                _objTl.BoPhanNhap = Common.User.MaPB.ToString();
                _objTl.NgayNhap = _db.GetSystemDate();
                _objTl.NhaCungCap = hdTl.NhaCungCap;
                _objTl.CongViec = hdTl.MaCongViec;
                _db.hdctnThanhLies.InsertOnSubmit(_objTl);
                txtSoThanhLy.EditValue = soThanhLy;
                deNgayThanhLy.EditValue = _db.GetSystemDate();
                lueLoaiTien.ItemIndex = 0;

                spneTienThanhLy.EditValue = hdTl.TienSauThue - hdTl.DaTra + hdTl.DaThu;
                txtLyDo.Text = "Thanh lý hợp đồng thuê ngoài số " + soThanhLy + " - "+hdTl.SoHopDong;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmThanhLy_Edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _db.Dispose();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private void lueLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spneTyGia.EditValue = lueLoaiTien.GetColumnValue("TyGia");
            spneTienQuyDoi.EditValue = spneTienThanhLy.Value * spneTyGia.Value;
        }

        private void spneTienThanhLy_EditValueChanged(object sender, EventArgs e)
        {
            spneTienQuyDoi.EditValue = spneTienThanhLy.Value * spneTyGia.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtSoThanhLy.Text.Trim()=="")
            {
                DialogBox.Alert("Vui lòng nhập số thanh lý");
                txtSoThanhLy.Focus();
                return;
            }
            if(deNgayThanhLy.EditValue==null)
            {
                DialogBox.Alert("Vui lòng nhập ngày thanh lý");
                txtSoThanhLy.Focus();
                return;
            }
            hdctnDanhSachHopDongThueNgoai hd = _db.hdctnDanhSachHopDongThueNgoais.Single(p => p.SoHopDong == this.MaHopDong);

            _objTl.SoThanhLy = txtSoThanhLy.Text;
            _objTl.NgayThanhLy = deNgayThanhLy.DateTime;
            _objTl.TienThanhLy = spneTienThanhLy.Value;
            _objTl.MaLoaiTien = lueLoaiTien.EditValue.ToString();
            _objTl.TyGia = spneTyGia.Value;
            _objTl.TienThanhLyQuyDoi = spneTienQuyDoi.Value;
            _objTl.LyDo = txtLyDo.Text;
            _objTl.HopDongId = hd.RowID;

            if(_objTl.TienThanhLyQuyDoi>=0)
            {
                _objTl.PhaiTra = _objTl.TienThanhLyQuyDoi - _objTl.DaTra;
                _objTl.PhaiThu = 0;
            }
            else
            {
                _objTl.PhaiThu = (-_objTl.TienThanhLyQuyDoi) - _objTl.DaThu;
                _objTl.PhaiTra = 0;
            }

            hd.TrangThai = _objTl.PhaiThu == 0 & _objTl.PhaiTra == 0 ? 3 : 2;

            try
            {
                _db.SubmitChanges();
                DialogBox.Success("Dữ liệu đã được lưu");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
            finally
            {
                _db.Dispose();
                this.Close();
            }
        }
    }
}