using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.SMSZalo
{
    public class ReplaceField
    {
        public static string ReplaceCongNo( Building.SMSZalo.Gui.frmSend.smsZalo kh, string text)
        {
            #region Công nợ
            var Thang = kh.Thang;
            text = ReplaceFirstOccurrence(text, "Thang", Thang.ToString());
            var Nam = kh.Nam;
            text = ReplaceFirstOccurrence(text, "Nam", Nam.ToString());

            var NoDauKy = kh.NoDauKy != null ? string.Format("{0:n0}", kh.NoDauKy) : "0";
            text = ReplaceFirstOccurrence(text, "NoDauKy", NoDauKy);
            var PhatSinh = kh.PhatSinh != null ? string.Format("{0:n0}", kh.PhatSinh) : "0";
            text = ReplaceFirstOccurrence(text, "PhatSinh", PhatSinh);
            var DaThu = kh.DaThu != null ? string.Format("{0:n0}", kh.DaThu) : "0";
            text = ReplaceFirstOccurrence(text, "DaThu", DaThu);
            var KhauTru = kh.KhauTru != null ? string.Format("{0:n0}", kh.KhauTru) : "0";
            text = ReplaceFirstOccurrence(text, "KhauTru", KhauTru);
            var ConNo = kh.ConNo != null ? string.Format("{0:n0}", kh.ConNo) : "0";
            text = ReplaceFirstOccurrence(text, "ConNo", ConNo);
            var NoCuoi = kh.NoCuoi != null ? string.Format("{0:n0}", kh.NoCuoi) : "0";
            text = ReplaceFirstOccurrence(text, "NoCuoi", NoCuoi);
            #endregion
            return text;
        }

        public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            if (Place == -1)
            {
                return Source;
            }
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }
    }
}
