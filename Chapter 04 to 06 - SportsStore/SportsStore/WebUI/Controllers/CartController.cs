using System.Web.Mvc;
using DomainModel.Abstract;
using DomainModel.Entities;
using DomainModel.Services;
using System.Linq;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository productsRepository;
        private IOrderSubmitter orderSubmitter;
        public CartController(IProductsRepository productsRepository,
                              IOrderSubmitter orderSubmitter)
        {
            this.productsRepository = productsRepository;
            this.orderSubmitter = orderSubmitter;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)
        {
            Product product = productsRepository.Products
                                .FirstOrDefault(p => p.ProductID == productID);
            cart.AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

    public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnUrl)
        {
            Product product = productsRepository.Products
                                .FirstOrDefault(p => p.ProductID == productID);
            cart.RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            ViewData["CurrentCategory"] = "Cart";
            return View(cart);
        }

        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult CheckOut(Cart cart)
        {
            return View(cart.ShippingDetails);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult CheckOut(Cart cart, FormCollection form)
        {
            // Empty carts can't be checked out
            if(cart.Lines.Count == 0) {
                ModelState.AddModelError("Cart", "Sorry, your cart is empty!");
                return View();
            }

            // Invoke model binding manually
            if (TryUpdateModel(cart.ShippingDetails, form.ToValueProvider())) {
                orderSubmitter.SubmitOrder(cart);
                cart.Clear();
                return View("Completed");
            }
            else // Something was invalid
                return View();
        }
    }

}