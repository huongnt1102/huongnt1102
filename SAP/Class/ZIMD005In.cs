using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Class
{
    public class ZIMD005In : In
    {
        public string ZBPLS { get; set; }
        public string BU_GROUP { get; set; }
        public string TYPE { get; set; }
        public string TITLE { get; set; }
        public string SHORTNAME { get; set; }
        public string FULLNAME { get; set; }
        public string DC_TT { get; set; }
        public string DC_LH { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public string CITY2 { get; set; }
        /// <summary>
        /// Tỉnh
        /// </summary>
        public string CITY1 { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string COUNTRY { get; set; }
        public string TEL_NUMBER { get; set; }
        public string SMTP_ADDR { get; set; }
    }
}
