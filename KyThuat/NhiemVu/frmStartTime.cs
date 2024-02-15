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

namespace KyThuat.NhiemVu
{
    public partial class frmStartTime : DevExpress.XtraEditors.XtraForm
    {

        public btDauMucCongViec_NhanVien objCVNV;
        public tnNhanVien ObjNV { get; set; }
        MasterDataContext db;

        public frmStartTime()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objCVNV = db.btDauMucCongViec_NhanViens.SingleOrDefault(p => p.ID == objCVNV.ID);
                objCVNV.ThoiGianTH = (DateTime?)dateNgayBD.EditValue;
                objCVNV.DienGiai = txtLyDo.Text.Trim();
                var objLS = new btDauMucCongViec_NhanVienLSTH();
                objLS.DienGiai = txtLyDo.Text.Trim();
                objLS.MaNV = ObjNV.MaNV;
                objLS.NgayTH = db.GetSystemDate();
                objLS.TienDo = objCVNV.TienDo;
                objLS.TrangThai = objCVNV.TrangThai;
                objCVNV.btDauMucCongViec_NhanVienLSTHs.Add(objLS);
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được cập nhật!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Close();
        }

        private void frmStartTime_Load(object sender, EventArgs e)
        {
            if (objCVNV != null)
                dateNgayBD.EditValue = objCVNV.ThoiGianTH;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}