using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PerformanceDemos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult TracingDemo()
        {
            return View();
        }

        [EnableCompression]
        public void CompressionDemo()
        {
            // Output a lot of data
            for (int i = 0; i < 10000; i++)
                Response.Write("Hello " + i + "<br/>");
        }

        public ViewResult PerformanceMonitorDemo()
        {
            // Do something that takes a while
            for (var i = 0; i < 100; i++)
                new StreamReader(HostingEnvironment.MapPath("~/Global.asax")).ReadToEnd();

            return View();
        }
    }
}
