using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public class OrderServices: IOrderService
    {
        private TinyCrmDbContext dbContext_;

        public OrderServices(TinyCrmDbContext context)
        {
            dbContext_ = context;
        }

        public ApiResult<Order> CreateOrder(CreatingOrderOptions options)
        {
            var result = new ApiResult<Order>();

            ICustomerService customerservice = new CustomerService(dbContext_);

            if (options == null &&
                                    options.CustomerId == default(int))
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no new options";
                return result;
            }

            if (options.Products == null)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no products";
                return result;
            }

            var customer = customerservice.GetCustomerById(options.CustomerId);

            if (customer.Data == null)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no such customer";
                return result;
            }

            var order = new Order();
           
            order.Customer = customer.Data;
           
            order.CustomerId = options.CustomerId;

            foreach (var product in options.Products)
            {
                order.OrderProducts.Add(new OrderProduct()
                {
                    Product = product
                });
            }

            order.Created = DateTimeOffset.Now;

            if (!string.IsNullOrWhiteSpace(options.Address))
            {
                order.DeliveryAddress = options.Address;
            }
            
            if (options.OrderState != OrderStatus.InValid)
            {
                order.Status = options.OrderState;
            }

            result.Data = order;
            
            var custom = order.Customer;

            custom.Orders.Add(order);

            dbContext_.SaveChanges();

            result.Data = order;

            result.Error = StatusCode.Success;

            return result;
        }

        

        public ApiResult<List<Order>> SearchOrders(SearchingOrderOptions options)
        {
            var result = new ApiResult<List<Order>>();

            //my options are not nullable
            if ( options.CustomerId == default(int)  &&
                               options.OrderId == default(Guid) &&
                                            options.VatNumber == default(string) )
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no options ";
                return result;
            }

            var orders = dbContext_.Set<Order>().AsQueryable();

            if (options.CustomerId != default(int) )
            {
                orders = orders.Where(o => o.CustomerId == options.CustomerId);
            }

            if (options.OrderId != default(Guid))
            {
                orders = orders.Where(o => o.Id == options.OrderId);
            }

            if (!string.IsNullOrWhiteSpace(options.VatNumber))
            {
                ICustomerService customerservice = new CustomerService(dbContext_);

                var customer = customerservice.GetCustomerByVatNumber(options.VatNumber);

                orders = orders.Where(o => o.Customer == customer.Data);
            }

            result.Data = orders.ToList();

            return result;
        }
    }
}
