using BasketballTournamentSystem.Core.Models.Tournament;

namespace BasketballTournamentSystem.Core.Contracts
{
    public interface ITournamentService
    {
        Task<bool> AddTournament(TournamentViewModel model);
        List<TournamentViewModel> GetAllTournaments();
        Task<bool> RemoveTournament(Guid id);
        Task<bool> AddOnePoint(Guid id, Guid tId);
        Task<bool> AddThreePoints(Guid id, Guid tId);
        Task<TournamentViewModel> DetailsTournament(Guid id);
    }
}
