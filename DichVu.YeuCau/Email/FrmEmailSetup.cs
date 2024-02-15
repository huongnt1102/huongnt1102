using System;
using System.Linq;
using DevExpress.XtraEditors;
using Library;

namespace DichVu.YeuCau.Email
{
    public partial class FrmEmailSetup : XtraForm
    {
        private MasterDataContext _db;
        public FrmEmailSetup()
        {
            InitializeComponent();
        }

        private void FrmEmailSetup_Load(object sender, EventArgs e)
        {
            TranslateLanguage.TranslateControl(this, barManager1);
            Library.HeThongCls.PhanQuyenCls.Authorize(this, Common.User, barManager1);
            glkToaNha.DataSource = Common.TowerList;
            itemToaNha.EditValue = Common.User.MaTN;
            LoadData();
        }

        private void LoadData()
        {
            _db = new MasterDataContext();
            gc.DataSource = _db.tnycLoaiYeuCaus.Select(_ => new EmailSetup
            {
                SendCustomer = _db.email_Setups.FirstOrDefault(e =>
                                   e.RequestTyleId == _.ID & e.BuildingId == (byte?) itemToaNha.EditValue) != null
                    ?_db.email_Setups.FirstOrDefault(e => e.RequestTyleId == _.ID & e.BuildingId == (byte?) itemToaNha.EditValue).SendCustomer:false,
                SendEmployee = _db.email_Setups.FirstOrDefault(e =>
                                   e.RequestTyleId == _.ID & e.BuildingId == (byte?)itemToaNha.EditValue) != null
                    ? _db.email_Setups.FirstOrDefault(e => e.RequestTyleId == _.ID & e.BuildingId == (byte?)itemToaNha.EditValue).SendEmployee : false,
                RequestTyleName=_.TenLoaiYeuCau,
                RequestTyleNo=_.TenVT,
                Id = _.ID,
            }).ToList();
        }

        public class EmailSetup
        {
            public int? Id { get; set; }
            public bool? SendEmployee { get; set; }
            public bool? SendCustomer { get; set; }
            public string RequestTyleNo { get; set; }
            public string RequestTyleName { get; set; }
        }

        private void ItemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void ItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SubmitChanges();
            DialogBox.Success("Lưu cài đặt thành công");
            LoadData();
        }

        private void CkSendEmployee_CheckedChanged(object sender, EventArgs e)
        {
            var item = sender as CheckEdit;
            if (item == null) return;
            var id = (int) gv.GetFocusedRowCellValue("Id");
            var setup = _db.email_Setups.FirstOrDefault(_ =>
                _.RequestTyleId == id & _.BuildingId == ((byte?) itemToaNha.EditValue ?? Common.User.MaTN));
            if (setup != null)
            {
                setup.SendEmployee = (bool) item.EditValue;
            }
            else
            {
                var newSetup = new email_Setup
                {
                    BuildingId = ((byte?) itemToaNha.EditValue ?? Common.User.MaTN),
                    RequestTyleId = id,
                    RequestTyleName = gv.GetFocusedRowCellValue("RequestTyleName").ToString(),
                    RequestTyleNo = gv.GetFocusedRowCellValue("RequestTyleNo").ToString(),
                    SendEmployee = (bool) item.EditValue,
                    SendCustomer = false
                };
                _db.email_Setups.InsertOnSubmit(newSetup);
            }

            _db.SubmitChanges();
        }

        private void CkSendCustomer_CheckedChanged(object sender, EventArgs e)
        {
            var item = sender as CheckEdit;
            if (item == null) return;
            var id = (int)gv.GetFocusedRowCellValue("Id");
            var setup = _db.email_Setups.FirstOrDefault(_ =>
                _.RequestTyleId == id & _.BuildingId == ((byte?)itemToaNha.EditValue ?? Common.User.MaTN));
            if (setup != null)
            {
                setup.SendCustomer = (bool)item.EditValue;
            }
            else
            {
                var newSetup = new email_Setup
                {
                    BuildingId = ((byte?) itemToaNha.EditValue ?? Common.User.MaTN),
                    RequestTyleId = id,
                    RequestTyleName = gv.GetFocusedRowCellValue("RequestTyleName").ToString(),
                    RequestTyleNo = gv.GetFocusedRowCellValue("RequestTyleNo").ToString(),
                    SendCustomer = (bool) item.EditValue,
                    SendEmployee = false
                };
                _db.email_Setups.InsertOnSubmit(newSetup);
            }

            _db.SubmitChanges();
        }
    }
}