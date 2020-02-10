using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TinyCrmConsole.Services
{
    public class CustomerService : ICustomerService

    {
        
        public Customer CreateCustomer( CreatingProductOptions options)
        {
            
            if (options == null) //opote perimeono parametro pou mporei na parei default timi tote prepei na elegxo an einai null
            {
                return null;

            }

            // otan exoume int tote h default timi einai to miden kai ara den xreiazetai na elegxo gia null
            if (string.IsNullOrWhiteSpace(options.Email)
                            || string.IsNullOrWhiteSpace(options.VatNumber))
            {
                return null;
            }

            
            var customer = new Customer();

            if (!customer.EmailIsValid(options.Email))
            {
                return null;
            }

            if (!customer.VatNumberIsValid(options.VatNumber))
            {
                return null;
            }
            
                customer.Email = options.Email;

                customer.VatNumber = options.VatNumber;

                customer.Age = options.Age;

                customer.Firstname = options.Firstname;

                customer.Lastname = options.Lastname;

                customer.Phone = options.Phone;
            
            return customer;

        }
        
        public List<Customer> SearchCustomers(List<Customer> customers, SearchingCustomeroptions options)
        {
            if (options == null)
            {
                return null;
            }

            if (customers == null)
            {
                return null;
            }

            if (options.Id != 0)

            {
                customers = customers.Where(c => c.Id == options.Id).ToList();
            }

            if (!string.IsNullOrWhiteSpace(options.VatNumber))

            {
                customers = customers.Where(c => c.VatNumber == options.VatNumber).ToList();
            }


            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                customers = customers.Where(c => c.Email == options.Email).ToList();
            }


            if (!string.IsNullOrWhiteSpace(options.FirstName))
            {
                customers = customers.Where(c => c.Firstname.Contains(options.FirstName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(options.LastName))
            {
                customers = customers.Where(c => c.Lastname.Contains(options.LastName)).ToList();
            }

            return customers;
        }


        public Customer GetCustomerById(List<Customer> customers, int customerid )
        {
            var options = new SearchingCustomeroptions()
            {
                Id = customerid
            };

            var customer = SearchCustomers(customers, options).SingleOrDefault();


            return customer ;

        }

    }
}




