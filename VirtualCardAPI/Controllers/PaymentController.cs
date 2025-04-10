using Microsoft.AspNetCore.Mvc;
using VirtualCardAPI.Services.Abstract;

namespace VirtualCardAPI.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly ICardValidationService _cardValidationService;
        private readonly IPaymentGatewayService _paymentGatewayService;

        public PaymentController(ICardValidationService cardValidationService, IPaymentGatewayService paymentGatewayService)
        {
            _cardValidationService = cardValidationService;
            _paymentGatewayService = paymentGatewayService;
        }

        [HttpPost("process-payment")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            if (!_cardValidationService.IsValidCardNumber(paymentRequest.CardNumber))
            {
                return BadRequest("Invalid card number.");
            }

            var paymentResult = _paymentGatewayService.ProcessPayment(paymentRequest.CardNumber, paymentRequest.Amount);

            if (paymentResult)
            {
                return Ok("Payment processed successfully.");
            }

            return BadRequest("Payment failed.");
        }
    }

    public class PaymentRequest
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }

}
