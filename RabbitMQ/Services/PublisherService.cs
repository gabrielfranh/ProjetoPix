using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Services.Interfaces;

namespace RabbitMQ.Services
{
    public class Publisher : InitializeRabbitMQ, IPublisher
    {
        public bool PublishMessage(object message, string exchangeName, string exchangeType, string? routingKey)
        {
            if (Channel == null)
                return false;

            Channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            Channel.BasicPublish(exchange: exchangeName,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($" [x] Sent {jsonMessage}");

            return true;
        }
    }
}