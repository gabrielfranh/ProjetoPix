using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using RabbitMQService.Services.Interfaces;
using System.Threading.Channels;

namespace RabbitMQService.Services
{
    public class MessageProcessor : IMessageProcessor, IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageProcessor(string hostname, int port, string username, string password, string? virtualHost)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = hostname,
                Port = port,
                UserName = username,
                Password = password,
                VirtualHost = virtualHost
            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublishMessage(string exchange, string exchangeType, string routingKey, string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);
            _channel.ExchangeDeclare(exchange: exchange, type: exchangeType);

            _channel.BasicPublish(
                exchange: exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: body
            );
        }

        public void SubscribeToMessages(string exchange, string exchangeType, string queue, string routingKey, Func<string, Task> messageHandler)
        {
            _channel.ExchangeDeclare(exchange: exchange, type: exchangeType);
            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                  exchange: exchange,
                  routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                messageHandler(message);

                _channel.BasicAck(args.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: consumer
            );
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
