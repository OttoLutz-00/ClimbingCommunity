using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models
{
    public class ClimberCreate
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Username must be 50 characters or less.")]
        public string Username { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Bio must be 250 characters or less.")]
        public string Bio { get; set; }

        [Range(1,1000)]
        [Display(Name ="Home Gym")]
        public int GymId { get; set; }

    }
}
