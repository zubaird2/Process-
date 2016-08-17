using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Entity;
using DAL;
namespace BLL
{
    public class ProcessDataBLL
    {
        static ProcessObject preobject;
        ProcessDataDAL pdDAL = new ProcessDataDAL();
        public bool insert(ref ProcessObject pD)
        {
            bool status = false;
            if ((preobject == null) || (preobject.EndActiveTime != null))
            {
                preobject = pD;   
                //preobject.StartActiveTime = pD.StartActiveTime;
                //preobject.Id = pD.Id;
                //preobject.P_Hieght = pD.P_Hieght;
                //preobject.P_Width = pD.P_Width;
                //preobject.ProcessId = pD.ProcessId;
                //preobject.ProcessName = pD.ProcessName;
                //preobject.ProcessTitle = pD.ProcessName;
                //preobject.UId = pD.UId;
                return true;

            }
            else if (preobject.ProcessTitle.Equals(pD.ProcessTitle) && (preobject.EndActiveTime == null)) { }
            else if ((!preobject.ProcessTitle.Equals(pD.ProcessTitle)) && ((preobject.EndActiveTime == null)))
            {
                preobject.EndActiveTime = pD.StartActiveTime;
                preobject.keyStroke = pD.keyStroke;
                preobject.MouseTime = pD.MouseTime;
                preobject.MouseUseCount = pD.MouseUseCount;
                pdDAL.Save(ref preobject);

                pD.keyStroke = 0;
                pD.MouseTime = TimeSpan.Zero;
                pD.MouseUseCount = 0;
                status = true;
            }
            return status;
        }

        public List<ProcessInfo> GetLogsWithPercentageCal(DateTime dt1, DateTime dt2,int uid)
        {
            double tt = 0;
            double tk = 0;
            double tmt = 0;
            double tmc = 0;
            int id = 0;
            List<ProcessObject> pl = pdDAL.GetDataByDate_Id(dt1,dt2,uid);
            List<ProcessObject> plCopy = new List<ProcessObject>();
            for (int i = 0; i < pl.Count(); i++)
            {
                    ProcessObject p = new ProcessObject();
                    p.EndActiveTime = pl[i].EndActiveTime.Value;
                    p.Id = pl[i].Id;
                    p.keyStroke = pl[i].keyStroke;
                    p.ProcessName = pl[i].ProcessName;
                    p.ProcessTitle = pl[i].ProcessTitle;
                    p.ProcessId = pl[i].ProcessId;
                    p.StartActiveTime = pl[i].StartActiveTime;
                    p.UId = pl[i].UId;
                    p.MouseUseCount = pl[i].MouseUseCount;
                    p.MouseTime = pl[i].MouseTime;

                    TimeSpan ti = (pl[i].EndActiveTime.Value - pl[i].StartActiveTime);
                    tt = tt + (ti.TotalSeconds);

                    ti = pl[i].MouseTime.Value;
                    tmt = tmt + (ti.TotalSeconds);
                    tk = tk + pl[i].keyStroke.Value;
                    tmc = tmc + pl[i].MouseUseCount.Value;


                    plCopy.Add(p);
                
            }

            double tempKey = 0;
            double t = 0;
            double mc = 0;
            TimeSpan mt = new TimeSpan(0, 0, 0, 0);

            List<ProcessInfo> pI = new List<ProcessInfo>();
            for (int i = 0; i < pl.Count(); i++)
            {
                tempKey = 0;
                t = 0;
                mc = 0;
                mt = DateTime.Now - DateTime.Now;
                ProcessInfo pITemp = new ProcessInfo();
                pITemp.UserId = plCopy[i].UId;
                for (int j = 0; j < plCopy.Count(); j++)
                {
                    if (pl[i].ProcessName.Equals(plCopy[j].ProcessName))
                    {
                        pITemp.ProcessName = pl[i].ProcessName;
                        pITemp.ProcessTitle = pl[i].ProcessTitle;
                        tempKey = tempKey + plCopy[j].keyStroke.Value;


                        TimeSpan ti = (plCopy[j].EndActiveTime.Value - plCopy[j].StartActiveTime);
                        t = t + (ti.TotalSeconds);

                        mc = mc + Convert.ToInt32(plCopy[j].MouseUseCount.Value);
                        mt = mt + (TimeSpan)(plCopy[j].MouseTime.Value);

                        plCopy[j].ProcessName = " ";

                    }
                }
                if (pITemp.ProcessName != null)
                {
                    pITemp.PerKeyStroke = Math.Round((tempKey / tk) * 100, 2);
                    pITemp.PerMouseCount = Math.Round((mc / tmc) * 100, 2);
                    pITemp.PercentageMouseTime = Math.Round(((mt.TotalSeconds) / tmt) * 100, 2);
                    pITemp.PercentageProcessActiveTime = Math.Round((t / tt) * 100, 2);
                    id++;
                    pITemp.Id = id;
                    pI.Add(pITemp);

                }


            }

            return pI;
        }

