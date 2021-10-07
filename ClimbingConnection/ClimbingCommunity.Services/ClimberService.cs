using ClimbingCommunity.Models;
using ClimbingCommunity.Models.ClimberModels;
using ClimbingConnection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Services
{
    public class ClimberService
    {

        private readonly Guid _userId;

        public ClimberService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE
        public bool CreateClimber(ClimberCreate model)
        {
            var entity = new Climber()
            {
                OwnerId = _userId,
                Username = model.Username,
                Bio = model.Bio,
                GymId = model.GymId,
                TotalSends = 0
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Climbers.Add(entity);
                return ctx.SaveChanges() <= 3;
            }
        }

        // READ
        public IEnumerable<ClimberListItem> GetAllClimbers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Climbers
                    .Select(e => new ClimberListItem()
                    {
                        ClimberId = e.ClimberId,
                        Username = e.Username,
                        TopGrade = e.TopGrade,
                        TotalSends = e.TotalSends,
                        HomeGymName = e.Gym.Name,
                    });

                return query.ToArray();
            }
        }

        // READ BY ID
        public ClimberDetail GetClimberById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Climbers.Single(e => e.ClimberId == id);

                return new ClimberDetail()
                {
                    ClimberId = query.ClimberId,
                    GymId = query.GymId,
                    Username = query.Username,
                    Bio = query.Bio,
                    TopGrade = query.TopGrade,
                    TotalSends = query.TotalSends,
                    HomeGymName = query.Gym.Name
                };
            }
        }

        // UPDATE
        public bool UpdateClimber(ClimberEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var climber = ctx.Climbers.Single(e => e.OwnerId == _userId && e.ClimberId == model.ClimberId);

                if (climber == null)
                {
                    return false;
                }

                climber.Username = model.Username;
                climber.Bio = model.Bio;
                climber.GymId = model.GymId;

                return ctx.SaveChanges() == 1;
            }
        }

        // DELETE
        public bool DeleteClimber(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var climber = ctx.Climbers.Single(e => e.OwnerId == _userId && e.ClimberId == id);
                ctx.Climbers.Remove(climber);
                return ctx.SaveChanges() == 1;
            }
        }

        // READ (Checks if there is a climber that exists with a given owner id)
        public bool ClimberHasCreatedProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var climbers = ctx.Climbers.Where(e => e.OwnerId == _userId).DefaultIfEmpty(null).Single();
                if (climbers == null)
                    return false;
                else
                    return true;
            }
        }

        // READ (get climber name by OwnerId)
        public string GetClimberName()
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (ctx.Climbers.First(e => e.OwnerId == _userId).Username != null)
                {
                    return ctx.Climbers.First(e => e.OwnerId == _userId).Username;
                }
                else
                {
                    return null;
                }
            }
        }

        // READ (get climber Id by OwnerId)
        public int GetClimberId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                int number = 0;
                bool ownerHasClimber = ctx.Climbers.Where(e => e.OwnerId == _userId).SingleOrDefault() != null;

                if (ownerHasClimber)
                {
                    number = ctx.Climbers.Where(e => e.OwnerId == _userId).SingleOrDefault().ClimberId;
                }

                return number;

            }
        }

    }
}
