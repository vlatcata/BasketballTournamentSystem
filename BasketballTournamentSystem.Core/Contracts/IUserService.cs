using BasketballTournamentSystem.Core.Models.User;
using BasketballTournamentSystem.Infrastructure.Identity;

namespace BasketballTournamentSystem.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        bool SetRoleRequest(ApplicationUser user);
        Task<ApplicationUser> GetUserById(string id);
        Task<bool> RemoveRoleRequest(ApplicationUser user);
    }
}
