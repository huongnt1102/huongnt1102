using System.Linq;

namespace DichVu.BanGiaoMatBang.Class
{
    public static class Notifycation
    {
        private static Library.MasterDataContext _db = new Library.MasterDataContext();

        public static Library.ho_Notifycation GetNotifycation(Library.ho_Notifycation notifycation, byte? buildingId, string contents, bool isLocal, string title, int? customerId, int? scheduleId, int? scheduleApartmentId, int? apartmentId, string apartmentName)
        {
            notifycation.BuildingId = buildingId;
            notifycation.Contents = contents;
            notifycation.DateCreate = System.DateTime.UtcNow.AddHours(7);
            notifycation.IsLocal = isLocal;
            notifycation.Title = title;
            notifycation.UserCreate = Library.Common.User.MaNV;
            notifycation.UserCreateName = Library.Common.User.HoTenNV;
            notifycation.CustomerId = customerId;
            if (scheduleId != null) notifycation.ScheduleId = scheduleId;
            if (scheduleApartmentId != null) notifycation.ScheduleApartmentId = scheduleApartmentId;
            if (apartmentId != null) notifycation.ApartmentId = apartmentId;
            notifycation.ApartmentName = apartmentName;

            return notifycation;
        }

        public static Library.ho_SendNotifyStaff ViewNhacNhoDeXuat(int? userId, byte? buildingId)
        {
            return _db.ho_SendNotifyStaffs.FirstOrDefault(_ => _.UserId == userId & _.BuildingId == buildingId & _.IsNotify == true);
        }

        public static System.Collections.Generic.List<Library.ho_DeXuatDoiLich> GetListDeXuatNew(byte? buildingId)
        {
            return _db.ho_DeXuatDoiLiches.Where(_ => _.BuildingId == buildingId & _.AllowStatusId == 1).ToList();
        }

        public class NhacViec
        {
            public int? ID { get; set; }
            public string NoiDung { get; set; }
            public bool DaDoc { get; set; }
        }
    }
}
