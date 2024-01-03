using ShopShop.Models.DataBaseModels;
using System;

namespace ShopShop.Models.Models
{
    public class AddCategories
    {
        public static bool Add(DataBaseModels.ProductCategory Category)
        {
            Category.Name = Category.Name.ToUpper();
            using (var context = new ShoppingCartEntities())
            {
                Category.Id = Guid.NewGuid();
                Category.NumberOfProducts = 0;
                context.ProductCategories.Add(Category);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception) { return false; }
            }
            return true;
        }
    }
}