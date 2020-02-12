using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TinyCrmConsole.Services
{
    public class CustomerService : ICustomerService

    {
        private TinyCrmDbContext dbContext;
        private readonly object customerservice;

        public CustomerService(TinyCrmDbContext context)
        {
            dbContext = context;
        }



        public Customer CreateCustomer(CreatingCustomerOptions options)
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

            dbContext.Set<Customer>().Add(customer);
            dbContext.SaveChanges();
            return customer;

        }

        public List<Customer> SearchCustomers(SearchingCustomeroptions options)
        {
            if (options == null)
            {
                return null;
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

            return query.ToList();
        }


        public Customer GetCustomerById(int customerid)
        {
            var options = new SearchingCustomeroptions()
            {
                Id = customerid
            };

            var customer = SearchCustomers(options).SingleOrDefault();


            return customer;

        }


        public Customer GetCustomerByVatNumber(string vatNumber)
        {
            var options = new SearchingCustomeroptions()
            {
                VatNumber = vatNumber
            };

            var customer = SearchCustomers(options).SingleOrDefault();

            return customer;
            

        }

    }
}




