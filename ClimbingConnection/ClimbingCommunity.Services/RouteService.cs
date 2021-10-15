using ClimbingCommunity.Models.RouteModels;
using ClimbingConnection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Services
{
    public class RouteService
    {

        private readonly Guid _userId;

        public RouteService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE
        public bool CreateRoute(RouteCreate model)
        {
            var entity = new Route()
            {
                OwnerId = _userId,
                GymId = model.GymId,
                ClimberId = model.ClimberId,
                Name = model.Name,
                Description = model.Description,
                Grade = model.Grade,
                DateSet = DateTimeOffset.UtcNow,
                TotalSends = 0,
                IsOnWall = true
            };

            using (var ctx = new ApplicationDbContext())
            {
                //Update number of routes in the gym based on IsOnWall, in the case of creating a new route, it is always on the wall, so add 1 to the total routes in the gym.
                var gym = ctx.Gyms.Single(e => e.GymId == entity.GymId);
                gym.NumberOfRoutes++;

                ctx.Routes.Add(entity);
                return ctx.SaveChanges() <= 2;
            }
        }

        // READ
        public IEnumerable<RouteListItem> GetAllRoutes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Routes.Select(e => new RouteListItem()
                {
                    RouteId = e.RouteId,
                    GymId = e.GymId,
                    GymName = e.Gym.Name,
                    GymLocation = e.Gym.Location,
                    GymDescription = e.Gym.Description,
                    GymNumberOfRoutes = e.Gym.NumberOfRoutes,
                    ClimberId = e.ClimberId,
                    ClimberUsername = e.Climber.Username,
                    Name = e.Name,
                    Description = e.Description,
                    Grade = e.Grade,
                    DateSet = e.DateSet,
                    TotalSends = e.TotalSends,
                    IsOnWall = e.IsOnWall
                });

                return query.ToList();
            }
        }

        // READ BY ID (maybe needed only as a helper method)
        public RouteListItem GetRouteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Routes.Single(e => e.RouteId == id);

                var model = new RouteListItem()
                {
                    RouteId = query.RouteId,
                    GymId = query.GymId,
                    GymName = query.Gym.Name,
                    GymLocation = query.Gym.Location,
                    GymDescription = query.Gym.Description,
                    GymNumberOfRoutes = query.Gym.NumberOfRoutes,
                    ClimberId = query.ClimberId,
                    ClimberUsername = query.Climber.Username,
                    Name = query.Name,
                    Description = query.Description,
                    Grade = query.Grade,
                    DateSet = query.DateSet,
                    TotalSends = query.TotalSends,
                    IsOnWall = query.IsOnWall
                };

                return model;
            }
        }

        // READ BY GymId
        public IEnumerable<RouteListItem> GetAllRoutesByGymId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Routes.Where(e => e.GymId == id).Select(e => new RouteListItem()
                {
                    RouteId = e.RouteId,
                    GymId = e.GymId,
                    GymName = e.Gym.Name,
                    GymLocation = e.Gym.Location,
                    GymDescription = e.Gym.Description,
                    GymNumberOfRoutes = e.Gym.NumberOfRoutes,
                    ClimberId = e.ClimberId,
                    ClimberUsername = e.Climber.Username,
                    Name = e.Name,
                    Description = e.Description,
                    Grade = e.Grade,
                    DateSet = e.DateSet,
                    TotalSends = e.TotalSends,
                    IsOnWall = e.IsOnWall
                });

                return query.ToList();
            }
        }

        // UPDATE (regular edit)
        public bool UpdateRoute(RouteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Routes.Single(e => e.RouteId == model.RouteId);

                var gym = ctx.Gyms.Single(e => e.GymId == entity.GymId);
                //if the route was up, and is being taken down:
                if (entity.IsOnWall == true && model.IsOnWall == false)
                {
                    gym.NumberOfRoutes--;
                }
                //if the route was down, and is being put back up:
                else if (entity.IsOnWall == false && model.IsOnWall == true)
                {
                    gym.NumberOfRoutes++;
                }
                

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Grade = model.Grade;
                entity.IsOnWall = model.IsOnWall;



                return ctx.SaveChanges() <= 2;
            }
        }
    }
}
