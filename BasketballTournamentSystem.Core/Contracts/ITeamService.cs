using BasketballTournamentSystem.Core.Models.Team;

namespace BasketballTournamentSystem.Core.Contracts
{
    public interface ITeamService
    {
        Task<bool> CreateTeam(TeamViewModel model);
        Task<List<TeamViewModel>> GetAllTeams();
        Task<TeamViewModel> GetTeamDetails(Guid id);
        Task<bool> AddPlayerToTeam(Guid id);
        Task<bool> RemoveTeam(Guid id);
    }
}
