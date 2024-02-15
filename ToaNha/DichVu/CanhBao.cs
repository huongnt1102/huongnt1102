using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToaNha.DichVu
{
    public class CanhBao
    {
        public class chi_so_canh_bao
        {
            public decimal? CanhBaoDien { get; set; }
            public decimal? CanhBaoNuoc { get; set; }
            public decimal? CanhBaoGas { get; set; }
        }

        public class canh_bao_list
        {
            public string Name { get; set; }
            public decimal? CanhBao { get; set; }
        }

        public static chi_so_canh_bao GetChiSo(byte? matn)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@matn", matn, System.Data.DbType.Byte, null, null);
            var kq = Library.Class.Connect.QueryConnect.Query<chi_so_canh_bao>("dbo.tntoanha_get_canh_bao", param);
            return kq.First();
        }

        public static void SaveChiSo(byte? matn, chi_so_canh_bao cs)
        {
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(cs);
            param.Add("@matn", matn, System.Data.DbType.Byte, null, null);
            var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.tntoanha_set_canh_bao", param);
        }

        public static List<canh_bao_list> GetCanh_Bao_Lists(byte? matn)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@matn", matn, System.Data.DbType.Byte, null, null);
            var kq= Library.Class.Connect.QueryConnect.Query<canh_bao_list>("dbo.tntoanha_set_canh_bao_list", param).ToList();
            return kq;
        }
    }
}
