using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDKT.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Test
        public ActionResult Index()
        {
            ViewBag.number = "huynhẻtyuewrtyret";
            return View();
        }
    }
}