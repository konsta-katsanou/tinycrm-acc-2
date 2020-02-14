using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Interfaces
{
    public interface ICustomerService
    {
        ApiResult<List<Customer>> SearchCustomers(SearchingCustomeroptions options);

        ApiResult<Customer> CreateCustomer(CreatingCustomerOptions options);

        ApiResult<Customer> GetCustomerById(int customerid);

        ApiResult<Customer> GetCustomerByVatNumber(string customerVatNumber);

    }
}
