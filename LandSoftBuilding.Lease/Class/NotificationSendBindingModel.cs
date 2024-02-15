using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandSoftBuilding.Lease.Class
{
    public class NotificationSendBindingModel
    {
        /// <summary>
        /// Gửi cho nhiều token
        /// formart ["1", "2"]
        /// </summary>
        public List<string> registration_ids;

        /// <summary>
        /// Thông báo full
        /// </summary>
        public NotificationModel notification;

        /// <summary>
        /// Data 
        /// </summary>
        public DataModel data;

        /// <summary>
        /// Icon notify
        /// </summary>
        //public string small_icon { get; set; } = "ic_notification";
        public string  small_icon { get; private set; }
    
        

        /// <summary>
        /// Token nhận
        /// </summary>
        public string to;

        public bool content_available { get; private set; } //= true;

        public bool show_in_foreground { get; private set; }// = true;

        public NotificationSendBindingModel()
        {
            this.small_icon = "ic_notification";
            this.content_available = true;
            this.show_in_foreground = true;
        }
    }
}
