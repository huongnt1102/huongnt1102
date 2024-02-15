using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DichVu.KhoaSo.Class
{
    public class ClosingEntry
    {
        public static IEnumerable<Library.bcBookClosing> Closing(byte? TowerId, DateTime? Date, int ServiceId)
        {
            var model = new DichVu.KhoaSo.Class.CheckEntryModel
            {
                TowerId = TowerId,
                ServiceId = ServiceId, // Dịch vụ hóa đơn có id = 1
                Date = Date
            };
            return Library.Class.Connect.QueryConnect.QueryData<Library.bcBookClosing>("bcCheckClosingEntry", model);
        }

        public class GetData
        {
            public int Id { get; set; }

            public System.DateTime? DateFrom { get; set; }

            public System.DateTime? DateTo { get; set; }

            public string TowerName { get; set; }

            public string ServiceName { get; set; }

        }
    }
}
