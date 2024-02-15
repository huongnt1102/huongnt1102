using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Library;
using System.Linq;
using DevExpress.XtraGrid;
using System.Data.Linq.SqlClient;
using Building.AppVime;
using Building.AppVime.Class;
using Newtonsoft.Json;
using Library.Class.Connect;
using static Library.Properties.Settings;
using static Building.AppExtension.Model.ExtensionModel;

namespace Building.AppExtension
{
    public partial class frmTraLoi : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? IDToaNha { get; set; }
        public int? ID { get; set; }
        public frmTraLoi()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (meNoiDung.Text == "")
            {
                DialogBox.Error("Vui lòng nhập câu trả lời!");
                meNoiDung.Focus();
                return;
            }
            using (var db = new MasterDataContext())
            {
                try
                {
                    CommonVime.GetConfig();
                    var toa_nha = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == IDToaNha);
                    string building_code = toa_nha.DisplayName;
                    int building_matn = toa_nha.Id;

                    tbl_building_get_id model_param = new tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                    var param = new Dapper.DynamicParameters();
                    param.AddDynamicParams(model_param);

                    var connectString = Default.Building_dbConnectionString;
                    var idNew = QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", connectString, param).FirstOrDefault();
                    var managerId = Common.User.MaNV; ;
                    var messageID = ID;
                    var body = new ManagementContact();
                    body.Content = meNoiDung.Text;
                    var post = VimeService.PostH(body, $"/CustomerContact/SendResident?idNew={idNew}&messageID={messageID}&managerID={managerId}");
                    this.Close();
                }
                catch (Exception ex)
                {
                    DialogBox.Error("Error: " + ex);
                }
               
            }
        }

        private void frmTraLoi_Load(object sender, EventArgs e)
        {
            var check = db.app_KHLienHeChiTiets.FirstOrDefault(p => p.IDKHLienHe == ID);
            if (check != null)
            {
                meNoiDung.Text = check.NoiDungTraLoi;
            }

        }
    }
}
