using Azure.Core;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Netflix.Application.Common.Content;
using Netflix.Application.Common.Errors;
using Netflix.Application.Files.Commands.UploadFile;
using Netflix.Contracts.Common;
using Netflix.Domain;
using System.Linq;

namespace Netflix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("File not selected");
            }

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".avi", ".mp4" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                throw new Exception("Invalid file type");
            }

            if (file.Length > (50*1024*1024)) // Limit to 50 MB
            {
                throw new Exception("The file is too large.");
            }

            var command = new UploadFileCommand(file);

            var fileUrl = await _mediator.Send(command);
            

            return Ok(fileUrl);
        }
    }
}
