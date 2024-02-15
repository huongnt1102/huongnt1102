using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Library.Class.ThongBao
{
    public class ThongBaoNhanVien
    {
        public class ThongBaoReturn
        {
            public bool IsView { get; set; }
            public string Description { get; set; }
            public bool IsError { get; set; }
        }

        public class ThongBaoParam
        {
            public string KeyGroup { get; set; }
            public int? UserId { get; set; }
            public string Description { get; set; }
        }

        public static ThongBaoReturn CheckThongBao(ThongBaoParam tham_so)
        {
            ThongBaoReturn thongBaoReturn = new ThongBaoReturn();
            try
            {
                var param = new DynamicParameters();
                param.AddDynamicParams(tham_so);
                var a = Library.Class.Connect.QueryConnect.Query<ThongBaoReturn>("dbo.notify_program", param);
                thongBaoReturn = a.First();
                thongBaoReturn.IsError = false;
            }
            catch(Exception ex) { thongBaoReturn.IsView = false; thongBaoReturn.IsError = true; thongBaoReturn.Description = ex.Message; }

            return thongBaoReturn;
        }
    }
}
