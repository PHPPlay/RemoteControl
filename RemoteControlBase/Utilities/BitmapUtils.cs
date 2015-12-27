using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace iWay.RemoteControlBase.Utilities
{
    public static class BitmapUtils
    {
        public static Bitmap CopyFromScreen()
        {
            Rectangle screen_bounds = Screen.PrimaryScreen.Bounds;
            Bitmap image = new Bitmap(screen_bounds.Width, screen_bounds.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CopyFromScreen(Point.Empty, Point.Empty, screen_bounds.Size);
            graphics.Dispose();
            return image;
        }

        public static byte[] ConvertToBytes(Image image, float quality)
        {
            if (quality < 0.99f)
            {
                int thumbWidth = (int)(image.Width * quality);
                int thumbHeight = (int)(image.Height * quality);
                Image thumb = image.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);
                MemoryStream stream = new MemoryStream();
                thumb.Save(stream, ImageFormat.Jpeg);
                byte[] imageData = stream.ToArray();
                stream.Close();
                thumb.Dispose();
                return imageData;
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                byte[] imageData = stream.ToArray();
                stream.Close();
                return imageData;
            }
        }

        public static Image ConvertFromBytes(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            Image image = Image.FromStream(stream);
            stream.Close();
            return image;
        }

        public static string GetSampleString(Bitmap bmp, int accuracy)
        {
            StringBuilder builder = new StringBuilder();
            Bitmap thumb = (Bitmap)bmp.GetThumbnailImage(accuracy, accuracy, null, IntPtr.Zero);
            int thumbWidth = thumb.Width;
            int thumbHeight = thumb.Height;
            for (int x = 0; x < thumbWidth; x++)
            {
                for (int y = 0; y < thumbHeight; y++)
                {
                    Color oldColor = thumb.GetPixel(x, y);
                    int r = oldColor.R;
                    int g = oldColor.G;
                    int b = oldColor.B;
                    int c = (r * 76 + g * 151 + b * 28) >> 8;
                    builder.Append(c / 26);
                }
            }
            thumb.Dispose();
            return builder.ToString();
        }
    }
}
