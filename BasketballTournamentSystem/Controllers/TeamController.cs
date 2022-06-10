using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Team;
using BasketballTournamentSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;

namespace BasketballTournamentSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;
        private readonly UserManager<ApplicationUser> userManager;

        public TeamController(ITeamService _teamService, UserManager<ApplicationUser> _userManager, IPlayerService _playerService)
        {
            teamService = _teamService;
            userManager = _userManager;
            playerService = _playerService;
        }

        [Authorize]
        public async Task<IActionResult> AddTeam()
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
        public async Task<IActionResult> AddTeam(TeamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await teamService.CreateTeam(model);

            return RedirectToAction(nameof(GetAllTeams));
        }

        public async Task<IActionResult> GetAllTeams(string searchString, int pageNumber = 1, int pageSize = 3)
        {
            var excludeRecords = (pageSize * pageNumber) - pageSize;

            var teams = await teamService.GetAllTeams();

            if (!String.IsNullOrEmpty(searchString))
            {
                teams = teams.Where(t => t.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            var teamsPerPage = teams.Skip(excludeRecords).Take(pageSize).ToList();

            var result = new PagedResult<TeamViewModel>
            {
                Data = teamsPerPage,
                TotalItems = teams.Count,
                PageNumber = pageNumber,
                PageSize = pageSize

            };

            return View("AllTeams", result);
        }

        public async Task<IActionResult> TeamDetails(Guid Id)
        {
            
            var team = await teamService.GetTeamDetails(Id);

            return View(team);
        }

        [Authorize]
        public async Task<IActionResult> AddPlayerToTeam(Guid Id)
        {
            var team = await teamService.GetTeamDetails(Id);

            var players = await playerService.GetAllPlayers();
            if (players.Count <= 0 || players == null)
            {
                return Redirect("/Shared/NoPlayers");
            }

            if (team.Players.Count == 5)
            {
                return Redirect("/Shared/PlayerLimitReached");
            }

            var result = await teamService.AddPlayerToTeam(Id);

            return RedirectToAction(nameof(GetAllTeams));
        }

        [Authorize]
        public async Task<IActionResult> RemoveTeam(Guid Id)
        {
            var result = await teamService.RemoveTeam(Id);

            return RedirectToAction(nameof(GetAllTeams));
        }
    }
}
