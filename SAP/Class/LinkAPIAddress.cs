using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Class
{
    public class LinkAPIAddress
    {
        public Guid ID { get; set; }

        public int? TypeID { get; set; }

        public string TypeName { get; set; }

        public string LinkAPI { get; set; }

        public string Directional { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
