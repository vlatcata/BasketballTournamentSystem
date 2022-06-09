using BasketballTournamentSystem.Core.Models.Team;

namespace BasketballTournamentSystem.Core.Models.Tournament
{
    public class TournamentViewModel
    {
        public string Result { get; set; }

        public int TeamOneScore { get; set; }

        public int TeamTwoScore { get; set; }

        public TeamViewModel TeamOne { get; set; }

        public TeamViewModel TeamTwo { get; set; }
    }
}
