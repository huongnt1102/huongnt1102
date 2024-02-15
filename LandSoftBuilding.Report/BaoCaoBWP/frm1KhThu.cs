using Dapper;
using DevExpress.XtraGrid.Views.Grid;
using Library;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LandSoftBuilding.Report.BaoCaoBWP
{
    public partial class frm1KHThu : DevExpress.XtraEditors.XtraForm
    {
        public frm1KHThu()
        {
            InitializeComponent();
        }

        private void frm1KhThu_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            cbxToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;

            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = objKbc.Source[3];
            SetDate(3);

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

        /// <summary>
        /// cbx Kỳ báo cáo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxKbc_EditValueChanged(object sender, EventArgs e)
        {
            SetDate(((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedIndex);
        }

        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao { Index = index };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        public async void LoadData()
        {
            if (itemToaNha.EditValue == null) return;
            var wait = DialogBox.WaitingForm();
            var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
            var tuNgay = (DateTime)itemTuNgay.EditValue;
            var denNgay = (DateTime)itemDenNgay.EditValue;
            var model = new { tn_arr = strToaNha, tu_ngay = tuNgay, den_ngay = denNgay };
            var param = new DynamicParameters();
            param.AddDynamicParams(model);
            //var kq = Library.Class.Connect.QueryConnect.Query<bool>("dbo.tbl_phieuvanhanh_tong_checklist", param).ToList();
            gridControl1.DataSource = await Task.Run(() => Library.Class.Connect.QueryConnect.Query<dv_hoadon_bao_cao_lai__tong_hop>("dbo.dv_hoadon_bao_cao_lai__tong_hop", param));
            wait.Close();
        }
    }

    public class dv_hoadon_bao_cao_lai__tong_hop
    {
        public string STT { get; set; }
        public string TenVT { get; set; }
        public string NoiDungTinhLai { get; set; }
        public int? MaKH { get; set; }
        public string KyHieu { get; set; }
        public string TenKH { get; set; }
        public decimal? TongLai { get; set; }
        public decimal? DaTraLai { get; set; }
        public decimal? ChietKhau { get; set; }
        public decimal? ConLai { get; set; }
    }
}