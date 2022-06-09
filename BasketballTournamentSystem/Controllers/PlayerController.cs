using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Player;
using BasketballTournamentSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using BasketballTournamentSystem.Infrastructure.Data;

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

            return RedirectToAction(nameof(GetAllPlayers));
        }

        public async Task<IActionResult> GetAllPlayers(string searchString, int pageNumber = 1, int pageSize = 3)
        {
            var excludeRecords = (pageSize * pageNumber) - pageSize;

            var players = await playerService.GetAllPlayers();

            if (!String.IsNullOrEmpty(searchString))
            {
                players = players.Where(p => p.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            var playersPerPage = players.Skip(excludeRecords).Take(pageSize).ToList();

            var result = new PagedResult<PlayerViewModel>
            {
                Data = playersPerPage,
                TotalItems = players.Count,
                PageNumber = pageNumber,
                PageSize = pageSize

            };

            return View("AllPlayers", result);
        }

        public async Task<IActionResult> PlayerDetails(Guid Id)
        {
            var player = await playerService.GetPlayer(Id);

            return View(player);
        }

        [Authorize]
        public async Task<IActionResult> RemovePlayer(Guid Id)
        {
            var result = await playerService.RemovePlayer(Id);

            return RedirectToAction(nameof(GetAllPlayers));
        }
    }
}
