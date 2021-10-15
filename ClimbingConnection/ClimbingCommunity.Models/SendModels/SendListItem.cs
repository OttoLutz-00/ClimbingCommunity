using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.SendModels
{
    public class SendListItem
    {

        public int SendId { get; set; }
        public int RouteId { get; set; }
        public int ClimberId { get; set; }
        public int Attempts { get; set; }
        public string Description { get; set; }
        public int SuggestedGrade { get; set; }
        public DateTimeOffset DateSent { get; set; }

        // for virtual Route
        public string RouteName { get; set; }
        public int RouteGrade { get; set; }

        // for virtual Gym
        public string GymName { get; set; }
        public string GymLocation { get; set; }
        public string GymDescription { get; set; }
        public int GymNumberOfRoutes { get; set; }

        // for virtual Climber
        public string ClimberUsername { get; set; }

    }
}
