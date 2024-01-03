using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopShop.Models.DataBaseModels
{
    public class CartProductsList
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }

        public virtual Cart Cart { get; set; }

    }
}