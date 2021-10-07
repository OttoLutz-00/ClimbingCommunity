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
            TempData.Keep();
            var service = CreateClimberService();
            var model = service.GetAllClimbers();
            return View(model);
        }

        // GET: Climber/Create
        [Authorize]
        public ActionResult Create()
        {
            TempData.Keep();
            var service = CreateClimberService();
            bool hasClimber = service.ClimberHasCreatedProfile();
            if (hasClimber)
            {
                TempData["ClimberMessage"] = "You already have a climber profile.";
                return RedirectToAction("Details", new { id=service.GetClimberId()});
            }
            return View();
        }

        // POST: Climber/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClimberCreate model)
        {
            TempData.Keep();
            if (!ModelState.IsValid) return View(model);

            var service = CreateClimberService();

            if (!service.CreateClimber(model))
            {
                ModelState.AddModelError("", "Climber could not be created.");
                return View(model);
            }
            TempData["SaveResult"] = "Your Climber was created.";

            return RedirectToAction("Index", "Home");
        }

        // GET: Climber/Edit/{id}
        public ActionResult Edit(int id)
        {
            TempData.Keep();
            var service = CreateClimberService();
            var climber = service.GetClimberById(id);
            var model = new ClimberEdit()
            {
                ClimberId = id,
                Username = climber.Username,
                Bio = climber.Bio,
                GymId = climber.GymId
            };

            return View(model);
        }

        // POST: Climber/Edit/{id}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ClimberEdit model)
        {
            TempData.Keep();
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
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your climber profile could not be updated.");
            return View(model);
        }


        // GET: Climber/Details/{id}
        [Authorize]
        public ActionResult Details(int id)
        {
            TempData.Keep();
            var service = CreateClimberService();
            var model = service.GetClimberById(id);
            return View(model);
        }


    }
}