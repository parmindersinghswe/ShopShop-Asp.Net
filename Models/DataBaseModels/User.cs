using ShopShop.Models.DataBaseModels.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopShop.Models.DataBaseModels
{
    public class User
    {
        [Key]
        [ForeignKey("aspnet_Users")]
        public Guid Id { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}