using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public interface IOrderService
    {
        ApiResult<Order> CreateOrder(CreatingOrderOptions options);
        
        ApiResult<List<Order>> SearchOrders(SearchingOrderOptions options);
    }
}
