using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIPCRM
{
    public class CallDIPSupport
    {
        public static void Call(string name, string email, string clientNo, string clientPass)
        {
            //string clientNo = "", clientPass = "", email = "", name = "";
            //using (var db = new DataEntity.MasterDataContext())
            //{
            //    db.Support_getAccount(ref name, ref email, ref clientNo, ref clientPass);

            //}


            String _sData = String.Format("\"No\":\"{0}\", \"Name\":\"{1}\", \"Email\":\"{2}\", \"Pass\":\"{3}\"", clientNo, name, email, clientPass);
            _sData = "{" + _sData + "}";

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.Unicode.GetBytes(_sData);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "DIPSupport", "DIPCRM.Support.exe");
            p.StartInfo.Arguments = returnValue;
            p.Start();
        }
    }
}
