using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model;
using TinyCrmConsole.Model.Options;

namespace TinyCrmConsole.Services
{
    public class CustomerService : ICustomerService

    {
        private TinyCrmDbContext dbContext;
       // private  object customerservice;

        public CustomerService(TinyCrmDbContext context)
        {
            dbContext = context;
        }
        
        public ApiResult<Customer> CreateCustomer(CreatingCustomerOptions options)
        {
            var result = new ApiResult<Customer>();

            if (options == null) //opote perimeono parametro pou mporei na parei default timi tote prepei na elegxo an einai null
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no options";
                return result;
            }

            // otan exoume int tote h default timi einai to miden kai ara den xreiazetai na elegxo gia null

            if (string.IsNullOrWhiteSpace(options.Email))
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no email";
                return result;
            }

            if ( string.IsNullOrWhiteSpace(options.VatNumber))
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no vatnumber";
                return result;
            }
            
            var customer = new Customer();

            if (!customer.EmailIsValid(options.Email))
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "invalid  email";
                return result;
            }

            if (!customer.VatNumberIsValid(options.VatNumber))
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "invalid vatnumber";
                return result;
            }
            
            customer.Email = options.Email;

            customer.VatNumber = options.VatNumber;

            customer.Age = options.Age;

            customer.Firstname = options.Firstname;

            customer.Lastname = options.Lastname;

            customer.Phone = options.Phone;
            
            dbContext.Set<Customer>().Add(customer);

            dbContext.SaveChanges();

            result.Data = customer;

            result.Error = StatusCode.Success;

            return result;

        }

        public ApiResult<List<Customer>> SearchCustomers(SearchingCustomeroptions options)
        {
            var result = new ApiResult<List<Customer>>();

            if (options == null)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "no options";
                return result;
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

            result.Data = query.ToList();
            return result;
        }


        public ApiResult<Customer> GetCustomerById(int customerid)
        {
            var result = new ApiResult<Customer>();

            if (customerid == 0)
            {
                result.ErrorText = "no customerid";
                result.Error = StatusCode.BadRequest;
                return result;
            }
           
           var options = new SearchingCustomeroptions()
            {
                Id = customerid
            };

            var customer = SearchCustomers(options);

            if (customer.Data.Count >1)
            {
                result.Error = StatusCode.InterServError;
                result.ErrorText = "more than one customers";
                return result;
            }

            if (customer.Data.Count == 0 )
            {
                result.Error = StatusCode.NotFound;
                result.ErrorText = "no such customer";
                return result;
            }

                result.Data = customer.Data[0];
                result.Error = StatusCode.Success;
                return result;
        }


        public ApiResult<Customer> GetCustomerByVatNumber(string vatNumber)
        {
            var result = new ApiResult<Customer>();

            if (vatNumber== null)
            {
                result.Error = StatusCode.BadRequest;
                result.ErrorText = "null vatNumber";
                return result;
            }

            var options = new SearchingCustomeroptions()
            {
                VatNumber = vatNumber
            };

            var customer = SearchCustomers(options); 

            if (customer.Data.Count > 1)
            {
                result.Error = StatusCode.InterServError;
                result.ErrorText = "more than one customers";
                return result;
            }

            if (customer.Data.Count == 0)
            {
                result.Error = StatusCode.NotFound;
                result.ErrorText = "no customer with this vatnumber";
                return result;
            }

            result.Data = customer.Data[0];
            result.Error = StatusCode.Success;
            return result;
        }

    }
}




