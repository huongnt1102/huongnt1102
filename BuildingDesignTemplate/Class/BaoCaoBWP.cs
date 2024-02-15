using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingDesignTemplate.Class
{
    public static class BaoCaoBWP
    {
        public static string _1_KH_Thu(string contents, byte? matn, int nam, int thang)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = contents };

            var tuNgay = new DateTime(nam, thang, 1);

            var model = new { MaTN = matn, TuNgay = tuNgay };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            var result = Library.Class.Connect.QueryConnect.Query<bc_1_khthu>("bc_1_khthu", param);

            if (result.Count() > 0)
            {
                // tổng
                var item_1 = result.FirstOrDefault(_ => _.STT == 1);
                ctlRtf.Document.ReplaceAll("[n_1]", string.Format("{0:#,0}", item_1.N != null ? item_1.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_1]", string.Format("{0:#,0}", item_1.D != null ? item_1.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_1]", string.Format("{0:#,0}", item_1.T != null ? item_1.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // I

                var item_2 = result.FirstOrDefault(_ => _.STT == 2);
                ctlRtf.Document.ReplaceAll("[n_2]", string.Format("{0:#,0}", item_2.N != null ? item_2.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_2]", string.Format("{0:#,0}", item_2.D != null ? item_2.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_2]", string.Format("{0:#,0}", item_2.T != null ? item_2.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // I.4,5,6

                var item_4 = result.FirstOrDefault(_ => _.STT == 4);
                ctlRtf.Document.ReplaceAll("[n_4]", string.Format("{0:#,0}", item_4.N != null ? item_4.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_4]", string.Format("{0:#,0}", item_4.D != null ? item_4.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_4]", string.Format("{0:#,0}", item_4.T != null ? item_4.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_5 = result.FirstOrDefault(_ => _.STT == 5);
                ctlRtf.Document.ReplaceAll("[n_5]", string.Format("{0:#,0}", item_5.N != null ? item_5.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_5]", string.Format("{0:#,0}", item_5.D != null ? item_5.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_5]", string.Format("{0:#,0}", item_5.T != null ? item_5.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_6 = result.FirstOrDefault(_ => _.STT == 6);
                ctlRtf.Document.ReplaceAll("[n_6]", string.Format("{0:#,0}", item_6.N != null ? item_6.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_6]", string.Format("{0:#,0}", item_6.D != null ? item_6.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_6]", string.Format("{0:#,0}", item_6.T != null ? item_6.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // II

                var item_7 = result.FirstOrDefault(_ => _.STT == 7);
                ctlRtf.Document.ReplaceAll("[n_7]", string.Format("{0:#,0}", item_7.N != null ? item_7.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_7]", string.Format("{0:#,0}", item_7.D != null ? item_7.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_7]", string.Format("{0:#,0}", item_7.T != null ? item_7.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // II. 9,10,11

                var item_9 = result.FirstOrDefault(_ => _.STT == 9);
                ctlRtf.Document.ReplaceAll("[n_9]", string.Format("{0:#,0}", item_9.N != null ? item_9.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_9]", string.Format("{0:#,0}", item_9.D != null ? item_9.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_9]", string.Format("{0:#,0}", item_9.T != null ? item_9.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_10 = result.FirstOrDefault(_ => _.STT == 10);
                ctlRtf.Document.ReplaceAll("[n_10]", string.Format("{0:#,0}", item_10.N != null ? item_10.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_10]", string.Format("{0:#,0}", item_10.D != null ? item_10.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_10]", string.Format("{0:#,0}", item_10.T != null ? item_10.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_11 = result.FirstOrDefault(_ => _.STT == 11);
                ctlRtf.Document.ReplaceAll("[n_11]", string.Format("{0:#,0}", item_11.N != null ? item_11.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_11]", string.Format("{0:#,0}", item_11.D != null ? item_11.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_11]", string.Format("{0:#,0}", item_11.T != null ? item_11.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // IV
                var item_16 = result.FirstOrDefault(_ => _.STT == 16);
                ctlRtf.Document.ReplaceAll("[n_16]", string.Format("{0:#,0}", item_16.N != null ? item_16.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_16]", string.Format("{0:#,0}", item_16.D != null ? item_16.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_16]", string.Format("{0:#,0}", item_16.T != null ? item_16.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // IV. 17

                var item_17 = result.FirstOrDefault(_ => _.STT == 17);
                ctlRtf.Document.ReplaceAll("[n_17]", string.Format("{0:#,0}", item_17.N != null ? item_17.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[d_17]", string.Format("{0:#,0}", item_17.D != null ? item_17.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[t_17]", string.Format("{0:#,0}", item_17.T != null ? item_17.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            }

            return ctlRtf.Document.RtfText;
        }

        public static string _2_KH_Thu(string contents, byte? matn, int nam, int thang)
        {
            var ctlRtf = new DevExpress.XtraRichEdit.RichEditControl { RtfText = contents };

            var tuNgay = new DateTime(nam, thang, 1);
            var cuoi_thang = tuNgay.AddMonths(1).AddDays(-1);

            ctlRtf.Document.ReplaceAll("[nam]", nam.ToString(), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[thang]", thang.ToString("00"), SearchOptions.None);
            ctlRtf.Document.ReplaceAll("[ngaycuoithang]", cuoi_thang.Day.ToString("00"), SearchOptions.None);

            var model = new { MaTN = matn, TuNgay = tuNgay };
            var param = new Dapper.DynamicParameters();
            param.AddDynamicParams(model);
            var result = Library.Class.Connect.QueryConnect.Query<bc_2_khthu>("bc_2_khthu", param);

            if (result.Count() > 0)
            {
                //// tổng
                var item_1 = result.FirstOrDefault(_ => _.STT == 1);
                ctlRtf.Document.ReplaceAll("[pt_1]", string.Format("{0:#,0}", item_1.PhaiThu != null ? item_1.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_1]", string.Format("{0:#,0}", item_1.DaThu != null ? item_1.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_1]", string.Format("{0:#,0}", item_1.TyLe != null ? item_1.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_1]", string.Format("{0:#,0}", item_1.ConNo != null ? item_1.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tdt_1]", string.Format("{0:#,0}", item_1.DienTich != null ? item_1.DienTich : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // I

                var item_2 = result.FirstOrDefault(_ => _.STT == 2);
                ctlRtf.Document.ReplaceAll("[pt_2]", string.Format("{0:#,0}", item_2.PhaiThu != null ? item_2.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_2]", string.Format("{0:#,0}", item_2.DaThu != null ? item_2.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_2]", string.Format("{0:#,0}", item_2.TyLe != null ? item_2.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_2]", string.Format("{0:#,0}", item_2.ConNo != null ? item_2.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tdt_2]", string.Format("{0:#,0}", item_2.DienTich != null ? item_2.DienTich : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_7 = result.FirstOrDefault(_ => _.STT == 7);
                ctlRtf.Document.ReplaceAll("[pt_7]", string.Format("{0:#,0}", item_7.PhaiThu != null ? item_7.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_7]", string.Format("{0:#,0}", item_7.DaThu != null ? item_7.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_7]", string.Format("{0:#,0}", item_7.TyLe != null ? item_7.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_7]", string.Format("{0:#,0}", item_7.ConNo != null ? item_7.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tdt_7]", string.Format("{0:#,0}", item_7.DienTich != null ? item_7.DienTich : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_16 = result.FirstOrDefault(_ => _.STT == 16);
                ctlRtf.Document.ReplaceAll("[pt_16]", string.Format("{0:#,0}", item_16.PhaiThu != null ? item_16.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_16]", string.Format("{0:#,0}", item_16.DaThu != null ? item_16.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_16]", string.Format("{0:#,0}", item_16.TyLe != null ? item_16.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_16]", string.Format("{0:#,0}", item_16.ConNo != null ? item_16.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tdt_16]", string.Format("{0:#,0}", item_16.DienTich != null ? item_16.DienTich : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // I.3,4,5,6

                // Tiền đặt cọc chưa tìm ra loại nào
                ctlRtf.Document.ReplaceAll("[pt_3]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_3]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_3]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_3]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_4 = result.FirstOrDefault(_ => _.STT == 4);
                ctlRtf.Document.ReplaceAll("[pt_4]", string.Format("{0:#,0}", item_4.PhaiThu != null ? item_4.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_4]", string.Format("{0:#,0}", item_4.DaThu != null ? item_4.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_4]", string.Format("{0:#,0}", item_4.TyLe != null ? item_4.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_4]", string.Format("{0:#,0}", item_4.ConNo != null ? item_4.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_5 = result.FirstOrDefault(_ => _.STT == 5);
                ctlRtf.Document.ReplaceAll("[pt_5]", string.Format("{0:#,0}", item_5.PhaiThu != null ? item_5.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_5]", string.Format("{0:#,0}", item_5.DaThu != null ? item_5.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_5]", string.Format("{0:#,0}", item_5.TyLe != null ? item_5.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_5]", string.Format("{0:#,0}", item_5.ConNo != null ? item_5.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_6 = result.FirstOrDefault(_ => _.STT == 6);
                ctlRtf.Document.ReplaceAll("[pt_6]", string.Format("{0:#,0}", item_6.PhaiThu != null ? item_6.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_6]", string.Format("{0:#,0}", item_6.DaThu != null ? item_6.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_6]", string.Format("{0:#,0}", item_6.TyLe != null ? item_6.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_6]", string.Format("{0:#,0}", item_6.ConNo != null ? item_6.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // II

                // Tiền đặt cọc chưa tìm ra loại nào
                ctlRtf.Document.ReplaceAll("[pt_8]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_8]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_8]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_8]", string.Format("{0:#,0}", 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // II. 9,10,11

                var item_9 = result.FirstOrDefault(_ => _.STT == 9);
                ctlRtf.Document.ReplaceAll("[pt_9]", string.Format("{0:#,0}", item_9.PhaiThu != null ? item_9.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_9]", string.Format("{0:#,0}", item_9.DaThu != null ? item_9.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_9]", string.Format("{0:#,0}", item_9.TyLe != null ? item_9.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_9]", string.Format("{0:#,0}", item_9.ConNo != null ? item_9.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_10 = result.FirstOrDefault(_ => _.STT == 10);
                ctlRtf.Document.ReplaceAll("[pt_10]", string.Format("{0:#,0}", item_10.PhaiThu != null ? item_10.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_10]", string.Format("{0:#,0}", item_10.DaThu != null ? item_10.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_10]", string.Format("{0:#,0}", item_10.TyLe != null ? item_10.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_10]", string.Format("{0:#,0}", item_10.ConNo != null ? item_10.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                var item_11 = result.FirstOrDefault(_ => _.STT == 11);
                ctlRtf.Document.ReplaceAll("[pt_11]", string.Format("{0:#,0}", item_11.PhaiThu != null ? item_11.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_11]", string.Format("{0:#,0}", item_11.DaThu != null ? item_11.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_11]", string.Format("{0:#,0}", item_11.TyLe != null ? item_11.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_11]", string.Format("{0:#,0}", item_11.ConNo != null ? item_11.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                //// IV
                //var item_16 = result.FirstOrDefault(_ => _.STT == 16);
                //ctlRtf.Document.ReplaceAll("[n_16]", string.Format("{0:#,0}", item_16.N != null ? item_16.N : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                //ctlRtf.Document.ReplaceAll("[d_16]", string.Format("{0:#,0}", item_16.D != null ? item_16.D : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                //ctlRtf.Document.ReplaceAll("[t_16]", string.Format("{0:#,0}", item_16.T != null ? item_16.T : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);

                // IV. 17

                var item_17 = result.FirstOrDefault(_ => _.STT == 17);
                ctlRtf.Document.ReplaceAll("[pt_17]", string.Format("{0:#,0}", item_17.PhaiThu != null ? item_17.PhaiThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[dt_17]", string.Format("{0:#,0}", item_17.DaThu != null ? item_17.DaThu : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[tl_17]", string.Format("{0:#,0}", item_17.TyLe != null ? item_17.TyLe : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
                ctlRtf.Document.ReplaceAll("[cn_17]", string.Format("{0:#,0}", item_17.ConNo != null ? item_17.ConNo : 0), DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
            }

            return ctlRtf.Document.RtfText;
        }

        public class bc_1_khthu
        {
            public int? STT { get; set; }
            public decimal? N { get; set; }
            public decimal? D { get; set; }
            public decimal? T { get; set; }
            public string DienGiai { get; set; }
            public string Nhom { get; set; }
        }

        public class bc_2_khthu
        {
            public int? STT { get; set; }
            public decimal? PhaiThu { get; set; }
            public decimal? DienTich { get; set; }
            public decimal? DaThu { get; set; }
            public decimal? TyLe { get; set; }
            public decimal? ConNo { get; set; }
            public string DienGiai { get; set; }
            public string Nhom { get; set; }
        }
    }
}
