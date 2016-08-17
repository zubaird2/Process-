using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
namespace DAL
{

    public class ProcessDataDAL
    {

        public void Save(ref ProcessObject p)
        {
            MyDbContext context = new MyDbContext();
            context.ProcessDatas.Add(p);
            context.SaveChanges();
            context.Dispose();
        }

        public List<ProcessObject> GetDataByDate_Id(DateTime dt1,DateTime dt2,int uid)
        {
            MyDbContext context = new MyDbContext();
            List<ProcessObject> pl = context.ProcessDatas.Where(m => m.StartActiveTime >= dt1 && m.StartActiveTime <= dt2 && m.UId == uid).ToList();
            context.Dispose();
            return pl;
        }
   

        public void endTime(ProcessObject p)
        {
            MyDbContext context = new MyDbContext();
            context.ProcessDatas.Add(p);
            context.SaveChanges();
            context.Dispose();
        }


        public List<ProcessObject> getList()
        {

            //KinectDatabaseEntities context = new KinectDatabaseEntities();
            MyDbContext context = new MyDbContext();
            List<ProcessObject> v = (from x in context.ProcessDatas
                                     select x).ToList();
            context.Dispose();
            return v;

        }

        public void saveimg(ImgObject imgObj)
        {
            
            MyDbContext context = new MyDbContext();
            context.ImgDatas.Add(imgObj);
            context.SaveChanges();
            context.Dispose();

        }


        public bool InsertData(List<ProcessObject> lst,ImgObject img)
        {
            MyDbContext context = new MyDbContext();
            for(int i=0;i<lst.Count;i++)
            {
                context.ProcessDatas.Add(lst[i]);
            }
            context.ImgDatas.Add(img);
            context.SaveChanges();
            context.Dispose();
            return true;
            

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
            MyDbContext context = new MyDbContext();
            List<ImgObject> imgs = context.ImgDatas.Where(m => m.UId == Uid).ToList();
            context.Dispose();
            foreach (var v in imgs)
            {
                v.img = ("http://localhost:6358/image/" + v.img);

            }
            return imgs;
            //var result=from context in 
        }


        public List<ImgObject> getAllImages()
        {
            MyDbContext context = new MyDbContext();
            List<ImgObject> imgs = context.ImgDatas.ToList();
            context.Dispose();
            foreach(var v in imgs)
            {
                v.img = ("http://localhost:6358/image/" + v.img);
            }
            return imgs;
        }

        public List<ImgObject> GetScreenShotsByDate_Id(DateTime dt1, DateTime dt2,int uid)
        {
            MyDbContext context = new MyDbContext();
            List<ImgObject> imgs = context.ImgDatas.Where(m => m.Time >= dt1 && m.Time <= dt2 && m.UId==uid).ToList();
            context.Dispose();
            foreach (var v in imgs)
            {
                v.img = ("http://service-21.apphb.com/App_Data/" + v.img);
            }
            return imgs;
        }

        public List<ProcessObject> getDataByDate(DateTime dt1,DateTime dt2)
        {
            MyDbContext context = new MyDbContext();
            List<ProcessObject> lst = context.ProcessDatas.Where(m => m.StartActiveTime >= dt1 && m.StartActiveTime <= dt2).ToList();
            context.Dispose();
            return lst;
        }

        public List<ProcessObject> getDataById(int id)
        {
            MyDbContext context = new MyDbContext();
            List<ProcessObject> lst = context.ProcessDatas.Where(m => m.UId == id).ToList();
            context.Dispose();
            return lst;
        }

        MyDbContext cntx = new MyDbContext();
        public List<ApplicationObject> GetApplication()
        {
            //MyDbContext context = new MyDbContext();
            List<ApplicationObject> lst = cntx.Applications.ToList();
            return lst;
        }

        public bool UpdateApplicationData(List<ApplicationObject> s)
        {
          

            cntx.SaveChanges();
            cntx.Dispose();
                return true;
        }

        public List<UserObjects> Login(string u,string p)
        {
            MyDbContext context = new MyDbContext();
            List<UserObjects> v = context.Users.Where(m => m.UserName == u && m.Password == p).ToList();
            return v;
        }

        MyDbContext contx = new MyDbContext();
        public List<UserObjects>   GetUser(string pp)
        {
            List<UserObjects> v = contx.Users.Where(m => m.Password == pp).ToList();
            return v;
        }

        public void savep(List<UserObjects> u)
        {
            contx.SaveChanges();
            contx.Dispose();
        }
    }


   
}
