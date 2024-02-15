using Library;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SAP.Funct
{
    public class SyncCus
    {
        public static void DongBo_N_KH(byte? MaTN)
        {
            try
            {
                #region Lấy dữ liệu khách hàng

                // Get dữ liệu
                var objZIMD005Data = Library.Class.Connect.QueryConnect.QueryData<Class.ZIMD005Data>("sapin_ZIMD005Data", new { MaTN = MaTN});

                DongBoKH(objZIMD005Data);

                #endregion
            }
            catch (System.Exception ex) { }
        }

        public static void DongBo_1_KH(int? MaKH)
        {
            try
            {
                #region Lấy dữ liệu khách hàng

                // Get dữ liệu
                var objZIMD005Data = Library.Class.Connect.QueryConnect.QueryData<Class.ZIMD005Data>("sapin_ZIMD005Data_1", new { MaKH = MaKH });

                DongBoKH(objZIMD005Data);

                #endregion
            }
            catch (System.Exception ex) { }
        }

        public static void DongBoKH(IEnumerable<Class.ZIMD005Data> objZIMD005Data)
        {
            try
            {
                #region Lấy dữ liệu khách hàng

                foreach (var cus in objZIMD005Data)
                {
                    //var cus = objZIMD005Data.First();
                    var item = new Class.System<Class.ZIMD005In>()
                    {
                        HEADER = new Class.Header()
                        {
                            ID = Convert.ToString(cus.ID),
                            IF_CODE = "ZIMD005",
                            ACTION = "POST",
                            BASE_64 = ""
                        },
                        CONTENT = new Class.ZIMD005In()
                        {
                            ZBPLS = cus.ZBPLS,
                            BU_GROUP = cus.BU_GROUP,
                            TYPE = cus.TYPE,
                            TITLE = cus.TITLE,
                            SHORTNAME = cus.SHORTNAME,
                            FULLNAME = cus.FULLNAME,
                            DC_TT = cus.DC_TT,
                            DC_LH = cus.DC_LH,
                            CITY2 = cus.CITY2,
                            CITY1 = cus.CITY1,
                            COUNTRY = cus.COUNTRY,
                            TEL_NUMBER = cus.TEL_NUMBER,
                            SMTP_ADDR = cus.SMTP_ADDR,
                            IDNUMBER = cus.IDNUMBER,
                            I_TYPE = cus.I_TYPE,
                            SYSTEM = cus.SYSTEM
                        }
                    };

                    // Post dữ liệu
                    var objZimd005Content = RequestApi.post_item(item, 500, Convert.ToString(cus.MaKH));

                    var objZimd005Json = JsonConvert.DeserializeObject<Class.Result<Class.ZIMD005Out>>(objZimd005Content);

                    // Update mã sap to customer
                    Library.Class.Connect.QueryConnect.QueryData<bool>("sapin_ZIMD005Data_Update", new
                    {
                        MaKH = cus.MaKH,
                        PARTNER = objZimd005Json.RESULT.PARTNER
                    });

                }
                DialogBox.Success();

                #endregion
            }
            catch (System.Exception ex) { DialogBox.Error(ex.Message); }
        }

        public static void DongBo_N_MB(byte? MaTN)
        {
            try
            {
                #region Lấy dữ liệu khách hàng

                // Get dữ liệu
                var objZIMD007Data = Library.Class.Connect.QueryConnect.QueryData<Class.ZIMD007Data>("sapin_ZIMD007Data", new { MaTN = MaTN });

                if (objZIMD007Data.Count() > 0)

                    DongBoMB(objZIMD007Data);

                #endregion
            }
            catch (System.Exception ex) { }
        }

        public static void DongBo_1_MB(int? MaMB)
        {
            try
            {
                #region Lấy dữ liệu khách hàng

                // Get dữ liệu
                var obj = Library.Class.Connect.QueryConnect.QueryData<Class.ZIMD007Data>("sapin_ZIMD007Data_1", new { MaMB = MaMB });

                if (obj.Count() > 0)

                    DongBoMB(obj);

                #endregion
            }
            catch (System.Exception ex) { }
        }

        public static void DongBoMB (IEnumerable<Class.ZIMD007Data> zIMD007Datas)
        {
            try
            {
                foreach(var mb in zIMD007Datas)
                {
                    var item = new Class.System<Class.ZIMD007In>()
                    {
                        HEADER = new Class.Header()
                        {
                            ID = Convert.ToString(mb.ID_KEY),
                            IF_CODE = "ZIMD007",
                            ACTION = "POST",
                            BASE_64 = ""
                        },
                        CONTENT = new Class.ZIMD007In()
                        {
                            ZZDUAN = mb.ZZDUAN,
                            CSHLS = mb.CSHLS,
                            ZZCAN = mb.ZZCAN,
                            SYSTEM = mb.SYSTEM
                        }
                    };

                    var objContent = RequestApi.post_item(item, 700, Convert.ToString(mb.MaMB));

                    var objJson = JsonConvert.DeserializeObject<Class.Result<Class.Out>>(objContent);

                    if(objJson.RESULT.MSG_TYPE == "S")
                    {
                        // Update mã sap to customer
                        Library.Class.Connect.QueryConnect.QueryData<bool>("sapin_ZIMD007Data_Update", new
                        {
                            MaKH = mb.MaMB
                        });
                    }
                        
                }

                DialogBox.Success();
            }
            catch(System.Exception ex) { }
        }

        public static void KiemTraDongBoSAP(int? MaKH, string TenTN)
        {
            try
            {
                // Get dữ liệu
                var objKhachHangs = Library.Class.Connect.QueryConnect.QueryData<Class.ZIMD009Data>("sapin_GetDataKiemTraTonTaiKH", new { MaKH = MaKH });

                if (objKhachHangs.Count() > 0)
                {
                    var objKhachHang = objKhachHangs.First();

                    var item = new Class.System<Class.ZIMD009In>()
                    {
                        HEADER = new Class.Header()
                        {
                            ID = Convert.ToString(objKhachHang.ID),
                            IF_CODE = "ZIMD009",
                            ACTION = "POST",
                            BASE_64 = ""
                        },
                        CONTENT = new Class.ZIMD009In()
                        {
                            ZBPLS = objKhachHang.KyHieu,
                            I_TYPE = objKhachHang.I_TYPE,
                            IDNUMBER = objKhachHang.IDNUMBER,
                            SYSTEM = "LS"
                        }
                    };

                    //DialogBox.Alert("Chuản bị dữ liệu đồng bộ thành công");
                    // Post dữ liệu
                    var objZimd009Content = RequestApi.post_item(item, 901, Convert.ToString(MaKH));
                    //DialogBox.Alert("Gọi api đồng bộ thành công");

                    var objZimd009Json = JsonConvert.DeserializeObject<Class.Result<Class.ZIMD009Out>>(objZimd009Content);
                    //DialogBox.Alert("Đang lưu kết quả đồng bộ");

                    ////DialogBox.Success(JsonConvert.SerializeObject
                    //                            (
                    //                                 objZimd009Content
                    //                            ));
                    // kiểm tra và lưu khách hàng sap
                    if (objZimd009Json.RESULT != null)
                    {
                        //DialogBox.Alert("objZimd009Json.RESULT != null");
                        if (objZimd009Json.RESULT.MSG_TYPE == "S")
                        {
                            // Gần giống Zimd009 update, nhưng k có các msg
                            //DialogBox.Alert(objZimd009Json.RESULT.MSG_TYPE);
                            Library.Class.Connect.QueryConnect.QueryData<string>("sapin_ZIMD009_Update", new
                            {
                                BU_GROUP = objZimd009Json.RESULT.BU_GROUP,
                                CITY1 = objZimd009Json.RESULT.CITY1,
                                CITY2 = objZimd009Json.RESULT.CITY2,
                                DC_LH = objZimd009Json.RESULT.DC_LH,
                                DC_TT = objZimd009Json.RESULT.DC_TT,
                                FULLNAME = objZimd009Json.RESULT.FULLNAME,
                                I_TYPE = objZimd009Json.RESULT.I_TYPE,
                                IDNUMBER = objZimd009Json.RESULT.IDNUMBER,
                                LAND1 = objZimd009Json.RESULT.LAND1,
                                PARTNER = objZimd009Json.RESULT.PARTNER,
                                SHORTNAME = objZimd009Json.RESULT.SHORTNAME,
                                SMTP_ADDR = objZimd009Json.RESULT.SMTP_ADDR,
                                TEL_NUMBER = objZimd009Json.RESULT.TEL_NUMBER,
                                TITLE = objZimd009Json.RESULT.TITLE,
                                ZZDUAN = TenTN,
                                TYPE = objZimd009Json.RESULT.TYPE
                            });
                        }
                    }

                    DialogBox.Success(objZimd009Json.RESULT.MSG_DESC);
                }

            }
            catch (System.Exception ex) { DialogBox.Alert(ex.Message); }
        }


        public static void KiemTraDongBoSAPByIdnumber(string I_Type, string IdNumber, string TenTN)
        {
            try
            {
                var item = new Class.System<Class.ZIMD009In>()
                {
                    HEADER = new Class.Header()
                    {

                        ID = Convert.ToString(Guid.NewGuid()),
                        IF_CODE = "ZIMD009",
                        ACTION = "POST",
                        BASE_64 = ""
                    },
                    CONTENT = new Class.ZIMD009In()
                        {
                            ZBPLS = "",
                            I_TYPE = I_Type,
                            IDNUMBER = IdNumber,
                            SYSTEM = "LS"
                        }

                };
                // Get mã khách hàng theo 
                // Post dữ liệu
                var objZimd009Content = RequestApi.post_item(item, 901, "0");

                var objZimd009Json = JsonConvert.DeserializeObject<Class.Result<Class.ZIMD009Out>>(objZimd009Content);

                // kiểm tra và lưu khách hàng sap
                if (objZimd009Json.RESULT != null)
                {
                    if (objZimd009Json.RESULT.MSG_TYPE == "S")
                    {
                        // Gần giống Zimd009 update, nhưng k có các msg
                        Library.Class.Connect.QueryConnect.QueryData<string>("sapin_ZIMD009_Update", new
                        {
                            BU_GROUP = objZimd009Json.RESULT.BU_GROUP,
                            CITY1 = objZimd009Json.RESULT.CITY1,
                            CITY2 = objZimd009Json.RESULT.CITY2,
                            DC_LH = objZimd009Json.RESULT.DC_LH,
                            DC_TT = objZimd009Json.RESULT.DC_TT,
                            FULLNAME = objZimd009Json.RESULT.FULLNAME,
                            I_TYPE = objZimd009Json.RESULT.I_TYPE,
                            IDNUMBER = objZimd009Json.RESULT.IDNUMBER,
                            LAND1 = objZimd009Json.RESULT.LAND1,
                            PARTNER = objZimd009Json.RESULT.PARTNER,
                            SHORTNAME = objZimd009Json.RESULT.SHORTNAME,
                            SMTP_ADDR = objZimd009Json.RESULT.SMTP_ADDR,
                            TEL_NUMBER = objZimd009Json.RESULT.TEL_NUMBER,
                            TITLE = objZimd009Json.RESULT.TITLE,
                            ZZDUAN = TenTN,
                            TYPE = objZimd009Json.RESULT.TYPE
                        });
                    }
                    
                }

                DialogBox.Success(objZimd009Json.RESULT.MSG_DESC);
            }
            catch (System.Exception ex) { }
        }

    }
}
