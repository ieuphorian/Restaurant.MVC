using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Data;

namespace Restaurant.WebApi.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var categories = db.Categories.Include(c => c.ParentCategory);
            return View(categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories categories, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image == null || image.ContentLength <= 0)
                {
                    ModelState.AddModelError("", "Resim Yükleyiniz");
                    ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "Name", categories.ParentCategoryId);
                    return View(categories);
                }
                categories.ImageUrl = DosyaYukle(image, Server.MapPath("~/Resimler"));
                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "Name", categories.ParentCategoryId);
            return View(categories);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "Name", categories.ParentCategoryId);
            return View(categories);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if ((image == null || image.ContentLength <= 0) && string.IsNullOrEmpty(categories.ImageUrl))
                {
                    ModelState.AddModelError("", "Resim Yükleyiniz");
                    ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "Name", categories.ParentCategoryId);
                    return View(categories);
                }
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategoryId = new SelectList(db.Categories, "Id", "Name", categories.ParentCategoryId);
            return View(categories);
        }

        // POST: Admin/Categories/Delete/5
        public bool Delete(int id)
        {
            try
            {
                Categories categories = db.Categories.Find(id);
                db.Categories.Remove(categories);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string DosyaYukle(HttpPostedFileBase yuklenecekDosya, string path)
        {
            if (yuklenecekDosya != null)
            {
                string dosyaYolu = Path.GetFileName(yuklenecekDosya.FileName);
                var yuklemeYeri = Path.Combine(path, dosyaYolu);
                yuklenecekDosya.SaveAs(yuklemeYeri);
            }
            return "/Resimler/" + Path.GetFileName(yuklenecekDosya.FileName);
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
