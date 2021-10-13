using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.RouteModels
{
    public class RouteCreate
    {
        [Required]
        public int GymId { get; set; }

        [Required]
        public int ClimberId { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Route name must be 80 characters or less.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description must be 500 characters or less.")]
        public string Description { get; set; }

        [Required]
        [Range(0,17, ErrorMessage = "Grade must be between 0 and 17.")]
        public int Grade { get; set; }
    }
}
