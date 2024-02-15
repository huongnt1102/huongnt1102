using Dapper;
using Library;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LandSoftBuilding.Receivables.Reports
{
    public partial class FrmBaoCaoTuoiNo : DevExpress.XtraEditors.XtraForm
    {
        public FrmBaoCaoTuoiNo()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoTuoiNo_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            cbxToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var param = new Dapper.DynamicParameters();
            cbxLoaiDichVu.DataSource = Library.Class.Connect.QueryConnect.Query<cn_bao_cao_tuoi_no_loai_dich_vu>("dbo.cn_bao_cao_tuoi_no_loai_dich_vu", param).ToList();

            try
            {
                itemLoaiDichVu.EditValue = 13;
            }
            catch { }

            LoadData();
        }

        /// <summary>
        /// NẠP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        public  void LoadData()
        {
            if (itemToaNha.EditValue == null) return;
            if (itemLoaiDichVu.EditValue == null) return;
            var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var strLoaiDichVu = (itemLoaiDichVu.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var model = new { tn_arr = strToaNha, ldv_arr = strLoaiDichVu };
            var param = new DynamicParameters();
            param.AddDynamicParams(model);
            //var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.tbl_phieuvanhanh_tong_checklist", param).ToList();
            gridControl1.DataSource = Library.Class.Connect.QueryConnect.Query<cn_bao_cao_tuoi_no>("dbo.cn_bao_cao_tuoi_no", param);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Commoncls.ExportExcel(gridControl1);
        }
    }

    public class cn_bao_cao_tuoi_no_loai_dich_vu
    {
        public int ID { get; set; }
        public string TenHienThi { get; set; }
    }

    public class cn_bao_cao_tuoi_no
    {
        public int? MaKH { get; set; }

        public decimal? CON_NO { get; set; }

        public decimal? TIEN_TRONG_HAN_TT { get; set; }

        public decimal? TIEN_TRE_30 { get; set; }

        public decimal? TIEN_TRE_60 { get; set; }

        public decimal? TIEN_TRE_90 { get; set; }

        public decimal? TIEN_TRE_120 { get; set; }

        public decimal? TIEN_TRE_HON_120 { get; set; }

        public string TenVT { get; set; }

        public string KyHieu { get; set; }

        public string TenKH { get; set; }

    }

}