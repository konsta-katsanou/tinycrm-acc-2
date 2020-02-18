using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;

namespace TinyCrmConsole.Model.Options
{
    public class CreateOrderOptions
    {
        public List<Guid> ProductsIds { get; set; }

        public int CustomerId { get; set; }

        public string Address { get; set; }

    }
}
