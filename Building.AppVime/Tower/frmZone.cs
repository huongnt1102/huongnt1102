using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime.Tower
{
    public partial class frmZone : DevExpress.XtraEditors.XtraForm
    {
        public int TowerId { get; set; }
        public int ServiceId { get; set; }
        public object Datasource { get; set; }
        MasterDataContext db;
        bool IsLoaded = false;

        public frmZone()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEditZone();
            f.ServiceId = ServiceId;
            f.TowerId = this.TowerId;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmEditZone();
            f.TowerId = this.TowerId;
            f.ServiceId = ServiceId;
            f.KeyId = (int)gvZone.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvZone.FocusedRowHandle >= 0)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

                db = new MasterDataContext();

                var obj = db.app_Zones.FirstOrDefault(p => p.Id == (int)gvZone.GetFocusedRowCellValue("Id"));
                try
                {
                    db.app_Zones.DeleteOnSubmit(obj);
                    db.SubmitChanges();

                    gvZone.DeleteRow(gvZone.FocusedRowHandle);
                }
                catch (Exception ex) { }
            }
            else
            {
                DialogBox.Alert("Vui lòng chọn dữ liệu.");
            }
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            lookUpService.DataSource = Datasource;
            itemService.EditValue = ServiceId;

            LoadData();

            IsLoaded = true;
        }

        void LoadData()
        {
            db = new MasterDataContext();
            gcZone.DataSource = db.app_Zones.Where(p => p.TowerId == this.TowerId & p.ServiceId == this.ServiceId);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gvZone_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvZone.FocusedRowHandle < 0)
            {
                gcZoneTime.DataSource = null;
                return;
            }

            LoadTime();
        }

        void LoadTime()
        {
            try
            {
                //db = new MasterDataContext();
                gcZoneTime.DataSource = db.app_ZoneTimes.Where(p => p.ZoneId == (int)gvZone.GetFocusedRowCellValue("Id"))
                        .Select(p => new
                        {
                            p.Id,
                            p.Name,
                            p.NumberIndex,
                            TimeFrom = string.Format("{0}:{1}", p.HourFrom >= 10 ? p.HourFrom.ToString() : "0" + p.HourFrom.ToString(), p.MinuteFrom >= 10 ? p.MinuteFrom.ToString() : "0" + p.MinuteFrom.ToString()),
                            TimeTo = string.Format("{0}:{1}", p.HourTo >= 10 ? p.HourTo.ToString() : "0" + p.HourTo.ToString(), p.MinuteTo >= 10 ? p.MinuteTo.ToString() : "0" + p.MinuteTo.ToString())
                        });
            }
            catch
            {
                gcZoneTime.DataSource = null;
            }
        }

        private void itemTime_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvZone.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Khu vực], xin cảm ơn.");
                return;
            }

            var f = new frmEditZoneTime();
            f.TowerId = this.TowerId;
            f.ZoneId = (int)gvZone.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadTime();
        }

        private void itemTime_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvZoneTime.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Suất], xin cảm ơn.");
                return;
            }

            var f = new frmEditZoneTime();
            f.TowerId = this.TowerId;
            f.ZoneId = (int)gvZone.GetFocusedRowCellValue("Id");
            f.KeyId = (int)gvZoneTime.GetFocusedRowCellValue("Id");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadTime();
        }

        private void itemTime_Del_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvZoneTime.FocusedRowHandle < 0)
            {
                DialogBox.Alert("Vui lòng chọn [Suất], xin cảm ơn.");
                return;
            }

            if (DialogBox.Question("Bạn có chắc chắn muốn xóa không?") == DialogResult.No) return;

            try
            {
                db = new MasterDataContext();
                var objCheck = db.app_BookingDetails.FirstOrDefault(p => p.ZoneTimeId == (int)gvZoneTime.GetFocusedRowCellValue("Id"));
                if (objCheck == null)
                {
                    var obj = db.app_ZoneTimes.FirstOrDefault(p => p.Id == (int)gvZoneTime.GetFocusedRowCellValue("Id"));
                    db.app_ZoneTimes.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    gvZoneTime.DeleteRow(gvZoneTime.FocusedRowHandle);
                }
                else
                {
                    DialogBox.Alert("[Suất] này đã được sử dụng. Vui lòng kiểm tra lại, xin cảm ơn.");
                }
            }
            catch { }
        }

        private void itemService_EditValueChanged(object sender, EventArgs e)
        {
            if (IsLoaded)
            {
                try
                {
                    ServiceId = Convert.ToInt32(itemService.EditValue);
                }catch { }

                LoadData();

                if (gvZone.FocusedRowHandle >= 0)
                {
                    LoadTime();
                }
                else
                {
                    gcZoneTime.DataSource = null;
                }        
            }
        }
    }
}
