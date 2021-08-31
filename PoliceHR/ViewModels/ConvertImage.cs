using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace PoliceHR.ViewModels
{
   public class ConvertImage
    {
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            try
            {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }
            return img;
            }
            catch
            {
                return null;
            }
            
        }
        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            try
            {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
            }
            catch
            {
                return null;
            }
            
        }
    }
}
