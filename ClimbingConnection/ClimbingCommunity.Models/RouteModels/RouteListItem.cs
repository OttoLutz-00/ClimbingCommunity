using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.RouteModels
{
    public class RouteListItem
    {
        public int RouteId { get; set; }
        public int GymId { get; set; }
        [Display(Name = "Gym")]
        public string GymName { get; set; }
        public string GymLocation { get; set; }
        public string GymDescription { get; set; }
        public int GymNumberOfRoutes { get; set; }
        public int ClimberId { get; set; }
        [Display(Name = "Route Setter")]
        public string ClimberUsername { get; set; }
        [Display(Name = "Route Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Grade { get; set; }
        [Display(Name = "Date Set")]
        public DateTimeOffset DateSet { get; set; }
        [Display(Name = "Total Sends")]
        public int TotalSends { get; set; }
        [Display(Name = "On The Wall")]
        public bool IsOnWall { get; set; }
    }
}
