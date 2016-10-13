using System.Data;
using System.Data.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PerformanceDemos.Models;
using System.Linq;

namespace PerformanceDemos.Controllers
{
    public class LinqToSqlMonitoringDemoController : Controller
    {
        /// For this demo to work, you'll need to edit the following connection 
        /// string appropriately. Point it to the SportsStore database.
        private const string connectionString = @"data source=.\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=SportsStore;";

        public ViewResult Index()
        {
            var dc = new DataContext(connectionString);
            dc.Log = (StringWriter) HttpContext.Items["linqToSqlLog"];
            var productsTable = dc.GetTable<Product>();

            ViewData["numProducts"] = productsTable.Count();
            return View(productsTable);
        }
    }
}