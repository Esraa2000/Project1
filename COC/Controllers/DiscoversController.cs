using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;


namespace COC.Controllers
{
    public class DiscoversController : Controller
    {
      private readonly QUDBContext db;
       

public DiscoversController(QUDBContext Db)
{
    db = Db;
    
}

        
        public async Task<IActionResult> Index()
        {
            var discovers = await db.Discovers.ToListAsync();
            return View(discovers);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Discover discover =await db.Discovers.FindAsync(id);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

       
        public IActionResult Create()
        {
            DiscoverVM Disobj= new DiscoverVM();
            return View(Disobj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscoverVM vm, IFormFile fileupload)
        {
            Discover discoverModele = new Discover();
            discoverModele.Id = vm.ID;
            discoverModele.ImageUrl = vm.ImageURL;
            discoverModele.Title = vm.Title;
            discoverModele.Content = vm.Content;
            if (fileupload != null)
            {
                var profileimage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageUploadDis");
                if (!Directory.Exists(profileimage))
                {
                    Directory.CreateDirectory(profileimage);
                }
                var filePath = Path.Combine(profileimage, fileupload.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                   await fileupload.CopyToAsync(stream);
                }
                var profileImage = "/ImageUploadDis/" + fileupload.FileName;
                discoverModele.ImageUrl = profileImage;
            }
            
                db.Discovers.Add(discoverModele);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            

            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Discover discover =await db.Discovers.FindAsync(id);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Discover discover)
        {
            if (ModelState.IsValid)
            {
                db.Discovers.Update(discover);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(discover);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Discover discover = await db.Discovers.FindAsync(id);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Discover discover = await db.Discovers.FindAsync(id);
            db.Discovers.Remove(discover);
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
