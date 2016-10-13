using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using DomainModel.Abstract;
using DomainModel.Entities;
using System.Linq;
using WebUI.Controllers;
using NUnit.Framework;

namespace Tests
{
    [NUnit.Framework.TestFixture]
    public class AdminControllerTests
    {
        // Will share this same repository across all the AdminControllerTests
        private Moq.Mock<IProductsRepository> mockRepos;

        // This method gets called before each test is run
        [SetUp]
        public void SetUp()
        {
            // Make a new mock repository with 50 products
            List<Product> allProducts = new List<Product>();
            for (int i = 1; i <= 50; i++)
                allProducts.Add(new Product { ProductID = i, Name = "Product " + i });
            mockRepos = new Moq.Mock<IProductsRepository>();
            mockRepos.Setup(x => x.Products)
                              .Returns(allProducts.AsQueryable());
        }

        [Test]
        public void Index_Action_Lists_All_Products()
        {
            // Arrange
            AdminController controller = new AdminController(mockRepos.Object);

            // Act
            ViewResult results = controller.Index();

            // Assert: Renders default view
            Assert.IsEmpty(results.ViewName);
            // Assert: Check that all the products are included
            var prodsRendered = (List<Product>)results.ViewData.Model;
            Assert.AreEqual(50, prodsRendered.Count);
            for (int i = 0; i < 50; i++)
                Assert.AreEqual("Product " + (i + 1), prodsRendered[i].Name);
        }

        [Test]
        public void Edit_Action_Renders_Default_View_With_Specified_Product()
        {
            // Arrange
            AdminController controller = new AdminController(mockRepos.Object);

            // Act
            ViewResult result = controller.Edit(17);

            // Assert: Renders default view
            Assert.IsEmpty(result.ViewName);
            Product renderedProduct = (Product)result.ViewData.Model;
            Assert.AreEqual(17, renderedProduct.ProductID);
            Assert.AreEqual("Product 17", renderedProduct.Name);
        }

[Test]
public void Edit_Action_Saves_Product_To_Repository_And_Redirects_To_Index()
{
    // Arrange
    AdminController controller = new AdminController(mockRepos.Object);

    Product newProduct = new Product();

    // Act
    var result = (RedirectToRouteResult)controller.Edit(newProduct, null);

    // Assert: Saved product to repository and redirected
    mockRepos.Verify(x => x.SaveProduct(newProduct));
    Assert.AreEqual("Index", result.RouteValues["action"]);
}

[Test]
public void Delete_Action_Deletes_Product_Then_Redirects_To_Index()
{
    // Arrange
    AdminController controller = new AdminController(mockRepos.Object);
    Product prod24 = mockRepos.Object.Products.First(p => p.ProductID == 24);

    // Act (attempt to delete product 24)
    RedirectToRouteResult result = controller.Delete(24);

    // Assert
    Assert.AreEqual("Index", result.RouteValues["action"]);
    Assert.AreEqual("Product 24 has been deleted",
                    controller.TempData["message"]);
    mockRepos.Verify(x => x.DeleteProduct(prod24));
}

    }

}