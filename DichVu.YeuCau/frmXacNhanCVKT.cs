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

namespace DichVu.YeuCau
{
    public partial class frmXacNhanCVKT : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV { get; set; }

        public frmXacNhanCVKT()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmXacNhanCVKT_Load(object sender, EventArgs e)
        {
            dateNgayXN.EditValue = db.GetSystemDate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            long? MaCV = Convert.ToInt64(spinMaSo.EditValue); ;
            if (spinMaSo.EditValue == null)
            {
                DialogBox.Alert("Bạn cần nhập mã số công việc để cập nhật. Xin cảm ơn!");
                return;
            }
            var objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == MaCV);
            if (objDMCV == null)
            {
                DialogBox.Alert("Mã số công việc này không tồn tại. Vui lòng nhập lại!");
                spinMaSo.Focus();
                return;
            }
            objDMCV.ThoiGianXacNhan = (DateTime?)dateNgayXN.EditValue;
            objDMCV.ThoiGianTheoLich = (DateTime?)dateNgayXN.EditValue;
            var objLS = new btDauMucCongViec_LichSu();
            objLS.MaNVCN = objNV.MaNV;
            objLS.NgayCN = db.GetSystemDate();
            objLS.DienGiai = txtDienGiai.Text.Trim();
            objLS.TienDo = objDMCV.TienDoTH;
            objDMCV.btDauMucCongViec_LichSus.Add(objLS);
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Dữ liệu đã được cập nhật!");
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể cập nhật. Vui lòng kiểm tra lại!");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}