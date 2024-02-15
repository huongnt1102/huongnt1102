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
    public partial class FrmScheduleEdit : XtraForm
    {
        public int? Id { get; set; }
        public int? PlanId { get; set; }
        public byte BuildingId { get; set; }
        public string PlanName {get;set;}

        private MasterDataContext _db = new MasterDataContext();
        private ho_Schedule _schedule;
        private string _scheduleGroupName;
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

            _schedule = new ho_Schedule();

            glkPlan.Properties.DataSource = _db.ho_Plans.Where(_ => _.BuildingId == BuildingId & _.IsAllow.GetValueOrDefault()!=true & _.IsLocal == false).Select(_ => new {_.Id, _.Name, _.DateHandOverFrom, _.DateHandOverTo}).ToList();
            if (PlanId != null) glkPlan.EditValue = PlanId;

            glkScheduleGroup.Properties.DataSource = _db.ho_ScheduleGroups;

            var listBuildingChecklist = _db.ho_BuildingChecklists.Where(_ => _.BuildingId == BuildingId & _.IsNotUse == false)
                .Select(_ => new {_.Id, Name = _.ListChecklistName}).ToList();
            glkBuildingChecklist.DataSource = listBuildingChecklist;

            glkCustomer.DataSource = _db.tnKhachHangs.Where(_ => _.MaTN == BuildingId).Select(_ => new { Id = _.MaKH, Name = _.IsCaNhan == true ? (_.HoKH + " " + _.TenKH) : _.CtyTen, _.KyHieu, _.IsCaNhan }).ToList();
            glkUser.DataSource = _db.tnNhanViens.Select(_=>new{_.HoTenNV,_.MaNV,_.MaSoNV}).ToList();

            glkDuty.DataSource = _db.ho_Duties.Where(_ => _.BuildingId == BuildingId).ToList();

            // chổ này lấy lên danh sách mặt bằng chưa bàn giao khách hàng, vậy nếu đã bàn giao nội bộ thì sao?
            // có những trạng thái như sau, hoặc có thể nói là có những trường hợp như sau:
            // 1. mới tạo lịch bàn giao nội bộ, chưa duyệt
            // 2. đã duyệt, đang tiến hành bàn giao nội bộ // cái duyệt này là status ok của nội bộ mới duyệt
            // 3. đã bàn giao nội bộ ok, đang tiến hành bàn giao khách hàng
            // 4. đã bàn giao khách hàng ok, đóng
            // vậy đầu tiên ở đây lấy lên danh sách mặt bằng không có trong scheduleApartment luôn, tạo lịch rồi ah? vậy tạo rồi vào làm gì nữa??, duyệt rồi thì triển luôn, đã bàn giao nội bộ rồi thì bàn giao khách hàng đi, vào đây làm gì? và đã bàn giao khách hàng rồi thì thăng luôn thôi.
            // xíu mình sẽ xét trường hợp nếu khách hàng họ muốn chỉnh hoặc thêm lại sau

            // danh sách mặt bằng có trong

            
            var l1 = _db.ho_ScheduleApartments
                .Where(_ => _.BuildingId == BuildingId & _.IsChoose == true & _.StatusId == 4)
                .Select(_ => new ApartmentSchedule
                {
                    ApartmentId = _.ApartmentId, ApartmentName = _.ApartmentName,
                    CustomerName = _.CustomerName != null ? _.CustomerName : _db.mbMatBangs.First(mb => mb.MaMB == _.ApartmentId).MaKH != null ? (_db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.IsCaNhan == true ? (_db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.HoKH + " " + _db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.TenKH) : _db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.CtyTen) : null,
                    CustomerId = (_.CustomerId!=null?_.CustomerId:_db.mbMatBangs.First(kh=>kh.MaMB==_.ApartmentId).MaKH!=null?_db.mbMatBangs.First(kh=>kh.MaMB==_.ApartmentId).MaKH:null), 
                    
                    BuildingChecklistName = _.BuildingChecklistName,
                    BuildingChecklistId = _.BuildingChecklistId, DateHandoverFrom = DateTime.UtcNow.AddHours(7),
                    DateHandoverTo = DateTime.UtcNow.AddHours(7), DateNotification = _.DateNumberNotification, Id = 0,
                    UserName = "", ScheduleApartmentLocal = _.Id, DutyName = ""
                })
                .ToList();

            gcScheduleApartment.DataSource =l1;

            if (Id != null)
            {
                _schedule = _db.ho_Schedules.FirstOrDefault(_ => _.Id == Id);
                if (_schedule != null)
                {
                    if (_schedule.DateHandoverFrom != null) dateHandoverFrom.DateTime = (DateTime)_schedule.DateHandoverFrom;
                    if (_schedule.DateHandoverTo != null) dateHandoverTo.DateTime = (DateTime)_schedule.DateHandoverTo;

                    txtScheduleName.Text = _schedule.Name;
                    
                    glkScheduleGroup.EditValue = _schedule.ScheduleGroupId;
                    glkPlan.EditValue = _schedule.PlanId;
                    _schedule.DateUpdate = DateTime.UtcNow.AddHours(7);
                    _schedule.UserUpdate = Common.User.MaNV;
                    _schedule.UserUpdateName = Common.User.HoTenNV;
                    
                    _scheduleGroupName = _schedule.ScheduleGroupName;
                    PlanName = _schedule.PlanName;
                    //_.BuildingId == BuildingId &
                    var apartmentAll = _db.ho_ScheduleApartments.Where(_ => _.BuildingId == BuildingId & _.IsChoose == true & (_.StatusId == 4 || (_.StatusId == 5 & _.ScheduleApartmentLocalId != null))).Select(_ => new { _.Id, _.ScheduleId, _.ApartmentName, _.ApartmentId, _.CustomerId, _.CustomerName, _.BuildingChecklistId, _.BuildingChecklistName, _.UserId, _.UserName, _.DateHandoverFrom, _.DateHandoverTo, _.DateNumberNotification, _.ScheduleApartmentLocalId, _.IsChoose, _.DutyId, _.DutyName }).AsEnumerable();
                    var apartmentChoose = apartmentAll.Where(_ => _.ScheduleId == _schedule.Id).Select(_ => new { _.Id,_.DateHandoverFrom,_.DateHandoverTo,_.DateNumberNotification,_.ScheduleApartmentLocalId,_.IsChoose,_.DutyId,_.DutyName }).ToList();

                    //danh sách này có cả id mb của phiếu
                    //var apartmentNews = apartmentAll.Select(_ =>
                    //    new ApartmentSchedule
                    //    {
                    //        ApartmentName = _.ApartmentName,
                    //        IsChoose = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? apartmentChoose.First(item => item.Id == _.Id).IsChoose : null,
                    //        Id = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? apartmentChoose.First(item => item.Id == _.Id).Id : 0,
                    //        ApartmentId = _.ApartmentId,
                    //        CustomerId = (_.CustomerId != null ? _.CustomerId : _db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).MaKH != null ? _db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).MaKH : null),
                    //        CustomerName = _.CustomerName != null ? _.CustomerName : _db.mbMatBangs.First(mb => mb.MaMB == _.ApartmentId).MaKH != null ? (_db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.IsCaNhan == true ? (_db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.HoKH + " " + _db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.TenKH) : _db.mbMatBangs.First(kh => kh.MaMB == _.ApartmentId).tnKhachHang.CtyTen) : null,
                    //        CustomerId = _.CustomerId,
                    //        CustomerName = _.CustomerName,
                    //        BuildingChecklistId = _.BuildingChecklistId,
                    //        BuildingChecklistName = _.BuildingChecklistName,
                    //        UserId = null,
                    //        UserName = "",
                    //        DateHandoverFrom = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? apartmentChoose.First(item => item.Id == _.Id).DateHandoverFrom : DateTime.UtcNow.AddHours(7),
                    //        DateHandoverTo = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? apartmentChoose.First(item => item.Id == _.Id).DateHandoverTo : DateTime.UtcNow.AddHours(7),
                    //        DateNotification = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? apartmentChoose.First(item => item.Id == _.Id).DateNumberNotification : (decimal)0,
                    //        ScheduleApartmentLocal = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? _.ScheduleApartmentLocalId : (int?)_.Id,
                    //        DutyId = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? apartmentChoose.First(item => item.Id == _.Id).DutyId : null,
                    //        DutyName = apartmentChoose.FirstOrDefault(item => item.Id == _.Id) != null ? apartmentChoose.First(item => item.Id == _.Id).DutyName : "",
                    //    }).ToList();

                    //List<ho_ScheduleApartment> b = Enumerable.Range(1, 3).Chunk(2).Reverse().ToArray();

                    var mbMatbang = (from p in _db.mbMatBangs
                        where p.MaTN == BuildingId
                        select new
                        {
                            p.MaMB,
                            HoTen = p.tnKhachHang.IsCaNhan == true ? p.tnKhachHang.HoKH + " " + p.tnKhachHang.TenKH : p.tnKhachHang.CtyTen,
                            p.MaKH
                        }).ToList();

                    var listSub = Common.GetEnumerableOfEnumerables(apartmentAll, 200);

                    List<ApartmentSchedule> apartmentNews = new List<ApartmentSchedule>();

                    List<Task> tasks = new List<Task>();

                    foreach (var i in listSub)
                    {
                        tasks.Add(Task.Factory.StartNew( () => {
                            foreach (var apartment in i)
                            {
                                var itemApartment = new ApartmentSchedule();
                                itemApartment.ApartmentName = apartment.ApartmentName;
                                itemApartment.IsChoose = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).IsChoose : null;
                                itemApartment.Id = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).Id : 0;
                                itemApartment.ApartmentId = apartment.ApartmentId;
                                //itemApartment.CustomerId = (apartment.CustomerId != null ? apartment.CustomerId : _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).MaKH != null ? _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).MaKH : null);
                                //itemApartment.CustomerName = apartment.CustomerName != null ? apartment.CustomerName : _db.mbMatBangs.First(mb => mb.MaMB == apartment.ApartmentId).MaKH != null ? (_db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.IsCaNhan == true ? (_db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.HoKH + " " + _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.TenKH) : _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.CtyTen) : null;
                                //itemApartment.CustomerId = apartment.CustomerId;
                                //itemApartment.CustomerName = apartment.CustomerName;
                                itemApartment.CustomerId = (apartment.CustomerId != null ? apartment.CustomerId : mbMatbang.First(kh => kh.MaMB == apartment.ApartmentId).MaKH != null ? mbMatbang.First(kh => kh.MaMB == apartment.ApartmentId).MaKH : null);
                                itemApartment.CustomerName = apartment.CustomerName != null ? apartment.CustomerName : mbMatbang.First(kh => kh.MaMB == apartment.ApartmentId).HoTen != null ? mbMatbang.First(kh => kh.MaMB == apartment.ApartmentId).HoTen : null;
                                itemApartment.BuildingChecklistId = apartment.BuildingChecklistId;
                                itemApartment.BuildingChecklistName = apartment.BuildingChecklistName;
                                itemApartment.UserId = null;
                                itemApartment.UserName = "";
                                itemApartment.DateHandoverFrom = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DateHandoverFrom : DateTime.UtcNow.AddHours(7);
                                itemApartment.DateHandoverTo = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DateHandoverTo : DateTime.UtcNow.AddHours(7);
                                itemApartment.DateNotification = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DateNumberNotification : (decimal)0;
                                itemApartment.ScheduleApartmentLocal = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartment.ScheduleApartmentLocalId : (int?)apartment.Id;
                                itemApartment.DutyId = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DutyId : null;
                                itemApartment.DutyName = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DutyName : "";

                                apartmentNews.Add(itemApartment);
                            }
                        }));
                    }
                    Task.WaitAll(tasks.ToArray()); 

                    
                    //foreach (var apartment in apartmentAll)
                    //{
                    //    var itemApartment = new ApartmentSchedule();
                    //    itemApartment.ApartmentName = apartment.ApartmentName;
                    //    itemApartment.IsChoose = apartmentChoose.FirstOrDefault(item=>item.Id == apartment.Id)!=null?apartmentChoose.First(item=>item.Id == apartment.Id).IsChoose:null;
                    //    itemApartment.Id = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null?apartmentChoose.First(item=>item.Id == apartment.Id).Id:0;
                    //    itemApartment.ApartmentId = apartment.ApartmentId;
                    //    itemApartment.CustomerId = (apartment.CustomerId != null ? apartment.CustomerId : _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).MaKH != null ? _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).MaKH : null);
                    //    itemApartment.CustomerName = apartment.CustomerName != null ? apartment.CustomerName : _db.mbMatBangs.First(mb => mb.MaMB == apartment.ApartmentId).MaKH != null ? (_db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.IsCaNhan == true ? (_db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.HoKH + " " + _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.TenKH) : _db.mbMatBangs.First(kh => kh.MaMB == apartment.ApartmentId).tnKhachHang.CtyTen) : null;
                    //    itemApartment.CustomerId = apartment.CustomerId;
                    //    itemApartment.CustomerName = apartment.CustomerName;
                    //    itemApartment.BuildingChecklistId = apartment.BuildingChecklistId;
                    //    itemApartment.BuildingChecklistName = apartment.BuildingChecklistName;
                    //    itemApartment.UserId = null;
                    //    itemApartment.UserName = "";
                    //    itemApartment.DateHandoverFrom = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DateHandoverFrom : DateTime.UtcNow.AddHours(7);
                    //    itemApartment.DateHandoverTo = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DateHandoverTo : DateTime.UtcNow.AddHours(7);
                    //    itemApartment.DateNotification = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DateNumberNotification : (decimal)0;
                    //    itemApartment.ScheduleApartmentLocal = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartment.ScheduleApartmentLocalId :(int?) apartment.Id;
                    //    itemApartment.DutyId = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DutyId:null;
                    //    itemApartment.DutyName = apartmentChoose.FirstOrDefault(item => item.Id == apartment.Id) != null ? apartmentChoose.First(item => item.Id == apartment.Id).DutyName : "";

                    //    apartmentNews.Add(itemApartment);
                    //}



                    gcScheduleApartment.DataSource = apartmentNews;
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
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
        }

        public class ApartmentSchedule
        {
            public int? Id { get; set; }
            public int? ScheduleApartmentLocal { get; set; }
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

                if (string.IsNullOrEmpty(glkPlan.EditValue.ToString())==true)
                {
                    DialogBox.Error("Vui lòng chọn kế hoạch bàn giao");
                    glkPlan.Focus();
                    return;
                }

                for (var i = 0; i < gvScheduleApartment.RowCount; i++)
                {
                    if (gvScheduleApartment.GetRowCellValue(i, "IsChoose") == null) continue;
                    var maKh = (int?)gvScheduleApartment.GetRowCellValue(i, "CustomerId");
                    if (maKh == null & (bool)gvScheduleApartment.GetRowCellValue(i, "IsChoose") == true)
                    {
                        DialogBox.Error("Vui lòng chọn khách hàng");
                        return;
                    }
                }

                #endregion
                
                _schedule.Name = txtScheduleName.Text;
                _schedule.PlanId = (int) glkPlan.EditValue;
                _schedule.PlanName = PlanName;
                _schedule.DateHandoverFrom = dateHandoverFrom.DateTime;
                _schedule.DateHandoverTo = dateHandoverTo.DateTime;
                _schedule.IsLocal = false;
                _schedule.ScheduleGroupId = (int) glkScheduleGroup.EditValue;
                _schedule.ScheduleGroupName = _scheduleGroupName;

                #region Lưu schedule apartment

                for (var i = 0; i < gvScheduleApartment.RowCount; i++)
                {
                    if (gvScheduleApartment.GetRowCellValue(i, "IsChoose") == null) continue;
                    if (gvScheduleApartment.GetRowCellValue(i, "ApartmentId") == null) continue;
                    var id = (int?) gvScheduleApartment.GetRowCellValue(i, "Id");
                    var scheduleApartment = new ho_ScheduleApartment();

                    if (id != null)
                    {
                        scheduleApartment = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == id) ?? new ho_ScheduleApartment();
                    }

                    scheduleApartment.ApartmentId = (int?) gvScheduleApartment.GetRowCellValue(i, "ApartmentId");
                    scheduleApartment.ApartmentName =
                        gvScheduleApartment.GetRowCellValue(i, "ApartmentName").ToString();
                    scheduleApartment.CustomerId = (int?) gvScheduleApartment.GetRowCellValue(i, "CustomerId");
                    scheduleApartment.CustomerName = gvScheduleApartment.GetRowCellValue(i, "CustomerName").ToString();
                    scheduleApartment.ScheduleName = txtScheduleName.Text;
                    scheduleApartment.PlanId = (int?) glkPlan.EditValue;
                    scheduleApartment.PlanName = PlanName;
                    scheduleApartment.BuildingChecklistId =
                        (int?) gvScheduleApartment.GetRowCellValue(i, "BuildingChecklistId");
                    scheduleApartment.BuildingChecklistName =
                        gvScheduleApartment.GetRowCellValue(i, "BuildingChecklistName").ToString();
                    scheduleApartment.BuildingId = BuildingId;
                    scheduleApartment.UserCreateId = Common.User.MaNV;
                    scheduleApartment.UserCreateName = Common.User.HoTenNV;
                    scheduleApartment.DateCreate = DateTime.UtcNow.AddHours(7);
                    scheduleApartment.DateHandoverFrom =
                        (DateTime?) gvScheduleApartment.GetRowCellValue(i, "DateHandoverFrom");
                    scheduleApartment.DateHandoverTo =
                        (DateTime?) gvScheduleApartment.GetRowCellValue(i, "DateHandoverTo");
                    scheduleApartment.UserId = (int?) gvScheduleApartment.GetRowCellValue(i, "UserId");
                    scheduleApartment.UserName = gvScheduleApartment.GetRowCellValue(i, "UserName").ToString();
                    scheduleApartment.DateNumberNotification =
                        (decimal?) gvScheduleApartment.GetRowCellValue(i, "DateNotification");
                    scheduleApartment.StatusId = 5;
                    scheduleApartment.StatusName = "Chưa bàn giao"; //"Mới tạo lịch bàn giao nội bộ, chưa duyệt";
                    scheduleApartment.IsChoose = (bool?) gvScheduleApartment.GetRowCellValue(i, "IsChoose");

                    var scheduleApartmentLocalId =
                        (int?) gvScheduleApartment.GetRowCellValue(i, "ScheduleApartmentLocal");

                    scheduleApartment.ScheduleApartmentLocalId = scheduleApartmentLocalId;

                    scheduleApartment.DutyId = (int?) gvScheduleApartment.GetRowCellValue(i, "DutyId");
                    scheduleApartment.DutyName = gvScheduleApartment.GetRowCellValue(i, "DutyName").ToString();
                    scheduleApartment.IsLocal = false;

                    _schedule.ho_ScheduleApartments.Add(scheduleApartment);

                    
                    if (scheduleApartmentLocalId != null)
                    {
                        int trangThai = 53;
                        var scheduleApartmentLocal =
                            _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == scheduleApartmentLocalId);
                        if (scheduleApartmentLocal != null)
                        {
                            if (scheduleApartment.IsChoose == true)
                            {
                                scheduleApartmentLocal.StatusId = 5;
                                scheduleApartmentLocal.StatusName = "Đang bàn giao khách hàng";
                            }
                            else
                            {
                                scheduleApartmentLocal.StatusId = 4;
                                scheduleApartmentLocal.StatusName = "Chờ bàn giao khách hàng";
                            }
                        }

                        Library.mbMatBang matBang = GetMatBang(scheduleApartmentLocal.ApartmentId);
                        if (matBang != null) matBang.MaTT = trangThai;
                    }
                }

                #endregion

                #region Lưu lịch sử

                // lịch sử bàn giao chỉ dành cho status bàn giao khách hàng thôi, lịch sử bàn giao, lịch sử confirm sẽ được lưu riêng
                var history = new ho_PlanHistory();
                history.BuildingId = _schedule.BuildingId;
                history.Content = "Cập nhật lịch bàn giao: " + _schedule.Name;
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

                if (Id == null) _db.ho_Schedules.InsertOnSubmit(_schedule);
                _db.SubmitChanges();

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

        private Library.mbMatBang GetMatBang(int? matBangId)
        {
            return _db.mbMatBangs.FirstOrDefault(_ => _.MaMB == matBangId);
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

        private void glkDuty_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var item = sender as GridLookUpEdit;
                if (item.EditValue == null) return;
                gvScheduleApartment.SetFocusedRowCellValue("DutyId", item.EditValue);
                gvScheduleApartment.SetFocusedRowCellValue("DutyName", item.Properties.View.GetFocusedRowCellValue("Name"));
                var timeFrom = (DateTime)item.Properties.View.GetFocusedRowCellValue("HourFrom");
                var timeTo = (DateTime)item.Properties.View.GetFocusedRowCellValue("HourTo");
                var day = (DateTime)gvScheduleApartment.GetFocusedRowCellValue("DateHandoverFrom");

                gvScheduleApartment.SetFocusedRowCellValue("DateHandoverFrom", new DateTime(day.Year, day.Month, day.Day, timeFrom.Hour, timeFrom.Minute, 0));
                gvScheduleApartment.SetFocusedRowCellValue("DateHandoverTo", new DateTime(day.Year, day.Month, day.Day, timeTo.Hour, timeTo.Minute, 0));

                gvScheduleApartment.UpdateCurrentRow();
            }
            catch{}
        }

        private void DateHandoverFrom_EditValueChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < gvScheduleApartment.RowCount; i++)
            {
                var timeFrom = (DateTime)gvScheduleApartment.GetRowCellValue(i, "DateHandoverFrom");
                var value = dateHandoverFrom.DateTime;
                var timeTo = (DateTime)gvScheduleApartment.GetRowCellValue(i, "DateHandoverTo");
                gvScheduleApartment.SetRowCellValue(i, "DateHandoverFrom", new DateTime(value.Year, value.Month, value.Day, timeFrom.Hour, timeFrom.Minute, timeFrom.Second));
                gvScheduleApartment.SetRowCellValue(i, "DateHandoveTo", new DateTime(value.Year, value.Month, value.Day, timeTo.Hour, timeTo.Minute, timeTo.Second));
            }
        }
    }
}