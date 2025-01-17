using COC.ModelDB;
using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;

namespace COC.Controllers
{
    public class AdminController : Controller
    {
        private QUDBContext db = new QUDBContext();

        
        private identityDbContext _identityDb = new identityDbContext();

        public ActionResult GetUser()
        {
            
            RegisterViewModel obj = new RegisterViewModel();
            if (User.Identity.IsAuthenticated)
            {
                obj.Email = "Welcome " + User.Identity.Name;
            }
            else
            {
                obj.Email = "Welcome for guest user.";
            }

           

            return PartialView(obj);
        }

        public ActionResult GetUserDash()
        {
            var usersWithRoles = (from user in _identityDb.AspNetUsers
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in _identityDb.AspNetRoles on userRole.Id
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_in_Role_ViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            return PartialView(usersWithRoles);
        }

        public ActionResult Index()
        {
            CountData obj = new CountData();
            obj.EventCount = db.Events.Count();
            obj.NewsCount = db.News.Count();
            obj.PhotoCount = db.PhotosVideos.Count();
            obj.UserCount = (from user in _identityDb.AspNetUsers
                             select new
                             {
                                 UserId = user,

                                 RoleNames = (from userRole in user.Roles
                                              join role in _identityDb.AspNetRoles on userRole.Id
                                              equals role.Id
                                              select role.Name).ToList()
                             }).Count();

            return View(obj);
        }

        public ActionResult SearchAdmin()
        {
            UsersinRoleViewModel model = new UsersinRoleViewModel();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SearchAdminData(UsersinRoleViewModel model)
        {
            var usersWithRoles = (from user in _identityDb.AspNetUsers
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in _identityDb.AspNetRoles on userRole.Id
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).Where(u => u.Email.Contains(model.Email)).ToList().Select(p => new UsersinRoleViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            return View(usersWithRoles);
        }
    }
}
