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

namespace Building.AppVime.Tower
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public frmEdit()
        {
            InitializeComponent();
        }

        public byte? TowerId { get; set; }
        MasterDataContext db = new MasterDataContext();
        app_TowerSettingPage objTSP;
        app_TowerSettingPageDetail objDetail;

        bool IsTimeLoaded = false;
        bool IsDealineLoaded = false;

        string building_code { get; set; }
        byte building_matn { get; set; }

        string LinkImage;

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            lookUpTower.Properties.DataSource = db.tnToaNhas.Select(p => new {
                p.MaTN,
                p.TenTN
            });



            try
            {
                LinkImage = db.tblConfigs.FirstOrDefault().WebUrl;

                if (TowerId != null)
                {
                    objTSP = db.app_TowerSettingPages.FirstOrDefault(p => p.Id == TowerId);
                    if (objTSP != null)
                    {
                        txtAddress.Text = objTSP.Address;
                        txtDisplayName.Text = objTSP.DisplayName;
                        txtHotline.Text = objTSP.Hotline;

                        picBanner.Image = LoadImage(objTSP.Banner);
                        picLogo.Image = LoadImage(objTSP.Logo);

                        chkInputHex.Checked = objTSP.IsInputHex.GetValueOrDefault();

                        txtColorCode.Text = objTSP.BackgroundColorHex;
                        colorEdit.Text = (objTSP.BackgroundColor ?? -1).ToString();

                        lookUpTower.EditValue = objTSP.Id;

                        building_code = objTSP.DisplayName;
                        building_matn = objTSP.Id;

                        var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == TowerId);
                        if (objTN != null)
                        {
                            try
                            {
                                string[] arrPay = objTN.ThoiHan.Split('-');
                                spinKyThanhToan.EditValue = Convert.ToInt32(arrPay[0]);
                                SetPrefix(arrPay[1]);

                                string[] arrDead = objTN.HanThanhToan.Split('-');
                                spinDeadline.EditValue = Convert.ToInt32(arrDead[0]);
                                SetPrefixDeadline(arrDead[1]);
                            }
                            catch
                            {
                                cmbDeadline.SelectedIndex = -1;
                                cmdKyThanhToan.SelectedIndex = -1;
                            }
                        }
                    }

                    objDetail = db.app_TowerSettingPageDetails.FirstOrDefault(p => p.Id == TowerId);
                    if (objDetail != null)
                    {
                        txtNoiDung.InnerHtml = objDetail.ContentGeneral;
                    }

                    lookUpTower.Enabled = false;
                }
                else
                {
                    chkInputHex.Checked = false;
                    txtColorCode.Enabled = false;
                }
            }
            catch { }

            IsTimeLoaded = true;
            IsDealineLoaded = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();

            TowerId = (byte)lookUpTower.EditValue;
            objTSP = db.app_TowerSettingPages.FirstOrDefault(p => p.Id == TowerId);
            if (objTSP == null)
            {
                objTSP = new app_TowerSettingPage();
                objTSP.Address = txtAddress.Text;
                objTSP.DisplayName = txtDisplayName.Text;
                objTSP.Hotline = txtHotline.Text;
                objTSP.Id = TowerId.Value;
                objTSP.IsAutoConfirm = true;
                objTSP.Logo = "";
                objTSP.Banner = "";
                db.app_TowerSettingPages.InsertOnSubmit(objTSP);
            }
            else
            {
                objTSP.Address = txtAddress.Text;
                objTSP.DisplayName = txtDisplayName.Text;
                objTSP.Hotline = txtHotline.Text;
                objTSP.Id = TowerId.Value;
                objTSP.IsAutoConfirm = true;
                objTSP.BackgroundColorHex = txtColorCode.Text.Trim();
                objTSP.BackgroundColor = chkInputHex.Checked ? -1 : Convert.ToInt32(colorEdit.Text);
                objTSP.IsInputHex = chkInputHex.Checked;
            }

            if (picLogo.Tag != null)
            {
                objTSP.Logo = picLogo.Tag.ToString();
            }

            if (picBanner.Tag != null)
            {
                objTSP.Banner = picBanner.Tag.ToString();
            }

            if (objDetail == null)
            {
                objDetail = new app_TowerSettingPageDetail();
                objDetail.Id = TowerId.Value;
                objDetail.ContentGeneral = txtNoiDung.InnerHtml;
                db.app_TowerSettingPageDetails.InsertOnSubmit(objDetail);
            }
            else
            {
                objDetail.ContentGeneral = txtNoiDung.InnerHtml;
            }

            try
            {
                var objTN = db.tnToaNhas.FirstOrDefault(p => p.MaTN == TowerId);
                if (objTN != null)
                {
                    objTN.HanThanhToan = string.Format("{0}-{1}",spinDeadline.EditValue.ToString(),GetPrefixDeadline(cmbDeadline.Text));// $"{spinDeadline.EditValue.ToString()}-{GetPrefixDeadline(cmbDeadline.Text)}";
                    objTN.ThoiHan = string.Format("{0}-{1}",spinKyThanhToan.EditValue.ToString(),GetPrefix(cmdKyThanhToan.Text));// $"{spinKyThanhToan.EditValue.ToString()}-{GetPrefix(cmdKyThanhToan.Text)}";
                }

                db.SubmitChanges();

                //var connect_string = db.Connection.ConnectionString;
                // lấy từ db, có nhiều trường hợp không lấy được mật khẩu.
                var connect_string = Library.Class.Enum.ConnectString.CONNECT_STRING;
                
                var ma_hoa = Building_KeyConnectString.EncDec.Encrypt(connect_string);

                Class.tbl_building_edit_from_orther_db_insert model_param = new Class.tbl_building_edit_from_orther_db_insert() { Building_Address = objTSP.Address, Building_Code = objTSP.DisplayName, Building_ConnectString = ma_hoa, Building_Matn = objTSP.Id, Building_Name = objTSP.DisplayName, Hotline = objTSP.Hotline, Logo = objTSP.Logo};
                var param = new DynamicParameters();
                param.AddDynamicParams(model_param);
                //param.Add("EmployeeId", employeeId);
                var a = Library.Class.Connect.QueryConnect.QueryAsyncString<Class.tbl_building_edit_from_orther_db_return>("dbo.tbl_building_edit_from_orther_db", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);
            }
            catch {
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

        void SetPrefix(string prefix)
        {
            var dateNow = db.GetSystemDate();
            var fromDate = new DateTime(Convert.ToInt32(dateNow.Year), Convert.ToInt32(dateNow.Month), 1, 0, 0, 0);
            var dateLastOfMonth = Common.GetLastDayOfMonth(dateNow);
            string monthText = dateNow.Month < 10 ? ("0" + dateNow.Month.ToString()) : dateNow.Month.ToString();
            string payFrom = "";

            switch (prefix)
            {
                case "EM"://Đầu tháng đến cuối tháng
                    cmdKyThanhToan.SelectedIndex = 0;
                    payFrom = string.Format("01/{0}/{1} - ", monthText, dateNow.Year); //$"01/{monthText}/{dateNow.Year} - ";
                    payFrom += string.Format("{0}/{1}/{2}", dateLastOfMonth, monthText, dateNow.Year);// $"{dateLastOfMonth.Day}/{monthText}/{dateNow.Year}";
                    break;

                case "IM"://Ngày này tháng trước đến ngày này tháng này
                    cmdKyThanhToan.SelectedIndex = 1;

                    string dayText = Convert.ToInt32(spinKyThanhToan.EditValue) < 10 ? ("0" + spinKyThanhToan.EditValue.ToString()) : spinKyThanhToan.EditValue.ToString();

                    //Tháng trước
                    fromDate = fromDate.AddDays(-1);
                    monthText = fromDate.Month < 10 ? ("0" + fromDate.Month.ToString()) : fromDate.Month.ToString();
                    payFrom = string.Format("{0}/{1}/{2} - ", dayText, monthText, fromDate.Year);// $"{dayText}/{monthText}/{fromDate.Year} - ";

                    //Tháng này
                    monthText = dateNow.Month < 10 ? ("0" + dateNow.Month.ToString()) : dateNow.Month.ToString();
                    payFrom += string.Format("{0}/{1}/{2}", dayText, monthText, dateNow.Year); //$"{dayText}/{monthText}/{dateNow.Year}";
                    break;
            }

            txtKyThanhToan.Text = payFrom;
        }

        string GetPrefix(string val)
        {
            switch (val)
            {
                case "Ngày này tháng trước đến ngày này tháng này":
                    return "IM";
                default: return "EM";//Đầu tháng đến cuối tháng
            }
        }

        string GetPrefixDeadline(string val)
        {
            switch (val)
            {
                case "Tháng này"://Ngày này tháng này
                    return "IM";

                default: return "NM";//Ngày này tháng sau
            }
        }

        void SetPrefixDeadline(string prefix)
        {
            var dateNow = db.GetSystemDate();
            

            var fromDate = new DateTime(Convert.ToInt32(dateNow.Year), Convert.ToInt32(dateNow.Month), 1, 0, 0, 0);
            var dateLastOfMonth = Common.GetLastDayOfMonth(dateNow);
            string monthText = dateNow.Month < 10 ? ("0" + dateNow.Month.ToString()) : dateNow.Month.ToString();
            string dealine = "";
            
            string dayText = Convert.ToInt32(spinDeadline.EditValue) < 10 ? ("0" + spinDeadline.EditValue.ToString()) : spinDeadline.EditValue.ToString();

            switch (prefix)
            {
                case "IM"://Ngày này tháng này
                    cmbDeadline.SelectedIndex = 0;

                    monthText = dateNow.Month < 10 ? ("0" + dateNow.Month.ToString()) : dateNow.Month.ToString();
                    dealine = string.Format("{0}/{1}/{2}", dayText, monthText, dateNow.Year);// $"{dayText}/{monthText}/{dateNow.Year}";
                    break;

                case "NM"://Ngày này tháng sau
                    cmbDeadline.SelectedIndex = 1;

                    dateNow = dateNow.AddMonths(1);
                    monthText = dateNow.Month < 10 ? ("0" + dateNow.Month.ToString()) : dateNow.Month.ToString();
                    dealine = string.Format("{0}/{1}/{2}",dayText,monthText,dateNow.Year);// $"{dayText}/{monthText}/{dateNow.Year}";
                    break;
            }

            txtDeadline.Text = dealine;
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
            var wait = DialogBox.WaitingForm();
            wait.SetCaption("Đang tải hình. Vui lòng chờ...");
            try
            {
                var frm = new FTP.frmUploadFile();
                if (frm.SelectFile(true))
                {
                    picLogo.Tag = "";

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


                    var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecretNoId");
                    picLogo.Tag = ret.Replace("\"", "");
                    picLogo.Image = LoadImage(picLogo.Tag.ToString());

                    
                }
                frm.Dispose();
            }
            catch
            {
                Library.DialogBox.Error("Cần lưu page tòa nhà xong mới upload được ảnh.");
            }

            wait.Close();
            wait.Dispose();
        }

        private void picBanner_DoubleClick(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            wait.SetCaption("Đang tải hình. Vui lòng chờ...");
            try
            {
                var frm = new FTP.frmUploadFile();
                if (frm.SelectFile(true))
                {

                    picBanner.Tag = "";

                    //var mineType = CommonVime.GetMimeType(frm.ClientPath);

                    //byte[] img = File.ReadAllBytes(frm.ClientPath);
                    //CommonVime.GetConfig();

                    //Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                    //var param = new DynamicParameters();
                    //param.AddDynamicParams(model_param);
                    ////param.Add("EmployeeId", employeeId);
                    //var a = Library.Class.Connect.QueryConnect.QueryAsyncString<int>("dbo.tbl_building_get_id", VimeService.isPersonal == false? Library.Class.Enum.ConnectString.CONNECT_MYHOME: Library.Class.Enum.ConnectString.CONNECT_STRING, param);

                    //var model = new
                    //{
                    //    Bytes = img,
                    //    ApiKey = CommonVime.ApiKey,
                    //    SecretKey = CommonVime.SecretKey,
                    //    MineType = mineType,
                    //    IdNew = a.FirstOrDefault(),
                    //    IsEdit = true,
                    //    ImageUrl = "string",
                    //    isPersonal = VimeService.isPersonal
                    //};


                    //var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecretNoId");

                    var ret = GetLinkImage(frm.ClientPath);

                    picBanner.Tag = ret.Replace("\"", "");
                    picBanner.Image = LoadImage(picBanner.Tag.ToString());

                    wait.Close();
                    wait.Dispose();
                }
                frm.Dispose();
            }
            catch
            {
                wait.Close();
                Library.DialogBox.Error("Cần lưu page tòa nhà xong mới upload được ảnh.");
            }

            
        }

        public string GetLinkImage(string clientPath)
        {
            try
            {
                var mineType = CommonVime.GetMimeType(clientPath);

                byte[] img = File.ReadAllBytes(clientPath);
                CommonVime.GetConfig();

                Class.tbl_building_get_id model_param = new Class.tbl_building_get_id() { Building_Code = building_code, Building_MaTN = building_matn };
                var param = new DynamicParameters();
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
                    IsEdit = true,
                    ImageUrl = "string",
                    isPersonal = VimeService.isPersonal
                };


                var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecretNoId");

                return ret;
            }
            catch (System.Exception ex) { return ""; }
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

        private void colorEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkInputHex.Checked)
                {
                    int colorInt = Convert.ToInt32(colorEdit.Text);
                    txtColorCode.Text = ColorTranslator.ToHtml(Color.FromArgb(colorInt));
                }
            }
            catch { }
        }

        private void chkInputHex_EditValueChanged(object sender, EventArgs e)
        {
            colorEdit.Enabled = !chkInputHex.Checked;
            txtColorCode.Enabled = chkInputHex.Checked;
        }

        private void txtColorCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var color = System.Drawing.ColorTranslator.FromHtml(txtColorCode.Text);
                txtTestColor.BackColor = color;
            }
            catch { }
        }

        private void spinKyThanhToan_EditValueChanged(object sender, EventArgs e)
        {
           if(IsTimeLoaded) SetPrefix(GetPrefix(cmdKyThanhToan.Text));
        }

        private void cmdKyThanhToan_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmdKyThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsTimeLoaded) SetPrefix(GetPrefix(cmdKyThanhToan.Text));
        }

        private void spinDeadline_EditValueChanged(object sender, EventArgs e)
        {
            if (IsDealineLoaded) SetPrefixDeadline(GetPrefixDeadline(cmbDeadline.Text));
        }

        private void cmbDeadline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsDealineLoaded) SetPrefixDeadline(GetPrefixDeadline(cmbDeadline.Text));
        }

        private void txtNoiDung_ImageBrowser(object sender, ImageBrowserEventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            if (frm.SelectFile(true))
            {
                var wait = DialogBox.WaitingForm();
                wait.SetCaption("Đang tải hình. Vui lòng chờ...");

                //var mineType = CommonVime.GetMimeType(frm.ClientPath);

                //byte[] img = File.ReadAllBytes(frm.ClientPath);

                //CommonVime.GetConfig();

                //var model = new
                //{
                //    Bytes = img,
                //    ApiKey = CommonVime.ApiKey,
                //    SecretKey = CommonVime.SecretKey,
                //    MineType = mineType
                //};

                //var ret = Building.AppVime.VimeService.PostImage(model, "/Upload/ImageViaApiAndSecret");

                var ret = GetLinkImage(frm.ClientPath);

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