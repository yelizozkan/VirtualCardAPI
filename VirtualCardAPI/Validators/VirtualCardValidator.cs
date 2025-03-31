using FluentValidation;
using VirtualCardAPI.Models;

namespace VirtualCardAPI.Validators
{
    public class VirtualCardValidator : AbstractValidator<VirtualCard>
    {
        public VirtualCardValidator()
        {
            RuleFor(c => c.CardNumber)
                .NotEmpty()
                .CreditCard().WithMessage("Invalid credit card number format");

            RuleFor(c => c.CardHolder)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Card holder name cannot exceed 50 characters")
                .Matches("^[a-zA-Z ]+$").WithMessage("Card holder name should only contain letters and spaces");

            RuleFor(c => c.ExpirationDate)
                .GreaterThan(DateTime.Now)
                .LessThan(DateTime.Now.AddYears(10)).WithMessage("Expiration date cannot be more than 10 years in the future");

            RuleFor(c => c.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative");
        }   
    }
}
