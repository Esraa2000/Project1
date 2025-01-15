using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using COC.ModelDB;
using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;


namespace COC.Controllers
{
    public class DiscoversController : Controller
    {
        private QUDBContext db = new QUDBContext();

        // GET: Discovers
        public ActionResult Index()
        {
            return View(db.Discovers.ToList());
        }

        // GET: Discovers/Details/5
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

        // GET: Discovers/Create
        public ActionResult Create()
        {
            DiscoverVM Disobj= new DiscoverVM();
            return View(Disobj);
        }

        // POST: Discovers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Discovers/Edit/5
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

        // POST: Discovers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Discovers/Delete/5
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

        // POST: Discovers/Delete/5
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
