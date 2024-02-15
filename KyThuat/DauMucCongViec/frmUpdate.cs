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
    public partial class frmUpdate : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien ObjNV { get; set; }
        public long? MaDMCV { get; set; }
        MasterDataContext db;
        btDauMucCongViec objDMCV;

        public frmUpdate()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          //  objDMCV.TienDoTH = (decimal?)spinTienDo.EditValue;
            var objls = new btDauMucCongViec_LichSu();
            objls.MaNVCN = ObjNV.MaNV;
            objls.NgayCN = DateTime.Now;
            objls.TienDo = objDMCV.TienDoTH = (decimal?)spinTienDo.EditValue;
            objls.DienGiai = txtDienGiai.Text.Trim();
            objDMCV.btDauMucCongViec_LichSus.Add(objls);
            try
            {
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
            objDMCV = db.btDauMucCongViecs.SingleOrDefault(p => p.ID == MaDMCV);
            this.Text = string.Format("Cập nhật tiến độ công viêc: {0}", objDMCV.MaSoCV);
            spinTienDo.EditValue = (decimal?)objDMCV.TienDoTH;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}