using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Common.Services
{
    public interface ICloudStorageService
    {
        Task<string> UploadBlobAsync(IFormFile file);

        Task<string> RemoveBlobByUrlAsync(string url);
    }
}
