using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandSoftBuilding.Lease.Class
{
    public class NotificationModel
    {
        public string title { get; set; }

        public string body { get; set; }

        //public string sound { get; set; } = "default";
        public string sound { get; private set; } 

        public NotificationModel()
        {
            this.sound = "default";
        }

        /// <summary>
        /// dùng cho android nếu trùng roomId thì chỉ sinh 1 notification và đè cái cũ
        /// </summary>
        public string tag { get; set; }
        public string badge { get; set; }
    }
}
