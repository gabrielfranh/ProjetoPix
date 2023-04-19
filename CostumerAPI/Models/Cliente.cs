using System.ComponentModel.DataAnnotations;

namespace CostumerAPI.Models
{
    public class Cliente
    {
        [Required]
        public long Id { get; set; }

        public string Nome { get; set; }
    }
}
