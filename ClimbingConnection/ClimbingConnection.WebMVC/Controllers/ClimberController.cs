using ClimbingCommunity.Models;
using ClimbingCommunity.Models.ClimberModels;
using ClimbingCommunity.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClimbingConnection.WebMVC.Controllers
{
    public class ClimberController : Controller
    {
        // access to service layer
        private ClimberService CreateClimberService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ClimberService(userId);
            return service;
        }

        // GET: Climber/Index
        [Authorize]
        public ActionResult Index()
        {
            if (!(TempData.Peek("HasProfile") as bool? ?? false))
            {
                TempData["ClimberMessage"] = "Please complete your profile to access Gyms, Climbers, and Routes";
                return RedirectToAction("Create","Climber");
            }


            var service = CreateClimberService();
            var model = service.GetAllClimbers();
            return View(model);
        }

        // GET: Climber/Create
        [Authorize]
        public ActionResult Create()
        {
           
            var service = CreateClimberService();
            bool hasClimber = service.ClimberHasCreatedProfile();
            if (hasClimber)
            {
                TempData["ClimberMessage"] = "You already have a climber profile.";
                return RedirectToAction("Details", new { id=service.GetClimberId()});
            }
            var gymService = new GymService(Guid.Parse(User.Identity.GetUserId()));
            ViewBag.gymList = gymService.GetAllGyms();

            return View();
        }

        // POST: Climber/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClimberCreate model)
        {
            
            if (!ModelState.IsValid) return View(model);

            var service = CreateClimberService();

            if (!service.CreateClimber(model))
            {
                var gymService = new GymService(Guid.Parse(User.Identity.GetUserId()));
                ViewBag.gymList = gymService.GetAllGyms();
                ModelState.AddModelError("", "Climber could not be created.");
                return View(model);
            }
            TempData["SaveResult"] = "Your Climber was created.";
            
            return RedirectToAction("Index", "Home");
        }

        // GET: Climber/Edit/{id}
        public ActionResult Edit(int id)
        {
            
            var service = CreateClimberService();
            var climber = service.GetClimberById(id);
            int currentUsersClimberId = service.GetClimberId();
            if (currentUsersClimberId != id)
            {
                TempData["ClimberMessage"] = "You cannot edit another user's climber profile.";
                return RedirectToAction("Details", new { id=currentUsersClimberId});
            }

            var model = new ClimberEdit()
            {
                ClimberId = id,
                Username = climber.Username,
                Bio = climber.Bio,
                GymId = climber.GymId
            };
            var gymService = new GymService(Guid.Parse(User.Identity.GetUserId()));
            ViewBag.gymList = gymService.GetAllGyms();

            return View(model);
        }

        // POST: Climber/Edit/{id}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ClimberEdit model)
        {
            
            if (!ModelState.IsValid) return View(model);

            if (model.ClimberId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateClimberService();

            if (service.UpdateClimber(model))
            {
                TempData["SaveResult"] = "Your climber profile was updated.";
                return RedirectToAction("Index", "Home");
            }

            var gymService = new GymService(Guid.Parse(User.Identity.GetUserId()));
            ViewBag.gymList = gymService.GetAllGyms();
            ModelState.AddModelError("", "Your climber profile could not be updated.");
            return View(model);
        }


        // GET: Climber/Details/{id}
        [Authorize]
        public ActionResult Details(int id)
        {
            var service = CreateClimberService();
            var model = service.GetClimberById(id);
            TempData["CurrentClimberId"] = service.GetClimberId();
            return View(model);
        }

        // GET: Climber/Delete/{id}
        
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            
            var service = CreateClimberService();
            var model = service.GetClimberById(id);
            return View(model);
        }

        // POST: Climber/Delete/{id}
        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClimber(int id)
        {
            var service = CreateClimberService();
            service.DeleteClimber(id);
            return RedirectToAction("Index");
        }

    }
}