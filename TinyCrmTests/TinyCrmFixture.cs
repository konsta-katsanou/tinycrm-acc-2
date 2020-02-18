using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Services;

namespace TinyCrmTests
{
    public class TinyCrmFixture : IDisposable
    {
        public TinyCrmDbContext Context { get; private set; }
        public IProductService Products { get; private set; }
        public ICustomerService Customers { get; private set; }
        public IOrderService Orders { get; private set; }
        public IReportService Reports { get; private set; }
        public TinyCrmFixture()
        {
            Context = new TinyCrmDbContext();
            Products = new ProductService(Context);
            Customers = new CustomerService(Context);
            Orders = new OrderService(Context, 
                                      Customers,   
                                      Products);
            Reports = new ReportService(Context, Customers,
                                         Products,Orders);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
        
    }
}
