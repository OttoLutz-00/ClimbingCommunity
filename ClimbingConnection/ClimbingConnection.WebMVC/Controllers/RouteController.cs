using ClimbingCommunity.Models.RouteModels;
using ClimbingCommunity.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClimbingConnection.WebMVC.Controllers
{
    public class RouteController : Controller
    {

        // access to service layer
        private RouteService CreateRouteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RouteService(userId);
            return service;
        }

        // GET: Route/Index
        public ActionResult Index()
        {

            if (!(TempData.Peek("HasProfile") as bool? ?? false))
            {
                TempData["ClimberMessage"] = "Please complete your profile to access Gyms, Climbers, and Routes";
                return RedirectToAction("Create", "Climber");
            }

            var service = CreateRouteService();
            var model = service.GetAllRoutes();
            return View(model);
        }

        // GET: Route/Details{id}
        [Authorize]
        public ActionResult Details(int id)
        {
            var service = CreateRouteService();
            var model = service.GetRouteById(id);

            var climberService = new ClimberService(Guid.Parse(User.Identity.GetUserId()));
            TempData["CurrentClimberId"] = climberService.GetClimberId();

            return View(model);
        }

        // GET: Route/Gym/{id}
        public ActionResult Gym(int id)
        {
            var service = CreateRouteService();
            var model = service.GetAllRoutesByGymId(id);
            return View(model);
        }

        // GET: Route/Create
        [Authorize]
        public ActionResult Create()
        {

            var service = CreateRouteService();
            var gymService = new GymService(Guid.Parse(User.Identity.GetUserId()));
            ViewBag.gymList = gymService.GetAllGyms();
            var climberService = new ClimberService(Guid.Parse(User.Identity.GetUserId()));
            ViewBag.climberList = climberService.GetAllClimbers();
            return View();
        }

        // POST: Route/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RouteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRouteService();

            if (!service.CreateRoute(model))
            {
                var gymService = new GymService(Guid.Parse(User.Identity.GetUserId()));
                ViewBag.gymList = gymService.GetAllGyms();
                var climberService = new ClimberService(Guid.Parse(User.Identity.GetUserId()));
                ViewBag.climberList = climberService.GetAllClimbers();
                ModelState.AddModelError("", "Route could not be created.");

                return View(model);
            }

            return RedirectToAction("Index", "Route");
        }

        // GET: Route/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateRouteService();
            var route = service.GetRouteById(id);

            var model = new RouteEdit()
            {
                RouteId = id,
                Name = route.Name,
                Description = route.Description,
                Grade = route.Grade,
                IsOnWall = route.IsOnWall
            };

            return View(model);
        }

        // POST: Route/Edit/{id}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, RouteEdit model)
        {

            if (!ModelState.IsValid) return View(model);

            if (model.RouteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateRouteService();

            if (service.UpdateRoute(model))
            {
                TempData["SaveResult"] = "Your route was updated.";
                return RedirectToAction("Index", "Route");
            }

            ModelState.AddModelError("", "Your route could not be updated.");
            return View(model);
        }
    }
}