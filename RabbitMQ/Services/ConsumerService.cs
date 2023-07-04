using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Services.Interfaces;

namespace RabbitMQ.Services
{
    public class Consumer : InitializeRabbitMQ, IConsumer
    {
        public object? ConsumeMessage(string exchangeName, string exchangeType, string? routingKey)
        {
            if (Channel == null)
                return null;

            string jsonObject = "";

            Channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

            // declare a server-named queue
            var queueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queue: queueName,
                              exchange: exchangeName,
                              routingKey: routingKey);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                jsonObject = Encoding.UTF8.GetString(body);
            };

            Channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return jsonObject;
        }
    }
}
