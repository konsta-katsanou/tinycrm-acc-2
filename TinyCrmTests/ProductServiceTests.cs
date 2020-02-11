using Xunit;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model.Options;
using TinyCrmConsole.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model;

namespace TinyCrmTests
{
    public class ProductServiceTests
    {
        private TinyCrmDbContext context_;


        public ProductServiceTests()
        {
            context_ = new TinyCrmDbContext();
        }
        
        [Fact]
        public void AddProduct_Success()
        {
            IProductService productservice =
                    new MyProductService(context_);

            var options = new CreatingProduct()
            {
                Id = "125asd",
                Description = "pc's adaptor",
                Price = 25,
                Category = ProductCategory.Computers
            };

            var product = productservice.CreateProduct(options);

            Assert.NotNull(product);
            Assert.Equal(options.Id, product.Id);
            Assert.Equal(options.Name, product.Name);
            Assert.Equal(options.Price, product.Price);
            Assert.Equal(options.Category, product.Category);
            
        }

        [Fact]
        public void AddProduct_Fail_Null_Validation()
        {
            IProductService productservice =
                    new MyProductService(context_);

            var options = new CreatingProduct()
            {
                
            };

            var product = productservice.CreateProduct(options);

            Assert.Null(product);

        }

        [Fact]
        public void AddProduct_IdInvalid_Inputs()
        {
            IProductService productservice =
                    new MyProductService(context_);
            //invalid Id
            var options = new CreatingProduct()
            {
                Id = "....",
                Name = "pc's adaptor",
                Price = 25,
                Category = ProductCategory.Computers
            };

            var product = productservice.CreateProduct(options);

            Assert.NotNull(product);

            options = new CreatingProduct()
            {
                Id = null ,
               Name = "pc's adaptor",
                Price = 25,
                Category = ProductCategory.Computers
            };

            product = productservice.CreateProduct(options);

            Assert.NotNull(product);

            //invalid Name
            options = new CreatingProduct()
            {
                Id = "123456",
                Name = "...",
                Price = 25,
                Category = ProductCategory.Computers
            };

            product = productservice.CreateProduct(options);

            Assert.NotNull(product);

            options = new CreatingProduct()
            {
                Id = "123456 ",
                Name = null,
                Price = 25,
                Category = ProductCategory.Computers
            };

            product = productservice.CreateProduct(options);

            Assert.NotNull(product);

            //invalid Price

            options = new CreatingProduct()
            {
                Id = "123456 ",
                Name = "Laptop",
                Price = -2,
                Category = ProductCategory.Computers
            };

            product = productservice.CreateProduct(options);

            Assert.NotNull(product);


            //invalid category

            options = new CreatingProduct()
            {
                Id = "123456 ",
                Name = "Laptop",
                Price = 25,
                Category = ProductCategory.Invalid
            };

            product = productservice.CreateProduct(options);

            Assert.NotNull(product);


        }

       




    }
}

