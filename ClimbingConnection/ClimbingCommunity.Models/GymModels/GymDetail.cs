using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.GymModels
{
    public class GymDetail
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Display(Name = "Number of Routes")]
        public int NumberOfRoutes { get; set; }

    }
}
