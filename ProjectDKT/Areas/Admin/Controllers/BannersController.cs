using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDKT.Models;
using System.IO;
using System.Drawing;
using ProjectDKT.Areas.Admin.Helpers;

namespace ProjectDKT.Areas.Admin.Controllers
{
    public class BannersController : Controller
    {
        private static string UPLOAD = "~/uploads/";
        private CauVanChuyenDatabaseEntities db = new CauVanChuyenDatabaseEntities();

        // GET: Banners
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }

        // GET: Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        //Check if image valid
        public bool checkImage(HttpPostedFileBase Image)
        {
            if (Image == null || Image.ContentLength == 0)
            {
                ModelState.AddModelError("UrlImage", "Please choose a file");
                return false;
            }
            try
            {
                var bitmap = Bitmap.FromStream(Image.InputStream);
                // valid image stream
            }
            catch
            {
                ModelState.AddModelError("UrlImage", "Choose the right format image");
                return false;
            }

            return true;

        }




        // POST: Banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsActive")] Banner banner, HttpPostedFileBase Image)
        {
            var imagePath = "";
            if (checkImage(Image))
            {
                imagePath = ImageHelpers.GetPathToSaveImage(Server.MapPath(UPLOAD), Image.FileName);
                if (System.IO.File.Exists(imagePath))
                {
                    //ModelState.AddModelError("UrlImage", "File exist");
                }else
                {
                    Image.SaveAs(imagePath);
                }
            }
            
            if (ModelState.IsValid)
            {
                banner.DateUpload = DateTime.Now;
                banner.UrlImage = imagePath;
                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BannerID,DateUpload,IsActive")] Banner banner, HttpPostedFileBase Image = null)
        {
            Banner ban = db.Banners.Find(banner.BannerID);
            var imagePath = "";
            if (checkImage(Image))
            {  
                try
                {
                    imagePath = ImageHelpers.GetPathToSaveImage(Server.MapPath(UPLOAD), Image.FileName);
                    ImageHelpers.DeleteImage(Request.MapPath("~/uploads/" + Path.GetFileName(banner.UrlImage)));
                    Image.SaveAs(imagePath);
                    banner.UrlImage = imagePath;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("UrlImage", "Error save and delete file");

                }

            }
            else
            {
                banner.UrlImage = ban.UrlImage;
            }
            if (ModelState.IsValid)
            {

                db.Entry(ban).State = EntityState.Detached;
                db.Entry(banner).State = EntityState.Modified;
                //db.Banners.Attach(ban);
                //ban.UrlImage = banner.UrlImage;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Find(id);
            ImageHelpers.DeleteImage(Request.MapPath("~/uploads/" + Path.GetFileName(banner.UrlImage)));
            db.Banners.Remove(banner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
