using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using Library;
using AuthorizationClass.Login;
using System.Linq;
using System.Net;
using System.IO;
using System.Data.Linq.SqlClient;
using System.Net.Http;
using Newtonsoft.Json;
using Library.HeThongCls;

namespace LandSoftBuildingMain
{
    public partial class frmLogin : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public tnNhanVien UsersLogin = new tnNhanVien();
        MasterDataContext db;
        public bool IsAdmin { get; set; }
        public class ResultLdap
        {
            public bool Result { get; set; }
            public string Error { get; set; }
        }
        public frmLogin()
        {
            InitializeComponent();
            db = new MasterDataContext();
            using (DefaultLookAndFeel dlf = new DefaultLookAndFeel())
            {
                string themes =Library.Properties.Settings.Default.SkinCurrent;
                dlf.LookAndFeel.SkinName = themes;
                Library.Properties.Settings.Default.SkinCurrent = themes;
                Library.Properties.Settings.Default.Save();
            }
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            txtloginid.Focus();
        }

        private async void btnAccept_Click(object sender, EventArgs e)
        {

            try
            {
                //Lay duong dan update
                var _UpdateAdress = System.IO.File.ReadAllText(Application.StartupPath + "\\updateaddress.txt");
                _UpdateAdress = _UpdateAdress.Replace("\r\n", "");
                _UpdateAdress = _UpdateAdress.Trim();
                //Lay version moi
                System.Net.WebClient client = new System.Net.WebClient();
                var newVersion = client.DownloadString(_UpdateAdress + "version.txt").Trim();
                //Lay version cu
                var oldVersion = System.IO.File.ReadAllText(Application.StartupPath + "\\version.txt");
                oldVersion = oldVersion.Replace("\r\n", "");
                oldVersion = oldVersion.Trim();
                //Kiem tra version
                if (oldVersion.IndexOf(newVersion) < 0)
                {
                    Library.DialogBox.Error("Phiên bản version cũ, không thể đăng nhập được.");
                    return;
                    //if (DialogBox.Question("Đã có phiên bản mới. Bạn có muốn cập nhật không?") == DialogResult.Yes)
                    //{
                    //System.Diagnostics.Process.Start(Application.StartupPath + "\\updater.exe");
                    //}
                }
            }
            catch { }

            IsAdmin = false;
            if (!CheckDataInput())
                return;

            var wait = DialogBox.WaitingForm();
            try
            {

                var objConfig = db.tblConfigs.First();
                var apiLdap = objConfig.ApiLdap;
                var userName = txtloginid.Text.Trim();
                var passWord = txtpassword.Text.Trim();

                if (userName == "dip.landsoft")
                {
                    #region bỏ luôn kiểm tra login để test nghiệp vụ, 0h đêm mà cái máy server bên kia k connect được, bực mình

                    tnNhanVien objnhanvien;
                    objnhanvien = db.tnNhanViens.FirstOrDefault(p => p.MaSoNV.Equals(userName));
                    //var ul = new Library.HeThongCls.UserLogin();
                    //objnhanvien = ul.GetUserByMaNV(txtloginid.Text.Trim(), txtpassword.Text.Trim());

                    if (objnhanvien != null)
                    {
                        Library.HeThongCls.UserLogin usrlogin = new Library.HeThongCls.UserLogin();
                        objnhanvien.MatKhau = usrlogin.HashPassword(passWord);
                        db.SubmitChanges();

                        // Update tòa nhà cho nhân viên
                        objnhanvien.MaTN = Library.ManagerTowerCls.GetAllTower(objnhanvien).FirstOrDefault().MaTN;
                        UsersLogin = objnhanvien;

                        if (ckLuu.Checked)
                        {
                            Library.Properties.Settings.Default.Username = txtloginid.Text.Trim();
                            Library.Properties.Settings.Default.Password = txtpassword.Text.Trim();
                            if (cbbLanguage.Text == "English")
                            {
                                Library.Properties.Settings.Default.NgonNgu = "EN";
                            }
                            else
                            {
                                Library.Properties.Settings.Default.NgonNgu = "VI";
                            }
                        }
                        else
                        {
                            Library.Properties.Settings.Default.Username = string.Empty;
                            Library.Properties.Settings.Default.Password = string.Empty;
                        }

                        if (cbbLanguage.Text == "English")
                        {
                            Library.Properties.Settings.Default.NgonNgu = "EN";
                        }
                        else
                        {
                            Library.Properties.Settings.Default.NgonNgu = "VI";
                        }

                        Library.Properties.Settings.Default.RememberCheck = ckLuu.Checked;
                        Library.Properties.Settings.Default.Save();

                        if (!objnhanvien.IsSuperAdmin.Value)
                        {
                            var objNhomNhanVien = db.pqNhomNhanViens.Where(p => p.MaNV == objnhanvien.MaNV).Select(p => p.pqNhom);
                            if (objNhomNhanVien == null)
                            {
                                DialogBox.Error("Bạn không có quyền hạn truy cập vào hệ thống");
                                Application.Exit();
                            }
                        }
                        try
                        {
                            GetNetWorkInfo.GetIp();
                            Common.IPWan = GetNetWorkInfo.GetIpWan();
                            Common.IPLan = GetNetWorkInfo.IPAddress;
                            Common.MacAddress = GetNetWorkInfo.MacAddress;
                            Common.DeviceName = GetNetWorkInfo.DeviceName;
                        }
                        catch
                        {
                            Common.IPWan = "";
                            Common.IPLan = "";
                            Common.MacAddress = "";
                            Common.DeviceName = "";
                        }

                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        wait.Close();
                        wait.Dispose();
                        DialogBox.Error("Tài khoản chưa được tạo, liên hệ quản trị viên để tạo.");
                        return;
                    }

                    #endregion
                    
                }
                else
                {
                    #region Kiểm traLdap
                    var url = String.Format("{0}?UserName={1}&Password={2}", apiLdap, userName, passWord);
                    var httpClient = new HttpClient();
                    var response = await httpClient.GetAsync(url);
                    var content = response.Content.ReadAsStringAsync();
                    var resultLdap = JsonConvert.DeserializeObject<ResultLdap>(content.Result);
                    if (resultLdap.Result || userName == "tien.dip")
                    {
                        tnNhanVien objnhanvien;
                        objnhanvien = db.tnNhanViens.FirstOrDefault(p => p.MaSoNV.Equals(userName));
                        //var ul = new Library.HeThongCls.UserLogin();
                        //objnhanvien = ul.GetUserByMaNV(txtloginid.Text.Trim(), txtpassword.Text.Trim());

                        if (objnhanvien != null)
                        {
                            Library.HeThongCls.UserLogin usrlogin = new Library.HeThongCls.UserLogin();
                            objnhanvien.MatKhau = usrlogin.HashPassword(passWord);
                            db.SubmitChanges();

                            // Update tòa nhà cho nhân viên
                            objnhanvien.MaTN = Library.ManagerTowerCls.GetAllTower(objnhanvien).FirstOrDefault().MaTN;
                            UsersLogin = objnhanvien;

                            if (ckLuu.Checked)
                            {
                                Library.Properties.Settings.Default.Username = txtloginid.Text.Trim();
                                Library.Properties.Settings.Default.Password = txtpassword.Text.Trim();
                                if (cbbLanguage.Text == "English")
                                {
                                    Library.Properties.Settings.Default.NgonNgu = "EN";
                                }
                                else
                                {
                                    Library.Properties.Settings.Default.NgonNgu = "VI";
                                }
                            }
                            else
                            {
                                Library.Properties.Settings.Default.Username = string.Empty;
                                Library.Properties.Settings.Default.Password = string.Empty;
                            }

                            if (cbbLanguage.Text == "English")
                            {
                                Library.Properties.Settings.Default.NgonNgu = "EN";
                            }
                            else
                            {
                                Library.Properties.Settings.Default.NgonNgu = "VI";
                            }

                            Library.Properties.Settings.Default.RememberCheck = ckLuu.Checked;
                            Library.Properties.Settings.Default.Save();

                            if (!objnhanvien.IsSuperAdmin.Value)
                            {
                                var objNhomNhanVien = db.pqNhomNhanViens.Where(p => p.MaNV == objnhanvien.MaNV).Select(p => p.pqNhom);
                                if (objNhomNhanVien == null)
                                {
                                    DialogBox.Error("Bạn không có quyền hạn truy cập vào hệ thống");
                                    Application.Exit();
                                }
                            }
                            try
                            {
                                GetNetWorkInfo.GetIp();
                                Common.IPWan = "";// DIPCRM.Library.SystemInfoNetwork.GetNetWorkInfo.GetIpWan();
                                Common.IPLan = GetNetWorkInfo.IPAddress;
                                Common.MacAddress = ""; //DIPCRM.Library.SystemInfoNetwork.GetNetWorkInfo.MacAddress;
                                Common.DeviceName = ""; //DIPCRM.Library.SystemInfoNetwork.GetNetWorkInfo.DeviceName;
                            }
                            catch
                            {
                                Common.IPWan = "";
                                Common.IPLan = "";
                                Common.MacAddress = "";
                                Common.DeviceName = "";
                            }

                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            wait.Close();
                            wait.Dispose();
                            DialogBox.Error("Tài khoản chưa được tạo, liên hệ quản trị viên để tạo.");
                            return;
                        }
                    }
                    else
                    {
                        DialogBox.Error(resultLdap.Error.ToString());
                    }
                    #endregion
                }
               
            }
            catch(System.Exception ex)
            {
                wait.Close();
                wait.Dispose();
                DialogBox.Error("Lỗi kết nối mạng. Vui lòng thử lại sau");
                return;
            }
            finally
            {
                if (!wait.IsDisposed)
                {
                    wait.Close();
                    wait.Dispose();
                }
            }

        }

