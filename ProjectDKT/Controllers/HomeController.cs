using ProjectDKT.Areas.Admin.Models;
using ProjectDKT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDKT.Controllers
{
    public class HomeController : Controller
    {
        CauVanChuyenDatabaseEntities db = new CauVanChuyenDatabaseEntities();

        public ActionResult Index()
        {
            NewsBannerImage nbi = new NewsBannerImage();
            nbi.Banner = db.Banners.Where(m => m.IsActive == true ).ToList();
            nbi.New = db.News.Where(m => (m.IsActive == true) && (m.Typenew.NameType == "Dịch vụ")).ToList();
            
            return View(nbi);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ImageLoad(int? id)
        {
            byte[] b = (byte[])Session["ContentStream"];
            int length = (int)Session["ContentLength"];
            string type = (string)Session["ContentType"];
            Session["ContentLength"] = null;
            Session["ContentType"] = null;
            Session["ContentStream"] = null;
            return File(b, type);
        }
    }
}