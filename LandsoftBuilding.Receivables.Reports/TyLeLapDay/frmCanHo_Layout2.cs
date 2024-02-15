using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Library;

namespace LandsoftBuilding.Receivables.Reports.TyLeLapDay
{
    public partial class frmCanHo_Layout2 : DevExpress.XtraEditors.XtraForm
    {
        //private MasterDataContext db;
        List<LtData> ltData = new List<LtData>();

        public frmCanHo_Layout2()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            //var db = new MasterDataContext();
            ////var _MaTN = (byte)itemToaNha.EditValue;
            //var List_tn = db.tnToaNhas.ToList();

            //foreach (var item in List_tn)
            //{

            //    var List_mb = (from tn in db.tnToaNhas
            //                   join mb in db.mbMatBangs on tn.MaTN equals mb.MaTN

            //                   join lmb in db.mbLoaiMatBangs on mb.MaLMB equals lmb.MaLMB

            //                   join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT
            //                   where tn.MaTN == item.MaTN
            //                   select new LtData
            //                   {
            //                       TenTN = tn.TenTN,

            //                       //TongDienTich = db.mbMatBangs.Where(o => o.MaTN == item.MaTN).Sum(o => o.DienTich),
            //                       TongDienTichCanHo = lmb.MaNMB == 1 ? mb.DienTich : 0,
            //                       TongSoCanHo = lmb.MaNMB == 1 ? mb.MaMB : 0,

            //                       //Đang sử dụng
            //                       TongSoCanHoDaBanGiao = tt.ChoThue.GetValueOrDefault() == false & lmb.MaNMB == 1 ? mb.MaMB : 0,
            //                       DienTichDaBanGiao = tt.ChoThue.GetValueOrDefault() == false & lmb.MaNMB == 1 ? mb.DienTich : 0,

            //                       //Chưa sử dụng
            //                       TongSoCanHoChuaBanGiao = tt.ChoThue.GetValueOrDefault() == true & lmb.MaNMB == 1 ? mb.MaMB : 0,
            //                       DienTichChuaBanGiao = tt.ChoThue.GetValueOrDefault() == true & lmb.MaNMB == 1 ? mb.DienTich : 0,

            //                   }).ToList();
                
            //    ltData.AddRange(List_mb);

            //}
            //var l = (from lt in ltData
            //         group lt by new { lt.TenTN } into gr
            //         select new
            //         {
            //             TenTN = gr.Key.TenTN,

            //             //Thông tin chung
            //             TongDienTichCanHo = gr.Sum(o => o.TongDienTichCanHo).GetValueOrDefault(),
            //             TongSoCanHo = gr.Count(o => o.TongSoCanHo > 0),

            //             //Đang sử dụng
            //             TongSoCanHoDaBanGiao = gr.Count(o => o.TongSoCanHoDaBanGiao > 0),
            //             DienTichDaBanGiao = gr.Sum(o => o.DienTichDaBanGiao).GetValueOrDefault(),

            //             //Chưa sử dụng
            //             TongSoCanHoChuaBanGiao = gr.Count(o => o.TongSoCanHoChuaBanGiao > 0),
            //             DienTichChuaBanGiao = gr.Sum(o => o.DienTichChuaBanGiao).GetValueOrDefault(),

            //             //Tỷ lệ
            //             TyLeLapDayCanHo = gr.Count(o => o.TongSoCanHo > 0) == 0 ? 0 : gr.Count(o => o.TongSoCanHoDaBanGiao > 0) / gr.Count(o => o.TongSoCanHo > 0),
            //             TyLeLapDay = gr.Sum(o => o.DienTichDaBanGiao).GetValueOrDefault() == 0 ? 0 : gr.Sum(o => o.DienTichDaBanGiao).GetValueOrDefault() / gr.Sum(o => o.TongDienTichCanHo).GetValueOrDefault(),
            //             //TyLeLapDay = gr.Sum(o => o.DienTichDaBanGiao).GetValueOrDefault() / gr.FirstOrDefault(o => o.TenTN == gr.Key.TenTN).TongDienTich,

            //         }).ToList();

            //gcMatBang.DataSource = l;
            try
            {

                var ltData = Library.Class.Connect.QueryConnect.QueryData<LtData>("bcTyLeLapDay_CanHo_Layout2", new
                {
                    Thang = itemThang.EditValue,
                    Nam = itemNam.EditValue,
                    Tuan = itemTuan.EditValue
                });

                gcMatBang.DataSource = ltData;
            }
            catch (System.Exception ex) { }
        }

        private void frmDienTichCanHo_Load(object sender, EventArgs e)
        {
            itemThang.EditValue = System.DateTime.Now.Month;
            itemNam.EditValue = System.DateTime.Now.Year;
            itemTuan.EditValue = DateTime.Now.GetWeekInMonth() > 4 ? 4 : DateTime.Now.GetWeekInMonth();
            LoadData();
        }

        private void itemNap_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadData();
        }

        public class LtData
        {
            public string TenTN { get; set; }
            public string TenKN { get; set; }
            public decimal? TongDienTich { get; set; }
            public decimal? TongDienTichCanHo { get; set; }
            public int? TongSoCanHo { get; set; }

            public decimal? TongSoCanHoDaBanGiao { get; set; }
            public decimal? DienTichDaBanGiao { get; set; }

            public decimal? TongSoCanHoChuaBanGiao { get; set; }
            public decimal? DienTichChuaBanGiao { get; set; }

            public decimal? TyLeLapDay { get; set; }
            public decimal? TyLeLapDayCanHo { get; set; }



            public string DienGiai { get; set; }
        }

        private void itemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcMatBang);
        }
    }
}