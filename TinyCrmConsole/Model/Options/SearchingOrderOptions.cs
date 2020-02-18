using System;
using TinyCrm.Core.Model;

namespace TinyCrmConsole.Model.Options
{
    public class SearchingOrderOptions
    {
        public int? CustomerId { get; set; }

        public Guid OrderId { get; set; }

        public string VatNumber { get; set; }

        public OrderStatus OrderState { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset LastDate { get; set; }
    }
}

