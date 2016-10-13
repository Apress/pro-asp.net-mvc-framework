using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace DomainModel.Entities
{
[Table(Name = "Products")]
public class Product : IDataErrorInfo
{
    [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
    public int ProductID { get; set; }

    [Column] public string Name { get; set; }
    [Column] public string Description { get; set; }
    [Column] public decimal Price { get; set; }
    [Column] public string Category { get; set; }
    [Column] public byte[] ImageData { get; set; }
    [Column] public string ImageMimeType { get; set; }

    public string this[string propName]
    {
        get {
            if ((propName == "Name") && string.IsNullOrEmpty(Name))
                return "Please enter a product name";
            if ((propName == "Description") && string.IsNullOrEmpty(Description))
                return "Please enter a description";
            if ((propName == "Price") && (Price < 0))
                return "Price must not be negative";
            if ((propName == "Category") && string.IsNullOrEmpty(Category))
                return "Please specify a category";
            return null;
        }
    }

    public string Error { get { return null; } } // Not required
}
}
