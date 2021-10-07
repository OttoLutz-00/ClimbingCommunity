using ClimbingCommunity.Models.GymModels;
using ClimbingConnection.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimbingCommunity.Services
{
    public class GymService
    {
        private readonly Guid _userId;

        public GymService(Guid userId)
        {
            _userId = userId;
        }

        // CREATE
        public bool CreateGym(GymCreate model)
        {
            var entity = new Gym()
            {
                OwnerId = _userId,
                Name = model.Name,
                Description = model.Description,
                Location = model.Location,
                NumberOfRoutes = 0
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Gyms.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // READ
        public IEnumerable<GymListItem> GetAllGyms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Gyms
                    .Select(e => new GymListItem()
                    {
                        Name = e.Name,
                        Location = e.Location,
                        NumberOfRoutes = e.NumberOfRoutes
                    });

                return query.ToArray();
            }
        }

        // READ BY ID
        public GymDetail GetGymById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Gyms.Single(e => e.OwnerId == _userId && e.GymId == id);

                return new GymDetail()
                {
                    Name = query.Name,
                    Description = query.Description,
                    Location = query.Location,
                    NumberOfRoutes = query.NumberOfRoutes
                };
            }
        }

        // UPDATE
        public bool UpdateGym(GymEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var gym = ctx.Gyms.Single(e => e.OwnerId == _userId && e.GymId == model.GymId);

                gym.Name = model.Name;
                gym.Description = model.Description;
                gym.Location = model.Location;

                return ctx.SaveChanges() == 1;
            }
        }

        // DELETE
        public bool DeleteGym(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var gym = ctx.Gyms.Single(e => e.OwnerId == _userId && e.GymId == id);
                ctx.Gyms.Remove(gym);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
