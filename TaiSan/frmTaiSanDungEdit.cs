using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq;
using System.Linq;

namespace TaiSan
{
    public partial class frmTaiSanDungEdit : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNhanVien;
        MasterDataContext db;
        public int MaTS { get; set; }
        private tsTaiSanSuDung objTS;

        public frmTaiSanDungEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        void LoadData()
        {
            if (MaTS != 0)
            {
                objTS = db.tsTaiSanSuDungs.Single(p => p.MaTS == MaTS);
                lookHangSX.EditValue = objTS.MaHSX;
                lookLoaiTS.EditValue = objTS.MaLTS;
                lookMatBang.EditValue = objTS.MaMB;
                lookNhaCungCap.EditValue = objTS.MaNCC;
                lookTaiSanNguon.EditValue = objTS.MaTSCHA;
                lookToaNha.EditValue = objTS.MaTN;
                lookTrangThai.EditValue = objTS.MaTT;
                txtDienGiai.Text = objTS.DienGiai;
                txtKyHieu.EditValue = objTS.KyHieu;
                spinGiaTriTS.EditValue = objTS.GiaTriTaiSan;
                spinThoiHan.EditValue = objTS.ThoiHan;
                dateNgayBDSuDung.EditValue = objTS.NgaySD;
                dateNgayHetHan.EditValue = objTS.NgayHH;
                dateNgaySX.EditValue = objTS.NgaySX;
            }

        }

        void SaveData()
        {
            #region Rang buoc du lieu
            if (txtKyHieu.Text == "")
            {
                DialogBox.Alert("Vui lòng nhập ký hiệu tài sản. Xin cảm ơn!");
                txtKyHieu.Focus();
                return;
            }
            if (lookLoaiTS.EditValue == null)
            {
                DialogBox.Alert("Cần chọn loại tài sản. Xin cảm ơn!");
                lookLoaiTS.Focus();
                return;
            }
            #endregion

            var wait = DialogBox.WaitingForm();
            try
            {
                if (MaTS == 0)
                {
                    objTS = db.tsTaiSanSuDungs.Single(p => p.MaTS == MaTS);
                    objTS.MaNVTao = objNhanVien.MaNV;
                    objTS.NgayTao = DateTime.Now;
                    db.tsTaiSanSuDungs.InsertOnSubmit(objTS);
                }
                else
                {
                    objTS.MaNVCN = objNhanVien.MaNV;
                    objTS.NgayCN = DateTime.Now;
                    tsTaiSanSuDung_L objls = new tsTaiSanSuDung_L();
                    objls.MaNVCN = objNhanVien.MaNV;
                    objls.NgayCN = DateTime.Now;
                    objTS.tsTaiSanSuDung_Ls.Add(objls);
                }
                objTS.KyHieu = txtKyHieu.Text.Trim();
                objTS.MaTSCHA = (int?)lookTaiSanNguon.EditValue;
                objTS.MaLTS = (int?)lookLoaiTS.EditValue;
                objTS.MaTN = (byte?)lookToaNha.EditValue;
                objTS.MaMB = (int?)lookMatBang.EditValue;
                objTS.MaTT = (int?)lookTrangThai.EditValue;
                objTS.MaXX = (int?)lookXuatXu.EditValue;
                objTS.MaHSX = (int?)lookHangSX.EditValue;
                objTS.MaNCC = (int?)lookNhaCungCap.EditValue;
                objTS.NgayHH = (DateTime?)dateNgayHetHan.EditValue;
                objTS.NgaySD = (DateTime?)dateNgayBDSuDung.EditValue;
                objTS.NgaySX = (DateTime?)dateNgaySX.EditValue;
                objTS.ThoiHan = (int?)spinThoiHan.EditValue;
                objTS.GiaTriTaiSan = (decimal?)spinGiaTriTS.EditValue;
                objTS.DienGiai = txtDienGiai.Text.Trim();

                db.SubmitChanges();
                DialogBox.Alert("Bạn đã cập nhật dữ liệu thành công!");
            }
            catch
            { }
            finally
            {
                wait.Close();
                wait.Dispose();               
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTaiSanDungEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}