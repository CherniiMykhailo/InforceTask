using InforceTask.Models;
using InforceTask.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InforceTask.Controllers
{
    [Route("About")]
    public class AboutController : Controller
    {
        private readonly IUrlRepository repository;

        public AboutController(IUrlRepository repository)
        {
            this.repository = repository;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var detail = repository.GetallDbContext().Abouts.FirstOrDefault(d => d.Id == 1);

            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var about = repository.GetallDbContext().Abouts.FirstOrDefault(a => a.Id == id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(About model)
        {
            if (ModelState.IsValid)
            {
                var about = repository.GetallDbContext().Abouts.FirstOrDefault(a => a.Id == model.Id);
                if (about == null)
                {
                    return NotFound();
                }

                about.Description = model.Description;
                repository.GetallDbContext().SaveChanges(); 

                return RedirectToAction("Index", "About");
            }
            return View(model);
        }
    }
}
