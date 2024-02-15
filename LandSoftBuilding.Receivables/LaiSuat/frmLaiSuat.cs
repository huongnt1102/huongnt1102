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

namespace LandSoftBuilding.Receivables.LaiSuat
{
    public partial class frmLaiSuat : DevExpress.XtraEditors.XtraForm
    {
        public decimal? pt_lai_xuat { get; set; }
        public frmLaiSuat()
        {
            InitializeComponent();
        }

        private void frmLaiSuat_Load(object sender, EventArgs e)
        {
            spinEdit1.EditValue = 0;
        }

        /// <summary>
        /// Đồng ý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //string text = "Đồng ý cài lãi xuất "+spinEdit1.Value.ToString() + " ?";
            string text = "Đồng ý thay đổi lãi xuất mới?";
            if (DialogBox.Question(text) == DialogResult.Yes)
            {
                pt_lai_xuat = spinEdit1.Value;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
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