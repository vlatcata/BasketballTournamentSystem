using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketballTournamentSystem.Infrastructure.Data
{
    public class Tournament
    {
        public Tournament()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Result { get; set; }

        public int TeamOneScore { get; set; }

        [Required]
        [ForeignKey(nameof(TeamOneId))]
        public Team TeamOne { get; set; }
        public Guid TeamOneId { get; set; }

        public int TeamTwoScore { get; set; }

        [Required]
        [ForeignKey(nameof(TeamTwoId))]
        public Team TeamTwo { get; set; }
        public Guid TeamTwoId { get; set; }
    }
}
