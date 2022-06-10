using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Player;
using BasketballTournamentSystem.Core.Models.Team;
using BasketballTournamentSystem.Core.Models.Tournament;
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
    public class TournamentService : ITournamentService
    {
        private readonly ApplicationDbContext context;

        public TournamentService(ApplicationDbContext _contex)
        {
            context = _contex;
        }

        public async Task<bool> AddTournament(TournamentViewModel model)
        {
            var result = true;

            var team1 = context.Teams.Select(t => t).Include(t => t.Players).FirstOrDefault();
            var team2 = context.Teams.Select(t => t).Include(t => t.Players).FirstOrDefault();

            var tournament = new Tournament()
            {
                Name = model.Name,
                Result = "",
                TeamOne = team1,
                TeamTwo = team2,
                TeamOneScore = 0,
                TeamTwoScore = 0
            };

            try
            {
                context.Tournaments.Add(tournament);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public List<TournamentViewModel> GetAllTournaments()
        {
            var tournaments = context.Tournaments.Select(t => new TournamentViewModel()
            {
                Id = t.Id,
                Name = t.Name,
                Result = t.Result,
                TeamOne = context.Teams.Where(x => x.Id == t.Id).Select(t => new TeamViewModel()
                {
                    Id = t.Id,
                    ImageUrl = t.ImageUrl,
                    Name = t.Name,
                    Players = t.Players.Select(p => new PlayerViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageUrl = p.ImageUrl,
                        GamesWon = p.GamesWon,
                        IsInTeam = p.IsInTeam,
                        Number = p.Number,
                        Scores = p.Scores,
                        Speed = p.Speed,
                        Stamina = p.Stamina
                    })
                    .ToList(),
                })
                .FirstOrDefault(),
                TeamTwo = context.Teams.Where(x => x.Id == t.Id).Select(t => new TeamViewModel()
                {
                    Id = t.Id,
                    ImageUrl = t.ImageUrl,
                    Name = t.Name,
                    Players = t.Players.Select(p => new PlayerViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageUrl = p.ImageUrl,
                        GamesWon = p.GamesWon,
                        IsInTeam = p.IsInTeam,
                        Number = p.Number,
                        Scores = p.Scores,
                        Speed = p.Speed,
                        Stamina = p.Stamina
                    })
                    .ToList(),
                })
                .FirstOrDefault()
            })
                .ToList();

            return tournaments;
        }

        public async Task<bool> RemoveTournament(Guid id)
        {
            var result = true;

            var tournament = context.Tournaments.Where(t => t.Id == id).FirstOrDefault();

            try
            {
                context.Tournaments.Remove(tournament);
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
