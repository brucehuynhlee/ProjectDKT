using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDKT.Models;
using ProjectDKT.Areas.Admin.Helpers;
using System.Drawing;
using System.IO;

namespace ProjectDKT.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private static string UPLOAD = "~/uploads/";
        private CauVanChuyenDatabaseEntities db = new CauVanChuyenDatabaseEntities();

        // GET: Admin/News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Typenew);
            return View(news.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.TypeNewsID = new SelectList(db.Typenews, "TypeNewsID", "NameType");
            return View();
        }

        //Check if image valid
        public void checkImage(HttpPostedFileBase Image)
        {
            if (Image == null || Image.ContentLength == 0)
            {
                ModelState.AddModelError("UrlImage", "Please choose a file");

            }
            try
            {
                var bitmap = Bitmap.FromStream(Image.InputStream);
                // valid image stream
            }
            catch
            {
                ModelState.AddModelError("UrlImage", "Choose the right format image");
            }
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsID,Title,Summary,UrlImage,UrlLink,DateUpload,IsActive,TypeNewsID")] News news,HttpPostedFileBase upload)
        {
            checkImage(upload);
            news.DateUpload = DateTime.Now;
            if (ModelState.IsValid)
            {
                var imagePath = ImageHelpers.GetPathToSaveImage(Server.MapPath(UPLOAD), upload.FileName);
                upload.SaveAs(imagePath);
                news.UrlImage = imagePath;
                news.DateUpload = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeNewsID = new SelectList(db.Typenews, "TypeNewsID", "NameType", news.TypeNewsID);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeNewsID = new SelectList(db.Typenews, "TypeNewsID", "NameType", news.TypeNewsID);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,Title,Summary,UrlImage,UrlLink,DateUpload,IsActive,TypeNewsID")] News news,HttpPostedFileBase upload)
        {
            News ban = db.News.Find(news.NewsID);
            var imagePath = "";
            if (upload != null)
            {
                checkImage(upload);
                try
                {
                    imagePath = ImageHelpers.GetPathToSaveImage(Server.MapPath(UPLOAD), upload.FileName);
                    ImageHelpers.DeleteImage(Request.MapPath("~/uploads/" + Path.GetFileName(news.UrlImage)));
                    upload.SaveAs(imagePath);
                    news.UrlImage = imagePath;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("UrlImage", "Error save and delete file");

                }

            }
            else
            {
                news.UrlImage = ban.UrlImage;
            }
            //checkImage(Image);
            if (ModelState.IsValid)
            {
                db.Entry(ban).State = EntityState.Detached;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeNewsID = new SelectList(db.Typenews, "TypeNewsID", "NameType", news.TypeNewsID);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
