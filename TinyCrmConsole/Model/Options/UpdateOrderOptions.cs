using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;

namespace TinyCrmConsole.Model.Options
{
    public class UpdateOrderOptions
    {
        public Guid OrderId { get; set; }

        public OrderStatus State { get; set; }
    }
}
