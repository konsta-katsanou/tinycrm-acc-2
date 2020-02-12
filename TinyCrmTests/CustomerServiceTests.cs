using Xunit;
using TinyCrm.Core.Data;
using TinyCrmConsole.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;

namespace TinyCrmTests
{
    public class CustomerServiceTests
    {
        private TinyCrmDbContext context_;

        
        public CustomerServiceTests()
        {
            context_ = new TinyCrmDbContext();
        }


        [Fact]
        public void CreateCustomer_Success()
        {
            ICustomerService customerservice =
                    new CustomerService(context_);

            var options = new CreatingCustomerOptions()
            { 
                
                Email = "ddw@gmail.com",
                Firstname = "Dimitris",
                VatNumber = "123456889"
            };

            var customer = customerservice.CreateCustomer(options);

            Assert.NotNull(customer);

            Assert.Equal(options.Email, customer.Email);

            Assert.Equal(options.VatNumber, customer.VatNumber);

            Assert.Equal(options.Firstname, customer.Firstname);

            //elegxoume kai thn apothikeusi sthn basi


            var testidcustomer = customerservice.GetCustomerById(customer.Id); 
            
            
            //mporei kapoios na eixe ksexasei na kanei SaveChanges 
            //opote etsi mporoume na elegksoume oti ginetai save
            //tautoxrona mporoyme na elegksoume kai tin nea synartisi getcustomerby id


            Assert.NotNull(testidcustomer);
            

        }

        [Fact]
        public void CreateCustomer_Fail_Null_Validation()
        {
            ICustomerService customerservice =
                        new CustomerService(context_);

            var options = new CreatingCustomerOptions()
            {
                VatNumber = null
            };

            var customer = customerservice.CreateCustomer(options);

            Assert.Null(customer);// elegxo an den mporei na ginei eisagogi neou pelati


            options = new CreatingCustomerOptions()
            {
                VatNumber = "....."
            };

            customer = customerservice.CreateCustomer(options);

            Assert.Null(customer);

            options = new CreatingCustomerOptions()
            {
                VatNumber = "11hnvwksd25"
            };

            customer = customerservice.CreateCustomer(options);

            Assert.Null(customer);

        }

        [Fact]
        public void CreateCustomer_Email_Fail_Validation()
        {
            ICustomerService customerservice =
                        new CustomerService(context_);

            var options = new CreatingCustomerOptions()
            {
                Email = null,
                Firstname = "Dimitris",
                VatNumber = "123456789"
            };

            var customer = customerservice.CreateCustomer(options);

            Assert.Null(customer);
            
             options = new CreatingCustomerOptions()
            {
                Email = "kkgmail",
                Firstname = "Dimitris",
                VatNumber = "123456789"
            };

            customer = customerservice.CreateCustomer(options);

            Assert.Null(customer);

       

            options = new CreatingCustomerOptions()
            {
                Email = "......",
                Firstname = "Dimitris",
                VatNumber = "123456789"
            };

            customer = customerservice.CreateCustomer(options);

            Assert.Null(customer);

        }




        [Fact]
        public void Unique_VatNumber_Success()
        {
            ICustomerService customerservice =
                       new CustomerService(context_);

            var options = new CreatingCustomerOptions()
            {
                Email = "kk@gmail.com",
                Firstname = "Konstantina",
                VatNumber = "123456879"
            };

            var customer = customerservice.CreateCustomer(options);

           var testCustomer = customerservice.GetCustomerByVatNumber(options.VatNumber);

            Assert.NotNull(testCustomer);
            
        }
   }
}
