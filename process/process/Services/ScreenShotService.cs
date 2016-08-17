using Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace process.Services
{
    class ScreenShotService
    {
        //screenshots code 
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        static public void ScreenShot(Object o, EventArgs e)
        {
            Bitmap screenshotBmp;

            screenshotBmp = new System.Drawing.Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(screenshotBmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, screenshotBmp.Size);
            }

            IntPtr handle = IntPtr.Zero;
            try
            {
                DateTime scrnShotTime = DateTime.Now;
                Bitmap b = new Bitmap(screenshotBmp);
                string path = "screenshots\\"+scrnShotTime.ToOADate()+"-"+Panel.po.UId+"-"+scrnShotTime.ToOADate() + ".jpeg";
                
                b.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                //ImgObject img = new ImgObject();
                //img.img = name;
                //img.UId = Panel.po.UId;
                //img.Time = scrnShotTime;
              
                //ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
                //sc.SaveScreenShot(b, img);
                //sc.Close();
                
               
            }

            finally
            {
                DeleteObject(handle);
            }

        }

    }
}
