using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Class
{
    public class ZIAR008In_DetailItem
    {
        public string ITEM { get; set; } // Item này để là 1, chắc là số thứ tự
        public string SOCONGNO { get; set; }
        public string ZZDICHVU { get; set; }
        public string ZZDUAN { get; set; }
        public string ZZTIEUDA { get; set; }
        public string ZZKHU_TOA { get; set; }
        public string ZZTANG_PK { get; set; }
        public string ZZCAN { get; set; }
        public string AMOUNT { get; set; }
        public string TAX_AMOUNT { get; set; }
        public string TAXMT_AMOUNT { get; set; }
        public string ZZCUDANCDT { get; set; }
    }
}
