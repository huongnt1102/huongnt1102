using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Newtonsoft.Json;

namespace SAP.Funct
{
    public class SyncPhieuThu
    {
        public static string SAP_PT { get; set; }
        public static string SAP_MSG { get; set; }

        public static bool DongBo(Class.ZIAR009List Data)
        {
            SAP_PT = "";
            SAP_MSG = "";

            var item = new Class.System<Class.ZIAR009In_Content>()
            {
                HEADER = new Class.Header()
                {
                    ID = Convert.ToString( Data.ID),
                    IF_CODE = "ZIAR009",
                    ACTION = "POST",
                    BASE_64 = ""
                },
                CONTENT = new Class.ZIAR009In_Content()
                {
                    LOAIPHIEU = Data.LOAIPHIEU,
                    SOPHIEU = Data.SOPHIEU,
                    SOHOPDONG = Data.SOHOPDONG,
                    COMPANY = Data.COMPANY,
                    CURRENCY = Data.CURRENCY,
                    DOCTEXT = Data.DOCTEXT,
                    POSTDATE = Data.POSTDATE,
                    TYPE = Data.TYPE,
                    SYSTEM = Data.SYSTEM,
                    DETAIL_ITEM  = new List<Class.ZIAR009In_DetailItem>()
                    {
                        new  Class.ZIAR009In_DetailItem()
                        {
                            ITEM = Data.ITEM,
                            SOCONGNO = Data.SOCONGNO,
                            ZBPLS = Data.ZBPLS,
                            ZZDICHVU = Data.ZZDICHVU,
                            ZZDUAN = Data.ZZDUAN,
                            ZZKHU_TOA = Data.ZZKHU_TOA,
                            ZZTANG_PK = Data.ZZTANG_PK,
                            ZZCAN = Data.ZZCAN,
                            AMOUNT = Convert.ToString(Data.AMOUNT),
                            BANK_ACCOUNT = Data.BANK_ACCOUNT,
                            HOUSE_BANK = Data.HOUSE_BANK,
                            ZZTIEUDA = Data.ZZTIEUDA
                        }
                    }
                }
            };

            var PostItem = RequestApi.post_item(item, 901, Convert.ToString( Data.IDKEY));

            var JsonItem = JsonConvert.DeserializeObject<Class.Result<Class.Row<Class.Document>>>(PostItem);

            bool IsThanhCong = true;

            if (JsonItem.RESULT != null)
            {
                if (JsonItem.RESULT.ROW.Count() > 0)
                {
                    var RowFirst = JsonItem.RESULT.ROW.FirstOrDefault();
                    if (RowFirst != null)
                    {
                        //if (RowFirst.MSG_TYPE == "S")
                        //{
                            Library.Class.Connect.QueryConnect.QueryData<bool>("sapin_ZIAR009_Update",
                                new
                                {
                                    SOCONGNO = Convert.ToString(Data.IDKEY),
                                    DOCUMENT = RowFirst.DOCUMENT,
                                    MSG = RowFirst.MSG_DESC,
                                    TYPE = item.CONTENT.TYPE,
                                    MSG_TYPE = RowFirst.MSG_TYPE
                                });

                            SAP_PT = RowFirst.DOCUMENT;
                            SAP_MSG = RowFirst.MSG_DESC;

                        if (RowFirst.MSG_TYPE != "S") IsThanhCong = false;
                        //}
                    }
                }
            }

            return IsThanhCong;
        }
    }
}
