using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopShop.Models.DataBaseModels
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("ProductCategory")]
        public Guid CategoryId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FriendlyId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TotalNumberOfProducts { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}