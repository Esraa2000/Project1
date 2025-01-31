using COC.ModelDB;
using COC.ModelDB.QUDB;
using COC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace COC.Controllers
{
    public class AdminController : Controller
    {
        private QUDBContext db = new QUDBContext();

        
        private identityDbContext _identityDb = new identityDbContext();

        public async Task<IActionResult> GetUser()
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

        public async Task<IActionResult> GetUserDash()
        {
            var usersWithRoles = (await _identityDb.AspNetUsers
        .Select(user => new
        {
            UserId = user.Id,
            Username = user.UserName,
            Email = user.Email,
            RoleNames = (from userRole in user.Roles
                         join role in _identityDb.AspNetRoles on userRole.Id equals role.Id
                         select role.Name).ToList()
        })
        .ToListAsync())
        .Select(p => new UsersinRoleViewModel()
        {
            UserId = p.UserId,
            Username = p.Username,
            Email = p.Email,
            Role = string.Join(",", p.RoleNames)
        });

            return PartialView(usersWithRoles);
        }

        public async Task<IActionResult> Index()
        {
            CountData obj = new CountData();
            obj.EventCount = await db.Events.CountAsync();
            obj.NewsCount =await db.News.CountAsync();
            obj.PhotoCount =await db.PhotosVideos.CountAsync();
            obj.UserCount = await (from user in _identityDb.AspNetUsers
                                   select new
                                   {
                                       UserId = user,

                                       RoleNames = (from userRole in user.Roles
                                                    join role in _identityDb.AspNetRoles on userRole.Id
                                                    equals role.Id
                                                    select role.Name).ToList()
                                   }).CountAsync();

            return View(obj);
        }

        public IActionResult SearchAdmin()
        {
            UsersinRoleViewModel model = new UsersinRoleViewModel();

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchAdminData(UsersinRoleViewModel model)
        {
            var usersWithRoles = (await _identityDb.AspNetUsers
                .Select(user => new
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    RoleNames = (from userRole in user.Roles
                                 join role in _identityDb.AspNetRoles on userRole.Id equals role.Id
                                 select role.Name).ToList()
                })
                .ToListAsync())
                .Where(u => u.Email.Contains(model.Email))
                .Select(p => new UsersinRoleViewModel
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
