using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;

namespace TinyCrmConsole.Model.Options
{
    public class ProductSearchingOptions
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public ProductCategory Category { get; set; }
        
    }
}
