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
    }
}