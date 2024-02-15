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

namespace Library.Controls.LichHen
{
    public partial class formDoiNhanVien : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        private int? MaLH;
        Library.LichHen objLH;
        private int? MaNV_Cu;
        public formDoiNhanVien(int? MaLH)
        {
            InitializeComponent();
            this.MaLH = MaLH;
        }

        private void formDoiNhanVien_Load(object sender, EventArgs e)
        {
            this.objLH = db.LichHens.Single(o => o.MaLH == this.MaLH);
            //glkNhanVien1.LoadData(this.objLH.MaTN);#L
            glkNhanVien1.LoadData(null);
            MaNV_Cu = objLH.MaNVTiep;

            glkNhanVien1.EditValue = this.objLH.MaNVTiep;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.objLH.MaNVTiep = (int?)glkNhanVien1.EditValue;

            var objLH_NV = objLH.LichHen_tnNhanViens.FirstOrDefault(o => o.MaNV == this.MaNV_Cu);

            if (objLH_NV != null)
                objLH_NV.MaNV = objLH.MaNVTiep.Value;

            db.SubmitChanges();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}