using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmHandoverCheckListAllow : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Bổ sung: Thêm bàn giao thành công không có lỗi, và bàn giao thành công có lỗi (16/9/2019)
        /// Bổ sung: ngày hoàn thành dự kiến (16/9/2019)
        /// </summary>

        public int? Id { get; set; }
        public byte? BuildingId { get; set; }
        public int? Loai { get; set; } // 1: bàn giao của nhân viên, 2: bàn giao của nhà thầu

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
            glkUserAction.Properties.DataSource = _db.tnNhanViens.Select(_ => new {_.HoTenNV, _.MaNV, _.MaSoNV}).ToList();
            _userName = "";
            //dateNgayDuKienHoanThanh.DateTime = DateTime.Now;

            _scheduleApartment = _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == Id);
            if (_scheduleApartment == null) return;

            dateAction.DateTime = DateTime.UtcNow.AddHours(7);
            if (_scheduleApartment.NgayDuKienHoanThanh != null)
                dateNgayDuKienHoanThanh.DateTime = (DateTime) _scheduleApartment.NgayDuKienHoanThanh;

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

                if (dateNgayDuKienHoanThanh.Text!="") _scheduleApartment.NgayDuKienHoanThanh = dateNgayDuKienHoanThanh.DateTime;

                foreach (var item in _scheduleApartment.ho_ScheduleApartmentCheckLists)
                {
                    if (Loai == 2)
                    {
                        item.DateFix = dateAction.DateTime;
                        
                        item.UserFixId = Common.User.MaNV;
                        item.UserFixName = Common.User.HoTenNV;
                        item.UserFixNote = item.UserAllowNote;
                    }
                    else
                    {
                        item.DateAllow = dateAction.DateTime;
                        
                        item.UserAllow = Common.User.MaNV;
                        item.UserAllowName = Common.User.HoTenNV;
                    }

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

                int trangThai = 1;
                if (_scheduleApartment.ho_ScheduleApartmentCheckLists.Count(_ => _.IsNoQuality == true) > 0)
                {
                    

                    // Nếu vẫn có tích, tức là người ta chọn có lỗi)
                    // khi có lỗi, thì phải kiểm tra, nếu có lỗi mà tích vào vẫn thành công
                    if (chkBanGiaoThanhCong.Checked == true)
                    {
                        // nếu tích vào thành công
                        if (Loai == 2)
                        {
                            _scheduleApartment.StatusId = 9;
                            _scheduleApartment.StatusName = "Nhà thầu đã sửa chữa xong";
                            trangThai = 60;
                        }
                        else
                        {
                            _scheduleApartment.StatusId = 4;
                            _scheduleApartment.StatusName = "Bàn giao nội bộ thành công (Có lỗi)";
                            trangThai = 4;
                        }
                    }
                    else
                    {
                        _scheduleApartment.StatusId = 3;
                        _scheduleApartment.StatusName = "Chuyển nhà thầu xử lý";
                        trangThai = 2;
                    }

                    #region Lưu lịch sử

                    var history = new ho_PlanHistory();
                    history.PlanId = _scheduleApartment.PlanId;
                    history.PlanName = _scheduleApartment.PlanName;
                    history.Content = "Kiểm tra hạng mục chưa đạt, chuyển nhà thầu xử lý";
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
                    if (Loai == 2)
                    {
                        _scheduleApartment.StatusId = 9;
                        _scheduleApartment.StatusName = "Nhà thầu đã sửa chữa xong";
                        trangThai = 60;
                    }
                    else
                    {
                        _scheduleApartment.StatusId = 4;
                        _scheduleApartment.StatusName = "Bàn giao nội bộ thành công";
                        trangThai = 3;
                    }

                    #region Lưu lịch sử

                    var history = new ho_PlanHistory();
                    history.PlanId = _scheduleApartment.PlanId;
                    history.PlanName = _scheduleApartment.PlanName;
                    history.Content = "Chờ bàn giao khách hàng";
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
                }

                Library.mbMatBang matBang = GetMatBang(_scheduleApartment.ApartmentId);
                if (matBang != null) matBang.MaTT = trangThai;

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

        private Library.mbMatBang GetMatBang(int? matBangId)
        {
            return _db.mbMatBangs.FirstOrDefault(_ => _.MaMB == matBangId);
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

            
            gv.SetFocusedRowCellValue("IsChoose", (bool)item.EditValue);
        }
    }
}