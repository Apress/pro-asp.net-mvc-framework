using System.Web.Mvc;
using System.Web.Security;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Login()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string name, string password, string returnUrl)
        {
            if (FormsAuthentication.Authenticate(name, password)) {
                // Assign a default redirection destination if not set
                returnUrl = returnUrl ?? Url.Action("Index", "Admin");
                // Grant cookie and redirect
                FormsAuthentication.SetAuthCookie(name, false);
                return Redirect(returnUrl); ;
            }
            else {
                ViewData["lastLoginFailed"] = true;
                return View();
            }
        }
    }
}