using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ShopShop.Models.Models
{
    public class ProxyProduct
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TotalNumberOfProducts { get; set; }
        public int Qty { get; set; } = 1;
    }
}