using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.ClimberModels
{
    public class ClimberDetail
    {
        public int ClimberId { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        [Display(Name = "Top Grade")]
        public int TopGrade { get; set; }
        [Display(Name = "Total Sends")]
        public int TotalSends { get; set; }
        [Display(Name = "Home Gym")]
        public string HomeGymName { get; set; }
        public int? GymId { get; set; }

    }
}
