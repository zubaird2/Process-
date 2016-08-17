using Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebModule.Models;

namespace WebModule.Controllers
{
    public class ScreenShot
    {
        public IEnumerable<string> Images { get; set; }
    }
    public class HomeController : Controller
    {
        [HttpGet]
        public JsonResult Login(string u, string p)
        {
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            bool sts = sc.Login(u, p);
            if (sts)
                Session["username"] = u;
            return Json(sts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Home");
        }

        public JsonResult UpdatePassword(string pp, string np)
        {
            bool sts = false;
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            sts = sc.UpdatePassword(pp, np);
            sc.Close();
            return Json(sts, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            if (Session["username"] != null)
            //if (Session.Keys.Count>0)
            {
                return View();

            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult ScreenShots()
        {
            return View();
        }

        public ActionResult ViewLogs()
        {
            return View();
        }

        public ActionResult Chart()
        {
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            List<ApplicationObject> applications = sc.GetApplication();
            sc.Close();
            return View(applications);
        }


        public ActionResult SelectApplication()
        {
            if (Session["username"] != null)
            {
                ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
                List<ApplicationObject> applications = sc.GetApplication();
                sc.Close();
                return View(applications);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public JsonResult ViewLogsWithDate_Id(DateTime? s, DateTime? e, int? uid)
        {
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            List<Entity.ProcessInfo> processes = sc.GetLogsWithPercentageCal((DateTime)s, (DateTime)e, (int)uid);
            sc.Close();
            //List<Entity.ProcessObject> processes = sc.getAllProecss();
            return Json(processes, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ViewScreenShotsWithDate_Id(DateTime? s, DateTime? e, int? uid)
        {
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            List<ImgObject> imgs = sc.GetScreenShotsByDate_Id((DateTime)s, (DateTime)e, (int)uid);
            sc.Close();

            return Json(imgs, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public JsonResult UpdateApplicationData(List<string> s)
        {
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            bool r = sc.UpdateApplicationData(s);
            sc.Close();
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDataForChart(DateTime? s, DateTime? e, int? uid)
        {
            ServiceReference1.Service1Client sc = new ServiceReference1.Service1Client();
            List<KeyValuePair<string, double>> lst = sc.GetDataForChart((DateTime)s, (DateTime)e, (int)uid);

            sc.Close();

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}
