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

namespace DichVu.ChoThue.CongNo
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaCN { get; set; }
        thueCongNo objCN;
        MasterDataContext db;
        public tnNhanVien objNV;
        public frmEdit()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            objCN = db.thueCongNos.FirstOrDefault(p => p.MaCN == MaCN);
            if (objCN == null)
                return;
            spinSoTien.EditValue = objCN.ConNo ?? 0;
            dateNgayTT.EditValue = objCN.ChuKyMin;
            txtDienGiai.Text = objCN.DienGiai;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            var objLS = new cnlsDieuChinh();
            objLS.SoTienTruocDC = objCN.ConNo;
            objLS.SoTien = (decimal?)spinSoTien.EditValue;
            objLS.GhiChu = txtDienGiai.Text;
            objLS.MaNV = objNV.MaNV;
            objLS.NgayDC = db.GetSystemDate();
            db.cnlsDieuChinhs.InsertOnSubmit(objLS);
            objCN.ConNo = (decimal?)spinSoTien.EditValue;
            objCN.ChuKyMin = objCN.ChuKyMax = (DateTime?)dateNgayTT.EditValue;
            objCN.DienGiai = txtDienGiai.Text;
          
            db.SubmitChanges();
            this.Close();
        }
    }
}