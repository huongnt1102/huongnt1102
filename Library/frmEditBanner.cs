using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using FTP;

namespace Library
{
    public partial class frmEditBanner : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? ID;
        public frmEditBanner()
        {
            InitializeComponent();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (buttonEdit1.Text == "")
            {
                DialogBox.Error("Vui lòng chọn banner");
                buttonEdit1.Focus();
                return;
            }
            if (txtTitle.Text == "")
            {
                DialogBox.Error("Vui lòng nhập tiêu đề");
                txtTitle.Focus();
                return;
            }
            if (ID != null)
            {

                var obj = db.app_bannerHomes.FirstOrDefault(p => p.ID == ID);
                if (obj != null)
                {
                    //xóa ảnh cũ
                    if (buttonEdit1.Text != obj.Link)
                    {
                        List<string> files = new List<string>();
                        var cmd = new FtpClient();
                        files.Add(obj.Link);
                        foreach (var url in files)
                        {
                            cmd.Url = url;
                            try
                            {
                                cmd.DeleteFile();
                            }
                            catch { }
                        }
                        //thay ảnh và lưu dữ liệu mới
                        var frm = new frmUploadFile();
                        frm.Folder = "Banner";
                        frm.ClientPath = buttonEdit1.Text.ToString();
                        frm.ShowDialog();
                        obj.ImageURL = frm.FileName;
                        obj.Banner = imageToByteArray();
                    }
                    //Xóa tòa nhà cũ
                    db.app_bannerHome_Towers.DeleteAllOnSubmit(db.app_bannerHome_Towers.Where(p => p.BannerID == ID));
                    obj.Title = txtTitle.Text;
                    if (checkHeader.Checked & (bool?)checkFooter.EditValue == false)
                    {
                        obj.Header = true;
                        obj.Footer = false;
                    }
                    else
                    {
                        obj.Header = false;
                        obj.Footer = true;
                    }
                    obj.Link = txtLink.Text;
                    obj.IsDisplay = (bool)checkHienThi.EditValue;
                    obj.Order_Banner = spinOrders.EditValue==null?1:Convert.ToInt32(spinOrders.EditValue);
                    obj.Update_By = Common.User.MaNV;
                    obj.Update_Date = DateTime.Now;
                    db.SubmitChanges();
                    //Cập nhật tòa nhà mới
                    
                    for (int i = 0; i < grvToaNha.RowCount; i++)
                    {
                        if (Convert.ToBoolean(grvToaNha.GetRowCellValue(i, "Duyet")) == true)
                        {
                            app_bannerHome_Tower objtw = new app_bannerHome_Tower();
                            objtw.TowerID = Convert.ToByte(grvToaNha.GetRowCellValue(i, "MaTN"));
                            objtw.BannerID = ID;
                            objtw.Create_Date = DateTime.Now;
                            objtw.Create_By = Common.User.MaNV;
                            db.app_bannerHome_Towers.InsertOnSubmit(objtw);
                            try
                            {
                                db.SubmitChanges();
                            }
                            catch(Exception ex)
                            {
                                DialogBox.Alert(ex.Message);
                            }
                            
                        }
                    }
                   
                    DialogBox.Alert("Lưu thành công!");
                    this.Close();
                }
            }
            else
            {
                var frm = new frmUploadFile();
                frm.Folder = "Banner";
                frm.ClientPath = buttonEdit1.Text.ToString();
                frm.ShowDialog();
                app_bannerHome obj = new app_bannerHome();
                obj.Link = txtLink.Text;
                obj.Title = txtTitle.Text;
                if (checkHeader.Checked & (bool?)checkFooter.EditValue == false)
                {
                    obj.Header = true;
                    obj.Footer = false;
                }
                else
                {
                    obj.Header = false;
                    obj.Footer = true;
                }
                obj.IsDisplay = (bool)checkHienThi.EditValue;
                obj.ImageURL = frm.FileName;
                obj.Banner = imageToByteArray();
                obj.Order_Banner= spinOrders.EditValue == null ? 1 : Convert.ToInt32(spinOrders.EditValue);
                obj.Create_By = Common.User.MaNV;
                obj.Create_Date = DateTime.Now;
                db.app_bannerHomes.InsertOnSubmit(obj);
                
                for(int i=0;i<grvToaNha.RowCount;i++)
                {
                    if(Convert.ToBoolean(grvToaNha.GetRowCellValue(i,"Duyet"))==true)
                    {
                        app_bannerHome_Tower objtw = new app_bannerHome_Tower();
                        objtw.TowerID = Convert.ToByte(grvToaNha.GetRowCellValue(i,"MaTN"));
                        objtw.Create_Date = DateTime.Now;
                        objtw.Create_By = Common.User.MaNV;
                        obj.app_bannerHome_Towers.Add(objtw);
                    }
                }
                db.SubmitChanges();
                DialogBox.Alert("Lưu thành công!");
                this.Close();
                if (frm.DialogResult != DialogResult.OK) return;
            }
           
        }

