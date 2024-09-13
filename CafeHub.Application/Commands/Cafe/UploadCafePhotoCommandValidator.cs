using FluentValidation;

namespace CafeHub.Application.Commands.Cafe
{
    public class UploadCafePhotoCommandValidator : AbstractValidator<UploadCafePhotoCommand>
    {
        public UploadCafePhotoCommandValidator()
        {

        }
    }
}