using ShopShop.Models.DataBaseModels.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopShop.Models.DataBaseModels
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid AddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime ReceiveDate { get; set; }
        // [ForeignKey("UserModifying")]
        public User ModifiedBy { get; set; }
        public User CreatedBy { get; set; }
        // public virtual aspnet_Users UserModifying { get; set; }
        // [ForeignKey("UserCreating")]
        // public Guid CreatedBy { get; set; }
        // public virtual User UserCreating { get; set; }
        public ICollection<ProductsOrderList> ProductOrderLists { get; set; }
    }
}