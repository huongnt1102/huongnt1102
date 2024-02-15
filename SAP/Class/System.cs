using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Class
{
    public class System<T>
    {
        public Header HEADER { get; set; }
        public T CONTENT { get; set; }
    }
}
