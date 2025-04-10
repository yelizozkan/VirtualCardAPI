namespace VirtualCardAPI.Services.Abstract
{
    public interface IPaymentGatewayService
    {
        bool ProcessPayment(string cardNumber, decimal amount);
    }
}
