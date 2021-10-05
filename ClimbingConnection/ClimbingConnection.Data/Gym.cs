using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingConnection.Data
{
    public class Gym
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int GymId { get; set; }
        [Required]
        [MaxLength(75, ErrorMessage = "Gym name must be 75 characters or less.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Description must be 250 characters or less.")]
        public string Description { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "Location must be 150 characters or less.")]
        public string Location { get; set; }
        [Display(Name = "Number of Routes")]
        public int NumberOfRoutes { get; set; }

    }
}
