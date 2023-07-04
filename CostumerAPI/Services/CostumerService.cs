using CostumerAPI.DTO;
using CostumerAPI.Repositories.Interface;
using CostumerAPI.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQService.Services.Interfaces;

namespace CostumerAPI.Services
{
    public class CostumerService : ICostumerService
    {
        private readonly ICostumerRepository _costumerRepository;
        private readonly int _itarator = 3;
        private readonly string _pepper;
        private readonly IMessageProcessor _messageProcessor;

        public CostumerService(ICostumerRepository costumerRepository, IMessageProcessor messageProcessor)
        {
            _costumerRepository = costumerRepository;
            _pepper = Environment.GetEnvironmentVariable("Pepper234#$");
            _messageProcessor = messageProcessor;
        }

        public async Task<CostumerDTO> GetCostumerById(int id)
        {
            return await _costumerRepository.GetCostumerById(id);
        }

        public async Task<CostumerDTO> Create(CostumerDTO costumer)
        {
            var salt = PasswordHasher.GenerateSalt();
            costumer.PasswordHash = PasswordHasher.ComputeHash(costumer.Password, salt, _pepper, _itarator);
            costumer.Password = "";

            var consumerCreated =  await _costumerRepository.Create(costumer);
            consumerCreated.Salt= salt;

            PublishMessage("direct_exchange", ExchangeType.Direct, "user_create", consumerCreated);
            
            return consumerCreated;
        }

        public async Task<CostumerDTO> Update(CostumerDTO costumer)
        {
            return await _costumerRepository.Update(costumer);
        }

        public async Task DeleteCostumerById(int id)
        {
            await _costumerRepository.DeleteCostumerById(id);
        }

        private void PublishMessage(string exchangeName, string exchangeType, string routingKey, object message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);

            if (message == null)
                throw new Exception("Mensagem nula");
            
            _messageProcessor.PublishMessage(exchangeName, exchangeType, routingKey, jsonMessage);
        }
    }
}
