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
    public partial class frmComplete : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        public long? MACV { get; set; }
        btDauMucCongViec_NhanVien objCVNV;
        public frmComplete()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmConfirmTime_Load(object sender, EventArgs e)
        {
              objCVNV = db.btDauMucCongViec_NhanViens.SingleOrDefault(p => p.ID == MACV);
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objCVNV.TienDo = 100;
                objCVNV.TrangThai = 4;
                objCVNV.ThoiGianHT = (DateTime?)dateTGXN.EditValue;
                objCVNV.HoanThanh = true;
                objCVNV.DienGiai = txtDienGiai.Text.Trim();
               // objCVNV
                var objLS = new btDauMucCongViec_NhanVienLSTH();
                objLS.MaNV = objNV.MaNV;
                objLS.NgayTH = DateTime.Now;
                objLS.TienDo = 100;
                objLS.TrangThai = 4;
                objLS.DienGiai = txtDienGiai.Text;
                objCVNV.btDauMucCongViec_NhanVienLSTHs.Add(objLS);
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được cập nhật.");
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể cập nhật.");
            }
            this.Close();
        }


    }
}