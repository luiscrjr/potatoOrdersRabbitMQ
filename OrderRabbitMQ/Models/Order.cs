using OrderRabbitMQ.Enuns;
using System;
using System.Collections.Generic;

namespace OrderRabbitMQ.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public EOrderStatus Status { get; private set; }
    }
}
