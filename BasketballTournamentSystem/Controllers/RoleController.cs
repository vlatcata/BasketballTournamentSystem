using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasketballTournamentSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(IUserService _userService, UserManager<ApplicationUser> _userManager)
        {
            userService = _userService;
            userManager = _userManager;
        }

        public async Task<IActionResult> SetRoleRequest(string Id)
        {
            var user = await userService.GetUserById(Id);

            if(user.HasRoleRequest)
            {
                return Redirect("/Shared/RoleAlreadyRequested");
            }

            var result = userService.SetRoleRequest(user);

            return Redirect("/Shared/RoleHasBeenRequested");
        }

        public async Task<IActionResult> GiveRole()
        {
            var users = await userService.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> AcceptRole(string Id)
        {
            var user = await userService.GetUserById(Id);

            await userManager.AddToRoleAsync(user, "Guest");

            await userService.RemoveRoleRequest(user);

            return Redirect("/Admin/User/ManageUsers");
        }

        public async Task<IActionResult> DeclineRole(string Id)
        {
            var user = await userService.GetUserById(Id);

            var removedRoleRequest = await userService.RemoveRoleRequest(user);

            if (!removedRoleRequest)
            {
                Redirect("Error/CustomError500");
            }

            return Redirect("/Admin/User/ManageUsers");
        }
    }
}
