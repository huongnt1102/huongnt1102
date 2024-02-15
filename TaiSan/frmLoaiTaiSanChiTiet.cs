using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using Library;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;


namespace TaiSan
{
    public partial class frmLoaiTaiSanChiTiet : DevExpress.XtraEditors.XtraForm
    {
        public int MaLTS { get; set; }
        public tnNhanVien objNhanVien;
        public tsLoaiTaiSan objlts;
        MasterDataContext db = new MasterDataContext();
        tsLoaiTaiSan objLTS;
        public frmLoaiTaiSanChiTiet()
        {
            InitializeComponent();
            TranslateLanguage.TranslateControl(this);
        }

        public void LoadData()
        {
            lookDVT.Properties.DataSource = db.tsLoaiTaiSan_DVTs;
            lookLTSCaptren.Properties.DataSource = db.tsLoaiTaiSans.Select(p => new { p.MaLTS,p.TenLTS});
            lookToaNha.Properties.DataSource = db.tnToaNhas.Select(p => new { p.MaTN, p.TenVT,p.TenTN});
            lookType.Properties.DataSource = db.tsLoaiTaiSan_Types;
            if (MaLTS != 0)
            {
                objLTS = db.tsLoaiTaiSans.Single(p=>p.MaLTS==MaLTS);
                lookDVT.EditValue = (int)objLTS.MaDVT;
                lookLTSCaptren.EditValue = objLTS.MaLTSCHA;
                lookToaNha.EditValue = objLTS.MaTN;
                lookType.EditValue = objLTS.TypeID;
                txtKyHieu.Text = objLTS.KyHieu;
                txtDacTinh.Text = objLTS.DacTinh;
                txtQuocGia.Text = objLTS.QuocGiaSX;
                txtTenLTS.Text = objLTS.TenLTS;
            }
            
        }

        public void SaveData()
        {
            #region rang buoc
            if (txtTenLTS.Text == "")
            {
                DialogBox.Alert("Bạn cần nhập tên loại tài sản. Xin cảm ơn!");
                txtTenLTS.Focus();
                return;
            }
            #endregion
            var wait = DialogBox.WaitingForm();
            try
            {
                if (MaLTS == 0)
                {
                    objLTS = new tsLoaiTaiSan();
                    objLTS.MaNVTao = objNhanVien.MaNV;
                    objLTS.NgayTao = DateTime.Now;
                    db.tsLoaiTaiSans.InsertOnSubmit(objLTS);
                }
                else
                {
                    objLTS = db.tsLoaiTaiSans.Single(p => p.MaLTS == MaLTS);
                    objLTS.MaNVCN = objNhanVien.MaNV;
                    objLTS.NgayCN = DateTime.Now;
                }
                objLTS.KyHieu = txtKyHieu.Text.Trim();
                objLTS.TenLTS = txtTenLTS.Text.Trim();
                objLTS.MaTN = (byte)(lookToaNha.EditValue == null ? 0 : lookToaNha.EditValue);
                objLTS.MaLTSCHA = lookLTSCaptren.EditValue == null ? 0 : (int)lookLTSCaptren.EditValue;
                objLTS.TypeID = lookType.EditValue == null ? 0 : (int)lookType.EditValue;
                objLTS.MaDVT = lookDVT.EditValue == null ? 0 : (int)lookDVT.EditValue;
                objLTS.DacTinh = txtDacTinh.Text.Trim();
                objLTS.QuocGiaSX = txtQuocGia.Text.Trim();
                objLTS.HinhAnh = imageToByteArray(pictureBox1.Image);

                db.SubmitChanges();
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

        //public static byte[] ImageToBinary(string imagePath)
        //{
        //    FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        //    byte[] buffer = new byte[fileStream.Length];
        //    fileStream.Read(buffer, 0, (int)fileStream.Length);
        //    fileStream.Close();
        //    return buffer;
        //}

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