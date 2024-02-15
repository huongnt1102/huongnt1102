using System;
using System.Linq;

namespace DichVu.BanGiaoMatBang.Customer
{
    public partial class FrmMacNhienBanGiao : DevExpress.XtraEditors.XtraForm
    {
        public int? HandOverId { get; set; }

        private Library.MasterDataContext _db = new Library.MasterDataContext();
        private Library.ho_ScheduleApartment _handOver;

        public FrmMacNhienBanGiao()
        {
            InitializeComponent();
        }

        private void FrmMacNhienBanGiao_Load(object sender, System.EventArgs e)
        {
            _handOver = GetHandOver();
            if (_handOver.Id == 0) _db.ho_ScheduleApartments.InsertOnSubmit(_handOver);

            dateDay.DateTime = System.DateTime.UtcNow.AddHours(7);
            if (_handOver.NgayMacNhienBanGiao != null) dateDay.DateTime = (DateTime) _handOver.NgayMacNhienBanGiao;

            txtContents.Text = _handOver.NoteMacNhienBanGiao;
        }

        private Library.ho_ScheduleApartment GetHandOver()
        {
            return HandOverId!=null ? _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == HandOverId): new Library.ho_ScheduleApartment();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _handOver = SaveScheduleApartmentCustomer(_handOver, 10, "Mặc nhiên bàn giao");
                SavePlanHistory(_handOver);

                Library.ho_ScheduleApartment scheduleApartmentLocal = GetScheduleApartmentLocal(_handOver);
                if (scheduleApartmentLocal != null) SavePlanHistory(scheduleApartmentLocal);

                SaveMatBang(_handOver, true, 57);

                _db.SubmitChanges();
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Library.DialogBox.Success();
                Close();
            }
            catch (System.Exception ex)
            {
                Library.DialogBox.Error(ex.Message);
            }
        }

        private Library.ho_ScheduleApartment GetScheduleApartment(int id)
        {
            return _db.ho_ScheduleApartments.FirstOrDefault(_ => _.Id == id);
        }

        private Library.ho_ScheduleApartment SaveScheduleApartmentCustomer(Library.ho_ScheduleApartment scheduleApartmentCustomer, int? statusId, string statusName)
        {
            scheduleApartmentCustomer.StatusId = statusId;
            scheduleApartmentCustomer.StatusName = statusName;
            scheduleApartmentCustomer.NgayMacNhienBanGiao = dateDay.DateTime;
            scheduleApartmentCustomer.NoteMacNhienBanGiao = txtContents.Text;
            return scheduleApartmentCustomer;
        }

        private Library.ho_ScheduleApartment GetScheduleApartmentLocal(Library.ho_ScheduleApartment scheduleApartmentCustomer)
        {
            if (scheduleApartmentCustomer.ScheduleApartmentLocalId == null) return null;
            var local = GetScheduleApartment((int)scheduleApartmentCustomer.ScheduleApartmentLocalId);
            if (local == null) return null;
            local.StatusId = scheduleApartmentCustomer.StatusId;
            local.StatusName = scheduleApartmentCustomer.StatusName;
            return local;
        }

        private void SavePlanHistory(Library.ho_ScheduleApartment scheduleApartment)
        {
            var history = new Library.ho_PlanHistory();
            history.PlanId = scheduleApartment.PlanId;
            history.PlanName = scheduleApartment.PlanName;
            history.Content = scheduleApartment.StatusName;
            history.DateHandoverFrom = scheduleApartment.DateHandoverFrom;
            history.DateHandoverTo = scheduleApartment.DateHandoverTo;
            history.BuildingId = scheduleApartment.BuildingId;
            history.DateCreate = System.DateTime.UtcNow.AddHours(7);
            history.UserCreate = Library.Common.User.MaNV;
            history.UserCreateName = Library.Common.User.HoTenNV;
            history.IsLocal = scheduleApartment.IsLocal;
            history.ScheduleId = scheduleApartment.ScheduleId;
            history.ScheduleName = scheduleApartment.ScheduleName;
            history.ScheduleApartmentId = scheduleApartment.Id;
            history.ApartmentId = scheduleApartment.ApartmentId;
            history.ApartmentName = scheduleApartment.ApartmentName;
            _db.ho_PlanHistories.InsertOnSubmit(history);
        }

        private void SaveMatBang(Library.ho_ScheduleApartment scheduleApartmentCustomer, bool? daGiaoChiaKhoa, int? trangThai)
        {
            var matBang = _db.mbMatBangs.FirstOrDefault(_ => _.MaMB == scheduleApartmentCustomer.ApartmentId);
            if (matBang != null)
            {
                matBang.NgayBanGiao = scheduleApartmentCustomer.DateHandoverTo;
                matBang.MaKH = scheduleApartmentCustomer.CustomerId;
                matBang.MaKHF1 = scheduleApartmentCustomer.CustomerId;
                matBang.DaGiaoChiaKhoa = daGiaoChiaKhoa;
                matBang.NhanVienBanGiaoNha = Library.Common.User.HoTenNV;
                matBang.MaTT = trangThai;
            }
        }

        private void ItemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}