using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public interface IOrderService
    {
        ApiResult<Order> CreateOrder(CreateOrderOptions options);

        ApiResult<IQueryable<Order>> SearchOrders(SearchingOrderOptions options);

       
        
    }
}
