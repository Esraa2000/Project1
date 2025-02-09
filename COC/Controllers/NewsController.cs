using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace COC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private readonly QUDBContext db;
public NewsController(QUDBContext Db)
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
                return BadRequest();
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
            NewsVM VM = new NewsVM();
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsVM vm, IFormFile FileUpload)
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
                    await FileUpload.CopyToAsync(stream);
                }

                
                newsmodel.ImageUrl = "/ImagesUpload/" + FileUpload.FileName;
            }

            db.News.Add(newsmodel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

           

            return View(vm);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
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
                db.News.Update(news);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
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

        
    }
}
