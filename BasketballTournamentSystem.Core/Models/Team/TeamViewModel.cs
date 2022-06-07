using BasketballTournamentSystem.Core.Models.Player;

namespace BasketballTournamentSystem.Core.Models.Team
{
    public class TeamViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<PlayerViewModel> Players { get; set; }
    }
}
