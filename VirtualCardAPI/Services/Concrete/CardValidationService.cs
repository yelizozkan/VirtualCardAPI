using VirtualCardAPI.Services.Abstract;

namespace VirtualCardAPI.Services.Concrete
{
    public class CardValidationService : ICardValidationService
    {
        // Fake kart doğrulama: Gerçek ödeme sağlayıcı API'si olmadan test edilebilir
        private readonly List<string> _validCardNumbers = new List<string>
        {
            "1234 5678 9012 3456", // Fake kart numarası
            "2345 6789 0123 4567"  // Fake kart numarası
        };

        public bool IsValidCardNumber(string cardNumber)
        {
            // Fake doğrulama: Kart numarasını listede arıyoruz
            return _validCardNumbers.Contains(cardNumber);
        }
    }
}
