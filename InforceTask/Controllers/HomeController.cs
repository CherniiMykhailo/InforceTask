using InforceTask.Models;
using InforceTask.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InforceTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlRepository repository;

        public HomeController(IUrlRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View(repository.Urls);
        }
    }
}
