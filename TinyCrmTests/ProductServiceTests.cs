using Xunit;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model.Options;
using TinyCrmConsole.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model;
using System;

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
                Name = "pc",
                Description = "pc's adaptor",
                Price = 25,
                Category = ProductCategory.Computers
            };

            var product = productservice.CreateProduct(options);

            Assert.NotNull(product);


            Assert.Equal(options.Name, product.Name);
            Assert.Equal(options.Price, product.Price);
            Assert.Equal(options.Category, product.Category);

            Assert.NotEqual(default(Guid), product.Id);


        }

        [Fact]
        public void AddProduct_Fail_Null_Validation()
        {
            IProductService productservice =
                  new MyProductService(context_);


            var options = new CreatingProduct()
            { };

            var product = productservice.CreateProduct(options);

            Assert.Null(product);

        }

        [Fact]
        public void AddProduct_InValid_Name()
        {
            IProductService productservice =
                  new MyProductService(context_);


            var options = new CreatingProduct()
            {

                Name = null,
                Price = 45,
                Category = ProductCategory.Smartphones,
                InStock = 10

            };

            var product = productservice.CreateProduct(options);

            Assert.Null(product);

            options = new CreatingProduct()
            {

                Name = "   ",
                Price = 45,
                Category = ProductCategory.Smartphones,
                InStock = 10

            };

            product = productservice.CreateProduct(options);

            Assert.Null(product);

        }

        [Fact]
        public void AddProduct_InValid_Price()
        {
            IProductService productservice =
                  new MyProductService(context_);

            var options = new CreatingProduct()
            {

                Name = "Laptop",
                Price = -2,
                Category = ProductCategory.Computers
            };

            var product = productservice.CreateProduct(options);

            Assert.Null(product);
        }



        [Fact]
        public void AddProduct_InValid_Caterory()
        {
            IProductService productservice =
                  new MyProductService(context_);


            var options = new CreatingProduct()
            {

                Name = "Laptop",
                Price = 25,
                Category = ProductCategory.Invalid
            };

            var product = productservice.CreateProduct(options);

            Assert.Null(product);


        }



    }
}

