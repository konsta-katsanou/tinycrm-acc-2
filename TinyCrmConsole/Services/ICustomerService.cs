using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> SearchCustomers(SearchingCustomeroptions options);

        Customer CreateCustomer(CreatingCustomerOptions options);

        Customer GetCustomerById(int customerid);

        Customer GetCustomerByVatNumber(string customerVatNumber);

    }
}
