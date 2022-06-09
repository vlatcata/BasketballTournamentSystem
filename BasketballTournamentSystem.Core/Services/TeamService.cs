using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Team;
using BasketballTournamentSystem.Data;
using BasketballTournamentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournamentSystem.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext context;

        public TeamService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> CreateTeam(TeamViewModel model)
        {
            var result = true;

            var team = new Team()
            {
                Name = model.Name,
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                IsInTournament = false
            };

            try
            {
                await context.Teams.AddAsync(team);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<List<TeamViewModel>> GetAllTeams()
        {
            var teams = await context.Teams.Select(t => new TeamViewModel()
            {
                Id = t.Id,
                ImageUrl = t.ImageUrl,
                Name = t.Name
            })
                .ToListAsync();

            return teams;
        }

        public async Task<TeamViewModel> GetTeamDetails(Guid id)
        {
            var team = await context.Teams.Select(t => new TeamViewModel()
            {
                Id = t.Id,
                Name = t.Name,
                ImageUrl = t.ImageUrl
            })
                .FirstOrDefaultAsync();

            return team;
        }
    }
}
