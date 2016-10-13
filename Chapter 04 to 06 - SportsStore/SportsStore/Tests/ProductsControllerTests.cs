using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DomainModel.Abstract;
using WebUI.Controllers;
using DomainModel.Entities;

namespace Tests
{
[TestFixture]
public class ProductsControllerTests
{
    [Test]
    public void List_Presents_Correct_Page_Of_Products()
    {
        // Arrange: 5 products in the repository
        IProductsRepository repository = MockProductsRepository(
            new Product { Name = "P1" }, new Product { Name = "P2" },
            new Product { Name = "P3" }, new Product { Name = "P4" },
            new Product { Name = "P5" }
        );
        ProductsController controller = new ProductsController(repository);
        controller.PageSize = 3; // This property doesn't yet exist, but by
                                 // accessing it, you're implicitly forming
                                 // a requirement for it to exist

        // Act: Request the second page (page size = 3)
        var result = controller.List(null, 2);

        // Assert: Check the results
        Assert.IsNotNull(result, "Didn't render view");
        var products = result.ViewData.Model as IList<Product>;
        Assert.AreEqual(2, products.Count, "Got wrong number of products");
        Assert.AreEqual(2, (int)result.ViewData["CurrentPage"], "Wrong page number");
        Assert.AreEqual(2, (int)result.ViewData["TotalPages"], "Wrong  page count");
        // Make sure the correct objects were selected
        Assert.AreEqual("P4", products[0].Name);
        Assert.AreEqual("P5", products[1].Name);
    }

    [Test]
    public void List_Includes_All_Products_When_Category_Is_Null()
    {
        // Set up scenario with two categories
        IProductsRepository repository = MockProductsRepository(
            new Product { Name = "Artemis", Category = "Greek" },
            new Product { Name = "Neptune", Category = "Roman" }
        );
        ProductsController controller = new ProductsController(repository);
        controller.PageSize = 10;

        // Request an unfiltered list
        var result = controller.List(null, 1);

        // Check that the results include both items
        Assert.IsNotNull(result, "Didn't render view");
        var products = (IList<Product>)result.ViewData.Model;
        Assert.AreEqual(2, products.Count, "Got wrong number of items");
        Assert.AreEqual("Artemis", products[0].Name);
        Assert.AreEqual("Neptune", products[1].Name);
    }

    [Test]
    public void List_Filters_By_Category_When_Requested()
    {
        // Set up scenario with two categories: Cats and Dogs
        IProductsRepository repository = MockProductsRepository(
            new Product { Name = "Snowball", Category = "Cats" },
            new Product { Name = "Rex", Category = "Dogs" },
            new Product { Name = "Catface", Category = "Cats" },
            new Product { Name = "Woofer", Category = "Dogs" },
            new Product { Name = "Chomper", Category = "Dogs" }
        );
        ProductsController controller = new ProductsController(repository);
        controller.PageSize = 10;

        // Request only the dogs
        var result = controller.List("Dogs", 1);

        // Check the results
        Assert.IsNotNull(result, "Didn't render view");
        var products = (IList<Product>)result.ViewData.Model;
        Assert.AreEqual(3, products.Count, "Got wrong number of items");
        Assert.AreEqual("Rex", products[0].Name);
        Assert.AreEqual("Woofer", products[1].Name);
        Assert.AreEqual("Chomper", products[2].Name);
        Assert.AreEqual("Dogs", result.ViewData["CurrentCategory"]);
    }

    static IProductsRepository MockProductsRepository(params Product[] prods)
    {
        // Generate an implementor of IProductsRepository at runtime using Moq
        var mockProductsRepos = new Moq.Mock<IProductsRepository>();
        mockProductsRepos.Setup(x => x.Products).Returns(prods.AsQueryable());
        return mockProductsRepos.Object;
    }
}
}
