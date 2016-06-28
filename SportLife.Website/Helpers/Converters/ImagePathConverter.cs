using System;
using System.Drawing;
using System.IO;

namespace SportLife.Website.Helpers.Converters {
    public static class ImagePathConverter {
        public static Uri GetDefaultImage()
        {
            return new Uri(@"..\Media\Images\default.png");
        }

        public static byte[] ImageToByteArray ( System.Drawing.Image imageIn ) {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage ( byte[] byteArrayIn ) {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}