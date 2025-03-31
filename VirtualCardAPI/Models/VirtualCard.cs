namespace VirtualCardAPI.Models
{
    public class VirtualCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }


    }
}

    

