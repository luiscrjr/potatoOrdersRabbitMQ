using OrderRabbitMQ.Enuns;
using OrderRabbitMQ.Models;
using System;
using System.Collections.Generic;

namespace OrderRabbitMQ.InputModels
{
    public class AddOrderInputModel
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public EOrderStatus Status { get; set; }
    }
}
