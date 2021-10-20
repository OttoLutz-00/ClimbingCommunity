using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingConnection.Data
{
    public class Send
    {

        [Required]
        public Guid OwnerId { get; set; }

        [Key]
        public int SendId { get; set; }

        [Required]
        [ForeignKey(nameof(Route))]
        public int RouteId { get; set; }

        public virtual Route Route { get; set; }

        [Required]
        [ForeignKey(nameof(Climber))]
        public int ClimberId { get; set; }

        public virtual Climber Climber { get; set; }

        [Required]
        [ForeignKey(nameof(Gym))]
        public int GymId { get; set; }

        public virtual Gym Gym { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Attempts must be between 1 and 100.")]
        public int Attempts { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description must be 500 characters or less.")]
        public string Description { get; set; }

        [Required]
        [Range(0, 17, ErrorMessage = "Suggested grade must be between 1 and 17.")]
        public int SuggestedGrade { get; set; }

        [Required]
        public DateTimeOffset DateSent { get; set; }



    }
}
