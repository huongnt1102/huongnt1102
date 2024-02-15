using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.AppExtension.Model
{
    public class ExtensionModel
    {
        public class ManagementContact
        {
            public string Content { get; set; }
        }

        public class VanChuyenModel
        {
            public bool? Duyet { get; set; }
            public int? manvDuyet { get; set; }
            public string traLoi { get; set; }
        }

        public class DangKyModel
        {
            public bool? Duyet { get; set; }
            public int? manvDuyet { get; set; }
            public string traLoi { get; set; }
        }
        public class BaoGiaSCModel
        {
            public int? nvqlTraLoi { get; set; }
            public string bqlTraLoi { get; set; }
        }
        public class YeuCauSCModel
        {
            public bool? Duyet { get; set; }
            public int? manvbql { get; set; }
            public string traLoi { get; set; }
        }
    }
}
