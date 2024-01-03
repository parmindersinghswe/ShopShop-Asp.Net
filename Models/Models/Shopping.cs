using ShopShop.Models.DataBaseModels;
using System;

namespace ShopShop.Models.Models
{
    public class Shopping
    {
        public static Guid Shop(ShoppingInformation ShoppingInfo)
        {
            if (Payment.Pay(ShoppingInfo.CreditCard, ShoppingInfo.ProductId, ShoppingInfo.Qty))
            {
                Guid NullId = new Guid("00000000-0000-0000-0000-000000000000");
                if (ShoppingInfo.ProductId != NullId)
                {
                    ProductsOrderList productorderlist = new ProductsOrderList()
                    {
                        ProductId = ShoppingInfo.ProductId,
                        ProductQty=ShoppingInfo.Qty
                    };
                    return PlaceOrder.OrderProduct(productorderlist);
                }
                else
                {
                    return PlaceOrder.OrderCartProducts();
                }
            }
            else
                return new Guid();
        }
    }
   public class ShoppingInformation
    {
        public CreditCard CreditCard { get; set; }
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
    }
}