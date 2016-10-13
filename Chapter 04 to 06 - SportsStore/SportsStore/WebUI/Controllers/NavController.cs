using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductsRepository productsRepository;
        public NavController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public ViewResult Menu(string highlightCategory)
        {
            // Put a Home link at the top
            List<NavLink> navLinks = new List<NavLink>();
            navLinks.Add(new CategoryLink(null) {
                IsSelected = (highlightCategory == null)
            });

            // Add a link for each distinct category
            var categories = productsRepository.Products.Select(x => x.Category);
            foreach (string category in categories.Distinct().OrderBy(x => x))
                navLinks.Add(new CategoryLink(category) {
                    IsSelected = (category == highlightCategory)
                });

            return View(navLinks);
        }


    }

    public class NavLink // Represents a link to any arbitrary route entry
    {
        public string Text { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public bool IsSelected { get; set; }
    }
    public class CategoryLink : NavLink // Specifically a link to a product category
    {
        public CategoryLink(string category)
        {
            Text = category ?? "Home";
            RouteValues = new RouteValueDictionary(new
            {
                controller = "Products",
                action = "List",
                category = category,
                page = 1
            });
        }
    }
}