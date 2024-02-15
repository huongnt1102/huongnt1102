using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.CongNoCls
{
    public class ChuKyCls
    {
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }

        public ChuKyCls() { }

        public ChuKyCls(DateTime _Min, DateTime _Max)
        {
            Min = _Min;
            Max = _Max;
        }
    }
}
