using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Player;
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

        public async Task<bool> AddPlayerToTeam(Guid id)
        {
            var result = true;

            var team = context.Teams.Where(t => t.Id == id).FirstOrDefault();

            // Gets a random player form the list of players
            var playerCount = context.Players.Where(p => p.IsInTeam == false).Count();
            var rnd = new Random();
            int randomCounter = rnd.Next(0, playerCount + 1);

            var player = context.Players.Select(p => p).Skip(randomCounter).FirstOrDefault();

            try
            {
                player.IsInTeam = true;
                team.Players.Add(player);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
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
                Name = t.Name,
                Players = t.Players.Select(p => new PlayerViewModel()
                {
                    Id = p.Id,
                    IsInTeam = p.IsInTeam,
                    GamesWon = p.GamesWon,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Number = p.Number,
                    Scores = p.Scores,
                    Speed = p.Speed,
                    Stamina = p.Stamina
                })
                .ToList()
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
                ImageUrl = t.ImageUrl,
                Players = t.Players.Select(p => new PlayerViewModel()
                {
                    Id = p.Id,
                    GamesWon = p.GamesWon,
                    ImageUrl = p.ImageUrl,
                    IsInTeam = true,
                    Name = p.Name,
                    Number = p.Number,
                    Scores = p.Scores,
                    Speed = p.Speed,
                    Stamina = p.Stamina
                })
                .ToList()
            })
                .FirstOrDefaultAsync();

            return team;
        }

        public async Task<bool> RemoveTeam(Guid id)
        {
            var result = true;

            var team = context.Teams.Where(t => t.Id == id)
                .Include(t => t.Players)
                .FirstOrDefault();

            foreach (var player in team.Players)
            {
                player.IsInTeam = false;
            }

            try
            {
                context.Teams.Remove(team);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
