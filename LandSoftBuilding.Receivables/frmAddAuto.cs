using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using Library;
using System.Data.Linq.SqlClient;

namespace LandSoftBuilding.Receivables
{
    public partial class frmAddAuto : DevExpress.XtraEditors.XtraForm
    {
        public frmAddAuto()
        {
            InitializeComponent();
        }

        private void frmAddAuto_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this);
            lkToaNha.Properties.DataSource = Common.TowerList;

            //Nap thang
            for (int i = 1; i < 13; i++)
            {
                cmbThang.Properties.Items.Add(string.Format("Tháng {0:00}", i));
            }

            //Gan dữ liệu ban đầu
            lkToaNha.EditValue = Common.User.MaTN;
            cmbThang.EditValue = string.Format("Tháng {0:00}", DateTime.Now.Month);
            spNam.EditValue = DateTime.Now.Year;
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (DialogBox.Question("Bạn chắc chắn tạo hóa đơn?") == System.Windows.Forms.DialogResult.No) return;

            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    db.dvHoaDon_InsertAll((byte?)lkToaNha.EditValue, cmbThang.SelectedIndex + 1, Convert.ToInt32(spNam.Value), Common.User.MaNV);
                    //db.CapNhatVAT((byte?) lkToaNha.EditValue, cmbThang.SelectedIndex + 1, Convert.ToInt32(spNam.Value));
                }
                DIPBMS.SystemLog.Classes.SYS_LOG.Insert("Tạo hóa đơn tự động", "Tạo hóa đơn", " Tháng " + (cmbThang.SelectedIndex + 1).ToString() + "- Năm " + Convert.ToInt32(spNam.Value).ToString()+"- Dự án: "+ lkToaNha.Text);
                DialogBox.Success("Đã tạo hóa đơn thành công");

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}