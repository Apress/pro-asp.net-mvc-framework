using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GridDemo.Models;
using System.Linq;

namespace GridDemo.Controllers
{
    public class SortableMountainsController : Controller
    {
        private static readonly List<MountainInfo> mountainData = SampleData.SevenSummits;

        public ViewResult Index()
        {
            // Display them in a random order
            var rng = new Random();
            return View(mountainData.OrderBy(x => rng.Next(int.MaxValue)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string Index(string chosenOrder)
        {
            string correctOrder = mountainData
                .OrderByDescending(x => x.HeightInMeters)
                .Aggregate("", (str, mountain) => str + mountain.Name + "|");
            return correctOrder == chosenOrder ? "You're right!" : "Sorry, you're wrong.";         
        }
    }
}