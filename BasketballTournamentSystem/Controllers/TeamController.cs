using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Team;
using BasketballTournamentSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;

namespace BasketballTournamentSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly UserManager<ApplicationUser> userManager;

        public TeamController(ITeamService _teamService, UserManager<ApplicationUser> _userManager)
        {
            teamService = _teamService;
            userManager = _userManager;
        }

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

        public async Task<IActionResult> TeamDetails(Guid id)
        {
            var team = await teamService.GetTeamDetails(id);

            return View(team);
        }

        public async Task<IActionResult> AddPlayerToTeam(Guid id)
        {
            var team = await teamService.GetTeamDetails(id);

            if (team.Players.Count == 5)
            {
                return Redirect("/Team/PlayerLimitReached");
            }

            await teamService.AddPlayerToTeam(id);

            return RedirectToAction(nameof(TeamDetails), id);
        }
    }
}
