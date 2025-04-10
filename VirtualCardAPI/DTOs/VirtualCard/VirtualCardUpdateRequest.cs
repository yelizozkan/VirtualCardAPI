namespace VirtualCardAPI.DTOs.VirtualCard
{
    public class VirtualCardUpdateRequest
    {
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
