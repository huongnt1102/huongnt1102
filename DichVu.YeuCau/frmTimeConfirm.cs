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
using System.Data.Linq;

namespace DichVu.YeuCau
{
    public partial class frmTimeConfirm : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objNV;
        MasterDataContext db;
        public int? MaNguonCV { get; set; }
        public frmTimeConfirm()
        {

            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.NguonCV == 0 & p.MaNguonCV == MaNguonCV);
                var objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == MaNguonCV);;
                objDMCV.ThoiGianXacNhan = objDMCV.GioXacNhan = objDMCV.ThoiGianTH = (DateTime?)dateEdit1.EditValue;
                //objDMCV.GioTH = objDMCV.GioXacNhan = (DateTime?)timeEdit1.EditValue;
                objDMCV.TrangThaiCV = 3;
                objYC.MaTT = 4;
                var objLS = new tnycLichSuCapNhat();
                objYC.tnycLichSuCapNhats.Add(objLS);
                objLS.MaNV = objNV.MaNV;
                objLS.NgayCN = db.GetSystemDate();
                objLS.MaTT = 4;
                db.SubmitChanges();
                DialogBox.Alert("Đã hoàn thành xác nhận thời gian làm việc của khách hàng và kĩ thuật.");
                this.Close();
            }
            catch
            {
                DialogBox.Error("Đã có lỗi phát sinh vui lòng kiểm tra lại!");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTimeConfirm_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = db.GetSystemDate();
        }
    }
}