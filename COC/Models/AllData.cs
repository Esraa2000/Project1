using COC.ModelDB.QUDB;
using System.ComponentModel.DataAnnotations;

namespace COC.Models
{
    public class AllData
    {
        public List<News> Newsobj = new List<News>();
        public List<Event> Eventobj = new List<Event>();
        public List<PhotosVideo> photosobj = new List<PhotosVideo>();
        public List<MainMenu> MainMenuObj = new List<MainMenu>();
        public List<SubMenu> SubMenuObj = new List<SubMenu>();
        public Social SocialObj = new Social();
        public List<Discover> DiscoverObj = new List<Discover>();
        public ExternalLoginListViewModel externalLoginListViewModel = new ExternalLoginListViewModel();
        public string search { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
