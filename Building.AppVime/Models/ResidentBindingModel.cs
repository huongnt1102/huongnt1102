using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Building.AppVime
{
    public class ResidentBindingModel
    {
        public string PhoneNumber { get; set; }

        public string DisplayName { get; set; }
    }

    public class ResidentRegisterBindingModel
    {
        public string phoneNumber { get; set; }

        public string displayName { get; set; }

        public string apiKey { get; set; }

        public string secretKey { get; set; }
    }

    public class ResidentRegisterBindingModelNoId
    {
        public string phoneNumber { get; set; }

        public string displayName { get; set; }

        public string apiKey { get; set; }

        public string secretKey { get; set; }

        public int idNew { get; set; }
       // public bool isPersonal { get; set; } = false;
    }
}
