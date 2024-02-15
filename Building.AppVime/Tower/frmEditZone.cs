using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Building.AppVime.Tower
{
    public partial class frmEditZone : DevExpress.XtraEditors.XtraForm
    {
        public int TowerId { get; set; }
        public int KeyId { get; set; }
        public int ServiceId { get; set; }
        MasterDataContext db;
        app_Zone objZone;

        public frmEditZone()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                DialogBox.Alert("Vui lòng nhập [Tên khu], xin cảm ơn.");
                txtName.Focus();
                return;
            }

            try
            {
                objZone.Name = txtName.Text.Trim();
                objZone.NameEN = txtNameEN.Text.Trim();
                objZone.AmountTimesBooking = Convert.ToByte(spinAmount.Value);
                objZone.IsBookToEndTime = chkBookToEndTime.Checked;

                if (KeyId == 0)
                {
                    db.SubmitChanges();

                    KeyId = objZone.Id;
                }

                var objService = db.app_TowerSettingServices.Where(p => p.TowerId == TowerId & p.ServiceId == ServiceId).FirstOrDefault();
                if (objService != null)
                {
                    objService.ZoneIdDefault = KeyId;
                }

                db.SubmitChanges();

                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                DialogBox.Alert("Đã có lỗi xảy ra. Vui lòng thử lại, xin cảm ơn.");
            }

            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            objZone = db.app_Zones.FirstOrDefault(p => p.Id == KeyId);
            if (objZone != null)
            {
                txtName.Text = objZone.Name;
                txtNameEN.Text = objZone.NameEN;
                spinAmount.EditValue = objZone.AmountTimesBooking ?? 0;
                chkBookToEndTime.Checked = objZone.IsBookToEndTime.GetValueOrDefault();
            }
            else
            {
                spinAmount.EditValue = 1;
                objZone = new app_Zone();
                objZone.TowerId = this.TowerId;
                objZone.ServiceId = this.ServiceId;
                db.app_Zones.InsertOnSubmit(objZone);
            }
        }
    }
}
