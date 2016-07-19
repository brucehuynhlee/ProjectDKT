using ProjectDKT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDKT.Areas.Admin.Controllers
{
    public class ImagesController : Controller
    {
        private CauVanChuyenDatabaseEntities db = new CauVanChuyenDatabaseEntities();
        // GET: Admin/Images
        public ActionResult Index()
        {
            return View();
        }

        //post method
        public ActionResult Create()
        {
            ViewBag.CatalogID = new SelectList(db.Catalogs.ToList(), "CatalogID", "CatalogName");
            return View();
        }
    }
}