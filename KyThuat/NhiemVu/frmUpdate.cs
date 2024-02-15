using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Linq;
using System.Linq;
using Library;

namespace KyThuat.NhiemVu
{
    public partial class frmUpdate : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien ObjNV { get; set; }
        public long? MaDMCV { get; set; }
        MasterDataContext db;
        btDauMucCongViec_NhanVien objDMCV;

        public frmUpdate()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          //  objDMCV.TienDoTH = (decimal?)spinTienDo.EditValue;
           
            try
            {
                var objls = new btDauMucCongViec_NhanVienLSTH();
                objls.MaNV = ObjNV.MaNV;
                objls.NgayTH = DateTime.Now;
                objls.TienDo = (decimal?)spinTienDo.EditValue;
                objDMCV.TienDo = (decimal?)spinTienDo.EditValue;
                objls.DienGiai = objDMCV.DienGiai = txtDienGiai.Text.Trim();
                objDMCV.TrangThai = objls.TrangThai = (byte?)lookTrangThai.EditValue;
                objDMCV.btDauMucCongViec_NhanVienLSTHs.Add(objls);
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã cập nhật.");
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể cập nhật.");
            }
            this.Close();
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            objDMCV = new btDauMucCongViec_NhanVien();
            lookTrangThai.Properties.DataSource = db.btDauMucCongViec_TrangThaiCVNVs;
            objDMCV = db.btDauMucCongViec_NhanViens.SingleOrDefault(p => p.ID == MaDMCV);
          //  this.Text = string.Format("Cập nhật tiến độ công viêc: {0}", objDMCV.MaSoCV);
            spinTienDo.EditValue = (decimal?)objDMCV.TienDo;
            lookTrangThai.EditValue = objDMCV.TrangThai;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}