using COC.ModelDB.QUDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Policy;


namespace COC.Controllers
{
    public class SocialsController : Controller
    {
        private readonly QUDBContext db;
 public SocialsController(QUDBContext Db)
 {
     db = Db;
 }
        public async Task<IActionResult> Index()
        {
            return View(await db.Socials.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Social social = await db.Socials.FindAsync(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }
       


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Social social)
        {
            if (ModelState.IsValid)
            {
                db.Socials.Add(social);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(social);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Social social = await db.Socials.FindAsync(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Social social)
        {
            if (ModelState.IsValid)
            {
                db.Socials.Update(social);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(social);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Social social =await db.Socials.FindAsync(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Social social = await db.Socials.FindAsync(id);
            db.Socials.Remove(social);
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
