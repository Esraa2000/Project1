using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using COC.Repositories;


namespace COC.Controllers
{
    public class DiscoversController : Controller
    {
      private readonly IDiscoverRepository db;
       

public DiscoversController(IDiscoverRepository Db)
{
    db = Db;
    
}

        
        public async Task<IActionResult> Index()
        {
            var discovers = await db.GetAll();
            return View(discovers);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Discover discover =await db.GetById(id.Value);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

       
        public IActionResult Create()
        {
            DiscoverVM Disobj= new DiscoverVM();
            return View(Disobj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscoverVM vm, IFormFile fileupload)
        {
            Discover discoverModele = new Discover();
            discoverModele.Id = vm.ID;
            discoverModele.ImageUrl = vm.ImageURL;
            discoverModele.Title = vm.Title;
            discoverModele.Content = vm.Content;
            if (fileupload != null)
            {
                var profileimage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageUploadDis");
                if (!Directory.Exists(profileimage))
                {
                    Directory.CreateDirectory(profileimage);
                }
                var filePath = Path.Combine(profileimage, fileupload.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                   await fileupload.CopyToAsync(stream);
                }
                var profileImage = "/ImageUploadDis/" + fileupload.FileName;
                discoverModele.ImageUrl = profileImage;
            }
            
                db.Add(discoverModele);
                
                return RedirectToAction("Index");
            

            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Discover discover =await db.GetById(id.Value);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Discover discover)
        {
            if (ModelState.IsValid)
            {
                db.Update(discover);
                
                return RedirectToAction("Index");
            }
            return View(discover);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Discover discover = await db.GetById(id.Value);
            if (discover == null)
            {
                return NotFound();
            }
            return View(discover);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            await db.Delete(id);
            return RedirectToAction("Index");
        }

       
    }
}
