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
    public partial class frmChoThue_Layout2 : DevExpress.XtraEditors.XtraForm
    {
        //private MasterDataContext db;
        List<LtData> ltData = new List<LtData>();

        public frmChoThue_Layout2()
        {
            InitializeComponent();
        }

        void LoadData ()
        {
            //var db = new MasterDataContext();
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

            //                       TongDienTich = db.mbMatBangs.Where(o => o.MaTN == item.MaTN).Sum(o => o.DienTich),
            //                       TongDienTichVanPhong = lmb.MaNMB == 2 ? mb.DienTich : 0,
            //                       //TongDienTichKhac = (lmb.MaNMB != 1 & lmb.MaNMB != 2) ? mb.DienTich : 0,

            //                       TongSoVanPhong = lmb.MaNMB == 2 ? mb.MaMB : 0,
            //                       //TongSoKhac = (lmb.MaNMB != 1 & lmb.MaNMB != 2) ? mb.MaMB : 0,

            //                       //Đang sử dụng
            //                       VanPhongThue = tt.ChoThue.GetValueOrDefault() == false & lmb.MaNMB == 2 ? mb.DienTich : 0,
            //                       KhacThue = tt.ChoThue.GetValueOrDefault() == false & (lmb.MaNMB != 1 & lmb.MaNMB != 2) ? mb.DienTich : 0,
            //                       TongDienTichThue = tt.ChoThue.GetValueOrDefault() == false & lmb.MaNMB != 1 ? mb.DienTich : 0,

            //                       //Chưa sử dụng
            //                       VanPhongTrong = tt.ChoThue.GetValueOrDefault() == true & lmb.MaNMB == 2 ? mb.DienTich : 0,
            //                       KhacTrong = tt.ChoThue.GetValueOrDefault() == true & (lmb.MaNMB != 1 & lmb.MaNMB != 2) ? mb.DienTich : 0,
            //                       TongDienTichTrong = tt.ChoThue.GetValueOrDefault() == true & lmb.MaNMB != 1 ? mb.DienTich : 0,

            //                   }).ToList();
            //    ltData.AddRange(List_mb);
            //}
            //var l = (from lt in ltData
            //         group lt by new { lt.TenTN } into gr
            //         select new
            //         {
            //             TenTN = gr.Key.TenTN,

            //             //TongDienTich = gr.Sum(o => o.TongDienTich).GetValueOrDefault(),
            //             TongDienTich = gr.Sum(o => o.TongDienTichVanPhong + o.TongDienTichKhac).GetValueOrDefault(),
            //             TongDienTichVanPhong = gr.Sum(o => o.TongDienTichVanPhong).GetValueOrDefault(),
            //             //TongDienTichKhac = gr.Sum(o => o.TongDienTichKhac).GetValueOrDefault(),

            //             TongSoVanPhong = gr.Count(o => o.TongSoVanPhong > 0),
            //             //TongSoKhac = gr.Count(o => o.TongSoKhac > 0),

            //             VanPhongThue = gr.Sum(o => o.VanPhongThue).GetValueOrDefault(),
            //             KhacThue = gr.Sum(o => o.KhacThue).GetValueOrDefault(),
            //             TongDienTichThue = gr.Sum(o => o.VanPhongThue + o.KhacThue).GetValueOrDefault(),

            //             VanPhongTrong = gr.Sum(o => o.VanPhongTrong).GetValueOrDefault(),
            //             KhacTrong = gr.Sum(o => o.KhacTrong).GetValueOrDefault(),
            //             TongDienTichTrong = gr.Sum(o => o.VanPhongTrong + o.KhacTrong).GetValueOrDefault(),

            //             TyLeLapDay = gr.Sum(o => o.TongDienTichThue).GetValueOrDefault() / gr.FirstOrDefault(o => o.TenTN == gr.Key.TenTN).TongDienTich,
            //         }).ToList();
            //gcMatBang.DataSource = l;

            try
            {

                var ltData = Library.Class.Connect.QueryConnect.QueryData<LtData>("bcTyLeLapDay_ChoThue_Layout2", new
                {
                    Thang = itemThang.EditValue,
                    Nam = itemNam.EditValue,
                    Tuan = itemTuan.EditValue
                });

                gcMatBang.DataSource = ltData;
            }
            catch (System.Exception ex) { }
        }

        private void frmDienTichVanPhong_Load(object sender, EventArgs e)
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
            public decimal? TongDienTichVanPhong { get; set; }
            public decimal? TongDienTichKhac { get; set; }
            public decimal? TongDienTich { get; set; }

            public decimal? VanPhongThue { get; set; }
            public decimal? KhacThue { get; set; }
            public decimal? TongDienTichThue { get; set; }

            public decimal? VanPhongTrong { get; set; }
            public decimal? KhacTrong { get; set; }
            public decimal? TongDienTichTrong { get; set; }

            public decimal? TyLeLapDay { get; set; }

            public decimal? TyLeChoThue { get; set; }
            public decimal? TyLeKhac { get; set; }

            public string DienGiai { get; set; }
        }

        private void itemPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcMatBang);
        }
    }
}