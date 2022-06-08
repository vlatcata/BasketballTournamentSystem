using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
