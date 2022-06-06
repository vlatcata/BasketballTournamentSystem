using BasketballTournamentSystem.Core.Contracts;
using BasketballTournamentSystem.Core.Services;

namespace BasketballTournamentSystem.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
