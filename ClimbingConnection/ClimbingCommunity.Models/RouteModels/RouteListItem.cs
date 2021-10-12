using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.RouteModels
{
    public class RouteListItem
    {
        public int RouteId { get; set; }
        public int GymId { get; set; }
        public string GymName { get; set; }
        public string GymLocation { get; set; }
        public string GymDescription { get; set; }
        public int GymNumberOfRoutes { get; set; }
        public int ClimberId { get; set; }
        public string ClimberUsername { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Grade { get; set; }
        public DateTimeOffset DateSet { get; set; }
        public int TotalSends { get; set; }
        public bool IsOnWall { get; set; }
    }
}
