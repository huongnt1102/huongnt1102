using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Library;
using Library.Utilities;

namespace DichVu.KhachHang.CSKH
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        int? MaKH;
        ncNhuCau objNC;
        ncNhatKy objNK;
        public frmProcess(int? maKH)
        {
            InitializeComponent();
            this.MaKH = maKH;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
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

            if (!objNC.ncSanPhams.Any())
            {
                DialogBox.Error("Vui lòng nhập nhu cầu thuê");
                return;
            }

            // Lưu nhật ký
            objNK.NgayXL = dateNgayXL.DateTime;
            objNK.MaTT = (byte?)lkTrangThai.EditValue;
            objNK.MaNVQL = (int?)lookNhanVien.EditValue;
            objNK.DienGiai = txtDienGiai.Text;
            
            // Lưu cơ hội
            objNC.MaTT = 1;
            var objTT = db.ncTrangThais.FirstOrDefault(p => p.MaTT == objNC.MaTT);
            objNC.SoNC = SoChungTuCls.TaoSCT_CoHoi(Common.User.MaTN);
            objNC.MaTN = Common.User.MaTN;
            objNC.TiemNang = (byte?)objTT.TiemNang;
            objNC.MaKH = this.MaKH;
            objNC.MaNVQL = objNK.MaNVQL ?? Common.User.MaNV;
            objNC.DienGiai = objNK.DienGiai;

            // Lưu trạng thái xử lý khách hàng
            var objKH = db.tnKhachHangs.Single(o => o.MaKH == this.MaKH);
            objKH.XuLy_idkhTrangThaiXuLy = (int?)lkTrangThai.EditValue;
            objKH.XuLy_NgayXuLy = dateNgayXL.DateTime;

            db.SubmitChanges();

            NhuCauCls.SetTenCoHoi(objNC.MaNC);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            lkTrangThai.Properties.DataSource = db.ncTrangThais.OrderBy(p => p.STT).Select(p => new { p.MaTT, p.TenTT });
            lookNhanVien.Properties.DataSource = db.tnNhanViens.Select(p => new { MaNV = p.MaNV, HoTen = p.HoTenNV });

            glkNhuCauThue.DataSource = db.NhuCauThues.OrderBy(p => p.STT).Select(p => new { p.ID, p.TenNhuCau });

            lkToaNha.DataSource = db.tnToaNhas;

            dateNgayXL.EditValue = DateTime.Now;

            objNC = new ncNhuCau();
            objNC.MaNVN = Common.User.MaNV;
            objNC.NgayNhap = DateTime.Now;
            objNC.MaTT = 1;
            db.ncNhuCaus.InsertOnSubmit(objNC);

            objNK = new ncNhatKy();
            objNK.MaNVN = Common.User.MaNV;
            objNC.ncNhatKies.Add(objNK);

            gcNhuCau.DataSource = objNC.ncSanPhams;
        }

        class NhuCau
        {
            public bool IsCheck { get; set; }
            public int? idNhuCau { get; set; }
            public decimal? SoLuong { get; set; }
        }

        private void lookNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lookNhanVien.EditValue = null;
            
        }
    }
}
