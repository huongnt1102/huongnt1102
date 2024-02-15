using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MSDN.Html.Editor;
using System.Linq;
using Library;
using System.IO;

namespace Building.AppVime.Tower
{
    public partial class frmEditRule : DevExpress.XtraEditors.XtraForm
    {
        public frmEditRule()
        {
            InitializeComponent();
        }

        public byte? TowerId { get; set; }
        public int? RuleId { get; set; }
        public int? Id { get; set; }

        MasterDataContext db;
        app_TowerSettingRule objRule;

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var listRule = db.app_TowerSettingRules.Where(p => p.TowerId == this.TowerId).Select(p => p.RuleId).ToList();
            lookUpRule.Properties.DataSource = db.app_TowerRules.Where(p => !listRule.Contains(p.Id) | p.Id == RuleId);

            try
            {
                if (Id != null)
                {
                    objRule = db.app_TowerSettingRules.FirstOrDefault(p => p.Id == this.Id);
                    if (objRule != null)
                    {
                        RuleId = objRule.RuleId;
                        lookUpRule.EditValue = RuleId;
                        lookUpRule.Enabled = false;
                        txtNoiDung.InnerHtml = objRule.Contents;
                        btnFile.Text = objRule.FileUrl;
                    }
                }
            }
            catch { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            if (Id == null)
            {
                objRule = new app_TowerSettingRule();
                objRule.TowerId = this.TowerId;
                objRule.RuleId = (int)lookUpRule.EditValue;
                db.app_TowerSettingRules.InsertOnSubmit(objRule);
            }

            objRule.Contents = txtNoiDung.InnerHtml;
            objRule.FileUrl = btnFile.Text;

            try
            {
                db.SubmitChanges();
            }
            catch
            {
                wait.Close();
                wait.Dispose();
            }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    btnFile.Text = "";
                    break;
                case 1:
                    bool IsOK = true;
                    var frm = new FTP.frmUploadFile();
                    if (frm.SelectFile(false))
                    {
                        var wait = DialogBox.WaitingForm();
                        wait.SetCaption("Đang tải file. Vui lòng chờ...");

                        try
                        {
                            var mineType = CommonVime.GetMimeType(frm.ClientPath);

                            byte[] img = File.ReadAllBytes(frm.ClientPath);

                            CommonVime.GetConfig();
                            var db = new MasterDataContext();
                            var toa_nha = db.app_TowerSettingPages.FirstOrDefault(_ => _.Id == TowerId);
                            if (toa_nha == null)
                            {
                                Library.DialogBox.Error("Vui lòng cài đặt page trước");
                                return;
                            }

                            Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = toa_nha.DisplayName, Building_MaTN = toa_nha.Id };
                            var param = new Dapper.DynamicParameters();
                            param.AddDynamicParams(model_param);
                            //param.Add("EmployeeId", employeeId);
                            var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false ? Library.Class.Enum.ConnectString.CONNECT_MYHOME : Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                            var model = new
                            {
                                Bytes = img,
                                ApiKey = CommonVime.ApiKey,
                                SecretKey = CommonVime.SecretKey,
                                MineType = mineType,
                                IdNew = a.FirstOrDefault(),
                                isPersonal = VimeService.isPersonal
                            };

                            var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/FileViaApiAndSecretNoId");
                            string data = ret.Replace("\"", "");
                            if (data.Equals("FILE_TO_LARGE"))
                            {
                                IsOK = false;
                            }
                            else
                            {
                                btnFile.Text = data;
                            }
                        }
                        catch(System.Exception ex) { DialogBox.Alert("File không phù hợp, nguyên nhân: \n" + ex.Message); }
                        


                        wait.Close();
                        wait.Dispose();
                    }
                    frm.Dispose();

                    if (!IsOK)
                    {
                        DialogBox.Alert("File vượt quá kích thước cho phép (5MB)");
                    }
                    break;
            }
        }

        private void btnFields_Click(object sender, EventArgs e)
        {

        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {

        }

        private void txtNoiDung_ImageBrowser(object sender, ImageBrowserEventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(true))
            {
                var wait = DialogBox.WaitingForm();
                wait.SetCaption("Đang tải hình. Vui lòng chờ...");

                var mineType = CommonVime.GetMimeType(frm.ClientPath);

                byte[] img = File.ReadAllBytes(frm.ClientPath);

                CommonVime.GetConfig();

                var model = new
                {
                    Bytes = img,
                    ApiKey = CommonVime.ApiKey,
                    SecretKey = CommonVime.SecretKey,
                    MineType = mineType
                };

                var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecret");
                e.ImageUrl = ret.Replace("\"", "");

                wait.Close();
                wait.Dispose();
            }
            frm.Dispose();
        }

        private void txtNoiDung_FileBrowser(object sender, FileBrowserEventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(false))
            {
                var wait = DialogBox.WaitingForm();
                wait.SetCaption("Đang tải file. Vui lòng chờ...");

                var mineType = CommonVime.GetMimeType(frm.ClientPath);

                byte[] img = File.ReadAllBytes(frm.ClientPath);

                CommonVime.GetConfig();

                var model = new
                {
                    Bytes = img,
                    ApiKey = CommonVime.ApiKey,
                    SecretKey = CommonVime.SecretKey,
                    MineType = mineType
                };

                var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/FileViaApiAndSecret");
                e.FileUrl = ret.Replace("\"", "");

                wait.Close();
                wait.Dispose();
            }
            frm.Dispose();
        }
    }
}