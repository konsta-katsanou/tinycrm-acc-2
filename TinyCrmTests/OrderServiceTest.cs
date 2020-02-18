using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;
using TinyCrmConsole.Services;
using Xunit;

namespace TinyCrmTests
{
    public class OrderServiceTest : IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext context_;
        private ICustomerService customer_;
        private IProductService products_;
        private IOrderService orders_;
        private ProductServiceTests productServicetests_;
        private CustomerServiceTests customerServicetests_;
        public OrderServiceTest(
              TinyCrmFixture fixture)
        {
            productServicetests_ =
                new ProductServiceTests(fixture);
            context_ = fixture.Context;
            customer_ = fixture.Customers;
            products_ = fixture.Products;
            orders_ = fixture.Orders;
            customerServicetests_ =
                 new CustomerServiceTests(fixture);
        }

        [Fact]
        public Order CreateOrder_Succees()
        {
            var customer = customerServicetests_
                          .CreateCustomer_Success();

            var p1 = productServicetests_.AddProduct_Success();
            var p2 = productServicetests_.AddProduct_Success();

            var orderoptions = new CreateOrderOptions
            {
                CustomerId = customer.Id,
                ProductsIds = new List<Guid>() { p1.Id, p2.Id }
            };

            var oresult = orders_.CreateOrder(orderoptions);
            Assert.NotNull(oresult.Data);
            Assert.True(oresult.Success);

            var orderId = oresult.Data.Id;
            var order = context_.Set<Order>()
                .Where(o => o.Id == orderId)
                .SingleOrDefault();
            foreach (var id in orderoptions.ProductsIds)
            {
                var op = order.OrderProducts
                    .Where(p => p.ProductId == id)
                    .SingleOrDefault();
            }
            return oresult.Data;
        }

        [Fact]
        public void CreateOrder_Null_Fail()
        {
            CreateOrderOptions orderOptions = null;
            var order = orders_.CreateOrder(orderOptions);
            Assert.Null(order.Data);
        }

        [Fact]
        public void CreateOrder_NoProducts()
        {
            var custresult = customerServicetests_.CreateCustomer_Success();
            var orderOptions = new CreateOrderOptions()
            {
                CustomerId = custresult.Id
            };
            var order = orders_.CreateOrder(orderOptions);
            Assert.Null(order.Data);
        }
        
        [Fact]
        public void CreateOrder_Order_Is_Saved_To_CustomerList()
        {
            var custresult = customerServicetests_.CreateCustomer_Success();

            var p1 = productServicetests_.AddProduct_Success();
            var p2 = productServicetests_.AddProduct_Success();

            var oroptions = new CreateOrderOptions()
            {
                CustomerId = custresult.Id,
                ProductsIds = new List<Guid> { p1.Id, p2.Id }
            };
            
            var ordresult = orders_.CreateOrder(oroptions);

            var customerOrder = ordresult.Data;

            var length = customerOrder.Customer.Orders.Count;

            Assert.Equal(1, length);
        }


        [Fact]
        public void SearchOrder_Success()
        {
            var customer = customerServicetests_.CreateCustomer_Success();

            var p1 = productServicetests_.AddProduct_Success();
            var p2 = productServicetests_.AddProduct_Success();
            var orderOptions = new CreateOrderOptions()
            {
                CustomerId = customer.Id,
                ProductsIds = new List<Guid> { p1.Id, p2.Id }
            };
            
            var ordresult = orders_.CreateOrder(orderOptions);

            var order = ordresult.Data;

            Assert.NotNull(order);
            

            var option = new SearchingOrderOptions()
            {
                CustomerId = customer.Id
            };

            var res3 = orders_.SearchOrders(option);

            var testorder = res3.Data;

            Assert.NotNull(testorder);

            var length = testorder.Count();

            Assert.Equal(1, length);
            List<Order> orders = testorder.ToList();
            Assert.Equal(orders[0].Id , ordresult.Data.Id);
            
        }

        [Fact]
        public void SearchOrder_Fail()
        {
            SearchingOrderOptions opt = null;
            var result = orders_.SearchOrders(opt);
            Assert.Null(result.Data);
        }
    }
}
