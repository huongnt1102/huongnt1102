using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Library;

namespace Building.Asset.VanHanh
{
    public partial class frmViewImg : XtraForm
    {
        public long? ID;

        private MasterDataContext _db;

        public frmViewImg()
        {
            InitializeComponent();
        }

        private void frmViewImg_Load(object sender, EventArgs e)
        {
            if (ID != null & ID != 0)
            {
                _db = new MasterDataContext();
                
                var o = _db.tbl_PhieuVanHanh_ChiTiets.FirstOrDefault(_ => _.ID == ID);
                if (o != null)
                {
                    if (o.HinhAnh!=null & o.HinhAnh!="")
                    {
                        var sLImg = o.HinhAnh.Trim('[').Trim(']');
                        var lImg = sLImg.Split(',').ToList();

                        foreach (var i in lImg)
                        {
                            var url = i.Trim('"');
                            if (url != "")
                            {
                                var image = LoadPicture(url);
                                var resizeImage = ResizeImage(image, layoutControl1.Size.Width-30, layoutControl1.Size.Height-30, true);
                                imageSlider1.Images.Add(resizeImage);
                                imageSlider1.MinimumSize = resizeImage.Size;
                            }
                        }
                        imageSlider1.LayoutMode = ImageLayoutMode.MiddleCenter;
                        imageSlider1.SlideNext();

                    }
                }
            }
        }
        public static Bitmap LoadPicture(string url)
        {
            HttpWebRequest wreq;
            HttpWebResponse wresp;
            Stream mystream;
            Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;
            try
            {
                wreq = (HttpWebRequest)HttpWebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                wresp = (HttpWebResponse)wreq.GetResponse();

                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new Bitmap(mystream);
            }
            catch
            {
                //
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();

                if (wresp != null)
                    wresp.Close();
            }

            return bmp;
        }
        public static Image ResizeImage(Image image, int w, int h, bool centerImage)
        {
            if (image == null)
            {
                return null;
            }

            int canvasWidth = w;
            int canvasHeight = h;
            int originalWidth = image.Size.Width;
            int originalHeight = image.Size.Height;

            Image thumbnail =
                new Bitmap(canvasWidth, canvasHeight); 
            Graphics graphic =
                         Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            double ratioX = canvasWidth / (double)originalWidth;
            double ratioY = canvasHeight / (double)originalHeight;
            double ratio = ratioX < ratioY ? ratioX : ratioY; 

            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);

            int posX = Convert.ToInt32((canvasWidth - image.Width * ratio) / 2);
            int posY = Convert.ToInt32((canvasHeight - image.Height * ratio) / 2);

            if (!centerImage)
            {
                posX = 0;
                posY = 0;
            }
            graphic.Clear(Color.White);
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            ImageCodecInfo[] info =
                             ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality,
                             100L);

            Stream s = new MemoryStream();
            thumbnail.Save(s, info[1],
                              encoderParameters);

            return Image.FromStream(s);
        }

        private void itemClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}