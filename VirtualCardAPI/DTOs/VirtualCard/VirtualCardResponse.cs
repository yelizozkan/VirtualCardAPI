namespace VirtualCardAPI.DTOs.VirtualCard
{
    public class VirtualCardResponse
    {
        public int Id { get; set; }
        public string CardHolder { get; set; }
        public string MaskedCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
