using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Player;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly UserManager<IdentityUser> userManager;

        public PlayerController(IPlayerService _playerService, UserManager<IdentityUser> _userManager)
        {
            playerService = _playerService;
            userManager = _userManager;
        }

        [Authorize]
        public async Task<IActionResult> AddPlayer()
        {
            if (!User.IsInRole("Guest"))
            {
                var user = await userManager.GetUserAsync(User);
                return View("RequestRole", user);
            }
            return View();
        }

        [HttpPost]
        [Authorize]
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

            return View("AllPlayers", players);
        }
    }
}
