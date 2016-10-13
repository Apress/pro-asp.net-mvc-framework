using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductsRepository productsRepository;
        public AdminController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public ViewResult Index()
        {
            return View(productsRepository.Products.ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Edit(int productId)
        {
            Product product = (from p in productsRepository.Products
                               where p.ProductID == productId
                               select p).First();
            return View(product);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid) {
                if (image != null) {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                productsRepository.SaveProduct(product);
                TempData["message"] = product.Name + " has been saved.";
                return RedirectToAction("Index");
            }
            else // Validation error, so redisplay same view
                return View(product);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public RedirectToRouteResult Delete(int productId)
        {
            Product product = (from p in productsRepository.Products
                               where p.ProductID == productId
                               select p).First();
            productsRepository.DeleteProduct(product);
            TempData["message"] = product.Name + " has been deleted";
            return RedirectToAction("Index");
        }
    }
}