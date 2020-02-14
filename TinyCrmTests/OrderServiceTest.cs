using System;
using System.Collections.Generic;
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


        public OrderServiceTest(TinyCrmFixture fixture)
        {
            context_ = fixture.Context;
            customer_ = fixture.Customer;
            products_ = fixture.Product;
            orders_ = fixture.Order;
        }

        [Fact]
        public void CreateOrder_Succees()
        {
            // Step 2: Create a new customer
            var options = new CreatingCustomerOptions()
            {
                Firstname = "Dimitris",
                VatNumber = $"11{DateTime.Now:fffffff}",
                Email = "dd@Codehub.com",
            };

            var customer = customer_.CreateCustomer(options);

            Assert.NotNull(customer.Data);

            var orderOptions = new CreatingOrderOptions();

            orderOptions.CustomerId = customer.Data.Id;

            orderOptions.Products = new List<Product>() { new Product()
            {
                Name = "product 2",
                Price = 113.33M,
                Category = ProductCategory.Computers
            }
            , new Product()
                 {
                     Name = "product 1",
                     Price = 155.33M,
                     Category = ProductCategory.Computers
                 }
            };

            orderOptions.Date = DateTimeOffset.Now;

            orderOptions.OrderState = OrderStatus.Pending;

            var order = orders_.CreateOrder(orderOptions);


            Assert.NotNull(order.Data);
            Assert.Equal(order.Data.Customer.Id, order.Data.CustomerId);
            Assert.Equal(orderOptions.Address, order.Data.DeliveryAddress);
            Assert.Equal(orderOptions.OrderState, order.Data.Status);
        }

        [Fact]
        public void CreateOrder_Null_Fail()
        {
            var orderOptions = new CreatingOrderOptions();

            var order = orders_.CreateOrder(orderOptions);

            Assert.Null(order.Data);

        }
        
        [Fact]
        public void CreateOrder_NoCustomerId()
        {
            var orderOptions = new CreatingOrderOptions();

            orderOptions.Products = new List<Product>() { new Product()
            {
                Name = "product 2",
                Price = 113.33M,
                Category = ProductCategory.Computers
            }
            , new Product()
                 {
                     Name = "product 1",
                     Price = 155.33M,
                     Category = ProductCategory.Computers
                 }

            };

            orderOptions.Date = DateTimeOffset.Now;

            orderOptions.OrderState = OrderStatus.Pending;

            var order = orders_.CreateOrder(orderOptions);

            Assert.Null(order.Data);
        }
        
        [Fact]
        public void CreateOrder_NoProducts()
        {

            var options = new CreatingCustomerOptions()
            {
                Firstname = "Dimitris",
                VatNumber = $"11{DateTime.Now:fffffff}",
                Email = "dd@Codehub.com",
            };

            var customer = customer_.CreateCustomer(options);

            var orderOptions = new CreatingOrderOptions();

            orderOptions.CustomerId = customer.Data.Id;

            orderOptions.Date = DateTimeOffset.Now;

            orderOptions.OrderState = OrderStatus.Pending;

            var order = orders_.CreateOrder(orderOptions);

            Assert.Null(order.Data);
        }

        [Fact]
        public void CreateOrder_Order_Is_Saved_To_CustomerList()
        {
            var options = new CreatingCustomerOptions()
            {
                Firstname = "kgnhgnlsfnv",
                VatNumber = $"11{DateTime.Now:fffffff}",
                Email = "dd@Cohtrhb.com"

            };

            var customer = customer_.CreateCustomer(options);

            var orderOptions = new CreatingOrderOptions();

            orderOptions.CustomerId = customer.Data.Id;

            orderOptions.Products = new List<Product>() { new Product()
            {
                Name = "product 2",
                Price = 113.33M,
                Category = ProductCategory.Computers
            }
            , new Product()
                 {
                     Name = "product 1",
                     Price = 155.33M,
                     Category = ProductCategory.Computers
                 }
            };

            orderOptions.Date = DateTimeOffset.Now;

            orderOptions.OrderState = OrderStatus.Pending;

            var order = orders_.CreateOrder(orderOptions);

            var customerOrder = order.Data;

            var length = customerOrder.Customer.Orders.Count;

            Assert.Equal(1, length);


        }

        [Fact]
        public void SearchOrder_Success()
        {
            //create new customer
            var options = new CreatingCustomerOptions()
            {
                Firstname = "Paraskevi",
                VatNumber = $"11{DateTime.Now:fffffff}",
                Email = "kkp@Codehub.com",
            };

            var res1 = customer_.CreateCustomer(options);
            var customer = res1.Data;
            Assert.NotNull(customer);

            //new order for this customer
            var orderOptions = new CreatingOrderOptions();

            orderOptions.CustomerId = customer.Id;

            orderOptions.Products = new List<Product>() { new Product()
            {
                Name = "product 3",
                Price = 113.33M,
                Category = ProductCategory.Computers
            }
            , new Product()
                 {
                     Name = "product 4",
                     Price = 155.33M,
                     Category = ProductCategory.Computers
                 }

            };

            var res2 = orders_.CreateOrder(orderOptions);

            var order = res2.Data;

            Assert.NotNull(order);

            //search the above order for the customer

            var option = new SearchingOrderOptions()
            {
                CustomerId = customer.Id
            };

            var res3 = orders_.SearchOrders(option);

            var testorder = res3.Data;

            Assert.NotNull(testorder);

            var length = testorder.Count;

            Assert.Equal(1, length);

            Assert.Equal(testorder[0].Id , order.Id);
        }
        
        [Fact]
        public void SearchOrder_Fail()
        {
            var opt = new SearchingOrderOptions()
            {
                CustomerId = default(int),
                 OrderId = default(Guid),
                 VatNumber = default(string)
             };

            var result = orders_.SearchOrders(opt);

            Assert.Null(result.Data);
            
        }

    }
}
