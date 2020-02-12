using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;

namespace TinyCrmConsole.Model.Options
{
    public class CreatingProduct
    {
        

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }

        //optional properties

        public decimal Discount { get; set; }

       
        public string Description { get; set; }

        public int InStock { get; set; }

        


    }
}
