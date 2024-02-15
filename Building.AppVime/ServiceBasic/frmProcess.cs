using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Building.AppVime.ServiceBasic
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 1: Tiếp nhận; 2: Đổi trạng thái; 3: Đổi nhân viên; 4: Giao việc
        /// </summary>
        public byte TypeId { get; set; }
        public long? Id;
        public byte StatusId { get; set; }
        public int TowerId { get; set; }
        public string TowerName { get; set; }
        public string ServiceName { get; set; }

        MasterDataContext db;

        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            CommonVime.GetConfig();
            string secretKey = CommonVime.SecretKey;
            string apiKey = CommonVime.ApiKey;
            var db = new MasterDataContext();
            var tn = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == TowerId);
            if (tn == null)
            {
                DialogBox.Warning("Không tìm ra tòa nhà.");
                return;
            }

            Class.tbl_building_get_id model_param_1 = new Class.tbl_building_get_id() { Building_Code = tn.DisplayName, Building_MaTN = tn.Id };
            var param_1 = new Dapper.DynamicParameters();
            param_1.AddDynamicParams(model_param_1);
            //param.Add("EmployeeId", employeeId);
            var b = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param_1);

            var model = new
            {
                BookingId = Id,
                StatusId = StatusId,
                Description = txtContents.Text,
                TowerName = TowerName,
                ServiceName = ServiceName,
                EmployeeId = Common.User.MaNV,
                EmployeeName = Common.User.HoTenNV,
                ApiKey = apiKey,
                SecretKey = secretKey,
                idNew = b.FirstOrDefault(),
                isPersonal = VimeService.isPersonal
            };

            var retval = VimeService.PostH(model, "/Vendors/ServiceBasic/UpdateSoftwareNoId");
            switch (retval.Replace("\"", ""))
            {
                case "100":
                    DialogResult = DialogResult.OK;
                    break;
                case "101":

                    break;
                default:

                    break;
            }

            this.Close();
        }
    }
}