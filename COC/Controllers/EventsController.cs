using COC.ModelDB.QUDB;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace COC.Controllers
{
    public class EventsController : Controller
    {
        private readonly QUDBContext db;


 public EventsController(QUDBContext Db)
 {
     db = Db;
 }


       
        public async Task<IActionResult> Index()
        {
            return View(await db.Events.FindAsync());
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

     
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(@event);
        }
         
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
                Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Update(@event);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            db.Events.Remove(@event);
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
