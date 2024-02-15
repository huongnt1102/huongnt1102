using System.Drawing;
using System.Drawing.Drawing2D;

namespace Library
{
    /// <summary>
    /// Resize image
    /// </summary>
    public class ResizeImage
    {
        #region Properties
        private string _pathSource;
        #endregion        

        #region Initalize
        public ResizeImage(string pathSource)
        {
            _pathSource = pathSource;
        }

        #endregion

        #region Methods
        public Image ResizeImages(Size size)
        {
            Image imgToResize = Image.FromFile(_pathSource);
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
        #endregion
    }
}