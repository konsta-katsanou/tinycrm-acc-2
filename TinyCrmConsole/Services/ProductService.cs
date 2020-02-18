using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using TinyCrmApi.Model;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;


namespace TinyCrm.Core.Services
{
    public class ProductService : IProductService
    {
        private TinyCrm.Core.Data.TinyCrmDbContext context;

        public ProductService(Data.TinyCrmDbContext dbContext)
        {
            context = dbContext;
        }

        public ApiResult<Product> AddProduct(
               CreatingProductOptions options)
        {
            if (options == null)
            {
                return new ApiResult<Product>(
                     StatusCode.BadRequest,
                     "no options given");
            }

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                return new ApiResult<Product>(
                    StatusCode.BadRequest,
                    "no name given");
            }

            if (options.Price <= 0)
            {
                return new ApiResult<Product>
                   (StatusCode.BadRequest,
                   "no vslid price");
            }

            if (options.Category ==
              ProductCategory.Invalid)
            {
                return new ApiResult<Product>(
                   StatusCode.BadRequest,
                   "no valid category");
            }
            var product = new Product()
            {
                Category = options.Category,
                Name = options.Name,
                Price = options.Price,
                Description = options.Description,
                Discount = options.Discount,
                InStock = options.InStock
                
            };

            context.Set<Product>().Add(product);
            context.SaveChanges();
            return ApiResult<Product>.CreateSuccessful(product);

        }


        public ApiResult<IQueryable<Product>> SearchProducts(
               ProductSearchingOptions options)
        {
            if (options == null)
            {
                return new ApiResult<IQueryable<Product>>(
                     StatusCode.BadRequest,
                     "no options were given");
            }

            var query = context.Set<Product>().AsQueryable();

            if (options.Id != default(Guid))
            {
                query = query.Where(p => p.Id.Equals(options.Id));
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(p => p.Name
                             .Contains(options.Name,
                              StringComparison.OrdinalIgnoreCase));
            }

            if (options.MinPrice != 0)
            {
                query = query.Where(p => p.Price >= options.MinPrice);
            }

            if (options.MaxPrice >= 0)
            {
                query = query.Where(p => p.Price <= options.MaxPrice);

            }

            query = query.Where(p => p.Category == options.Category);

            return ApiResult<IQueryable<Product>>
                           .CreateSuccessful(query);
        }

        public ApiResult<bool> UpdateProduct(Guid productId,
               UpdateProductOptions options)
        {
            if (options == null)
            {
                return new ApiResult<bool>(
                   StatusCode.BadRequest,
                  "no options given to update");
            }

            var presult = GetProductById(productId);
            if (!presult.Success)
            {
                return new ApiResult<bool>(
                   StatusCode.BadRequest,
                  "there is no such product");
            }
            var product = presult.Data;

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                product.Description = options.Description;
            }

            if (options.Price == null ||
                       options.Price <= 0)
            {
                return new ApiResult<bool>(
                   StatusCode.BadRequest,
                   "invalid price");
            } else
            {
                product.Price = options.Price.Value;
            }
        
            if (options.Discount == null ||
                        options.Discount < 0)
            {
                return new ApiResult<bool>(
                    StatusCode.BadRequest,
                    "invalid discount");
            }
            else
            {
                product.Discount = options.Discount.Value;
            }

            return ApiResult<bool>.CreateSuccessful(true);
        }


        public ApiResult<Product> GetProductById(Guid id)
        {
            var product = context
                .Set<Product>()
                .SingleOrDefault(s => s.Id == id);

            if (product == null)
            {
                return new ApiResult<Product>(
                     StatusCode.NotFound, 
                     $"Product {id} not found");
            }

            return ApiResult<Product>.CreateSuccessful(product);
        }

        public int SumOfStocks()
        {
            var sum = context.Set<Product>()
                              .AsQueryable()
                             .Sum(c => c.InStock);
            return sum;
        }
    }
}





