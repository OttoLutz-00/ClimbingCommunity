using ClimbingCommunity.Models.SendModels;
using ClimbingConnection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Services
{
    public class SendService
    {

        private readonly Guid _userId;

        public SendService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE
        public bool CreateSend(SendCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                int id = 0;
                bool ownerHasClimber = ctx.Climbers.Where(e => e.OwnerId == _userId).SingleOrDefault() != null;
                if (ownerHasClimber) id = ctx.Climbers.Where(e => e.OwnerId == _userId).SingleOrDefault().ClimberId;

                var entity = new Send()
                {
                    OwnerId = _userId,
                    RouteId = model.RouteId,
                    ClimberId = id,
                    Attempts = model.Attempts,
                    Description = model.Description,
                    SuggestedGrade = model.SuggestedGrade,
                    DateSent = DateTimeOffset.Now
                };

                ctx.Sends.Add(entity);
                return ctx.SaveChanges() <= 2;

            }

        }
        // READ
        public IEnumerable<SendListItem> GetAllSends()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Sends.Select(e => new SendListItem()
                {
                    // for send info
                    SendId = e.SendId,
                    RouteId = e.RouteId,
                    ClimberId = e.ClimberId,
                    Attempts = e.Attempts,
                    Description = e.Description,
                    SuggestedGrade = e.SuggestedGrade,
                    DateSent = e.DateSent,
                    //for climber info
                    ClimberUsername = e.Climber.Username,
                    // for route info
                    RouteName = e.Route.Name,
                    RouteGrade = e.Route.Grade,
                    // for gym info
                    GymName = e.Gym.Name,
                    GymLocation = e.Gym.Location,
                    GymDescription = e.Gym.Description,
                    GymNumberOfRoutes = e.Gym.NumberOfRoutes
                });

                return query.ToList();
            }
        }

        // READ BY GymId
        public IEnumerable<SendListItem> GetAllSendsByGymId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Sends.Where(e => e.GymId == id).Select(e => new SendListItem()
                {
                    // for send info
                    SendId = e.SendId,
                    RouteId = e.RouteId,
                    ClimberId = e.ClimberId,
                    Attempts = e.Attempts,
                    Description = e.Description,
                    SuggestedGrade = e.SuggestedGrade,
                    DateSent = e.DateSent,
                    //for climber info
                    ClimberUsername = e.Climber.Username,
                    // for route info
                    RouteName = e.Route.Name,
                    RouteGrade = e.Route.Grade,
                    // for gym info
                    GymName = e.Gym.Name,
                    GymLocation = e.Gym.Location,
                    GymDescription = e.Gym.Description,
                    GymNumberOfRoutes = e.Gym.NumberOfRoutes
                });

                return query.ToList();
            }
        }
    }
}
