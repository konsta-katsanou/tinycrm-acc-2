using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public class MyProductService : IProductService
    {
        

        public Product CreateProduct(CreatingProduct options)
        {
            if (options == null)
            {
                return null;
            }

            var product = new Product();

            if (string.IsNullOrWhiteSpace(options.Id) &&
                                    string.IsNullOrWhiteSpace(options.Name) &&
                                            options.Price == 0 &&
                                            options.Category == 0)
            {
                return null;
            }

            product.Category = options.Category;
            product.Id = options.Id;
            product.Name = options.Name;
            product.Price = options.Price;
            product.Description = options.Description;
            product.Discount = options.Discount;

            return product;
        }

        public List<Product> SearchProducts(List<Product> products, ProductSearchingOptions options)
        {
            if (products == null)
            {
                return null;
            }

            if (options == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(options.Id))
            {

                products = products.Where(p => p.Id.Equals(options.Id,
                                                    StringComparison.OrdinalIgnoreCase)).ToList();

            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                products = products.Where(p => p.Name.Contains(options.Name,
                                                   StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (options.MinPrice != 0)
            {
                products = products.Where(p =>
                                        p.Price >= options.MinPrice)
                                       .ToList();
            }

            if (options.MaxPrice >= 0)
            {
                products = products.Where(p =>
                                        p.Price <= options.MaxPrice)
                                       .ToList();
            }

            return products;

        }
    }
}
