namespace RabbitMQ.Services.Interfaces
{
    internal interface IPublisher
    {
        public bool PublishMessage(object message, string exchangeName, string exchangeType, string? routingKey); 
    }
}
