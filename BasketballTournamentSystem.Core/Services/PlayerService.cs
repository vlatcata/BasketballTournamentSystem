using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.Player;
using BasketballTournamentSystem.Data;
using BasketballTournamentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasketballTournamentSystem.Core.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext context;

        public PlayerService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> CreatePlayer(PlayerViewModel model)
        {
            var result = false;

            var player = new Player()
            {
                Name = model.Name,
                GamesWon = model.GamesWon,
                ImageUrl = model.ImageUrl,
                Number = model.Number,
                Scores = model.Scores,
                Speed = model.Speed,
                Stamina = model.Stamina,
                IsInTeam = false
            };

            try
            {
                await context.AddAsync(player);
                await context.SaveChangesAsync();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public Task<List<PlayerViewModel>> GetAllPlayers()
        {
            var players = context.Players.Select(p => new PlayerViewModel
            {
                GamesWon = p.GamesWon,
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Number = p.Number,
                Scores = p.Scores,
                Speed = p.Speed,
                Stamina = p.Stamina
            })
                .ToListAsync();

            return players;
        }

        public async Task<PlayerViewModel> GetPlayer(Guid id)
        {
            var player = context.Players
                .Where(p => p.Id == id)
                .Select(p => new PlayerViewModel
            {
                GamesWon = p.GamesWon,
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Number = p.Number,
                Scores = p.Scores,
                Speed = p.Speed,
                Stamina = p.Stamina

            })
                .FirstOrDefault();

            return player;
        }
    }
}
