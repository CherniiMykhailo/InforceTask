using InforceTask.Models;
using InforceTask.Models.Repository;
using InforceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceTask.Controllers
{
    [Authorize]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUrlRepository repository;

        public UserController(IUrlRepository repository)
        {
            this.repository = repository;
        }

        [Route("Create")]
        public ViewResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(string originalUrl)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                ModelState.AddModelError("originalUrl", "The URL cannot be empty.");
                return View();
            }

            var existingUrl = repository.Urls.FirstOrDefault(u => u.OriginalUrl == originalUrl);
            if (existingUrl != null)
            {
                ModelState.AddModelError("originalUrl", "This URL already exists in the database.");
                return View();
            }

            var currentUser = User.Identity?.Name ?? "Unknown User";

            var shortenerService = new UrlShortenerService(repository.GetDbContext());
            string uniqueShortUrl = await shortenerService.GenerateUniqueCode();

            var newUrl = new Url
            {
                OriginalUrl = originalUrl,
                ShortUrl = GenerateShortUrl(),
                CreatedBy = currentUser,
                CreatedDate = DateTime.UtcNow
            };

            repository.CreateUrl(newUrl);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int id)
        {
            var url = repository.Urls.FirstOrDefault(u => u.Id == id);

            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var url = repository.Urls.FirstOrDefault(u => u.Id == id);

            if (url == null)
            {
                return NotFound();
            }

            repository.DeleteUrl(url);

            return RedirectToAction("Index", "Home");
        }

        private string GenerateShortUrl()
        {
            return Guid.NewGuid().ToString().Substring(0, 6);
        }
    }
}
