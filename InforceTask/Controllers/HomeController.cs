using InforceTask.Models;
using InforceTask.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InforceTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlRepository repository;
        private SignInManager<IdentityUser> signInManager;

        public HomeController(IUrlRepository repository, SignInManager<IdentityUser> signInManager)
        {
            this.repository = repository;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                ViewData["IsAuthenticated"] = true;
                ViewData["Username"] = User.Identity.Name;
            }
            else
            {
                ViewData["IsAuthenticated"] = false;
            }

            return View(repository.Urls);
        }

        [HttpGet("{shortUrl}")]
        public IActionResult RedirectToOriginal(string shortUrl)
        {
            var url = repository.Urls.FirstOrDefault(u => u.ShortUrl == shortUrl);
            if (url == null)
            {
                return NotFound("Short URL not found.");
            }

            return Redirect(url.OriginalUrl);
        }
    }
}
