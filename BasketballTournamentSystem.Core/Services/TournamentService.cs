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

        public async Task<bool> AddOnePoint(Guid id, Guid tId)
        {
            var result = true;

            var tournament = context.Tournaments.Where(t => t.Id == tId).FirstOrDefault();
            Team team;

            try
            {
                if (tournament.TeamOneId == id)
                {
                    team = tournament.TeamOne;
                    tournament.TeamOneScore += 1;
                }
                else
                {
                    team = tournament.TeamTwo;
                    tournament.TeamTwoScore += 1;
                }

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> AddThreePoints(Guid id, Guid tId)
        {
            var result = true;

            var tournament = context.Tournaments.Where(t => t.Id == tId).FirstOrDefault();
            Team team;

            try
            {
                if (tournament.TeamOneId == id)
                {
                    team = tournament.TeamOne;
                    tournament.TeamOneScore += 3;
                }
                else
                {
                    team = tournament.TeamTwo;
                    tournament.TeamTwoScore += 3;
                }

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> AddTournament(TournamentViewModel model)
        {
            var result = true;

            var team1 = context.Teams.Select(t => t).Where(t => t.IsInTournament == false && t.Players.Count == 5).Include(t => t.Players).FirstOrDefault();
            var team2 = context.Teams.Select(t => t).Where(t => t.IsInTournament == false && t.Players.Count == 5).Skip(1).Include(t => t.Players).FirstOrDefault();

            if (team1 == null || team2 == null)
            {
                return false;
            }

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
                team1.IsInTournament = true;
                team2.IsInTournament = true;
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
                TeamOneScore= t.TeamOneScore,
                TeamOne = context.Teams.Where(te => te.Id == t.TeamOneId).Select(t => new TeamViewModel()
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
                TeamTwoScore = t.TeamTwoScore,
                TeamTwo = context.Teams.Where(te => te.Id == t.TeamTwoId).Select(t => new TeamViewModel()
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

            var result = CheckScores(tournaments);

            return result;
        }

        private List<TournamentViewModel> CheckScores(List<TournamentViewModel> tournaments)
        {
            foreach (var tournament in tournaments)
            {
                if (tournament.TeamOneScore >= 31)
                {
                    tournament.Result = $"{tournament.TeamOne.Name} win!";
                }
                else if (tournament.TeamTwoScore >= 31)
                {
                    tournament.Result = $"{tournament.TeamTwo.Name} win!";
                }
            }

            return tournaments.ToList();
        }

        public async Task<bool> RemoveTournament(Guid id)
        {
            var result = true;

            var tournament = context.Tournaments.Where(t => t.Id == id).Include(t => t.TeamOne).Include(t => t.TeamTwo).FirstOrDefault();

            try
            {
                tournament.TeamOne.IsInTournament = false;
                tournament.TeamTwo.IsInTournament = false;
                context.Tournaments.Remove(tournament);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<TournamentViewModel> DetailsTournament(Guid id)
        {
            var tournament = context.Tournaments.Where(t => t.Id == id).Select(t => new TournamentViewModel()
            {
                Id = t.Id,
                Name = t.Name,
                Result = t.Result,
                TeamOneScore = t.TeamOneScore,
                TeamTwoScore = t.TeamTwoScore,
                TeamOne = context.Teams.Where(te => te.Id == t.TeamOneId).Select(t => new TeamViewModel()
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
                TeamTwo = context.Teams.Where(te => te.Id == t.TeamTwoId).Select(t => new TeamViewModel()
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
                .FirstOrDefault();

            return tournament;
        }
    }
}
