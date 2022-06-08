using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult CustomErrorPage()
        {
            return View();
        }

        public IActionResult CustomError500()
        {
            return View();
        }

        public IActionResult RoleHasBeenRequested()
        {
            return View();
        }

        public IActionResult RoleAlreadyRequested()
        {
            return View();
        }
    }
}
