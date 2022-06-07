using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballTournamentSystem.Infrastructure.Data
{
    public class Player
    {
        public Player()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public double Stamina { get; set; }

        [Required]
        public int Scores { get; set; }

        [Required]
        public double Speed { get; set; }

        [Required]
        public int GamesWon { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }
        public Guid TeamId { get; set; }
    }
}
