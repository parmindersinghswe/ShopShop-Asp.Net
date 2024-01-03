using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopShop.Models.Models
{
    public class Categories
    {
        public static List<string> GetNames()
        {
            List<string> CategoriesList;
            using (var context = new ShoppingCartEntities())
            {
                CategoriesList = context.ProductCategories.ToList().Select(c => c.Name).ToList();
            }
            return CategoriesList;
        }
        public static List<ProductCategory> GetCategories()
        {
            List<ProductCategory> CategoriesList;
            using (var context = new ShoppingCartEntities())
            {
                CategoriesList = context.ProductCategories.Include("Products").ToList();
            }
            return CategoriesList;
        }
        public static Guid GetId(string CategoryName)
        {
            Guid id = Guid.NewGuid();
            using (var context = new ShoppingCartEntities())
            {
                id = (from cat in context.ProductCategories.ToList() where (cat.Name.Equals(CategoryName)) select cat.Id).ToList().FirstOrDefault();
            }
            return id;
        }
        public static List<ProxyCategory> CategoriesIncludingProducts()
        {
            List<ProxyCategory> CategoriesList;
            using (var context = new ShoppingCartEntities())
            {
                CategoriesList = (from category in context.ProductCategories
                      select (
                      new ProxyCategory()
                      {
                          Id = category.Id,
                          Name = category.Name,
                          Products = ((from product in context.Products
                                       where (product.CategoryId == category.Id)
                                       select (
                                       new ProxyProduct()
                                       {
                                           Id = product.Id,
                                           CategoryId = product.CategoryId,
                                           Name = product.Name,
                                           Price = product.Price,
                                           TotalNumberOfProducts = product.TotalNumberOfProducts
                                       }
                                       )).ToList()
                                    )
                      }
                    )
                    ).ToList();
            }
            return CategoriesList;
        }
    }
}