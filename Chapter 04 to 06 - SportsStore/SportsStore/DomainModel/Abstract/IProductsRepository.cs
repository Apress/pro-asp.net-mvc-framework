using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface IProductsRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        void DeleteProduct(Product product);
    }
}
