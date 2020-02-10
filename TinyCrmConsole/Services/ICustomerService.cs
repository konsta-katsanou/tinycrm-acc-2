using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> SearchCustomers(List<Customer> customers, SearchingCustomeroptions options);

        Customer CreateCustomer( CreatingProductOptions options);

        Customer GetCustomerById(List<Customer> customers, int customerid);

    }
}
