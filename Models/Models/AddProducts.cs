using ShopShop.Models.DataBaseModels;
using System;
using System.Linq;

namespace ShopShop.Models.Models
{
    public class AddProducts
    {
      public static bool Add(Product product)
            {
                product.Name = product.Name.ToString().ToUpper();
                using (var context = new ShoppingCartEntities())
                {
                    product.Id = Guid.NewGuid();
                    product.TotalNumberOfProducts = product.TotalNumberOfProducts;
                    context.Products.Add(product);

                var category = (from cat in context.ProductCategories where cat.Id == product.CategoryId select cat).FirstOrDefault();
                if (category != null)
                {
                    category.NumberOfProducts += 1;
                }
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