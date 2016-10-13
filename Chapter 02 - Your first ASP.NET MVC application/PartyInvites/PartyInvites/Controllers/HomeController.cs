using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewData["greeting"] = (hour < 12 ? "Good morning" : "Good afternoon");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult RSVPForm()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult RSVPForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                guestResponse.Submit();
                return View("Thanks", guestResponse);
            }
            else // Validation error, so redisplay data entry form
                return View(); 
        }
    }
}
