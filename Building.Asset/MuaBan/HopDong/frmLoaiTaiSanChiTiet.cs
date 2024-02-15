using System;
using System.Drawing;
using System.Windows.Forms;
using Library;
using System.IO;
using System.Drawing.Drawing2D;


namespace TaiSan
{
    public partial class frmLoaiTaiSanChiTiet : DevExpress.XtraEditors.XtraForm
    {

        public frmLoaiTaiSanChiTiet()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        public void LoadData()
        {

            
        }

        public void SaveData()
        {

            var wait = DialogBox.WaitingForm();
            try
            {

            }
            catch
            {
            }
            finally
            {
                wait.Close();
            }
            this.Close();
        }

        public Image ResizeByWidth(Image img, int width)
        {
            int originalW = img.Width;
            int originalH = img.Height;
            int resizedW = width;
            int resizedH = (originalH * resizedW) / originalW;
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.High;
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            graphic.Dispose();
            return (Image)bmp;
        }


        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public void LoadAnh()
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            Openfile.Filter = Openfile.Filter = "JPG Files (*.jpg)| *.jpg| All files (*.*)|*.*";
            Openfile.FilterIndex = 1;
            Openfile.RestoreDirectory = true;

            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                Image HinhAnh = Image.FromFile(Openfile.FileName);
                pictureBox1.Image = ResizeByWidth(HinhAnh, 176);
                txtHinhAnh.Text = Openfile.FileName;
            }
        }

        private void frmLoaiTaiSanChiTiet_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            LoadAnh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}