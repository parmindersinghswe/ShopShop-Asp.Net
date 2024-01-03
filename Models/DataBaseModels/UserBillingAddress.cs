using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopShop.Models.DataBaseModels
{
    public class UserBillingAddress
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string City { get; set; }
        public string LocalityOrStreet { get; set; }
        public string FlatNoBuildingName { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string LandMark { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateMobileNumber { get; set; }
    }
}