using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
using System.Security.AccessControl;
using Microsoft.Win32;
using ICSharpCode.SharpZipLib.Zip;

namespace Updater
{
    public partial class UpdateVer : DevExpress.XtraEditors.XtraForm
    {
        public UpdateVer()
        {
            //RemoveFormCloseButton();

            InitializeComponent();
        }

        string NewVersion = "";

        void RepairUpdater()
        {
            string PathFile = Application.StartupPath + "\\uninsep.bat";
            string[] cmdLines = {":Repeat", 
                "del \"" + Application.StartupPath + "\\updater.exe\"", 
                "if exist \"" + Application.StartupPath + "\\updater.exe\" goto Repeat ",
                "rename \"" + Application.StartupPath + "\\updaternew.exe\" \"updater.exe\"", 
                "del \"" + PathFile +  "\";" };

            System.IO.FileStream stream = System.IO.File.Create(PathFile);
            stream.Close();
            System.IO.File.WriteAllLines(PathFile, cmdLines);
            Process pLS = new Process();
            pLS.StartInfo.FileName = PathFile;
            pLS.StartInfo.CreateNoWindow = true;
            pLS.StartInfo.UseShellExecute = false;
            try
            {
                pLS.Start();
            }
            catch
            {
            }
        }

        private void UpdateVer_Load(object sender, EventArgs e)
        {
            ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = 437;
            //RegistryKey uac = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
            //if (uac == null)
            //{
            //    uac = Registry.CurrentUser.CreateSubKey(("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"));
            //}
            //uac.SetValue("EnableLUA", 1);
            //uac.Close();

            //var script = PowerShell.Create();
            //script.AddScript("Set-ItemProperty \"HKLM:\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" -Name \"ConsentPromptBehaviorAdmin\" -Value \"0\" ");
            //script.Invoke();


            try
            {
                lblStatus.Text = "Đang tìm kiếm bản cập nhật mới";
                this.Update();

                //Lay duong dan update
                var _UpdateAdress = System.IO.File.ReadAllText(Application.StartupPath + "\\updateaddress.txt");
                _UpdateAdress = _UpdateAdress.Replace("\r\n", "");
                _UpdateAdress = _UpdateAdress.Trim();
                //Lay version moi
                System.Net.WebClient client = new System.Net.WebClient();
                NewVersion = client.DownloadString(_UpdateAdress + "version.txt");

                var oldVersion = "";
                try
                {
                    oldVersion = System.IO.File.ReadAllText(Application.StartupPath + "\\version.txt").Replace("\r\n", "").Trim();
                }
                catch { }

                if (oldVersion.IndexOf(NewVersion) >= 0)
                {
                    lblStatus.Text = "Không có bản cập nhật mới";
                    //btnCancel.Text = "Thoát";
                    return;
                }

                try
                {
                    var noi_dung_update = System.IO.File.ReadAllText(Application.StartupPath + "\\updatelog.txt");
                    memoEdit1.Text = noi_dung_update;
                }
                catch (Exception ex) { }

                foreach (Process p in Process.GetProcesses())
                {
                    if ((p.ProcessName.ToLower() == "landsoftbuilding"))
                        p.Kill();
                }

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadDataCompleted);
                client.DownloadDataAsync(new Uri(_UpdateAdress + "version.zip"));
            }
            catch
            {
                lblStatus.Text = "Không tìm thấy phiên bản mới";
                //btnCancel.Text = "Thoát";
            }
        }

        void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                lblStatus.Text = "Đang cài đặt..";
                progress.Position = 0;
                progress.Properties.Step = 1;

                ZipInputStream zipIn = new ZipInputStream(new MemoryStream(e.Result));
                ZipEntry entry;

                int fileCount = 0;
                while ((entry = zipIn.GetNextEntry()) != null)
                {
                    fileCount++;
                }
                progress.Properties.Maximum = fileCount;

                zipIn = new ZipInputStream(new MemoryStream(e.Result));
                while ((entry = zipIn.GetNextEntry()) != null)
                {
                    progress.PerformStep();
                    this.Update();

                    string path = Application.StartupPath + "\\" + (entry.Name.ToLower() != "updater.exe" ? entry.Name : "updaternew.exe");

                    if (entry.IsDirectory)
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        continue;
                    }

                    //File.Delete(AppDir + entry.Name);
                    FileStream streamWriter = File.Create(path);
                    long size = entry.Size;
                    byte[] data = new byte[size];
                    while (true)
                    {
                        size = zipIn.Read(data, 0, data.Length);
                        if (size > 0) streamWriter.Write(data, 0, (int)size);
                        else break;
                    }
                    streamWriter.Close();

                    //if (entry.Name.ToLower() == "updater.exe")
                    //    RepairUpdater();
                }

                //Luu ver
                string pathVersion = Application.StartupPath + "\\version.txt";
                StreamWriter sw = new StreamWriter(pathVersion);
                sw.WriteLine(NewVersion);
                sw.Close();
                //
                lblStatus.Text = "Cài đặt hoàn tất";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình tải xuống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lblStatus.Text = string.Format("Đang tải: {0:#,0} byte / {1:#,0} byte", e.BytesReceived, e.TotalBytesToReceive);

            double Position = Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(e.TotalBytesToReceive) * 100;
            progress.Position = Convert.ToInt32(Math.Round(Position, 0));
            progress.Update();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateVer_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (this.DialogResult == DialogResult.OK)
                Process.Start(Application.StartupPath + "\\landsoftbuilding.exe");
        }

        /// <summary>
        /// Tắt nút x đóng form, không cho tắt form update
        /// </summary>
        private void RemoveFormCloseButton()
        {
            DevExpress.Skins.Skin currentSkin;
            DevExpress.Skins.SkinElement elementFormButtonClose;

            currentSkin = DevExpress.Skins.FormSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
            elementFormButtonClose = currentSkin[DevExpress.Skins.FormSkins.SkinFormButtonClose];
            elementFormButtonClose.Image.Image = null;
            elementFormButtonClose.Glyph.Image = null;
        }

        #region Disable Close Button
        private const int CS_NOCLOSE = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }
        }
        #endregion

        private void UpdateVer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
            }
        }
    }
}