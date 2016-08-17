using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
namespace WebModule.Models
{
    public class MHomeController
    {
        public List<string> screenshot()
        {
            DirectoryInfo Folder;
            FileInfo[] Images;

            //Folder = new DirectoryInfo(@"Content/img/");
            //Images = Folder.GetFiles();
            List<String> imagesList = new List<String>();
            var server = HttpContext.Current.Server;
            imagesList.Add( "../Content/img/22 February, 201642422.9150160185.jpeg");
            imagesList.Add("../Content/img/16 March, 201642445.9416247685.jpeg");
            //var server = HttpContext.Current.Server;
            //for (int i = 0; i < Images.Length; i++)
            //{
            //    imagesList.Add("@"+(server.MapPath("~") + (String.Format("{0}", Images[i].Name))));
            //    // Console.WriteLine(String.Format(@"{0}/{1}", folderName, Images[i].Name));
            //}


            return imagesList;
        }


    }
}