
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using COC.ModelDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using COC.ModelDB.QUDB;
namespace COC.Controllers
{
    public class NewstestController : Controller
    {
        private QUDBContext db = new QUDBContext();

        
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        public ActionResult Create()
        {
            newstest vm = new newstest(); 
            return View(vm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(newstest  vm, IFormFile FileUpload)
        {
            News newsmodel = new News();
            newsmodel.Title = vm.Title;
            newsmodel.Content = vm.Content;
            newsmodel.Date = (DateTime)vm.Date;

            if (FileUpload != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagesUpload", FileUpload.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    FileUpload.CopyTo(stream);
                }
                newsmodel.ImageUrl = "/ImagesUpload/" + FileUpload.FileName;
            }



            db.News.Add(newsmodel);
            db.SaveChanges();
            return RedirectToAction("Index");




            return View(newsmodel);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

   
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
