using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
namespace LandSoftBuilding.Receivables
{
    public partial class frmSelectMonth : DevExpress.XtraEditors.XtraForm
    {
        public int Nam;
        public int Thang;
        public bool IsSave = false;
        public frmSelectMonth()
        {
            InitializeComponent();
        }

        private void frmSelectMonth_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.UtcNow.AddHours(7).Year - 1; i <= DateTime.UtcNow.AddHours(7).Year + 1; i++)
            {
                cmb_Nam.Properties.Items.Add(i);
            }
            cmb_Thang.SelectedIndex = DateTime.UtcNow.AddHours(7).Month - 1;
            cmb_Nam.SelectedIndex = 1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if(cmb_Thang.SelectedIndex==-1)
            {
                DialogBox.Alert("Vui lòng chọn tháng muốn khấu trừ");
                cmb_Thang.Focus();
                return;
            }
            if(cmb_Nam.SelectedIndex==-1)
            {
                DialogBox.Alert("Vui lòng chọn năm muốn khấu trừ");
                cmb_Nam.Focus();
            }
            Nam =Convert.ToInt32(cmb_Nam.SelectedItem.ToString());
            Thang = cmb_Thang.SelectedIndex + 1;
            string text="Bạn có chắc chắn muốn khấu trừ tự động hóa đơn vào tháng "+ Thang.ToString()+"/"+Nam.ToString()+" không?";
            if (DialogBox.Question(text) == DialogResult.Yes)
            {
                IsSave = true;
                this.Close();
            }
            else
            {
                 IsSave = false;
                this.Close();
            }
        }
    }
}