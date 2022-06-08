using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.User;
using BasketballTournamentSystem.Data;
using BasketballTournamentSystem.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasketballTournamentSystem.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> RemoveRoleRequest(ApplicationUser user)
        {
            var result = true;

            try
            {
                user.HasRoleRequest = false;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await context.Users.Select(x => new UserListViewModel
            {
                Id = x.Id,
                Email = x.Email,
                HasRoleRequest = x.HasRoleRequest
            })
            .ToListAsync();
        }

        public bool SetRoleRequest(ApplicationUser user)
        {
            var result = true;

            if (!user.HasRoleRequest)
            {
                try
                {
                    user.HasRoleRequest = true;
                    context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
