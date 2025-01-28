using COC.ModelDB.QUDB;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace COC.Controllers
{
    public class MainMenuController : Controller
    {
        private QUDBContext db = new QUDBContext();

        public ActionResult Index()
        {
            return View(db.MainMenus.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            MainMenu main_Menu = db.MainMenus.Find(id);
            if (main_Menu == null)
            {
                return NotFound();
            }
            return View(main_Menu);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                db.MainMenus.Add(mainMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mainMenu);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            MainMenu main_Menu = db.MainMenus.Find(id);
            if (main_Menu == null)
            {
                return NotFound();
            }
            return View(main_Menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                db.MainMenus.Update(mainMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mainMenu);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            MainMenu main_Menu = db.MainMenus.Find(id);
            if (main_Menu == null)
            {
                return NotFound();
            }
            return View(main_Menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainMenu main_Menu = db.MainMenus.Find(id);
            db.MainMenus.Remove(main_Menu);
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
