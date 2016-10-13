using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using DomainModel.Abstract;
using DomainModel.Entities;
using NUnit.Framework;
using WebUI.Controllers;

namespace Tests
{
    [TestFixture]
    public class NavControllerTests
    {
        [Test]
        public void Takes_IProductsRepository_As_Constructor_Param()
        {
            // This test "passes" if it compiles, so no Asserts are needed
            new NavController((IProductsRepository)null);
        }

        [Test]
        public void Produces_Home_Plus_NavLink_Object_For_Each_Distinct_Category()
        {
            // Arrange: Product repository with a few categories
            IQueryable<Product> products = new [] {
                new Product { Name = "A", Category = "Animal" },
                new Product { Name = "B", Category = "Vegetable" },
                new Product { Name = "C", Category = "Mineral" },
                new Product { Name = "D", Category = "Vegetable" },
                new Product { Name = "E", Category = "Animal" }
            }.AsQueryable();
            var mockProductsRepos = new Moq.Mock<IProductsRepository>();
            mockProductsRepos.Setup(x => x.Products).Returns(products);
            var controller = new NavController(mockProductsRepos.Object);

            // Act: Call the Menu() action
            ViewResult result = controller.Menu(null);

            // Assert: Check it rendered one NavLink per category
            // (in alphabetical order)
            var links = ((IEnumerable<NavLink>)result.ViewData.Model).ToList();
            Assert.IsEmpty(result.ViewName); // Should render default view
            Assert.AreEqual(4, links.Count);
            Assert.AreEqual("Home", links[0].Text);
            Assert.AreEqual("Animal", links[1].Text);
            Assert.AreEqual("Mineral", links[2].Text);
            Assert.AreEqual("Vegetable", links[3].Text);
            foreach (var link in links)
            {
                Assert.AreEqual("Products", link.RouteValues["controller"]);
                Assert.AreEqual("List", link.RouteValues["action"]);
                Assert.AreEqual(1, link.RouteValues["page"]);
                if (links.IndexOf(link) == 0) // is this the "Home" link?
                    Assert.IsNull(link.RouteValues["category"]);
                else
                    Assert.AreEqual(link.Text, link.RouteValues["category"]);
            }
        }

        [Test]
        public void Highlights_Current_Category()
        {
            // Arrange: Product repository with a couple of categories
            IQueryable<Product> products = new[] {
                new Product { Name = "A", Category = "Animal" },
                new Product { Name = "B", Category = "Vegetable" },
            }.AsQueryable();
            var mockProductsRepos = new Moq.Mock<IProductsRepository>();
            mockProductsRepos.Setup(x => x.Products).Returns(products);
            var controller = new NavController(mockProductsRepos.Object);
 

            // Act
            var result = controller.Menu("Vegetable");

            // Assert
            var highlightedLinks = ((IEnumerable<NavLink>)result.ViewData.Model)
                                     .Where(x => x.IsSelected).ToList();
            Assert.AreEqual(1, highlightedLinks.Count);
            Assert.AreEqual("Vegetable", highlightedLinks[0].Text);
        } 

    }

}