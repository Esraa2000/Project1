using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace COC.Controllers
{
    public class SubMenuController : Controller
    {
        private QUDBContext db = new QUDBContext();

       
        public async Task<IActionResult> Index()
        {
            return View(await db.SubMenus.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            SubMenu sub_Menu = await db.SubMenus.FindAsync(id);
            if (sub_Menu == null)
            {
                return NotFound();
            }
            return View(sub_Menu);
        }

        public async Task<IActionResult> Create()
        {
            SubMenuVM vm = new SubMenuVM();
            var mainmnlst = await db.MainMenus.ToListAsync();
            vm.MainMenuList = new SelectList(mainmnlst, "ID", "Name");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubMenuVM vm)
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
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            SubMenu sub_Menu = await db.SubMenus.FindAsync(id);
            if (sub_Menu == null)
            {
                return NotFound();
            }
            return View(sub_Menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( SubMenu sub_Menu)
        {
            if (ModelState.IsValid)
            {
                db.SubMenus.Update(sub_Menu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sub_Menu);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            SubMenu sub_Menu = await db.SubMenus.FindAsync(id);
            if (sub_Menu == null)
            {
                return NotFound();
            }
            return View(sub_Menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SubMenu sub_Menu = await db.SubMenus.FindAsync(id);
            db.SubMenus.Remove(sub_Menu);
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
