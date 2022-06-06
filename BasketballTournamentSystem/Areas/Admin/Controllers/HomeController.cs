using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
