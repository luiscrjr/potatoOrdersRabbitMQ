using System;

namespace OrderRabbitMQ.Models
{
    public class OrderItem
    {
        public OrderItem(int id, int productId, int quantity, decimal price)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
}
