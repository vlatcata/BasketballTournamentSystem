using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Tournament;
using BasketballTournamentSystem.Infrastructure.Identity;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;
        private readonly UserManager<ApplicationUser> userManager;

        public TournamentController(ITournamentService _tournamentService, ITeamService _teamService, UserManager<ApplicationUser> _userManager)
        {
            tournamentService = _tournamentService;
            teamService = _teamService;
            userManager = _userManager;
        }

        [Authorize]
        public async Task<IActionResult> AddTournament()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTournament(TournamentViewModel model)
        {
            if (!User.IsInRole("Guest") && !User.IsInRole("Administrator"))
            {
                var user = await userManager.GetUserAsync(User);
                return View("RequestRole", user);
            }

            var result = await tournamentService.AddTournament(model);

            if (!result)
            {
                return Redirect("/Shared/NoTeams");
            }

            return RedirectToAction(nameof(GetAllTournaments));
        }

        public async Task<IActionResult> GetAllTournaments(string searchString, int pageNumber = 1, int pageSize = 4)
        {
            var excludeRecords = (pageSize * pageNumber) - pageSize;

            var tournaments = tournamentService.GetAllTournaments();

            if (!String.IsNullOrEmpty(searchString))
            {
                tournaments = tournaments.Where(p => p.TeamOne.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) 
                || p.TeamTwo.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            var tournamentsPerPage = tournaments.Skip(excludeRecords).Take(pageSize).ToList();

            var result = new PagedResult<TournamentViewModel>
            {
                Data = tournamentsPerPage,
                TotalItems = tournaments.Count,
                PageNumber = pageNumber,
                PageSize = pageSize

            };

            return View(result);
        }

        [Authorize]
        public async Task<IActionResult> RemoveTournament(Guid Id)
        {
            var result = await tournamentService.RemoveTournament(Id);

            return RedirectToAction(nameof(GetAllTournaments));
        }

        [Authorize]
        public async Task<IActionResult> AddOnePoint(Guid Id, Guid tId)
        {
            var result = await tournamentService.AddOnePoint(Id, tId);

            return RedirectToAction(nameof(GetAllTournaments));
        }

        [Authorize]
        public async Task<IActionResult> AddThreePoints(Guid Id, Guid tId)
        {
            var result = await tournamentService.AddThreePoints(Id, tId);

            return RedirectToAction(nameof(GetAllTournaments));
        }

        public async Task<IActionResult> TournamentDetails(Guid Id)
        {
            var tournament = await tournamentService.DetailsTournament(Id);

            return View(tournament);
        }
    }
}
