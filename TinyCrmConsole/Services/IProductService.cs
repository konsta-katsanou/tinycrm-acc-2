using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System.Collections.Generic;
using TinyCrmConsole.Model.Options;
using TinyCrmConsole.Model;
using System.Linq;
using System;

namespace TinyCrm.Core.Services
{
   
    public interface IProductService
    {
        ApiResult<Product> AddProduct(CreatingProductOptions options);

        ApiResult<IQueryable<Product>> SearchProducts(
               ProductSearchingOptions options);

        ApiResult<bool> UpdateProduct(Guid productId,
               UpdateProductOptions options);

        ApiResult<Product> GetProductById(Guid id);

        int SumOfStocks();


    }
}
