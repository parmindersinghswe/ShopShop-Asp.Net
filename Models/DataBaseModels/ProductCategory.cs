using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopShop.Models.DataBaseModels
{
    public class ProductCategory
    {
        [Key]
        public Guid Id { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(255)]
        public string Name { get; set; }
        public int NumberOfProducts { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}