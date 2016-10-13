using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridDemo.Models;

namespace GridDemo.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<MountainInfo> mountainData = SampleData.SevenSummits;

        public ActionResult Index()
        {
            return RedirectToAction("Summits");
        }

        private const int PageSize = 3;

        public ActionResult Summits(int? page)
        {
            ViewData["currentPage"] = page ?? 1;
            ViewData["totalPages"] = (int)Math.Ceiling(1.0 * mountainData.Count / PageSize);
            var items = mountainData.Skip(((page ?? 1) - 1) * PageSize).Take(PageSize);

            if (Request.IsAjaxRequest())
                return View("SummitsGrid", items);  // HTML fragment
            else
                return View(items);                 // Complete HTML page
        }


        public string DeleteItem(string item)
        {
            return "OK, I'm deleting " + HttpUtility.HtmlEncode(item);
        }
    }


}