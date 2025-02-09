using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace COC.Controllers
{
    public class PhotosVideosController : Controller
    {
        private readonly QUDBContext db;
 public PhotosVideosController(QUDBContext db)
 {
     db = db;
 }

        
        public async Task<IActionResult> Index()
        {
            return View(await db.PhotosVideos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            PhotosVideo photosVideo = await db.PhotosVideos.FindAsync(id);
            if (photosVideo == null)
            {
                return NotFound();
            }
            return View(photosVideo);
        }

        public IActionResult Create()
        {
            PhotosVideoVM obj = new PhotosVideoVM();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(PhotosVideoVM VM, IFormFile fileupload)
        {
            PhotosVideo photomodel = new PhotosVideo();
            photomodel.Title = VM.Title;
            photomodel.Id = VM.ID;
            photomodel.VideoUrl = VM.VideoURL;
            if (fileupload != null)
            {
                var profileimage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EventImage");
                if (!Directory.Exists(profileimage))
                {
                    Directory.CreateDirectory(profileimage);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileupload.FileName;
                var filePath = Path.Combine(profileimage, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileupload.CopyToAsync(fileStream);
                }
                photomodel.ImageUrl = "/EventImage/" + uniqueFileName;

                photomodel.ImageUrl = profileimage;
            }


            db.PhotosVideos.Add(photomodel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");



            return View(VM);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            PhotosVideo photosVideo = await db.PhotosVideos.FindAsync(id);
            if (photosVideo == null)
            {
                return NotFound();
            }
            return View(photosVideo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( PhotosVideo photosVideo)
        {
            if (ModelState.IsValid)
            {
                db.PhotosVideos.Update(photosVideo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(photosVideo);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            PhotosVideo photosVideo = await db.PhotosVideos.FindAsync(id);
            if (photosVideo == null)
            {
                return NotFound();
            }
            return View(photosVideo);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PhotosVideo photosVideo = await db.PhotosVideos.FindAsync(id);
            db.PhotosVideos.Remove(photosVideo);
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
