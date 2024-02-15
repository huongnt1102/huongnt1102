using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace KyThuat.KeHoach
{
    public partial class frmSelectTime : DevExpress.XtraEditors.XtraForm
    {
        public int? MaNguonCV { get; set; }
        public tnNhanVien objNV { get; set; }
        public frmSelectTime()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            KyThuat.DauMucCongViec.frmEdit frm = new DauMucCongViec.frmEdit();
            frm.NguonCV = 2;
            frm.btHanNgay = Convert.ToInt32(spinSoNgay.EditValue);
            frm.objnhanvien = objNV;
            frm.MaNguonCV = MaNguonCV;
            frm.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}