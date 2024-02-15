using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Win32;
using System.Management;
using Library;

namespace Building.AppVime
{
    public partial class frmVer : DevExpress.XtraEditors.XtraForm
    {

        public frmVer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                if (numAndroidVer.Value == 0 && numAndroidVer.Value == 0)
                {

                    DialogBox.Error("Vui lòng nhập version!");
                    return;
                }
                else if (numAndroidVer.Value != 0 && numIOSVer.Value == 0)
                {
                    var objVer = new app_Version()
                    {
                        Version = (int)numAndroidVer.Value,
                        DateCreated = DateTime.Now,
                        OperatingSystem = true,// 1: Android, 0: IOS

                    };
                    db.app_Versions.InsertOnSubmit(objVer);

                    var objVerI = new app_Version()
                    {
                        Version = int.Parse(lbIOS.Text),
                        DateCreated = DateTime.Now,
                        OperatingSystem = false,// 1: Android, 0: IOS

                    };
                    db.app_Versions.InsertOnSubmit(objVerI);


                }
                else if (numAndroidVer.Value == 0 && numIOSVer.Value != 0)
                {
                    var objVer = new app_Version()
                    {
                        Version = (int)numAndroidVer.Value,
                        DateCreated = DateTime.Now,
                        OperatingSystem = false,// 1: Android, 0: IOS

                    };
                    db.app_Versions.InsertOnSubmit(objVer);
                    var objVerA = new app_Version()
                    {
                        Version = int.Parse(lbAndroid.Text),
                        DateCreated = DateTime.Now,
                        OperatingSystem = true,// 1: Android, 0: IOS

                    };
                    db.app_Versions.InsertOnSubmit(objVerA);
                }
                else
                {

                    var objVerA = new app_Version()
                    {
                        Version = (int)numAndroidVer.Value,
                        DateCreated = DateTime.Now,
                        OperatingSystem = true,// 1: Android, 0: IOS
                    };
                    var objVerI = new app_Version()
                    {
                        Version = (int)numIOSVer.Value,
                        DateCreated = DateTime.Now,
                        OperatingSystem = false,// 1: Android, 0: IOS
                    };

                    db.app_Versions.InsertOnSubmit(objVerI);
                    db.app_Versions.InsertOnSubmit(objVerA);
                }
                db.SubmitChanges();
                DialogBox.Alert("Đã cập nhật thông tin phiên bản mới!");
                LoadData();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void LoadData()
        {
            using (var db = new MasterDataContext())
            {
                var objVerA = db.app_Versions.OrderByDescending(p => p.DateCreated).FirstOrDefault(p => p.OperatingSystem == true);
                var objVerI = db.app_Versions.OrderByDescending(p => p.DateCreated).FirstOrDefault(p => p.OperatingSystem == false);
                lbAndroid.Text = objVerA != null? objVerA.Version== null? "0": objVerA.Version.ToString():"0";
                lbIOS.Text = objVerI != null? objVerI.Version == null ? "0": objVerI.Version.ToString(): "0";
                numAndroidVer.Value = 0;
                numIOSVer.Value = 0;
            }
        }
        private void frmVer_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}