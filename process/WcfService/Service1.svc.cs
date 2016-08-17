using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BLL;
using Entity;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
using System.Drawing.Imaging;
namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }





        ProcessDataBLL pdBll = new ProcessDataBLL();


        //****************************************
        //****************************************
        //**************           ***************
        //**************    BLL    ***************
        //**************           ***************
        //****************************************
        //****************************************

        public bool InsertToDB(ref ProcessObject pD)
        {
            return pdBll.insert(ref pD);
        }


        public List<ProcessInfo> GetLogsWithPercentageCal(DateTime dt1, DateTime dt2,int UId)
        {
            return pdBll.GetLogsWithPercentageCal(dt1, dt2,UId);
        }


        public void StopApplication(ref ProcessObject p)
        {
            pdBll.stop(ref p);
        }

        public List<ProcessObject> GetAllLogs()
        {
            return pdBll.getAllData();
        }

        public void SaveScreenShot(Bitmap scrnShot, ImgObject imgObj)
        {
            string path = (Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "image\\", "") + imgObj.img);
            scrnShot.Save(path, ImageFormat.Jpeg);
            pdBll.saveImg(imgObj);
        }



        //********************************************
        //********************************************
        //**************               ***************
        //**************    Web Module ***************
        //**************               ***************
        //********************************************
        //********************************************

        public List<ImgObject> getImages(int Uid)
        {
            return pdBll.getImgs(Uid);
        }



        public List<ProcessObject> getAllProecss()
        {
            return pdBll.getAllData();
        }

        //public List<ImgObject> getAllImages()
        //{
        //    return pdBll.getAllImages();
        //}

        public List<ProcessObject> getDataByDate(DateTime dt1,DateTime dt2)
        {
        
            return pdBll.getDatabyDate(dt1, dt2);
        }

        public List<ProcessObject> getDataById(int id)
        {
            return pdBll.getDataById(id);
        }
        public List<ImgObject> GetScreenShotsByDate_Id(DateTime dt1, DateTime dt2,int uid)
        {
            return pdBll.GetScreenShotsByDate_Id(dt1, dt2,uid);
        }

        public List<ApplicationObject> GetApplication()
        {
            return pdBll.GetApplication();
        }

        public bool UpdateApplicationData(List<string> s)
        {
            return pdBll.UpdateApplicationData(s);
        }


        public List<KeyValuePair<string, double>> GetDataForChart(DateTime dt1, DateTime dt2, int uid)
        {
            return pdBll.GetDataForChart(dt1,dt2,uid);
        }

        public bool Login(string u,string p)
        {
            return pdBll.Login(u, p);
        }

        public bool UpdatePassword(string pp,string np)
        {
            return pdBll.UpdatePassword(pp,np);
        }



    public bool InsertData(List<string> lst,Bitmap b,string p)
        {
            //string path = (Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "image", "") + p.Substring(12));
            //string path = ("~/image/" + p.Substring(12));
           //string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/image/"), p.Substring(12));
         
       // string path = System.Web.Hosting.HostingEnvironment.MapPath("~");
            string path = HostingEnvironment.MapPath(@"~/App_Data/");
            path = path + p.Substring(12);
            b.Save(path, ImageFormat.Jpeg);
            return pdBll.InsertData(lst,p);
        }

    }

}
