using System;

namespace TinyCrm.Core.Model
{
    public class Product
    { 
        public Guid  Id { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public decimal Discount { get; set; }

        public string Description { get; set; }

        public ProductCategory Category { get; set; }
        
        public int InStock { get; set; }

        public int TotalNumberSold { get; set; }

        public Product()
        {
            TotalNumberSold = 0;
        }
    }
}
