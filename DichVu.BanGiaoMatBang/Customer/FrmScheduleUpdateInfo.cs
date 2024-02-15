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
using Library;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmScheduleUpdateInfo : DevExpress.XtraEditors.XtraForm
    {
        public int? Id { get; set; }
        public byte? BuildingId { get; set; }

        private MasterDataContext _db = new MasterDataContext();

        public FrmScheduleUpdateInfo()
        {
            InitializeComponent();
        }

        private void FrmScheduleUpdateInfo_Load(object sender, EventArgs e)
        {
            if (Id == null) return;

            glkCustomer.DataSource = _db.tnKhachHangs.Where(_ => _.MaTN == BuildingId).Select(_ => new { Id = _.MaKH, Name = _.IsCaNhan == true ? (_.HoKH + " " + _.TenKH) : _.CtyTen, _.KyHieu, _.IsCaNhan }).ToList();
            glkDuty.DataSource = _db.ho_Duties.Where(_ => _.BuildingId == BuildingId).ToList();

            gc.DataSource = _db.ho_ScheduleApartments.Where(_ => _.ScheduleId == Id & _.ApartmentId!=null);
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                gv.UpdateCurrentRow();

                _db.SubmitChanges();

                DialogBox.Success("Thiết lập lịch bàn giao thành công.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                DialogBox.Error("Đã xảy ra lỗi, vui lòng kiểm tra lại.");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void GlkCustomer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }

                gv.SetFocusedRowCellValue("CustomerId", item.EditValue);
                gv.SetFocusedRowCellValue("CustomerName", item.Properties.View.GetFocusedRowCellValue("Name"));
                gv.UpdateCurrentRow();

            }
            catch { }
        }

        private void GlkDuty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;
                gv.SetFocusedRowCellValue("DutyId", item.EditValue);
                gv.SetFocusedRowCellValue("DutyName", item.Properties.View.GetFocusedRowCellValue("Name"));
                var timeFrom = (DateTime)item.Properties.View.GetFocusedRowCellValue("HourFrom");
                var timeTo = (DateTime)item.Properties.View.GetFocusedRowCellValue("HourTo");
                var day = (DateTime)gv.GetFocusedRowCellValue("DateHandoverFrom");

                gv.SetFocusedRowCellValue("DateHandoverFrom", new DateTime(day.Year, day.Month, day.Day, timeFrom.Hour, timeFrom.Minute, 0));
                gv.SetFocusedRowCellValue("DateHandoverTo", new DateTime(day.Year, day.Month, day.Day, timeTo.Hour, timeTo.Minute, 0));

                gv.UpdateCurrentRow();
            }
            catch { }
        }
    }
}