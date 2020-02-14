using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model
{
    public class Order
    {
        public Guid Id { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTimeOffset Created { get; set; }

        public OrderStatus Status { get; set; }

        //navigation properties for customer
        public int CustomerId { get; set;}

        public Customer Customer { get; set; }


        public List<OrderProduct> OrderProducts { get; set; }

        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }

    }
}
