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

namespace KyThuat.DauMucCongViec
{
    public partial class frmConfirmTime : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        public long? MaDMCV { get; set; }
        btDauMucCongViec objDMCV;
        public frmConfirmTime()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmConfirmTime_Load(object sender, EventArgs e)
        {
            objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == MaDMCV);
            dateTGXN.EditValue = objDMCV.ThoiGianXacNhan == null ? DateTime.Now : objDMCV.ThoiGianXacNhan;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btDauMucCongViec_LichSu objLS = new btDauMucCongViec_LichSu();
            objDMCV.ThoiGianTH = objDMCV.ThoiGianXacNhan = (DateTime?)dateTGXN.EditValue;
            objDMCV.GioXacNhan = (DateTime?)timeGioXN.EditValue;
            
            DateTime dt = DateTime.Now;
            var time=dt.TimeOfDay;
            objLS.DienGiai = txtDienGiai.Text.Trim();
            objLS.MaNVCN = objNV.MaNV;
            objLS.NgayCN = DateTime.Now;
            objLS.TienDo = objDMCV.TienDoTH;
            objLS.TrangThaiCV = objDMCV.TrangThaiCV;
            objDMCV.btDauMucCongViec_LichSus.Add(objLS);
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Cập nhật dữ liệu thành công!");
            }
            catch
            {
                DialogBox.Error("Có lỗi phát sinh, dữ liệu không thể cập nhật!");
            }
        }


    }
}