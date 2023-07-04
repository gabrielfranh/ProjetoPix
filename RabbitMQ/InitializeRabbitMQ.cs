using RabbitMQ.Client;

namespace RabbitMQ
{
    public class InitializeRabbitMQ
    {
        public static IModel Channel { get; set; }

        public InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            Channel = connection.CreateModel();
        }
    }
}
