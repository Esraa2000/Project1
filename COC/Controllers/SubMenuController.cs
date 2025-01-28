using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;


namespace COC.Controllers
{
    public class SubMenuController : Controller
    {
        private QUDBContext db = new QUDBContext();

       
        public ActionResult Index()
        {
            return View(db.SubMenus.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            SubMenu sub_Menu = db.SubMenus.Find(id);
            if (sub_Menu == null)
            {
                return NotFound();
            }
            return View(sub_Menu);
        }

        public ActionResult Create()
        {
            SubMenuVM vm = new SubMenuVM();
            var mainmnlst = db.MainMenus.ToList();
            vm.MainMenuList = new SelectList(mainmnlst, "ID", "Name");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubMenuVM vm)
        {

            SubMenu subobj = new SubMenu();
            subobj.Name = vm.Name;
            subobj.MainMenuId = vm.MainMenuID;
            subobj.Order = vm.Order;
            subobj.Title = vm.Title;
            subobj.Url = vm.URL;
            subobj.Content = vm.Content;
            
            

            if (ModelState.IsValid)
            {
                db.SubMenus.Add(subobj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            SubMenu sub_Menu = db.SubMenus.Find(id);
            if (sub_Menu == null)
            {
                return NotFound();
            }
            return View(sub_Menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SubMenu sub_Menu)
        {
            if (ModelState.IsValid)
            {
                db.SubMenus.Update(sub_Menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sub_Menu);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            SubMenu sub_Menu = db.SubMenus.Find(id);
            if (sub_Menu == null)
            {
                return NotFound();
            }
            return View(sub_Menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubMenu sub_Menu = db.SubMenus.Find(id);
            db.SubMenus.Remove(sub_Menu);
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