        public void stop(ref ProcessObject p)
        {
                if (preobject.EndActiveTime == null)
                {
                    preobject.EndActiveTime = DateTime.Now;
                    preobject.keyStroke = p.keyStroke;
                    preobject.MouseTime = p.MouseTime;
                    preobject.MouseUseCount = p.MouseUseCount;
                    p.MouseUseCount = 0;
                    p.MouseTime = TimeSpan.Zero;
                    p.keyStroke = 0;
                }
            pdDAL.endTime(preobject);
        }

        public List<ProcessObject> getAllData()
        {
            return pdDAL.getList();
        }


        public void saveImg(ImgObject imgObj)
        {
            pdDAL.saveimg(imgObj);
        }



        //********************************************
        //********************************************
        //**************               ***************
        //**************    Web Module ***************
        //**************               ***************
        //********************************************
        //********************************************

        public List<ImgObject> getImgs(int Uid)
        {
            return pdDAL.getImages(Uid);

        }


        public List<ImgObject> getAllImages()
        {
            return pdDAL.getAllImages();
        }

        public List<ProcessObject> getDatabyDate(DateTime dt1, DateTime dt2)
        {
            List<ProcessObject> pl = pdDAL.getDataByDate(dt1,dt2);
            return pl;

        }

        public List<ProcessObject> getDataById(int id)
        {
            List<ProcessObject> pl = pdDAL.getDataById(id);
            return pl;
        }
        public List<ImgObject> GetScreenShotsByDate_Id(DateTime dt1, DateTime dt2,int uid)
        {
            List<ImgObject> il = pdDAL.GetScreenShotsByDate_Id(dt1, dt2, uid);

            return il;
        }
        
        public List<ApplicationObject> GetApplication()
        {
            return pdDAL.GetApplication();
        }

        public bool UpdateApplicationData(List<string> s)
        {
            string[] lst;
            lst = s[0].Split(',');
            List<ApplicationObject> list = pdDAL.GetApplication();
            for (int i = 0; i < list.Count; i++)
            {
                ApplicationObject ao = list[i];
                for (int lc = 0; lc < lst.Length; lc = lc + 2)
                {
                    if (lst[lc].Equals(ao.App_Name))
                    {
                        ao.Productivity = lst[lc + 1];
                        
                        break;
                    }
                }
            }
            return pdDAL.UpdateApplicationData(list);
        }


