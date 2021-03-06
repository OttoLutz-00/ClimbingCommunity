using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.RouteModels
{
    public class RouteEdit
    {

        public int RouteId { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Route name must be 80 characters or less.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description must be 500 characters or less.")]
        public string Description { get; set; }

        [Required]
        [Range(0, 17, ErrorMessage = "Grade must be between 1 and 17.")]
        public int Grade { get; set; }

        [Required]
        [Display(Name = "Route Is On Wall")]
        public bool IsOnWall { get; set; }

    }
}