        private void frmEditBanner_Load(object sender, EventArgs e)
        {
            GetTower();
            var check = db.app_bannerHomes.FirstOrDefault(p => p.ID == ID);
            if (check != null)
            {
                buttonEdit1.Text = check.Link;
                picBanner.Image = GetImage(check.Banner.ToArray());
                checkHienThi.Checked = (bool)check.IsDisplay;
                checkHeader.EditValue = check.Header;
                checkFooter.EditValue = check.Footer;
                txtTitle.Text = check.Title;
                txtLink.Text = check.Link;
                spinOrders.EditValue = check.Order_Banner;
                GetTowerByBanner(check.ID);
            }
            else
            {
                checkHeader.EditValue = true;
                GetCheckAllTower();
            }
           
        }
        void GetCheckAllTower()
        {
            for(int i=0;i<grvToaNha.RowCount;i++)
            {
                grvToaNha.SetRowCellValue(i, "Duyet", true);
            }
        }
        private void GetTower()
        {
            gcToaNha.DataSource = db.tnToaNhas.Select(p=> new ToaNhaBannerCls {Duyet=false, MaTN=p.MaTN,TenTN=p.TenTN });
        }
        private void GetTowerByBanner(int iBannerID)
        {
            var obj = db.app_bannerHome_Towers.Where(p => p.BannerID == iBannerID).ToList();
            if(obj.Count>0)
            {
                for(int i=0;i<grvToaNha.RowCount;i++)
                {
                    foreach(var item in obj)
                    {
                        if(Convert.ToByte(grvToaNha.GetRowCellValue(i,"MaTN"))==item.TowerID.Value)
                        {
                            grvToaNha.SetRowCellValue(i, "Duyet", true);
                        }
                    }
                }
            }
        }
        public byte[] imageToByteArray()
        {
            MemoryStream ms = new MemoryStream();
            Image imageIn = picBanner.Image;
            try
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch { };
            return ms.ToArray();
        }

        private Image GetImage(byte[] Logo)
        {
            MemoryStream stream = new MemoryStream(Logo);
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            return img;
        }

        private void picBanner_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = GetImageFilter();// "Image Files(*.jpg; *.jpeg;*.png *.gif; *.bmp;)|*.jpg; *jpeg;*.png; *.gif; *.bmp;";
                open.FilterIndex = 2;
                if (open.ShowDialog() == DialogResult.OK)
                {
                    buttonEdit1.Text = open.FileName;
                    picBanner.Image = new Bitmap(open.FileName);
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error("Error: " + ex);
                return;
            }
        }
        private static string GetImageFilter()
        {
            return
                "All Files (*.*)|*.*" +
                "|All Pictures (*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico)" +
                    "|*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico" +
                "|JPEG File Interchange Format (*.jpg;*.jpeg;*.jfif;*.jpe)|*.jpg;*.jpeg;*.jfif;*.jpe" +
                "|Portable Network Graphics (*.png)|*.png" +
                "|Bitmap Image File (*.bmp;*.dib;*.rle)|*.bmp;*.dib;*.rle" +
                "|Tag Image File Format (*.tif;*.tiff)|*.tif;*.tiff" +
                "|Scalable Vector Graphics (*.svg)|*.svg" +
                "|Icon (*.ico)|*.ico";
        }
        private void checkHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHeader.Checked)
            {
                checkFooter.EditValue = false;
            }
        }

        private void checkFooter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFooter.Checked)
            {
                checkHeader.EditValue = false;
            }
        }

       
    }
    class ToaNhaBannerCls
    {
        public bool Duyet { set; get; }
        public byte MaTN { set; get; }
        public string TenTN { set; get; }
    }
}
