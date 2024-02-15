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
    public partial class FrmScheduleChange : DevExpress.XtraEditors.XtraForm
    {
        public int? ScheduleId { get; set; }
        public int? ScheduleApartment { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_Schedule _schedule;

        public FrmScheduleChange()
        {
            InitializeComponent();
        }

        private void FrmScheduleChange_Load(object sender, EventArgs e)
        {
            _schedule = _db.ho_Schedules.FirstOrDefault(_ => _.Id == ScheduleId);
            if (_schedule == null) return;

            chkChoose.Checked = true;
            txtApartment.Text = "";
            txtCustomer.Text = "";

            // mặt bằng đã có (đã có trong lịch khác, nhưng chưa bàn giao hoàn thành)
            glkApartment1.Properties.DataSource = _db.ho_ScheduleApartments.Where(_ => _.BuildingId == _schedule.BuildingId & _.IsChoose == true & _.ScheduleApartmentLocalId != null & _.StatusId >= 5 & _.StatusId < 8 & _.IsLocal == false).Select(_ => new {_.Id, _.ApartmentId, _.ApartmentName}).ToList();

            // mặt bằng mới (thõa điều kiện đã bàn giao nội bộ xong, và chưa có cái lịch nào chọn nó)
            glkApartment2.Properties.DataSource = _db.ho_ScheduleApartments.Where(_ => _.BuildingId == _schedule.BuildingId & _.IsChoose == true & _.StatusId == 4 & _.IsLocal == true).Select(_=>new{_.ApartmentId,_.ApartmentName,_.Id}).ToList();

            // khách hàng
            glkCustomer.Properties.DataSource = _db.tnKhachHangs.Where(_=>_.MaTN == _schedule.BuildingId).Select(_=>new{_.MaKH,Name = _.IsCaNhan==true?_.HoKH+" "+_.TenKH:_.CtyTen, _.KyHieu}).ToList();
        }

        private void ChkChoose_CheckedChanged(object sender, EventArgs e)
        {
            var item = sender as CheckEdit;
            if (item.EditValue == null) return;
            if ((bool)item.EditValue == true)
            {
                glkApartment1.Properties.ReadOnly = false;
                glkApartment2.Properties.ReadOnly = true;
                glkApartment2.EditValue = null;
            }
            else
            {
                glkApartment1.Properties.ReadOnly = true;
                glkApartment2.Properties.ReadOnly = false;
                glkApartment1.EditValue = null;
            }
        }

        private void GlkApartment1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;

                txtApartment.Text = item.Properties.View.GetFocusedRowCellValue("ApartmentName").ToString();
            }
            catch{}
        }

        private void GlkApartment2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;

                txtApartment.Text = item.Properties.View.GetFocusedRowCellValue("ApartmentName").ToString();
            }
            catch { }
        }

        private void GlkCustomer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;

                txtCustomer.Text = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch { }
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var scheduleApartment = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == ScheduleApartment);
                ho_ScheduleApartment apartmentChoose;
                if (scheduleApartment == null)
                {
                    DialogBox.Error("Chổ trống không tồn tại");
                    return;
                }

                if (chkChoose.Checked == true)
                {
                    if (glkApartment1.EditValue == null)
                    {
                        DialogBox.Error("Vui lòng chọn mặt bằng");
                        return;
                    }
                    apartmentChoose = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == (int)glkApartment1.EditValue);
                    
                }
                else
                {
                    if (glkApartment2.EditValue == null)
                    {
                        DialogBox.Error("Vui lòng chọn mặt bằng");
                        return;
                    }

                    apartmentChoose = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == (int)glkApartment2.EditValue);
                }

                // đoạn này đưa qua bên đề xuất luôn
                //if (glkCustomer.EditValue != null)
                //{
                //    scheduleApartment.CustomerId = (int)glkCustomer.EditValue;
                //    scheduleApartment.CustomerName = txtCustomer.Text;
                //}

                if (apartmentChoose != null)
                {

                    #region Tạo phiếu đề xuất

                    var deXuatDoiLich = new ho_DeXuatDoiLich();
                    deXuatDoiLich.ScheduleApartmentFromId = apartmentChoose.Id; // căn hộ
                    deXuatDoiLich.ScheduleApartmentToId = scheduleApartment.Id; // vị trí null

                    deXuatDoiLich.ApartmentFromId = apartmentChoose.ApartmentId;
                    deXuatDoiLich.ApartmentFromName = apartmentChoose.ApartmentName;

                    if (glkCustomer.EditValue != null)
                    {
                        deXuatDoiLich.CustomerId = (int)glkCustomer.EditValue;
                        deXuatDoiLich.CustomerName = txtCustomer.Text;
                    }

                    deXuatDoiLich.UserCreateId = Common.User.MaNV;
                    deXuatDoiLich.UserCreateName = Common.User.HoTenNV;
                    deXuatDoiLich.DateCreate = DateTime.Now;
                    deXuatDoiLich.BuildingId = scheduleApartment.BuildingId;
                    deXuatDoiLich.ScheduleToId = scheduleApartment.ScheduleId;
                    deXuatDoiLich.ScheduleToName = scheduleApartment.ScheduleName;
                    deXuatDoiLich.PlanToId = scheduleApartment.PlanId;
                    deXuatDoiLich.PlanToName = scheduleApartment.PlanName;
                    deXuatDoiLich.DateHandOverFrom = scheduleApartment.DateHandoverFrom;
                    deXuatDoiLich.DateHandOverTo = scheduleApartment.DateHandoverTo;
                    deXuatDoiLich.DutyId = scheduleApartment.DutyId;
                    deXuatDoiLich.DutyName = scheduleApartment.DutyName;
                    deXuatDoiLich.IsChange = chkChoose.Checked;
                    deXuatDoiLich.AllowStatusId = 1;
                    
                    _db.ho_DeXuatDoiLiches.InsertOnSubmit(deXuatDoiLich);

                    #endregion

                    #region Lưu mặt bằng chọn
                    //#region Lưu lịch sử

                    //var historyscheduleApartment = new ho_PlanHistory();
                    ////historyscheduleApartment.Content = "Chuyển từ mặt bằng: " + chkChoose.Checked == true ? apartmentChoose.ApartmentName + " sang " + txtApartment.Text :);
                    //historyscheduleApartment.Content = chkChoose.Checked == true ? ("Chuyển từ mặt bằng " + apartmentChoose.ApartmentName + " sang " + txtApartment.Text) : "Chuyển mặt bằng " + txtApartment.Text + " vào chổ trống";
                    //historyscheduleApartment.PlanId = scheduleApartment.PlanId;
                    //historyscheduleApartment.PlanName = scheduleApartment.PlanName;
                    //historyscheduleApartment.DateHandoverFrom = scheduleApartment.DateHandoverFrom;
                    //historyscheduleApartment.DateHandoverTo = scheduleApartment.DateHandoverTo;
                    //historyscheduleApartment.DateCreate = DateTime.UtcNow.AddHours(7);
                    //historyscheduleApartment.UserCreate = Common.User.MaNV;
                    //historyscheduleApartment.UserCreateName = Common.User.HoTenNV;
                    //historyscheduleApartment.BuildingId = scheduleApartment.BuildingId;
                    //historyscheduleApartment.IsLocal = scheduleApartment.IsLocal;
                    //historyscheduleApartment.ScheduleId = scheduleApartment.ScheduleId;
                    //historyscheduleApartment.ScheduleName = scheduleApartment.ScheduleName;
                    //historyscheduleApartment.ScheduleApartmentId = scheduleApartment.Id;
                    //historyscheduleApartment.ApartmentId = apartmentChoose.ApartmentId;
                    //historyscheduleApartment.ApartmentName = apartmentChoose.ApartmentName;

                    //if (glkCustomer.EditValue != null)
                    //{
                    //    historyscheduleApartment.CustomerId = (int?)glkCustomer.EditValue;
                    //    historyscheduleApartment.CustomerName = txtCustomer.Text;
                    //}
                    //_db.ho_PlanHistories.InsertOnSubmit(historyscheduleApartment);

                    //#endregion

                    //scheduleApartment.ApartmentId = apartmentChoose.ApartmentId;
                    //scheduleApartment.ApartmentName = txtApartment.Text;
                    //scheduleApartment.ScheduleApartmentLocalId = chkChoose.Checked == true ? apartmentChoose.ScheduleApartmentLocalId ?? 0 : apartmentChoose.Id;
                    //scheduleApartment.StatusId = 6;
                    //scheduleApartment.StatusName = "Chờ bàn giao khách hàng";

                    //// sau khi đổi thành công, phải chuyển mặt bằng cũ thành trạng thái 5 Đang bàn giao khách hàng
                    //apartmentChoose.StatusId = 5;
                    //apartmentChoose.StatusName = "Đang bàn giao khách hàng";
                    //if (chkChoose.Checked == true)
                    //{
                    //    // nếu chọn từ mặt bằng đã có, phải trả lại vị trí này = trống
                    //    apartmentChoose.ApartmentId = null;
                    //    apartmentChoose.ApartmentName = null;
                    //    apartmentChoose.CustomerId = null;
                    //    apartmentChoose.CustomerName = null;
                    //    apartmentChoose.StatusName = "Chờ xác nhận";
                    //    apartmentChoose.ScheduleApartmentLocalId = null;
                    //}

                    //// kiểm tra nếu có buildingChecklist thì phải tạo phiếu checklist, phiếu checklist cũ thì sao?
                    //// nếu có checklist cũ, thì không tạo mới. Nếu chưa có checklist, tức là đây là mặt bằng trống mới, thì mới tạo checklist, chổ này mình thấy chưa ổn lắm.
                    //if (scheduleApartment.ho_ScheduleApartmentCheckLists.Count <= 0)
                    //{
                    //    // tạo checklist mới
                    //    var buildingChecklist = _db.ho_BuildingChecklists.FirstOrDefault(_ => _.Id == scheduleApartment.BuildingChecklistId);
                    //    if (buildingChecklist != null)
                    //    {
                    //        if (buildingChecklist.ListChecklistId != null)
                    //        {
                    //            var listChecklist = _db.ho_Checklists.Where(_ => _.ListChecklistId == buildingChecklist.ListChecklistId & _.IsNotUse == false);
                    //            foreach (var checklist in listChecklist)
                    //            {
                    //                var apartmentChecklist = new ho_ScheduleApartmentCheckList();
                    //                apartmentChecklist.GroupName = checklist.GroupName;
                    //                apartmentChecklist.BuildingId = scheduleApartment.BuildingId;
                    //                apartmentChecklist.DateAction = scheduleApartment.DateHandoverFrom;
                    //                //apartmentChecklist.UserActionId = scheduleApartment.UserId;
                    //                //apartmentChecklist.UserActionName = scheduleApartment.UserName;
                    //                apartmentChecklist.UserCreate = Common.User.MaNV;
                    //                apartmentChecklist.UserCreateName = Common.User.HoTenNV;
                    //                apartmentChecklist.DateCreate = DateTime.UtcNow.AddHours(7);
                    //                apartmentChecklist.PlanId = scheduleApartment.PlanId;
                    //                apartmentChecklist.Name = checklist.Name;
                    //                apartmentChecklist.PlanName = scheduleApartment.PlanName;
                    //                apartmentChecklist.ScheduleName = scheduleApartment.ScheduleName;
                    //                apartmentChecklist.BuildingChecklistId = buildingChecklist.Id;
                    //                apartmentChecklist.BuildingChecklistName = buildingChecklist.ListChecklistName;
                    //                apartmentChecklist.IsLocal = false;
                    //                apartmentChecklist.Stt = checklist.Stt;
                    //                apartmentChecklist.ScheduleId = scheduleApartment.ScheduleId;
                    //                apartmentChecklist.Description = checklist.Description;
                    //                apartmentChecklist.IsNoQuality = false;
                    //                scheduleApartment.ho_ScheduleApartmentCheckLists.Add(apartmentChecklist);
                    //            }
                    //        }
                    //    }
                    //}

                    #endregion
                }

                

                scheduleApartment.DateUpdate = DateTime.UtcNow.AddHours(7);
                scheduleApartment.UserUpdateId = Common.User.MaNV;
                scheduleApartment.UserUpdateName = Common.User.HoTenNV;
                scheduleApartment.IsBooked = true;

                _db.SubmitChanges();
                DialogBox.Success("Lưu dữ liệu thành công.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                DialogBox.Error("Bị lỗi không lưu được");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}