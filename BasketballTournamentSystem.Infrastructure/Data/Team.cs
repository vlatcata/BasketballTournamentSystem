using System.ComponentModel.DataAnnotations;

namespace BasketballTournamentSystem.Infrastructure.Data
{
    public class Team
    {
        public Team()
        {
            Id = Guid.NewGuid();
            Players = new List<Player>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public bool IsInTournament { get; set; }

        public List<Player> Players { get; set; }
    }
}
