using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        public int PageSize = 4; // Will change this later
        private IProductsRepository productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public ViewResult List(string category, int page)
        {
            var productsInCategory = (category == null)
                ? productsRepository.Products
                : productsRepository.Products.Where(x => x.Category == category);

            int numProducts = productsInCategory.Count();
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numProducts / PageSize);
            ViewData["CurrentPage"] = page;
            ViewData["CurrentCategory"] = category;

            return View(productsInCategory
                                         .Skip((page - 1) * PageSize)
                                         .Take(PageSize)
                                         .ToList()
                      );
        }

        public FileContentResult GetImage(int ProductID)
        {
            Product product = (from p in productsRepository.Products
                               where p.ProductID == ProductID
                               select p).First();
            return File(product.ImageData, product.ImageMimeType);
        }
    }
}
