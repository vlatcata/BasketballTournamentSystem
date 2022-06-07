using BasketballTournamentSystem.Core.Models.Team;
using System.ComponentModel.DataAnnotations;

namespace BasketballTournamentSystem.Core.Models.Player
{
    public class PlayerViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} cannot be empty or more than 50 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", ErrorMessage = "{0} Must be a valid image adress")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, 99)]
        public int Number { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Stamina must be between 1 and 10")]
        public double Stamina { get; set; }

        [Required]
        public int Scores { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Speed must be between 1 and 10")]
        public double Speed { get; set; }

        [Required]
        public int GamesWon { get; set; }

        [Required]
        public bool IsInTeam { get; set; }
    }
}
