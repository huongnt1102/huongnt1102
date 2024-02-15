using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Library;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;

namespace LandsoftBuilding.Receivables.Reports.TyLeLapDay
{
    public partial class frmTongTinChung_Layout2 : DevExpress.XtraEditors.XtraForm
    {
        //private MasterDataContext db;
        List<LtData> ltData = new List<LtData>();

        public frmTongTinChung_Layout2()
        {
            InitializeComponent();
        }

        private string GetClass<T>(List<T> iList)
        {
            var className = "DataDanhSachMatBang";
            var text = "public class " + className + " { ";

            System.ComponentModel.PropertyDescriptorCollection propertyDescriptorCollection = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                System.ComponentModel.PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) type = Nullable.GetUnderlyingType(type);

                text = text + " public " + type + " " + propertyDescriptor.Name + " {get; set;}";
            }

            text = text + " }";
            return text;
        }

        void LoadData()
        {
            //gcMatBang.DataSource = null;
            //gcMatBang.DataSource = linqInstantFeedbackSource1;

            //var db = new MasterDataContext();
            //var _MaTN = (byte)itemToaNha.EditValue;
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
            //                       TongDienTichCanHo = lmb.MaNMB == 1 ? mb.DienTich : 0,
            //                       TongDienTichVanPhong = lmb.MaNMB == 2 ? mb.DienTich : 0,
            //                       TongDienTichKhac = (lmb.MaNMB != 1 & lmb.MaNMB != 2) ? mb.DienTich : 0,

            //                       //Đang sử dụng
            //                       CanHoThue = tt.ChoThue.GetValueOrDefault() == false & lmb.MaNMB == 1 ? mb.DienTich : 0,
            //                       VanPhongThue = tt.ChoThue.GetValueOrDefault() == false & lmb.MaNMB == 2 ? mb.DienTich : 0,
            //                       KhacThue = tt.ChoThue.GetValueOrDefault() == false & (lmb.MaNMB != 1 & lmb.MaNMB != 2) ? mb.DienTich : 0,
            //                       TongDienTichThue = tt.ChoThue.GetValueOrDefault() == false ? mb.DienTich : 0,

            //                       //Chưa sử dụng
            //                       CanHoTrong = tt.ChoThue.GetValueOrDefault() == true & lmb.MaNMB == 1 ? mb.DienTich : 0,
            //                       VanPhongTrong = tt.ChoThue.GetValueOrDefault() == true & lmb.MaNMB == 2 ? mb.DienTich : 0,
            //                       KhacTrong = tt.ChoThue.GetValueOrDefault() == true & (lmb.MaNMB != 1 & lmb.MaNMB != 2) ? mb.DienTich : 0,
            //                       TongDienTichTrong = tt.ChoThue.GetValueOrDefault() == true ? mb.DienTich : 0,



            //                   }).ToList();
            //    ltData.AddRange(List_mb);
            //}

            //var l = (from lt in ltData
            //         group lt by new { lt.TenTN } into gr
            //         select new
            //         {
            //             TenTN = gr.Key.TenTN,

            //             //Thông tin chung
            //             TongDienTich = gr.Sum(o => o.TongDienTichCanHo + o.TongDienTichVanPhong + o.TongDienTichKhac).GetValueOrDefault(),
            //             TongDienTichCanHo = gr.Sum(o => o.TongDienTichCanHo).GetValueOrDefault(),
            //             TongDienTichVanPhong = gr.Sum(o => o.TongDienTichVanPhong).GetValueOrDefault(),
            //             TongDienTichKhac = gr.Sum(o => o.TongDienTichKhac).GetValueOrDefault(),

            //             //Đang sử dụng
            //             CanHoThue = gr.Sum(o => o.CanHoThue).GetValueOrDefault(),
            //             VanPhongThue = gr.Sum(o => o.VanPhongThue).GetValueOrDefault(),
            //             KhacThue = gr.Sum(o => o.KhacThue).GetValueOrDefault(),
            //             TongDienTichThue = gr.Sum(o => o.TongDienTichThue).GetValueOrDefault(),

            //             //Chưa sử dụng
            //             CanHoTrong = gr.Sum(o => o.CanHoTrong).GetValueOrDefault(),
            //             VanPhongTrong = gr.Sum(o => o.VanPhongTrong).GetValueOrDefault(),
            //             KhacTrong = gr.Sum(o => o.KhacTrong).GetValueOrDefault(),
            //             TongDienTichTrong = gr.Sum(o => o.TongDienTichTrong).GetValueOrDefault(),

            //             //Tỷ lệ
            //             TyLeCanHo = gr.Sum(o => o.CanHoThue).GetValueOrDefault() / gr.FirstOrDefault(o => o.TenTN == gr.Key.TenTN).TongDienTich,
            //             TyLeVanPhong = gr.Sum(o => o.VanPhongThue).GetValueOrDefault() / gr.FirstOrDefault(o => o.TenTN == gr.Key.TenTN).TongDienTich,
            //             TyLeKhac = gr.Sum(o => o.KhacThue).GetValueOrDefault() / gr.FirstOrDefault(o => o.TenTN == gr.Key.TenTN).TongDienTich,
            //             TyLeTong = gr.Sum(o => o.TongDienTichThue).GetValueOrDefault() / gr.FirstOrDefault(o => o.TenTN == gr.Key.TenTN).TongDienTich,

            //         }).ToList();

            try
            {

                var ltData = Library.Class.Connect.QueryConnect.QueryData<LtData>("bcTyLeLapDay_Chung1_Layout2", new
                {
                    Thang = itemThang.EditValue,
                    Nam = itemNam.EditValue,
                    Tuan = itemTuan.EditValue
                });

                gcMatBang.DataSource = ltData;
            }
            catch(System.Exception ex) { }

        }

        void RefreshData()
        {
            linqInstantFeedbackSource1.Refresh();
        }
     
        private void frmMatBang_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            lkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            itemNam.EditValue = System.DateTime.Now.Year;
            itemThang.EditValue = System.DateTime.Now.Month;
            itemTuan.EditValue = DateTime.Now.GetWeekInMonth() > 4 ? 4 : DateTime.Now.GetWeekInMonth();
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemToaNha_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void grvMatBang_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                if (e.Column.Caption == "Ký hiệu")
                    e.Appearance.BackColor = Color.FromArgb((int)grvMatBang.GetRowCellValue(e.RowHandle, "MauNen"));
            }
            catch { }
        }

        private void grvMatBang_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {

        }

        private void linqInstantFeedbackSource1_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            try
            {
                (e.Tag as MasterDataContext).Dispose();
            }
            catch { }
        }
    
        private void linqInstantFeedbackSource1_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            //var db = new MasterDataContext();
            //var _MaTN = (byte)itemToaNha.EditValue;
            //var List_tn = db.tnToaNhas.ToList();

            //foreach (var item in List_tn)
            //{
            //    var List_mb = (from tn in db.tnToaNhas
            //                   join mb in db.mbMatBangs on tn.MaTN equals mb.MaTN
            //                   join tt in db.mbTrangThais on mb.MaTT equals tt.MaTT
            //                   where tn.MaTN == item.MaTN
            //                   select new LtData
            //                   {
            //                       TenTN = tn.TenTN,
            //                       TongDienTichThue = tt.ChoThue == false ? mb.DienTich : 0,
            //                       TongDienTichTrong = tt.ChoThue == true ? mb.DienTich : 0,
            //                       TongDienTich = db.mbMatBangs.Where(o => o.MaTN == item.MaTN).Sum(o => o.DienTich)
            //                   }).ToList();
            //    ltData.AddRange(List_mb);
            //}

            //e.QueryableSource = from lt in ltData
            //                    select new
            //                    {
            //                        lt.TenTN,
            //                        lt.TongDienTich,
            //                        lt.TongDienTichThue,
            //                        lt.TongDienTichTrong,
            //                        lt.DienGiai
            //                    };
            //e.Tag = db;
        }

        private void btnExportMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gcMatBang);
        }

        public class LtData
        {
            public string TenTN { get; set; }
            public string TenKN { get; set; }
            public decimal? TongDienTichCanHo { get; set; }
            public decimal? TongDienTichVanPhong { get; set; }
            public decimal? TongDienTichKhac { get; set; }
            public decimal? TongDienTich { get; set; }

            public decimal? CanHoThue { get; set; }
            public decimal? VanPhongThue { get; set; }
            public decimal? KhacThue { get; set; }
            public decimal? TongDienTichThue { get; set; }

            public decimal? CanHoTrong { get; set; }
            public decimal? VanPhongTrong { get; set; }
            public decimal? KhacTrong { get; set; }
            public decimal? TongDienTichTrong { get; set; }

            public decimal? TyLeCanHo { get; set; }
            public decimal? TyLeVanPhong { get; set; }
            public decimal? TyLeKhac { get; set; }
            public decimal? TyLeTong { get; set; }
            public string DienGiai { get; set; }
        }
        
    }
}