using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Building.AppVime.Models
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
        //public NotificationModel data;

        public DataModel data;

        /// <summary>
        /// Icon notify
        /// </summary>
        public string small_icon { get; set; } 

        /// <summary>
        /// Token nhận
        /// </summary>
        public string to;
    }

    public class NotificationModel
    {
        public string id { get; set; }

        public string title { get; set; }

        public string body { get; set; }

        public string sound { get; set; }

        public byte actionId { get; set; }

        /// <summary>
        /// dùng cho android nếu trùng roomId thì chỉ sinh 1 notification và đè cái cũ
        /// </summary>
        public string tag { get; set; }
    }

    public class DataModel
    {
        public Guid Id { get; set; }
    }
}