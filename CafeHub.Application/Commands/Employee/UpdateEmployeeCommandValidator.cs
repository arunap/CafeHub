using System.Text.RegularExpressions;
using FluentValidation;

namespace CafeHub.Application.Commands.Employee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Employee ID is required.");

            RuleFor(x => x.CafeId)
                .NotEmpty().WithMessage("Cafe ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(new Regex(@"^\+65\d{8}$")).WithMessage("Phone number must be a valid Singapore number starting with +65 and followed by 8 digits.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender is required and must be valid.");
        }
    }
}