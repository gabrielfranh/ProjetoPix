using KeyAPI.Useful.Enums;

namespace KeyAPI.DTO
{
    public class KeyDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public KeyType Type { get; set; }
        public string KeyNumber { get; set; }
        public int CostumerId { get; set; }
    }
}
