using System;
using System.Linq;
using Microsoft.Extensions.Options;
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

        { //inserting the data into the dataset

            using (var context = new TinyCrmDbContext())
            {


                var customer = new Customer()
                {
                    Lastname = "Katsanou",
                    Email = "koukourikou@gmail.com",
                    Firstname = "Paraskevi",
                    Age = 25

                };
                context.Add(customer);
                context.SaveChanges();

                ICustomerService customerservice = new CustomerService(context);

                var options = new SearchingCustomeroptions()
                {
                    Email = "koukourikou@gmail.com",
                    FirstName = "Paraskevi"
                };

                var results = customerservice.SearchCustomers(options);



                Console.WriteLine($"Found {results.Count()} customers");

                var createoptions = new CreatingCustomerOptions()
                {
                    VatNumber = "125648934",
                    Email = "constance@gmail.com",
                    Age = 32,
                    Lastname = "Papadimitriou"
                };

                var result = customerservice.CreateCustomer(createoptions);

                context.Set<Customer>().Add(result);
                context.SaveChanges();

                Customer customerbyid = customerservice.GetCustomerById(1);

                Console.WriteLine($"The name of the customer with id {customerbyid.Id} is {customerbyid.Firstname}");



                //ProductService Implementation


                //    IProductService productservice = new MyProductService(context);

                //    var pro_options = new CreatingProduct()
                //    {
                //        Id = "5963",
                //        Description = "good camera analysis",
                //        Price = 160,
                //        Name = "Samsung A7",
                //        Category = ProductCategory.Laptops


                //    };

                //    var product1 = productservice.CreateProduct(pro_options);

                //    context.Set<Product>().Add(product1);


                //    context.SaveChanges();



                //    var searchoptions = new ProductSearchingOptions()
                //    {
                //        Name = "Samsung A7"
                //    };

                //    var products = productservice.SearchProducts( searchoptions);

                //    foreach (var product in products)
                //    {
                //        Console.WriteLine($"The products have ids {product.Id}");
                //    }
                //}

                IProductService productservice = new MyProductService(context);

                context.AddRange(new Product { Name = "pc", InStock = 5, Id = "123" }, new Product
                { Name = "laptop", InStock = 10, Id = "116" });

                var total = productservice.TotalStock();



                Console.Write(total);


                Console.ReadKey();
            }
        }
        
    }
}



