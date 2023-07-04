namespace RabbitMQService.Services.Interfaces
{
    public interface IMessageProcessor
    {
        public void PublishMessage(string exchange, string exchangeType, string routingKey, string message);
        public void SubscribeToMessages(string exchange, string exchangeType, string queue, string routingKey, Func<string, Task> messageHandler);
    }
}
