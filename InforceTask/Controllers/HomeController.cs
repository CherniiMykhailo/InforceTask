using InforceTask.Models;
using InforceTask.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InforceTask.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IUrlRepository repository;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(IUrlRepository repository, SignInManager<IdentityUser> signInManager)
        {
            this.repository = repository;
            this.signInManager = signInManager;
        }

        [Route("Index")]
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
