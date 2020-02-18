using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core;
using TinyCrm.Core.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public interface IReportService
    {

        List<Product> Top10ProductsSold();

        List<Customer> Top10CustomerByGross();

        ApiResult<decimal> TotalSalesInAPeriod(
            SearchingOrderOptions option);
                 

        ApiResult<int> NumberOfOrdersInEachState(
                   SearchingOrderOptions option);
    }
}
