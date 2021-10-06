using ClimbingCommunity.Models;
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
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ClimberService(userId);
            var model = service.GetAllClimbers();
            return View(model);
        }

        // GET: Climber/Create
        [Authorize]
        public ActionResult Create()
        {
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
                ModelState.AddModelError("", "Climber could not be created.");
                return View(model);
            }
            TempData["SaveResult"] = "Your Climber was created.";

            return RedirectToAction("Index");
        }


    }
}