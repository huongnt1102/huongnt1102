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
    public partial class FrmHandoverCheckListAllow : DevExpress.XtraEditors.XtraForm
    {
        public int? Id { get; set; }
        public byte? BuildingId { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_ScheduleApartment _scheduleApartment;
        private string _userName;

        public FrmHandoverCheckListAllow()
        {
            InitializeComponent();
        }
        private void FrmHandoverCheckList_Load(object sender, EventArgs e)
        {
            if (Id == null) return;
            glkUserAction.Properties.DataSource =
                _db.tnNhanViens.Select(_ => new {_.HoTenNV, _.MaNV, _.MaSoNV}).ToList();
            _userName = "";
            //dateNgayDuKienHoanThanh.DateTime =new DateTime();

            _scheduleApartment = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == Id);
            if (_scheduleApartment == null) return;

            dateAction.DateTime = DateTime.UtcNow.AddHours(7);
            if (_scheduleApartment.NgayDuKienHoanThanh != null) dateNgayDuKienHoanThanh.DateTime = (DateTime) _scheduleApartment.NgayDuKienHoanThanh;

            if (_scheduleApartment.UserId != null) glkUserAction.EditValue = _scheduleApartment.UserId;
            if (_scheduleApartment.UserName != null) _userName = _scheduleApartment.UserName;

            gc.DataSource = _scheduleApartment.ho_ScheduleApartmentCheckLists;
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gv.PostEditor();
                gv.UpdateCurrentRow();

                if (glkUserAction.EditValue == null)
                {
                    DialogBox.Error("Vui lòng phân quyền nhân viên thực hiện trước");
                    glkUserAction.Focus();
                    return;
                }

                if (dateNgayDuKienHoanThanh.Text != "") _scheduleApartment.NgayDuKienHoanThanh = dateNgayDuKienHoanThanh.DateTime;

                foreach (var item in _scheduleApartment.ho_ScheduleApartmentCheckLists)
                {
                    item.DateAllow = (DateTime)dateAction.DateTime;
                    //if(glkUserAction.EditValue!=null) _.UserAllow = (int) glkUserAction.EditValue;
                    //_.UserAllowName = _userName;
                    item.UserAllow = Common.User.MaNV;
                    item.UserAllowName = Common.User.HoTenNV;

                    if (item.IsNoQuality == true)
                    {
                        #region Lưu lịch sử

                        var history = new ho_PlanHistory();
                        history.PlanId = _scheduleApartment.PlanId;
                        history.PlanName = _scheduleApartment.PlanName;
                        history.Content = "Lý do chưa đạt: " + item.UserAllowNote;
                        history.DateHandoverFrom = _scheduleApartment.DateHandoverFrom;
                        history.DateHandoverTo = _scheduleApartment.DateHandoverTo;
                        history.BuildingId = _scheduleApartment.BuildingId;
                        history.DateCreate = DateTime.UtcNow.AddHours(7);
                        history.UserCreate = Common.User.MaNV;
                        history.UserCreateName = Common.User.HoTenNV;
                        history.IsLocal = _scheduleApartment.IsLocal;
                        history.ScheduleId = _scheduleApartment.ScheduleId;
                        history.ScheduleName = _scheduleApartment.ScheduleName;
                        history.ScheduleApartmentId = _scheduleApartment.Id;
                        history.ScheduleApartmentChecklistId = item.Id;
                        history.ScheduleApartmentChecklistName = item.Name;
                        history.ApartmentId = _scheduleApartment.ApartmentId;
                        history.ApartmentName = _scheduleApartment.ApartmentName;
                        history.IsNoQuality = true;
                        _db.ho_PlanHistories.InsertOnSubmit(history);

                        #endregion
                    }
                }

                //_scheduleApartment.ho_ScheduleApartmentCheckLists.ToList().ForEach(_ =>
                //{
                //    _.DateAllow = (DateTime) dateAction.DateTime;
                //    //if(glkUserAction.EditValue!=null) _.UserAllow = (int) glkUserAction.EditValue;
                //    //_.UserAllowName = _userName;
                //    _.UserAllow = Common.User.MaNV;
                //    _.UserAllowName = Common.User.HoTenNV;
                //});

                int trangThai = 53;

                if (_scheduleApartment.ho_ScheduleApartmentCheckLists.Count(_ => _.IsNoQuality == true) > 0)
                {
                    if (chkBanGiaoThanhCong.Checked == true)
                    {
                        _scheduleApartment.StatusId = 8;
                        _scheduleApartment.StatusName = "Bàn giao thành công có lỗi";
                        var local = _db.ho_ScheduleApartments.FirstOrDefault(_ =>
                            _.Id == _scheduleApartment.ScheduleApartmentLocalId);
                        if (local != null)
                        {
                            local.StatusId = 8;
                            local.StatusName = "Bàn giao thành công có lỗi";
                        }

                        trangThai = 58;
                    }
                    else
                    {
                        _scheduleApartment.StatusId = 7;
                        _scheduleApartment.StatusName = "Bàn giao không thành công (Có lỗi)";
                        trangThai = 54;
                    }
                    
                    #region Lưu lịch sử

                    var history = new ho_PlanHistory();
                    history.PlanId = _scheduleApartment.PlanId;
                    history.PlanName = _scheduleApartment.PlanName;
                    history.Content = _scheduleApartment.StatusName;
                    history.DateHandoverFrom = _scheduleApartment.DateHandoverFrom;
                    history.DateHandoverTo = _scheduleApartment.DateHandoverTo;
                    history.BuildingId = _scheduleApartment.BuildingId;
                    history.DateCreate = DateTime.UtcNow.AddHours(7);
                    history.UserCreate = Common.User.MaNV;
                    history.UserCreateName = Common.User.HoTenNV;
                    history.IsLocal = _scheduleApartment.IsLocal;
                    history.ScheduleId = _scheduleApartment.ScheduleId;
                    history.ScheduleName = _scheduleApartment.ScheduleName;
                    history.ScheduleApartmentId = _scheduleApartment.Id;
                    history.ApartmentId = _scheduleApartment.ApartmentId;
                    history.ApartmentName = _scheduleApartment.ApartmentName;
                    history.IsNoQuality = true;
                    _db.ho_PlanHistories.InsertOnSubmit(history);

                    #endregion
                }
                else
                {
                    _scheduleApartment.StatusId = 8;
                    _scheduleApartment.StatusName = "Đã bàn giao thành công";
                    trangThai = 55;

                    #region Lưu lịch sử

                    var history = new ho_PlanHistory();
                    history.PlanId = _scheduleApartment.PlanId;
                    history.PlanName = _scheduleApartment.PlanName;
                    history.Content = "Đã bàn giao thành công";
                    history.DateHandoverFrom = _scheduleApartment.DateHandoverFrom;
                    history.DateHandoverTo = _scheduleApartment.DateHandoverTo;
                    history.BuildingId = _scheduleApartment.BuildingId;
                    history.DateCreate = DateTime.UtcNow.AddHours(7);
                    history.UserCreate = Common.User.MaNV;
                    history.UserCreateName = Common.User.HoTenNV;
                    history.IsLocal = _scheduleApartment.IsLocal;
                    history.ScheduleId = _scheduleApartment.ScheduleId;
                    history.ScheduleName = _scheduleApartment.ScheduleName;
                    history.ScheduleApartmentId = _scheduleApartment.Id;
                    history.ApartmentId = _scheduleApartment.ApartmentId;
                    history.ApartmentName = _scheduleApartment.ApartmentName;
                    _db.ho_PlanHistories.InsertOnSubmit(history);

                    #endregion

                    if (_scheduleApartment.ScheduleApartmentLocalId != null)
                    {
                        var local = _db.ho_ScheduleApartments.FirstOrDefault(_ =>
                            _.Id == _scheduleApartment.ScheduleApartmentLocalId);
                        if (local != null)
                        {
                            local.StatusId = 8;
                            local.StatusName = "Đã bàn giao thành công";

                            #region Lưu lịch sử

                            var historylocal = new ho_PlanHistory();
                            historylocal.PlanId = local.PlanId;
                            historylocal.PlanName = local.PlanName;
                            historylocal.Content = "Đã bàn giao thành công";
                            historylocal.DateHandoverFrom = local.DateHandoverFrom;
                            historylocal.DateHandoverTo = local.DateHandoverTo;
                            historylocal.BuildingId = local.BuildingId;
                            historylocal.DateCreate = DateTime.UtcNow.AddHours(7);
                            historylocal.UserCreate = Common.User.MaNV;
                            historylocal.UserCreateName = Common.User.HoTenNV;
                            historylocal.IsLocal = local.IsLocal;
                            historylocal.ScheduleId = local.ScheduleId;
                            historylocal.ScheduleName = local.ScheduleName;
                            historylocal.ScheduleApartmentId = local.Id;
                            history.ApartmentId = local.ApartmentId;
                            history.ApartmentName = local.ApartmentName;
                            _db.ho_PlanHistories.InsertOnSubmit(historylocal);

                            #endregion
                        }
                    }

                    var mbMatBang = _db.mbMatBangs.FirstOrDefault(_ => _.MaMB == _scheduleApartment.ApartmentId);
                    if (mbMatBang != null)
                    {
                        mbMatBang.NgayBanGiao = _scheduleApartment.DateHandoverTo;
                        mbMatBang.MaKH = _scheduleApartment.CustomerId;
                        mbMatBang.MaKHF1 = _scheduleApartment.CustomerId;
                        mbMatBang.DaGiaoChiaKhoa = true;
                        mbMatBang.NhanVienBanGiaoNha = Common.User.HoTenNV;
                        mbMatBang.MaTT = trangThai;
                    }
                }

                _db.SubmitChanges();
                DialogBox.Success("Lưu hạng mục checklist bàn giao thành công.");
                DialogResult = DialogResult.OK;
                Close();
                
            }
            catch
            {
                DialogBox.Error("Lưu hạng mục không thành công");
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            var item = sender as CheckEdit;
            if (item.EditValue == null) return;


            gv.SetFocusedRowCellValue("IsChoose", (bool) item.EditValue);
        }
    }
}