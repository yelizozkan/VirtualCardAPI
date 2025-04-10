using FluentValidation;
using VirtualCardAPI.DTOs.VirtualCard;
using VirtualCardAPI.Models;

namespace VirtualCardAPI.Validators
{
    public class VirtualCardValidator : AbstractValidator<VirtualCardCreateRequest>
    {
        public VirtualCardValidator()
        {
            RuleFor(x => x.CardNumber)
               .NotEmpty()
               .CreditCard();

            RuleFor(x => x.CardHolder)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.ExpirationDate)
                .GreaterThan(DateTime.Now).WithMessage("Son kullanma tarihi geçmiş olamaz.");

            RuleFor(x => x.Balance)
                .GreaterThanOrEqualTo(0);
        }   
    }
}
