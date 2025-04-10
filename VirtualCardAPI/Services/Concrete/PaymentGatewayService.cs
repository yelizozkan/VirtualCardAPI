using VirtualCardAPI.Services.Abstract;

namespace VirtualCardAPI.Services.Concrete
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        public bool ProcessPayment(string cardNumber, decimal amount)
        {
            // Fake ödeme işlem simülasyonu
            if (cardNumber == "1234 5678 9012 3456" && amount <= 1000)
            {
                return true; // Ödeme başarılı
            }
            return false; // Ödeme başarısız
        }
    }
}
