using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrmApi.Model;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrm.Core.Services
{
    
        public class CustomerService : ICustomerService

    {
        private TinyCrmDbContext dbContext;
        // private  object customerservice;

        public CustomerService(TinyCrmDbContext context)
        {
            dbContext = context;
        }

        public ApiResult<Customer> CreateCustomer(
                    CreatingCustomerOptions options)
        {
            var result = new ApiResult<Customer>();
            if (options == null)
            {
                return new ApiResult<Customer>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = $"no options were inserted"
                };
            }

            if (string.IsNullOrWhiteSpace(options.Email))
            {
                return new ApiResult<Customer>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = $"no email was inserted"
                };
            }

            if (string.IsNullOrWhiteSpace(options.VatNumber))
            {
                return new ApiResult<Customer>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = $"no vatnumber was inserted"
                };
            }
            var customer = new Customer();
            if (!customer.EmailIsValid(options.Email))
            {
                return new ApiResult<Customer>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = $"the email was invalid"
                };
            }
            if (!customer.VatNumberIsValid(options.VatNumber))
            {
                return new ApiResult<Customer>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = $"the vatnumber was invalid"
                };
            }
            customer.Email = options.Email;
            customer.VatNumber = options.VatNumber;
            customer.Age = options.Age;
            customer.Firstname = options.Firstname;
            customer.Lastname = options.Lastname;
            customer.Phone = options.Phone;
            dbContext.Set<Customer>().Add(customer);
            dbContext.SaveChanges();
            return ApiResult<Customer>.CreateSuccessful(customer);
        }

        public ApiResult<IQueryable<Customer>> SearchCustomers(
                    SearchingCustomerOptions options)
        {
            var result = new ApiResult<List<Customer>>();

            if (options == null)
            {
                return new ApiResult<IQueryable<Customer>>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = "null options"
                };
            }
            var query = dbContext.Set<Customer>().AsQueryable();

            if (options.Id != 0)
            {
                query = query.Where(c => c.Id == options.Id);
            }

            if (!string.IsNullOrWhiteSpace(options.VatNumber))
            {
                query = query.Where(c => c.VatNumber == options.VatNumber);
            }
            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                query = query.Where(c => c.Email == options.Email); ;
            }
            if (!string.IsNullOrWhiteSpace(options.FirstName))
            {
                query = query.Where(c => c.Firstname.Contains(options.FirstName)); ;
            }
            if (!string.IsNullOrWhiteSpace(options.LastName))
            {
                query = query.Where(c => c.Lastname.Contains(options.LastName)); ;
            }
            return new ApiResult<IQueryable<Customer>>()
            {
                Data = query
            };
        }

        public ApiResult<Customer> GetCustomerById(
                     int customerid)
        {
            if (customerid == 0)
            {
                return new ApiResult<Customer>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = "the id is null"
                };
            }
            var options = new SearchingCustomerOptions()
            {
                Id = customerid
            };
            var cusresult = SearchCustomers(options);
            var customer = cusresult
                          .Data
                          .SingleOrDefault();
            return ApiResult<Customer>.CreateSuccessful(customer);
        }


        public ApiResult<Customer> GetCustomerByVatNumber(
                     string vatNumber)
        {
           
            if (vatNumber == null)
            {
                return new ApiResult<Customer>()
                {
                    ErrorCode = StatusCode.BadRequest,
                    ErrorText = "no given vatnumber"
                };
            }

            var options = new SearchingCustomerOptions()
            {
                VatNumber = vatNumber
            };

            var customer = SearchCustomers(options).Data.SingleOrDefault();

            return new ApiResult<Customer>()
            {
                Data = customer

            };
        }

        public decimal TotalGross(int customerid)
        {
            var cresult = GetCustomerById(customerid);
            var orders = cresult.Data.Orders;
            if (orders == null)
            {
                cresult.Data.TotalGross = 0;
                return cresult.Data.TotalGross ;
            }
            cresult.Data.TotalGross = orders.Sum(o => o.OrderCost);
            return cresult.Data.TotalGross ;
        }
    }
}





