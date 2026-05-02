using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Netflix.Application.Common.Errors;
using Netflix.Application.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Services
{
    internal class CloudinaryCloudStorageService : ICloudStorageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".avi", ".mp4" };
        private readonly string[] _videoExtensions = { ".avi", ".mp4" };

        public CloudinaryCloudStorageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadBlobAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new Exception("File not selected");

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(extension) || !_permittedExtensions.Contains(extension))
                    throw new Exception("Invalid file type");

                if (file.Length > 100 * 1024 * 1024)
                    throw new Exception("The file is too large.");

                // Use Guid instead of Ticks to avoid duplicates on parallel uploads
                var publicId = Guid.NewGuid().ToString();

                // Read stream into memory first to avoid stream sharing issues in parallel
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                if (_videoExtensions.Contains(extension))
                {
                    var uploadParams = new VideoUploadParams
                    {
                        File = new FileDescription(file.FileName, memoryStream),
                        PublicId = publicId,
                        Folder = "submissionmedia"
                    };
                    var uploadResult = await _cloudinary.UploadLargeAsync(uploadParams, 6000000);

                    if (uploadResult.Error != null)
                        throw new Exception(uploadResult.Error.Message);

                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, memoryStream),
                        PublicId = publicId,
                        Folder = "submissionmedia"
                    };
                    var uploadResult = await _cloudinary.UploadLargeAsync(uploadParams, 6000000);


                    if (uploadResult.Error != null)
                        throw new Exception(uploadResult.Error.Message);

                    return uploadResult.SecureUrl.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while uploading the file: " + ex.Message);
            }
        }

        public async Task<string> RemoveBlobByUrlAsync(string url)
        {
            try
            {
                // Extract public ID from Cloudinary URL
                // URL format: https://res.cloudinary.com/{cloud}/image/upload/v{version}/{folder}/{publicId}.{ext}
                var uri = new Uri(url);
                var segments = uri.AbsolutePath.Split('/');
                var uploadIndex = Array.IndexOf(segments, "upload");

                if (uploadIndex == -1)
                    throw new ParsingValidationException(url, "Invalid Cloudinary URL");

                // Skip "upload" and version segment (v1234567)
                var publicIdWithExtension = string.Join("/", segments.Skip(uploadIndex + 2));
                var publicId = Path.ChangeExtension(publicIdWithExtension, null);

                var resourceType = url.Contains("/video/") ? ResourceType.Video : ResourceType.Image;

                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = resourceType
                };

                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Error != null)
                    throw new Exception(result.Error.Message);

                return publicId;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing the file: " + ex.Message);
            }
        }
    }
}
