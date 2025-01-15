using System;
using System.Collections.Generic;
using System.Data;
using COC.Models;
using System.Linq;
using System.Net;
using System.Web;
using COC.ModelDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using COC.ModelDB.QUDB;


namespace COC.Controllers
{
    public class SocialsController : Controller
    {
        private QUDBContext db = new QUDBContext();

       
        public ActionResult Index()
        {
            return View(db.Socials.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

     
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Social social)
        {
            if (ModelState.IsValid)
            {
                db.Socials.Add(social);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(social);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Social social)
        {
            if (ModelState.IsValid)
            {
                db.Socials.Update(social);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(social);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Social social = db.Socials.Find(id);
            if (social == null)
            {
                return NotFound();
            }
            return View(social);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Social social = db.Socials.Find(id);
            db.Socials.Remove(social);
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
