using Microsoft.AspNetCore.Mvc;
using OrderRabbitMQ.InputModels;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderRabbitMQ.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "potato_orders";
        public OrdersController()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddOrderInputModel addOrderInputModel)
        {

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // Criação da fila
                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var orderInfoJson = JsonSerializer.Serialize(addOrderInputModel);
                    var bytesOrderMessage = Encoding.UTF8.GetBytes(orderInfoJson);

                    // Publicar menssagem.
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesOrderMessage
                        );
                }
            }

            return Accepted();
        }
    }
}
