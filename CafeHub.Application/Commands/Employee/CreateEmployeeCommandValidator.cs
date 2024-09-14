using System.Text.RegularExpressions;
using FluentValidation;

namespace CafeHub.Application.Commands.Employee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            // CafeId validation
            // RuleFor(x => x.CafeId)
            //     .NotEmpty().WithMessage("CafeId is required.");

            // Name validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            // EmailAddress validation
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            // PhoneNumber validation
            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone number is required.")
           .Matches(new Regex(@"^\+65\d{8}$")).WithMessage("Phone number must be a valid Singapore number starting with +65 and followed by 8 digits.");


            // Gender validation
            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender must be a valid value.");
        }
    }
}