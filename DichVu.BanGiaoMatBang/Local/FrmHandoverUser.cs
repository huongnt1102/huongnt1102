using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmHandoverUser : DevExpress.XtraEditors.XtraForm
    {
        public byte? BuildingId { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private int count;
        private int number;
        private string userName;

        public FrmHandoverUser()
        {
            InitializeComponent();
        }

        private void FrmHandoverLocalUser_Load(object sender, EventArgs e)
        {
            count = 0;
            number = 0;
            userName = "";
            glkUser.Properties.DataSource = _db.ho_UserHandovers;
        }

        private void CkIsChoose_CheckedChanged(object sender, EventArgs e)
        {
            var item = sender as CheckEdit;
            if (item.EditValue == null) return;

            if ((bool)item.EditValue == true) count++;
            else count--;

            if (count > number)
            {
                DialogBox.Error("Nhân viên này chỉ được " + number + " mặt bằng");
                item.EditValue = false;
                gv.SetFocusedRowCellValue("IsChoose", false);
                gv.UpdateCurrentRow();
                return;
            }
            gv.SetFocusedRowCellValue("IsChoose", (bool)item.EditValue);
            gv.UpdateCurrentRow();
        }

        private void GlkUser_EditValueChanged(object sender, EventArgs e)
        {
            var item = sender as GridLookUpEdit;
            if (item.EditValue == null) return;

            try
            {
                count = 0;
                number = int.Parse(item.Properties.View.GetFocusedRowCellValue("NumberLocal").ToString());
                txtNumber.Text = number + " Mặt bằng";
                userName = item.Properties.View.GetFocusedRowCellValue("UserName").ToString();
                
                // lấy ra hết danh sách mặt bằng mà nhân viên này đã chọn
                var list = _db.ho_ScheduleApartments
                    .Where(_ => _.BuildingId == BuildingId & _.IsChoose == true & _.StatusId == 2 & (_.UserId == null || _.UserId == (int)item.EditValue)).Select(_ => new { _.Id, _.UserId, _.ApartmentName, _.CustomerName, _.DateHandoverFrom, _.DateHandoverTo })
                    .AsEnumerable();
                var listChoose = list.Where(_ => _.UserId == (int)item.EditValue).AsEnumerable();
                var listUser = list.Select(_ =>
                    new ApartmentUser
                    {
                        IsChoose = listChoose.FirstOrDefault(j => j.Id == _.Id) != null,
                        Id = _.Id,
                        ApartmentName = _.ApartmentName,
                        CustomerName = _.CustomerName,
                        DateHandoverFrom = _.DateHandoverFrom,
                        DateHandoverTo = _.DateHandoverTo
                    }).ToList();

                gc.DataSource = listUser;

                if (listChoose != null) count = listChoose.ToList().Count;
            }
            catch{}
        }

        public class ApartmentUser
        {
            public bool? IsChoose { get; set; }

            public int? Id { get; set; }

            public string ApartmentName { get; set; }
            public string CustomerName { get; set; }

            public DateTime? DateHandoverFrom { get; set; }
            public DateTime? DateHandoverTo { get; set; }
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.UpdateCurrentRow();

                if (glkUser.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn nhân viên");
                    glkUser.Focus();
                    return;
                }

                _db.ho_ScheduleApartments
                    .Where(_ => _.BuildingId == BuildingId & _.IsChoose == true & _.StatusId == 2 &
                                (_.UserId == null || _.UserId == (int) glkUser.EditValue)).ToList().ForEach(_ =>
                    {
                        _.UserName = "";
                        _.UserId = null;
                        _.DateNumberNotification = 0;
                    });

                int countApartmentOfUser = 0;
                //string apartmentNames = "";
                for (var i = 0; i < gv.RowCount; i++)
                {
                    if (gv.GetRowCellValue(i, "IsChoose") == null) continue;

                    var isChoose = (bool?) gv.GetRowCellValue(i, "IsChoose");
                    if (isChoose == false) continue;

                    var id = int.Parse(gv.GetRowCellValue(i, "Id").ToString());
                    var o = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == id);
                    if (o == null) continue;

                    o.UserId = (int) glkUser.EditValue;
                    o.UserName = _db.ho_UserHandovers.First(_ => _.UserId == (int) glkUser.EditValue).UserName;
                    o.DateNumberNotification = spinNumberNotification.Value;

                    countApartmentOfUser++;
                    //apartmentNames = apartmentNames + o.ApartmentName + ", ";
                    SavePlanHistory(o);
                }

                SendNotifyToApp(countApartmentOfUser);

                _db.SubmitChanges();
                DialogBox.Success("Thiết lập nhân viên bàn giao thành công.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private void SendNotifyToApp(int? countApartmentOfUser)
        {
            //, apartmentNames.TrimEnd(' ').TrimEnd(',')
            var noiDung = string.Format("Bạn được {0} giao {1} căn bàn giao nội bộ. Bạn vui lòng kiểm tra.", Library.Common.User.HoTenNV, countApartmentOfUser);
            //var result = Building.AppVime.News.SendNotification.SendEmployee(DichVu.BanGiaoMatBang.Class.Const.TyleNotificationName.GUI_THONG_BAO_APP_NOI_BO_CHO_NHAN_VIEN, noiDung, (int)glkUser.EditValue);
            //if (result == true)
            //{
                SaveHistoryNotifycationStaff(noiDung);
            //}
        }

        private void SaveHistoryNotifycationStaff(string contents)
        {
            _db.ho_NotifycationStaffs.InsertOnSubmit(new Library.ho_NotifycationStaff
            {
                BuildingId = BuildingId, Contents = contents, DateCreate = System.DateTime.UtcNow.AddHours(7),
                IsLocal = true, StaffId = (int) glkUser.EditValue, StaffName = userName,
                Title = DichVu.BanGiaoMatBang.Class.Const.TyleNotificationName.GUI_THONG_BAO_APP_NOI_BO_CHO_NHAN_VIEN,
                TyleId = DichVu.BanGiaoMatBang.Class.Const.TyleNotifycationId.GUI_THONG_BAO_APP_NOI_BO_CHO_NHAN_VIEN_ID,
                UserCreateId = Library.Common.User.MaNV, UserCreateName = Library.Common.User.HoTenNV
            });
        }

        private void SavePlanHistory(Library.ho_ScheduleApartment scheduleApartment)
        {
            _db.ho_PlanHistories.InsertOnSubmit(new ho_PlanHistory
            {
                PlanId = scheduleApartment.PlanId,
                PlanName = scheduleApartment.PlanName,
                Content = "Thiết lập nhân viên bàn giao",
                DateHandoverFrom = scheduleApartment.DateHandoverFrom,
                DateHandoverTo = scheduleApartment.DateHandoverTo,
                DateCreate = DateTime.UtcNow.AddHours(7),
                UserCreate = Common.User.MaNV,
                UserCreateName = Common.User.HoTenNV,
                BuildingId = scheduleApartment.BuildingId,
                IsLocal = scheduleApartment.IsLocal,
                ScheduleId = scheduleApartment.ScheduleId,
                ScheduleName = scheduleApartment.ScheduleName,
                ScheduleApartmentId = scheduleApartment.Id,
                ApartmentId = scheduleApartment.ApartmentId,
                ApartmentName = scheduleApartment.ApartmentName
            });
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}