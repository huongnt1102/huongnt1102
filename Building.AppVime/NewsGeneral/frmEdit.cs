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
namespace Building.AppVime.NewsGeneral
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public byte? TowerId { get; set; }
        public long? KeyId { get; set; }

        MasterDataContext db = new MasterDataContext();
        app_NewsGeneral objNews;
        app_NewsGeneralDetail objDetail;

        string building_code { get; set; }
        byte building_matn { get; set; }

        string LinkImage;

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            lookUpCategory.Properties.DataSource = db.app_NewsCategories;
            try
            {
                var toa_nha = db.app_TowerSettingPages.FirstOrDefault();
                if (toa_nha == null)
                {
                    Library.DialogBox.Error("Vui lòng cài đặt page trước");
                    return;
                }

                building_code = toa_nha.DisplayName;
                building_matn = toa_nha.Id;

                if (KeyId != null)
                {
                    objNews = db.app_NewsGenerals.FirstOrDefault(p => p.Id == KeyId);
                    if (objNews != null)
                    {
                        txtTitle.Text = objNews.Title;
                        txtShortDescription.Text = objNews.ShortDescription;
                        picImage.Image = LoadImage(objNews.ImageUrl);
                        chkImportant.Checked = objNews.IsImportant.GetValueOrDefault();
                        btnFile.Text = objNews.FileUrl;
                        txtLink.Text = objNews.Link;
                        lookUpCategory.EditValue = objNews.CategoryId;
                        chkDraff.Checked = objNews.IsDraff.GetValueOrDefault();

                        if (objNews.app_NewsGeneralDetail != null)
                        {
                            objDetail = objNews.app_NewsGeneralDetail;
                            txtNoiDung.InnerHtml = objDetail.Description;
                        }
                    }
                    else
                    {
                        DialogBox.Error("Dữ liệu không tìm thấy. Vui lòng kiểm tra lại.");
                        this.Close();
                    }
                }
                else
                {
                    lookUpCategory.ItemIndex = 0;
                }
            }
            catch { }
        }

        void Save(bool isDraff)
        {
            var wait = DialogBox.WaitingForm();

            if (objNews == null)
            {
                objNews = new app_NewsGeneral();
                objNews.DateCreate = db.GetSystemDate();
                objNews.EmployeeId = Common.User.MaNV;
                db.app_NewsGenerals.InsertOnSubmit(objNews);
            }

            var dateNow = db.GetSystemDate();

            objNews.FileUrl = btnFile.Text;
            objNews.IsDraff = isDraff;
            objNews.IsImportant = chkImportant.Checked;
            objNews.ShortDescription = txtShortDescription.Text;
            objNews.Title = txtTitle.Text;
            objNews.Link = txtLink.Text;
            objNews.CategoryId = (int?)lookUpCategory.EditValue;

            if (picImage.Tag != null)
            {
                objNews.ImageUrl = picImage.Tag.ToString();
            }

            if (objDetail == null)
            {
                objDetail = new app_NewsGeneralDetail();
                objDetail.Description = txtNoiDung.InnerHtml;
                objDetail.app_NewsGeneral = objNews;
                db.app_NewsGeneralDetails.InsertOnSubmit(objDetail);
            }
            else
            {
                objDetail.Description = txtNoiDung.InnerHtml;
            }

            try
            {
                db.SubmitChanges();

                ////Đăng tin
                //if (!isDraff)
                //{
                //    wait.SetCaption("Đang gửi tin cho cư dân.");

                //    try
                //    {
                //        db.SubmitChanges();

                //        //Gửi notify
                //        CommonVime.GetConfig();
                //        var model = new
                //        {
                //            Id = objNews.Id,
                //            ApiKey = CommonVime.ApiKey,
                //            SecretKey = CommonVime.SecretKey
                //        };

                //        var retval = VimeService.Post(model, "/News/Post");
                //        //if(!retval.Replace("\"", "").Equals("OK"))
                //        //{
                //        //    DialogBox.Error("Đăng tin có lỗi");
                //        //}
                //    }
                //    catch (Exception ex)
                //    {
                //        DialogBox.Error("Đăng tin có lỗi. Mã lỗi: " + ex.Message);
                //    }
                //}
            }
            catch (Exception ex)
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFields_Click(object sender, EventArgs e)
        {

        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {

        }

        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(true))
            {
                var wait = DialogBox.WaitingForm();
                wait.SetCaption("Đang tải hình. Vui lòng chờ...");

                picImage.Tag = frm.ClientPath;
                //picLogo.Image = LoadImageLocal(picLogo.Tag.ToString());

                var mineType = CommonVime.GetMimeType(frm.ClientPath);

                byte[] img = File.ReadAllBytes(frm.ClientPath);

                CommonVime.GetConfig();

                Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                var param = new Dapper.DynamicParameters();
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

                var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecretNoId");
                string url = ret.Replace("\"", "");
                if (!string.IsNullOrEmpty(url))
                {
                    picImage.Tag = url;
                    picImage.Image = LoadImage(picImage.Tag.ToString());
                }

                wait.Close();
                wait.Dispose();
            }
            frm.Dispose();
        }

        private Image LoadImageLocal(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch { }

            return null;
        }

        public System.Drawing.Bitmap LoadImage(string imgUrl)
        {
            try
            {
                return new System.Drawing.Bitmap(new System.IO.MemoryStream(new System.Net.WebClient().DownloadData(imgUrl)));
            }
            catch
            {
                return null;
            }
        }

        private void btnSaveDraff_Click(object sender, EventArgs e)
        {
            Save(chkDraff.Checked);
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

                        CommonVime.GetConfig();
                        var model = new
                        {
                            Bytes = img,
                            ApiKey = CommonVime.ApiKey,
                            SecretKey = CommonVime.SecretKey,
                            MineType = mineType,
                            isPersonal = VimeService.isPersonal
                        };

                        var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/FileViaApiAndSecret");
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

                Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                var param = new Dapper.DynamicParameters();
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

                var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecretNoId");
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