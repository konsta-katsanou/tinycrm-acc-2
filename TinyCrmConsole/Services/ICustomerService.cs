using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Interfaces
{
    public interface ICustomerService
    {
        ApiResult<IQueryable<Customer>> SearchCustomers(SearchingCustomerOptions options);

        ApiResult<Customer> CreateCustomer(CreatingCustomerOptions options);

        ApiResult<Customer> GetCustomerById(int customerid);

        ApiResult<Customer> GetCustomerByVatNumber(string customerVatNumber);

        decimal TotalGross(int customerid);

    }
}
