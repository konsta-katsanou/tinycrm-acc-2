using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Services;

namespace TinyCrmTests
{
    public class TinyCrmFixture
    {
        public TinyCrmDbContext Context { get; private set; }

        public IProductService Product { get; private set; }

        public ICustomerService Customer { get; private set; }

        public IOrderService Order { get; private set; }


        public TinyCrmFixture()
        {
            Context = new TinyCrmDbContext();

            Product = new MyProductService(Context);

            Customer = new CustomerService(Context);

            Order = new OrderServices(Context);

        }
        
    }
}
