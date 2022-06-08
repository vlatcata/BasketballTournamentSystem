using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Player;
using BasketballTournamentSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly UserManager<ApplicationUser> userManager;

        public PlayerController(IPlayerService _playerService, UserManager<ApplicationUser> _userManager)
        {
            playerService = _playerService;
            userManager = _userManager;
        }

        [Authorize]
        public async Task<IActionResult> AddPlayer()
        {
            if (!User.IsInRole("Guest") && !User.IsInRole("Administrator"))
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

        public async Task<IActionResult> PlayerDetails(Guid Id)
        {
            var player = await playerService.GetPlayer(Id);

            return View(player);
        }
    }
}
