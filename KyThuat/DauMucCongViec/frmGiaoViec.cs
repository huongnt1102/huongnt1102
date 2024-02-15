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

namespace KyThuat.DauMucCongViec
{
    public partial class frmGiaoViec : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        public long? MaDMCV { get; set; }
        btDauMucCongViec objDMCV;

        public frmGiaoViec()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmGiaoViec_Load(object sender, EventArgs e)
        {
            objDMCV = db.btDauMucCongViecs.SingleOrDefault(p=>p.ID==MaDMCV);
            txtMaSoCV.Text = objDMCV.MaSoCV;
            dateNgayTH.EditValue = DateTime.Now;
            lookNVGiaoViec.Properties.DataSource = db.tnNhanViens.Select(p => new { p.MaNV, p.HoTenNV });
            lookNVGiaoViec.EditValue = objNV.MaNV;
            lookTrangThaiCV.DataSource = db.btDauMucCongViec_TrangThaiCVNVs;
            slNhanVien.DataSource = db.tnNhanViens
                .Select(p => new {
                    p.MaNV,
                    p.MaSoNV,
                    p.HoTenNV,
                    p.tnPhongBan.TenPB,
                    p.tnChucVu.TenCV
                });
            gcNhanVien.DataSource = objDMCV.btDauMucCongViec_NhanViens;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var objLS = new btDauMucCongViec_LichSu();
                objLS.DienGiai = string.Format("Giao việc nhân viên và cập nhật ngày thực hiện là {0:dd/MM/yyyy}", (DateTime?)dateNgayTH.EditValue);
                objLS.MaNVCN = objNV.MaNV;
                objLS.NgayCN = DateTime.Now;
                objLS.TienDo = objDMCV.TienDoTH;
                objLS.TrangThaiCV = 6;
                objDMCV.btDauMucCongViec_LichSus.Add(objLS);
                objDMCV.ThoiGianTH = (DateTime?)dateNgayTH.EditValue;
                objDMCV.TrangThaiCV = 6;
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã cập nhật thành công");
                this.Close();
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể cập nhật. Vui long kiểm tra lại!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookNVGiaoViec_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gvNhanVien_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
           // gvNhanVien.SetFocusedRowCellValue("MaCVBT",MaDMCV);
            gvNhanVien.SetFocusedRowCellValue("MaNVGiaoViec", objNV.MaNV);
            gvNhanVien.SetFocusedRowCellValue("NgayGiaoViec", DateTime.Now);
        }

        private void gcNhanVien_Click(object sender, EventArgs e)
        {

        }
    }
}