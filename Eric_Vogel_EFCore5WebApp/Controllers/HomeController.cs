using Microsoft.AspNetCore.Mvc;

namespace Eric_Vogel_EFCore5WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
