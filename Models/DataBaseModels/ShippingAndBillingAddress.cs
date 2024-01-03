using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopShop.Models.DataBaseModels
{
    public class ShippingAndBillingAddress
    {
        [Key]
        public Guid Id { get; set; }
        //[ForeignKey("Order")]
        //public Guid OrderId { get; set; }
        //public Order Order { get; set; }
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
        public Guid ShippingId { get; set; }
        public Guid BillingId { get; set; }
    }
}