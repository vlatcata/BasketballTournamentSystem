using BasketballTournamentSystem.Core.Models.Player;
using System.ComponentModel.DataAnnotations;

namespace BasketballTournamentSystem.Core.Models.Team
{
    public class TeamViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} cannot be empty or more than 30 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", ErrorMessage = "{0} Must be a valid image adress")]
        public string ImageUrl { get; set; }

        public List<PlayerViewModel>? Players { get; set; }
    }
}
