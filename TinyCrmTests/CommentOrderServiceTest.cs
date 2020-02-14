//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.EntityFrameworkCore;
//using TinyCrm.Core.Data;
//using TinyCrm.Core.Model;
//using TinyCrm.Core.Services;
//using TinyCrmConsole.Interfaces;
//using TinyCrmConsole.Model;
//using TinyCrmConsole.Model.Options;
//using Xunit;

//namespace TinyCrmTests
//{
//    public class CommentOrderServiceTest : IClassFixture<TinyCrmFixture>

//    {
//        private TinyCrmDbContext context_;
//        private ICustomerService customer_;
//        private IProductService products_;


//        public OrderServiceTest(TinyCrmFixture fixture)
//        {
//            context_ = fixture.Context;
//            customer_ = fixture.Customer;
//            products_ = fixture.Product;

//        }

//        //[Fact]
//        //public void CreateNewOrder()
//        //{
//        //    //step 1 :create product
//        //    var poptions = new CreatingProduct()
//        //    {
//        //        Name = "Product 1",
//        //        Price = 155.33M,
//        //        Category = ProductCategory.Computers
//        //    };

//        //    var presult = products_.CreateProduct(poptions);

//        //    Assert.Equal(StatusCode.Success, presult.Error);

//        //     poptions = new CreatingProduct()
//        //    {
//        //        Name = "Product 2",
//        //        Price = 155.33M,
//        //        Category = ProductCategory.Computers
//        //    };

//        //    var presult1 = products_.CreateProduct(poptions);

//        //    Assert.Equal(StatusCode.Success, presult1.Error);

//        //    //step 2 create customer
//        //    var options = new CreatingCustomerOptions()
//        //    {
//        //        Email = "kkp@gmail.com",
//        //        Firstname = "ntina",
//        //        VatNumber = "010101010"
//        //    };

//        //    Customer customer = customer_.CreateCustomer(options);

//        //    Assert.NotNull(customer);
//        //    //step 3 create order

//        //    var order = new Order()
//        //    {
//        //        DeliveryAddress = "Patra",
//        //        Status = OrderStatus.Pending,
//        //        Created = DateTimeOffset.Now
//        //    };

//        //    //step 4 add products

//        //    order.OrderProducts.Add(
//        //        new OrderProduct() { Product = presult.Data });
//        //    order.OrderProducts.Add(
//        //        new OrderProduct() { Product = presult1.Data });



//        //    customer.Orders.Add(order);

//        //    context_.SaveChanges();

//        //    var dborder = context_.Set<Order>()
//        //        .SingleOrDefault(o => o.Id == order.Id);

//        //    Assert.NotNull(dborder);
//        //    Assert.Equal(order.DeliveryAddress, dborder.DeliveryAddress);

//        //}

//        [Fact]
//        public void CreateOrder()
//        {
//            // Step 1: Create products
//            var poptions = new CreatingProduct()
//            {
//                Name = "product 1",
//                Price = 155.33M,
//                Category = ProductCategory.Computers
//            };
//            var presult1 = products_.CreateProduct(poptions);
//            Assert.Equal(StatusCode.Success, presult1.Error);
//            poptions = new CreatingProduct()
//            {
//                Name = "product 2",
//                Price = 113.33M,
//                Category = ProductCategory.Computers
//            };
//            var presult2 = products_.CreateProduct(poptions);
//            Assert.Equal(StatusCode.Success, presult2.Error);
//            // Step 2: Create a new customer
//            var options = new CreatingCustomerOptions()
//            {
//                Firstname = "Dimitris",
//                VatNumber = $"11{DateTime.Now:fffffff}",
//                Email = "dd@Codehub.com",
//            };
//            var customer = customer_.CreateCustomer(options);
//            Assert.NotNull(customer);
//            // Step 3: Create the order
//            var order = new Order()
//            {
//                DeliveryAddress = "Athens",
//                Status = OrderStatus.Pending,
//                Created = DateTimeOffset.Now
//            };
//            // Step 4: Add products
//            order.OrderProducts.Add(
//                new OrderProduct()
//                {
//                    Product = presult1.Data
//                });
//            order.OrderProducts.Add(
//                ////new OrderProduct()
//                {
//                Product = presult2.Data
//                });
//            customer.Orders.Add(order);
//            context_.SaveChanges();
//            var dbOrder = context_
//                .Set<Order>()
//                .SingleOrDefault(o => o.Id == order.Id);
//            Assert.NotNull(dbOrder);
//            Assert.Equal(order.DeliveryAddress, dbOrder.DeliveryAddress);
//        }

//        [Fact]

//        public void GetOrder()
//        {
//            var orderid = Guid.Parse("");

//            var order = context_
//                    .Set<Order>()
//                    .Include(o => o.OrderProducts) //gia na kanei to join me to pinaka ton order product
//                    .SingleOrDefault(o => o.Id == orderid);





//        }
//    }
//}
