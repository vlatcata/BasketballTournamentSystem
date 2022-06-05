using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult CustomErrorPage()
        {
            return View();
        }
    }
}
