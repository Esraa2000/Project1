using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace COC.Controllers
{
    public class NewstestController : Controller
    {
        private readonly QUDBContext db;
 public NewstestController(QUDBContext Db)
 {
     db = Db;
 }
        
        public async Task<IActionResult> Index()
        {
            return View(await db.News.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        public IActionResult Create()
        {
            newstest vm = new newstest(); 
            return View(vm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(newstest  vm, IFormFile FileUpload)
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
                   await FileUpload.CopyToAsync(stream);
                }
                newsmodel.ImageUrl = "/ImagesUpload/" + FileUpload.FileName;
            }



            db.News.Add(newsmodel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");




            return View(newsmodel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
            await db.SaveChangesAsync();
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
