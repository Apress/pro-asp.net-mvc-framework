using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomainModel.Entities;

namespace WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(null,
                "", // Only matches the empty URL (i.e. ~/)
                new { controller = "Products", action = "List",
                      category = (string)null, page = 1 } 
            );

            routes.MapRoute(null,
                "Page{page}", // Matches ~/Page2, ~/Page123, but not ~/PageXYZ
                new { controller = "Products", action = "List", category = (string)null },
                new { page = @"\d+" } // Constraints: page must be numerical
            ); 
            
            routes.MapRoute(null,
                "{category}", // Matches ~/Football or ~/AnythingWithNoSlash
                new { controller = "Products", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "{category}/Page{page}", // Matches ~/Football/Page567
                new { controller = "Products", action = "List" }, // Defaults
                new { page = @"\d+" } // Constraints: page must be numerical
            );

            routes.MapRoute(null, "{controller}/{action}");
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}