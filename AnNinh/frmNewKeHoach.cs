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

namespace AnNinh
{
    public partial class frmNewKeHoach : DevExpress.XtraEditors.XtraForm
    {
        public tnNhanVien objnhanvien;
        public frmNewKeHoach()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                AnNinhKeHoach obj = new AnNinhKeHoach();
                obj.TenKeHoach = txtTenKeHoach.Text.Trim();
                obj.MaNV = objnhanvien.MaNV;

                db.AnNinhKeHoaches.InsertOnSubmit(obj);
                db.SubmitChanges();
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void frmNewKeHoach_Load(object sender, EventArgs e)
        {
            txtTenKeHoach.Focus();
        }
    }
}