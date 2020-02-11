using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public class MyProductService : IProductService
    {
        private TinyCrmDbContext dbContext;

        public MyProductService(TinyCrmDbContext context)
        {
            dbContext = context;
        }


        public Product CreateProduct(CreatingProduct options)
        {
            if (options == null)
            {
                return null;
            }

            var product = new Product();

            if (string.IsNullOrWhiteSpace(options.Id) &&
                                    string.IsNullOrWhiteSpace(options.Name) &&
                                            options.Price < 0 &&
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

        public List<Product> SearchProducts( ProductSearchingOptions options)
        {
           

            if (options == null)
            {
                return null;
            }

             var query = dbContext.Set<Product>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Id))
            {

                query = query.Where(p => p.Id.Equals(options.Id,
                                                    StringComparison.OrdinalIgnoreCase));

            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(p => p.Name.Contains(options.Name,
                                                   StringComparison.OrdinalIgnoreCase));
            }

            if (options.MinPrice != 0)
            {
                query = query.Where(p =>
                                        p.Price >= options.MinPrice)
                                       ;
            }

            if (options.MaxPrice >= 0)
            {
                query = query.Where(p =>
                                        p.Price <= options.MaxPrice)
                                       ;
            }

            return query.ToList();

        }


        public int TotalStock()
        {
            var query = dbContext.Set<Product>().AsQueryable();
                        
            var totalStock = query.Sum(c => c.InStock );

            return totalStock;


        }
    }
}
