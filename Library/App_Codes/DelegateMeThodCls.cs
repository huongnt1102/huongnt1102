using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.App_Codes
{
    public static class DelegateMeThodCls
    {
        public delegate void CoHoiView_dlg(int? maLH);

        public static CoHoiView_dlg CoHoiView;
    }
}
