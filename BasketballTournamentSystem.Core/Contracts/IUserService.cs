using BasketballTournamentSystem.Core.Models.User;

namespace BasketballTournamentSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        bool SetRoleRequest(string userId);
    }
}
