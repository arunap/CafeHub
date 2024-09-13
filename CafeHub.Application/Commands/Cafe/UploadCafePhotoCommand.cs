using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Application.Commands.Cafe
{
    public class UploadCafePhotoCommand : IRequest<string>
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }

    public class UploadCafePhotoCommandHandler : IRequestHandler<UploadCafePhotoCommand, string>
    {
        private const string CAFE_IMAGE_UPLOADS = "wwwroot/uploads/cafeImages";
        public async Task<string> Handle(UploadCafePhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.FormFile.FileName)}";
                var filePath = Path.Combine(CAFE_IMAGE_UPLOADS, fileName);
                System.IO.Directory.CreateDirectory(CAFE_IMAGE_UPLOADS);

                using var stream = new FileStream(filePath, FileMode.Create);
                await request.FormFile.CopyToAsync(stream);

                return fileName;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }
    }
}