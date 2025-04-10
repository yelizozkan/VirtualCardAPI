namespace VirtualCardAPI.DTOs.VirtualCard
{
    public class VirtualCardCreateRequest
    {
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Balance { get; set; }
    }
}
