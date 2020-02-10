using System;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;
using TinyCrmConsole.Services;

namespace TinyCrmConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TinyCrmDbContext context = new TinyCrmDbContext())
            {

                var customer = new Customer();
                customer.VatNumber = "123456789";
                customer.Age = 25;
                customer.Email = "kon.katsan@gmail.com";
                customer.Firstname = "Konstantina";

                var customer1 = new Customer();
                customer.VatNumber = "147523698";
                customer.Age = 15;
                customer.Email = "koukourikou@gmail.com";
                customer.Firstname = "bladimiros";



                context.Add(customer);
                context.Add(customer1);

                context.SaveChanges();

                var customers = context.Set<Customer>().ToList();




                ICustomerService customerservice = new CustomerService();

                var options = new SearchingCustomeroptions()
                {
                    FirstName = "stan",

                    Email = "kon.katsan@gmail.com"
                };

                var results = customerservice.SearchCustomers(customers, options);

                Console.WriteLine($"Found {results.Count()} customers");


                var createoptions = new CreatingProductOptions()

                {
                    Firstname = "Paraskevi",
                    Email = "papadopoulou@gmail.com",
                    VatNumber = "124256378"
                };

                var result = customerservice.CreateCustomer(createoptions);

                context.Set<Customer>().Add(result);
                context.SaveChanges();

                Customer customerbyid = customerservice.GetCustomerById(customers, 1);

                Console.WriteLine($"The name of the customer with id {customerbyid.Id} is {customerbyid.Firstname}");



                //ProductService Implementation


                IProductService productservice = new MyProductService();

                var pro_options = new CreatingProduct()
                {
                    Id = "ased452",
                    Description = "good camera analysis",
                    Price = 160,
                    Name = "Samsung A7",
                    Category = ProductCategory.Smartphones


                };

                var product1 = productservice.CreateProduct(pro_options);

                context.Set<Product>().Add(product1);

                var pro_options1 = new CreatingProduct()
                {
                    Id ="ert1234",

                    Price = 250,
                    Name = "Toshiba",
                    Category = ProductCategory.Televisions


                };
                var product2 = productservice.CreateProduct(pro_options1);

                context.Set<Product>().Add(product2);

                context.SaveChanges();

                var products = context.Set<Product>().ToList();

                var searchoptions = new ProductSearchingOptions()
                {
                    MaxPrice = 500,
                    MinPrice = 100
                };

                products = productservice.SearchProducts(products, searchoptions);
                foreach (var product in products)
                {
                    Console.WriteLine($"The products have ids {product.Id}");
                }
            }

            Console.Write("Ok");

            Console.ReadKey();
        }
    }
}



