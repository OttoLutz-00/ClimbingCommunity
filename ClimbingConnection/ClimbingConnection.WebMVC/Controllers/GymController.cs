using ClimbingCommunity.Models.GymModels;
using ClimbingCommunity.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClimbingConnection.WebMVC.Controllers
{
    public class GymController : Controller
    {
        // access to service layer
        private GymService CreateGymService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GymService(userId);
            return service;
        }
        // GET: Gym/Index
        [Authorize]
        public ActionResult Index()
        {
            
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GymService(userId);
            var model = service.GetAllGyms();
            return View(model);
        }

        // GET: Gym/Create
        [Authorize]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Gym/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GymCreate model)
        {
            
            if (!ModelState.IsValid) return View(model);

            var service = CreateGymService();

            if (!service.CreateGym(model))
            {
                ModelState.AddModelError("", "Gym could not be created.");
                return View(model);
            }
            TempData["SaveResult"] = "Your Gym was created.";

            return RedirectToAction("Index");
        }

        // GET: Gym/Details/{id}
        [Authorize]
        public ActionResult Details(int id)
        {
            
            var service = CreateGymService();
            var model = service.GetGymById(id);
            TempData["gymid"] = id;
            return View(model);
        }

        // GET: Gym/Edit/{id}
        [Authorize]
        public ActionResult Edit(int id)
        {
            var service = CreateGymService();
            var gym = service.GetGymById(id);
            var model = new GymEdit()
            {
                GymId = id,
                Name = gym.Name,
                Description = gym.Description,
                Location = gym.Location
            };
            return View(model);
        }

        // POST: Gym/Edit/{id}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GymEdit model)
        {
            
            if (!ModelState.IsValid) return View(model);

            if (model.GymId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateGymService();

            if (service.UpdateGym(model))
            {
                TempData["SaveResult"] = "Your gym was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your gym could not be updated.");
            return View(model);
        }

        // GET: Gym/Routes/{id}

        // GET: Gym/Sends/{id}
    }
}