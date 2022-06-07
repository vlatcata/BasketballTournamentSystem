using BasketballTournamentSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService _playerService)
        {
            playerService = _playerService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
