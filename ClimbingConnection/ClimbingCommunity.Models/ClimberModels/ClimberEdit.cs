using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Models.ClimberModels
{
    public class ClimberEdit
    {
        public int ClimberId { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public int GymId { get; set; }
    }
}
