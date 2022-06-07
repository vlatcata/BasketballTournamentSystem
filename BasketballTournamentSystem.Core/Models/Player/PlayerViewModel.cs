using BasketballTournamentSystem.Core.Models.Team;

namespace BasketballTournamentSystem.Core.Models.Player
{
    public class PlayerViewModel
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Number { get; set; }

        public double Stamina { get; set; }

        public int Scores { get; set; }

        public double Speed { get; set; }

        public int GamesWon { get; set; }

        public TeamViewModel? Team { get; set; }
    }
}
