using Microsoft.AspNetCore.Identity;

namespace BasketballTournamentSystem.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        public bool HasRoleRequest { get; set; }
    }
}
