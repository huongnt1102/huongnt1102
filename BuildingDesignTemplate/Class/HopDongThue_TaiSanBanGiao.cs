using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingDesignTemplate.Class
{
    public class HopDongThue_TaiSanBanGiao
    {
        public static string Merge(long? mahd, string rtfText)
        {
            if (mahd == 0) return rtfText;

            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = rtfText };

            try
            {
                var document = ctlRtf.Document;

                ctlRtf.Document.RtfText = Public(ctlRtf);

                var tableList = (from p in document.Tables
                                 select new { cellName = document.GetText(p.Rows[0].Cells[0].Range).Replace(" ", "") }).AsEnumerable().Select((t, indexs) => new Field { Index = indexs, Name = t.cellName }).ToList();
                ctlRtf = DieuHuong("[bhdt]", ctlRtf, tableList);
                return ctlRtf.Document.RtfText;
            }
            catch(System.Exception ex) { }

            return rtfText;
        }

        public static Field GetField(List<Field> fields, string name)
        {
            return fields.FirstOrDefault(_ => _.Name.Contains(name));
        }

        public static DevExpress.XtraRichEdit.RichEditControl DieuHuong(string name, DevExpress.XtraRichEdit.RichEditControl ctlRtf,  List<Field> fields)
        {
            Field field = GetField(fields, name);
            if (field != null)
            {
                ctlRtf.Document.ReplaceAll("[bhdt]", "", SearchOptions.None);
                //switch (name)
                //{
                //    case "[bhdt]":
                //        //ctlRtf.Document.RtfText = glex_tb(ma_tn, thang, nam, ma_kh, ctlRtf, con_no, field, document);
                //        ctlRtf.Document.ReplaceAll("[bhdt]", "", SearchOptions.None);
                //        break;
                //}
            }
            return ctlRtf;
        }

        public static string Public(DevExpress.XtraRichEdit.RichEditControl ctlRtf)
        {
            //var dau_thang = new DateTime(nam, thang, 1);
            //var cuoi_thang_1 = dau_thang.AddDays(-1);
            //var thang_2 = dau_thang.AddMonths(-2);
            //var cuoi_thang = dau_thang.AddMonths(1).AddDays(-1);
            //var thang_sau = dau_thang.AddMonths(1);

            //ctlRtf.Document.ReplaceAll("[Nam]", nam.ToString(), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[Thang]", dau_thang.Month.ToString("00"), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[Thang1]", cuoi_thang_1.Month.ToString("00"), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[NgayCuoiThang1]", cuoi_thang_1.Day.ToString("00"), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[NgayCuoiThang]", cuoi_thang.Day.ToString("00"), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[Nam1]", cuoi_thang_1.Year.ToString(), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[Thang2]", thang_2.Month.ToString("00"), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[Nam2]", thang_2.Year.ToString(), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[ngayin]", System.DateTime.Now.Day.ToString(), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[thangin]", System.DateTime.Now.Month.ToString(), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[namin]", System.DateTime.Now.Year.ToString(), SearchOptions.None);
            //ctlRtf.Document.ReplaceAll("[thang_sau]", thang_sau.Month.ToString("00"), SearchOptions.None);

            //var model = new { ma_kh = ma_kh, ma_mb = 0 };
            //var param = new Dapper.DynamicParameters();
            //param.AddDynamicParams(model);
            //var result = Library.Class.Connect.QueryConnect.Query<dv_hoadon_thong_bao_phi_kh>("dv_hoadon_thong_bao_phi_kh", param);

            //if (result.Count() > 0)
            //{
            //    var item = result.First();
            //    ctlRtf.Document.ReplaceAll("[TenKH]", item.TenKH != null ? item.TenKH : "", SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[MaSoMB]", item.MaSoMB != null ? item.MaSoMB : "", SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[TenTL]", item.TenTL != null ? item.TenTL : "", SearchOptions.None);
            //    ctlRtf.Document.ReplaceAll("[MaPhu]", item.MaPhu != null ? item.MaPhu : "", SearchOptions.None);
            //}


            return ctlRtf.Document.RtfText;
        }
    }
}
