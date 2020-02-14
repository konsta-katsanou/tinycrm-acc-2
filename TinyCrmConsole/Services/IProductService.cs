using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System.Collections.Generic;
using TinyCrmConsole.Model.Options;
using TinyCrmConsole.Model;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductService
    {
        ApiResult<List<Product>> SearchProducts(ProductSearchingOptions options);

        ApiResult<Product> CreateProduct(CreatingProductOptions options);

        int TotalStock();

    }
}
