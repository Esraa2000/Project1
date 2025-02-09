using COC.ModelDB.QUDB;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace COC.Controllers
{
    public class MainMenuController : Controller
    {
        private readonly QUDBContext db;


public MainMenuController(QUDBContext Db)
{
    db = Db;
}

        public async Task<IActionResult> Index()
        {
            return View(await db.MainMenus.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            MainMenu main_Menu = await db.MainMenus.FindAsync(id);
            if (main_Menu == null)
            {
                return NotFound();
            }
            return View(main_Menu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                await db.MainMenus.AddAsync(mainMenu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mainMenu);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            MainMenu main_Menu = await db.MainMenus.FindAsync(id);
            if (main_Menu == null)
            {
                return NotFound();
            }
            return View(main_Menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                db.MainMenus.Update(mainMenu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mainMenu);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            MainMenu main_Menu =await db.MainMenus.FindAsync(id);
            if (main_Menu == null)
            {
                return NotFound();
            }
            return View(main_Menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            MainMenu main_Menu = await db.MainMenus.FindAsync(id);
            db.MainMenus.Remove(main_Menu);
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
