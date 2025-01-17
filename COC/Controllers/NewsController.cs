using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace COC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
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
            NewsVM VM = new NewsVM();
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsVM vm, IFormFile FileUpload)
        {
            News newsmodel = new News();
            newsmodel.Title = vm.Title;
            newsmodel.Content = vm.Content;
            newsmodel.Date = (DateTime)vm.Date;
            newsmodel.Highlight = vm.Highlight;

            if (FileUpload != null)
            {
                var profileimage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImagesUpload");
                if (!Directory.Exists(profileimage))
                {
                    Directory.CreateDirectory(profileimage);
                }                                                                                      
                var filePath = Path.Combine(profileimage, FileUpload.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    FileUpload.CopyTo(stream);
                }

                
                newsmodel.ImageUrl = "/ImagesUpload/" + FileUpload.FileName;
            }

            db.News.Add(newsmodel);
            db.SaveChanges();
            return RedirectToAction("Index");

           

            return View(vm);
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
                db.News.Update(news);
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
