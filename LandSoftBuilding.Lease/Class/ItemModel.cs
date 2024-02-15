using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandSoftBuilding.Lease.Class
{
    public class ItemModel
    {
        public string id { get; set; }

        public string title { get; set; }

        public string shortDescription { get; set; }

        public string imageUrl { get; set; }

        public DateTime? dateCreate { get; set; }

        public byte towerId { get; set; }

        public string towerName { get; set; }

        public bool isRead { get; set; }

        public bool isImportant { get; set; }

        public byte typeId { get; set; }

        public long residentId { get; set; }
    }
}
