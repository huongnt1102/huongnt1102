﻿using System.Data;
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
using System;

namespace DichVu
{
    public partial class frmBQLDuyet : XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? IDToaNha { get; set; }
        public int? ID { get; set; }
        public frmBQLDuyet()
        {
            InitializeComponent();
        }

        private void frmBQLDuyet_Load(object sender, EventArgs e)
        {
            var check = db.tnSuaChuaVatTus.SingleOrDefault(p => p.ID == ID);
            if (check != null)
            {
                meNoiDung.Text = check.BQLTraLoi;
                checkDuyet.Checked = (bool)check.BQLDuyet;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
                    //var idNew = QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", connectString, param).FirstOrDefault();
                    var idNew = 17;
                    var body = new YeuCauSCModel();
                    body.traLoi = meNoiDung.Text;
                    body.manvbql = Common.User.MaNV;
                    body.Duyet = checkDuyet.Checked;
                    var post = VimeService.PostH(body, $"/YeuCauSuaChua/BanQuanLyDuyet?idNew={idNew}&idYeuCau={ID}");
                    this.Close();
                }
                catch (Exception ex)
                {
                    DialogBox.Error("Error: " + ex);
                }
            }
        }
    }
}