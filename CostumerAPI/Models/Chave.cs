namespace CostumerAPI.Models
{
    public class Chave
    {
        public Guid Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public long IdCliente { get; set; }
    }
}
