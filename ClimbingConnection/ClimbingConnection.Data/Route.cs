using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingConnection.Data
{
    public class Route
    {
        [Required]
        public Guid OwnerId { get; set; }

        [Key]
        public int RouteId { get; set; }

        [Required]
        [ForeignKey(nameof(Gym))]
        public int GymId { get; set; }

        public virtual Gym Gym { get; set; }

        [Required]
        [ForeignKey(nameof(Climber))]
        public int ClimberId { get; set; }

        public virtual Climber Climber { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Route name must be 80 characters or less.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description must be 500 characters or less.")]
        public string Description { get; set; }

        [Required]
        [Range(0,17, ErrorMessage = "Grade must be between 1 and 17.")]
        public int Grade { get; set; }

        [Required]
        [Display(Name = "Date Set")]
        public DateTimeOffset DateSet { get; set; } 

        [Required]
        [Display(Name = "Total Sends")]
        public int TotalSends { get; set; }

        [Required]
        [Display( Name = "Route Is On Wall")]
        public bool IsOnWall { get; set; }
    }
}
