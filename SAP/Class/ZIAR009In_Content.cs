using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Class
{
    public class ZIAR009In_Content
    {
        public string LOAIPHIEU { get; set; }
        public string SOPHIEU { get; set; }
        public string SOHOPDONG { get; set; }
        public string COMPANY {get;set;}
        public string CURRENCY {get;set;}
        public string DOCTEXT { get; set; }
        public string POSTDATE { get; set; }
        public string TYPE { get; set; }
        public string SYSTEM { get; set; }

        public List<ZIAR009In_DetailItem> DETAIL_ITEM { get; set; }
    }
}
