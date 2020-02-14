using Xunit;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model.Options;
using TinyCrmConsole.Services;
using TinyCrmConsole.Interfaces;
using TinyCrmConsole.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model;
using System;
using TinyCrmConsole.Model;

namespace TinyCrmTests
{
    public class ProductServiceTests:
                            IClassFixture<TinyCrmFixture>
        

    {
        private TinyCrmDbContext context_;

        private IProductService products;

        public ProductServiceTests(TinyCrmFixture fixture) 
        {
            context_ = fixture.Context;
            products = fixture.Product;
        }

        [Fact]
        public void AddProduct_Success()
        {
            var options = new CreatingProductOptions()
            {
                Name = "upologistis",
                Description = "epitrapezios upologistis",
                Price = 500,
                Category = ProductCategory.Computers
            };

            var result = products.CreateProduct(options);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(options.Name, result.Data.Name);
            
            Assert.Equal(options.Category, result.Data.Category);

            Assert.NotEqual(default(Guid), result.Data.Id);
        }

        [Fact]
        public void AddProduct_Fail_Null_Validation()
        {
            
            var options = new CreatingProductOptions() { };

            var product = products.CreateProduct(options);
            
            Assert.Null(product.Data);
            Assert.Equal(StatusCode.BadRequest, product.Error);

        }

        [Fact]
        public void AddProduct_InValid_Name()
        {
            var options = new CreatingProductOptions()
            {
                Name = null,
                Price = 45,
                Category = ProductCategory.Smartphones,
                InStock = 10
            };

            var product = products.CreateProduct(options);

            Assert.Null(product.Data);
            Assert.Equal(StatusCode.BadRequest, product.Error);

            options = new CreatingProductOptions()
            {
                Name = "   ",
                Price = 45,
                Category = ProductCategory.Smartphones,
                InStock = 10
            };

            product = products.CreateProduct(options);

            Assert.Null(product.Data);
            Assert.Equal(StatusCode.BadRequest, product.Error);
        }

        [Fact]
        public void AddProduct_InValid_Price()
        {
            var options = new CreatingProductOptions()
            {
                Name = "Laptop",
                Price = -2,
                Category = ProductCategory.Computers
            };

            var product = products.CreateProduct(options);

            Assert.Null(product.Data);
            Assert.Equal(StatusCode.BadRequest, product.Error);
        }
        
        [Fact]
        public void AddProduct_InValid_Caterory()
        {
            var options = new CreatingProductOptions()
            {
                Name = "Laptop",
                Price = 25,
                Category = ProductCategory.Invalid
            };
            var product = products.CreateProduct(options);

            Assert.Null(product.Data);
            Assert.Equal(StatusCode.BadRequest, product.Error);
            
        }
    }
}

