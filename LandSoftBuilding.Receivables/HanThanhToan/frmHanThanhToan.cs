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

namespace LandSoftBuilding.Receivables.HanThanhToan
{
    public partial class frmHanThanhToan : DevExpress.XtraEditors.XtraForm
    {
        public DateTime? han_thanh_toan { get; set; }
        public frmHanThanhToan()
        {
            InitializeComponent();
        }

        private void frmLaiSuat_Load(object sender, EventArgs e)
        {
            dateEdit1.DateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Đồng ý
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string text = "Đồng ý cài hạn thanh toán "+dateEdit1.DateTime.ToString() + " ?";
            if (DialogBox.Question(text) == DialogResult.Yes)
            {
                han_thanh_toan = dateEdit1.DateTime;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                this.Close();
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