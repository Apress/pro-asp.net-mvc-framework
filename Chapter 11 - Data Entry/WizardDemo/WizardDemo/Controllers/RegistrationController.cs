using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using WizardDemo.Models;

namespace WizardDemo.Controllers
{
    public class RegistrationController : Controller
    {
        public RegistrationData regData;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            regData = (SerializationUtils.Deserialize(Request.Form["regData"])
                       ?? TempData["regData"]
                          ?? new RegistrationData()) as RegistrationData;
            TryUpdateModel(regData);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
                TempData["regData"] = regData;
        }

        public ActionResult BasicDetails(string nextButton)
        {
            if ((nextButton != null) && ModelState.IsValid) {
                return RedirectToAction("ExtraDetails");
            }
            return View(regData);
        }


        public ActionResult ExtraDetails(string backButton, string nextButton)
        {
            if (backButton != null)
                return RedirectToAction("BasicDetails");
            else if ((nextButton != null) && ModelState.IsValid)
                return RedirectToAction("Confirm");
            else
                return View(regData);
        }

        public ActionResult Confirm(string backButton, string nextButton)
        {
            if (backButton != null)
                return RedirectToAction("ExtraDetails");
            else if (nextButton != null)
                return RedirectToAction("Complete");
            else
                return View(regData);
        }

        public ActionResult Complete()
        {
            // Todo: Save regData to database; render a "completed" view
            return Content("OK, we're done");
        }
    }
}