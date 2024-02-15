using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;

namespace DichVu.YeuCau.LeTan
{
    public partial class frmXuLy : DevExpress.XtraEditors.XtraForm
    {
        public DateTime? NgayRa { get; set; }
        public string GhiChu { get; set; }
        public byte? MaTN { get; set; }
        public long? ID { get; set; }
        MasterDataContext db;

        public frmXuLy()
        {
            InitializeComponent();
        }

        private void frmXuLy_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            lkTrangThai.Properties.DataSource = db.LeTanTrangThais;
            lkNhanVien.Properties.DataSource = db.tnNhanViens.Where(p => p.MaTN == this.MaTN);
            //dateNgayVao.EditValue = db.GetSystemDate();
            txtGhiChu.EditValue = "";
            dateTGTra.EditValue = db.GetSystemDate();
            var lt = db.ltLeTans.Single(p => p.ID == this.ID);
            if (lt != null)
            {
                txtMaThe.Text = lt.SoThe;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lkNhanVien.EditValue == null)
            {
                DialogBox.Alert("Vui lòng nhập [NV xử lý]. Xin cám ơn!");
                lkNhanVien.Focus();
                return;
            }
            ltXuLy objXL = new ltXuLy();
            objXL.NgayXL = db.GetSystemDate();
            objXL.MaLT = this.ID;
            objXL.GhiChu = txtGhiChu.Text;
            objXL.MaNV = (int)lkNhanVien.EditValue;
            objXL.MaTT = (int)lkTrangThai.EditValue;
            var tam =  spSLTon.EditValue;
            objXL.SLTon = Convert.ToInt32(spSLTon.EditValue);
            objXL.SLTra = Convert.ToInt32(spSLTra.EditValue);
            objXL.MaThe = txtMaThe.Text;
            objXL.GioRa =(DateTime?) dateTGTra.EditValue;
            db.ltXuLies.InsertOnSubmit(objXL);
            db.SubmitChanges();
            using (var up = new MasterDataContext())
            {
                var update = up.ltLeTans.Single(p => p.ID == this.ID);
                update.MaTT = (int)lkTrangThai.EditValue;
                update.GioRa = (DateTime?)dateTGTra.EditValue;
                update.ThoiGianTra = (DateTime?)dateTGTra.EditValue;
                up.SubmitChanges();
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spSLTra_EditValueChanged(object sender, EventArgs e)
        {
           
            var SL = Convert.ToInt32(spSLTra.EditValue);
            if (SL > 0)
            {
                var lt = db.ltLeTans.Single(p => p.ID == this.ID).SoLuongNguoi;
                if (lt > 0)
                {
                    spSLTon.EditValue = lt - SL;
                }
                
            }
        }
    }
}