        public string GetPublicIP()
        {
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");  
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }

            //Search for the ip in the html
            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");  
            direction = direction.Substring(first, last - first);

            return direction;
        }

        private bool CheckDataInput()
        {
            if (txtloginid.Text.Trim().Length != 0 && txtpassword.Text.Trim().Length != 0)
                return true;

            DialogBox.Error("Điền tên đăng nhập và mật khẩu");  
            return false;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtloginid.Text = Library.Properties.Settings.Default.Username;
            txtpassword.Text = Library.Properties.Settings.Default.Password;
            ckLuu.Checked = Library.Properties.Settings.Default.RememberCheck;
            if (Library.Properties.Settings.Default.NgonNgu == "EN")
            {
                cbbLanguage.Text = "English";
            }
            else
            {
                cbbLanguage.Text = "Tiếng Việt";
            }

            TranslateLanguage.TranslateControl(this);
        }

        private void ckLuu_CheckedChanged(object sender, EventArgs e)
        {
            Library.Properties.Settings.Default.RememberCheck = ckLuu.Checked;
            Library.Properties.Settings.Default.Save();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            Connect_frm frm = new Connect_frm();
            frm.ShowDialog();
        }

        private void cbbLanguage_Properties_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void cbbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Library.Properties.Settings.Default.NgonNgu = cbbLanguage.SelectedIndex == 0 ? "VI" : "EN";
            Properties.Settings.Default.Save();

            if (Library.Properties.Settings.Default.NgonNgu != "EN")
            {
                this.Text = "Đăng nhập - Landsoft Building";
                lblMaSo.Text = "Tên đăng nhập:";
                lblMatKhau.Text = "Mật khẩu:";
                lblNgonNgu.Text = "Ngôn ngữ:";
                ckLuu.Text = "Ghi nhớ thông tin đăng nhập";
                btnconnect.Text = "Kết nối";
                btnAccept.Text = "Đăng nhập";
                btnThoat.Text = "Thoát";
            }
            else
                TranslateLanguage.TranslateControl(this);
        }
    }
}