using System;

namespace TinyCrmConsole.Model.Options
{
    public class SearchingOrderOptions
    {
        public int CustomerId { get; set; }

        public Guid OrderId { get; set; }

        public string VatNumber { get; set; }
    }
}
