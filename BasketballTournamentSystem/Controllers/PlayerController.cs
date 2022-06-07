using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Player;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult AddPlayer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(PlayerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await playerService.CreatePlayer(model);

            return View();
        }

        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await playerService.GetAllPlayers();

            return View(players);
        }
    }
}
