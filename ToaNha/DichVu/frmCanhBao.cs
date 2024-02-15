using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ToaNha.DichVu
{
    public partial class frmCanhBao : DevExpress.XtraEditors.XtraForm
    {
        public byte? MaTN { get; set; }

        public frmCanhBao()
        {
            InitializeComponent();
        }

        private void frmCanhBao_Load(object sender, EventArgs e)
        {
            var item = ToaNha.DichVu.CanhBao.GetChiSo(MaTN);
            spinEdit1.EditValue = item.CanhBaoDien;
            spinEdit2.EditValue = item.CanhBaoNuoc;
            spinEdit3.EditValue = item.CanhBaoGas;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ToaNha.DichVu.CanhBao.SaveChiSo(MaTN, new CanhBao.chi_so_canh_bao { CanhBaoDien = (decimal?)spinEdit1.EditValue, CanhBaoNuoc = (decimal?)spinEdit2.EditValue, CanhBaoGas = (decimal?)spinEdit3.EditValue });
                Library.DialogBox.Success();
                this.Close();
            }
            catch { }
        }
    }
}