using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Models.User;
using BasketballTournamentSystem.Data;
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

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await context.Users.Select(x => new UserListViewModel
            {
                Id = x.Id,
                Email = x.Email
            })
            .ToListAsync();
        }

        public bool SetRoleRequest(string userId)
        {
            var result = true;

            //var user =  context.Users.Where(x => x.HasRoleRequest);

            if (user == null)
            {
                result = false;
            }

            return result;
        }
    }
}
