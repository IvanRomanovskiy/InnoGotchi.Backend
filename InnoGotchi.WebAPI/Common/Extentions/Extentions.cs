using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace InnoGotchi.WebAPI.Common.Extentions
{
    public static class Extentions
    {
        public static Image ResizeImage(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        public static byte[]? TryScaleImage(string baseStringPicture)
        {
            try
            {
                var value = Convert.FromBase64String(baseStringPicture);
                using (var stream = new MemoryStream(value))
                {
                    var image = Image.FromStream(stream);
                    image.ResizeImage(250, 250);
                    var imageStream = new MemoryStream();
                    image.Save(imageStream, image.RawFormat);
                    var result = imageStream.ToArray();
                    imageStream.Dispose();
                    return result;
                }
            }
            catch { return null; }
        }
    }
}
