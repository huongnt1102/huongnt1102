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
using System.Linq;
using Library;

namespace FTP
{
    public partial class frmDownloadFile : DevExpress.XtraEditors.XtraForm
    {
        public frmDownloadFile()
        {
            InitializeComponent();
        }

        public string FileName { get; set; }

        public string SavePath { get; set; }

        public bool? IsFileMap { get; set; }

        public bool SaveAs()
        {
            using (var frm = new SaveFileDialog())
            {
                frm.FileName = this.FileName.Substring(this.FileName.LastIndexOf('/') + 1);
                frm.Filter = "file|*" + this.FileName.Substring(this.FileName.LastIndexOf('.'));
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    SavePath = frm.FileName;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void UploadFile_frm_Load(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();

                var ftp = new FtpClient();
                ftp.Url = this.FileName;

                long dataLength = ftp.GetFileSize();

                var reader = ftp.DownloadFile();

                MemoryStream memStream = new MemoryStream();
                byte[] buffer = new byte[1024];

                int value = 0;

                while (true)
                {
                    Application.DoEvents();

                    int bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        //Nothing was read, finished downloading
                        progress.Position = 100;
                        lblPross.Text = string.Format("Đã tải xuống {0} trên tổng số {1} bytes...", dataLength, dataLength);

                        Application.DoEvents();
                        break;
                    }
                    else
                    {
                        memStream.Write(buffer, 0, bytesRead);

                        value += bytesRead;

                        if (value <= dataLength)
                        {
                            progress.Position = Convert.ToInt32(value / dataLength * 100);
                            lblPross.Text = string.Format("Đã tải xuống {0} trên tổng số {1} bytes...", value, dataLength);

                            progress.Refresh();
                            Application.DoEvents();
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //Save data
                var downloadedData = memStream.ToArray();
                var filePath = this.SavePath;
                if (this.SavePath == null)
                {
                    var dirPath = Application.StartupPath + "\\cach\\";
                    try
                    {
                        Directory.Delete(dirPath, true);
                    }
                    catch { }
                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);
                    var fileName = this.FileName.Substring(this.FileName.LastIndexOf('/') + 1);
                    filePath = dirPath + Commoncls.TiegVietKhongDau(fileName);
                }

                if (File.Exists(filePath))
                {
                    if (IsFileMap == null)
                        filePath = filePath.Insert(filePath.LastIndexOf('.'), DateTime.Now.ToString("hhmmss"));
                }

                FileStream newFile = new FileStream(filePath, FileMode.Create);
                newFile.Write(downloadedData, 0, downloadedData.Length);
                newFile.Close();

                if (this.SavePath == null)
                {
                    Process.Start(filePath);
                }
                else
                {
                    if (IsFileMap == null)
                        if (DialogBox.Question("Đã tải xuống, bạn có muốn xem lại không?") == DialogResult.Yes)
                        {
                            Process.Start(filePath);
                        }
                }

                reader.Close();
                memStream.Close();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error connecting to the FTP Server.");
            }
        }
    }
}