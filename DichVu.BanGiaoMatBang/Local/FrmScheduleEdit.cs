using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmScheduleEdit : XtraForm
    {
        public int? Id { get; set; }
        public int? HandoverId { get; set; }
        public byte BuildingId { get; set; }
        public string PlanName { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_Schedule _schedule;
        private string _scheduleGroupName;
        private string buildingNo;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        public FrmScheduleEdit()
        {
            InitializeComponent();
        }

        private void FrmScheduleEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            itemClearText.ItemClick += ItemClearText_ItemClick;
            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;

            dateHandoverFrom.DateTime = DateTime.UtcNow.AddHours(7);
            dateHandoverTo.DateTime = DateTime.UtcNow.AddHours(7);
            txtScheduleName.Text = "";
            var building = _db.tnToaNhas.First(_ => _.MaTN == BuildingId);
            txtNo.Text = CreateScheduleNo(building.TenVT);
            buildingNo = building.TenVT;

            _schedule = new ho_Schedule();

            glkHandover.Properties.DataSource = _db.ho_Plans.Where(_ => _.BuildingId == BuildingId & _.IsAllow.GetValueOrDefault()!=true & _.IsLocal == true).Select(_ => new {_.Id, _.Name, _.DateHandOverFrom, _.DateHandOverTo}).ToList();
            if (HandoverId != null) glkHandover.EditValue = HandoverId;

            glkScheduleGroup.Properties.DataSource = _db.ho_ScheduleGroups;

            var listBuildingChecklist = _db.ho_BuildingChecklists.Where(_ => _.BuildingId == BuildingId & _.IsNotUse ==false)
                .Select(_ => new {_.Id, Name = _.ListChecklistName}).ToList();
            glkBuildingChecklist.DataSource = listBuildingChecklist;
            int buildingChecklistFirstId = 0;
            string buildingChecklistFirstName = "";
            if(listBuildingChecklist.Count>0)
            {
                buildingChecklistFirstId = listBuildingChecklist.First().Id;
                buildingChecklistFirstName = listBuildingChecklist.First().Name;
            }

            glkCustomer.DataSource = _db.tnKhachHangs.Where(_ => _.MaTN == BuildingId).Select(_ => new { Id = _.MaKH, Name = _.IsCaNhan == true ? (_.HoKH + " " + _.TenKH) : _.CtyTen, _.KyHieu, _.IsCaNhan }).ToList();
            glkUser.DataSource = _db.tnNhanViens.Select(_=>new{_.HoTenNV,_.MaNV,_.MaSoNV}).ToList();

            glkDuty.DataSource = _db.ho_Duties.Where(_ => _.BuildingId == BuildingId);

            // chổ này lấy lên danh sách mặt bằng chưa bàn giao khách hàng, vậy nếu đã bàn giao nội bộ thì sao?
            // có những trạng thái như sau, hoặc có thể nói là có những trường hợp như sau:
            // 1. mới tạo lịch bàn giao nội bộ, chưa duyệt
            // 2. đã duyệt, đang tiến hành bàn giao nội bộ // cái duyệt này là status ok của nội bộ mới duyệt
            // 3. đã bàn giao nội bộ ok, đang tiến hành bàn giao khách hàng
            // 4. đã bàn giao khách hàng ok, đóng
            // vậy đầu tiên ở đây lấy lên danh sách mặt bằng không có trong scheduleApartment luôn, tạo lịch rồi ah? vậy tạo rồi vào làm gì nữa??, duyệt rồi thì triển luôn, đã bàn giao nội bộ rồi thì bàn giao khách hàng đi, vào đây làm gì? và đã bàn giao khách hàng rồi thì thăng luôn thôi.
            // xíu mình sẽ xét trường hợp nếu khách hàng họ muốn chỉnh hoặc thêm lại sau

            // danh sách mặt bằng có trong
            var l1 = _db.ho_ScheduleApartments.Where(_ => _.BuildingId == BuildingId&_.IsChoose == true & _.ApartmentId!=null).Select(_=>new{_.ApartmentId}).AsEnumerable();

            // lọc ra danh sách mặt bằng có trong mặt bằng nhưng k có trong ScheduleApartments
            var ll2 = _db.mbMatBangs.Where(_ => l1.All(item => item.ApartmentId != _.MaMB) & _.DaGiaoChiaKhoa==false & _.MaTN == BuildingId).Select(_ =>
                new ApartmentSchedule
                {
                    ApartmentName = _.MaSoMB + " - " + _.mbTangLau.TenTL + " - " + _.mbTangLau.mbKhoiNha.TenKN,
                    ApartmentId = _.MaMB, BuildingChecklistName = buildingChecklistFirstName,
                    UserName = "", BuildingChecklistId = buildingChecklistFirstId,
                    DateNotification = 0, DateHandoverFrom = DateTime.UtcNow.AddHours(7),
                    DateHandoverTo = DateTime.UtcNow.AddHours(7),
                    DutyName = "",
                    CustomerId = _.MaKH,
                    CustomerName = _.MaKH!=null?(_.tnKhachHang.IsCaNhan == true?_.tnKhachHang.HoKH+" "+_.tnKhachHang.TenKH:_.tnKhachHang.CtyTen):""
                }).ToList();

            gcScheduleApartment.DataSource = ll2;

            if (Id != null)
            {
                _schedule = _db.ho_Schedules.FirstOrDefault(_ => _.Id == Id);
                if (_schedule != null)
                {
                    if (_schedule.DateHandoverFrom != null) dateHandoverFrom.DateTime = (DateTime) _schedule.DateHandoverFrom;
                    if (_schedule.DateHandoverTo != null) dateHandoverTo.DateTime = (DateTime) _schedule.DateHandoverTo;

                    txtScheduleName.Text = _schedule.Name;
                    
                    glkScheduleGroup.EditValue = _schedule.ScheduleGroupId;
                    glkHandover.EditValue = _schedule.PlanId;
                    _schedule.DateUpdate = DateTime.UtcNow.AddHours(7);
                    _schedule.UserUpdate = Common.User.MaNV;
                    _schedule.UserUpdateName = Common.User.HoTenNV;
                    
                    _scheduleGroupName = _schedule.ScheduleGroupName;
                    PlanName = _schedule.PlanName;
                    txtNo.Text = _schedule.No ?? CreateScheduleNo(building.TenVT);

                    var apartmentAll = _db.ho_ScheduleApartments.Where(_ => _.BuildingId == BuildingId & _.ScheduleId != _schedule.Id & _.IsChoose == true & _.ApartmentId != null).Select(_ => new { _.ApartmentId }).AsEnumerable();
                    var apartmentChoose = _db.ho_ScheduleApartments.Where(_ => _.ScheduleId == _schedule.Id & _.ApartmentId != null).Select(_ =>
                        new
                        {
                            _.ApartmentId, _.Id, _.CustomerId, _.CustomerName, _.BuildingChecklistId,
                            _.BuildingChecklistName, _.UserId, _.UserName, _.DateHandoverFrom, _.DateHandoverTo,
                            _.DateNumberNotification, _.IsChoose,_.DutyId,_.DutyName
                        }).AsEnumerable();
                    
                    var ll3 = _db.mbMatBangs.Where(_ => apartmentAll.All(item => item.ApartmentId != _.MaMB) & _.DaGiaoChiaKhoa==false & _.MaTN == BuildingId).Select(_ =>
                        new ApartmentSchedule
                        {
                            ApartmentName = _.MaSoMB + " - " + _.mbTangLau.TenTL + " - " + _.mbTangLau.mbKhoiNha.TenKN,
                            IsChoose = apartmentChoose.FirstOrDefault(item=>item.ApartmentId == _.MaMB)!=null?apartmentChoose.First(item=>item.ApartmentId == _.MaMB).IsChoose:null,
                            Id = apartmentChoose.FirstOrDefault(item=>item.ApartmentId == _.MaMB)!=null?apartmentChoose.First(item=>item.ApartmentId == _.MaMB).Id:(int?) null,
                            ApartmentId = _.MaMB,
                            //CustomerId = ll4.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? ll4.First(item => item.ApartmentId == _.MaMB).CustomerId : null,
                            //CustomerName = ll4.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? ll4.First(item => item.ApartmentId == _.MaMB).CustomerName : "",
                            CustomerId = _.MaKH,
                            CustomerName = _.MaKH!=null?(_.tnKhachHang.IsCaNhan == true?_.tnKhachHang.HoKH+" "+_.tnKhachHang.TenKH:_.tnKhachHang.CtyTen):"",
                            BuildingChecklistId = apartmentChoose.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? apartmentChoose.First(item => item.ApartmentId == _.MaMB).BuildingChecklistId : buildingChecklistFirstId,
                            BuildingChecklistName = apartmentChoose.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? apartmentChoose.First(item => item.ApartmentId == _.MaMB).BuildingChecklistName:buildingChecklistFirstName,
                            UserId = apartmentChoose.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? apartmentChoose.First(item => item.ApartmentId == _.MaMB).UserId : null,
                            UserName = apartmentChoose.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? apartmentChoose.First(item => item.ApartmentId == _.MaMB).UserName:"",
                            DateHandoverFrom = apartmentChoose.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? apartmentChoose.First(item => item.ApartmentId == _.MaMB).DateHandoverFrom : DateTime.UtcNow.AddHours(7),
                            DateHandoverTo = apartmentChoose.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? apartmentChoose.First(item => item.ApartmentId == _.MaMB).DateHandoverTo:DateTime.UtcNow.AddHours(7),
                            DateNotification = apartmentChoose.FirstOrDefault(item => item.ApartmentId == _.MaMB) != null ? apartmentChoose.First(item => item.ApartmentId == _.MaMB).DateNumberNotification:(decimal)0,
                            DutyId = apartmentChoose.FirstOrDefault(item=>item.ApartmentId == _.MaMB)!=null?apartmentChoose.First(item=>item.ApartmentId == _.MaMB).DutyId:null,
                            DutyName = apartmentChoose.FirstOrDefault(item=>item.ApartmentId == _.MaMB)!=null?apartmentChoose.First(item=>item.ApartmentId==_.MaMB).DutyName:""
                        }).ToList();

                    gcScheduleApartment.DataSource = ll3;
                }
                else
                {
                    _schedule = new ho_Schedule();
                    _schedule.BuildingId = BuildingId;
                    _schedule.UserCreate = Common.User.MaNV;
                    _schedule.UserCreateName = Common.User.HoTenNV;
                    _schedule.DateCreate = DateTime.UtcNow.AddHours(7);
                }
            }
            else
            {
                _schedule.BuildingId = BuildingId;
                _schedule.UserCreate = Common.User.MaNV;
                _schedule.UserCreateName = Common.User.HoTenNV;
                _schedule.DateCreate = DateTime.UtcNow.AddHours(7);
            }

        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var no = txtNo.Text;
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtNo.Text = no;
        }

        private string CreateScheduleNo(string buildingNo)
        {
            if (buildingNo == null) return "";
            //var db = new MasterDataContext();

            string temp = "";
            string stt;

            temp = buildingNo + ".LNB.";
            var param = new Dapper.DynamicParameters();
            var result = Library.Class.Connect.QueryConnect.Query<string>("dbo.ho_schedule_getnewno", param);
            stt = result.First();
            //var obj = (from _ in _db.ho_Schedules
            //    where _.BuildingId == BuildingId
            //    orderby _.No.Substring(_.No.IndexOf('.') + 5) descending
            //    select new
            //    {
            //        Stt = _.No.Substring(_.No.IndexOf('.') + 5)
            //    }).FirstOrDefault();
            //if (obj == null || (obj != null & obj.Stt == null))
            //{
            //    stt = "0001";
            //}
            //else stt = (int.Parse(obj.Stt) + 1).ToString().PadLeft(4, '0');

            temp = temp + stt;
            return temp;
        }

        private string CreateScheduleApartmentNo(string buildingNo)
        {
            if (buildingNo == null) return "";
            //var db = new MasterDataContext();

            string temp = "";
            string stt;

            temp = buildingNo + ".BGNB.";
            var param = new Dapper.DynamicParameters();
            var result = Library.Class.Connect.QueryConnect.Query<string>("dbo.ho_schedule_apartment_getnewno", param);
            stt = result.First();
            //var obj = (from _ in _db.ho_ScheduleApartments
            //    where _.BuildingId == BuildingId
            //    orderby _.No.Substring(_.No.IndexOf('.') + 6) descending
            //    select new
            //    {
            //        Stt = _.No.Substring(_.No.IndexOf('.') + 6)
            //    }).FirstOrDefault();
            //if (obj == null || (obj != null & obj.Stt == null))
            //{
            //    stt = "0001";
            //}
            //else stt = (int.Parse(obj.Stt) + 1).ToString().PadLeft(4, '0');

            temp = temp + stt;
            return temp;
        }

        public class ApartmentSchedule
        {
            public int? Id { get; set; }
            public bool? IsChoose { get; set; }
            public int? ApartmentId { get; set; }
            public int? CustomerId { get; set; }
            public int? BuildingChecklistId { get; set; }
            public int? UserId { get; set; }
            public int? DutyId { get; set; }

            public string ApartmentName { get; set; }
            public string CustomerName { get; set; }
            public string BuildingChecklistName { get; set; }
            public string UserName { get; set; }
            public string DutyName { get; set; }

            public DateTime? DateHandoverFrom { get; set; }
            public DateTime? DateHandoverTo { get; set; }

            public decimal? DateNotification { get; set; }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gvScheduleApartment.PostEditor();
                gvScheduleApartment.UpdateCurrentRow();

                #region Kiểm tra

                if (glkScheduleGroup.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn Nhóm Lịch");
                    glkScheduleGroup.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(glkHandover.EditValue.ToString())==true)
                {
                    DialogBox.Error("Vui lòng chọn kế hoạch bàn giao");
                    glkHandover.Focus();
                    return;
                }

                #endregion
                
                _schedule.Name = txtScheduleName.Text;
                _schedule.PlanId = (int) glkHandover.EditValue;
                _schedule.PlanName = PlanName;
                _schedule.DateHandoverFrom = dateHandoverFrom.DateTime;
                _schedule.DateHandoverTo = dateHandoverTo.DateTime;
                _schedule.IsLocal = true;
                _schedule.ScheduleGroupId = (int) glkScheduleGroup.EditValue;
                _schedule.ScheduleGroupName = _scheduleGroupName;
                _schedule.No = txtNo.Text;

                if (Id == null) _db.ho_Schedules.InsertOnSubmit(_schedule);
                _db.SubmitChanges();

                #region Lưu schedule apartment

                for (var i = 0; i < gvScheduleApartment.RowCount; i++)
                {
                    if (gvScheduleApartment.GetRowCellValue(i, "IsChoose") == null) continue;
                    if (gvScheduleApartment.GetRowCellValue(i, "ApartmentId") == null) continue;
                    var id = (int?) gvScheduleApartment.GetRowCellValue(i, "Id");
                    var scheduleApartment = new ho_ScheduleApartment();

                    if (id != 0)
                    {
                        scheduleApartment = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == id)??new ho_ScheduleApartment();
                    }

                    scheduleApartment.ApartmentId = (int?) gvScheduleApartment.GetRowCellValue(i, "ApartmentId");
                    scheduleApartment.ApartmentName = gvScheduleApartment.GetRowCellValue(i, "ApartmentName").ToString();
                    //scheduleApartment.CustomerId = (int?) gvScheduleApartment.GetRowCellValue(i, "CustomerId");
                    //scheduleApartment.CustomerName = gvScheduleApartment.GetRowCellValue(i, "CustomerName").ToString();
                    scheduleApartment.ScheduleName = txtScheduleName.Text;
                    scheduleApartment.PlanId = (int?) glkHandover.EditValue;
                    scheduleApartment.PlanName = PlanName;
                    scheduleApartment.BuildingChecklistId = (int?) gvScheduleApartment.GetRowCellValue(i, "BuildingChecklistId");
                    scheduleApartment.BuildingChecklistName = gvScheduleApartment.GetRowCellValue(i, "BuildingChecklistName").ToString();
                    scheduleApartment.BuildingId = BuildingId;
                    scheduleApartment.UserCreateId = Common.User.MaNV;
                    scheduleApartment.UserCreateName = Common.User.HoTenNV;
                    scheduleApartment.DateCreate = DateTime.UtcNow.AddHours(7);
                    scheduleApartment.DateHandoverFrom = (DateTime?) gvScheduleApartment.GetRowCellValue(i, "DateHandoverFrom");
                    scheduleApartment.DateHandoverTo = (DateTime?) gvScheduleApartment.GetRowCellValue(i, "DateHandoverTo");
                    scheduleApartment.UserId = (int?) gvScheduleApartment.GetRowCellValue(i, "UserId");
                    scheduleApartment.UserName = gvScheduleApartment.GetRowCellValue(i, "UserName").ToString();
                    scheduleApartment.DateNumberNotification = (decimal?) gvScheduleApartment.GetRowCellValue(i, "DateNotification");
                    scheduleApartment.StatusId = 1;
                    scheduleApartment.StatusName = "Chờ xác nhận";//"Mới tạo lịch bàn giao nội bộ, chưa duyệt";
                    scheduleApartment.IsChoose = (bool?) gvScheduleApartment.GetRowCellValue(i, "IsChoose");
                    scheduleApartment.DutyId = (int?)gvScheduleApartment.GetRowCellValue(i, "DutyId");
                    scheduleApartment.DutyName = gvScheduleApartment.GetRowCellValue(i, "DutyName").ToString();
                    scheduleApartment.IsLocal = true;
                    scheduleApartment.No = CreateScheduleApartmentNo(buildingNo);
                    scheduleApartment.CustomerId = (int?)gvScheduleApartment.GetRowCellValue(i, "CustomerId");
                    scheduleApartment.CustomerName = gvScheduleApartment.GetRowCellValue(i, "CustomerName")!=null? gvScheduleApartment.GetRowCellValue(i, "CustomerName").ToString():"";
                    //var testNo = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.No == scheduleApartment.No);
                    //if (testNo != null) scheduleApartment.No = CreateScheduleApartmentNo(buildingNo);

                    _schedule.ho_ScheduleApartments.Add(scheduleApartment);
                    _db.SubmitChanges();
                }

                #endregion

                #region Lưu lịch sử

                // lịch sử bàn giao chỉ dành cho status bàn giao khách hàng thôi
                var history = new ho_PlanHistory();
                history.BuildingId = _schedule.BuildingId;
                history.Content = "Cập nhật lịch bàn giao: "+_schedule.Name;
                history.DateCreate = DateTime.UtcNow.AddHours(7);
                history.DateHandoverFrom = _schedule.DateHandoverFrom;
                history.DateHandoverTo = _schedule.DateHandoverTo;
                history.IsLocal = true;
                history.PlanId = _schedule.PlanId;
                history.PlanName = _schedule.PlanName;
                history.UserCreate = Common.User.MaNV;
                history.UserCreateName = Common.User.HoTenNV;
                history.ScheduleName = _schedule.Name;
                _db.ho_PlanHistories.InsertOnSubmit(history);
                #endregion

                history.ScheduleId = _schedule.Id;

                _db.ho_ScheduleApartments.DeleteAllOnSubmit(_db.ho_ScheduleApartments.Where(_ => _.IsChoose == false));
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

        private void ItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(gvScheduleNotification.GetFocusedDataSourceRowIndex().ToString()))
                //{
                //    gvScheduleNotification.DeleteSelectedRows();
                //}
            }
            catch (Exception)
            {
                //throw;
            }
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

                gvScheduleApartment.SetFocusedRowCellValue("CustomerId", item.EditValue);
                gvScheduleApartment.SetFocusedRowCellValue("CustomerName",
                    item.Properties.View.GetFocusedRowCellValue("Name"));
                gvScheduleApartment.UpdateCurrentRow();

            }
            catch { }
        }

        //private void GvScheduleNotification_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    //try
        //    //{
        //    //    if (e.Column.FieldName == "UserNotification" & e.Column.FieldName == "NoticeTime")
        //    //    {
        //    //        gvScheduleNotification.SetFocusedRowCellValue("ScheduleName", txtScheduleName.Text);
        //    //    }
        //    //    var id = gvScheduleNotification.GetFocusedRowCellValue("Id");
        //    //    if (id == null | (int?)id == 0) return;
        //    //    if (e.Column.FieldName == "UserNotification" & e.Column.FieldName == "NoticeTime")
        //    //    {
        //    //        gvScheduleNotification.SetFocusedRowCellValue("UserUpdate", Common.User.MaNV);
        //    //        gvScheduleNotification.SetFocusedRowCellValue("DateUpdate", DateTime.UtcNow.AddHours(7));
        //    //        gvScheduleNotification.SetFocusedRowCellValue("ScheduleName", txtScheduleName.Text);
        //    //        gvScheduleNotification.SetFocusedRowCellValue("UserUpdateName", Common.User.MaNV);
        //    //    }
        //    //}
        //    //catch{}
        //}

        private void GlkHandover_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn kế hoạch.");
                    return;
                }
                if (item.Properties.View.GetFocusedRowCellValue("Name") == null) return;
                PlanName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void GlkScheduleGroup_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    DialogBox.Alert("Vui lòng chọn nhóm lịch.");
                    return;
                }
                if (item.Properties.View.GetFocusedRowCellValue("Name") == null) return;
                _scheduleGroupName = item.Properties.View.GetFocusedRowCellValue("Name").ToString();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void GlkBuildingChecklist_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }

                gvScheduleApartment.SetFocusedRowCellValue("BuildingChecklistId", item.EditValue);
                gvScheduleApartment.SetFocusedRowCellValue("BuildingChecklistName",
                    item.Properties.View.GetFocusedRowCellValue("Name"));
                gvScheduleApartment.UpdateCurrentRow();

            }
            catch { }
        }

        private void GlkUser_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null)
                {
                    return;
                }

                gvScheduleApartment.SetFocusedRowCellValue("UserId", item.EditValue);
                gvScheduleApartment.SetFocusedRowCellValue("UserName",
                    item.Properties.View.GetFocusedRowCellValue("HoTenNV"));
                gvScheduleApartment.UpdateCurrentRow();

            }
            catch { }
        }

        private void GlkDuty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;
                gvScheduleApartment.SetFocusedRowCellValue("DutyId", item.EditValue);
                gvScheduleApartment.SetFocusedRowCellValue("DutyName", item.Properties.View.GetFocusedRowCellValue("Name"));

                var timeFrom = (DateTime) item.Properties.View.GetFocusedRowCellValue("HourFrom");
                var timeTo = (DateTime)item.Properties.View.GetFocusedRowCellValue("HourTo");
                var day = (DateTime) gvScheduleApartment.GetFocusedRowCellValue("DateHandoverFrom");

                //var newTimeFrom = new DateTime(day.Year, day.Month, day.Day, timeFrom.Hour, timeFrom.Minute, 0);
                gvScheduleApartment.SetFocusedRowCellValue("DateHandoverFrom", new DateTime(day.Year, day.Month, day.Day, timeFrom.Hour, timeFrom.Minute, 0));
                gvScheduleApartment.SetFocusedRowCellValue("DateHandoverTo", new DateTime(day.Year, day.Month, day.Day, timeTo.Hour, timeTo.Minute, 0));

                gvScheduleApartment.UpdateCurrentRow();
            }
            catch { }
        }

        private void DateHandoverFrom_EditValueChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < gvScheduleApartment.RowCount; i++)
            {
                var timeFrom = (DateTime) gvScheduleApartment.GetRowCellValue(i, "DateHandoverFrom");
                var value = dateHandoverFrom.DateTime;
                var timeTo = (DateTime)gvScheduleApartment.GetRowCellValue(i, "DateHandoverTo");
                gvScheduleApartment.SetRowCellValue(i,"DateHandoverFrom",new DateTime(value.Year,value.Month, value.Day,timeFrom.Hour, timeFrom.Minute, timeFrom.Second));
                gvScheduleApartment.SetRowCellValue(i, "DateHandoveTo", new DateTime(value.Year, value.Month, value.Day, timeTo.Hour, timeTo.Minute, timeTo.Second));
            }
        }
    }
}