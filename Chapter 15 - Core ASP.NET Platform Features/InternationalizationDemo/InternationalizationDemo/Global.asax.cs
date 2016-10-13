using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace InternationalizationDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Uses WebForms code to apply "auto" culture to current thread and deal with 
            // invalid culture requests automatically. Defaults to en-US when not specified.
            using(var fakePage = new Page()) {
                var ignored = fakePage.Server;     // Work around a WebForms quirk
                fakePage.Culture = "auto:en-US";   // Apply local formatting to this thread
                fakePage.UICulture = "auto:en-US"; // Apply local language to this thread
            }
        }
    }
}