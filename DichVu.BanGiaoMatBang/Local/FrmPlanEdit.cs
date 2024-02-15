using System;
using System.Linq;
using System.Windows.Forms;
using Library;

namespace DichVu.BanGiaoMatBang.Local
{
    public partial class FrmPlanEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? Id { get; set; }
        public byte? BuildingId { get; set; }
        public bool? IsLocal { get; set; }

        private MasterDataContext _db = new MasterDataContext();
        private ho_Plan _plan;
        private System.Collections.Generic.List<Library.Class.HuongDan.ShowAuto.ControlItem> controls;

        private ho_PlanHistory _history;
        public FrmPlanEdit()
        {
            InitializeComponent();
        }

        private void FrmPlanEdit_Load(object sender, EventArgs e)
        {
            //controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoSave(this.Controls);
            controls = Library.Class.HuongDan.ShowAuto.GetControlItemsAutoTag(this.Controls);
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            itemClearText.ItemClick += ItemClearText_ItemClick;
            itemHuongDan.ItemClick += ItemHuongDan_ItemClick;

            _plan = new ho_Plan();

            _history = new ho_PlanHistory();
            _history.Content = "Lập kế hoạch bàn giao nội bộ";
            _history.BuildingId = BuildingId;

            txtHanoverName.Text = "";
            dateHandoverFrom.DateTime = DateTime.UtcNow.AddHours(7);
            dateHandoverTo.DateTime = DateTime.UtcNow.AddHours(7);
            var building = _db.tnToaNhas.First(_ => _.MaTN == BuildingId);
            txtNo.Text = CreateNo(building.TenVT);

            if (Id != null)
            {
                _plan = _db.ho_Plans.FirstOrDefault(_ => _.Id == Id);
                if (_plan != null)
                {
                    txtHanoverName.Text = _plan.Name;
                    if (_plan.DateHandOverFrom != null) dateHandoverFrom.DateTime = (DateTime) _plan.DateHandOverFrom;
                    if (_plan.DateHandOverTo != null) dateHandoverTo.DateTime = (DateTime) _plan.DateHandOverTo;
                    _plan.DateUpdate = DateTime.UtcNow.AddHours(7);
                    _plan.UserUpdate = Common.User.MaNV;
                    _plan.UserUpdateName = Common.User.HoTenNV;
                    txtNo.Text = _plan.No ?? CreateNo(building.TenVT);

                    _history.Content = "Chỉnh sửa kế hoạch bàn giao";
                }
                else
                {
                    _plan = new ho_Plan();
                    _plan.BuildingId = BuildingId;
                    _plan.UserCreate = Common.User.MaNV;
                    _plan.DateCreate = DateTime.UtcNow.AddHours(7);
                    _plan.UserCreateName = Common.User.HoTenNV;
                    _db.ho_Plans.InsertOnSubmit(_plan);
                }
            }
            else
            {
                _plan.BuildingId = BuildingId;
                _plan.UserCreateName = Common.User.HoTenNV;
                _plan.UserCreate = Common.User.MaNV;
                _plan.DateCreate = DateTime.UtcNow.AddHours(7);
                _db.ho_Plans.InsertOnSubmit(_plan);
            }
        }

        private void ItemHuongDan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Library.Class.HuongDan.ShowAuto.ActiveDemo(true, controls);
        }

        private void ItemClearText_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var no = txtNo.Text;
            Library.Class.HuongDan.ShowAuto.ClearText(this.Controls);
            txtNo.Text = no;
        }

        private string CreateNo(string buildingNo)
        {
            if (buildingNo == null) return "";
            var db = new MasterDataContext();

            string temp = "";
            string stt="";

            if(IsLocal==true) temp = buildingNo + ".KHNB.";
            else temp = buildingNo + ".KHKH.";
            var param = new Dapper.DynamicParameters();
            var result = Library.Class.Connect.QueryConnect.Query<string>("dbo.ho_schedule_apartment_get_new_no", param);
            stt = result.First();
            //var obj = (from _ in db.ho_Plans
            //    where _.BuildingId == BuildingId & _.IsLocal == IsLocal
            //    orderby _.No.Substring(_.No.IndexOf('.') + 6) descending
            //    select new
            //    {
            //        Stt = _.No.Substring(_.No.IndexOf('.') + 6)
            //    }).FirstOrDefault();
            //if (obj == null||(obj!=null&obj.Stt==null))
            //{
            //    stt = "0001";
            //}
            //else stt = (int.Parse(obj.Stt) + 1).ToString().PadLeft(4, '0');

            temp = temp + stt;
            return temp;
        }

        private void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _plan.Name = txtHanoverName.Text;
                _plan.DateHandOverFrom = dateHandoverFrom.DateTime;
                _plan.DateHandOverTo = dateHandoverTo.DateTime;
                
                // lưu nội bộ hoặc khách hàng
                _plan.IsLocal = IsLocal;

                _history.PlanName = txtHanoverName.Text;
                _history.DateHandoverFrom = dateHandoverFrom.DateTime;
                _history.DateHandoverTo = dateHandoverTo.DateTime;
                _history.DateCreate = DateTime.UtcNow.AddHours(7);
                _history.UserCreate = Common.User.MaNV;
                _history.UserCreateName = Common.User.HoTenNV;
                _history.IsLocal = IsLocal;
                _plan.ho_PlanHistories.Add(_history);
                _plan.No = txtNo.Text;

                _db.SubmitChanges();

                DialogBox.Success("Lưu dữ liệu thành công");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch {
                DialogBox.Error("Đã xảy ra lỗi, vui lòng kiểm tra lại.");
            }
        }

        private void itemCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}