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
    public partial class frmComplete : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public tnNhanVien objNV;
        public long? MaDMCV { get; set; }
        btDauMucCongViec objDMCV;
        public frmComplete()
        {

            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmConfirmTime_Load(object sender, EventArgs e)
        {
              objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == MaDMCV);
            //dateTGXN.EditValue = objDMCV.ThoiGianXacNhan == null ? DateTime.Now : objDMCV.ThoiGianXacNhan;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objDMCV.TienDoTH = 100M;
            objDMCV.TrangThaiCV = 7;
            objDMCV.ThoiGianHT = (DateTime?)dateTGXN.EditValue;
            var objls = new btDauMucCongViec_LichSu();
            objls.MaNVCN = objNV.MaNV;
            objls.NgayCN = DateTime.Now;
            objls.TienDo = 100M;
            objls.TrangThaiCV = 7;
            objls.DienGiai = txtDienGiai.Text.Trim();
            objDMCV.btDauMucCongViec_LichSus.Add(objls);
            tnycYeuCau objYC;
            if (objDMCV.NguonCV == 0)//nếu nguồn từ yêu cau KH thì cập nhật luôn tạng thái hoàn thành cho yêu cầu từ lễ tân
            {
                objYC = db.tnycYeuCaus.SingleOrDefault(p => p.ID == objDMCV.MaNguonCV);
                objYC.NgayHoanThanh = DateTime.Now;
                objYC.MaTT = 5;
                var objLSYC = new tnycLichSuCapNhat();
                //objLSYC.TrangThaiYC = 5;
                objLSYC.NgayCN = DateTime.Now;
                objLSYC.MaNV = objNV.MaNV;
                objYC.tnycLichSuCapNhats.Add(objLSYC);
            }
            try
            {
                db.SubmitChanges();
                DialogBox.Alert("Cập nhật dữ liệu thành công!");
            }
            catch
            {
                DialogBox.Alert("Dữ liệu không thể lưu!");
            }
            this.Close();
        }


    }
}