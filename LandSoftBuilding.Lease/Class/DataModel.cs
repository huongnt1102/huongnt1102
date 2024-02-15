using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandSoftBuilding.Lease.Class
{
    public class DataModel
    {
        public byte actionId { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        public object item { get; set; }
        public int CountUnread { get; set; }

        public string idNotify { get; set; }
    }
}
