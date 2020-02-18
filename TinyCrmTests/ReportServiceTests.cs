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
    public class ReportServiceTests :
                            IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext context_;
        private IProductService products;
        private IOrderService orders;
        private ICustomerService customers;
        private IReportService report;
        private ProductServiceTests productServicetests_;
        private CustomerServiceTests customerServicetests_;
        private OrderServiceTest orderServiceTest_;
        public ReportServiceTests(TinyCrmFixture fixture)
        {
            context_ = fixture.Context;
            products = fixture.Products;
            orders = fixture.Orders;
            customers = fixture.Customers;
            report = fixture.Reports;
            orderServiceTest_ = new OrderServiceTest(fixture);
            productServicetests_ = new ProductServiceTests(fixture);
            customerServicetests_ = new CustomerServiceTests(fixture);
            
        }

        [Fact]
        public void Top10Products_Success()
        {
            var o1 = orderServiceTest_.CreateOrder_Succees();
            var o2 = orderServiceTest_.CreateOrder_Succees();
            var t10 = report.Top10ProductsSold();
            var pr = t10.Where(p => p.TotalNumberSold > 0);
            var len = pr.Count();
            Assert.NotEqual( 0 , len);
        }

        [Fact]
        public void Top10Customer_Success()
        {
            var o1 = orderServiceTest_.CreateOrder_Succees();
            var o2 = orderServiceTest_.CreateOrder_Succees();
            var t10C = report.Top10CustomerByGross();
            Assert.NotNull(t10C);
        }

        [Fact]
        public void OrderInAPeriod_Success()
        {
            var o1 = orderServiceTest_.CreateOrder_Succees();
            var o2 = orderServiceTest_.CreateOrder_Succees();
            var o3 = orderServiceTest_.CreateOrder_Succees();
            var options = new SearchingOrderOptions()
            {
                StartDate = o1.Created,
                LastDate = o3.Created
            };
            var ords = report.OrderInAPeriod(options).Data.Count();
            Assert.Equal(3, ords);
        }

        [Fact]
        public void OrdersInEachState_Success()
        {
            var options = new SearchingOrderOptions()
            {
                OrderState = OrderStatus.Cancelled
            };
            var orders = report.NumberOfOrdersInEachState(options);
            
            Assert.True( orders.Data == 0);
        }

        [Fact]
        public void OrdersInEachState_Success_Pending()
        {
            var options = new SearchingOrderOptions()
            {
                OrderState = OrderStatus.Pending
            };
            var orders = report.NumberOfOrdersInEachState(options);

            Assert.True(orders.Data != 0);
        }




    }
}
