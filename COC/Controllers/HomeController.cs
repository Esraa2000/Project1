
using Microsoft.AspNetCore.Mvc;
using COC.Models;
using Microsoft.EntityFrameworkCore;
using COC.ModelDB;
using COC.ModelDB.QUDB;

namespace COC.Controllers
{
    public class HomeController : Controller

    {
       
        private readonly QUDBContext db;
        private readonly identityDbContext _identityDb;

        public HomeController(QUDBContext Db, identityDbContext identityDb)
        {
            db = Db;
            _identityDb = identityDb;
        }

        public async Task<IActionResult> Index()
        {
            
            AllData obj = new AllData();

            obj.Newsobj = await db.News.ToListAsync();
            obj.Eventobj = await db.Events.ToListAsync();
            obj.photosobj = await db.PhotosVideos.ToListAsync();
            obj.DiscoverObj = await db.Discovers.ToListAsync();
            obj.MainMenuObj = await db.MainMenus.ToListAsync();
            obj.SubMenuObj=await db.SubMenus.ToListAsync();
            return View(obj);
        }

        public async Task<IActionResult> GetMenu()
        {
            
            AllData obj = new AllData();

            obj.MainMenuObj = await db.MainMenus.Include("SubMenu").ToListAsync();
            obj.SocialObj = await db.Socials.FirstOrDefaultAsync();

            return PartialView(obj);
        }

        public async Task<IActionResult> GetFooter()
        {
            AllData obj = new AllData();

            
            obj.MainMenuObj = await db.MainMenus.Include("SubMenu").ToListAsync();
            return PartialView(obj);
        }

        public async Task<IActionResult> NewsDetails(int newsid)
        {
            var newsdetails = await db.News.FindAsync(newsid);


            return View(newsdetails);
        }

        public async Task<IActionResult> DiscoverDetails(int Discid)
        {
            var discdetails = await db.Discovers.FindAsync(Discid);
            return View(discdetails);
        }

        public async Task<IActionResult> SubMenuDetails(int SubMid)
        {
            var submenudetails = await db.SubMenus.FindAsync(SubMid);

            return View(submenudetails);
        }

        public async Task<IActionResult> EventDetails(int Eventid)
        {
            var eventdetails = await db.Events.FindAsync(Eventid);
            return View(eventdetails);
        }

        [HttpPost]
        public async Task<IActionResult> SearchData(AllData alldata)
        {
            var data = await db.News.Where(news => news.Title.Contains(alldata.search)).ToListAsync();

            return View(data);
        }

        [HttpPost]
        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
