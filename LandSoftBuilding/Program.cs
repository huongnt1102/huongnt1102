using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using Library;
using LandSoftBuildingMain;
using System.Linq;
using DevExpress.XtraEditors;
namespace LandSoftBuilding
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-VN");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");

            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalDigits = 0;
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "VNÐ";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ",";

            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalDigits = 0;
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentGroupSeparator = ".";

            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WindowsFormsSettings.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();

            frmBanner frmb = new frmBanner();
            frmb.Show();
            #region Kiem tra version
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
                    //if (DialogBox.Question("Đã có phiên bản mới. Bạn có muốn cập nhật không?") == DialogResult.Yes)
                    //{
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\updater.exe");
                    return;
                    //}
                }
            }
            catch { }
            #endregion

            #region Check ket noi sql
            var sqlInfo = new Library.SqlSetting();
            if (sqlInfo.Conn != "")
            {
                Building.SystemLog.Classes.SqlHelper.ConnectString = it.EncDec.Decrypt(sqlInfo.Conn);
                sqlInfo.SqlConn = it.EncDec.Decrypt(sqlInfo.Conn);
                sqlInfo.Save();
            }
            else
            {
                using (Connect_frm frmConnect = new Connect_frm())
                {
                    frmConnect.ShowDialog();
                    if (frmConnect.DialogResult != DialogResult.OK)
                    {
                        frmb.Close();
                        return;
                    }
                }
            }

            if (!Library.SqlCommon.sqlTestConnect(sqlInfo.SqlConn))
            {
                using (Connect_frm frmConnect = new Connect_frm())
                {
                    frmConnect.ShowDialog();
                    if (frmConnect.DialogResult != DialogResult.OK)
                    {
                        frmb.Close();
                        return;
                    }
                }
            }
            #endregion

            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            if (frm.DialogResult != DialogResult.OK)
            {
                return;
            }
            
            frmb.Close();

            Library.Common.User = frm.UsersLogin;
            Library.Common.TowerList = Library.ManagerTowerCls.GetAllTower(frm.UsersLogin);

            #region Load tong dai
            // DIP.SwitchBoard.SwitchBoard.SoftPhone = new DIP.SwitchBoard.SoftPhones();
            #endregion

            // Mã HĐ + Password Amazon
            //EmailAmazon.MailCommon.MaHD = 3152;
            //EmailAmazon.MailCommon.MatKhau = "5LNAA4NV7OT9227GEXC581A9OXEJAT";
            try
            {
                frmMain frmmain = new frmMain()
                {
                    IsAdmin = frm.IsAdmin,
                    User = frm.UsersLogin
                };
                try
                {
                    Application.Run(frmmain);
                }
                catch (NullReferenceException e)
                {
                    // Do something with e, please.
                }
                
            }
            catch { }
        }
    }
}