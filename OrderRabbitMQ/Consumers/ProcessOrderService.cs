using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderRabbitMQ.InputModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderRabbitMQ.Consumers
{
    public class ProcessOrderService : BackgroundService
    {
        private readonly RabbitMqConfigurate _rabbitMqConfigurate;
        private readonly IConnection _connection;
        private readonly IModel _modelChannel;

        public ProcessOrderService(IOptions<RabbitMqConfigurate> options)
        {
            _rabbitMqConfigurate = options.Value;

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfigurate.Host
            };


            _connection = factory.CreateConnection();
            _modelChannel = _connection.CreateModel();
            _modelChannel.QueueDeclare(
                queue: _rabbitMqConfigurate.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_modelChannel);

            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var getString = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<AddOrderInputModel>(getString);

                _modelChannel.BasicAck(args.DeliveryTag, false);
            };

            _modelChannel.BasicConsume(_rabbitMqConfigurate.Queue, false, consumer);
            return Task.CompletedTask;
        }
    }
}



