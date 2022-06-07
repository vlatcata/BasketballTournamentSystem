using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Required]
        [ForeignKey(nameof(TeamOneId))]
        public Team TeamOne { get; set; }
        public Guid TeamOneId { get; set; }

        [Required]
        [ForeignKey(nameof(TeamTwoId))]
        public Team TeamTwo { get; set; }
        public Guid TeamTwoId { get; set; }
    }
}
