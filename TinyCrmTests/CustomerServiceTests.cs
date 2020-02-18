using Xunit;
using TinyCrm.Core.Data;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;
using System;
using TinyCrm.Core.Model;

namespace TinyCrmTests
{
    public class CustomerServiceTests :
                                  IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext context_;
        private ICustomerService customers;

        public CustomerServiceTests(TinyCrmFixture fixture)
        {
            context_ = fixture.Context;
            customers = fixture.Customers;
        }


        [Fact]
        public Customer CreateCustomer_Success()
        {
            var options = new CreatingCustomerOptions()
            { 
                Email = "ddip@gmail.com",
                Firstname = "Dimitris",
                VatNumber = $"56{DateTime.Now:fffffff}"
            };

            var customer = customers.CreateCustomer(options);

            Assert.NotNull(customer);

            Assert.NotNull(customer.Data);

            Assert.Equal(options.Email, customer.Data.Email);

            Assert.Equal(options.VatNumber, customer.Data.VatNumber);

            Assert.Equal(options.Firstname, customer.Data.Firstname);
          
            //checking the savechanges command
            
            var testidcustomer = customers.GetCustomerById(customer.Data.Id);
            
            Assert.NotNull(testidcustomer);

            Assert.NotNull(testidcustomer.Data);
            return customer.Data;
            
        }

        [Fact]
        public void CreateCustomer_Fail_Null_Validation()
        {
            var options = new CreatingCustomerOptions()
            {
                VatNumber = null
            };

            var customer = customers.CreateCustomer(options);

            Assert.Null(customer.Data);
            
            options = new CreatingCustomerOptions()
            {
                VatNumber = "....."
            };

            customer = customers.CreateCustomer(options);

            Assert.Null(customer.Data);

            options = new CreatingCustomerOptions()
            {
                VatNumber = "11hnvwksd25"
            };

            customer = customers.CreateCustomer(options);

            Assert.Null(customer.Data);

        }

        [Fact]
        public void CreateCustomer_Email_Fail_Validation()
        {
            var options = new CreatingCustomerOptions()
            {
                Email = null,
                Firstname = "Dimitris",
                VatNumber = "123456789"
            };

            var customer = customers.CreateCustomer(options);

            Assert.Null(customer.Data);
            
             options = new CreatingCustomerOptions()
            {
                Email = "kkgmail",
                Firstname = "Dimitris",
                VatNumber = "123456789"
            };

            customer = customers.CreateCustomer(options);

            Assert.Null(customer.Data);
            
            options = new CreatingCustomerOptions()
            {
                Email = "......",
                Firstname = "Dimitris",
                VatNumber = "123456789"
            };

            customer = customers.CreateCustomer(options);

            Assert.Null(customer.Data);

        }
        

        [Fact]
        public void Unique_VatNumber_Success()
        {
            
            var options = new CreatingCustomerOptions()
            {
                Email = "kk@gmail.com",
                Firstname = "Konstantina",
                VatNumber = $"11{ DateTime.Now:fffffff}"
            };

          var customer = customers.CreateCustomer(options);

          var testCustomer = customers.GetCustomerByVatNumber(options.VatNumber);

            Assert.NotNull(testCustomer);
            Assert.NotNull(testCustomer.Data);

            var options1 = new CreatingCustomerOptions()
            {
                VatNumber = options.VatNumber
            };

            var testcustomer = customers.CreateCustomer(options1);

            Assert.Null(testcustomer.Data);

        }
   }
}
