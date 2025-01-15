
using Microsoft.AspNetCore.Mvc;
using COC.Models;
using Microsoft.EntityFrameworkCore;
using COC.ModelDB;
using COC.ModelDB.QUDB;

namespace COC.Controllers
{
    public class HomeController : Controller

    {
        private QUDBContext db = new QUDBContext();
        private identityDbContext _identityDb = new identityDbContext();

        public ActionResult Index()
        {
            
            AllData obj = new AllData();

            obj.Newsobj = db.News.ToList();
            obj.Eventobj = db.Events.ToList();
            obj.photosobj = db.PhotosVideos.ToList();
            obj.DiscoverObj = db.Discovers.ToList();
            obj.MainMenuObj = db.MainMenus.ToList();
            obj.SubMenuObj=db.SubMenus.ToList();
            return View(obj);
        }

        public ActionResult GetMenu()
        {
            
            AllData obj = new AllData();

            obj.MainMenuObj = db.MainMenus.Include("SubMenu").ToList();
            obj.SocialObj = db.Socials.FirstOrDefault();

            return PartialView(obj);
        }

        public ActionResult GetFooter()
        {
            AllData obj = new AllData();

            
            obj.MainMenuObj = db.MainMenus.Include("SubMenu").ToList();
            return PartialView(obj);
        }

        public ActionResult NewsDetails(int newsid)
        {
            var newsdetails = db.News.Find(newsid);


            return View(newsdetails);
        }

        public ActionResult DiscoverDetails(int Discid)
        {
            var discdetails = db.Discovers.Find(Discid);
            return View(discdetails);
        }

        public ActionResult SubMenuDetails(int SubMid)
        {
            var submenudetails = db.SubMenus.Find(SubMid);

            return View(submenudetails);
        }

        public ActionResult EventDetails(int Eventid)
        {
            var eventdetails = db.Events.Find(Eventid);
            return View(eventdetails);
        }

        [HttpPost]
        public ActionResult SearchData(AllData alldata)
        {
            var data = db.News.Where(news => news.Title.Contains(alldata.search)).ToList();

            return View(data);
        }

        [HttpPost]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}