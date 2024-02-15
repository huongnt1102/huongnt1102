using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Class
{
    public class ZIAR008In_Content
    {
        public string LOAIPHIEU { get; set; }
        public string SOHOADON { get; set; }
        public string SOHOPDONG { get; set; }
        public string COMPANY { get; set; }
        public string CURRENCY { get; set; }
        public string ZBPLS { get; set; }
        public string DOCTEXT { get; set; }
        public string POSTDATE { get; set; }
        public string TYPE { get; set; }
        public string SYSTEM { get; set; }
        public string NHIEUKY { get; set; }
        public List<ZIAR008In_DetailItem> DETAIL_ITEM { get; set; }
    }
}
