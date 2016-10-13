using System.Data.Linq.Mapping;

namespace PerformanceDemos.Models
{
    [Table(Name = "Products")]
    public class Product
    {
        [Column] public string Name { get; set; }
        [Column] public decimal Price { get; set; }
    }
}