using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;

namespace TinyCrmConsole.Model.Options
{
    public class CreatingOrderOptions
    {
        public List<Product> Products { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; }

        public OrderStatus OrderState { get; set; }

        public DateTimeOffset Date { get; set; }
        
    }
}
