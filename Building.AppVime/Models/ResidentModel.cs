using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Building.AppVime
{
    public class ResidentStatusBindingModel
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public List<ResidentStatusBindingModel> GetData()
        {
            var result = new List<ResidentStatusBindingModel>();

            result.Add(new ResidentStatusBindingModel()
            {
                Id = 10,
                Name = "Chờ duyệt"
            });

            result.Add(new ResidentStatusBindingModel()
            {
                Id = 20,
                Name = "Đã duyệt"
            });

            result.Add(new ResidentStatusBindingModel()
            {
                Id = 30,
                Name = "Rời khỏi"
            });

            result.Add(new ResidentStatusBindingModel()
            {
                Id = 100,
                Name = "Chưa đăng ký"
            });

            return result;
        }

        public List<ResidentStatusBindingModel> GetDataProcess()
        {
            var result = new List<ResidentStatusBindingModel>();

            result.Add(new ResidentStatusBindingModel()
            {
                Id = 10,
                Name = "Chờ duyệt"
            });

            result.Add(new ResidentStatusBindingModel()
            {
                Id = 20,
                Name = "Đã duyệt"
            });

            return result;
        }
    }

    public class ResidentModel
    {
        public bool IsCheck { get; set; }

        public Guid Id { get; set; }

        public int AmountNewsUnread { get; set; }

        public int AmountNotifyUnread { get; set; }

        public DateTime? DateOfJoin { get; set; }

        public DateTime? DateOfProcess { get; set; }

        public string DescriptionProcess { get; set; }

        public string Employeer { get; set; }

        public string FullName { get; set; }

        public byte StatusId { get; set; }

        public byte? TypeId { get; set; }

        public string Phone { get; set; }

        public string EmployeerProcess { get; set; }

        public decimal ResidentId { get; set; }

        public string Code { get; set; }

        public string Floor { get; set; }
    }

    public class ResidentChoiceModel
    {
        public decimal Id { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
        public byte? ToweId { get; set; }
        public int? ContactId { get; set; }
    }

    public class ApartmentModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Phone { get; set; }

        public string Floor { get; set; }
    }
}
