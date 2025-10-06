using Microsoft.AspNetCore.Mvc;

namespace first_mvc.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
