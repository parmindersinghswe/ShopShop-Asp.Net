using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopShop.Models.DataBaseModels
{
    public class ProductsOrderList
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public int ProductQty { get; set; }
        public virtual Product Product { get; set; }
    }
}