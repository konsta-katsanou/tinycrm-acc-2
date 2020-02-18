using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrmApi.Model;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public class ReportService : IReportService
    {
        private readonly TinyCrmDbContext context_;
        private readonly ICustomerService customer_;
        private readonly IProductService product_;
        private readonly IOrderService order_;

        public ReportService(
            TinyCrmDbContext context,
            ICustomerService customer,
            IProductService product,
            IOrderService order )
        {
            context_ = context;
            customer_ = customer;
            product_ = product;
            order_ = order;
        }

        public List<Product> Top10ProductsSold( )
        {
            var query = context_.Set<Product>()
                                .AsQueryable();
            var top10 = query.OrderByDescending(p => p.TotalNumberSold)
                        .Take(10);
            return query.ToList();
        }
        
        public List<Customer> Top10CustomerByGross()
        {
            var query = context_.Set<Customer>()
                       .AsQueryable();
            var top10 = query
                 .OrderByDescending(c => c.TotalGross )
                 .Take(10);
            return top10.ToList();
        }
        
        public ApiResult<decimal> TotalSalesInAPeriod(
           SearchingOrderOptions options)
        {
            if (options == null)
            {
                return ApiResult<decimal>.CreateUnSuccessful(
                     StatusCode.BadRequest,
                     "null options");
            }
            if (options.StartDate == null && options.LastDate == null)
            {
                return ApiResult<decimal>.CreateUnSuccessful(
                    StatusCode.BadRequest,
                    " at least one date must be provided");
            }
            var orders = context_.Set<Order>().AsQueryable();
            if (options.StartDate != null)
            {
                orders = orders.Where(o => o.Created >= options.StartDate);
            }
            if (options.LastDate!= null)
            {
                orders = orders.Where(o => o.Created <= options.LastDate);
            }

            var totalsales = orders.Sum(o => o.OrderCost);

            return ApiResult<decimal>
                           .CreateSuccessful(totalsales);
        }

        public ApiResult<int> NumberOfOrdersInEachState(
                 SearchingOrderOptions option)
        {
            if (option == null)
            {
                return new ApiResult<int>(
                        StatusCode.BadRequest,
                        "null options");
            }
            var state = option.OrderState;
            var orresult = order_.SearchOrders(option);
            var order= orresult.Data;
            var numorders= order.Count();
            return ApiResult<int>.CreateSuccessful(
                 numorders);
        }
    }
}
