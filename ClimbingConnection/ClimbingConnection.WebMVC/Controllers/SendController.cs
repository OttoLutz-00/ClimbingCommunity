using ClimbingCommunity.Models.SendModels;
using ClimbingCommunity.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClimbingConnection.WebMVC.Controllers
{
    public class SendController : Controller
    {

        // access to service layer
        private SendService CreateSendService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SendService(userId);
            return service;
        }

        // GET: Send/Create
        [Authorize]
        public ActionResult Create()
        {
            var service = new RouteService(Guid.Parse(User.Identity.GetUserId()));
            ViewBag.sendRouteList = service.GetAllRoutes();

            return View();
        }

        // POST: Send/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SendCreate model)
        {
            var routeService = new RouteService(Guid.Parse(User.Identity.GetUserId()));
            ViewBag.sendRouteList = routeService.GetAllRoutes();
            if (!ModelState.IsValid) return View(model);

            var service = CreateSendService();
            if (!service.CreateSend(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        // GET: Send/Index
        public ActionResult Index()
        {
            if (!(TempData.Peek("HasProfile") as bool? ?? false))
            {
                TempData["ClimberMessage"] = "Please complete your profile to access Gyms, Climbers, and Routes";
                return RedirectToAction("Create", "Climber");
            }

            var service = CreateSendService();
            var model = service.GetAllSends();
            return View(model);
        }



        // GET: Send/Gym/{id}
        public ActionResult Gym(int id)
        {
            if (!(TempData.Peek("HasProfile") as bool? ?? false))
            {
                TempData["ClimberMessage"] = "Please complete your profile to access Gyms, Climbers, and Routes";
                return RedirectToAction("Create", "Climber");
            }

            var service = CreateSendService();
            var model = service.GetAllSendsByGymId(id);
            return View(model);
        }


        // GET: Send/Climber
        public ActionResult Climber(int id)
        {
            if (!(TempData.Peek("HasProfile") as bool? ?? false))
            {
                TempData["ClimberMessage"] = "Please complete your profile to access Gyms, Climbers, and Routes";
                return RedirectToAction("Create", "Climber");
            }

            var service = CreateSendService();
            var model = service.GetAllSendsByClimberId(id);
            return View(model);
        }


        // GET: Send/Route
        public ActionResult Route(int id)
        {
            if (!(TempData.Peek("HasProfile") as bool? ?? false))
            {
                TempData["ClimberMessage"] = "Please complete your profile to access Gyms, Climbers, and Routes";
                return RedirectToAction("Create", "Climber");
            }

            var service = CreateSendService();
            var model = service.GetAllSendsByRouteId(id);
            return View(model);
        }
    }
}