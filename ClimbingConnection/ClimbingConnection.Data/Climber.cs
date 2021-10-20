using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingConnection.Data
{
    public class Climber
    {
        public Guid OwnerId { get; set; }
        [Key]
        public int ClimberId { get; set; }
        [ForeignKey(nameof(Gym))]
        public int? GymId { get; set; }
        public virtual Gym Gym { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        public string Bio { get; set; }
        [Display(Name = "Top Grade")]
        public int TopGrade { get; set; }
        [Display(Name = "Total Sends")]
        public int TotalSends { get; set; }
    }
}
