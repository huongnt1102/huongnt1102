using DevExpress.XtraEditors;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DichVu.Khac
{
    public partial class frmDonGia : DevExpress.XtraEditors.XtraForm
    {
        public decimal? DonGia { get; set; }

        public frmDonGia()
        {
            InitializeComponent();
        }

        private void frmLaiSuat_Load(object sender, EventArgs e)
        {
            spinDonGia.EditValue = 0;
            var db = new Library.MasterDataContext();
        }

        /// <summary>
        /// Đồng ý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DonGia = spinDonGia.Value;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch { }
        }

        /// <summary>
        /// Hủy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}