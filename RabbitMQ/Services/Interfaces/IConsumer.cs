namespace RabbitMQ.Services.Interfaces
{
    internal interface IConsumer
    {
        public object? ConsumeMessage(string exchangeName, string exchangeType, string? routingKey);
    }
}
