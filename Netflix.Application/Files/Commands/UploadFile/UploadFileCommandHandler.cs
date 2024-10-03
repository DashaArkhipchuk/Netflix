using MediatR;
using Netflix.Application.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Files.Commands.UploadFile
{
    internal class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly ICloudStorageService _cloudStorageService;

        public UploadFileCommandHandler(ICloudStorageService cloudStorageService)
        {
            _cloudStorageService = cloudStorageService;
        }

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            return await _cloudStorageService.UploadBlobAsync(request.File);
        }
    }
}
