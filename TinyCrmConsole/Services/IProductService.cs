using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System.Collections.Generic;
using TinyCrmConsole.Model.Options;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductService
    {
        List<Product> SearchProducts(ProductSearchingOptions options);

        Product CreateProduct(CreatingProduct options);

        int TotalStock();

    }
}
