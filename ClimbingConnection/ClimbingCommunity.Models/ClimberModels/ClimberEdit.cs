using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.ClimberModels
{
    public class ClimberEdit
    {
        public int ClimberId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Bio { get; set; }
        [Display(Name ="Home Gym")]
        public int? GymId { get; set; }
    }
}
