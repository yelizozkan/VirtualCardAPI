namespace VirtualCardAPI.Services.Abstract
{
    public interface ICardValidationService
    {
        bool IsValidCardNumber(string cardNumber);
    }
}
