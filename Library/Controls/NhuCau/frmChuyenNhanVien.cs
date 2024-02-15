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


namespace Library.Controls.NhuCau
{
    public partial class frmChuyenNhanVien : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? MaNC { get; set; }
        ncNhuCau objNC;
        byte SDBID = 0;

        public frmChuyenNhanVien()
        {
            InitializeComponent();

            
        }

        private void frmChuyenNhanVien_Load(object sender, EventArgs e)
        {
            //ctlStaffCheckListEdit1.LoadData();
            ctlStaffCheckListEdit1.Properties.DataSource = db.tnNhanViens.Where(o=>o.IsLocked.GetValueOrDefault() != true).Select(o => new { o.MaNV, HoTenNV = string.Format("{0} - {1}", o.MaSoNV, o.HoTenNV) }).ToList();
            if (this.MaNC != null)
            {
                objNC = db.ncNhuCaus.Single(p => p.MaNC == this.MaNC);
                string nv = "";
                foreach (var i in objNC.ncNhanVienHoTros)
                {
                    nv += i.MaNV + ", ";
                }
                nv = nv.TrimEnd(' ').TrimEnd(',');
                ctlStaffCheckListEdit1.SetEditValue(nv);
            }
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            string[] nv = ctlStaffCheckListEdit1.EditValue != null ? ctlStaffCheckListEdit1.EditValue.ToString().Split(',') : null;

            //objNC.NhanVienHoTro = ctlStaffCheckListEdit1.Text;

            if (this.MaNC != null)
            {
                if (nv != null)
                    foreach (var i in objNC.ncNhanVienHoTros)
                    {
                        if (nv.Where(p => p == i.MaNV.ToString()).Count() <= 0)
                        {
                            db.ncNhanVienHoTros.DeleteOnSubmit(i);
                        }
                    }
            }

            if (nv[0] != "")
            {
                foreach (var i in nv)
                {
                    if (objNC.ncNhanVienHoTros.Where(p => p.MaNV.ToString() == i).Count() <= 0)
                    {
                        var objNV = new ncNhanVienHoTro();
                        objNV.MaNV = int.Parse(i);
                        objNC.ncNhanVienHoTros.Add(objNV);
                    }
                }
            }
            //
            db.SubmitChanges();

            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}