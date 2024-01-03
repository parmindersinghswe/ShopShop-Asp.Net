using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopShop.Models.Models
{
    public class ProxyCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProxyProduct> Products { get; set; }
    }
}