using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Netflix.Application.Common.Errors;
using Netflix.Application.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Netflix.Infrastructure.Services
{
    public class CloudStorageService : ICloudStorageService
    {
        private const string ContainerName = "submissionmedia";
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        private readonly Dictionary<string, string> _contentTypes = new Dictionary<string, string>()
        {
            {".avi", "video/x-msvideo"},   
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg" },
            {".mp4", "video/mp4"},
            {".png", "image/png"},    
        };

        public CloudStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task<string> RemoveBlobByUrlAsync(string url)
        {
            try
            {
                var parts = url.Split('/');
                var filename = parts[parts.Length - 1];
                if (filename.Split('.').Length != 2)
                {
                    throw new ParsingValidationException(url, "Invalid url");
                }
                var blobClient = _containerClient.GetBlobClient(filename);
                await blobClient.DeleteIfExistsAsync();

                return filename;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing file file: " + ex.Message);
            }
        }

        public async Task<string> UploadBlobAsync(IFormFile file)
        {
            try
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

                if (file.Length > (100 * 1024 * 1024)) // Limit to 100 MB
                {
                    throw new Exception("The file is too large.");
                }

                var filename = DateTime.Now.Ticks.ToString() + extension;

                var blobClient = _containerClient.GetBlobClient(filename);

                var blobHttpHeader = new BlobHttpHeaders { ContentType = _contentTypes[extension] };

                await blobClient.UploadAsync(file.OpenReadStream(), new BlobUploadOptions { HttpHeaders = blobHttpHeader });

                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while uploading the file: " + ex.Message);
            }
        }
    }
}
