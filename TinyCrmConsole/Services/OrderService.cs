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
    public class OrderService : IOrderService
    {
        private readonly TinyCrmDbContext context_;
        private readonly ICustomerService customer_;
        private readonly IProductService product_;

        public OrderService(
            TinyCrmDbContext context,
            ICustomerService customers,
            IProductService products)
        {
            context_ = context;
            customer_ = customers;
            product_ = products;
        }

        public ApiResult<Order> UpdateOrder(
            UpdateOrderOptions options)
        {
            if (options == null)
            {
                return ApiResult<Order>.CreateUnSuccessful(
                    StatusCode.BadRequest,
                    "null options");
            }
            if (options.OrderId == default(Guid) 
                  && options.State == OrderStatus.InValid)
            {
                return ApiResult<Order>.CreateUnSuccessful(
                    StatusCode.BadRequest,
                   "at least one option must be provided");
            }
            var oroptions = new SearchingOrderOptions()
            {
                OrderId = options.OrderId
            };
            var oresult = SearchOrders(oroptions).Data
                .SingleOrDefault();
            oresult.Status = options.State;
            context_.SaveChanges();
            return ApiResult<Order>.CreateSuccessful(oresult); 
        }

        public ApiResult<Order> CreateOrder(
            CreateOrderOptions createoptions)
        {
            if (createoptions == null)
            {
                return new ApiResult<Order>(
                    StatusCode.BadRequest,
                   "no options were provided");
            }
            if (createoptions.ProductsIds == null)
            {
                return new ApiResult<Order>(
                    StatusCode.BadRequest,
                    "no products were given");
            }
            var customerresult = customer_
                           .GetCustomerById(createoptions.CustomerId);
            if (!customerresult.Success)
            {
                return ApiResult<Order>.Create(customerresult);
            }
            var order = new Order();
            order.CustomerId = createoptions.CustomerId;
            foreach (var id in createoptions.ProductsIds)
            {
                var prodResult = product_
                     .GetProductById(id);
                if (!prodResult.Success)
                {
                    return ApiResult<Order>.Create(
                        prodResult);
                }
                order.OrderProducts.Add(
                    new OrderProduct()
                    {
                        Product = prodResult.Data
                    });

                prodResult.Data.TotalNumberSold +=  1;
                order.OrderCost += prodResult.Data.Price;
            }

            order.Created = DateTimeOffset.Now;
            if (!string.IsNullOrWhiteSpace(createoptions.Address))
            {
                order.DeliveryAddress = createoptions.Address;
            }
            order.Status = OrderStatus.Pending;
            customerresult.Data.TotalGross = customer_.TotalGross(order.CustomerId);
            context_.Set<Order>().Add(order);
            context_.SaveChanges();
            return ApiResult<Order>.CreateSuccessful(order);
        }

        public ApiResult<IQueryable<Order>> SearchOrders(
            SearchingOrderOptions options)
        {
            
            if (options == null)
            {
                return new ApiResult<IQueryable<Order>>(
                     StatusCode.BadRequest,
                     "null options");
            }

            var orders = context_.Set<Order>().AsQueryable();

            if (options.CustomerId != null)
            {
                orders = orders.Where(o => o.CustomerId == options.CustomerId);
            }

            if (options.OrderId != default(Guid))
            {
                orders = orders.Where(o => o.Id == options.OrderId);
            }

            if (!string.IsNullOrWhiteSpace(options.VatNumber))
            {
                var customerresult = customer_.GetCustomerByVatNumber(options.VatNumber);
                orders = orders.Where(o => o.Customer == customerresult.Data);
            }

            if (options.OrderState != 0)
            {
                orders = orders.Where(o => o.Status == options.OrderState);
            }

            return ApiResult<IQueryable<Order>>.CreateSuccessful(orders);
        }

    }
}
       