        public List<KeyValuePair<string, double>> GetDataForChart(DateTime s,DateTime e,int uid)
        {
            double tt = 0;
            List<KeyValuePair<string, double>> lst = new List<KeyValuePair<string,double>>();
            List<ProcessObject> pl = pdDAL.GetDataByDate_Id(s, e, uid);
            List<ProcessObject> plCopy = new List<ProcessObject>();
            for (int i = 0; i < pl.Count(); i++)
            {
                ProcessObject p = new ProcessObject();
                p.EndActiveTime = pl[i].EndActiveTime.Value;
                p.Id = pl[i].Id;
                p.keyStroke = pl[i].keyStroke;
                p.ProcessName = pl[i].ProcessName;
                p.ProcessTitle = pl[i].ProcessTitle;
                p.ProcessId = pl[i].ProcessId;
                p.StartActiveTime = pl[i].StartActiveTime;
                p.UId = pl[i].UId;
                p.MouseUseCount = pl[i].MouseUseCount;
                p.MouseTime = pl[i].MouseTime;

                TimeSpan ti = (pl[i].EndActiveTime.Value - pl[i].StartActiveTime);
                tt = tt + (ti.TotalSeconds);


                plCopy.Add(p);

            }

            double t = 0;
            List<ProcessInfo> pI = new List<ProcessInfo>();
            List<ApplicationObject> AppLst = pdDAL.GetApplication();
            for (int i = 0; i < pl.Count(); i++)
            {
                t = 0;
                string name="";
                for (int j = 0; j < plCopy.Count(); j++)
                {
                    if (pl[i].ProcessName.Equals(plCopy[j].ProcessName))
                    {
                        name = pl[i].ProcessName;
                        TimeSpan ti = (plCopy[j].EndActiveTime.Value - plCopy[j].StartActiveTime);
                        t = t + (ti.TotalSeconds);
                        plCopy[j].ProcessName = " ";

                    }
                }
                if (name != "")
                {
                    double pt = Math.Round((t / tt) * 100, 2);
                    KeyValuePair<string, double> var = new KeyValuePair<string, double>(name,pt);
                    lst.Add(var);

                }


            }

            for (int i = 0; i < lst.Count; i++)
            {
                for (int j = 0; j < AppLst.Count; j++)
                {
                    if (lst[i].Key.Equals(AppLst[j].Exe_Name))
                    {
                        string n = AppLst[j].App_Name;
                        if (AppLst[j].Productivity.Equals("Productive"))
                            n = n + ",p";
                        else if (AppLst[j].Productivity.Equals("UnProductive"))
                            n = n + ",u";
                        else
                            n = n + ",o";
                        KeyValuePair<string, double> v = new KeyValuePair<string, double>(n, lst[i].Value);
                        lst.RemoveAt(i);
                        //lst.Add(v);
                        lst.Insert(i, v);
                    }
                }
            }
                return lst;
        }


        public bool Login(string u,string p)
        {
            List<UserObjects> v = pdDAL.Login(u,p);
            if (v.Count==0)
                return false;
            else
                return true;
        }

        public bool UpdatePassword(string pp, string np)
        {
            List<UserObjects> v = pdDAL.GetUser(pp);
            if (v.Count == 0)
                return false;
            else
            {
                v[0].Password = np;
                pdDAL.savep(v);

                return true;
            }

        }



        public bool InsertData(List<string> lst,string img)
        {
            List<ProcessObject> POLst = new List<ProcessObject>();
            
            for(int i=0;i<lst.Count;i++)
            {
                string[] s = lst[i].Split(',');
                ProcessObject p = new ProcessObject();
                p.ProcessId=Convert.ToInt32(s[0]);
                p.ProcessName=s[1];
                p.ProcessTitle=s[2];
                p.StartActiveTime=Convert.ToDateTime(s[3]);
                p.EndActiveTime = Convert.ToDateTime(s[4]);
                p.keyStroke=Convert.ToInt32(s[5]);
                p.MouseTime=TimeSpan.Parse(s[6]);
                p.MouseUseCount = Convert.ToInt32(s[7]);
                p.P_Hieght=Convert.ToInt32(s[8]);
                p.P_Width=Convert.ToInt32(s[9]);
                p.UId=Convert.ToInt32(s[10]);
                POLst.Add(p);
            }

            string ss = img.Substring(12);
            string[] ls = ss.Split('-');
            ImgObject imgObj = new ImgObject();
            imgObj.UId = Convert.ToInt32(ls[1]);
            double n = Convert.ToDouble(ls[0]);
            imgObj.Time = DateTime.FromOADate(n);
            imgObj.img = ss;
            return pdDAL.InsertData(POLst,imgObj);
        }
    }
}
