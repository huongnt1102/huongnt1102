using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmDeXuatDoiLich : DevExpress.XtraEditors.XtraForm
    {
        private Library.MasterDataContext _db = new Library.MasterDataContext();

        public FrmDeXuatDoiLich()
        {
            InitializeComponent();
        }

        private void FrmDeXuatDoiLich_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkBuilding.DataSource = Common.TowerList;
            itemBuilding.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemStartDate.EditValue = objKbc.DateFrom;
            itemEndDate.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var buildingId = (byte)itemBuilding.EditValue;
                var startDate = (DateTime?)itemStartDate.EditValue;
                var endDate = (DateTime?)itemEndDate.EditValue;
                _db = new MasterDataContext();
                
                // khi mình chọn vị trí để đề xuất, thì đó chính là vị trí mới, sheduleaperment to
                gc.DataSource = (from dx in _db.ho_DeXuatDoiLiches
                    join tu in _db.ho_ScheduleApartments on dx.ScheduleApartmentFromId equals tu.Id
                    join den in _db.ho_ScheduleApartments on dx.ScheduleApartmentToId equals den.Id
                    where dx.BuildingId == buildingId & SqlMethods.DateDiffDay(startDate, dx.DateCreate) >= 0 &
                          SqlMethods.DateDiffDay(dx.DateCreate, endDate) >= 0
                    select new
                    {
                        dx.Id, dx.ApartmentFromName, dx.CustomerName, dx.DateHandOverFrom, dx.DutyName, dx.IsChange,
                        dx.PlanToName, dx.ScheduleToName, dx.ho_DeXuatDoiLichStatus.Name, dx.DateCreate, TuViTri=tu.ApartmentName,
                        DenViTri=den.ApartmentName, dx.DateHandOverTo, DieuKien = (tu.ApartmentId!=null & dx.IsChange==true)?"Không thể xóa":"", Loai=dx.IsChange==true?"Đổi ca":"Đặt chổ mới", Color = dx.ho_DeXuatDoiLichStatus.Color
                    }).ToList();

            }
            catch { }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRowCellValue("Id") == null)
                {
                    DialogBox.Error("Vui lòng chọn phiếu cần cập nhật, xin cảm ơn.");
                    return;
                }

                var id = (int?) gv.GetFocusedRowCellValue("Id");
                var deXuat = GetDeXuat(id);
                if (deXuat == null) return;

                var apartmentChoose = GetScheduleApartment(deXuat.ScheduleApartmentFromId);
                var scheduleApartment = GetScheduleApartment(deXuat.ScheduleApartmentToId);

                if (KiemTra(apartmentChoose, scheduleApartment, false, Enum.DUYET)) return;

                #region Lưu mặt bằng chọn

                var planHistory = GetPlanHistory(deXuat, apartmentChoose, scheduleApartment);
                scheduleApartment = UpdateSheduleApartment(scheduleApartment, apartmentChoose, deXuat, Enum.DUYET);
                apartmentChoose = UpdateApartmentChoose(apartmentChoose, deXuat, Enum.DUYET);
                CreateListChecklist(scheduleApartment, apartmentChoose);
                
                #endregion

                deXuat= UpdateDeXuat(deXuat);

                SendNotify(Enum.NOI_DUNG_DUYET, Enum.TIEU_DE_DUYET, scheduleApartment.CustomerId, scheduleApartment.ScheduleId, scheduleApartment.Id, scheduleApartment.ApartmentId, scheduleApartment.ApartmentName);


                _db.ho_PlanHistories.InsertOnSubmit(planHistory);
                _db.SubmitChanges();
                DialogBox.Success("Lưu dữ liệu thành công.");
                DialogResult = DialogResult.OK;
                LoadData();

            }
            catch (Exception)
            {
                //throw;
            }
        }

        #region Duyệt, Hủy duyệt, không duyệt

        public class Enum
        {
            public const string DUYET = "Duyet";

            public const string HUY_DUYET = "HuyDuyet";
            public const string HUY_DUYET_CUSTOMER = "HuyDuyetCustomer";
            public const string HUY_DUYET_LOCAL = "HuyDuyetLocal";

            public const string NOI_DUNG_DUYET = "Đề xuất đổi lịch của quý khách đã được duyệt.";
            public const string TIEU_DE_DUYET = "Duyệt đề xuất";

            public const string NOI_DUNG_KHONG_DUYET = "Đề xuất đổi lịch của quý khách không được duyệt.";
        }

        private void SendNotify(string content, string title, int? customerId, int? scheduleId, int? scheduleApartmentId, int? apartmentId, string apartmentName)
        {
            Library.ho_Notifycation notifycation = new Library.ho_Notifycation();
            notifycation = DichVu.BanGiaoMatBang.Class.Notifycation.GetNotifycation(notifycation, (byte)itemBuilding.EditValue, content, false, title, customerId, scheduleId, scheduleApartmentId, apartmentId, apartmentName);
            if (customerId != null)
            {
                //var resultSend = Building.AppVime.News.SendNotification.Send(content, title, (int)customerId);
                //if (resultSend != null) 
                    _db.ho_Notifycations.InsertOnSubmit(notifycation);
            }
        }

        private bool KiemTra(Library.ho_ScheduleApartment apartmentChoose, Library.ho_ScheduleApartment scheduleApartment,bool? isChange, string loai)
        {
            switch (loai)
            {
                case Enum.DUYET:
                    if (apartmentChoose == null | scheduleApartment == null) return true;

                    if (scheduleApartment.ApartmentId != null)
                    {
                        DialogBox.Error("Lịch này đã có mặt bằng, vui lòng chọn chổ trống");
                        return true;
                    }
                    
                    break;

                case Enum.HUY_DUYET:
                    if (apartmentChoose.ApartmentId != null & isChange == true)
                    {
                        DialogBox.Error("Lịch này đã có mặt bằng, nên không trả về được");
                        return true;
                    }
                    
                    break;
            }

            if (scheduleApartment.StatusId == 8)
            {
                DialogBox.Error("Vị trí này đã bàn giao mặt bằng thành công, không được đổi nữa");
                return true;
            }
            if (apartmentChoose.StatusId == 8)
            {
                DialogBox.Error("Mặt bằng này đã bàn giao thành công, không được đổi nữa");
                return true;
            }
            
            return false;
        }

        private Library.ho_DeXuatDoiLich UpdateDeXuat(Library.ho_DeXuatDoiLich deXuat)
        {
            deXuat.DateUpdate = DateTime.UtcNow.AddHours(7);
            deXuat.UserAllowId = Common.User.MaNV;
            deXuat.UserAllowName = Common.User.HoTenNV;

            deXuat.AllowStatusId = 2;

            return deXuat;
        }

        private void CreateListChecklist(Library.ho_ScheduleApartment scheduleApartment, Library.ho_ScheduleApartment apartmentChoose)
        {
            // kiểm tra nếu có buildingChecklist thì phải tạo phiếu checklist, phiếu checklist cũ thì sao?
            // nếu có checklist cũ, thì không tạo mới. Nếu chưa có checklist, tức là đây là mặt bằng trống mới, thì mới tạo checklist, chổ này mình thấy chưa ổn lắm.
            if (scheduleApartment.ho_ScheduleApartmentCheckLists.Count <= 0)
            {
                // tạo checklist mới
                var buildingChecklist = _db.ho_BuildingChecklists.FirstOrDefault(_ => _.Id == scheduleApartment.BuildingChecklistId);
                if (buildingChecklist != null)
                {
                    if (buildingChecklist.ListChecklistId != null)
                    {
                        var listChecklist = _db.ho_Checklists.Where(_ => _.ListChecklistId == buildingChecklist.ListChecklistId & _.IsNotUse == false);
                        foreach (var checklist in listChecklist)
                        {
                            var apartmentChecklist = CreateChecklist(checklist, buildingChecklist, scheduleApartment);
                            scheduleApartment.ho_ScheduleApartmentCheckLists.Add(apartmentChecklist);
                        }

                        // delete hết checklist ở mặt bằng cũ đi
                        _db.ho_ScheduleApartmentCheckLists.DeleteAllOnSubmit(apartmentChoose.ho_ScheduleApartmentCheckLists);
                    }
                }
            }
            else
            {
                // test 
                //db.ho_ScheduleApartmentCheckLists.Where(_ => _.ScheduleApartmentId == apartmentChoose.Id).ToList().ForEach(
                //    a => { a.ScheduleApartmentId = scheduleApartment.Id; });

                //db.ho_ScheduleApartmentCheckLists.Where(_ => _.ScheduleApartmentId == scheduleApartment.Id).ToList().ForEach(
                //    a => { a.ScheduleApartmentId = apartmentChoose.Id; });
            }
        }

        private Library.ho_ScheduleApartment UpdateSheduleApartment(Library.ho_ScheduleApartment scheduleApartment, Library.ho_ScheduleApartment apartmentChoose, Library.ho_DeXuatDoiLich deXuat, string loai)
        {
            switch (loai)
            {
                case Enum.DUYET:
                    scheduleApartment.ApartmentId = apartmentChoose.ApartmentId;
                    scheduleApartment.ApartmentName = apartmentChoose.ApartmentName;
                    scheduleApartment.ScheduleApartmentLocalId = deXuat.IsChange == true ? apartmentChoose.ScheduleApartmentLocalId ?? 0 : apartmentChoose.Id;
                    scheduleApartment.StatusId = 6;
                    scheduleApartment.StatusName = "Chờ bàn giao khách hàng";
                    scheduleApartment.IsBooked = false;
                    scheduleApartment.CustomerId = deXuat.CustomerId;
                    scheduleApartment.CustomerName = deXuat.CustomerName;
                    break;

                case Enum.HUY_DUYET:
                    scheduleApartment.StatusName = "Chưa bàn giao";
                    scheduleApartment.ApartmentId = null;
                    scheduleApartment.ApartmentName = null;
                    scheduleApartment.CustomerId = null;
                    scheduleApartment.CustomerName = null;

                    scheduleApartment.StatusId = 5;
                    scheduleApartment.StatusName = "Chưa bàn giao";
                    scheduleApartment.IsBooked = false;
                    break;
            }
            
            return scheduleApartment;
        }

        private Library.ho_ScheduleApartment UpdateApartmentChoose(Library.ho_ScheduleApartment apartmentChoose, Library.ho_DeXuatDoiLich deXuat, string loai)
        {
            switch (loai)
            {
                case Enum.DUYET:
                    // sau khi đổi thành công, phải chuyển mặt bằng cũ thành trạng thái 5 Đang bàn giao khách hàng
                    apartmentChoose.StatusId = 5;
                    apartmentChoose.StatusName = "Chưa bàn giao";
                    if (deXuat.IsChange == true)
                    {
                        // nếu chọn từ mặt bằng đã có, phải trả lại vị trí này = trống
                        apartmentChoose.ApartmentId = null;
                        apartmentChoose.ApartmentName = null;
                        apartmentChoose.CustomerId = null;
                        apartmentChoose.CustomerName = null;
                        apartmentChoose.StatusName = "Chưa bàn giao";
                        apartmentChoose.ScheduleApartmentLocalId = null;
                    }
                    break;

                case Enum.HUY_DUYET:
                    apartmentChoose.ApartmentId = deXuat.ApartmentFromId;
                    apartmentChoose.ApartmentName = deXuat.ApartmentFromName;
                    apartmentChoose.CustomerId = deXuat.CustomerId;
                    apartmentChoose.CustomerName = deXuat.CustomerName;
                    break;

                case Enum.HUY_DUYET_CUSTOMER:
                    apartmentChoose.StatusId = 6;
                    apartmentChoose.StatusName = "Chờ bàn giao khách hàng";
                    break;

                case Enum.HUY_DUYET_LOCAL:
                    apartmentChoose.ScheduleApartmentLocalId = null;
                    apartmentChoose.StatusId = 4;
                    apartmentChoose.StatusName = "Chờ bàn giao khách hàng";
                    break;
            }
            
            return apartmentChoose;
        }

        private Library.ho_ScheduleApartmentCheckList CreateChecklist(Library.ho_Checklist checklist, Library.ho_BuildingChecklist buildingChecklist, Library.ho_ScheduleApartment scheduleApartment)
        {
            var apartmentChecklist = new ho_ScheduleApartmentCheckList
            {
                GroupName = checklist.GroupName,
                BuildingId = scheduleApartment.BuildingId,
                DateAction = scheduleApartment.DateHandoverFrom,
                UserCreate = Common.User.MaNV,
                UserCreateName = Common.User.HoTenNV,
                DateCreate = DateTime.UtcNow.AddHours(7),
                PlanId = scheduleApartment.PlanId,
                Name = checklist.Name,
                PlanName = scheduleApartment.PlanName,
                ScheduleName = scheduleApartment.ScheduleName,
                BuildingChecklistId = buildingChecklist.Id,
                BuildingChecklistName = buildingChecklist.ListChecklistName,
                IsLocal = false,
                Stt = checklist.Stt,
                ScheduleId = scheduleApartment.ScheduleId,
                Description = checklist.Description,
                IsNoQuality = false
            };
            //apartmentChecklist.UserActionId = scheduleApartment.UserId;
            //apartmentChecklist.UserActionName = scheduleApartment.UserName;

            return apartmentChecklist;
        }

        private Library.ho_DeXuatDoiLich GetDeXuat(int? id)
        {
            return _db.ho_DeXuatDoiLiches.FirstOrDefault(_ => _.Id == id);
        }

        private Library.ho_ScheduleApartment GetScheduleApartment(int? id)
        {
            return _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == id);
        }

        private Library.ho_PlanHistory GetPlanHistory(Library.ho_DeXuatDoiLich deXuat, Library.ho_ScheduleApartment apartmentChoose, Library.ho_ScheduleApartment scheduleApartment)
        {
            #region Lưu lịch sử

            var planHistory = new ho_PlanHistory
            {
                Content = deXuat.IsChange == true
                    ? ("Chuyển từ mặt bằng " + apartmentChoose.ApartmentName + " sang " +
                       scheduleApartment.ApartmentName)
                    : "Chuyển mặt bằng " + scheduleApartment.ApartmentName + " vào chổ trống",
                PlanId = scheduleApartment.PlanId,
                PlanName = scheduleApartment.PlanName,
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
                ApartmentId = apartmentChoose.ApartmentId,
                ApartmentName = apartmentChoose.ApartmentName
            };
            //_planHistory.Content = "Chuyển từ mặt bằng: " + chkChoose.Checked == true ? apartmentChoose.ApartmentName + " sang " + txtApartment.Text :);

            if (deXuat.CustomerId != null)
            {
                planHistory.CustomerId = deXuat.CustomerId;
                planHistory.CustomerName = deXuat.CustomerName;
            }

            return planHistory;

            #endregion
        }
        #endregion

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // nếu đã duyệt thì không được xóa, phải bỏ duyệt
            try
            {
                int[] indexs = gv.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Alert("Vui lòng chọn những phiếu cần xóa");
                    return;
                }

                if (DialogBox.QuestionDelete() == DialogResult.No) return;

                foreach (var r in indexs)
                {
                    var deXuatDoiLich = GetDeXuat(int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                    if (deXuatDoiLich != null)
                    {

                        // xóa đề xuất
                        // 2. kiểm tra mặt bằng from, trả dữ liệu về cho mặt bằng from, mặt bằng to để trống
                        var apartmentChoose = GetScheduleApartment(deXuatDoiLich.ScheduleApartmentFromId);
                        var scheduleApartment = GetScheduleApartment(deXuatDoiLich.ScheduleApartmentToId);

                        if (apartmentChoose != null & scheduleApartment != null)
                        {
                            if (KiemTra(apartmentChoose, scheduleApartment, deXuatDoiLich.IsChange,
                                Enum.HUY_DUYET)) return;

                            apartmentChoose = UpdateApartmentChoose(apartmentChoose, deXuatDoiLich, Enum.HUY_DUYET);

                            scheduleApartment = UpdateSheduleApartment(scheduleApartment, apartmentChoose, deXuatDoiLich, Enum.HUY_DUYET);

                            if (deXuatDoiLich.AllowStatusId == 2)
                            {
                                // update hết tất cả checklist cũ thành checklist mới
                                _db.ho_ScheduleApartmentCheckLists.Where(_ => _.ScheduleApartmentId == scheduleApartment.Id).ToList().ForEach(a => { a.ScheduleApartmentId = apartmentChoose.Id; });
                                apartmentChoose.ScheduleApartmentLocalId =
                                    scheduleApartment.ScheduleApartmentLocalId;
                                scheduleApartment.ScheduleApartmentLocalId = null;
                            }

                            apartmentChoose = UpdateApartmentChoose(apartmentChoose, deXuatDoiLich, deXuatDoiLich.IsChange == true ? Enum.HUY_DUYET_CUSTOMER : Enum.HUY_DUYET_LOCAL);
                        }

                        // 1. xóa bảng đề xuất
                        _db.ho_DeXuatDoiLiches.DeleteOnSubmit(deXuatDoiLich);

                    }

                    _db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng đề xuất này nên không xóa được");
                return;
            }
        }

        private void gv_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if ((e.Column.FieldName == "Name") & gv.GetRowCellValue(e.RowHandle, "Color")!=null)
                {
                    e.Appearance.BackColor = System.Drawing.Color.FromArgb((int)gv.GetRowCellValue(e.RowHandle, "Color"));
                }
            }
            catch { }
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // nếu đã duyệt rồi thì phải hủy duyệt
            // nếu đã duyệt thì không được xóa, phải bỏ duyệt
            try
            {
                //using (var db = new MasterDataContext())
                //{
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn phiếu");
                        return;
                    }
                    if (DialogBox.Question("Bạn muốn không duyệt?") == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var deXuatDoiLich = GetDeXuat(int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (deXuatDoiLich != null)
                        {

                            if (deXuatDoiLich.AllowStatusId == 2)
                            {
                                DialogBox.Error("Vui lòng bỏ duyệt");
                                return;
                            }
                            // trường hợp không duyệt thì phải bảo đảm là đã bỏ duyệt.
                            // thả vị trí đến ra
                            var apartmentTo = GetScheduleApartment(deXuatDoiLich.ScheduleApartmentToId);
                            if (apartmentTo != null)
                            {
                                apartmentTo.IsBooked = false;
                            }

                            var scheduleFrom = GetScheduleApartment(deXuatDoiLich.ScheduleApartmentFromId);
                            if (scheduleFrom != null)
                            {
                                if (scheduleFrom.CustomerId != null)
                                    SendNotify(Enum.NOI_DUNG_KHONG_DUYET, Enum.TIEU_DE_DUYET, scheduleFrom.CustomerId, scheduleFrom.ScheduleId, scheduleFrom.Id, scheduleFrom.ApartmentId, scheduleFrom.ApartmentName);
                            }

                            deXuatDoiLich.AllowStatusId = 3;
                        }

                    }
                    _db.SubmitChanges();
                    LoadData();
                //}
            }
            catch (Exception)
            {
                DialogBox.Error("Có nơi khác đang dùng đề xuất này nên không xóa được");
                return;
            }
        }

        private void itemHuyDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // nếu đã duyệt rồi thì phải hủy duyệt
            // nếu đã duyệt thì không được xóa, phải bỏ duyệt
            try
            {
                //using (var db = new MasterDataContext())
                //{
                    int[] indexs = gv.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Alert("Vui lòng chọn phiếu");
                        return;
                    }
                    if (DialogBox.Question("Bạn muốn hủy duyệt?") == DialogResult.No) return;

                    foreach (var r in indexs)
                    {
                        var deXuatDoiLich = GetDeXuat(int.Parse(gv.GetRowCellValue(r, "Id").ToString()));
                        if (deXuatDoiLich != null)
                        {

                            // xóa đề xuất
                            // 2. kiểm tra mặt bằng from, trả dữ liệu về cho mặt bằng from, mặt bằng to để trống

                            var apartmentChoose = GetScheduleApartment(deXuatDoiLich.ScheduleApartmentFromId);
                            var scheduleApartment = GetScheduleApartment(deXuatDoiLich.ScheduleApartmentToId);

                            if (apartmentChoose != null & scheduleApartment != null)
                            {
                                if (KiemTra(apartmentChoose, scheduleApartment, deXuatDoiLich.IsChange, Enum.HUY_DUYET)) return;

                                apartmentChoose = UpdateApartmentChoose(apartmentChoose, deXuatDoiLich, Enum.HUY_DUYET);
                                scheduleApartment = UpdateSheduleApartment(scheduleApartment, apartmentChoose, deXuatDoiLich, Enum.HUY_DUYET);

                                if (deXuatDoiLich.AllowStatusId == 2)
                                {

                                    // update hết tất cả checklist cũ thành checklist mới
                                    _db.ho_ScheduleApartmentCheckLists.Where(_ => _.ScheduleApartmentId == scheduleApartment.Id).ToList().ForEach(
                                        a => { a.ScheduleApartmentId = apartmentChoose.Id; });
                                    apartmentChoose.ScheduleApartmentLocalId =
                                        scheduleApartment.ScheduleApartmentLocalId;
                                    scheduleApartment.ScheduleApartmentLocalId = null;
                                }

                                apartmentChoose = UpdateApartmentChoose(apartmentChoose, deXuatDoiLich, deXuatDoiLich.IsChange == true ? Enum.HUY_DUYET_CUSTOMER : Enum.HUY_DUYET_LOCAL);
                            }

                            // 1. xóa bảng đề xuất
                            //db.ho_DeXuatDoiLiches.DeleteOnSubmit(deXuatDoiLich);
                            deXuatDoiLich.AllowStatusId = 1;
                        }

                    //}
                    _db.SubmitChanges();
                    LoadData();
                }
            }
            catch (Exception)
            {
                DialogBox.Error("Đã có lỗi xảy ra");
                return;
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gc);
        }

        private void ItemSendNotifyStaff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new DichVu.BanGiaoMatBang.Category.FrmSendNotifyStaff() {BuidingId = (byte) itemBuilding.EditValue})
            {
                frm.ShowDialog();
                LoadData();
            }
        }
    }
}