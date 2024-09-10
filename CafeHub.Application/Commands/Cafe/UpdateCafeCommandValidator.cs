using FluentValidation;

namespace CafeHub.Application.Commands.Cafe
{
    public class UpdateCafeCommandValidator : AbstractValidator<UpdateCafeCommand>
    {
        public UpdateCafeCommandValidator()
        {
            RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Must(x => x.Length > 6 && x.Length < 10).WithMessage("Minimum 6 character and max 10 characters");

            RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(256).WithMessage("Description cannot exceed 256 characters");

            RuleFor(c => c.Location)
            .NotEmpty().WithMessage("Location is required.");
        }
    }
}