using ClimbingCommunity.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClimbingConnection.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var service = new ClimberService(Guid.Parse(User.Identity.GetUserId()));
                if (service.ClimberHasCreatedProfile())
                {
                    TempData["HasProfile"] = true;
                    TempData["ClimberName"] = service.GetClimberName();
                    TempData["ClimberId"] = service.GetClimberId();
                }
                else
                {
                    TempData["HasProfile"] = false;
                }
            }

            TempData.Keep();

            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            TempData.Keep();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            TempData.Keep();
            return View();
        }
    }
}