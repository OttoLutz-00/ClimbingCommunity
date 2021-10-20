using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.SendModels
{
    public class SendCreate
    {
        [Required]
        public int RouteId { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Attempts must be between 1 and 100.")]
        public int Attempts { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description must be 500 characters or less.")]
        public string Description { get; set; }

        [Required]
        [Range(0, 17, ErrorMessage = "Suggested grade must be between 1 and 17.")]
        public int SuggestedGrade { get; set; }

    }
}
