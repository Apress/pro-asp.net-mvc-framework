using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HmacDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ClaimForm", "PrizeClaim");
        }
    }
}
