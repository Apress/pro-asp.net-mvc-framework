using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using DomainModel.Abstract;
using DomainModel.Entities;
using DomainModel.Services;
using NUnit.Framework;
using WebUI.Controllers;

namespace Tests
{
[TestFixture]
public class CartControllerTests
{
    [Test]
    public void Can_Add_Product_To_Cart()
    {
        // Arrange: Set up a mock repository with two products
        var mockProductsRepos = new Moq.Mock<IProductsRepository>();
        var products = new System.Collections.Generic.List<Product> {
            new Product { ProductID = 14, Name = "Much Ado About Nothing" },
            new Product { ProductID = 27, Name = "The Comedy of Errors" },
        };
        mockProductsRepos.Setup(x => x.Products)
                         .Returns(products.AsQueryable());
        var cart = new Cart();
        var controller = new CartController(mockProductsRepos.Object, null);

        // Act: Try adding a product to the cart
        RedirectToRouteResult result = 
            controller.AddToCart(cart, 27, "someReturnUrl");

        // Assert
        Assert.AreEqual(1, cart.Lines.Count);
        Assert.AreEqual("The Comedy of Errors", cart.Lines[0].Product.Name);
        Assert.AreEqual(1, cart.Lines[0].Quantity);

        // Check that the visitor was redirected to the cart display screen
        Assert.AreEqual("Index", result.RouteValues["action"]);
        Assert.AreEqual("someReturnUrl", result.RouteValues["returnUrl"]);
    }

    [Test]
    public void Can_Remove_Product_From_Cart()
    {
        // Arrange: Set up a mock repository with two products
        var mockProductsRepos = new Moq.Mock<IProductsRepository>();
        var products = new System.Collections.Generic.List<Product> {
            new Product { ProductID = 14, Name = "Much Ado About Nothing" },
            new Product { ProductID = 27, Name = "The Comedy of Errors" },
        };
        mockProductsRepos.Setup(x => x.Products)
                         .Returns(products.AsQueryable());
        var cart = new Cart();
        cart.AddItem(products[1], 2); // 2x Comedy of Errors
        cart.AddItem(products[0], 3); // 3x Much Ado
        var controller = new CartController(mockProductsRepos.Object, null);

        // Act: Try removing Much Ado
        RedirectToRouteResult result =
            controller.RemoveFromCart(cart, 14, "someReturnUrl");

        // Assert
        Assert.AreEqual(1, cart.Lines.Count);
        Assert.AreEqual("The Comedy of Errors", cart.Lines[0].Product.Name);
        Assert.AreEqual(2, cart.Lines[0].Quantity);

        // Check that the visitor was redirected to the cart display screen
        Assert.AreEqual("Index", result.RouteValues["action"]);
        Assert.AreEqual("someReturnUrl", result.RouteValues["returnUrl"]);
    }

    [Test]
    public void Index_Action_Renders_Default_View_With_Cart_And_ReturnUrl()
    {
        // Set up the controller
        Cart cart = new Cart();
        CartController controller = new CartController(null, null);

        // Invoke action method
        ViewResult result = controller.Index(cart, "myReturnUrl");

        // Verify results
        Assert.IsEmpty(result.ViewName); // Renders default view
        Assert.AreSame(cart, result.ViewData.Model);
        Assert.AreEqual("myReturnUrl", result.ViewData["returnUrl"]);
        Assert.AreEqual("Cart", result.ViewData["CurrentCategory"]);
    }

[Test]
public void
Submitting_Order_With_No_Lines_Displays_Default_View_With_Error()
{
    // Arrange
    CartController controller = new CartController(null, null);
    Cart cart = new Cart();
    // Act
    var result = controller.CheckOut(cart, new FormCollection());
    // Assert
    Assert.IsEmpty(result.ViewName);
    Assert.IsFalse(result.ViewData.ModelState.IsValid);
}

[Test]
public void
Submitting_Empty_Shipping_Details_Displays_Default_View_With_Error()
{
    // Arrange
    CartController controller = new CartController(null, null);
    Cart cart = new Cart();
    cart.AddItem(new Product(), 1);
    // Act
    var result = controller.CheckOut(cart, new FormCollection {
        { "Name", "" }                                               
    });
    // Assert
    Assert.IsEmpty(result.ViewName);
    Assert.IsFalse(result.ViewData.ModelState.IsValid);
}

[Test]
public void Valid_Order_Goes_To_Submitter_And_Displays_Completed_View()
{
    // Arrange
    var mockSubmitter = new Moq.Mock<IOrderSubmitter>();
    CartController controller = new CartController(null, mockSubmitter.Object);
    Cart cart = new Cart();
    cart.AddItem(new Product(), 1);
    var formData = new FormCollection {
        { "Name", "Steve" }, { "Line1", "123 My Street" },
        { "Line2", "MyArea" }, { "Line3", "" },
        { "City", "MyCity" }, { "State", "Some State" },
        { "Zip", "123ABCDEF" }, { "Country", "Far far away" },
        { "GiftWrap", bool.TrueString }
    };

    // Act
    var result = controller.CheckOut(cart, formData);

    // Assert
    Assert.AreEqual("Completed", result.ViewName);
    mockSubmitter.Verify(x => x.SubmitOrder(cart));
    Assert.AreEqual(0, cart.Lines.Count);
}

}

}