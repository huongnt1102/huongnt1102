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
using Dapper;

namespace Building.AppVime.Services
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }
        
        public int Id { get; set; }
        public int ServiceId { get; set; }

        MasterDataContext db;
        app_SettingService objSS;
        string building_code { get; set; }
        byte building_matn { get; set; }

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();

            var wait = DialogBox.WaitingForm();

            lookUpUnit.Properties.DataSource = db.DonViTinhs.Select(p => new { Id = p.ID, Name = p.TenDVT });
            lookUpUnit.ItemIndex = 0;

            var listSS = db.app_SettingServices.Select(p => p.Id).ToList();
            glService.Properties.DataSource = (from p in db.dvLoaiDichVus
                                               join gr in db.dvLoaiDichVus on p.ParentID equals gr.ID into tempParent
                                               from gr in tempParent.DefaultIfEmpty()
                                               where (!listSS.Contains(p.ID) | p.ID == Id) //& p.ParentID != null
                                               select new
                                               {
                                                   Name = p.TenHienThi,
                                                   Id = p.ID,
                                                   Group = gr != null ? gr.TenHienThi : ""
                                               });
            var toa_nha = db.app_TowerSettingPages.FirstOrDefault();
            if(toa_nha == null)
            {
                Library.DialogBox.Error("Vui lòng cài đặt page trước");
                return;
            }

            building_code = toa_nha.DisplayName;
            building_matn = toa_nha.Id;

            //db.dvLoaiDichVus.Where(p => (!listSS.Contains(p.ID) | p.ID == Id) & p.ParentID != null).Select(p => new {
            //    Name = p.TenHienThi,
            //    Id = p.ID
            //});

            try
            {
                if (Id != 0)
                {
                    objSS = db.app_SettingServices.FirstOrDefault(p => p.Id == this.Id);
                    if (objSS != null)
                    {
                        glService.EditValue = Id;
                        glService.Enabled = false;
                        txtNoiDung.InnerHtml = objSS.Description;
                        btnFile.Text = objSS.FileUrl;
                        spinAmount.EditValue = objSS.Amount ?? 0;
                        spinDeposit.EditValue = objSS.Deposit ?? 0;
                        spinNumberIndex.EditValue = objSS.NumberIndex ?? 0;
                        spinPrice.EditValue = objSS.Price ?? 0;
                        chkExtension.Checked = (objSS.TypeId ?? 0) == 0 ? false : true;
                        spinPreregistraionTime.EditValue = objSS.PreregistrationTime ?? 1;
                        lookUpUnit.EditValue = objSS.UnitId;

                        picLogo.Image = CommonVime.LoadImage(objSS.Logo);
                    }
                }else
                {
                    chkExtension.Checked = false;
                    groupExtension.Enabled = false;
                }
            }
            catch { }

            if (!wait.IsDisposed)
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(glService.EditValue == null)
            {
                DialogBox.Alert("Vui lòng chọn [Dịch vụ], xin cảm ơn.");
                glService.Focus();
                return;
            }

            var wait = DialogBox.WaitingForm();

            if (Id == 0)
            {
                objSS = new app_SettingService();
                objSS.Id = (int)glService.EditValue;
                db.app_SettingServices.InsertOnSubmit(objSS);
            }

            objSS.Description = txtNoiDung.InnerHtml;
            objSS.FileUrl = btnFile.Text;
            objSS.Deposit = spinDeposit.Value;
            if (picLogo.Tag != null)
            {
                objSS.Logo = picLogo.Tag.ToString();
            }

            objSS.MaximumBookInMonth = 1;
            objSS.NumberIndex = (int)spinNumberIndex.Value;
            objSS.PreregistrationTime = (int)spinPreregistraionTime.Value;
            objSS.DateModify = db.GetSystemDate();
            objSS.EmployeeIdModify = Common.User.MaNV;
            objSS.TypeId = (byte)(chkExtension.Checked ? 1 : 0);

            if (chkExtension.Checked)
            {
                objSS.Price = spinPrice.Value;
                objSS.UnitId = (int)lookUpUnit.EditValue;
                objSS.Amount = (int)spinAmount.Value;
            }
            else
            {
                objSS.Price = 0;
                objSS.UnitId = null;
                objSS.Amount = 0;
            }

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
                        
                        var mineType = CommonVime.GetMimeType(frm.ClientPath);

                        byte[] img = File.ReadAllBytes(frm.ClientPath);

                        Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                        var param = new DynamicParameters();
                        param.AddDynamicParams(model_param);
                        //param.Add("EmployeeId", employeeId);
                        var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                        CommonVime.GetConfig();
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

        private void chkExtension_EditValueChanged(object sender, EventArgs e)
        {
            groupExtension.Enabled = chkExtension.Checked;
        }

        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(true))
            {
                var wait = DialogBox.WaitingForm();
                wait.SetCaption("Đang tải hình. Vui lòng chờ...");

                var mineType = CommonVime.GetMimeType(frm.ClientPath);

                byte[] img = File.ReadAllBytes(frm.ClientPath);

                CommonVime.GetConfig();

                Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                var param = new DynamicParameters();
                param.AddDynamicParams(model_param);
                //param.Add("EmployeeId", employeeId);
                var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                var model = new
                {
                    Bytes = img,
                    ApiKey = CommonVime.ApiKey,
                    SecretKey = CommonVime.SecretKey,
                    MineType = mineType,
                    IdNew = a.FirstOrDefault(),
                    IsEdit = true,
                    ImageUrl = "string",
                    isPersonal = VimeService.isPersonal
                };

                
                string link = "/Upload/ImageViaApiAndSecret?idNew=" + a.FirstOrDefault();

                var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecretNoId");
                picLogo.Tag = ret.Replace("\"", "");
                picLogo.Image = CommonVime.LoadImage(picLogo.Tag.ToString());

                wait.Close();
                wait.Dispose();
            }
            frm.Dispose();
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