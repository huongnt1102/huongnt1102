using System.Linq;

namespace DichVu.BanGiaoMatBang.Notifycation
{
    public partial class HistoryNotifycationStaff : DevExpress.XtraEditors.XtraForm
    {
        public System.Collections.Generic.List<Library.PhanQuyen.ControlName> LControlName { get; set; }

        private Library.MasterDataContext _db;

        public HistoryNotifycationStaff()
        {
            InitializeComponent();
        }

        private void HistoryNotifycationStaff_Load(object sender, System.EventArgs e)
        {
            Library.TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Library.Common.User, barManager1);

            lkBuilding.DataSource = Library.Common.TowerList;
            itemBuilding.EditValue = Library.Common.User.MaTN;

            Library.KyBaoCao kbc = new Library.KyBaoCao();
            foreach (string item in kbc.Source) cbxKbc.Items.Add(item);
            itemKbc.EditValue = kbc.Source[3];

            SetDate(3);
            LoadData();
        }

        private void SetDate(int index)
        {
            var kbc = new Library.KyBaoCao() { Index = index };
            kbc.SetToDate();

            itemDateFrom.EditValue = kbc.DateFrom;
            itemDateTo.EditValue = kbc.DateTo;
        }

        private void LoadData()
        {
            _db = new Library.MasterDataContext();
            var tuNgay = (System.DateTime)itemDateFrom.EditValue;
            var denNgay = (System.DateTime)itemDateTo.EditValue;

            gc.DataSource = _db.ho_NotifycationStaffs
                .Where(_ => _.BuildingId == (byte?) itemBuilding.EditValue &
                            System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(tuNgay, _.DateCreate) >= 0 &
                            System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(_.DateCreate, denNgay) >= 0).Select(_ =>
                    new {_.Contents, _.DateCreate, _.Id, _.StaffId, _.StaffName, _.Title, _.TyleId, _.UserCreateName});
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemTaoPhanQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //CreatePhanQuyen();
        }

    }
}