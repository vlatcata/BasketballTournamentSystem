using BasketballTournamentSystem.Core.Models.Player;

namespace BasketballTournamentSystem.Core.Contracts
{
    public interface IPlayerService
    {
        Task<bool> CreatePlayer(PlayerViewModel model);

        Task<List<PlayerViewModel>> GetAllPlayers();
    }
}
