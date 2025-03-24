using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceTask.Controllers
{
    [Authorize]
    [Route("User")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
