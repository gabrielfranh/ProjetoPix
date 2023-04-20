using KeyAPI.Useful.Enums;

namespace KeyAPI.Models
{
    public class Key
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public KeyType Type { get; set; }
        public string KeyNumber { get; set; }
        public int CostumerId { get; set; }
    }
}
