using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Class.Connect
{
    public class QueryConnect
    {
        public static IEnumerable<T> Query<T>(string sql, DynamicParameters param = null, bool isStoreProcedure = true)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Library.Properties.Settings.Default.Building_dbConnectionString))
            {
                conn.Open();
                return conn.Query<T>(sql, param, commandType: isStoreProcedure ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text);
            }
        }

        public static System.Collections.Generic.IEnumerable<T> QueryAsyncString<T>(string sql, string _connectString, DynamicParameters param = null, bool isStoreProcedure = true)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                conn.Open();
                return conn.Query<T>(sql, param, commandType: isStoreProcedure ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text);
            }
        }

        public static IEnumerable<T> QueryData<T>(string store, object model = null)
        {
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            return Library.Class.Connect.QueryConnect.Query<T>(store, param);
        }
    }
}
