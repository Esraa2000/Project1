using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace COC.Controllers
{
    public class DiscoversController : Controller
    {
        private QUDBContext db = new QUDBContext();

        
        public ActionResult Index()
        {
            return View(db.Discovers.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Discover discover = db.Discovers.Find(id);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

       
        public ActionResult Create()
        {
            DiscoverVM Disobj= new DiscoverVM();
            return View(Disobj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiscoverVM vm, IFormFile fileupload)
        {
            Discover discoverModele = new Discover();
            discoverModele.Id = vm.ID;
            discoverModele.ImageUrl = vm.ImageURL;
            discoverModele.Title = vm.Title;
            discoverModele.Content = vm.Content;
            if (fileupload != null)
            {
                var profileimage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageUploadDis");//System.Web.HttpContext.Current.Server.MapPath("~/ImageUploadDis/"), fileupload.FileName);
                if (!Directory.Exists(profileimage))
                {
                    Directory.CreateDirectory(profileimage);
                }
                var filePath = Path.Combine(profileimage, fileupload.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    fileupload.CopyTo(stream);
                }
                var profileImage = "/ImageUploadDis/" + fileupload.FileName;
                discoverModele.ImageUrl = profileImage;
            }
            
                db.Discovers.Add(discoverModele);
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
            Discover discover = db.Discovers.Find(id);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Discover discover)
        {
            if (ModelState.IsValid)
            {
                db.Discovers.Update(discover);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discover);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Discover discover = db.Discovers.Find(id);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discover discover = db.Discovers.Find(id);
            db.Discovers.Remove(discover);
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
