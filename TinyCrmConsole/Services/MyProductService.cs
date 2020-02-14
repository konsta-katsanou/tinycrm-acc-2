using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrmConsole.Model;
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


        public ApiResult<Product> CreateProduct(CreatingProductOptions options)
        {
            var result = new ApiResult<Product>();

            if (options == null)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "null options";
                return result;
            }

            var product = new Product();

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "null or empty name";
                return result;
            }

            if (options.Price <= 0)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "negative or zero price";
                return result;
            }

            if (options.Category == ProductCategory.Invalid)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "invalid category";
                return result;
            }

            product.Category = options.Category;
            product.Name = options.Name;
            product.Price = options.Price;
            product.Description = options.Description;
            product.Discount = options.Discount;
            product.InStock = options.InStock;

            dbContext.Set<Product>().Add(product);
            dbContext.SaveChanges();
            
            result.Data = product;
            result.Error = StatusCode.Success;
            return result;
        }



        public ApiResult<List<Product>> SearchProducts(ProductSearchingOptions options)
        {
            var result = new ApiResult<List<Product>>();

            if (options == null)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no options given";
                return result;
            }

            var query = dbContext.Set<Product>().AsQueryable();

            if (options.Id != default(Guid))
            {
                query = query.Where(p => p.Id.Equals(options.Id));
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(p => p.Name.Contains(options.Name,
                                                   StringComparison.OrdinalIgnoreCase));
            }

            if (options.MinPrice != 0)
            {
                query = query.Where(p =>
                                        p.Price >= options.MinPrice);
            }

            if (options.MaxPrice >= 0)
            {
                query = query.Where(p =>
                                        p.Price <= options.MaxPrice);

            }

            query = query.Where(p => p.Category == options.Category);

            result.Data = query.ToList();

            return result;

        }


        public int TotalStock()
        {
            var query = dbContext.Set<Product>().AsQueryable();

            var totalStock = query.Sum(c => c.InStock);

            return totalStock;


        }
    }
}
