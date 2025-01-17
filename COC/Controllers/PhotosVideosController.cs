using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace COC.Controllers
{
    public class PhotosVideosController : Controller
    {
        private QUDBContext db = new QUDBContext();

        
        public ActionResult Index()
        {
            return View(db.PhotosVideos.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            PhotosVideo photosVideo = db.PhotosVideos.Find(id);
            if (photosVideo == null)
            {
                return NotFound();
            }
            return View(photosVideo);
        }

        public ActionResult Create()
        {
            PhotosVideoVM obj = new PhotosVideoVM();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(PhotosVideoVM VM, IFormFile fileupload)
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
                    fileupload.CopyTo(fileStream);
                }
                photomodel.ImageUrl = "/EventImage/" + uniqueFileName;

                photomodel.ImageUrl = profileimage;
            }


            db.PhotosVideos.Add(photomodel);
            db.SaveChanges();
            return RedirectToAction("Index");



            return View(VM);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            PhotosVideo photosVideo = db.PhotosVideos.Find(id);
            if (photosVideo == null)
            {
                return NotFound();
            }
            return View(photosVideo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( PhotosVideo photosVideo)
        {
            if (ModelState.IsValid)
            {
                db.PhotosVideos.Update(photosVideo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photosVideo);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            PhotosVideo photosVideo = db.PhotosVideos.Find(id);
            if (photosVideo == null)
            {
                return NotFound();
            }
            return View(photosVideo);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhotosVideo photosVideo = db.PhotosVideos.Find(id);
            db.PhotosVideos.Remove(photosVideo);
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
