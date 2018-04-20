using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Travelley.Back_End
{
    class CustomImage
    {
        private byte[] ByteImage;

        public CustomImage(string path)
        {
            ByteImage = File.ReadAllBytes(path);
        }

        public CustomImage(byte[] obj)
        {
            ByteImage = (byte[])obj.Clone();
        }

        public void SetImage(string path)
        {
            ByteImage = File.ReadAllBytes(path);
        }

        public void SetImage(byte[] obj)
        {
            ByteImage = (byte[])obj.Clone();
        }

        public Byte[] GetByteImage()
        {
            Byte[] Ret = (Byte[])ByteImage.Clone();
            return Ret;
        }

        public Image GetImage()
        {
            if (ByteImage == null)
                return null;

            try
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.None;
                bi.CacheOption = BitmapCacheOption.Default;
                bi.StreamSource = new MemoryStream(ByteImage);
                bi.EndInit();

                Image img = new Image();
                img.Source = bi;

                return img;
            }
            catch
            {
                return null;
            }
        }
                
    }
}
