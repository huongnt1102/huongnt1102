using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Newtonsoft.Json;

namespace SAP.Funct
{
    public class SyncHoaDon
    {
        public static string SAP_HD { get; set; }
        public static string SAP_MSG { get; set; }

        public static void DongBo(Class.ZIAR008List Data)
        {
            SAP_HD = "";
            SAP_MSG = "";

            var item = new Class.System<Class.ZIAR008In_Content>()
            {
                HEADER = new Class.Header()
                {
                    ID = Data.ID,
                    IF_CODE = "ZIAR008",
                    ACTION = "POST",
                    BASE_64 = ""
                },
                CONTENT = new Class.ZIAR008In_Content()
                {
                    LOAIPHIEU = Data.LOAIPHIEU,
                    SOHOADON = Data.SOHOADON,
                    SOHOPDONG = Data.SOHOPDONG,
                    COMPANY = Data.COMPANY,
                    CURRENCY = Data.CURRENCY,
                    ZBPLS = Data.ZBPLS,
                    DOCTEXT = Data.DOCTEXT,
                    POSTDATE = Data.POSTDATE,
                    TYPE = Data.TYPE,
                    SYSTEM = Data.SYSTEM,
                    NHIEUKY = Data.NHIEUKY,
                    DETAIL_ITEM = new List<Class.ZIAR008In_DetailItem>()
                    {
                        new  Class.ZIAR008In_DetailItem()
                        {
                            ITEM = Data.ITEM,
                            SOCONGNO = Data.SOCONGNO,
                            ZZDICHVU = Data.ZZDICHVU,
                            ZZDUAN = Data.ZZDUAN,
                            ZZKHU_TOA = Data.ZZKHU_TOA,
                            ZZTANG_PK = Data.ZZTANG_PK,
                            ZZCAN = Data.ZZCAN,
                            AMOUNT = Convert.ToString(Data.AMOUNT),
                            TAX_AMOUNT = Convert.ToString(Data.TAX_AMOUNT),
                            TAXMT_AMOUNT = Convert.ToString(Data.TAXMT_AMOUNT),
                            ZZCUDANCDT = Data.ZZCUDANCDT,
                            ZZTIEUDA = Data.ZZTIEUDA
                        }
                    }
                }
            };

            var PostItem = RequestApi.post_item(item, 801, Data.SOCONGNO);

            var JsonItem = JsonConvert.DeserializeObject<Class.Result<Class.Row<Class.Document>>>(PostItem);

            if (JsonItem.RESULT != null)
            {
                if (JsonItem.RESULT.ROW.Count() > 0)
                {
                    var RowFirst = JsonItem.RESULT.ROW.FirstOrDefault();
                    if (RowFirst != null)
                    {
                        //if (RowFirst.MSG_TYPE == "S")
                        //{
                            Library.Class.Connect.QueryConnect.QueryData<bool>("sapin_ZIAR008_Update",
                                new
                                {
                                    SOCONGNO = Data.SOCONGNO,
                                    DOCUMENT = RowFirst.DOCUMENT,
                                    MSG = RowFirst.MSG_DESC,
                                    MSG_TYPE = RowFirst.MSG_TYPE,
                                    TYPE = item.CONTENT.TYPE
                                });

                            SAP_HD = RowFirst.DOCUMENT;
                            SAP_MSG = RowFirst.MSG_DESC;
                        //}
                    }
                }
            }
        }
    }
}
