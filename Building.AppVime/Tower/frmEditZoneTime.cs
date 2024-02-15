using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Building.AppVime.Tower
{
    public partial class frmEditZoneTime : DevExpress.XtraEditors.XtraForm
    {
        public int TowerId { get; set; }
        public int KeyId { get; set; }
        public int ZoneId { get; set; }
        MasterDataContext db;
        app_ZoneTime objZone;

        public frmEditZoneTime()
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

            objZone.HourFrom = Convert.ToByte(spinHourFrom.Value);
            objZone.MinuteFrom = Convert.ToByte(spinMinuteFrom.Value);
            objZone.HourTo = Convert.ToByte(spinHourTo.Value);
            objZone.MinuteTo = Convert.ToByte(spinMinuteTo.Value);

            var dateNow = DateTime.Now;
            var dateFrom = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, objZone.HourFrom ?? 0, objZone.MinuteFrom ?? 0, 0);
            var dateTo = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, objZone.HourTo ?? 0, objZone.MinuteTo ?? 0, 0);

            bool isPermission = SqlMethods.DateDiffMinute(dateFrom, dateTo) > 0 ? true : false;
            if (!isPermission)
            {
                DialogBox.Alert("Thời gian không hợp lệ. Vui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }

            try
            {
                objZone.Name = txtName.Text.Trim();
                objZone.NumberIndex = Convert.ToByte(spinNumberIndex.Value);
                
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

            objZone = db.app_ZoneTimes.FirstOrDefault(p => p.Id == KeyId);
            if (objZone != null)
            {
                txtName.Text = objZone.Name;
                spinNumberIndex.EditValue = objZone.NumberIndex ?? 0;
                spinHourFrom.EditValue = objZone.HourFrom ?? 0;
                spinHourTo.EditValue = objZone.HourTo ?? 0;
                spinMinuteFrom.EditValue = objZone.MinuteFrom ?? 0;
                spinMinuteTo.EditValue = objZone.MinuteTo ?? 0;
            }
            else
            {
                spinNumberIndex.EditValue =1;
                spinHourFrom.EditValue = 8;
                spinHourTo.EditValue = 8;
                spinMinuteFrom.EditValue = 0;
                spinMinuteTo.EditValue = 0;

                objZone = new app_ZoneTime();
                objZone.TowerId = this.TowerId;
                objZone.ZoneId = this.ZoneId;
                db.app_ZoneTimes.InsertOnSubmit(objZone);
            }
        }
    }
}
