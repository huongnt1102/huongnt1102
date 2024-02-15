using System;
using System.Linq;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmHandoverHistory : DevExpress.XtraEditors.XtraForm
    {
        public FrmHandoverHistory()
        {
            InitializeComponent();
        }

        private void FrmHandoverHistory_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            ckCbxToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            var objKbc = new KyBaoCao();
            foreach (var item in objKbc.Source) cbxKbc.Items.Add(item);
            itemKyBaoCao.EditValue = objKbc.Source[3];
            SetDate(3);
            LoadData();
        }
        private void SetDate(int index)
        {
            var objKbc = new KyBaoCao
            {
                Index = index
            };
            objKbc.SetToDate();
            itemTuNgay.EditValue = objKbc.DateFrom;
            itemDenNgay.EditValue = objKbc.DateTo;
        }

        private void LoadData()
        {
            try
            {
                var strToaNha = (itemToaNha.EditValue ?? "").ToString().TrimEnd(',').TrimStart(',').Replace(" ", "");
                var ltToaNha = strToaNha.Split(',');
                if (strToaNha == "") return;
                var db = new MasterDataContext();
                gc.DataSource = db.ho_PlanHistories.Where(_ => ltToaNha.Contains(_.BuildingId.ToString()));
            }
            catch { }
        }
        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}