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

namespace Library.Controls.NhuCau
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        public ncNhatKy objNK { get; set; }

        public int? MaLH { get; set; }

        public int? maNC { get; set; }

        public byte? MaTN { get; set; }

        ncNhuCau objNC;
        MasterDataContext db = new MasterDataContext();

        public frmDuyet()
        {
            InitializeComponent();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
            try
            {
                objNK = new ncNhatKy();
                objNK.MaNVN = Common.User.MaNV;
                objNK.NgayNhap = db.GetSystemDate();
                objNK.MaNC = this.maNC;
                db.ncNhatKies.InsertOnSubmit(objNK);

                objNC = db.ncNhuCaus.Single(o => o.MaNC == this.maNC);

                lkHinhThuTiepXuc.Properties.DataSource = db.ncHinhThucTiepXucs.OrderBy(p => p.STT).Select(p => new { p.ID, p.TenHTTX });

                lkTrangThai.Properties.DataSource = db.ncTrangThais.OrderBy(p => p.STT).Select(p => new { p.MaTT, p.TenTT });
                //lkNhanVien.Properties.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, HoTen = p.HoTenNV });
                glkNhanVien1.LoadData(null);

                dateNgayXL.EditValue = DateTime.Now;
                //lkTrangThai.EditValue = objNK.MaTT;
                ctlEditLichHen.Enabled = false;
                ctlEditLichHen.MaNC = this.maNC;
                ctlEditLichHen.MaTN = this.MaTN;
                ctlEditLichHen.LoadData(null);
            }
            catch { }
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            #region Rang buoc nhap lieu
            if (dateNgayXL.EditValue == null)
            {
                DialogBox.Error("Vui lòng nhập ngày xử lý");
                dateNgayXL.Focus();
                return;
            }

            if (lkTrangThai.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn trạng thái");
                lkTrangThai.Focus();
                return;
            }

            if (lkHinhThuTiepXuc.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn hình thức tiếp xúc");
                lkHinhThuTiepXuc.Focus();
                return;
            }

            //if (glkNhanVien1.EditValue == null)
            //{
            //    DialogBox.Error("Vui lòng chọn nhân viên");
            //    glkNhanVien1.Focus();
            //    return;
            //}
            #endregion

            var objTT = db.ncTrangThais.Single(o => o.MaTT == (int?)lkTrangThai.EditValue);

            objNK.NgayXL = dateNgayXL.DateTime;
            objNK.MaTT = objTT.MaTT;
            objNK.MaHTTX = (short?)lkHinhThuTiepXuc.EditValue;
            //objNK.MaNVQL = (int?)glkNhanVien1.EditValue;
            objNK.MaNVQL = Common.User.MaNV;//(int?)glkNhanVien1.EditValue == null ? Common.User.MaNV : (int?)glkNhanVien1.EditValue;
            objNK.DienGiai = txtDienGiai.Text;

            objNC.MaTT = objNK.MaTT;
            objNC.TiemNang = (byte?)objTT.TiemNang;

            objNK.NgayDuKien = dateDuKienMuaHang.EditValue == null ? (DateTime?)null :dateDuKienMuaHang.DateTime;

            if (ckbLichHen.Checked)
            {
                if (!ctlEditLichHen.Save(true))
                    return;
            }
            else
            {
                db.SubmitChanges();
                Library.Utilities.NhuCauCls.TinhTiemNang(this.maNC);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateNgayBD_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void ckbLichHen_CheckedChanged(object sender, EventArgs e)
        {
            ctlEditLichHen.Enabled = ckbLichHen.Checked;
            ctlEditLichHen.dateNgayBD.EditValue = DateTime.Now;
            ctlEditLichHen.glkNhanVien_Tiep.EditValue = Common.User.MaNV;
        }
    }
}